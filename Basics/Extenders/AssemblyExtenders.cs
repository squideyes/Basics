// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Reflection;

namespace SquidEyes.Basics;

public static class AssemblyExtenders
{
    public static T? GetAttribute<T>(this Assembly assembly)
        where T : Attribute
    {
        T? result = default;

        var attribs = Attribute.GetCustomAttributes(
            assembly, typeof(T), false);

        if (attribs != null && attribs.Any())
            result = (T)attribs[0];

        return result;
    }
}