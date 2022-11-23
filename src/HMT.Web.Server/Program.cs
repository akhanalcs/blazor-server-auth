using HMT.Web.Server.Data;
using Microsoft.EntityFrameworkCore;
using HMT.Web.Server.Areas.Identity;
using HMT.Web.Server.Features.WeatherForecasts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services we're adding - start

builder.Services.AddDbContext<HMTDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HMTConnection")));

builder.Services.AddDefaultIdentity<HMTUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HMTDbContext>();

builder.Services.AddScoped<TokenProvider>();

builder.Services.AddSingleton<WeatherForecastService>();

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