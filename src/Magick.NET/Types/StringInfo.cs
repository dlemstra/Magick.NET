// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal partial class StringInfo
{
    public StringInfo(byte[] datum)
    {
        Datum = datum;
    }

    public byte[] Datum { get; }

    public static StringInfo? CreateInstance(IntPtr instance)
    {
        if (instance == IntPtr.Zero)
            return null;

        var native = new NativeStringInfo(instance);

        var datum = ByteConverter.ToArray(native.Datum_Get(), (int)native.Length_Get());
        if (datum is null)
            return null;

        return new StringInfo(datum);
    }
}
