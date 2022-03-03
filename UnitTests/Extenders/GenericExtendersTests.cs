// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com) 
// 
// This file is part of SquidEyes.Basics
// 
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
        default(bool).IsDefaultValue().Should().BeTrue();
        default(byte).IsDefaultValue().Should().BeTrue();
        default(char).IsDefaultValue().Should().BeTrue();
        default(short).IsDefaultValue().Should().BeTrue();
        default(int).IsDefaultValue().Should().BeTrue();
        default(long).IsDefaultValue().Should().BeTrue();
        default(sbyte).IsDefaultValue().Should().BeTrue();
        default(ushort).IsDefaultValue().Should().BeTrue();
        default(uint).IsDefaultValue().Should().BeTrue();
        default(ulong).IsDefaultValue().Should().BeTrue();
        default(DateTime).IsDefaultValue().Should().BeTrue();
        default(decimal).IsDefaultValue().Should().BeTrue();
        default(float).IsDefaultValue().Should().BeTrue();
        default(double).IsDefaultValue().Should().BeTrue();
        default(UriKind).IsDefaultValue().Should().BeTrue();
    }

    private static List<int> ParseChoices(string choices) =>
        choices.ToListOf(v => int.Parse(v));

    [Theory]
    [InlineData("1,2,3", 2, true)]
    [InlineData("", 2, false)]
    [InlineData("1,2,3", 4, false)]
    public void InWorksWithIEnumerable(string choices, int value, bool expected) =>
        value.In(ParseChoices(choices)).Should().Be(expected);

    [Theory]
    [InlineData("1,2,3", 2, true)]
    [InlineData("1,2,3", 4, false)]
    public void InWorksWithNonEmptyHashSet(string choices, int value, bool expected) =>
        value.In(new HashSet<int>(ParseChoices(choices))).Should().Be(expected);

    [Fact]
    public void InWorksWithEmptyHashSet() => 1.In(new HashSet<int>()).Should().Be(false);

    [Fact]
    public void InWorksWithParams() => 1.In(1, 2, 3).Should().BeTrue();

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

    [Fact]
    public void ValidatedWithGoodValueShouldReturnValue() =>
        2.Validated("Code", v => v == 2).Should().Be(2);

    [Fact]
    public void ValidatedWithGoodValueShouldReturnConvertedValue() =>
        2.Validated("Code", v => v == 2, v => v.ToString()).Should().Be("2");

    [Fact]
    public void ValidatedWithBadFieldNameShouldThrowError()
    {
        FluentActions.Invoking(() => 2.Validated("", v => v == 2))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ValidatedWithBadValueShouldThrowError()
    {
        FluentActions.Invoking(() => 2.Validated("Code", v => v != 2))
            .Should().Throw<ArgumentOutOfRangeException>();
    }
}
