// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com)
//
// This file is part of SquidEyes.Basics
//
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;

namespace SquidEyes.Basics;

public class DialSet
{
    internal enum DataKind
    {
        Bool = 1,
        Byte,
        Short,
        Int,
        Long,
        Float,
        Double,
        Enum,
        Uri,
        String,
        DateTime,
        DateOnly,
        TimeOnly,
        TimeSpan
    }

    private readonly Dictionary<string, (DataKind DataKind, IConvertible Value)> data = new();

    public int Count => data.Count;

    public void Upsert(string key, bool value) =>
        Upsert(key, DataKind.Bool, value);

    public void Upsert(string key, byte value) =>
        Upsert(key, DataKind.Byte, value);

    public void Upsert(string key, short value) =>
        Upsert(key, DataKind.Short, value);

    public void Upsert(string key, int value) =>
        Upsert(key, DataKind.Int, value);

    public void Upsert(string key, long value) =>
        Upsert(key, DataKind.Long, value);

    public void Upsert(string key, float value) =>
        Upsert(key, DataKind.Float, value);

    public void Upsert(string key, double value) =>
        Upsert(key, DataKind.Double, value);

    public void Upsert(string key, Enum value) =>
        Upsert(key, DataKind.Enum, value.ToString());

    public void Upsert(string key, Uri value) =>
        Upsert(key, DataKind.Uri, value.AbsoluteUri);

    public void Upsert(string key, string value) =>
        Upsert(key, DataKind.String, value);

    public void Upsert(string key, DateTime value) =>
        Upsert(key, DataKind.DateTime, value);

    public void Upsert(string key, DateOnly value) =>
        Upsert(key, DataKind.DateOnly, value.ToDateTime(new TimeOnly()));

    public void Upsert(string key, TimeOnly value) =>
        Upsert(key, DataKind.TimeOnly, value.Ticks);

    public void Upsert(string key, TimeSpan value) =>
        Upsert(key, DataKind.TimeSpan, value.Ticks);

    public bool GetBool(string key) =>
        Get(key, DataKind.Bool, v => (bool)v);

    public byte GetByte(string key) =>
        Get(key, DataKind.Byte, v => (byte)v);

    public short GetShort(string key) =>
        Get(key, DataKind.Short, v => (short)v);

    public int GetInt(string key) =>
        Get(key, DataKind.Int, v => (int)v);

    public long GetLong(string key) =>
        Get(key, DataKind.Long, v => (long)v);

    public float GetFloat(string key) =>
        Get(key, DataKind.Float, v => (float)v);

    public double GetDouble(string key) =>
        Get(key, DataKind.Double, v => (double)v);

    public T GetEnum<T>(string key) =>
        Get(key, DataKind.Enum, v => ((string)v).ToEnumValue<T>());

    public string GetString(string key) =>
        Get(key, DataKind.String, v => (string)v);

    public Uri GetUri(string key) =>
        Get(key, DataKind.Uri, v => new Uri((string)v));

    public DateOnly GetDateOnly(string key) =>
        Get(key, DataKind.DateOnly, v => DateOnly.FromDateTime((DateTime)v));

    public TimeOnly GetTimeOnly(string key) =>
        Get(key, DataKind.TimeOnly, v => new TimeOnly((long)v));

    public DateTime GetDateTime(string key) =>
        Get(key, DataKind.DateTime, v => (DateTime)v);

    public TimeSpan GetTimeSpan(string key) =>
        Get(key, DataKind.TimeSpan, v => new TimeSpan((long)v));

    private T Get<T>(string key, DataKind dataKind, Func<IConvertible, T> convert)
    {
        var tuple = data[key];

        if (dataKind != tuple.DataKind)
            throw new InvalidOperationException(nameof(dataKind));

        return convert(tuple.Value);
    }

    public bool ContainsKey(string key) => data.ContainsKey(key);

    public bool ContainsKeys(params string[] keys)
    {
        if (Count != keys.Length)
            return false;

        foreach (var key in keys)
        {
            if (!ContainsKey(key))
                return false;
        }

        return true;
    }

    private void Upsert(string key, DataKind dataKind, IConvertible value)
    {
        if (!key.IsNonEmptyAndTrimmed())
            throw new ArgumentOutOfRangeException(nameof(key));

        if (data.ContainsKey(key))
            data[key] = (dataKind, value);
        else
            data.Add(key, (dataKind, value));
    }

    private static string GetValueString(DataKind dataKind, IConvertible value)
    {
        return dataKind switch
        {
            DataKind.DateOnly => ((DateTime)value).ToString("MM/dd/yyyy"),
            DataKind.TimeOnly => new TimeOnly((long)value).ToString("HH:mm:ss.fff"),
            DataKind.TimeSpan => new TimeSpan((long)value).ToString(@"d\.hh\:mm\:ss\.fff"),
            _ => value.ToString()!
        };
    }

    public Dictionary<string, string> ToDictionary()
    {
        var dict = new Dictionary<string, string>();

        foreach (var key in data.Keys)
        {
            var (dataKind, value) = data[key];

            dict.Add(key, GetValueString(dataKind, value));
        }

        return dict;
    }

    public string ToQueryString()
    {
        var sb = new StringBuilder();

        foreach (var key in data.Keys)
        {
            if (sb.Length == 0)
                sb.Append('?');
            else
                sb.Append('&');

            sb.Append(key);

            sb.Append('=');

            var (dataKind, value) = data[key];

            sb.Append(GetValueString(dataKind, value));
        }

        return sb.ToString();
    }
}