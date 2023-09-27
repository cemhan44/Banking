using System;
using System.Collections.Generic;
using Banking.Domain.AggregateModels.AccountModels;
using Banking.Domain.SeedWork;

namespace Banking.Application
{
	public interface ICustomerRepository: IRepository
	{
		IEnumerable<Account> GetCustomerAccounts(Int32 holderId);

		void AddCustomer(String firstName, String lastName, String email);
	}
}
