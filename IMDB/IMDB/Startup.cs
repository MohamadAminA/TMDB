using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.DataLayer;
using IMDB.Services.Api;
using IMDB.Services.Database;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IMDB
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
            services.AddControllersWithViews();

            #region Authorization
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
            });
            #endregion

           #region DataBase Context
            services.AddDbContext<ContextDB>(options =>
                options.UseSqlServer(
                   
                    "Data Source=.;Initial Catalog=IMDB_DB;Integrated Security=true;MultipleActiveResultSets=true;",
                    //"Data Source=DESKTOP-0QSKDOG;Initial Catalog=IMDB_DB;Integrated Security=true;MultipleActiveResultSets=true;",
                    b => b.MigrationsAssembly("IMDB.DataLayer")),
                    ServiceLifetime.Transient
            );
            #endregion

            #region Add Services to asp core
            services.AddTransient<IMovie, MovieRepo>();
            services.AddTransient<IUser, UserRepo>();
            services.AddTransient<ReviewRepo, ReviewRepo>();

            services.AddTransient<MovieListRepo, MovieListRepo>();

            #endregion

            #region Caching
            services.AddResponseCaching();
            services.AddMemoryCache();
            #endregion


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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            #region security
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
