// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using Xunit;

namespace SquidEyes.UnitTests;

public class MajorMinorTests
{
    [Theory]
    [InlineData(1.2, 1, 2)]
    [InlineData(1.02, 1, 2)]
    [InlineData(1.002, 1, 2)]
    [InlineData(1.2345, 1, 234)]
    public void ImplicitConversionFromDoubleWorks(double value, byte major, byte minor)
    {
        MajorMinor majorMinor = value;

        majorMinor.Major.Should().Be(major);
        majorMinor.Minor.Should().Be(minor);
    }
}