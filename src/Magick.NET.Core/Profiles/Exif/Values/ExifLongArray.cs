// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal sealed class ExifLongArray : ExifArrayValue<uint>
    {
        public ExifLongArray(ExifTag<uint[]> tag)
            : base(tag)
        {
        }

        public ExifLongArray(ExifTagValue tag)
            : base(tag)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.Long;
    }
}
