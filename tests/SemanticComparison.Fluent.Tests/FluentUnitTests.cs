using SemanticComparison.Fluent.Tests.TestTypes;
using Xunit;

namespace SemanticComparison.Fluent.Tests
{
	public class FluentUnitTests
	{
		[Fact]
		public void Fluent_TestMissingProperties()
		{
			MissingProperty c1 = new MissingProperty(id: 1, name: nameof(MissingProperty), missing: 2);
			MissingProperty c2 = new MissingProperty(id: 1, name: nameof(MissingProperty), missing: 3);

			var sut = SemanticComparison.Fluent.FluentExtensions
				.ConfigureSemanticComparer<MissingProperty>()
				.Without(e => e.Missing)
				.Create();

			Assert.True(sut.Equals(c1, c2));
		}

		[Fact]
		public void Fluent_TestCustomType()
		{
			CustomType t1 = new CustomType(id: 1, name: "t1");
			ClassWithCustomType c1 = new ClassWithCustomType(id: 2, custom: t1);

			CustomType t2 = new CustomType(id: 1, name: "t2");
			ClassWithCustomType c2 = new ClassWithCustomType(id: 2, custom: t2);

			var customTypeEquality = SemanticComparison.Fluent.FluentExtensions
				.ConfigureSemanticComparer<CustomType>()
				.Without(e => e.Name)
				.Create();

			var sut = SemanticComparison.Fluent.FluentExtensions
				.ConfigureSemanticComparer<ClassWithCustomType>()
				.ForType<CustomType>((e1, e2) => customTypeEquality.Equals(e1, e2))
				.Create();

			Assert.True(sut.Equals(c1, c2));
		}
	}
}