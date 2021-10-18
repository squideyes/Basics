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

public static class NumericExtenders
{
    private const double DOUBLE_EPSILON = 0.00000001;
    private const float FLOAT_EPSILON = 0.00000001f;

    public static bool Approximates(this double a, double b) =>
        Math.Abs(a - b) < DOUBLE_EPSILON;

    public static bool Approximates(this float a, float b) =>
        MathF.Abs(a - b) < FLOAT_EPSILON;

    public static float ConditionalUpdate(float oldValue,
        float newValue, Func<float, float, bool> canUpdate)
    {
        if (canUpdate(oldValue, newValue))
            return newValue;
        else
            return oldValue;
    }

    public static double ConditionalUpdate(double oldValue,
        double newValue, Func<double, double, bool> canUpdate)
    {
        if (canUpdate(oldValue, newValue))
            return newValue;
        else
            return oldValue;
    }
}
