// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    internal static class ExifDataTypes
    {
        public static uint GetSize(ExifDataType dataType)
        {
            switch (dataType)
            {
                case ExifDataType.String:
                case ExifDataType.Byte:
                case ExifDataType.SignedByte:
                case ExifDataType.Undefined:
                    return 1;
                case ExifDataType.Short:
                case ExifDataType.SignedShort:
                    return 2;
                case ExifDataType.Long:
                case ExifDataType.SignedLong:
                case ExifDataType.Float:
                    return 4;
                case ExifDataType.Double:
                case ExifDataType.Rational:
                case ExifDataType.SignedRational:
                    return 8;
                default:
                    throw new NotSupportedException(dataType.ToString());
            }
        }
    }
}
