using FluentAssertions;
using SquidEyes.Basics;
using System.Text;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class StringBuilderExtendersTests
    {
        [Theory]
        [InlineData(',', "1,2")]
        [InlineData('|', "1|2")]
        public void X(char delimiter, string result2)
        {
            var sb = new StringBuilder();

            sb.AppendDelimited(1, delimiter);
            sb.ToString().Should().Be("1");

            sb.AppendDelimited(2, delimiter);
            sb.ToString().Should().Be(result2);
        }
    }
}
