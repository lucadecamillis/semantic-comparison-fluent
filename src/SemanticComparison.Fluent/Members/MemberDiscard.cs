using System.Reflection;

namespace SemanticComparison.Fluent.Members
{
	internal class MemberDiscard : IMemberComparer
	{
		readonly PropertyInfo propertyInfo;

		public MemberDiscard(PropertyInfo propertyInfo)
		{
			this.propertyInfo = propertyInfo;
		}

		#region IMemberComparer

		public new bool Equals(object x, object y)
		{
			// Always equal, no comparison should be done
			return true;
		}

		public int GetHashCode(object obj)
		{
			// Always call the equal
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