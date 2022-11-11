using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HMT.Web.Server.Data;
using MediatR;
using FluentValidation;
using HMT.Web.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using HMT.Web.Server.Services;
using Microsoft.AspNetCore.Identity;
using HMT.Web.Server.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services we're adding - start

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddDbContext<HMTDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HMTConnection")));

builder.Services.AddDefaultIdentity<HMTUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HMTDbContext>();

builder.Services.AddScoped<TokenProvider>();

builder.Services.AddSingleton<IInMemoryDatabase, InMemoryDatabase>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// builder.Host.UseSerilogLoggingSetup();
// Services we're adding - end

builder.Services.AddRazorPages(options => options.RootDirectory = "/Features");
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

// Make the implicit Program class public so test projects can access it. I got this from Microsoft docs: - AshishK
// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0#basic-tests-with-the-default-webapplicationfactory
public partial class Program { }