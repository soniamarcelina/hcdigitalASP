using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using hcdigital.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
  options.UseMySql(connString, ServerVersion.AutoDetect(connString)).LogTo(Console.WriteLine)
);



// private void ConfigureServices(IServiceCollection services)
//  {
//     services.AddLogging(builder =>
//     {
//         builder.AddConsole();  // Menambahkan penyimpanan log konsol
//     });

//  }

builder.Services.AddControllersWithViews();
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

app.UseHttpsRedirection(); // Middleware HTTPS redirection


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


