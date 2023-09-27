using System;
using Banking.Domain.SeedWork;

namespace Banking.Application
{
    public interface IAccountRepository: IRepository
    {
	    void Deposit(Int32 holderId, String accountNumber, Decimal money);

        void Withdraw(Int32 holderId, String accountNumber, Decimal money);

        void OpenAccount(Int32 holderId, String accountName, String accountNumber, Decimal initialBalance);

        void DeleteAccount(Int32 holderId, String accountNumber);
    }
}
