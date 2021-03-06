namespace SemanticComparison.Fluent.Tests.TestTypes
{
	public class MissingProperty
	{
		public int Id { get; }

		public string Name { get; }

		public int Missing { get; }

		public MissingProperty(int id, string name, int missing)
		{
			this.Id = id;
			this.Name = name;
			this.Missing = missing;
		}
	}
}