using Microsoft.EntityFrameworkCore;
using PeopleAPI.Models;
using System;

namespace PeopleAPI.Data
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options) { }
        public DbSet<Person> People { get; set; }
    }
}
