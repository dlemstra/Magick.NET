// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;

namespace ImageMagick;

/// <content/>
public partial class ImageProfile : IImageProfile
{
    /// <summary>
    /// Converts this instance to a readonly span.
    /// </summary>
    /// <returns>A readonly byte span.</returns>
    public ReadOnlySpan<byte> ToReadOnlySpan()
    {
        if (_data is null)
            return default;

        return _data.AsSpan();
    }
}

#endif
