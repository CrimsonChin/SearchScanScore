using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Sql.Entities
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeamId { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public ICollection<CollectedItem> CollectedItems { get; set; }
    }
}
