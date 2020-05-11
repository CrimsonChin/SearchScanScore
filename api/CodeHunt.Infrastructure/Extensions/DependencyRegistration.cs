using CodeHunt.Domain.Repositories;
using CodeHunt.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHunt.Infrastructure.Extensions
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<ICollectedItemRepository, CollectedItemRepository>()
                .AddScoped<IGameRepository, GameRepository>()
                .AddScoped<ISightingRepository, SightingRepository>()
                .AddScoped<ITeamRepository, TeamRepository>();

            return services;
        }
    }
}
