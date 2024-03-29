// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Security.Cryptography;
using System.Text;

namespace SquidEyes.Basics;

public static class CryptoHelper
{
    public static string GetHash(string name, string email, string mobile) =>
        GetSha256Hash(name + email + mobile);

    private static string GetSha256Hash(string value)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));

        var builder = new StringBuilder();

        for (int i = 0; i < bytes.Length; i++)
            builder.Append(bytes[i].ToString("x2"));

        return builder.ToString();
    }
}