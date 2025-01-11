using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PeopleAPI.Data;
using PeopleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddDbContext<PeopleDbContext>(options =>
    options.UseSqlServer("Server=appDB;Database=PeopleDB;User=sa;Password=YourStrong@Passw0rd;;TrustServerCertificate=True;"));
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PeopleDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.People.Any())
    {
        dbContext.People.AddRange(new List<Person>
        {
            new Person { Name = "Oskar Skrzyniarz", Email = "oskar.skrzyniarz@mail.pl" },
            new Person { Name = "Jan Kowalski", Email = "jan.kowal@mail.pl" },
            new Person { Name = "Anna Nowak", Email = "anna.k13131@mail.pl" },
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

app.MapGet("/", () => "PeopleAPI is running...");

// Run the application
app.Run();