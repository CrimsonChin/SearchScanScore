using System;

namespace CodeHunt.Domain.Models
{
    public class Sighting
    {
        public DateTime SightedAt { get; set; }

        public string SightedBy { get; set; }
    }
}
