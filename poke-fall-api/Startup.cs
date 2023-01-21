using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using poke_fall_api.Context;

namespace poke_fall_api
{
    public class Startup
    {
        private string AllowAll = "AllowAll";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PokefallContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("PokefallContext")));
            services.AddControllers();
            services.AddCors( options => 
            {
                options.AddPolicy(name: AllowAll,
                                builder => 
                                {
                                    builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                });
            });
            services.AddHttpClient();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "pokefall_api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "pokefall_api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowAll);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}