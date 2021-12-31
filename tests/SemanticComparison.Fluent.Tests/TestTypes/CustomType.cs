namespace SemanticComparison.Fluent.Tests.TestTypes
{
	public class CustomType
	{
		public int Id { get; }

		public string Name { get; }

		public CustomType(int id, string name)
		{
			this.Id = id;
			this.Name = name;
		}
	}
}