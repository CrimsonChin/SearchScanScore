using System;

namespace CodeHunt.Domain.Entities
{
    public class Sighting
    {
        public int SightingId { get; set; }

        public DateTime SightedAt { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int GuardId { get; set; }
        public Guard Guard { get; set; }
    }
}
