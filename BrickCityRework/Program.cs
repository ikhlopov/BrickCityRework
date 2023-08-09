using NLog;
using NLog.Web;
using System;
using BrickCityRework.Data;
using Microsoft.EntityFrameworkCore;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Database connection services
    builder.Services.AddDbContext<BrickCityContext>
        (options => options
            .UseSqlServer(builder.Configuration.GetConnectionString("BrickCityContext")));

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    else
    {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
    }

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<BrickCityContext>();
        context.Database.EnsureCreated();
    }


    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=File}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}