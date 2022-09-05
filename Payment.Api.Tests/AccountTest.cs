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
               new Dictionary<long, decimal>(){{ 1,1000m }}
               //, new List<Models.Transaction>() { new Models.Transaction() { AccountId=1,TransactionId=Guid.NewGuid().ToString(),Amount=100} }
               );
            AccountService accountService = new AccountService(db);
            accountService.ProcessPayment(1, Guid.NewGuid().ToString(), Models.OriginType.MASTER, 100);

            decimal expectedResult = 1098m;
            decimal actualResult=accountService.CheckBalance(1);

            Assert.That(expectedResult.Equals(actualResult));  
           
        }
    }
}