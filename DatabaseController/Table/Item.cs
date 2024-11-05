using System.ComponentModel.DataAnnotations;


namespace WebDB.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemName { get; set; } = "";

        [Required]
        [StringLength(20)]
        public string SKU { get; set; } = "";

        public decimal Price { get; set; }

        public decimal WholeSalePrice { get; set; }
        public bool IsActive { get; set; }
    }
}
