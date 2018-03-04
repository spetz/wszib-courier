using System;

namespace Courier.Core.Queries
{
    public class BrowseParcels : PagedQueryBase
    {
        public bool Mine { get; set; }
        public Guid UserId { get; set; }
    }
}