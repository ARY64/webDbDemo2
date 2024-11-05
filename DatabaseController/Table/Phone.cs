using System.ComponentModel.DataAnnotations;

namespace WebDB.Models
{
    public class Phone
    {
        [Key]
        public int PhoneId { get; set; }
        
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = "";
        public PhoneTypeEnum PhoneType { get; set; } = PhoneTypeEnum.Other;
        public enum PhoneTypeEnum { Home, Work, Mobile, Other}

    }
}
