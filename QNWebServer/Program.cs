using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using QNWebServer.Data;
using QNWebServer.Admin;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//db create
builder.Services.AddHttpClient();
builder.Services.AddSqlite<UserContext>("Data Source=users.db");
builder.Services.AddSqlite<ProjectContext>("Data Source=project.db");
builder.Services.AddSqlite<FileAssetContext>("Data Source=fileasset.db");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<NewProject>();
builder.Services.AddScoped<NewAsset>();
builder.Services.AddScoped<NewTask>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

//init user db

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserContext>();
    if (db.Database.EnsureCreated())
    {
        InitServer.InitUserDb(db);
    }

    var db_p = scope.ServiceProvider.GetRequiredService<ProjectContext>();
    if (db_p.Database.EnsureCreated())
    {
        InitServer.InitProjectDb(db_p);
    }

    var db_d = scope.ServiceProvider.GetRequiredService<FileAssetContext>();
    if (db_d.Database.EnsureCreated())
    {
        InitServer.InitFileAssetDb(db_d);
    }
}

app.Run();
