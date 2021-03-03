// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifSignedByte : ExifValue<sbyte>
    {
        public ExifSignedByte(ExifTagValue tag)
            : base(tag, default)
        {
        }

        public override ExifDataType DataType => ExifDataType.SignedByte;

        protected override string StringValue
            => Value.ToString("X2", CultureInfo.InvariantCulture);

        public override bool SetValue(object value)
        {
            if (base.SetValue(value))
                return true;

            switch (value)
            {
                case int intValue:
                    Value = (sbyte)intValue;
                    return true;
                default:
                    return false;
            }
        }
    }
}
