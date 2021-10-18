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

using System.Runtime.InteropServices;

namespace SquidEyes.Basics;

public static class DateTimeExtenders
{
    private static readonly TimeZoneInfo eastern =
        GetTimeZoneInfo("Eastern Standard Time", "America/New_York");

    private static readonly TimeZoneInfo central =
        GetTimeZoneInfo("Central Standard Time", "America/Chicago");

    private static readonly TimeZoneInfo mountain =
        GetTimeZoneInfo("Mountain Standard Time", "America/Denver");

    private static readonly TimeZoneInfo pacific =
        GetTimeZoneInfo("Pacific Standard Time", "America/Los_Angeles");

    public static DateTime ToEasternFromUtc(this DateTime value) =>
        value.ToZoneFromUtc(eastern);

    public static DateTime ToCentralFromUtc(this DateTime value) =>
        value.ToZoneFromUtc(central);

    public static DateTime ToMountainFromUtc(this DateTime value) =>
        value.ToZoneFromUtc(mountain);

    public static DateTime ToPacificFromUtc(this DateTime value) =>
        value.ToZoneFromUtc(pacific);

    public static DateTime ToUtcFromEastern(this DateTime value) =>
        value.ToUtcFromZone(eastern);

    public static DateTime ToUtcFromCentral(this DateTime value) =>
        value.ToUtcFromZone(central);

    public static DateTime ToUtcFromMountain(this DateTime value) =>
        value.ToUtcFromZone(mountain);

    public static DateTime ToUtcFromPacific(this DateTime value) =>
        value.ToUtcFromZone(pacific);

    private static DateTime ToZoneFromUtc(this DateTime value, TimeZoneInfo tzi)
    {
        if (value.Kind != DateTimeKind.Utc)
            throw new ArgumentOutOfRangeException(nameof(value));

        return TimeZoneInfo.ConvertTimeFromUtc(value, tzi);
    }

    private static DateTime ToUtcFromZone(this DateTime value, TimeZoneInfo tzi)
    {
        if (value.Kind != DateTimeKind.Unspecified)
            throw new ArgumentOutOfRangeException(nameof(value));

        return TimeZoneInfo.ConvertTimeToUtc(value, tzi);
    }

    public static bool IsDate(this DateTime value) =>
        value.TimeOfDay == TimeSpan.Zero;

    public static bool IsWeekday(this DateTime value)
    {
        return value.DayOfWeek switch
        {
            DayOfWeek.Saturday or DayOfWeek.Sunday => false,
            _ => true,
        };
    }

    public static bool OnInterval(this DateTime value, int seconds)
    {
        if (seconds <= 0)
            throw new ArgumentOutOfRangeException(nameof(seconds));

        return value.Ticks % TimeSpan.FromSeconds(seconds).Ticks == 0L;
    }

    private static TimeZoneInfo GetTimeZoneInfo(string windowsId, string linuxId)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return TimeZoneInfo.FindSystemTimeZoneById(windowsId);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return TimeZoneInfo.FindSystemTimeZoneById(linuxId);
        else
            throw new InvalidOperationException("Only works on Linux and Windows");
    }
}
