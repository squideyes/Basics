// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public static class IoExtenders
{
    public static void EnsurePathExists(this string path, bool isFileName = false)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));

        string fullPath;

        if (isFileName)
            fullPath = Path.GetDirectoryName(Path.GetFullPath(path))!;
        else
            fullPath = Path.GetFullPath(path);

        if (!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath!);
    }
}