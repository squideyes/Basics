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

public class MinMaxStepTests
{
    [Fact]
    public void ConstructorWithGoodArgs()
    {
        var mms = new MinMaxStep(1, 5, 2, 3);

        mms.Minimum.Should().Be(1);
        mms.Maximum.Should().Be(5);
        mms.Step.Should().Be(2);
        mms.Value.Should().Be(3);
    }

    [Theory]
    [InlineData(double.NaN, 3.0, 1.0, 1.0)]
    [InlineData(1, double.NaN, 1.0, 1.0)]
    [InlineData(3.0, 1.0, 1.0, 1.0)]
    [InlineData(1.0, 3.0, 0.0, 1.0)]
    [InlineData(1.0, 3.0, 1.0, 5.0)]
    public void ConstructorWithBadArgs(
        double minimum, double maximum, double step, double value)
    {
        FluentActions.Invoking(() => 
            new MinMaxStep(minimum, maximum, step, value))
                .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(3, 3)]
    public void IncrementReturnsExpectedValue(double value, double expected)
    {
        var minMaxStep = new MinMaxStep(1, 3, 1, value);

        minMaxStep.Increment().Should().Be(expected);
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(2, 1)]
    [InlineData(1, 1)]
    public void DecrementReturnsExpectedValue(double value, double expected)
    {
        var minMaxStep = new MinMaxStep(1, 3, 1, value);

        minMaxStep.Decrement().Should().Be(expected);
    }

    [Fact]
    public void ToStringSHouldReturnValueToString()
    {
        var mms = new MinMaxStep(1, 5, 2, 3);

        mms.ToString().Should().Be(mms.Value.ToString());
    }
}