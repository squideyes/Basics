// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using System;
using Xunit;

namespace SquidEyes.UnitTests;

public class MinMaxTests
{
    [Fact]
    public void GoodValuesShouldConstruct() => new MinMax<int>(1, 2);

    [Fact]
    public void BadValuesShouldNotConstruct()
    {
        FluentActions.Invoking(() => new MinMax<short>(2, 1))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ToStringShouldReturnExpectedValue() =>
        new MinMax<int>(1, 2).ToString().Should().Be("1 => 2");
}