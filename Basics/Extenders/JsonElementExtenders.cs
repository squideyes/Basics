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

using System.Text.Json;

namespace SquidEyes.Basics;

public static class JsonElementExtenders
{
    public static bool GetBoolean(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetBoolean();

    public static string GetString(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetString()!;

    public static float GetFloat(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetSingle();

    public static double GetDouble(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetDouble();

    public static int GetInt32(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetInt32();

    public static Uri GetUri(this JsonElement element, string propertyName)
    {
        return element.GetProperty(propertyName).GetString()
            .AsFunc(v => v == null ? null : new Uri(v))!;
    }

    public static T GetEnumValue<T>(this JsonElement element, string propertyName)
    {
        return element.GetProperty(propertyName).GetString()
            .AsFunc(v => v == null ? default : v.ToEnumValue<T>())!;
    }

    public static DateTime GetDateTime(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetDateTime();

    public static TimeSpan GetTimeSpan(this JsonElement element, string propertyName)
    {
        return element.GetProperty(propertyName).GetString()
           .AsFunc(v => v == null ? default : TimeSpan.Parse(v));
    }
}
