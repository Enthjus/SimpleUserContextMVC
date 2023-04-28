using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleUser.MVC.Areas.Identity.Data;
using SimpleUser.MVC.Authorization;
using SimpleUser.MVC.Data;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Services;
using SimpleUser.MVC.Validators;
using System.Runtime.Intrinsics.X86;

var builder = WebApplication.CreateBuilder(args);

AddAuthorizationPolicies();

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

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
app.UseAuthentication(); ;
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

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
    builder.Services.AddScoped<IValidator<CustomerCreateDto>, CustomerCreateValidator>();
    builder.Services.AddScoped<IValidator<CustomerUpdateDto>, CustomerUpdateValidator>();
    builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
}

void AddSingleton()
{
    builder.Services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
    builder.Services.AddSingleton<IAuthorizationHandler, IsSuperHandler>();
}
