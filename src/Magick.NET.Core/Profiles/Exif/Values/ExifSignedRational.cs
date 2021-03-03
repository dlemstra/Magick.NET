// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifSignedRational : ExifValue<SignedRational>
    {
        internal ExifSignedRational(ExifTag<SignedRational> tag)
            : base(tag, default)
        {
        }

        internal ExifSignedRational(ExifTagValue tag)
            : base(tag, default)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.SignedRational;

        protected override string StringValue
            => Value.ToString(CultureInfo.InvariantCulture);
    }
}
