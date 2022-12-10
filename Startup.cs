using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTracker.Data;
using TheTracker.Models;
using TheTracker.Services;
using TheTracker.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheTracker.Services.Factories;

namespace TheTracker
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(DataUtility.GetConnectionString(Configuration),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddDatabaseDeveloperPageExceptionFilter();
            ////MODIFY #01 Intro
            services.AddIdentity<TTUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<TTUserClaimsPrincipalFactory>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            // MODIFY #5 Services / Role Services (part 4)
            services.AddScoped<ITTRolesService, TTRolesService>();
            // ADD #9 Services / CompanyInfo Service (part 4)
            services.AddScoped<ITTCompanyInfoService, TTCompanyInfoService>();
            // ADD #15 Services / Project Service (part 5)
            services.AddScoped<ITTProjectService, TTProjectService>();
            // ADD #21  Services / Ticket Service (part 5)
            services.AddScoped<ITTTicketService, TTTicketService>();
            // ADD #23 Services / Ticket History Service (part 2)
            services.AddScoped<ITTTicketHistoryService, TTTicketHistoryService>();
            // ADD #25 Services / Notification Service
            services.AddScoped<ITTNotificationService, TTNotificationService>();
            // ADD #26 Services / Invite Service
            services.AddScoped<ITTInviteService, TTInviteService>();
            // ADD #27 Services / File Service
            services.AddScoped<ITTFileService, TTFileService>();

            // ADD #24 Services / Email Service
            services.AddScoped<IEmailSender, TTEmailService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddControllersWithViews();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
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