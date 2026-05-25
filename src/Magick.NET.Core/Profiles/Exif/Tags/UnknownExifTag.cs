// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

internal sealed class UnknownExifTag : ExifTag
{
    internal UnknownExifTag(ExifIfds ifd, ExifTagValue value)
        : base(ifd, (ushort)value)
    {
    }
}
