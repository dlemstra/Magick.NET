// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal struct NativeChannels
{
    private readonly UIntPtr _value;

    private NativeChannels(Channels value)
        => _value = ConvertValue(value);

    public static explicit operator NativeChannels(Channels channels)
        => new NativeChannels(channels);

    public static implicit operator UIntPtr(NativeChannels channels)
        => channels._value;

    private static UIntPtr ConvertValue(Channels value)
    {
        if (Runtime.Is64Bit)
            return (UIntPtr)value;

        if (value == Channels.All)
            return (UIntPtr)0b0111111111111111111111111111;

        if ((ulong)value > 0b1111111111111111111111111111)
            throw new ArgumentException("There is no support for setting more than 32 bits of the Channels on a 32-bit platform", nameof(value));

        return (UIntPtr)value;
    }
}
