// ********************************************************
// Copyright (C) 2021 Louis S. Berman (louis@squideyes.com)
//
// This file is part of SquidEyes.Basics
//
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Basics;

public static class ObjectExtenders
{
    public static List<T> AsList<T>(this T item) => new() { item };

    public static HashSet<T> AsHashSet<T>(this T item) => new() { item };
}