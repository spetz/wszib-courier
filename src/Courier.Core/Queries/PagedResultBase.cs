namespace Courier.Core.Queries
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int ResultsPerPage { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
    }
}