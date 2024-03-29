// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using Xunit;

namespace SquidEyes.UnitTests;

public class EnumListTests
{
    private enum Empty
    {
    }


    [Fact]
    public void FromAllShouldReturnExpectedValues()
    {
        var uriKinds = EnumList.FromAll<UriKind>();

        uriKinds.Should().ContainInOrder(
            UriKind.RelativeOrAbsolute,
            UriKind.Absolute,
            UriKind.Relative);
    }

    [Fact]
    public void EmptyEnumShouldReturnEmptyList()
    {
        var uriKinds = EnumList.FromAll<Empty>();

        uriKinds.Count.Should().Be(0);
    }
}