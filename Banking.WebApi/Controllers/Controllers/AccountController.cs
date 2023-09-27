using System;
using Banking.Application;
using Microsoft.AspNetCore.Mvc;

namespace Order.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class AccountController : ControllerBase
    {
	    private readonly IAccountRepository _AccountRepository;
	    public AccountController(IAccountRepository accountRepository)
	    {
		    _AccountRepository = accountRepository;
	    }
		
		[HttpPut("Deposit")]
		public ActionResult Deposit(Int32 holderId, String accountNumber, Decimal moneyToDeposit)
	    {
		    try
		    {
			    _AccountRepository.Deposit(holderId, accountNumber, moneyToDeposit);

			    return Ok();
			}
		    catch (Exception e)
		    {
				return StatusCode(500, $"Error: {e.Message}");
			}

		     
	    }

        [HttpPut("Withdraw")]
        public ActionResult Withdraw(Int32 holderId, String accountNumber, Decimal moneyToDeposit)
        {
           

            try
            {
	            _AccountRepository.Withdraw(holderId, accountNumber, moneyToDeposit);

	            return Ok();
			}
            catch (Exception e)
            {
				return StatusCode(500, $"Error: {e.Message}");
			}
        }

        [HttpDelete]
        public ActionResult DeleteAccount(Int32 holderId, String accountNumber)
        {
	        try
	        {
		        _AccountRepository.DeleteAccount(holderId, accountNumber);

		        return Ok();
			}
	        catch (Exception e)
	        {
				return StatusCode(500, $"Error: {e.Message}");
			}
        }

        [HttpPost]
        public ActionResult OpenAccount(Int32 holderId, String accountNumber, String accountName, Decimal initialBalance)
        {
	        try
            {
	            _AccountRepository.OpenAccount(holderId, accountNumber, accountNumber, initialBalance);

	            return Ok();
            }
            catch (Exception e)
            {
	            return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}
