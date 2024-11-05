using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebDB.Controllers;
/*
TODO: add 1000 gap numbering between Positions.
 + logic for 100 or 10 gaps or reordering if necessary
 add bulk item/quantity logic
*/
namespace WebDB.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        
        [Required]
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public required Order Order { get; set; }

        [Required]
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public required Item Item { get; set; }
        
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int Position { get; set; }


        static public bool OrderItemDemo(WebDbContext context, int i){
            var orderItem = new OrderItem
                    {
                        Item = new Item(),
                        Order = new Order(),
                        OrderId = i*100, // assuming an Order with Id 1 exists
                        ItemId = i*101, // assuming an Item with Id 1 exists
                        Price = i*10m,
                        Quantity = i + 10,
                    };
    
            return true;
        }
    
    
    }
}
