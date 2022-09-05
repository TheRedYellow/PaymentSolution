using Payment.Api.Models;

namespace Payment.Api.Services
{
    public class AccountService :IAccountService
    {
        public Db Database { get; init; }

        public AccountService(Db db)
        {
            this.Database = db;
        }

        /// <summary>
        /// Process payment for existing accounts
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="transactionId"></param>
        /// <param name="origin"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool ProcessPayment(long accountId, string transactionId, OriginType origin, decimal amount)
        {
            decimal currentBalance;
            if (!Database.Accounts.TryGetValue(accountId, out currentBalance))
            {
                return false;
            }


            decimal commission = 0m;

            switch (origin)
            {
                case OriginType.VISA:
                    commission = amount * 0.01m;
                    break;
                case OriginType.MASTER:
                    commission = amount * 0.02m;
                    break;
            }

            //Registering the transaction
            Database.Transactions.Add(new Transaction()
            {
                TransactionId = transactionId,
                AccountId = accountId,
                Amount = (amount - commission)
            });


            return true;
        }

        /// <summary>
        /// Make adjustment for existing transactions
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool MakeAdjustment(string transactionId, decimal amount)
        {
            var transaction = Database.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);

            if (transaction == null)
            {
                return false;
            }

            //Applying the adjustment to the transaction
           transaction.Amount += amount;

            return true;

        }

        /// <summary>
        /// Check balance for existing accounts
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public decimal CheckBalance(long accountId)
        {
            decimal currentBalance;
            if (!Database.Accounts.TryGetValue(accountId, out currentBalance))
            {
                return decimal.MinValue;
            }

            decimal totalSumOfTransactions = Database.Transactions.Where(t => t.AccountId == accountId)
                        .Sum(t => t.Amount);

            return currentBalance + totalSumOfTransactions;

        }
    }
}
