// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifShort : ExifValue<ushort>
    {
        public ExifShort(ExifTag<ushort> tag)
            : base(tag, default)
        {
        }

        public ExifShort(ExifTagValue tag)
            : base(tag, default)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.Short;

        protected override string StringValue
            => Value.ToString(CultureInfo.InvariantCulture);

        public override bool SetValue(object value)
        {
            if (base.SetValue(value))
                return true;

            switch (value)
            {
                case int intValue:
                    Value = (ushort)intValue;
                    return true;
                default:
                    return false;
            }
        }
    }
}
