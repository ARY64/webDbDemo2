using System.ComponentModel.DataAnnotations;

namespace WebDB.Models
{
    public class Address
    {
        [Key][Required]
        public int AddressId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        
        public string Line1 { get; set; } = "";
        public string Line2 { get; set; } = "";
        [Required]
        public string Street { get; set; } = "";
        [Required]
        public string City { get; set; } = "";
        [Required]
        public string State { get; set; } = "";
        [Required]
        public string Zip { get; set; } = "";
    }
}
