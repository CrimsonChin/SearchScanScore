using System;

namespace CodeHunt.Domain.Responses
{
    public class TeamSightingResponse
    {
        public DateTime SightedAt { get; set; }

        public string SightedBy { get; set; }
    }
}
