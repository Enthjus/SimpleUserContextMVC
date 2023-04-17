using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Services;
using SimpleUser.MVC.Validators;
using SimpleUser.Persistence.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services
.AddHttpClient();
//builder.Services.AddDbContext<ApplicationContext>(options =>
//  options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();
builder.Services.AddScoped<IValidator<UserCreateDto>, UserCreateValidator>();
builder.Services.AddScoped<IValidator<UserUpdateDto>, UserUpdateValidator>();

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

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var context = services.GetRequiredService<ApplicationContext>();
//    //context.Database.EnsureCreated();
//    DbInitializer.Initialize(context);
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              