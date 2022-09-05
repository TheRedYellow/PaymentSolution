using Payment.Api.Services;

namespace Payment.Api.Tests
{
    public class AccountTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Account_MasterCardPayment_WithSuccess()
        {
           
           Db db = new Db(
               new Dictionary<long, decimal>(){{ 1,1000m }} //Accounts
               );
            AccountService accountService = new AccountService(db);
            accountService.ProcessPayment(1, Guid.NewGuid().ToString(), Models.OriginType.MASTER, 100);

            decimal expectedResult = 1098m;
            decimal actualResult=accountService.CheckBalance(1);

            Assert.That(expectedResult.Equals(actualResult));  
           
        }

        [Test]
        public void Account_MakeAdjustment_WithSuccess()
        {
            string transactionId=Guid.NewGuid().ToString(); 

            Db db = new Db(
                new Dictionary<long, decimal>() { { 1, 1000m } } //Accounts
                , new List<Models.Transaction>() { new Models.Transaction() { AccountId=1,TransactionId=transactionId,Amount=100} } //Transactions
                );
            AccountService accountService = new AccountService(db);
            accountService.MakeAdjustment(transactionId, -5);

            decimal expectedResult = 1095m;
            decimal actualResult = accountService.CheckBalance(1);

            Assert.That(expectedResult.Equals(actualResult));

        }
    }
}