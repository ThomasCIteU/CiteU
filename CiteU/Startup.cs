using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiteU.Models.Helper;
using DatabaseAccess.Assemblee;
using DatabaseAccess.Droit;
using DatabaseAccess.Langue;
using DatabaseAccess.Login;
using DatabaseAccess.Pole;
using DatabaseAccess.Reunion;
using DatabaseAccess.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CiteU
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPoleRepository, PoleRepository>();
            services.AddTransient<ILangueRepository, LangueRepository>();
            services.AddTransient<IAssembleeRepository, AssembleeRepository>();
            services.AddTransient<IReunionRepository, ReunionRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IDroitRepository, DroitRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ClaimCiteU.Administrateur, policy => policy.RequireClaim("Droit", ClaimCiteU.Administrateur));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ClaimCiteU.Responsable, policy => policy.RequireClaim("Droit", ClaimCiteU.Responsable, ClaimCiteU.Administrateur));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Proclamateur", policy => policy.RequireClaim("Droit", ClaimCiteU.Proclamateur, ClaimCiteU.Responsable, ClaimCiteU.Administrateur));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //because we are in dev actually
                app.UseDeveloperExceptionPage();

                //app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
