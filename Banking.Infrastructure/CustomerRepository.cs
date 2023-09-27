using System;
using System.Collections.Generic;
using System.Linq;
using Banking.Application;
using Banking.Infrastructure.Context;
using System.Threading.Tasks;
using Banking.Domain.AggregateModels.AccountModels;
using Banking.Domain.AggregateModels.CustomerModels;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
	{
        private readonly BankingDbContext _DbContext;

        public CustomerRepository(BankingDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public void AddCustomer(String firstName, String lastName, String email)
        {
	        if (IsEmailExist(email))
		        throw new Exception("Email already registered!");

	        Customer itemToAdd = new Customer(firstName, lastName, email);

	        _DbContext.Customers.Add(itemToAdd);
	        SaveChangesAsync();
        }

        public IEnumerable<Account> GetCustomerAccounts(Int32 holderId)
        {
	        if (!IsCustomerExist(holderId))
		        throw new Exception("Holder could not be found!");

	        if (!HasCustomerAccounts(holderId))
		        throw new Exception("Holder has no account!");

            return _DbContext.Customers.First(s => s.Id == holderId).Accounts.AsEnumerable();
        }

        public Task<Int32> SaveChangesAsync()
        {
	        return _DbContext.SaveChangesAsync();
        }

        private Boolean IsCustomerExist(Int32 holderId)
        {
	        return _DbContext.Customers.Any(s => s.Id == holderId);
        }

        private Boolean IsEmailExist(String email)
        {
	        return _DbContext.Customers.Any(s => s.Email == email);
        }

        private Boolean HasCustomerAccounts(Int32 holderId)
        {
	        var customer = _DbContext.Customers.Include("Accounts").First(s => s.Id == holderId);

	        if (customer.Accounts == null || !customer.Accounts.Any())
		        return false;

	        return true;
        }
    }

}
