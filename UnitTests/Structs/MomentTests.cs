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

    ////////////////////////////

    [Theory]
    [InlineData("00:00:00.000")]
    [InlineData("23:59:00.000")]
    [InlineData("23:59:59.000")]
    [InlineData("23:59:59.999")]
    public void FromWithGoodArgs(string value)
    {
        var timeOnly = TimeOnly.Parse(value);

        var moment1 = Moment.From(timeOnly);

        moment1.Value.Should().Be(timeOnly);

        moment1.AsTimeSpan().Should().Be(TimeSpan.Parse(value));

        moment1.AsTimeOnly().Should().Be(timeOnly);

        moment1.Hour.Should().Be(timeOnly.Hour);
        moment1.Minute.Should().Be(timeOnly.Minute);
        moment1.Second.Should().Be(timeOnly.Second);
        moment1.Millisecond.Should().Be(timeOnly.Millisecond);

        moment1.ToString().Should().Be(
            timeOnly.ToString("HH\\:mm\\:ss\\.fff"));

        moment1.ToString(true).Should().Be(
            timeOnly.ToString("HH\\:mm"));

        moment1.ToString(false).Should().Be(
            timeOnly.ToString("HH\\:mm\\:ss\\.fff"));

        var moment2 = timeOnly.AsFunc(t => Moment.From(
            t.Hour, t.Minute, t.Second, t.Millisecond));

        moment1.Value.Should().Be(moment2.Value);
        moment1.AsTimeSpan().Should().Be(moment2.AsTimeSpan());
        moment1.AsTimeOnly().Should().Be(moment2.AsTimeOnly());
        moment1.Hour.Should().Be(moment2.Hour);
        moment1.Minute.Should().Be(moment2.Minute);
        moment1.Second.Should().Be(moment2.Second);
        moment1.Millisecond.Should().Be(moment2.Millisecond);
        moment1.ToString().Should().Be(moment2.ToString());
        moment1.ToString(true).Should().Be(moment2.ToString(true));
        moment1.ToString(false).Should().Be(moment2.ToString(false));
    }

    ////////////////////////////

    [Theory]
    [InlineData("-00:00:00.000", false)]
    [InlineData(" ", false)]
    [InlineData("XXX", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("00:00:00.000", true)]
    [InlineData("23:59:00.000", true)]
    [InlineData("23:59:59.000", true)]
    [InlineData("23:59:59.999", true)]
    [InlineData("1.00:00:00.00", false)]
    public void IsValidWithMixedArgs(string value, bool expected) =>
        Moment.IsValid(value).Should().Be(expected);

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

    ////////////////////////////////

    private static (Moment, Moment) GetMoments(int lhs, int rhs) =>
        (Moment.From(new TimeOnly(0, 0, lhs, 0)),
        Moment.From(new TimeOnly(0, 0, rhs, 0)));
}