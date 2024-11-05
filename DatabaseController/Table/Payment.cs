using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDB.Models
{
    public class Payment
    {
        //Demo purposes. Implement AES security or Stripe/3rd party API for real world applications.
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        
        [ForeignKey("AddressId")]
        [Required]
        public int AddressId { get; set; }

        [Required]
        public int PaymentType { get; set; } 
        [Required]
        [MaxLength(20)]
        public String CardNumber { get; set; } = "";
        [Required]
        public String CardExpiration { get; set; } = "";
        [Required]
        [MaxLength(4)]
        public String CardSecurityCode { get; set; } = ""; 
        [Required]
        [MaxLength(100)]
        public String CardHolderName { get; set; } = "";
    }
}
