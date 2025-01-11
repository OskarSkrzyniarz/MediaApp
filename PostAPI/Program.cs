using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostAPI.Data;
using PostAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure services
builder.Services.AddDbContext<PostDbContext>(options =>
    options.UseSqlServer("Server=appDB;Database=PostDB;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"));

builder.Services.AddControllers();

var app = builder.Build();

// Seed sample data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PostDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Posts.Any())
    {
        dbContext.Posts.AddRange(new List<Post>
        {
            new Post { Title = "Hello World", Content = "This is the first post.", CreatedAt = DateTime.Now },
            new Post { Title = "Docker Setup", Content = "Learn how to set up Docker.", CreatedAt = DateTime.Now.AddDays(-1) },
            new Post { Title = "Microservices", Content = "Introduction to microservices architecture.", CreatedAt = DateTime.Now.AddDays(-2) }
        });
        dbContext.SaveChanges();
    }
}

// Configure middleware
app.UseCors();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("/", () => "PostAPI is running...");

// Run the application
app.Run();