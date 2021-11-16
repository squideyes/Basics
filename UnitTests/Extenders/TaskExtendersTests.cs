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
using System.Threading.Tasks;
using Xunit;

namespace SquidEyes.UnitTests;

public class TaskExtendersTests
{
    [Fact]
    public void ForgetWorksAsExpected()
    {
        var startedOn = DateTime.UtcNow;

        Task.Run(async () => await Task.Delay(500)).Forget();

        (DateTime.UtcNow - startedOn).Should().BeLessThan(
            TimeSpan.FromMilliseconds(500));
    }
}
