using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Api.Models;
using Payment.Api.Services;
using System.Net;

namespace Payment.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
       

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AccountActivity(Activity activity)
        {
            if (!Enum.IsDefined(typeof(MessageType), activity.MessageType))
            {
                return new BadRequestObjectResult("Message type must be set to a PAYMENT or ADJUSTMENT");
            }


            bool result = false ;

            switch (activity.MessageType)
            {
                case MessageType.PAYMENT:

                    if (!accountService.Database.Accounts.Any(account => account.Key == activity.AccountId))
                    {
                        return NotFound("Account not found");
                    }
                    result = accountService.ProcessPayment(activity.AccountId, activity.TransactionId, activity.Origin, activity.Amount);
                    break;
                case MessageType.ADJUSTMENT:

                    if (!accountService.Database.Transactions.Any(transaction => transaction.TransactionId == activity.TransactionId))
                    {
                        return NotFound("Transaction not found");
                    }

                    result = accountService.MakeAdjustment(activity.TransactionId,  activity.Amount);
                    break;
            }

            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<decimal> CheckBalance(long accountId)
        {
            if (!accountService.Database.Accounts.Any(account => account.Key == accountId))
            {
                return NotFound("Account not found");
            }

            return accountService.CheckBalance(accountId);   
        }
    }
}
