using Newtonsoft.Json;
using Nabeey.Domain.Commons;
using Nabeey.Service.Helpers;
using Nabeey.Service.Exceptions;
using Nabeey.Domain.Configurations;

namespace Nabeey.Service.Extensions;

public static class CollectionExtension
{
    public static IQueryable<T> ToPaginate<T>(this IQueryable<T> values, PaginationParams @params)
    {
        var source = values.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);
        return source;
    }

	public static IEnumerable<T> ToPaginate<T>(this IEnumerable<T> values, PaginationParams @params)
	{
		var source = values.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);
		return source;
	}

	public static IEnumerable<TEntity> ToPagedList<TEntity>(this IEnumerable<TEntity> entities, PaginationParams @params)
        where TEntity : Auditable
    {
        if (@params.PageSize == 0 && @params.PageIndex == 0)
        {
            @params = new PaginationParams()
            {
                PageSize = 10,
                PageIndex = 1
            };
        }
        var metaData = new PaginationMetaData(entities.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);

       /* if(HttpContextHelper.ResponseHeaders is not null)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
        }*/

        return @params.PageIndex > 0 && @params.PageSize > 0 ?
            entities.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize) :
                    throw new CustomException(400, "Please, enter valid numbers");
    }

    public static IEnumerable<TEntity> OrderBy<TEntity>(this IEnumerable<TEntity> collect, Filter filter)
    {
        var prop = filter.OrderBy ?? "Id";

        var property = typeof(TEntity).GetProperties().FirstOrDefault(n
            => n.Name.Equals(prop, StringComparison.OrdinalIgnoreCase));

        property ??= typeof(TEntity).GetProperty("Id");

        if (property.Name is "Id" && !filter.IsDesc)
            return collect;

        if (filter.IsDesc)
            return collect.OrderByDescending(x => property.GetValue(x));

        return collect.OrderBy(x => property.GetValue(x));
    }
}