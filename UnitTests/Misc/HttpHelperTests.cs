// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com) 
// 
// This file is part of SquidEyes.Basics
// 
// The use of this source code is licensed under the terms 
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using Moq;
using Moq.Protected;
using SquidEyes.Basics;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SquidEyes.UnitTests;

public class HttpHelperTests
{
    private class Data
    {
        public int Id { get; init; }
        public string? Code { get; init; }
    }

    private const string GOOD_URL = "http://someco.com/";

    [Fact]
    public void ConstructsWithGoodUrl() =>
        _ = new HttpHelper(new HttpClient(), GOOD_URL);

    [Fact]
    public void ConstructsWithGoodUri() =>
        _ = new HttpHelper(new HttpClient(), new Uri(GOOD_URL));

    [Theory]
    [InlineData("http://someco|com", UriKind.Absolute)]
    [InlineData("data", UriKind.Absolute)]
    [InlineData("data", UriKind.Relative)]
    [InlineData("data", UriKind.RelativeOrAbsolute)]
    public void BadArgsThrowErrorsOnConstruct(string uriString, UriKind uriKind)
    {
        FluentActions.Invoking(() => new HttpHelper(new HttpClient(),
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
        var helper = new HttpHelper(new HttpClient(), GOOD_URL);

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
        var helper = new HttpHelper(new HttpClient(), GOOD_URL);

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
        var helper = new HttpHelper(new HttpClient(), GOOD_URL);

        FluentActions.Invoking(() => helper.SetQueryParam(name, value))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GetAsyncShouldReturnExpectedValue(bool toJson)
    {
        const string JSON = "[{ \"Id\": 1, \"Code\": \"ABC\"}, { \"Id\": 2, \"Code\": null }]";

        var handler = new Mock<HttpMessageHandler>();

        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JSON),
        };

        handler.Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(response);

        var client = new HttpClient(handler.Object);

        var helper = new HttpHelper(client, GOOD_URL);

        if (toJson)
        {
            var datas = await helper.GetJsonAsync<List<Data>>();

            datas.Should().NotBeNull();
            datas!.Count.Should().Be(2);
            datas![0].Should().BeEquivalentTo(new Data() { Id = 1, Code = "ABC" });
            datas![1].Should().BeEquivalentTo(new Data() { Id = 2 });
        }
        else
        {
            (await helper.GetStringAsync()).Should().Be(JSON);
        }

        handler.Protected().Verify(
           "SendAsync",
           Times.Exactly(1),
           ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
           ItExpr.IsAny<CancellationToken>());
    }
}
