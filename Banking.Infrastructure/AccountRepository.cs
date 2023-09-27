using System;
using System.Linq;
using Banking.Application;
using System.Threading.Tasks;
using Banking.Domain.AggregateModels.AccountModels;
using Banking.Domain.AggregateModels.CustomerModels;
using Banking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
	    private readonly BankingDbContext _DbContext;

	    public AccountRepository(BankingDbContext dbContext)
	    {
		    _DbContext = dbContext;
	    }

	    public void OpenAccount(Int32 holderId, String accountName, String accountNumber, Decimal initialBalance)
        {
	        if (initialBalance < 100)
		        throw new Exception("Account balance less than 100$ is not allowed!");

            if (!IsCustomerExist(holderId))
	            throw new Exception("Holder could not be found!");

            if (!IsAccountNameExist(holderId, accountName))
	            throw new Exception("There is an account with same name!");

            if (IsAccountNumberExist(accountNumber))
                throw new Exception("There is an account with same number!");
            
            Account accountToOpen = new Account(accountName, accountNumber, holderId, initialBalance);
            _DbContext.Accounts.Add(accountToOpen);
            SaveChangesAsync();
        }

	    public void DeleteAccount(Int32 holderId, String accountNumber)
	    {
		    if (!IsCustomerExist(holderId))
			    throw new Exception("Holder could not be found!");

		    if (!IsCustomerAccountNumberExist(holderId, accountNumber))
			    throw new Exception("There is not a customer account with same number!");

		    Account accountToDelete = _DbContext.Accounts.First(s => s.Number == accountNumber);
		    _DbContext.Accounts.Remove(accountToDelete);
		    SaveChangesAsync();
		}

	    public void Deposit(Int32 holderId, String accountNumber, Decimal money)
	    {
		    if (money > 10000)
		    {
			    throw new Exception("Deposit more than 10.000$ is not allowed!");
		    }

		    if (!IsCustomerExist(holderId))
			    throw new Exception("Holder could not be found!");

		    if (!IsCustomerAccountNumberExist(holderId, accountNumber))
			    throw new Exception("There is not a customer account with same number!");


		    Account account = _DbContext.Accounts.First(s => s.HolderId == holderId);
		    account.Balance += money;
			account.UpdateDate = DateTime.Now;
		    _DbContext.Accounts.Update(account);
		    SaveChangesAsync();
	    }

        public Task<Int32> SaveChangesAsync()
        {
	        return _DbContext.SaveChangesAsync();
		}

        public void Withdraw(Int32 holderId, String accountNumber, Decimal money)
        {
	        if (!IsCustomerExist(holderId))
		        throw new Exception("Holder could not be found!");

	        if (!IsCustomerAccountNumberExist(holderId, accountNumber))
		        throw new Exception("There is not a customer account with same number!");

	        Account account = _DbContext.Accounts.First(s => s.HolderId == holderId);

	        if (account.Balance - money < 100)
		        throw new Exception("Account balance less than 100$ is not allowed!");

			if (money > (9 * account.Balance) / 10)
		        throw new Exception("Single transaction limited with %90 of balance!");

			account.Balance -= money;
			account.UpdateDate = DateTime.Now;
			_DbContext.Accounts.Update(account);
            SaveChangesAsync();
		}

        private Boolean IsCustomerExist(Int32 holderId)
        {
	        return _DbContext.Customers.Any(s => s.Id == holderId);
        }
        //unique
        private Boolean IsAccountNumberExist(String accountNumber)
        {
			return _DbContext.Accounts.Any(s => s.Number == accountNumber);
        }
        //unique per customer
        private Boolean IsAccountNameExist(Int32 holderId, String accountName)
        {
	        Customer customer = _DbContext.Customers.First(s => s.Id == holderId);

			return customer.Accounts?.Any(s => s.Name == accountName) ?? true;
        }
        private Boolean IsCustomerAccountNumberExist(Int32 holderId, String accountNumber)
        {
	        Customer customer = _DbContext.Customers.Include("Accounts").First(s => s.Id == holderId);

	        if (customer.Accounts != null)
	        {
		        return customer.Accounts.Any(s => s.Number == accountNumber);
	        }

	        return false;
        }
	}
}
