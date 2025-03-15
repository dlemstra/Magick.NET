// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

internal static class HexColor
{
    public static bool TryParse(string value, out List<QuantumType> channels)
    {
        channels = new List<QuantumType>();

        if (value.Length < 13)
            return TryParseQ8(value, channels);

        return TryParseQ16(value, channels);
    }

    private static bool TryParseQ8(string value, List<QuantumType> channels)
    {
        int size;
        if (value.Length == 4 || value.Length == 5)
            size = 1;
        else if (value.Length == 3 || value.Length == 7 || value.Length == 9)
            size = 2;
        else
            return false;

        var quantum = QuantumScalerFactory.Create<QuantumType>();
        for (var i = 1; i < value.Length; i += size)
        {
            if (!TryParseHex(value, i, size, out var channel))
                return false;

            channels.Add(quantum.ScaleFromByte((byte)channel));
        }

        return true;
    }

    private static bool TryParseQ16(string value, List<QuantumType> channels)
    {
        if (value.Length != 13 && value.Length != 17)
            return false;

        for (var i = 1; i < value.Length; i += 4)
        {
            if (!TryParseHex(value, i, 4, out var channel))
                return false;

            channels.Add(Quantum.Convert(channel));
        }

        return true;
    }

    private static bool TryParseHex(string color, int offset, int length, out ushort channel)
    {
        channel = 0;
        ushort k = 1;

        var i = length - 1;
        while (i >= 0)
        {
            var c = color[offset + i];

            if (c >= '0' && c <= '9')
                channel += (ushort)(k * (c - '0'));
            else if (c >= 'a' && c <= 'f')
                channel += (ushort)(k * (c - 'a' + '\n'));
            else if (c >= 'A' && c <= 'F')
                channel += (ushort)(k * (c - 'A' + '\n'));
            else
                return false;

            i--;
            k = (ushort)(k * 16);
        }

        if (length == 1)
            channel += (byte)(channel * 16);

        return true;
    }
}
