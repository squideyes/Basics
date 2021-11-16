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
using Xunit;

namespace SquidEyes.UnitTests;

public class ObjectExtendersTests
{
    [Fact]
    public void AsListExtenderWorksWithSingleArg()
    {
        var list = "AAA".AsList();

        list.Count.Should().Be(1);

        list.Should().ContainInOrder("AAA");
    }
}