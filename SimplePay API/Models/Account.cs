namespace SimplePay_API.Models;

public class Account
{
    public int AccountId { get; set; }

    public int UserId { get; set; }

    public double AccountBalance { get; set; }

    public DateTime LastModify { get; set; }

    public User? User { get; set; }

    public Transaction? Transaction{ get; set; }
}
