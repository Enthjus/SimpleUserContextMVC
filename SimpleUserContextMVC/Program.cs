using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleUser.MVC.Authorization;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Services;
using SimpleUser.MVC.Validators;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

AddAuthorizationPolicies();
builder.Services.AddDbContext<IdentityDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFluentValidationAutoValidation();

AddScoped();
AddSingleton();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    var JWToken = context.Request.Cookies["JWToken"];
    if (!string.IsNullOrEmpty(JWToken))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
    }
    await next();
});

app.UseAuthentication(); ;
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();

void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("SuperAdmin", policy => policy.RequireClaim("Admin", "Admin"));
        options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Administrator"));
        options.AddPolicy("RequireManager", policy => policy.RequireRole("Manager"));
        options.AddPolicy("CanDeleteUser",
        policyBuilder =>
            policyBuilder.AddRequirements(
                new IsAllowedToDeleteUserRequirement(),
                new IsAccountSuperAdminRequirement()
            ));
    });
}

void AddScoped()
{
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IValidator<CustomerCreateDto>, CustomerCreateValidator>();
    builder.Services.AddScoped<IValidator<CustomerUpdateDto>, CustomerUpdateValidator>();
    builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
}

void AddSingleton()
{
    builder.Services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
    builder.Services.AddSingleton<IAuthorizationHandler, IsSuperHandler>();
}
