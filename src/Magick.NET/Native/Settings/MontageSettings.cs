// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

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

/// <content />
public partial class MontageSettings
{
    [NativeInterop(QuantumType = true, ManagedToNative = true)]
    private partial class NativeMontageSettings : NativeInstance
    {
        public NativeMontageSettings()
            => Instance = Create();

        public static partial IntPtr Create();

        [Instance(SetsInstance = false)]
        public partial void SetBackgroundColor(IMagickColor<QuantumType>? value);

        [Instance(SetsInstance = false)]
        public partial void SetBorderColor(IMagickColor<QuantumType>? value);

        [Instance(SetsInstance = false)]
        public partial void SetBorderWidth(uint value);

        [Instance(SetsInstance = false)]
        public partial void SetFillColor(IMagickColor<QuantumType>? value);

        [Instance(SetsInstance = false)]
        public partial void SetFont(string? value);

        [Instance(SetsInstance = false)]
        public partial void SetFontPointsize(double value);

        [Instance(SetsInstance = false)]
        public partial void SetFrameGeometry(string? value);

        [Instance(SetsInstance = false)]
        public partial void SetGeometry(string? value);

        [Instance(SetsInstance = false)]
        public partial void SetGravity(Gravity value);

        [Instance(SetsInstance = false)]
        public partial void SetShadow(bool value);

        [Instance(SetsInstance = false)]
        public partial void SetStrokeColor(IMagickColor<QuantumType>? value);

        [Instance(SetsInstance = false)]
        public partial void SetTextureFileName(string? value);

        [Instance(SetsInstance = false)]
        public partial void SetTileGeometry(string? value);

        [Instance(SetsInstance = false)]
        public partial void SetTitle(string? value);
    }
}
