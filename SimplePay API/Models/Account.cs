using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplePay_API.Models;

public class Account
{
    [Key]
    public int AccountId { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal AccountBalance { get; set; }

    public User? User { get; set; }

}
