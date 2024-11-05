using Microsoft.AspNetCore.Mvc;
using WebDB.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly WebDbContext _context;

        public PhoneController(WebDbContext context)
        {
            _context = context;
        }

        // GET: api/Phone
        [HttpGet]
        public ActionResult<IEnumerable<Phone>> GetPhones()
        {
            return _context.Phones.ToList();
        }

        // GET: api/Phone/5
        [HttpGet("{phoneNumber}")]
        public ActionResult<Phone> GetPhone(string phoneNumber)
        {
            var phone = _context.Phones.Find(phoneNumber);

            if (phone == null)
            {
                return NotFound();
            }

            return phone;
        }

        // POST: api/Phone
        [HttpPost]
        public ActionResult<Phone> PostPhone(Phone phone)
        {
            _context.Phones.Add(phone);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPhone), new { phoneNumber = phone.PhoneNumber }, phone);
        }

        // PUT: api/Phone/5
        [HttpPut("{phoneNumber}")]
        public IActionResult PutPhone(string phoneNumber, Phone phone)
        {
            if (phoneNumber != phone.PhoneNumber)
            {
                return BadRequest();
            }

            _context.Entry(phone).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Phone/5
        [HttpDelete("{phoneNumber}")]
        public IActionResult DeletePhone(string phoneNumber)
        {
            var phone = _context.Phones.Find(phoneNumber);

            if (phone == null)
            {
                return NotFound();
            }

            _context.Phones.Remove(phone);
            _context.SaveChanges();

            return NoContent();
        }
    }
}