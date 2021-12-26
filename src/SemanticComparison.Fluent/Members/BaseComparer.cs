using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SemanticComparison.Fluent.Members
{
	internal static class BaseComparer
	{
		/// <summary>
		/// Specifies whether the given collection is null or contains no elements
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static bool NullOrEmpty<T>(this IEnumerable<T> collection)
		{
			return collection == null || !collection.Any();
		}

		/// <summary>
		/// Verify whether the two properties objects refer to the same property
		/// for sake of comparison
		/// </summary>
		/// <param name="expected"></param>
		/// <param name="actual"></param>
		/// <returns></returns>
		public static bool IsSatisfiedBy(PropertyInfo expected, PropertyInfo actual)
		{
			if (actual == expected)
			{
				return true;
			}

			// use this other match type for inherited properties
			return actual.DeclaringType == expected.DeclaringType && actual.Name == expected.Name;
		}

		/// <summary>
		/// Checks whether the two enumerations are equal, i.e. they contains the same objects.
		/// If the flag checkOrder=true, it is also checked whether the order of objects
		/// is preserved within the two enumerations
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collectionA"></param>
		/// <param name="collectionB"></param>
		/// <param name="checkOrder"></param>
		/// <returns></returns>
		/// <remarks>ienumerables which are null and are empty are considered equal</remarks>
		public static bool Equals<T>(
			this IEnumerable<T> ienumerableA,
			IEnumerable<T> ienumerableB,
			bool checkOrder)
		{
			bool bothNull;
			if (AtLeastOneNullOrEmpty(ienumerableA, ienumerableB, out bothNull))
			{
				// In case collections are both null, we consider them as equal
				return bothNull;
			}

			if (checkOrder)
			{
				return ienumerableA.SequenceEqual(ienumerableB);
			}
			else
			{
				return ienumerableA.Count() == ienumerableB.Count() && ienumerableA.All(ienumerableB.Contains);
			}
		}

		/// <summary>
		/// Check equality for the two collections and perform the equality comparer on each equal element
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ienumerableA"></param>
		/// <param name="ienumerableB"></param>
		/// <param name="lookup"></param>
		/// <param name="equalityComparer"></param>
		/// <returns></returns>
		public static bool Equals<T>(
			this IEnumerable<T> ienumerableA,
			IEnumerable<T> ienumerableB,
			Func<T, T, bool> lookup,
			IEqualityComparer<T> equalityComparer)
		{
			bool bothNull;
			if (AtLeastOneNullOrEmpty(ienumerableA, ienumerableB, out bothNull))
			{
				// In case collections are both null, we consider them as equal
				return bothNull;
			}

			if (ienumerableA.Count() != ienumerableB.Count())
			{
				return false;
			}

			foreach (T objA in ienumerableA)
			{
				T objB = ienumerableB.FirstOrDefault(o => lookup(objA, o));
				if (objB == null || !equalityComparer.Equals(objA, objB))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Compare two collections according to the null or empty state
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ienumerableA"></param>
		/// <param name="ienumerableB"></param>
		/// <param name="both"></param>
		/// <returns></returns>
		private static bool AtLeastOneNullOrEmpty<T>(
			IEnumerable<T> ienumerableA,
			IEnumerable<T> ienumerableB,
			out bool both)
		{
			both = false;

			if (ienumerableA.NullOrEmpty() && ienumerableB.NullOrEmpty())
			{
				// if both ienumerables are null, we treat them as equal and sed the both out parameter to true
				both = true;

				return true;
			}

			if (ienumerableA.NullOrEmpty() || ienumerableB.NullOrEmpty())
			{
				// if one of the ienumerables is null and the other one not, return true
				return true;
			}

			// Both collections are NOT null
			return false;
		}
	}
}