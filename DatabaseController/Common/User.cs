using System.ComponentModel.DataAnnotations;

namespace WebDB.Models
{
    public class User
    {
        [Key][Required]
        public int UserId { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; } = "";
        [StringLength(50)]
        public string MiddleName { get; set; } = "";
        [StringLength(50)]
        public string LastName { get; set; } = "";

        public Phone PrimaryPhone { get; set; } = new Phone 
            { PhoneNumber = "",  PhoneType = Phone.PhoneTypeEnum.Other};
        
        [StringLength(100)]
        public string PrimaryEmail { get; set; } = "";

        
        public string UserName { get; set; } = "";
        [StringLength(50)]
        public string Email { get; set; } = "";

        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public int PasswordIterations { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
    
}