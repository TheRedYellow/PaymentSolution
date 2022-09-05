using Payment.Api.Models;

namespace Payment.Api.Services
{
    public interface IDb
    {
        Dictionary<long, decimal> Accounts { get; init; }
        List<Transaction> Transactions { get; init; }
    }
}
