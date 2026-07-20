// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#nullable enable

#if NETSTANDARD2_0
using System;
using System.Buffers;

namespace ImageMagick;

internal static class ReadOnlySequencePolyfill
{
    extension<T>(ReadOnlySequence<T> sequence)
    {
        public ReadOnlySpan<T> FirstSpan => sequence.First.Span;
    }
}
#endif
