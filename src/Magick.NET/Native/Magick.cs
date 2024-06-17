// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class MagickNET
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void LogDelegate(UIntPtr type, IntPtr value);

    [NativeInterop]
    private partial class NativeMagickNET
    {
        public static partial string Delegates_Get();

        public static partial string Features_Get();

        public static partial string ImageMagickVersion_Get();

        [Throws]
        [Cleanup(Name = nameof(DisposeFonts))]

        public static partial IntPtr GetFonts(out nuint length);

        public static partial string? GetFontFamily(IntPtr instance, nuint index);

        public static partial string? GetFontName(IntPtr instance, nuint index);

        public static partial void DisposeFonts(IntPtr instance);

        [Throws]
        public static partial void SetDefaultFontFile(string fileName);

        public static partial void SetLogDelegate(LogDelegate? method);

        public static partial void SetLogEvents(string events);

        public static partial void SetRandomSeed(ulong value);
    }
}
