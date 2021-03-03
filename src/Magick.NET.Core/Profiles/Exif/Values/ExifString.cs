// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifString : ExifValue<string>
    {
        public ExifString(ExifTag<string> tag)
            : base(tag, string.Empty)
        {
        }

        public ExifString(ExifTagValue tag)
            : base(tag, string.Empty)
        {
        }

        public override ExifDataType DataType
            => ExifDataType.String;

        protected override string StringValue => Value;

        public override bool SetValue(object value)
        {
            if (value is null)
                return false;

            if (base.SetValue(value))
                return true;

            switch (value)
            {
                case int intValue:
                    Value = intValue.ToString(CultureInfo.InvariantCulture);
                    return true;
                default:
                    return false;
            }
        }
    }
}