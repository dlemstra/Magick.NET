// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal sealed class ExifNumberArray : ExifArrayValue<Number>
    {
        public ExifNumberArray(ExifTag<Number[]> tag)
            : base(tag)
        {
        }

        public override ExifDataType DataType
        {
            get
            {
                if (Value is null)
                    return ExifDataType.Short;

                for (var i = 0; i < Value.Length; i++)
                {
                    if (Value[i] > (Number)ushort.MaxValue)
                        return ExifDataType.Long;
                }

                return ExifDataType.Short;
            }
        }
    }
}
