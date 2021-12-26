using System;
using System.Collections.Generic;
using System.Reflection;

namespace SemanticComparison.Fluent.Members
{
	internal class MemberListComparerWithEquality<T> : IMemberComparer
	{
		readonly PropertyInfo propertyInfo;
		readonly Func<T, T, bool> lookup;
		readonly IEqualityComparer<T> equalityComparer;

		public MemberListComparerWithEquality(
			PropertyInfo propertyInfo,
			Func<T, T, bool> lookup,
			IEqualityComparer<T> equalityComparer)
		{
			this.propertyInfo = propertyInfo;
			this.lookup = lookup;
			this.equalityComparer = equalityComparer;
		}

		#region IMemberComparer

		public new bool Equals(object x, object y)
		{
			if (x is IEnumerable<T> xList && y is IEnumerable<T> yList)
			{
				return BaseComparer.Equals(xList, yList, this.lookup, this.equalityComparer);
			}

			return false;
		}

		public int GetHashCode(object obj)
		{
			// Always call the equals
			return 1;
		}

		public bool IsSatisfiedBy(PropertyInfo request)
		{
			return BaseComparer.IsSatisfiedBy(expected: this.propertyInfo, actual: request);
		}

		public bool IsSatisfiedBy(FieldInfo request)
		{
			return false;
		}

		#endregion
	}
}