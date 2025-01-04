using Microsoft.EntityFrameworkCore;
using ProductDB.Data;
using ProductDB.Models;

var builder = WebApplication.CreateBuilder(args);

// Ensure you have the necessary MySQL package
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Add controllers and views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure middleware for production and development environments
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Force HTTPS redirection
app.UseHttpsRedirection();

// Serve static files like CSS, JavaScript, and images
app.UseStaticFiles();

// Use routing to map incoming requests to appropriate controllers
app.UseRouting();

// Enable authorization if needed (based on your requirements)
app.UseAuthorization();

// Set up the default route for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application
app.Run();
