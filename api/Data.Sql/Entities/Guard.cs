using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Data.Sql.Entities
{
    public class Guard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GuardId { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public IEnumerable<Sighting> Sightings { get; set; }
    }
}
