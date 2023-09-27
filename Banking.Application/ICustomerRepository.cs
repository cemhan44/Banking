using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Banking.Domain.AggregateModels.AccountModels;
using Banking.Domain.AggregateModels.CustomerModels;
using Banking.Domain.SeedWork;

namespace Banking.Application
{
	public interface ICustomerRepository: IRepository
	{
		IEnumerable<Account> GetCustomerAccounts(Int32 holderId);

		void AddCustomer(String firstName, String lastName, String email);

		Customer GetCustomer(Int32 id);

		IEnumerable<Customer> GetCustomers(Int32 skip, Int32 take);
	}
}
