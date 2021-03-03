// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifNumber : ExifValue<Number>
    {
        public ExifNumber(ExifTag<Number> tag)
            : base(tag, default)
        {
        }

        public override ExifDataType DataType
        {
            get
            {
                if (Value > ushort.MaxValue)
                    return ExifDataType.Long;

                return ExifDataType.Short;
            }
        }

        protected override string StringValue
            => Value.ToString(CultureInfo.InvariantCulture);

        public override bool SetValue(object value)
        {
            if (base.SetValue(value))
                return true;

            switch (value)
            {
                case int intValue:
                    Value = intValue;
                    return true;
                case uint uintValue:
                    Value = uintValue;
                    return true;
                case short shortValue:
                    Value = shortValue;
                    return true;
                case ushort ushortValue:
                    Value = ushortValue;
                    return true;
                default:
                    return false;
            }
        }
    }
}
