using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace MyWebAPI
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
            IdentityModelEventSource.ShowPII = true;

            //services.AddAuthentication("Bearer").AddJwtBearer("Bearer",
            // options =>
            // {
            //     options.Authority = "https://localhost:5001";
            //     options.Audience = "api1";
            //     options.RequireHttpsMetadata = false;

            //     //options.TokenValidationParameters = new TokenValidationParameters()
            //     //{
            //     //    ValidateAudience = false
            //     //};
            // });

            services.AddAuthentication("Bearer")
              .AddIdentityServerAuthentication(options =>
              {
                  options.Authority = "https://localhost:5001";
                  options.RequireHttpsMetadata = false;
                  options.ApiName = "api1";
              });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
