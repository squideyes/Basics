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
using System.Text;
using Xunit;

namespace SquidEyes.UnitTests;

public class StringBuilderExtendersTests
{
    [Theory]
    [InlineData(',', "1,2")]
    [InlineData('|', "1|2")]
    public void X(char delimiter, string result2)
    {
        var sb = new StringBuilder();

        sb.AppendDelimited(1, delimiter);
        sb.ToString().Should().Be("1");

        sb.AppendDelimited(2, delimiter);
        sb.ToString().Should().Be(result2);
    }
}
