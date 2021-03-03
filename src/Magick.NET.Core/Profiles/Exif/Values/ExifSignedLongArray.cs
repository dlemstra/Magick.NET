// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal sealed class ExifSignedLongArray : ExifArrayValue<int>
    {
        public ExifSignedLongArray(ExifTagValue tag)
            : base(tag)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.SignedLong;
    }
}
