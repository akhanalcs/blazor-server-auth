using HMT.Web.Server.Data;
using Microsoft.EntityFrameworkCore;
using HMT.Web.Server.Areas.Identity;
using HMT.Web.Server.Features.WeatherForecasts;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services we're adding - start

builder.Services.AddDbContext<HMTDbContext>(options =>
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
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<HMTUser>>();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<DbInitializer>(); // To initialize the database

// Services we're adding - end

builder.Services.AddRazorPages(options => options.RootDirectory = "/Features"); // Added this to rename Pages to Features
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    // Initialize the Db:
    using var scope = app.Services.CreateAsyncScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await dbInitializer.InitializeAsync();
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