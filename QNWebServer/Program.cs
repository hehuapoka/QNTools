using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using QNWebServer.Data;
using QNWebServer.Admin;

var builder = WebApplication.CreateBuilder(args);
//db create
builder.Services.AddHttpClient();
builder.Services.AddSqlite<UserConetext>("Data Source=users.db");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

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
    var db = scope.ServiceProvider.GetRequiredService<UserConetext>();
    if (db.Database.EnsureCreated())
    {
        InitServer.InitUserDb(db);
    }
}

app.Run();
