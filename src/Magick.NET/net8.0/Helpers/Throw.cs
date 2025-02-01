// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ImageMagick;

internal static partial class Throw
{
    public static void IfEmpty<T>([NotNull] ReadOnlySequence<T> value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value.IsEmpty)
            throw new ArgumentException("Value cannot be empty.", paramName);
    }

    public static void IfEmpty<T>([NotNull] ReadOnlySpan<T> value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value.IsEmpty)
            throw new ArgumentException("Value cannot be empty.", paramName);
    }
}

#endif

