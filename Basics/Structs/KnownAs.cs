// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.RegularExpressions;
using Vogen;

namespace SquidEyes.Basics;

[ValueObject(typeof(string))]
public partial struct KnownAs
{
    private static readonly Regex validator =
        new(@"^[A-Z][A-Za-z0-9]{0,23}$", RegexOptions.Compiled);

    private static Validation Validate(string value) => IsValid(value) ? 
        Validation.Ok : 
        Validation.Invalid($"A KnownAs must conform to {validator}");

    public static bool IsValid(string value) => validator.IsMatch(value);
}