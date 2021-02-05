using ePasieka.Models;
using ePasieka.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ePasieka
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            //  Wstrzykiwanie pe³nego wystapienia identifikatorów i skonfigurowanie kontekstu bazy danych.
            //  Zawsze od nowa jest tworzony DI kontrolera aby ró¿ni u¿ytkownicy mogli wykorzystywaæ wywo³ania tej samej us³ugi.

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            //Identity
            services.AddDefaultIdentity<User>(options =>
            {
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Default Lockout settings || 5 min, 3 Failed 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                // For demo (Max = 100)
                options.Lockout.MaxFailedAccessAttempts = 100;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>();


            //Cookie
            services.ConfigureApplicationCookie(o => {
                o.ExpireTimeSpan = TimeSpan.FromDays(5);
                o.SlidingExpiration = true;
            });

            //SendGrid 
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //Tworzona jest instancja na czas trwania ca³ego zadania(HTTP) --> AddScoped
            services.AddScoped<IBeeGardenRepository, BeeGardenRepository>();
            services.AddScoped<IBeehiveRepository, BeehiveRepository>();
            services.AddScoped<INucRepository, NucRepository>();
            services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();


            services.AddControllersWithViews();
            services.AddRazorPages();
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Uruchamia uwierzetelnienie
            app.UseAuthentication();
            //Uruchamia autoryzacjê
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
