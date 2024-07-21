// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

internal sealed class ExifSignedShortArray : ExifArrayValue<short>
{
    public ExifSignedShortArray(ExifTag<short[]> tag)
        : base(tag)
    {
    }

    public ExifSignedShortArray(ExifTagValue tag)
        : base(tag)
    {
    }

    public override ExifDataType DataType
        => ExifDataType.SignedShort;
}
