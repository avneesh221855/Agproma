using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AgpromaWebAPI.model;
//using AgpromaWebAPI.Hubs;
using Microsoft.EntityFrameworkCore;
using Serilog;
using AgpromaWebAPI.Repository;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.Hubs;
//using AgProMa.Services;
//using AgProMa.Repository;

namespace AgpromaWebAPI
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AgpromaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSignalR();
            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<IStoryRepository, StoryRepository>();
            services.AddScoped<IEpicServices, EpicService>();
            services.AddScoped<IEpicRepository, EpicRepository>();
           // services.AddScoped<IReleasePlansRepo, ReleasePlansRep/o>();
            //services.AddScoped<IReleasePlansService, ReleasePlansService>();
            //services.AddScoped<ISprintRepository, SprintRepository>();
            //services.AddScoped<ISprintService, SprintService>();
           // services.AddScoped<ICheckListRepository, ChecklistRepository>();
            //services.AddScoped<ICheckListService, ChecklistService>();
           // services.AddScoped<ITaskRepository, TaskRepository>();
           // services.AddScoped<ITaskServices, TaskService>();
            //services.AddScoped<ITaskBacklogReposiory, TaskBacklogRepository>();
            //services.AddScoped<ITaskBacklogService, TaskBacklogService>();
            //services.AddScoped<IEfficiencyService, EfficiencyService>();
            //services.AddScoped<IEfficiencyRepository, EfficiencyRepository>();
            //services.AddScoped<IBurndownService, BurndownService>();
            //services.AddScoped<IBurndownRepository, BurndownRepository>();
            services.AddCors();
            services.AddSingleton(Configuration);
            // Add framework services.
            ConfigureJwtAuthService(Configuration, services);
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseSignalR(routes =>
            {
                routes.MapHub<EpicHub>("epichub");
               // routes.MapHub<SprintBacklogHub>("sprint");
                //routes.MapHub<ReleasePlansHub>("releaseplan");
               // routes.MapHub<TeamHub>("teamhub");
                //routes.MapHub<TaskBacklogHub>("taskbacklog");
                routes.MapHub<BacklogHub>("backlog");
                //routes.MapHub<TaskHub>("taskhub");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
