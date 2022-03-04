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

public class AppInfoTests
{
    [Fact]
    public void AppInfoConstructsAsExpected()
    {
        var assembly = typeof(AppInfoTests).Assembly;

        var appInfo = new AppInfo(assembly);

        appInfo.Company.Should().Be("SquidEyes, LLC");
        appInfo.PackageId.Should().Be(assembly.GetName().Name);
        appInfo.Copyright.Should().Be("Copyright 2021 by SquidEyes, LLC");
        appInfo.Product.Should().Be(assembly.GetName().Name);
        appInfo.Version.Should().Be(new System.Version(1, 0, 0, 0));
        appInfo.Title.Should().Be("SquidEyes.UnitTests v1.0");
    }

    [Theory]
    [InlineData("SomeApp", "1.0", "SomeApp v1.0")]
    [InlineData("SomeApp", "1.0.0", "SomeApp v1.0")]
    [InlineData("SomeApp", "1.0.0.0", "SomeApp v1.0")]
    [InlineData("SomeApp", "1.0.1", "SomeApp v1.0.1")]
    [InlineData("SomeApp", "1.0.0.1", "SomeApp v1.0.0.1")]
    public void GetTitleReturnsExpectedValue(
        string packageId, string versionString, string title)
    {
        AppInfo.GetTitle(packageId,
            System.Version.Parse(versionString)).Should().Be(title);
    }
}