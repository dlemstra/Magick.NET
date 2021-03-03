// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifFloat : ExifValue<float>
    {
        public ExifFloat(ExifTagValue tag)
            : base(tag, default)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.Float;

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
                default:
                    return false;
            }
        }
    }
}
