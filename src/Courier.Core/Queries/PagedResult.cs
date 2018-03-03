using System.Collections.Generic;
using System.Linq;

namespace Courier.Core.Queries
{
    public class PagedResult<T> : PagedResultBase
    {
        public IEnumerable<T> Results { get; set; }
        public bool IsEmpty => Results == null || !Results.Any();

        private PagedResult()
        {
            Results = Enumerable.Empty<T>();
        }

        private PagedResult(IEnumerable<T> results,
            int currentPage, int resultsPerPage,
            int totalResults, int totalPages)
            {
                Results = results;
                CurrentPage = currentPage;
                ResultsPerPage = resultsPerPage;
                TotalResults = totalResults;
                TotalPages = totalPages;
            }

        public static PagedResult<T> Create(IEnumerable<T> results,
            int currentPage, int resultsPerPage,
            int totalResults, int totalPages)
            => new PagedResult<T>(results, currentPage, resultsPerPage,
                totalResults, totalPages);

        public static PagedResult<T> Empty => new PagedResult<T>();
    }
}