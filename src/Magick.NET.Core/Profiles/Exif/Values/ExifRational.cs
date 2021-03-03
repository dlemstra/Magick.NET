// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifRational : ExifValue<Rational>
    {
        public ExifRational(ExifTag<Rational> tag)
            : base(tag, default)
        {
        }

        public ExifRational(ExifTagValue tag)
            : base(tag, default)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.Rational;

        protected override string StringValue
            => Value.ToString(CultureInfo.InvariantCulture);
    }
}
