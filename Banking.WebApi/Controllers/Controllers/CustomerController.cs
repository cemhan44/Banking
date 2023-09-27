using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Banking.Application;
using Banking.Domain.AggregateModels.CustomerModels;
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

	    [HttpGet]
	    public ActionResult GetCustomer(Int32 id)
	    {
		    try
		    {
			    Customer customer = _CustomerRepository.GetCustomer(id);

			    return Ok(customer);
		    }
		    catch (Exception e)
		    {
			    return StatusCode(500, $"Error: {e.Message}");
		    }

	    }

	    [HttpGet("{skip}/{take}")]
		public ActionResult GetCustomers([Range(0, 10)] Int32 skip, [Range(1, 10)] Int32 take)
	    {
		    try
		    {
			    List<Customer> customers = _CustomerRepository.GetCustomers(skip, take).ToList();

			    return Ok(customers);
		    }
		    catch (Exception e)
		    {
			    return StatusCode(500, $"Error: {e.Message}");
		    }

	    }
	}
}
