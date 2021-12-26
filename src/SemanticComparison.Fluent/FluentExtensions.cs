namespace SemanticComparison.Fluent
{
	public static class FluentExtensions
	{
		/// <summary>
		/// Configure <see cref="SemanticComparison.SemanticComparer{T}"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static ISemanticComparerExpression<T> ConfigureSemanticComparer<T>()
		{
			return new SemanticComparerExpression<T>();
		}
	}
}