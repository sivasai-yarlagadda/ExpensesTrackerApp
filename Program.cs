using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//============================Added =================================================

// ✅ Register EF Core and MySQL
builder.Services.AddDbContext<ExpenseTrackerContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(5, 6, 0)) // ⬅️ match your MySQL version here
    ));

//====================================================================================
var app = builder.Build();

// ✅ Automatically create the database if it doesn’t exist
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExpenseTrackerContext>();
    // Option 1: Creates DB if missing (no migrations)
    db.Database.EnsureCreated();

    // OR Option 2 (recommended if you use EF migrations):
    // db.Database.Migrate();

    //Creating Default Tables with Columns
    DbInitializer.Initialize(db); // Seeds default data

}

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
