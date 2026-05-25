// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

namespace ImageMagick;

internal sealed class ExifSignedShort : ExifValue<short>
{
    public ExifSignedShort(ExifIfds ifd, ExifTagValue tag)
        : base(ifd, tag, default)
    {
    }

    public override ExifDataType DataType
        => ExifDataType.SignedShort;

    protected override string StringValue
        => Value.ToString(CultureInfo.InvariantCulture);

    public override bool SetValue(object value)
    {
        if (base.SetValue(value))
            return true;

        switch (value)
        {
            case int intValue:
                Value = (short)intValue;
                return true;
            default:
                return false;
        }
    }
}
