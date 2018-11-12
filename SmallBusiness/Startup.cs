using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmallBusiness.Models;
using SmallBusiness.Models.Repositories;
using SmallBusiness.Models.Repositories.EFRepositories;
using SmallBusiness.Services;
using System.Security.Claims;

namespace SmallBusiness
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration["ConnectionStrings:SmallBusiness"]));

            services.AddTransient<ICustomerRepository, EFCustomerRepository>();
            services.AddTransient<IUserRepository, EFUserRepository>();
            services.AddTransient<IGenderRepository, EFGenderRepository>();
            services.AddTransient<ICityRepository, EFCityRepository>();
            services.AddTransient<IRegionRepository, EFRegionRepository>();
            services.AddTransient<IClassificationRepository, EFClassificationRepository>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy =>
                policy.RequireAssertion(context =>
                context.User.HasClaim(c =>
                c.Type == ClaimTypes.Role &&
                c.Value == "Administrator")));
                options.AddPolicy("IsSameSeller", policy => policy.Requirements.Add(new OperationAuthorizationRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var _customer = new { controller = "Customer", action = "List" };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "Login",
                    defaults: new
                    {
                        controller = "User",
                        action = "Login"
                    });

                routes.MapRoute(
                    name: null,
                    template: "Customer",
                    defaults: _customer);

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: _customer);
            });
        }
    }
}
