﻿using BENKIEN.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<DatabaseContext>(); 

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => {
    x.LoginPath = "/Management/Login"; // Login sisteminin varsayılan login giriş adresini kendi adresimizle değiştiriyoruz.
    x.Cookie.Name = "ManagementLogin"; // oturum için oluşacak cookie nin ismini belirledik.
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.MaxAge = TimeSpan.FromHours(12);
    x.ExpireTimeSpan = TimeSpan.FromHours(12);
    x.SlidingExpiration = true;
    x.Cookie.HttpOnly = true;
    x.Cookie.SameSite = SameSiteMode.Lax;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
