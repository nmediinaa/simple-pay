namespace SimplePay_API.Models;

public class Transaction
{

    public int TransactionId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public Double Value { get; set; }
    public DateTime TransactionTime { get; set; }
}
