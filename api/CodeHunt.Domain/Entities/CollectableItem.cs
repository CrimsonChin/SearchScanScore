using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeHunt.Domain.Entities
{
    public class CollectableItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CollectableItemId { get; set; }

        public Guid ExternalId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
