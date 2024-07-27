// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

internal abstract class ExifArrayValue<TValueType> : ExifValue, IExifValue<TValueType[]>
{
    public ExifArrayValue(ExifTag<TValueType[]> tag)
        : base(tag)
    {
        Value = [];
    }

    public ExifArrayValue(ExifTagValue tag)
        : base(tag)
    {
        Value = [];
    }

    public override bool IsArray => true;

    public TValueType[] Value { get; set; }

    public override object GetValue()
        => Value;

    public override bool SetValue(object value)
    {
        if (value is null)
            return false;

        if (value is TValueType[] typeValueArray)
        {
            Value = typeValueArray;
            return true;
        }

        if (value is TValueType typeValue)
        {
            Value = [typeValue];
            return true;
        }

        return false;
    }
}
