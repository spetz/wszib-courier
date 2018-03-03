using System;
using System.Collections.Generic;
using System.Linq;

namespace Courier.Core.Queries
{
    public static class Pagination
    {
        public static PagedResult<T> Paginate<T>(this IEnumerable<T> values, PagedQueryBase query)
        {
            if (values == null || !values.Any())
            {
                return PagedResult<T>.Empty;
            }
            if (query.Page <= 0)
            {
                query.Page = 1;
            }
            if (query.Results <= 0)
            {
                query.Results = 10;
            }
            var totalResults = values.Count();
            var totalPages = Math.Ceiling((double)totalResults / query.Results);
            var paginatedValues = values.Skip(query.Page-1).Take(query.Results);

            return PagedResult<T>.Create(paginatedValues, query.Page, query.Results,
                totalResults, (int)totalPages);
        }
    }
}