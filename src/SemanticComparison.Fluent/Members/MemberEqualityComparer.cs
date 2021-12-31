using System.Collections.Generic;
using System.Reflection;
using System;

namespace SemanticComparison.Fluent.Members
{
	internal class MemberEqualityComparer<T> : IMemberComparer
	{
		readonly PropertyInfo propertyInfo;
		readonly IEqualityComparer<T> equality;

		public MemberEqualityComparer(PropertyInfo propertyInfo, IEqualityComparer<T> equality)
		{
			this.propertyInfo = propertyInfo;
			this.equality = equality;
		}

		#region IMemberComparer

		public new bool Equals(object x, object y)
		{
			if (x is T tx && y is T ty)
			{
				return this.equality.Equals(tx, ty);
			}

			return false;
		}

		public int GetHashCode(object obj)
		{
			if (obj is T t)
			{
				return this.equality.GetHashCode(t);
			}

			return 0;
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