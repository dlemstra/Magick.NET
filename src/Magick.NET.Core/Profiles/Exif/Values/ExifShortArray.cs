// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

internal sealed class ExifShortArray : ExifArrayValue<ushort>
{
    public ExifShortArray(ExifTag<ushort[]> tag)
        : base(tag)
    {
    }

    public ExifShortArray(ExifIfds ifd, ExifTagValue tag)
        : base(ifd, tag)
    {
    }

    public override ExifDataType DataType
        => ExifDataType.Short;
}
