// Copyright © 2021 by SquidEyes, LLC
// 
// Permission is hereby granted, free of charge, to any person obtaining 
// a copy of this software and associated documentation files (the “Software”),
// to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
// IN THE SOFTWARE.

namespace SquidEyes.Basics;

public static class IEnumerableExtenders
{
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (var item in items)
            action(item);
    }

    public static bool IsUnique<T>(this IEnumerable<T> values) =>
        values.All(new HashSet<T>().Add);

    public static bool HasItems<T>(
        this IEnumerable<T> items, bool nonDefault = true)
    {
        return items.HasItems(1, int.MaxValue,
            v => !nonDefault || !Equals(v, default(T)));
    }

    public static bool HasItems<T>(
        this IEnumerable<T> items, Func<T, bool>? isValid)
    {
        return items.HasItems(1, int.MaxValue, isValid);
    }

    public static bool HasItems<T>(this IEnumerable<T> items,
        int minItems, int maxItems, bool nonDefault = true)
    {
        return items.HasItems(minItems, maxItems, 
            v => !nonDefault || !Equals(v, default(T)));
    }

    public static bool HasItems<T>(this IEnumerable<T> items,
        int minItems, int maxItems, Func<T, bool>? isValid)
    {
        if (minItems < 1)
            throw new ArgumentOutOfRangeException(nameof(minItems));

        if (maxItems < minItems)
            throw new ArgumentOutOfRangeException(nameof(maxItems));

        int count = 0;

        foreach (var item in items)
        {
            if (isValid != null && !isValid(item))
                return false;

            if (++count > maxItems)
                return false;
        }

        return count >= minItems;
    }

    public static IEnumerable<List<T>> Chunked<T>(
        this IEnumerable<T> enumerable, int chunkSize)
    {
        static IEnumerable<T> GetChunk(IEnumerator<T> e, 
            Func<bool> innerMoveNext)
        {
            do
            {
                yield return e.Current;
            }
            while (innerMoveNext());
        }

        if (chunkSize < 1)
            throw new ArgumentOutOfRangeException(nameof(chunkSize));

        using var e = enumerable.GetEnumerator();

        while (e.MoveNext())
        {
            var remaining = chunkSize;

            var innerMoveNext = new Func<bool>(
                () => --remaining > 0 && e.MoveNext());

            yield return GetChunk(e, innerMoveNext).ToList();

            while (innerMoveNext()) ;
        }
    }
}
