using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebDB.Models;

/* 
 * This class is used to connect to the database and query the tables.
 * It inherits from the DbContext class, which is provided by Entity Framework Core.
 * The OnConfiguring method is used to configure the database connection using the connection string from the appsettings.json file.
 */

namespace WebDB.Controllers {
public class WebDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public WebDbContext(IConfiguration configuration, DbContextOptions<WebDbContext> options) : base(options)
    {
        _configuration = configuration;
        
    }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        // public DbSet<Group> Groups { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Phone> Phones { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21)));
    }
    public bool IsConnectionOpen()
    {
        try
        {
            this.Database.OpenConnection();
            this.Database.CloseConnection();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
}