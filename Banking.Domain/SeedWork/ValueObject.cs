﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain.SeedWork
{
	public abstract class ValueObject
	{
		protected static Boolean EqualOperator(ValueObject left, ValueObject right)
		{
			if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
			{
				return false;
			}
			return ReferenceEquals(left, right) || left.Equals(right);
		}

		protected static Boolean NotEqualOperator(ValueObject left, ValueObject right)
		{
			return !(EqualOperator(left, right));
		}

		protected abstract IEnumerable<Object> GetEqualityComponents();

		public override Boolean Equals(Object obj)
		{
			if (obj == null || obj.GetType() != GetType())
			{
				return false;
			}

			var other = (ValueObject)obj;

			return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
		}

		public override Int32 GetHashCode()
		{
			return GetEqualityComponents()
				.Select(x => x != null ? x.GetHashCode() : 0)
				.Aggregate((x, y) => x ^ y);
		}
		// Other utility methods
	}
}
