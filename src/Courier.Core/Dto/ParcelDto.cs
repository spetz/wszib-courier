using System;

namespace Courier.Core.Dto
{
    public class ParcelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime SentAt { get; set; }
        public bool Received { get; set; }
    }
}