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
using System.IO;
using Xunit;

namespace SquidEyes.UnitTests;

public class CsvEnumeratorTests
{
    [Theory]
    [InlineData("0", 1, false, 1)]
    [InlineData("A,B,C,D", 4, true, 0)]
    [InlineData("0,1,2,3\n0,1,2,3\n0,1,2,3", 4, false, 3)]
    [InlineData("A,B,C,D\n0,1,2,3\n0,1,2,3", 4, true, 2)]
    public void ShouldLoadAndEnumerateWithGoodData(
        string csv, int expectedFields, bool skipHeader, int expectedRows)
    {
        var rows = 0;

        foreach (var fields in new CsvEnumerator(
            csv.ToStream(), expectedFields, skipHeader))
        {
            for (int i = 0; i < expectedFields; i++)
                fields[i].Should().Be(i.ToString());

            rows++;
        }

        rows.Should().Be(expectedRows);
    }

    [Theory]
    [InlineData("0", 0, false)]
    [InlineData("A\n0", 0, true)]
    public void ShouldThrowErrorWithBadArgs(
        string csv, int expectedFields, bool skipHeader)
    {
        new CsvEnumerator(csv.ToStream(), expectedFields, skipHeader)
            .Enumerating(x => x).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("A", 2, false)]
    [InlineData("A", 2, true)]
    [InlineData("0", 2, false)]
    [InlineData("0,1,2", 2, false)]
    [InlineData("A,B\n0", 2, true)]
    [InlineData("A,B\n0,1,2", 2, true)]
    public void ShouldThrowErrorWithBadData(
        string csv, int expectedFields, bool skipHeader)
    {
        new CsvEnumerator(csv.ToStream(), expectedFields, skipHeader)
            .Enumerating(x => x).Should().Throw<InvalidDataException>();
    }
}
