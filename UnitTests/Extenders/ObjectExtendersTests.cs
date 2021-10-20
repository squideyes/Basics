using FluentAssertions;
using SquidEyes.Basics;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class ObjectExtendersTests
    {
        [Fact]
        public void AsListExtenderWorksWithSingleArg()
        {
            var list = "AAA".AsList();

            list.Count.Should().Be(1);

            list.Should().ContainInOrder("AAA");
        }
    }
}
