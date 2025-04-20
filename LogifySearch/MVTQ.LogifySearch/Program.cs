using Microsoft.Identity.Web;
using MVTQ.LogifySearch.Domain.Enums;
using MVTQ.LogifySearch.Domain.Extensions;
using MVTQ.LogifySearch.Function;
using MVTQ.LogifySearch.Infrastructure;
using MVTQ.LogifySearch.Infrastructure.Identity;
using MVTQ.LogifySearch.Application;
using MVTQ.LogifySearch.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web.UI;
using MVTQ.LogifySearch.Domain.Setting;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddDomainLayer(configuration);
builder.Services.AddInfrastructureLayer(configuration);
builder.Services.AddApplicationLayer(configuration);

builder.Services.Configure<DefaultUserSetting>(
    builder.Configuration.GetSection("DefaultUsers"));


builder.Services.AddControllersWithViews(options =>
{

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.EnsureDataBaseCreated(configuration);


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
