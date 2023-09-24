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

    public static IEnumerable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, PaginationParams @params)
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

        if (HttpContextHelper.ResponseHeaders != null)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
        }

        return @params.PageIndex > 0 && @params.PageSize > 0 ?
            entities.OrderBy(e => e.Id)
                .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize) :
                    throw new CustomException(400, "Please, enter valid numbers");
    }

    public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> collect, Filter filter)
    {
        if (filter.OrderBy is null)
            return collect;

        var property = typeof(TEntity).GetProperties().FirstOrDefault(n
            => n.Name.ToLower().Equals(filter.OrderBy.ToLower())
            );

        if (property is null)
            return collect;

        if (filter.IsDesc)
            return collect.OrderByDescending(x => property);

        return collect.OrderBy(x => property);
    }
}