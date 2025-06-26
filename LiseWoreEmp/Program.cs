using LiseWoreEmp.Models;
using LiseWoreEmp.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

namespace LiseWoreEmp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add DbContext with connection string
            builder.Services.AddDbContext<LiseWoreEmpContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("dbConn"))
                       .EnableSensitiveDataLogging());

            // Register DataSecurityProvider
            builder.Services.AddSingleton<DataSecurityProvider>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.LoginPath = "/User/Login";
                o.LogoutPath = "/User/Logout";
                o.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                o.SlidingExpiration = true;
            });
            builder.Services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(10);
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Static}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
