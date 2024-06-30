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

        public partial void SetBackgroundColor(IMagickColor<QuantumType>? value);

        public partial void SetBorderColor(IMagickColor<QuantumType>? value);

        public partial void SetBorderWidth(nuint value);

        public partial void SetFillColor(IMagickColor<QuantumType>? value);

        public partial void SetFont(string? value);

        public partial void SetFontPointsize(double value);

        public partial void SetFrameGeometry(string? value);

        public partial void SetGeometry(string? value);

        public partial void SetGravity(Gravity value);

        public partial void SetShadow(bool value);

        public partial void SetStrokeColor(IMagickColor<QuantumType>? value);

        public partial void SetTextureFileName(string? value);

        public partial void SetTileGeometry(string? value);

        public partial void SetTitle(string? value);
    }
}
