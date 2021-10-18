using FluentAssertions;
using SquidEyes.Basics;
using System.Collections.Generic;
using Xunit;

namespace SquidEyes.UnitTests.Extenders
{
    public class IEnumerableExtendersTests
    {
        [Fact]
        public void ForEachIteratesAsExpected()
        {
            int index = 0;

            ((IEnumerable<int>)new List<int> { 1, 2, 3 }).ForEach(
                number =>
                {
                    index++;

                    number.Should().Be(index);
                });

            index.Should().Be(3);
        }
    }
}
