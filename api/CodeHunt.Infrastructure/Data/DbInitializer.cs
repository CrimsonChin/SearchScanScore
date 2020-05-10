using System.Collections.Generic;
using CodeHunt.Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;

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
                ExternalId = "UVWMN",
                Name = "Example GameResponse"
            });

            context.SaveChanges();

            var teams = new[]
            {
                new Team
                {
                    TeamId = 1,
                    GameId = 1,
                    Name = "Vikings",
                    ExternalId = "VJG4Q"
                },
                new Team
                {
                    TeamId = 2,
                    GameId = 1,
                    Name = "Saxons",
                    ExternalId = "A8THQ"
                },
                new Team
                {
                    TeamId = 3,
                    GameId = 1,
                    Name = "Mercians",
                    ExternalId = "FJ7EM"
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
                    GameId = 1,
                    Name = "Strict Jeremy",
                    ExternalId = "Y709W"
                },
                new Guard
                {
                    GuardId = 2,
                    GameId = 1,
                    Name = "Watchful Mark",
                    ExternalId = "JA9L6"
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
                    GameId = 1,
                    Name = "Behind the book case",
                    ExternalId = "VU75T"
                },
                new CollectableItem
                {
                    CollectableItemId = 2,
                    GameId = 1,
                    Name = "Neither up nor down.  stair/jacket/palm",
                    ExternalId = "65EFU"
                },
                new CollectableItem
                {
                    CollectableItemId = 3,
                    GameId = 1,
                    Name = "The site of the most famous burglary in Bletchley",
                    ExternalId = "75GF0",
                },
                new CollectableItem
                {
                    CollectableItemId = 4,
                    GameId = 1,
                    Name = "What has face and two arms but no body?",
                    ExternalId = "YMDVJ",
                },
                new CollectableItem
                {
                    CollectableItemId = 5,
                    GameId = 1,
                    Name = "The war, the thought, the passing of time...",
                    ExternalId = "ID1ZO",
                },
                new CollectableItem
                {
                    CollectableItemId = 6,
                    GameId = 1,
                    Name = "Watch",
                    ExternalId = "P0DN9",
                },
                new CollectableItem
                {
                    CollectableItemId = 7,
                    GameId = 1,
                    Name = "A sculpture... or a blemish?",
                    ExternalId = "BQEY7",
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
