using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Banking.Domain.AggregateModels.CustomerModels;
using Banking.Domain.SeedWork;

namespace Banking.Domain.AggregateModels.AccountModels
{
    public class Account: BaseEntity, IAggregateRoot
    {
        public String Name { get; private set; }
        public String Number { get; private set; }
        public Decimal Balance { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public Int32 HolderId { get; private set; }
        public virtual Customer Customer { get; set; }
        

        public Account(String name, String number, Int32 holderId, Decimal balance)
        {
            //can be added client validations to here

	        Name = name;
	        Number = number;
            HolderId = holderId;
            Balance = balance;
        }


    }
}
