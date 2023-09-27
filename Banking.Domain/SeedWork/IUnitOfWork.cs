using System;
using System.Threading.Tasks;

namespace Banking.Domain.SeedWork
{
    public interface IUnitOfWork
    {
	    Task<Int32> SaveChangesAsync();
    }
}
