using Microsoft.AspNetCore.Mvc;
using PeopleAPI.Data;
using PeopleAPI.Models;

namespace PeopleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public PeopleController(PeopleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Person> Get() => _context.People.ToList();

        [HttpPost]
        public IActionResult Create(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = person.Id }, person);
        }
    }
}
