using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDB.Models;

namespace WebDB.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly WebDbContext _context;

        public OrderItemController(WebDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderItem/Index
        [HttpGet("Index")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> Index()
        {
            var lastOrderId = await _context.OrderItems.MaxAsync(o => o.OrderId);
            return await _context.OrderItems.Where(o => o.OrderId == lastOrderId).ToListAsync();
        }

        // GET: api/OrderItem/Index/{id}
        [HttpGet("Index/{id}")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> Index(int id)
        {
            return await _context.OrderItems.Where(o => o.OrderId == id).ToListAsync();
        }

        // GET: api/OrderItem/Details/5
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<OrderItem>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }

        // GET: api/OrderItem/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return BadRequest("This method is not supported for creating order items.");
        }

           // POST: api/OrderItem/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,ShippingCustomerId,DatePlaced,DateProcessed")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                // Check if the OrderId already exists
                var existingOrderItem = await _context.OrderItems.FindAsync(orderItem.OrderId);
                if (existingOrderItem != null)
                {
                    return Conflict(new { message = "OrderId already exists." });
                }
        
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return Ok(orderItem);
            }
            return BadRequest(ModelState);
        }

        // GET: api/OrderItem/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItems.FindAsync(id);
            if (orderItems == null)
            {
                return NotFound();
            }
            return Ok(orderItems);
        }

        // POST: api/OrderItem/Edit/5
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,ShippingCustomerId,DatePlaced,DateProcessed")] OrderItem orderItem)
        {
            if (id != orderItem.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(orderItem.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(orderItem);
            }
            return BadRequest(ModelState);
        }

        // POST: api/OrderItem/Delete/5
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if(orderItem == null)
            {
                return NotFound();
            }
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return Ok(orderItem);
        }


public class OrderItemDto
{
    required public String SessionToken { get; set; }
    public int OrderItemId { get; set;}
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int Position { get; set; }
    required public string Action { get; set; } // "create", "update", or "delete"
    public string? Metadata { get; set; } // Additional metadata
}
     
[HttpPost("UpdateOrderItem/{OrderId}")]
public async Task<IActionResult> UpdateOrderItem([FromBody] OrderItemDto orderItems){
    if (ModelState.IsValid){
        
    }
    Order order = await _context.Orders.FindAsync(orderItems.OrderId);
}

[HttpPost("CreateMultiple")]
public async Task<IActionResult> CreateMultiple([FromBody] List<OrderItemDto> orderItems)
{
    if (ModelState.IsValid)
    {
        // Check if required Order and Item exists
        foreach (var orderItemDto in orderItems)
        {
            // Check if OrderId matches previous order or look if Order exists
        }
            /*
            case 1: no previous OrderItem = add OrderItem
            case 2: previous OrderItem exists = update OrderItem
            case 3: previous OrderItem exists but replaced with empty OrderItem = delete OrderItem


            */

        if (Order.OrderExists(orderItemDto.OrderId))
            {
                return Conflict(new { message = $"OrderId {orderItemDto.OrderId} already exists." });
            }
        }

            var order = await _context.Orders.FindAsync(orderItemDto.OrderId);
            if (order == null)
            {
                return NotFound(new { message = $"Order with OrderId {orderItemDto.OrderId} not found." });
            }


            if (new OrderController(_context).GetOrder(orderItemDto.OrderId) is NotFoundResult))
            {
                return Conflict(new { message = $"OrderId {orderItemDto.OrderId} already exists." });
            }
            //orderItemDto needs an Order and Item object. should you find it here or in the frontend?
            var orderItem = new OrderItem
            {
                OrderId = orderItemDto.OrderId,
                Order = 
                ItemId = orderItemDto.ItemId,
                Price = orderItemDto.Price,
                Quantity = orderItemDto.Quantity,
                Position = orderItemDto.Position
            };

            _context.Add(orderItem);
        }
        await _context.SaveChangesAsync();
        return Ok(orderItems);
    }
    return BadRequest(ModelState);
}

    
    }
}