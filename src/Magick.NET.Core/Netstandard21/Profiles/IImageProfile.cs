// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

using System;

namespace ImageMagick;

/// <content/>
public partial interface IImageProfile
{
    /// <summary>
    /// Converts this instance to a readonly span.
    /// </summary>
    /// <returns>A readonly byte span.</returns>
    ReadOnlySpan<byte> ToReadOnlySpan();
}

#endif
