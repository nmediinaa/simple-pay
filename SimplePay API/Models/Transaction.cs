namespace SimplePay_API.Models;

public class Transaction
{

    public int TransactionId { get; set; }

    public int AccountSenderId { get; set; }

    public int AccountReceiverId { get; set; }

    public Double Value { get; set; }
    public DateTime TransactionTime { get; set; }

    public Account? Account { get; set; }

}
