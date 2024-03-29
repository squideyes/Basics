// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using System;
using System.Collections.Generic;
using Xunit;

namespace SquidEyes.UnitTests;

public class GenericExtendersTests
{
    [Fact]
    public void ToListOfShouldWork()
    {
        const int VALUE = 123;

        var list = VALUE.ToListOf();

        list.Count.Should().Be(1);
        list.Should().Contain(VALUE);
    }

    [Fact]
    public void ToHashSetOfShouldWork()
    {
        const int VALUE = 123;

        var hashSet = VALUE.ToHashSetOf();

        hashSet.Count.Should().Be(1);
        hashSet.Should().Contain(VALUE);
        hashSet.Contains(VALUE).Should().BeTrue();
    }

    [Theory]
    [InlineData(1, 1, true)]
    [InlineData(1, 2, false)]
    [InlineData(2, 1, false)]
    [InlineData(3, 1, true)]
    public void HasMaskBitsShouldWorkForBytes(byte value, byte mask, bool expected) =>
        value.HasMaskBits(mask).Should().Be(expected);

    [Theory]
    [InlineData(1, 1, true)]
    [InlineData(1, 2, false)]
    [InlineData(2, 1, false)]
    [InlineData(3, 1, true)]
    public void HasMaskBitsShouldWorkForInts(int value, int mask, bool expected) =>
        value.HasMaskBits(mask).Should().Be(expected);

    [Fact]
    public void IsDefaultValueWorkForIConvertbleTypes()
    {
        default(bool).IsDefault().Should().BeTrue();
        default(byte).IsDefault().Should().BeTrue();
        default(char).IsDefault().Should().BeTrue();
        default(short).IsDefault().Should().BeTrue();
        default(int).IsDefault().Should().BeTrue();
        default(long).IsDefault().Should().BeTrue();
        default(sbyte).IsDefault().Should().BeTrue();
        default(ushort).IsDefault().Should().BeTrue();
        default(uint).IsDefault().Should().BeTrue();
        default(ulong).IsDefault().Should().BeTrue();
        default(DateTime).IsDefault().Should().BeTrue();
        default(decimal).IsDefault().Should().BeTrue();
        default(float).IsDefault().Should().BeTrue();
        default(double).IsDefault().Should().BeTrue();
        default(UriKind).IsDefault().Should().BeTrue();
    }

    private static List<int> ParseChoices(string choices) =>
        choices.ToListOf(v => int.Parse(v));

    [Theory]
    [InlineData("1,2,3", 2, true)]
    [InlineData("1,2,3", 4, false)]
    public void InWorksWithIEnumerable(string choices, int value, bool expected) =>
        value.In(ParseChoices(choices)).Should().Be(expected);

    [Fact]
    public void InWorksWithParams() => 2.In(1, 2, 3).Should().BeTrue();

    [Fact]
    private void IEnumerableInWithoutValues()
    {
        FluentActions.Invoking(() => 1.In(new List<int> { }))
            .Should().Throw<Exception>();
    }

    [Fact]
    private void ParamsInWithoutValues() =>
        FluentActions.Invoking(() => 1.In()).Should().Throw<Exception>();

    [Theory]
    [InlineData(UriKind.Absolute, "ABSOLUTE")]
    [InlineData(UriKind.Relative, "RELATIVE")]
    public void EnumToUpperWorks(UriKind uriKind, string expected) =>
        uriKind.ToUpper().Should().Be(expected);

    [Theory]
    [InlineData(UriKind.Absolute, "absolute")]
    [InlineData(UriKind.Relative, "relative")]
    public void EnumToLowerWorks(UriKind uriKind, string expected) =>
        uriKind.ToLower().Should().Be(expected);

    [Theory]
    [InlineData(1, 1, 3, true, true)]
    [InlineData(2, 1, 3, true, true)]
    [InlineData(3, 1, 3, true, true)]
    [InlineData(1, 1, 3, false, false)]
    [InlineData(2, 1, 3, false, true)]
    [InlineData(3, 1, 3, false, false)]
    public void BetweenWorks<T>(T value, T minValue, T maxValue,
        bool exclusive, bool expected) where T : IComparable
    {
        value.Between(minValue, maxValue, exclusive).Should().Be(expected);
    }

    [Fact]
    public void BetweenThrowsErrorWithBadMinMax()
    {
        FluentActions.Invoking(() => 2.Between(3, 2)).Should()
            .Throw<ArgumentOutOfRangeException>();
    }
}