using System;
using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Entities;

namespace CodeHunt.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CodeHuntContext context)
        {
            context.Database.EnsureCreated();

            if (context.Games.Any())
            {
                return;   // DB has been seeded
            }

            context.Games.Add(new Game
            {
                GameId = 1,
                ExternalId = Guid.NewGuid(),
                Code = "UVWMN",
                Name = "Example GameResponse"
            });

            context.SaveChanges();

            var teams = new[]
            {
                new Team
                {
                    TeamId = 1,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "Vikings",
                    Code = "VJG4Q"
                },
                new Team
                {
                    TeamId = 2,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "Saxons",
                    Code = "A8THQ"
                },
                new Team
                {
                    TeamId = 3,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "Mercians",
                    Code = "FJ7EM"
                },
            };


            foreach (var team in teams)
            {
                context.Teams.Add(team);
            }

            context.SaveChanges();

            var guards = new[]
            {
                new Guard
                {
                    GuardId = 1,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "Strict Jeremy",
                    Code = "Y709W"
                },
                new Guard
                {
                    GuardId = 2,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "Watchful Mark",
                    Code = "JA9L6"
                }
            };

            foreach (var guard in guards)
            {
                context.Guards.Add(guard);
            }

            context.SaveChanges();

            var collectableItems = new List<CollectableItem>
            {
                new CollectableItem
                {
                    CollectableItemId = 1,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "The concrete cows inspired the brew",
                    Code = "VU75T"
                },
                new CollectableItem
                {
                    CollectableItemId = 2,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "What 3 words? stair/jacket/palm",
                    Code = "65EFU"
                },
                new CollectableItem
                {
                    CollectableItemId = 3,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "If you need a lift but raffles won't do.  It's not the distance that's the problem.",
                    Code = "75GF0",
                },
                new CollectableItem
                {
                    CollectableItemId = 4,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "What has face and two arms but no body?",
                    Code = "YMDVJ",
                },
                new CollectableItem
                {
                    CollectableItemId = 5,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "The home of the code breakers, stationed outside",
                    Code = "ID1ZO",
                },
                new CollectableItem
                {
                    CollectableItemId = 6,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "A beacon.",
                    Code = "P0DN9",
                },
                new CollectableItem
                {
                    CollectableItemId = 7,
                    ExternalId = Guid.NewGuid(),
                    GameId = 1,
                    Name = "Three greats, three golds.  Leaping into view in a roundabout way.",
                    Code = "BQEY7",
                }
            };

            foreach (var collectableItem in collectableItems)
            {
                context.CollectableItems.Add(collectableItem);
            }

            context.SaveChanges();
        }
    }
}
