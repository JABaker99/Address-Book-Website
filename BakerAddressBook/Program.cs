/// <summary>
/// Baker Address Book Web Application
/// Author: Jacob Baker
/// Created: 2025-09-21
/// Description:
/// This is the entry point for the Baker Address Book ASP.NET Core application.
/// It sets up services, middleware, routing, and starts the application.
/// </summary>
using Microsoft.EntityFrameworkCore;
using BakerAddressBook.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BakerAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BakerAddressBookContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Starts the application.
app.Run();
