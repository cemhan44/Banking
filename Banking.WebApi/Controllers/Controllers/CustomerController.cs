using System;
using System.Linq;
using Banking.Application;
using Banking.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Order.WebApi.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
    {
	    private readonly ICustomerRepository _CustomerRepository;
	    public CustomerController(ICustomerRepository customerRepository)
	    {
		    _CustomerRepository = customerRepository;
	    }
		
		[HttpGet("GetCustomerAccounts")]
	    public ActionResult GetCustomerAccounts(Int32 holderId)
	    {
		    try
		    {
			    var result = _CustomerRepository.GetCustomerAccounts(holderId).ToList();

			    return Ok(result);
			}
		    catch (Exception e)
		    {
			    return StatusCode(500, $"Error: {e.Message}");
		    }
		    
	    }

        [HttpPut]
	    public ActionResult AddCustomer(String name, String lastname, String email)
	    {
		    try
		    {
			    _CustomerRepository.AddCustomer(name, lastname, email);

			    return Ok();
			}
		    catch (Exception e)
		    {
				return StatusCode(500, $"Error: {e.Message}");
			}
		    
	    }
	}
}
