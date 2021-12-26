using System;
using System.Reflection;

namespace SemanticComparison.Fluent.Members
{
	internal class MemberLambdaComparer<T> : IMemberComparer
	{
		readonly PropertyInfo propertyInfo;
		readonly Func<T, T, bool> equality;

		public MemberLambdaComparer(PropertyInfo propertyInfo, Func<T, T, bool> equality)
		{
			this.propertyInfo = propertyInfo;
			this.equality = equality;
		}

		#region IMemberComparer

		public new bool Equals(object x, object y)
		{
			if (x is T tx && y is T ty)
			{
				return this.equality(tx, ty);
			}

			return false;
		}

		public int GetHashCode(object obj)
		{
			return obj.GetHashCode();
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