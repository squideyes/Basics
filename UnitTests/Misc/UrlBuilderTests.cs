// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Basics;
using System;
using Xunit;

namespace SquidEyes.UnitTests;

public class UrlBuilderTests
{
    private class Data
    {
        public int Id { get; init; }
        public string? Code { get; init; }
    }

    private const string GOOD_URL = "http://someco.com/";

    [Fact]
    public void ConstructsWithGoodUrl() => _ = new UrlBuilder(GOOD_URL);

    [Fact]
    public void ConstructsWithGoodUri() => _ = new UrlBuilder(new Uri(GOOD_URL));

    [Theory]
    [InlineData("http://someco|com", UriKind.Absolute)]
    [InlineData("data", UriKind.Absolute)]
    [InlineData("data", UriKind.Relative)]
    [InlineData("data", UriKind.RelativeOrAbsolute)]
    public void BadArgsThrowErrorsOnConstruct(string uriString, UriKind uriKind)
    {
        FluentActions.Invoking(() => new UrlBuilder(
            new Uri(uriString, uriKind))).Should().Throw<Exception>();
    }

    [Theory]
    [InlineData(null, null, null, null, GOOD_URL)]
    [InlineData(null, null, "CCC", null, GOOD_URL + "?code=CCC")]
    [InlineData(null, null, null, 1, GOOD_URL + "?number=1")]
    [InlineData(null, null, "CCC", 1, GOOD_URL + "?code=CCC&number=1")]
    [InlineData("AAA", null, null, null, GOOD_URL + "AAA")]
    [InlineData("AAA", null, "CCC", null, GOOD_URL + "AAA?code=CCC")]
    [InlineData("AAA", null, null, 1, GOOD_URL + "AAA?number=1")]
    [InlineData("AAA", null, "CCC", 1, GOOD_URL + "AAA?code=CCC&number=1")]
    [InlineData(null, "BBB", null, null, GOOD_URL + "BBB")]
    [InlineData(null, "BBB", "CCC", null, GOOD_URL + "BBB?code=CCC")]
    [InlineData(null, "BBB", null, 1, GOOD_URL + "BBB?number=1")]
    [InlineData(null, "BBB", "CCC", 1, GOOD_URL + "BBB?code=CCC&number=1")]
    [InlineData("AAA", "BBB", null, null, GOOD_URL + "AAA/BBB")]
    [InlineData("AAA", "BBB", "CCC", null, GOOD_URL + "AAA/BBB?code=CCC")]
    [InlineData("AAA", "BBB", null, 1, GOOD_URL + "AAA/BBB?number=1")]
    [InlineData("AAA", "BBB", "CCC", 1, GOOD_URL + "AAA/BBB?code=CCC&number=1")]
    public void GetUriShouldReturnExpectedUri(string segment1, string segment2,
        string code, int? number, string expectedUrl)
    {
        var helper = new UrlBuilder(GOOD_URL);

        if (segment1 != null)
            helper.AppendPathSegment(segment1);

        if (segment2 != null)
            helper.AppendPathSegment(segment2);

        if (code != null)
            helper.SetQueryParam("code", code);

        if (number.HasValue)
            helper.SetQueryParam("number", number.ToString());

        var uri = helper.GetUri();

        uri.AbsoluteUri.Should().Be(expectedUrl);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("|")]
    public void BadArgsThrowErrorsOnAppendPathSegment(string segment)
    {
        var helper = new UrlBuilder(GOOD_URL);

        FluentActions.Invoking(() => helper.AppendPathSegment(segment))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("CODE", "")]
    [InlineData("CODE", " ")]
    [InlineData("CODE", "|")]
    [InlineData("", "VALUE")]
    [InlineData(" ", "VALUE")]
    public void BadArgsThrowErrorsOnSetQueryParam(string name, string value)
    {
        var helper = new UrlBuilder(GOOD_URL);

        FluentActions.Invoking(() => helper.SetQueryParam(name, value))
            .Should().Throw<ArgumentOutOfRangeException>();
    }
}