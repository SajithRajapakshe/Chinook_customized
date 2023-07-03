using Chinook;
using Chinook.Areas.Identity;
using Chinook.Models;
using Chinook.Services;
using ChinookDataAccessLayer;
using ChinookDataAccessLayer.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Changed the configuration with Service life time of Dependency
builder.Services.AddDbContextFactory<ChinookContext>(opt =>
{
    opt.UseSqlite(connectionString);
    opt.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ChinookUserData>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ChinookContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ChinookUserData>>();

//Registered separate services to perform actions related to Artists, Tracks and Playlists
builder.Services.AddTransient<ArtistsService>();
builder.Services.AddTransient<TracksService>();
builder.Services.AddTransient<PlayListsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

//Registered two new controllers in order to accept ajaxx calls.
app.MapControllerRoute(
    name: "playList",
    pattern: "{controller=PlayLists}/{action=AddPlayList}/{playListName?}");

app.MapControllerRoute(
    name: "artist",
    pattern: "{controller=Artist}/{action=SearchArtist}/{artistName?}");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
