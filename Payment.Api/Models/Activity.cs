namespace Payment.Api.Models
{
    public class Activity
    {
        public MessageType MessageType { get; set; }
        public string TransactionId { get; set; }=Guid.NewGuid().ToString();
        public long AccountId { get; set; }

        public OriginType Origin { get; set; }
        public decimal Amount { get; set; }
    }
}
