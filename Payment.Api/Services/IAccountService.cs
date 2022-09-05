using Payment.Api.Models;

namespace Payment.Api.Services
{
    public interface IAccountService
    {
        Db Database { get; init; }
        bool ProcessPayment(long accountId, string transactionId, OriginType origin, decimal amount);
        bool MakeAdjustment(string transactionId, decimal amount);
        decimal CheckBalance(long accountId);
    }
}
