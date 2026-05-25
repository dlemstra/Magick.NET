// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

internal sealed class ExifFloatArray : ExifArrayValue<float>
{
    public ExifFloatArray(ExifIfds ifd, ExifTagValue tag)
        : base(ifd, tag)
    {
    }

    public override ExifDataType DataType
        => ExifDataType.Float;
}
