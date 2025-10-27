using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BCrypt.Net;
using FullStackFinalProject.Api.Data;
using FullStackFinalProject.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5070")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowBlazorClient");

app.MapGet("/api/productlist", async (AppDbContext db, string? role) =>
{
    if (role != "Admin")
        return Results.Json(new { message = "Admins are allowed" });

    var products = await db.Products
        .Include(p => p.Category)
        .Select(p => new
        {
            p.Id,
            p.Name,
            p.Price,
            p.Stock,
            Category = p.Category == null ? null : new { p.Category.Id, p.Category.Name }
        })
        .ToListAsync();

    return Results.Json(products);
});

app.MapPost("/api/register", async (AppDbContext db, User user) =>
{
    if (await db.Users.AnyAsync(u => u.Email == user.Email))
        return Results.BadRequest("Email already exists.");

    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

    // Assign role based on email, overriding any client-provided value
    user.Role = user.Email.EndsWith("@admin.com", StringComparison.OrdinalIgnoreCase) ? "Admin" : "User";

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Ok("User registered successfully.");
});

app.MapPost("/api/login", async (AppDbContext db, LoginRequest login) =>
{
    var user = await db.Users
        .FromSqlRaw("SELECT * FROM Users WHERE Email = @email", new SqlParameter("@email", login.Email))
        .FirstOrDefaultAsync();

    if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
        return Results.Unauthorized();

    return Results.Ok(new { user.Username, user.Role });
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Categories.Any())
    {
        var electronics = new Category { Name = "Electronics" };
        var accessories = new Category { Name = "Accessories" };
        db.Categories.AddRange(electronics, accessories);
        db.SaveChanges();

        db.Products.AddRange(
            new Product { Name = "Laptop", Price = 1200.5, Stock = 25, CategoryId = electronics.Id },
            new Product { Name = "Headphones", Price = 50.0, Stock = 100, CategoryId = accessories.Id }
        );
        db.SaveChanges();
    }
}

app.Run();

record LoginRequest(string Email, string Password);
