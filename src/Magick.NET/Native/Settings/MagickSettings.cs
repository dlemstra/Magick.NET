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
public partial class MagickSettings
{
    [NativeInterop(QuantumType = true, ManagedToNative = true)]
    private unsafe sealed partial class NativeMagickSettings : NativeInstance
    {
        public static partial NativeMagickSettings Create();

        public partial bool AntiAlias_Get();

        public partial void AntiAlias_Set(bool value);

        public partial IMagickColor<QuantumType>? BackgroundColor_Get();

        public partial void BackgroundColor_Set(IMagickColor<QuantumType>? value);

        public partial ColorSpace ColorSpace_Get();

        public partial void ColorSpace_Set(ColorSpace value);

        public partial ColorType ColorType_Get();

        public partial void ColorType_Set(ColorType value);

        public partial CompressionMethod Compression_Get();

        public partial void Compression_Set(CompressionMethod value);

        public partial bool Debug_Get();

        public partial void Debug_Set(bool value);

        public partial string? Density_Get();

        public partial void Density_Set(string? value);

        public partial nuint Depth_Get();

        public partial void Depth_Set(nuint value);

        public partial Endian Endian_Get();

        public partial void Endian_Set(Endian value);

        public partial string? Extract_Get();

        public partial void Extract_Set(string? set);

        public partial string? Format_Get();

        public partial void Format_Set(string? value);

        public partial double FontPointsize_Get();

        public partial void FontPointsize_Set(double value);

        public partial bool Monochrome_Get();

        public partial void Monochrome_Set(bool value);

        public partial Interlace Interlace_Get();

        public partial void Interlace_Set(Interlace value);

        public partial bool Verbose_Get();

        public partial void Verbose_Set(bool value);

        [Instance(SetsInstance = false)]
        public partial void SetColorFuzz(double value);

        [Instance(SetsInstance = false)]
        public partial void SetFileName(string? value);

        [Instance(SetsInstance = false)]
        public partial string? SetFont(string? value);

        [Instance(SetsInstance = false)]
        public partial void SetNumberScenes(nuint value);

        [Instance(SetsInstance = false)]
        public partial void SetOption(string key, string? value);

        [Instance(SetsInstance = false)]
        public partial void SetPage(string? value);

        [Instance(SetsInstance = false)]
        public partial void SetPing(bool value);

        [Instance(SetsInstance = false)]
        public partial void SetQuality(nuint value);

        [Instance(SetsInstance = false)]
        public partial void SetScene(nuint value);

        [Instance(SetsInstance = false)]
        public partial void SetScenes(string? value);

        [Instance(SetsInstance = false)]
        public partial void SetSize(string? value);
    }
}
