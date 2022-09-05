using Payment.Api.Models;

namespace Payment.Api.Services
{
    public class Db:IDb
    {
        public Dictionary<long, decimal> Accounts { get; init; }
        public List<Transaction> Transactions { get; init; }

        public Db(Dictionary<long, decimal> accounts, List<Transaction> transactions = null)
        {
            this.Accounts = accounts;
            Transactions = transactions ?? new List<Transaction>();
        }

       
    }
}
