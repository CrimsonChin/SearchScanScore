﻿using System;
using System.Linq;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Repositories;
using CodeHunt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeHunt.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly CodeHuntContext _context;

        public GameRepository(CodeHuntContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Game Get(string gameExternalId)
        {
            var game = _context.Games
                .Include(x => x.Teams)
                .Include(x => x.Guards)
                .Include(x => x.CollectableItems)
                .SingleOrDefault(x => x.ExternalId == gameExternalId);

            return game;
        }

        public Game Update(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            return game;
        }

        public void Reset(string gameExternalId)
        {
            var game = _context.Games
                .Include(x => x.Teams)
                .ThenInclude(x => x.CollectedItems) 
                .Include(x => x.Teams)
                .ThenInclude(x => x.Sightings)
                .SingleOrDefault(x => x.ExternalId == gameExternalId);

            game.IsActive = false;

            foreach (var team in game.Teams)
            {
                _context.CollectedItems.RemoveRange(team.CollectedItems);
                _context.Sightings.RemoveRange(team.Sightings);
            }
        }
    }
}
