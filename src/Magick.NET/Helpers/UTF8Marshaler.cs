// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ImageMagick;

internal sealed class UTF8Marshaler : INativeInstance
{
    private UTF8Marshaler(string? value)
    {
        if (value is null)
        {
            Instance = IntPtr.Zero;
            return;
        }

        // not null terminated
        var strbuf = Encoding.UTF8.GetBytes(value);
        Instance = Marshal.AllocHGlobal(strbuf.Length + 1);
        Marshal.Copy(strbuf, 0, Instance, strbuf.Length);

        // write the terminating null
        Marshal.WriteByte(Instance + strbuf.Length, 0);
    }

    public IntPtr Instance { get; private set; }

    public void Dispose()
    {
        if (Instance == IntPtr.Zero)
            return;

        Marshal.FreeHGlobal(Instance);
        Instance = IntPtr.Zero;
    }

    internal static INativeInstance CreateInstance(string? value)
        => new UTF8Marshaler(value);

    internal static string CreateInstance(IntPtr nativeData)
    {
        var result = CreateNullableInstance(nativeData);
        if (result is null)
            throw new InvalidOperationException("The string value should never be null.");

        return result;
    }

    internal static unsafe string? CreateNullableInstance(IntPtr nativeData)
    {
        if (nativeData == IntPtr.Zero)
            return null;

        var bytes = (byte*)nativeData;
        var length = 0;
        var walk = bytes;

        // find the end of the string
        while (*(walk++) != 0)
            length++;

        if (length == 0)
            return string.Empty;

        return Encoding.UTF8.GetString(bytes, length);
    }

    internal static string? CreateInstanceAndRelinquish(IntPtr nativeData)
    {
        var result = CreateNullableInstance(nativeData);

        MagickMemory.Relinquish(nativeData);

        return result;
    }
}
