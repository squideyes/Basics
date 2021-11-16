// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com) 
// 
// This file is part of SquidEyes.Basics
// 
// The use of this source code is licensed under the terms 
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Basics;

public class HttpHelper
{
    private readonly List<string> segments = new();
    private readonly Dictionary<string, string?> queryParams = new();

    private readonly HttpClient client;
    private readonly Uri baseUri;

    private JsonSerializerOptions? jsonSerializerOptions = null;

    public HttpHelper(HttpClient client, string uriString)
        : this(client, new Uri(uriString))
    {
    }

    public HttpHelper(HttpClient client, Uri uri)
    {
        if (!uri.IsAbsoluteUri)
            throw new ArgumentOutOfRangeException(nameof(uri));

        this.client = client;

        baseUri = new Uri(uri.GetLeftPart(UriPartial.Authority));

        segments.AddRange(uri.LocalPath.Split('/')
            .Where(s => !string.IsNullOrWhiteSpace(s)));
    }

    public HttpHelper AppendPathSegment(string segment)
    {
        if (string.IsNullOrWhiteSpace(segment))
            throw new ArgumentOutOfRangeException(nameof(segment));

        if (!Uri.IsWellFormedUriString(segment, UriKind.Relative))
            throw new ArgumentOutOfRangeException(nameof(segment));

        segments.Add(segment);

        return this;
    }

    public HttpHelper SetQueryParam(string token, string? value = null)
    {
        if (token.IsEmptyOrWhitespace())
            throw new ArgumentOutOfRangeException(nameof(token));

        if (!Uri.IsWellFormedUriString(value, UriKind.Relative))
            throw new ArgumentOutOfRangeException(nameof(value));

        if (value != null)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (!Uri.IsWellFormedUriString(value, UriKind.Relative))
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        queryParams.Add(token, value);

        return this;
    }

    public Uri GetUri()
    {
        var sb = new StringBuilder();

        sb.Append(baseUri.AbsoluteUri);
        sb.Append(string.Join("/", segments));

        int count = 0;

        foreach (var key in queryParams.Keys)
        {
            sb.Append(count++ == 0 ? '?' : '&');
            sb.Append(key);

            if (queryParams[key] != null)
            {
                sb.Append('=');
                sb.Append(queryParams[key]);
            }
        }

        return new Uri(sb.ToString());
    }

    public async Task<string> GetStringAsync() => await client.GetStringAsync(GetUri());

    public async Task<T?> GetJsonAsync<T>(JsonSerializerOptions? options = null)
        where T : class, new()
    {
        if (jsonSerializerOptions == null)
            jsonSerializerOptions = GetJsonSerializerOptions();

        var json = await GetStringAsync();

        return JsonSerializer.Deserialize<T?>(json, options ?? jsonSerializerOptions);
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        options.Converters.Add(new JsonStringEnumConverter());

        return options;
    }
}
