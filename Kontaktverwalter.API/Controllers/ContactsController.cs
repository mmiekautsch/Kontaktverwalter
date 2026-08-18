using Microsoft.AspNetCore.Mvc;
using Kontaktverwalter.API.DBModel;
using Kontaktverwalter.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace Kontaktverwalter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactManagerDbContext _context;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ContactManagerDbContext context, ILogger<ContactsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllContacts()
        {
            _logger.LogInformation("Retrieving all contacts");
            var contacts = await _context.ViewFullContactInfos
                .Select(c => new ContactDto
                {
                    IdPerson = c.IdPerson,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    DateOfBirth = c.DateOfBirth,
                    PostalCode = c.PostalCode,
                    City = c.City,
                    StreetName = c.StreetName,
                    StreetNumber = c.StreetNumber,
                    Country = c.Country,
                    PhoneNumber = c.PhoneNumber,
                    Type = c.Type
                })
                .ToListAsync();
            return Ok(contacts);
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> SearchForContact(string query)
        {
            _logger.LogInformation("Retrieving contact with query: {Query}", query);
            var contacts = await _context.ViewFullContactInfos
                .Where(c => (c.FirstName + " " + c.LastName).Contains(query))
                .Select(c => new ContactDto
                {
                    IdPerson = c.IdPerson,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    DateOfBirth = c.DateOfBirth,
                    PostalCode = c.PostalCode,
                    City = c.City,
                    StreetName = c.StreetName,
                    StreetNumber = c.StreetNumber,
                    Country = c.Country,
                    PhoneNumber = c.PhoneNumber,
                    Type = c.Type
                })
                .ToListAsync();
            return Ok(contacts);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateContact(long id, [FromBody] UpdateContactDto request)
        {
            _logger.LogInformation("Updating contact with ID: {Id}", id);

            var contact = await _context.People.FindAsync(id);
            if (contact == null)
            {
                _logger.LogWarning("Contact with ID: {Id} not found", id);
                return NotFound();
            }

            contact.FirstName = request.FirstName;
            contact.LastName = request.LastName;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}