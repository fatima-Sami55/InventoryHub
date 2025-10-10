using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


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

app.MapGet("/api/productlist", () =>
{
    var products = new[]
    {
        new {
            Id = 1,
            Name = "Laptop",
            Price = 1200.5,
            Stock = 25,
            Category = new { Id = 101, Name = "Electronics" }
        },
        new {
            Id = 2,
            Name = "Headphones",
            Price = 50.0,
            Stock = 100,
            Category = new { Id = 102, Name = "Accessories" }
        },
    };

    var options = new System.Text.Json.JsonSerializerOptions
    {
        PropertyNamingPolicy = null
    };

    return Results.Json(products, options);
});


app.Run();
