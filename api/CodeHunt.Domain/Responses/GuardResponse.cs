using System;

namespace CodeHunt.Domain.Responses
{
    public class GuardResponse
    {
        public Guid ExternalId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
