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
using Xunit;

namespace SquidEyes.UnitTests;

public class MomentTests
{
    [Theory]
    [InlineData("00:00:00.000")]
    [InlineData("23:59:00.000")]
    [InlineData("23:59:59.000")]
    [InlineData("23:59:59.999")]
    public void ParseWithGoodArgs(string value)
    {
        Moment.Parse(value).Should().Be(
            Moment.From(TimeSpan.Parse(value)));
    }

    [Theory]
    [InlineData("00:00:00.000")]
    [InlineData("23:59:00.000")]
    [InlineData("23:59:59.000")]
    [InlineData("23:59:59.999")]
    public void FromWithGoodArgs(string value)
    {
        var timeSpan = TimeSpan.Parse(value);

        var moment1 = Moment.From(timeSpan);

        moment1.Value.Should().Be(timeSpan);

        moment1.AsTimeSpan().Should().Be(timeSpan);

        moment1.AsTimeOnly().Should().Be(
            TimeOnly.FromTimeSpan(timeSpan));

        moment1.Hours.Should().Be(timeSpan.Hours);
        moment1.Minutes.Should().Be(timeSpan.Minutes);
        moment1.Seconds.Should().Be(timeSpan.Seconds);
        moment1.Milliseconds.Should().Be(timeSpan.Milliseconds);

        moment1.ToString().Should().Be(
            timeSpan.ToString("hh\\:mm\\:ss\\.fff"));

        moment1.ToString(false).Should().Be(
            timeSpan.ToString("hh\\:mm"));

        moment1.ToString(true).Should().Be(
            timeSpan.ToString("hh\\:mm\\:ss\\.fff"));

        var moment2 = timeSpan.AsFunc(t => Moment.From(
            t.Hours, t.Minutes, t.Seconds, t.Milliseconds));

        moment1.Value.Should().Be(moment2.Value);
        moment1.AsTimeSpan().Should().Be(moment2.AsTimeSpan());
        moment1.AsTimeOnly().Should().Be(moment2.AsTimeOnly());
        moment1.Hours.Should().Be(moment2.Hours);
        moment1.Minutes.Should().Be(moment2.Minutes);
        moment1.Seconds.Should().Be(moment2.Seconds);
        moment1.Milliseconds.Should().Be(moment2.Milliseconds);
        moment1.ToString().Should().Be(moment2.ToString());
        moment1.ToString(true).Should().Be(moment2.ToString(true));
        moment1.ToString(false).Should().Be(moment2.ToString(false));
    }

    ////////////////////////////

    [Fact]
    public void FromWithBadTimeSpanArg()
    {
        FluentActions.Invoking(() => _ = Moment.From(
            TimeSpan.FromDays(1))).Should()
                .Throw<ArgumentOutOfRangeException>();
    }

    ////////////////////////////

    [Fact]
    public void FromWithBadDiscreteArgs()
    {
        FluentActions.Invoking(() =>
            _ = Moment.From(24, 59, 59, 999)).Should()
                .Throw<ArgumentOutOfRangeException>();
    }

    ////////////////////////////

    [Fact]
    public void ConstructWithNoArgs() =>
        new Moment().Value.Should().Be(TimeSpan.Zero);

    ////////////////////////////

    [Fact]
    public void SetToDefault()
    {
        Moment moment = default;

        moment.Value.Should().Be(TimeSpan.Zero);
    }

    ////////////////////////////

    [Fact]
    public void GetHashCodeReturnsExpectedResult()
    {
        var moment = Moment.From(23, 59, 59, 999);

        moment.GetHashCode().Should().Be(moment.Value.GetHashCode());
    }

    //////////////////////////////

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GenericEquals(bool result)
    {
        var (m1, m2) = GetMoments(1, 2);

        m1.Equals(result ? m1 : m2).Should().Be(result);
    }

    //////////////////////////////

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ObjectEquals(bool result)
    {
        var (m1, m2) = GetMoments(1, 2);

        m1.Equals((object)(result ? m1 : m2)).Should().Be(result);
    }

    //////////////////////////////

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void EqualsOperator(bool result)
    {
        var (m1, m2) = GetMoments(1, 2);

        (m1 == (result ? m1 : m2)).Should().Be(result);
    }

    //////////////////////////////

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void NotEqualsOperator(bool result)
    {
        var (m1, m2) = GetMoments(1, 2);

        (m1 != (result ? m2 : m1)).Should().Be(result);
    }

    //////////////////////////////

    [Theory]
    [InlineData(3, 3, false)]
    [InlineData(2, 3, true)]
    [InlineData(1, 3, true)]
    public void LessThanOperator(int v1, int v2, bool result)
    {
        var (m1, m2) = GetMoments(v1, v2);

        (m1 < m2).Should().Be(result);
    }

    //////////////////////////////

    [Theory]
    [InlineData(4, 3, false)]
    [InlineData(3, 3, true)]
    [InlineData(2, 3, true)]
    [InlineData(1, 3, true)]
    public void LessThanOrEqualOperator(int v1, int v2, bool result)
    {
        var (m1, m2) = GetMoments(v1, v2);

        (m1 <= m2).Should().Be(result);
    }

    //////////////////////////////

    [Theory]
    [InlineData(3, 3, false)]
    [InlineData(3, 2, true)]
    [InlineData(3, 1, true)]
    public void GreaterThanOperator(int v1, int v2, bool result)
    {
        var (m1, m2) = GetMoments(v1, v2);

        (m1 > m2).Should().Be(result);
    }

    //////////////////////////////

    [Theory]
    [InlineData(3, 4, false)]
    [InlineData(3, 3, true)]
    [InlineData(3, 2, true)]
    [InlineData(3, 1, true)]
    public void GreaterThanOrEqualOperator(int v1, int v2, bool result)
    {
        var (m1, m2) = GetMoments(v1, v2);

        (m1 >= m2).Should().Be(result);
    }

    //////////////////////////////

    [Theory]
    [InlineData(1, 2, -1)]
    [InlineData(1, 1, 0)]
    [InlineData(2, 1, 1)]
    public void CompareTo(int v1, int v2, int result)
    {
        var (m1, m2) = GetMoments(v1, v2);

        m1.CompareTo(m2).Should().Be(result);
    }

    //////////////////////////////

    private static (Moment, Moment) GetMoments(int lhs, int rhs) =>
        (Moment.From(lhs), Moment.From(rhs));
}