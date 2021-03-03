// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal static class ByteConverter
    {
        public static int ToUInt(byte[] data, ref int offset)
        {
            if (offset + 4 > data.Length)
                return 0;

            int value = data[offset++] << 24;
            value |= data[offset++] << 16;
            value |= data[offset++] << 8;
            value |= data[offset++];

            int result = (int)(value & 0xffffffff);
            return result < 0 ? 0 : result;
        }

        public static short ToShort(byte[] data, ref int offset)
        {
            if (offset + 2 > data.Length)
                return 0;

            short result = (short)(data[offset++] << 8);
            result = (short)(result | (short)data[offset++]);
            return (short)(result & 0xffff);
        }
    }
}
