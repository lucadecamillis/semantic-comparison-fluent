namespace SemanticComparison.Fluent.Tests.TestTypes
{
	public class ClassWithCustomType
	{
		public int Id { get; }

		public CustomType Custom { get; }

		public ClassWithCustomType(int id, CustomType custom)
		{
			this.Id = id;
			this.Custom = custom;
		}
	}
}