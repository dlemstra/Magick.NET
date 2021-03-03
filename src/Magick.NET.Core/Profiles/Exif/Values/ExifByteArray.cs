// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal sealed class ExifByteArray : ExifArrayValue<byte>
    {
        public ExifByteArray(ExifTag<byte[]> tag, ExifDataType dataType)
            : base(tag) => DataType = dataType;

        public ExifByteArray(ExifTagValue tag, ExifDataType dataType)
            : base(tag) => DataType = dataType;

        public override ExifDataType DataType { get; }
    }
}
