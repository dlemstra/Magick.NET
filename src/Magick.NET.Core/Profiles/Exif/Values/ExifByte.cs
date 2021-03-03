// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick
{
    internal sealed class ExifByte : ExifValue<byte>
    {
        public ExifByte(ExifTag<byte> tag, ExifDataType dataType)
            : base(tag, default) => DataType = dataType;

        public ExifByte(ExifTagValue tag, ExifDataType dataType)
            : base(tag, default) => DataType = dataType;

        public override ExifDataType DataType { get; }

        protected override string StringValue
            => Value.ToString("X2", CultureInfo.InvariantCulture);

        public override bool SetValue(object value)
        {
            if (base.SetValue(value))
                return true;

            switch (value)
            {
                case int intValue:
                    Value = (byte)intValue;
                    return true;
                default:
                    return false;
            }
        }
    }
}
