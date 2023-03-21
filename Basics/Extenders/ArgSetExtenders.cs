// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public static class ArgSetExtenders
{
    internal static string ToTypeName(this Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        var typeName = type.FullName;

        var assemblyName = type.Assembly.GetName().Name;

        if (assemblyName != "System.Private.CoreLib")
            typeName += $", {assemblyName}";

        return typeName!;
    }

    internal static T GetAs<T>(this object @object) =>
        (T)@object;

    internal static bool IsArgType(this object value)
    {
        if (value.GetType().IsEnum)
            return true;

        return value switch
        {
            bool => true,
            byte => true,
            DateOnly => true,
            DateTime => true,
            decimal => true,
            double => true,
            EmailAddress => true,
            float => true,
            Guid => true,
            int => true,
            long => true,
            Name => true,
            PhoneNumber => true,
            Quantity => true,
            Ratchet => true,
            short => true,
            string => true,
            TimeOnly => true,
            TimeSpan => true,
            Uri => true,
            _ => false
        };
    }
}