using Microsoft.AspNetCore.Mvc;
using WebDB.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly WebDbContext _context;

        public OrderController(WebDbContext context)
        {
            _context = context;
            context.IsConnectionOpen();
        }

        // GET: api/Order
        [HttpGet("{pageNumber?}/{pageSize?}/{sortBy?}/{sortDescending?}")]
        public ActionResult<IEnumerable<Order>> GetOrders(int pageNumber = 1, int pageSize = 10, string sortBy = "OrderId", bool sortDescending = true)
        {   
            // TODO: add a cache option to recall last request from user session
            // Assemble lambda from user input then pass to query
            Expression<Func<Order, int>> lambdaQuery;
            IOrderedQueryable<Order> ordersQuery;
            // Assemble lambda from user input
            switch (sortBy)
            {
                case "OrderId": lambdaQuery = o => o.OrderId; break;
                case "BillingCustomerId": lambdaQuery = o => o.BillingCustomerId; break;
                case "ShippingCustomerId": lambdaQuery = o => o.ShippingCustomerId; break;
                default:
                    return BadRequest("Invalid sort by parameter");
            }
            // Pass lambda to query and sort
            if(sortDescending)
            {
                ordersQuery = _context.Orders.OrderByDescending(lambdaQuery);
            }
            else
            {
                ordersQuery = _context.Orders.OrderBy(lambdaQuery);
            }       
            // Apply pagination
            var paginatedQuery = ordersQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        
            return Ok(paginatedQuery);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/Order
        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.BillingCustomerId }, order);
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.BillingCustomerId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return NoContent();
        }

        // GET: api/Order/recent/5
        [HttpGet("recent/{count?}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetRecentOrders(int count=1)
        {
            return await _context.Orders.OrderByDescending(o => o.OrderId).Take(count).ToListAsync();
        }
    }
}