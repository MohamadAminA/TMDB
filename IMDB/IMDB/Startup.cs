using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChatRoom;
using IMDB.DataLayer;
using IMDB.Services.Api;
using IMDB.Services.Database;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
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
            //services.Configure<ForwardedHeadersOptions>(options =>
            //{
            //    options.ForwardLimit = 2;
            //    options.KnownProxies.Add(IPAddress.Parse("127.0.10.1"));
            //    options.ForwardedForHeaderName = "X-Forwarded-For-My-Custom-Header-Name";
            //});
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

                    //"Data Source=AMIN-LAPTOP\\SQLEXPRESS;Initial Catalog=IMDB_DB;Integrated Security=true;MultipleActiveResultSets=true;",
                    "Data Source=DESKTOP-0QSKDOG;Initial Catalog=IMDB_DB;Integrated Security=true;MultipleActiveResultSets=true;",
                    b => b.MigrationsAssembly("IMDB.DataLayer")),
                    ServiceLifetime.Transient
            );
            #endregion

            services.AddSignalR();

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
            //app.UseForwardedHeaders();
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

            //app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/"), builder => builder.RunProxy(new ProxyOptions
            //{
            //    Scheme = "https",
            //    Host = "103.151.177.106",
            //    Port = "80"
            //}));
            //https://free-proxy-list.net/
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chathub");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
