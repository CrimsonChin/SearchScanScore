using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Sql.Entities
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GameId { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Team> Teams { get; set; }
        public ICollection<Guard> Guards { get; set; }
        public ICollection<CollectableItem> CollectableItems { get; set; }
    }
}
