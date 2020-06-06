using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeHunt.Domain.Entities
{
    public class Guard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GuardId { get; set; }

        public Guid ExternalId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public IEnumerable<Sighting> Sightings { get; set; }
    }
}
