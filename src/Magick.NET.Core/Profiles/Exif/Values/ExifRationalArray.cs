// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal sealed class ExifRationalArray : ExifArrayValue<Rational>
    {
        public ExifRationalArray(ExifTag<Rational[]> tag)
            : base(tag)
        {
        }

        public ExifRationalArray(ExifTagValue tag)
            : base(tag)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.Rational;
    }
}
