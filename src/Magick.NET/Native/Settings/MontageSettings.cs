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
        public static partial NativeMontageSettings Create();

        [ReturnsVoid]
        public partial void SetBackgroundColor(IMagickColor<QuantumType>? value);

        [ReturnsVoid]
        public partial void SetBorderColor(IMagickColor<QuantumType>? value);

        [ReturnsVoid]
        public partial void SetBorderWidth(nuint value);

        [ReturnsVoid]
        public partial void SetFillColor(IMagickColor<QuantumType>? value);

        [ReturnsVoid]
        public partial void SetFont(string? value);

        [ReturnsVoid]
        public partial void SetFontPointsize(double value);

        [ReturnsVoid]
        public partial void SetFrameGeometry(string? value);

        [ReturnsVoid]
        public partial void SetGeometry(string? value);

        [ReturnsVoid]
        public partial void SetGravity(Gravity value);

        [ReturnsVoid]
        public partial void SetShadow(bool value);

        [ReturnsVoid]
        public partial void SetStrokeColor(IMagickColor<QuantumType>? value);

        [ReturnsVoid]
        public partial void SetTextureFileName(string? value);

        [ReturnsVoid]
        public partial void SetTileGeometry(string? value);

        [ReturnsVoid]
        public partial void SetTitle(string? value);
    }
}
