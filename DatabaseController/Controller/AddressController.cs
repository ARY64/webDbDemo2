using Microsoft.AspNetCore.Mvc;
using WebDB.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly WebDbContext _context;

        public AddressController(WebDbContext context)
        {
            _context = context;
        }

        // GET: api/Address
        [HttpGet]
        public ActionResult<IEnumerable<Address>> GetAddresses()
        {
            return _context.Addresses.ToList();
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public ActionResult<Address> GetAddress(int id)
        {
            var address = _context.Addresses.Find(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // POST: api/Address
        [HttpPost]
        public ActionResult<Address> PostAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId }, address);
        }

        // PUT: api/Address/5
        [HttpPut("{id}")]
        public IActionResult PutAddress(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Address/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var address = _context.Addresses.Find(id);

            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            _context.SaveChanges();

            return NoContent();
        }
    }
}