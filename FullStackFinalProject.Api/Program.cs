using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.MapGet("/api/productlist", () =>
{
    var products = new[]
    {
        new { Id = 1, Name = "Laptop", Price = 1200.5, Stock = 25 },
        new { Id = 2, Name = "Headphones", Price = 50.0, Stock = 100 },
    };

    return Results.Json(products);
});

app.Run();
