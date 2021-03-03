// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifSignedLong : ExifValue<int>
    {
        public ExifSignedLong(ExifTagValue tag)
            : base(tag, default)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.SignedLong;

        protected override string StringValue
            => Value.ToString(CultureInfo.InvariantCulture);
    }
}
