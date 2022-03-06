// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com)
//
// This file is part of SquidEyes.Basics
//
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public static class MiscExtenders
{
    public static void DoIfCanDo<T>(this T value, Func<T, bool> canDo, Action<T> @do)
    {
        if (canDo(value))
            @do(value);
    }

    public static R AsFunc<T, R>(this T value, Func<T, R> func) => func(value);

    public static void AsAction<T>(this T value, Action<T> action) => action(value);
}