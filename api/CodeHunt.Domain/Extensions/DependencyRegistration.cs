using CodeHunt.Domain.Mappers;
using CodeHunt.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHunt.Domain.Extensions
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            // Admin Mappers
            services
                .AddSingleton<ICollectableItemMapper, CollectableItemMapper>()
                .AddSingleton<IGameMapper, GameMapper>()
                .AddSingleton<IGuardMapper, GuardMapper>()
                .AddSingleton<ITeamMapper, TeamMapper>();

            // Team Mappers
            services
                .AddSingleton<IAnonymousCollectableItemMapper, AnonymousCollectableItemMapper>()
                .AddSingleton<ICollectedItemMapper, CollectedItemMapper>()
                .AddSingleton<ITeamSightingMapper, TeamSightingMapper>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<ICollectableItemService, CollectableItemService>()
                .AddScoped<ICollectedItemService, CollectedItemService>()
                .AddScoped<IGameService, GameService>()
                .AddScoped<ISightingService, SightingService>()
                .AddScoped<ITeamService, TeamService>();

            return services;
        }
    }
}
