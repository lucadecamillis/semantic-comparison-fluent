using System;
using SemanticComparison.Fluent.Tests.TestTypes;
using Xunit;

namespace SemanticComparison.Fluent.Tests
{
	public class FluentUnitTests
	{
		[Fact]
		public void Fluent_TestMissingProperties()
		{
			MissingProperty c1 = new(id: 1, name: nameof(MissingProperty), missing: 2);
			MissingProperty c2 = c1 with { missing = 3 };

			var sut = SemanticComparison.Fluent.FluentExtensions
				.ConfigureSemanticComparer<MissingProperty>()
				.Without(e => e.missing)
				.Create();

			Assert.True(sut.Equals(c1, c2));
		}
	}
}