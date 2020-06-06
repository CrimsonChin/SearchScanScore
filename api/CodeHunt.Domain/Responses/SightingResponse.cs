using System;

namespace CodeHunt.Domain.Responses
{
    public class SightingResponse
    {
        public Guid ExternalId { get; set; }

        public DateTime SightedAt { get; set; }

        public string SightedBy { get; set; }
    }
}
