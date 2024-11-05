using System.ComponentModel.DataAnnotations;

public class Session
{
    [Key]
    public int OrderId { get; set; }
    
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    public required string SessionToken { get; set; }

    public DateTime SessionDate { get; set; }
}
