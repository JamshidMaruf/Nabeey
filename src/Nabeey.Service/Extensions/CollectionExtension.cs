using Newtonsoft.Json;
using Nabeey.Domain.Commons;
using Nabeey.Service.Helpers;
using Nabeey.Service.Exceptions;
using Nabeey.Domain.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Nabeey.Service.Extensions;

public static class CollectionExtension
{
    public static IQueryable<T> ToPaginate<T>(this IQueryable<T> values, PaginationParams @params)
    {
        var source = values.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);
        return source;
    }

    public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, PaginationParams @params)
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

        if(HttpContextHelper.ResponseHeaders is not null)
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

    public static async Task<IEnumerable<T>> OrderByAsync<T>(this IQueryable<T> collect, Filter filter)
    {
        if (filter.OrderBy is null)
            return collect;
        if (filter.IsDesc)
            return await (from n in collect
                    orderby GetValueFromProperty(n, filter.OrderBy) descending
                    select n).ToListAsync();

        return await (from n in collect
                orderby GetValueFromProperty(n, filter.OrderBy) ascending
                select n).ToListAsync();
    }

    private static object GetValueFromProperty(object item, string propName)
         => item.GetType().GetProperties()
            .FirstOrDefault(property 
                => property.Name.Contains(propName, StringComparison.OrdinalIgnoreCase))
            .GetValue(item);
}