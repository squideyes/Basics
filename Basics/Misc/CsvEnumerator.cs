// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com) 
// 
// This file is part of SquidEyes.Basics
// 
// The use of this source code is licensed under the terms 
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Basics;

public class CsvEnumerator : IEnumerable<string[]>, IDisposable
{
    private readonly StreamReader reader;
    private readonly int expectedFields;

    private bool skipFirst;

    public CsvEnumerator(StreamReader reader, int expectedFields, bool skipFirst = false)
    {
        if (expectedFields <= 0)
            throw new ArgumentOutOfRangeException(nameof(expectedFields));

        this.reader = reader;
        this.expectedFields = expectedFields;
        this.skipFirst = skipFirst;
    }

    public CsvEnumerator(Stream stream, int expectedFields, bool skipFirst = false)
        : this(new StreamReader(stream), expectedFields, skipFirst)
    {
    }

    public CsvEnumerator(string fileName, int expectedFields, bool skipFirst = false)
        : this(File.OpenRead(fileName), expectedFields, skipFirst)
    {
    }

    public void Dispose()
    {
        if (reader != null)
            reader.Dispose();
    }

    public IEnumerator<string[]> GetEnumerator()
    {
        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            var fields = line.Split(',');

            if (fields.Length != expectedFields)
            {
                throw new InvalidDataException(
                    $"{expectedFields} expected; {fields.Length} found");
            }

            if (skipFirst)
            {
                skipFirst = false;

                continue;
            }

            yield return fields;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
