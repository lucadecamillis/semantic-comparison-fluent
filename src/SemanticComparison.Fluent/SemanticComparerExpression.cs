using SemanticComparison.Fluent.Members;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SemanticComparison.Fluent
{
	internal class SemanticComparerExpression<T> : ISemanticComparerExpression<T>
	{
		readonly IList<IMemberComparer> memberComparers;

		#region CTOR

		internal SemanticComparerExpression()
		{
			this.memberComparers = new List<IMemberComparer>(new[] { new MemberComparer(new SemanticComparer<T, T>()) });
		}

		#endregion

		public ISemanticComparerExpression<T> Without<TProperty>(
			Expression<Func<T, TProperty>> propertyLambda)
		{
			PropertyInfo propertyInfo = GetPropertyInfo(propertyLambda);

			this.memberComparers.Add(new MemberDiscard(propertyInfo));

			return this;
		}

		public ISemanticComparerExpression<T> ForType<TProperty>(
			Func<TProperty, TProperty, bool> @delegate)
		{
			this.memberComparers.Add(new MemberTypeComparer<TProperty>(@delegate));

			return this;
		}

		public ISemanticComparerExpression<T> ForType<TProperty>(
			IEqualityComparer<TProperty> comparer)
		{
			this.memberComparers.Add(new MemberTypeComparer<TProperty>(comparer.Equals));

			return this;
		}

		public ISemanticComparerExpression<T> ForCollection<TProperty>(
			Expression<Func<T, IEnumerable<TProperty>>> propertyLambda)
		{
			PropertyInfo propertyInfo = GetPropertyInfo(propertyLambda);

			this.memberComparers.Add(new MemberListComparer<TProperty>(propertyInfo));

			return this;
		}

		public ISemanticComparerExpression<T> ForCollection<TProperty>(
			Expression<Func<T, IEnumerable<TProperty>>> propertyLambda,
			Func<TProperty, TProperty, bool> equality,
			IEqualityComparer<TProperty> equalityComparer)
		{
			PropertyInfo propertyInfo = GetPropertyInfo(propertyLambda);

			this.memberComparers.Add(new MemberListComparerWithEquality<TProperty>(propertyInfo, equality, equalityComparer));

			return this;
		}

		public ISemanticComparerExpression<T> ForMember<TProperty>(
			Expression<Func<T, TProperty>> propertyLambda,
			Func<TProperty, TProperty, bool> @delegate)
		{
			PropertyInfo propertyInfo = GetPropertyInfo(propertyLambda);

			this.memberComparers.Add(new MemberLambdaComparer<TProperty>(propertyInfo, @delegate));

			return this;
		}

		public ISemanticComparerExpression<T> ForMember<TProperty>(
			Expression<Func<T, TProperty>> propertyLambda,
			IEqualityComparer<TProperty> equalityComparer)
		{
			PropertyInfo propertyInfo = GetPropertyInfo(propertyLambda);

			this.memberComparers.Add(new MemberEqualityComparer<TProperty>(propertyInfo, equalityComparer));

			return this;
		}

		public SemanticComparer<T> Create()
		{
			return new SemanticComparer<T>(this.memberComparers);
		}


		private static PropertyInfo GetPropertyInfo<TSource, TProperty>(
			Expression<Func<TSource, TProperty>> propertyLambda)
		{
			MemberExpression member = propertyLambda.Body as MemberExpression;
			if (member == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					propertyLambda.ToString()));

			PropertyInfo propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a field, not a property.",
					propertyLambda.ToString()));

			return propInfo;
		}
	}
}