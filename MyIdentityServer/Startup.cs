using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentityServer
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
          //  services.AddRazorPages();
            services.AddControllersWithViews();
            IdentityModelEventSource.ShowPII = true;
            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddInMemoryApiScopes(Config.GetApiScopes())
               .AddInMemoryApiResources(Config.GetApiResources())
               .AddInMemoryClients(Config.GetClients())
               .AddInMemoryIdentityResources(Config.GetIdentityResources())
               .AddTestUsers(Config.GetUsers());

            services.AddAuthentication()
                  .AddGoogle("Google", options =>
                  {
                      options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                      options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                      options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                  });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();            
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
               // endpoints.MapRazorPages();
            });
        }
    }
}
