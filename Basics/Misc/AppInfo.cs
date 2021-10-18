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

using System.Reflection;
using System.Text;

namespace SquidEyes.Basics;

public class AppInfo
{
    public AppInfo(Assembly assembly)
    {
        string GetValue<T>(Func<T, string> getValue) where T : Attribute =>
            getValue(assembly.GetAttribute<T>()!);

        Product = GetValue<AssemblyProductAttribute>(a => a.Product);
        Company = GetValue<AssemblyCompanyAttribute>(a => a.Company);
        PackageId = GetValue<AssemblyTitleAttribute>(a => a.Title);
        Version = assembly.GetName().Version!;
        Copyright = GetValue<AssemblyCopyrightAttribute>(a => a.Copyright);
    }

    public string Product { get; }
    public string PackageId { get; }
    public Version Version { get; }
    public string Company { get; }
    public string Copyright { get; }

    public string Title => GetTitle(PackageId, Version);

    public static string GetTitle(string packageId, Version version)
    {
        if (string.IsNullOrWhiteSpace(packageId))
            throw new ArgumentOutOfRangeException(nameof(packageId));

        var sb = new StringBuilder();

        sb.Append(packageId);

        sb.Append(" v");
        sb.Append(version.Major);
        sb.Append('.');
        sb.Append(version.Minor);

        if ((version.Build >= 1) || (version.Revision >= 1))
        {
            sb.Append('.');
            sb.Append(version.Build);
        }

        if (version.Revision >= 1)
        {
            sb.Append('.');
            sb.Append(version.Revision);
        }

        return sb.ToString();
    }
}
