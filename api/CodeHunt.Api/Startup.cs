using CodeHunt.Api.Hubs;
using CodeHunt.Api.NotificationServices;
using CodeHunt.Domain.Repositories;
using CodeHunt.Domain.Services;
using CodeHunt.Infrastructure.Data;
using CodeHunt.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CodeHunt.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // TODO Domain
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IGuardService, GuardService>();
            services.AddScoped<ITeamNotificationService, TeamNotificationService>();

            // TODO Infrastructure
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ICollectedItemRepository, CollectedItemRepository>();
            services.AddScoped<ISightingRepository, SightingRepository>();

            // TODO bootstrap
            services.AddMemoryCache();
            services.AddDbContext<CodeHuntContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddMemoryCache();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                }));

            services.AddSignalR();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Code Hunt", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors("CorsPolicy");

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Hunt API V1");
                });

            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TeamHub>("/teamHub");
            });
        }
    }
}
