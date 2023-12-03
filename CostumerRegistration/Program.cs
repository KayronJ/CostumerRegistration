global using CostumerRegistration.Models;
global using CostumerRegistration.DAL;
using Microsoft.EntityFrameworkCore;
using CostumerRegistration;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using CostumerRegistration.Services.ImageHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
    
var app = builder.Build();
//With that we can run "dotnet run seeddata" to seed our databasewh

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Costumer}/{action=Home}/{id?}");

app.Run();
