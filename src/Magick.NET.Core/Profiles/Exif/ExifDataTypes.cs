// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal static class ExifDataTypes
{
    public static uint GetSize(ExifDataType dataType)
        => dataType switch
        {
            ExifDataType.String or
            ExifDataType.Byte or
            ExifDataType.SignedByte or
            ExifDataType.Undefined => 1,

            ExifDataType.Short or
            ExifDataType.SignedShort => 2,

            ExifDataType.Long or
            ExifDataType.SignedLong or
            ExifDataType.Float => 4,

            ExifDataType.Double or
            ExifDataType.Rational or
            ExifDataType.SignedRational => 8,

            _ => throw new NotSupportedException(dataType.ToString()),
        };
}
