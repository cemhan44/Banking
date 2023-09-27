using System;
using System.Collections.Generic;
using Banking.Domain.SeedWork;

namespace Banking.Domain.AggregateModels.AccountModels
{
	// just implemented for reference future value objects
	public class Status : ValueObject

	{
		public enum AccountStatus
		{
			ACTIVE,
			WAITING,
			BANNED,
			FORBIDDEN
			//etc
		}
		public AccountStatus CurrentStatus { get; set; }

		/// <inheritdoc />
		protected override IEnumerable<Object> GetEqualityComponents()
		{
			yield return CurrentStatus;
		}
	}
}
