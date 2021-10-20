using FluentAssertions;
using SquidEyes.Basics;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class MiscExtendersTests
    {
        [Fact]
        public void AsFuncWorksAsExpected() =>
            "XXX".AsFunc(s => s).Should().Be("XXX");

        [Fact]
        public void AsActionWorksAsExpected() =>
            "XXX".AsAction(s => s.Should().Be("XXX"));
    }
}
