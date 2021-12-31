using SemanticComparison;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SemanticComparison.Fluent
{
	public interface ISemanticComparerExpression<T>
	{
		/// <summary>
		/// Discard the given property from comparison
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="delegate"></param>
		/// <returns></returns>
		ISemanticComparerExpression<T> Without<TProperty>(
			Expression<Func<T, TProperty>> propertyLambda);

		/// <summary>
		/// Provide custom delegate for comparison of properties of a given type
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="delegate"></param>
		/// <returns></returns>
		ISemanticComparerExpression<T> ForType<TProperty>(
			Func<TProperty, TProperty, bool> @delegate);

		/// <summary>
		/// Provide custom equality comparare for comparison of properties of a given type
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="comparer"></param>
		/// <returns></returns>
		ISemanticComparerExpression<T> ForType<TProperty>(
			IEqualityComparer<TProperty> comparer);

		/// <summary>
		/// Compare collections with default equality comparer
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="propertyLambda"></param>
		/// <returns></returns>
		ISemanticComparerExpression<T> ForCollection<TProperty>(
			Expression<Func<T, IEnumerable<TProperty>>> propertyLambda);

		/// <summary>
		/// Compare collections providing a custom equality comparer
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="propertyLambda"></param>
		/// <param name="equality"></param>
		/// <param name="equalityComparer"></param>
		/// <returns></returns>
		ISemanticComparerExpression<T> ForCollection<TProperty>(
			Expression<Func<T, IEnumerable<TProperty>>> propertyLambda,
			Func<TProperty, TProperty, bool> equality,
			IEqualityComparer<TProperty> equalityComparer);

		/// <summary>
		/// Provide a lambda custom equality comparer for property of a certain type
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="propertyLambda"></param>
		/// <param name="delegate"></param>
		/// <returns></returns>
		ISemanticComparerExpression<T> ForMember<TProperty>(
			Expression<Func<T, TProperty>> propertyLambda,
			Func<TProperty, TProperty, bool> @delegate);

		/// <summary>
		/// Provide a custom equality comparer for property of a certain type
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="propertyLambda"></param>
		/// <param name="equalityComparer"></param>
		/// <returns></returns>
		ISemanticComparerExpression<T> ForMember<TProperty>(
			Expression<Func<T, TProperty>> propertyLambda,
			IEqualityComparer<TProperty> equalityComparer);

		/// <summary>
		/// Create the comparer
		/// </summary>
		/// <returns></returns>
		SemanticComparer<T> Create();
	}
}