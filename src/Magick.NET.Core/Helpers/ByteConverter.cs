// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal static class ByteConverter
{
    public static int ToUInt(byte[] data, ref int offset)
    {
        if (offset + 4 > data.Length)
            return 0;

        var value = data[offset++] << 24;
        value |= data[offset++] << 16;
        value |= data[offset++] << 8;
        value |= data[offset++];

        var result = (int)(value & 0xffffffff);
        return result < 0 ? 0 : result;
    }

    public static short ToShort(byte[] data, ref int offset)
    {
        if (offset + 2 > data.Length)
            return 0;

        var result = (short)(data[offset++] << 8);
        result = (short)(result | (short)data[offset++]);
        return (short)(result & 0xffff);
    }

    public static byte[] FromShortReversed(short value)
    {
        var bytes = BitConverter.GetBytes(value);
        Array.Reverse(bytes);
        return bytes;
    }

    public static byte[] FromUnsignedIntegerReversed(uint value)
    {
        var bytes = BitConverter.GetBytes(value);
        Array.Reverse(bytes);
        return bytes;
    }
}
