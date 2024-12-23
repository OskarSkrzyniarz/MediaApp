using IdentityAPI.Data;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer("Server=appDB;Database=IdentityDB;User=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"));
builder.Services.AddControllers();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Users.Any())
    {
        dbContext.Users.AddRange(new List<User>
        {
            new User { Username = "osskpl", Password = "pass123" },
            new User { Username = "jankowalski", Password = "haslo" },
            new User { Username = "nowann", Password = "adminadmin" }
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

app.MapGet("/", () => "IdentityAPI is running...");

// Run the application
app.Run();
