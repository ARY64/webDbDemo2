using System.ComponentModel.DataAnnotations;

namespace WebDB.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int BillingCustomerId { get; set; }
        public int ShippingCustomerId { get; set; }
        public DateTime DatePlaced { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}
