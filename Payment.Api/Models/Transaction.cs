namespace Payment.Api.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
    }

}
