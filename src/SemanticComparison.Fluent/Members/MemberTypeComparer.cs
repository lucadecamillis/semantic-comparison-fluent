using System;
using System.Reflection;

namespace SemanticComparison.Fluent.Members
{
	internal class MemberTypeComparer<T> : IMemberComparer
	{
		readonly Func<T, T, bool> equality;

		#region CTOR

		internal MemberTypeComparer(Func<T, T, bool> equality)
		{
			this.equality = equality;
		}

		#endregion

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
			return request.PropertyType == typeof(T);
		}

		public bool IsSatisfiedBy(FieldInfo request)
		{
			return request.FieldType == typeof(T);
		}

		#endregion
	}
}