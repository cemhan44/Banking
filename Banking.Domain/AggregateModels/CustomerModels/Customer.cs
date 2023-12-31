﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Banking.Domain.AggregateModels.AccountModels;
using Banking.Domain.SeedWork;

namespace Banking.Domain.AggregateModels.CustomerModels
{
    public class Customer: BaseEntity, IAggregateRoot
    {
        public String FirstName {get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }

        [IgnoreDataMember]
        public ICollection<Account> Accounts { get; set; }

        public Customer()
        {

        }

        public Customer(String firstName, String lastName, String email, ICollection<Account> accounts = null)
        {
	        String regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

	        if (!Regex.IsMatch(email, regex, RegexOptions.IgnoreCase))
	        {
		        throw new Exception("Email does not match with the rules!");
            }

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Accounts = accounts ?? new List<Account>();
        }
    }
}
