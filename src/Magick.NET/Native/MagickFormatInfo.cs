// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class MagickFormatInfo
{
    private static MagickFormatInfo? CreateInstance(IntPtr instance)
    {
        if (instance == IntPtr.Zero)
            return null;

        var nativeInstance = new NativeMagickFormatInfo(instance);
        return new MagickFormatInfo(nativeInstance);
    }

    [NativeInterop]
    private partial class NativeMagickFormatInfo : ConstNativeInstance
    {
        [Throws]
        [Cleanup(Name = nameof(DisposeList), Arguments = "length")]
        public static partial IntPtr CreateList(out nuint length);

        public static partial void DisposeList(IntPtr instance, nuint length);

        [Throws]
        public static partial MagickFormatInfo? GetInfo(IntPtr list, nuint index);

        [Throws]
        public static partial MagickFormatInfo? GetInfoByName(string name);

        [Throws]
        public static partial MagickFormatInfo? GetInfoWithBlob(byte[] data, nuint length);

#if NETSTANDARD2_1
        [Throws]
        public static partial MagickFormatInfo? GetInfoWithBlob(ReadOnlySpan<byte> data, nuint length);
#endif

        public static partial bool Unregister(string name);

        public partial string Description_Get();

        public partial bool CanReadMultithreaded_Get();

        public partial bool CanWriteMultithreaded_Get();

        public partial string Format_Get();

        public partial bool SupportsMultipleFrames_Get();

        public partial bool SupportsReading_Get();

        public partial bool SupportsWriting_Get();

        public partial string? MimeType_Get();

        public partial string Module_Get();
    }
}
