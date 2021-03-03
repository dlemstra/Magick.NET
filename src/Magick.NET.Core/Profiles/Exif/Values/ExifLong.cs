// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifLong : ExifValue<uint>
    {
        public ExifLong(ExifTag<uint> tag)
            : base(tag, default)
        {
        }

        public ExifLong(ExifTagValue tag)
            : base(tag, default)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.Long;

        protected override string StringValue
            => Value.ToString(CultureInfo.InvariantCulture);

        public override bool SetValue(object value)
        {
            if (base.SetValue(value))
                return true;

            switch (value)
            {
                case int intValue:
                    Value = (uint)intValue;
                    return true;
                default:
                    return false;
            }
        }
    }
}
