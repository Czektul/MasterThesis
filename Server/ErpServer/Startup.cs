using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ErpServer.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace ErpServer
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
            services.AddJsonLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddViewLocalization();
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL");
            services.Configure<RequestLocalizationOptions>
                (opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                            new CultureInfo("pl-PL"),
                            new CultureInfo("en-GB")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("pl-PL");
                        // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });
            //DB connection
            var connection = @"Server=DESKTOP-TRNJHOC;Database=ERP_Database;Trusted_Connection=True;";
            services.AddDbContext<ERP_DatabaseContext>(options => options.UseSqlServer(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseStaticFiles();
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "UserPanel",
                    template: "{controller=UserPanel}/{action=Register}/{id?}");
            });
        }
    }
}
