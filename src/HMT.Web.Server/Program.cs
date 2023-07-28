using HMT.Web.Server.Data;
using Microsoft.EntityFrameworkCore;
using HMT.Web.Server.Areas.Identity;
using HMT.Web.Server.Features.WeatherForecasts;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services we're adding - start

builder.Services.AddDbContextFactory<HMTDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HMTConnection")));

builder.Services
.AddDefaultIdentity<HMTUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    // Set Password options here if you'd like:
    // Make sure you're consistent with Data Annotation rules defined in Areas/Identity/Pages/Account/Register.cshtml.cs file
    options.Password.RequiredLength = 6;
})
.AddRoles<HMTRole>()
.AddEntityFrameworkStores<HMTDbContext>();

builder.Services.AddScoped<TokenProvider>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<DbInitializer>(); // To initialize the database

builder.Services.AddRazorPages(options => options.RootDirectory = "/Features"); // Added this to rename Pages to Features
builder.Services.AddServerSideBlazor();

// Learned the hard way that this needs to be added after setting up Blazor (i.e. AddRazorPages, AddServerSideBlazor) - AshishK
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<HMTUser>>();

// To ensure custom claims are added to new identity when principal is refreshed.
builder.Services.ConfigureOptions<ConfigureSecurityStampOptions>();

// Services we're adding - end

var app = builder.Build();

// Initialize the Db:
using var scope = app.Services.CreateAsyncScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// @*Added by Identity Scaffolder*@
app.UseAuthentication();
app.UseAuthorization();

app.Run();