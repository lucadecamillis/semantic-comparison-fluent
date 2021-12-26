using System.Collections.Generic;
using System.Reflection;

namespace SemanticComparison.Fluent.Members
{
	internal class MemberListComparer<T> : IMemberComparer
	{
		readonly PropertyInfo propertyInfo;

		public MemberListComparer(PropertyInfo propertyInfo)
		{
			this.propertyInfo = propertyInfo;
		}

		#region IMemberComparer

		public new bool Equals(object x, object y)
		{
			if (x is IEnumerable<T> xList && y is IEnumerable<T> yList)
			{
				return BaseComparer.Equals(xList, yList, checkOrder: false);
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