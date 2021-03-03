// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal sealed class ExifDoubleArray : ExifArrayValue<double>
    {
        public ExifDoubleArray(ExifTag<double[]> tag)
            : base(tag)
        {
        }

        public ExifDoubleArray(ExifTagValue tag)
            : base(tag)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.Double;
    }
}
