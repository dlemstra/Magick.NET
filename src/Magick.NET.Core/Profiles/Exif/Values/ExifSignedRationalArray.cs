// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal sealed class ExifSignedRationalArray : ExifArrayValue<SignedRational>
    {
        public ExifSignedRationalArray(ExifTag<SignedRational[]> tag)
            : base(tag)
        {
        }

        public ExifSignedRationalArray(ExifTagValue tag)
            : base(tag)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.SignedRational;
    }
}
