using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Business.Utils.Extensions;
using Data.Utils.Extensions;
using BookWebApp.Utils.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//set db context for data layer
builder.Services.ConfigureSqlContextForDataLayer(builder.Configuration);
//identity config
builder.Services.ConfigureIdentity();
//cookie config
builder.Services.ConfigureCookie();
//dependency injection for Data Layer
builder.Services.setInterfaceConcretesForDataLayer();
//dependency injection for Business Layer
builder.Services.setInterfaceConcretesForBusinessLayer();

//automapper for main project(this project)
builder.Services.setAutoMapperForMainLayer();
//automapper for Data layer
builder.Services.setAutoMapperForDataLayer();
//automapper for Business layer
builder.Services.setAutoMapperForBusinessLayer();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name : "myTestRoute",
    pattern:"test/add",
    defaults : new { controller = "MyTest", action = "AddRoleAsync" }
	);


app.Run();
