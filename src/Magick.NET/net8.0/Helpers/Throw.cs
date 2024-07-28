// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick;

internal static partial class Throw
{
    public static void IfEmpty<T>(string paramName, [NotNull] ReadOnlySequence<T> value)
    {
        if (value.IsEmpty)
            throw new ArgumentException("Value cannot be empty.", paramName);
    }

    public static void IfEmpty<T>(string paramName, [NotNull] ReadOnlySpan<T> value)
    {
        if (value.IsEmpty)
            throw new ArgumentException("Value cannot be empty.", paramName);
    }
}

#endif

