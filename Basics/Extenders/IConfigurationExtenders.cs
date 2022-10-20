// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Configuration;

namespace SquidEyes.Basics;

public static class IConfigurationExtenders
{
    public static T GetAs<T>(this IConfiguration config, string name)
        where T : IConvertible
    {
        if (!name.IsNonEmptyAndTrimmed())
            throw new ArgumentOutOfRangeException(nameof(name));

        return (T)Convert.ChangeType(config[name], typeof(T));
    }
}