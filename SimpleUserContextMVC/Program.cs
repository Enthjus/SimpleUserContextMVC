using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Refit;
using SimpleUser.MVC.Authorization;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Middlewares;
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
builder.Services.AddHttpContextAccessor();
builder.Services
    .AddRefitClient<ICustomerApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7037/"))
    .AddHttpMessageHandler<AccessTokenHandler>();
//builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddHttpClient<ICustomerService, CustomerService>("httpClient", client => {
//    // Assume this is an "external" service which requires an API KEY
//    client.BaseAddress = new Uri("https://localhost:7037/");
//    // GitHub API versioning
//    // GitHub requires a user-agent
//    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
//}).AddHttpMessageHandler<AccessTokenHandler>();

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

//app.Use(async (context, next) =>
//{
//    var JWToken = context.Request.Cookies["JWToken"];
//    if (!string.IsNullOrEmpty(JWToken))
//    {
//        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
//    }
//    await next();
//});
app.UseMiddleware<TokenQueryParameterMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();

void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdmin", policy => policy.RequireClaim("Administrator", "Administrator"));
        options.AddPolicy("RequireManager", policy => policy.RequireClaim("Manager", "Manager"));
        options.AddPolicy("RequireUser", policy => policy.RequireClaim("User", "User"));
    });
}

void AddScoped()
{
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
    builder.Services.AddScoped<AccessTokenHandler>();
    builder.Services.AddScoped<IValidator<CustomerCreateDto>, CustomerCreateValidator>();
    builder.Services.AddScoped<IValidator<CustomerUpdateDto>, CustomerUpdateValidator>();
    builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
}

void AddSingleton()
{
    builder.Services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
    builder.Services.AddSingleton<IAuthorizationHandler, IsSuperHandler>();
}
