// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using SquidEyes.Basics;
using FluentAssertions;
using Xunit;

namespace SquidEyes.UnitTests;

public class SemVerTests
{
    [Theory]
    [InlineData("1.2.3", 1, 2, 3, null)]
    [InlineData("1.2.3-label", 1, 2, 3, "label")]
    public void SemVer_Parse_Should_ContructFromGoodValues(
        string value, byte major, byte minor, byte patch, string label)
    {
        var version = SemVer.Parse(value);

        version.Major.Should().Be(major);
        version.Minor.Should().Be(minor);
        version.Patch.Should().Be(patch);
        version.Label.Should().Be(label);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("a ")]
    [InlineData(" a")]
    [InlineData(" a ")]
    [InlineData("Abc")]
    public void SemVer_Parse_Should_ValidateLabel(string value)
    {
        FluentActions.Invoking(() => 
            SemVer.Parse(value)).Should().Throw<Exception>();
    }
}