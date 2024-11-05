using Microsoft.AspNetCore.Mvc;

/*
DatabaseController class has no current responsibilies. It can be used for
    health / security checks
    database maintenence like backup/restore
    or cross entity operations that don't clearly belong to a single entity's controller
*/

namespace WebDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        
        private readonly WebDbContext _context; //used to interact with the database

        public DatabaseController(WebDbContext context)
        {
            _context = context;
        }
        [HttpGet("{SKU2}")]
        public IActionResult GetBySKU(string SKU)
        {
            // Retrieve data from the database
            //var tableData = _context.Items.ToList();

            // Convert the data to JSON format
            // var json = new JsonResult(tableData);
    
            var data = new {SKU = "SKU"};

            var json = new JsonResult(data);
            return json;
        }
    }
}
