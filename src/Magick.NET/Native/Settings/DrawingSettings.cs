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

internal partial class DrawingSettings
{
    [NativeInterop(QuantumType = true, ManagedToNative = true)]
    private partial class NativeDrawingSettings : NativeInstance
    {
        public static partial NativeDrawingSettings Create();

        public partial IMagickColor<QuantumType>? BorderColor_Get();

        public partial void BorderColor_Set(IMagickColor<QuantumType>? value);

        public partial IMagickColor<QuantumType>? FillColor_Get();

        public partial void FillColor_Set(IMagickColor<QuantumType>? value);

        public partial FillRule FillRule_Get();

        public partial void FillRule_Set(FillRule value);

        public partial string? Font_Get();

        public partial void Font_Set(string? value);

        public partial string? FontFamily_Get();

        public partial void FontFamily_Set(string? value);

        public partial double FontPointsize_Get();

        public partial void FontPointsize_Set(double value);

        public partial FontStyleType FontStyle_Get();

        public partial void FontStyle_Set(FontStyleType value);

        public partial nuint FontWeight_Get();

        public partial void FontWeight_Set(nuint value);

        public partial bool StrokeAntiAlias_Get();

        public partial void StrokeAntiAlias_Set(bool value);

        public partial IMagickColor<QuantumType>? StrokeColor_Get();

        public partial void StrokeColor_Set(IMagickColor<QuantumType>? value);

        public partial double StrokeDashOffset_Get();

        public partial void StrokeDashOffset_Set(double value);

        public partial LineCap StrokeLineCap_Get();

        public partial void StrokeLineCap_Set(LineCap value);

        public partial LineJoin StrokeLineJoin_Get();

        public partial void StrokeLineJoin_Set(LineJoin value);

        public partial nuint StrokeMiterLimit_Get();

        public partial void StrokeMiterLimit_Set(nuint value);

        public partial double StrokeWidth_Get();

        public partial void StrokeWidth_Set(double value);

        public partial bool TextAntiAlias_Get();

        public partial void TextAntiAlias_Set(bool value);

        public partial TextDirection TextDirection_Get();

        public partial void TextDirection_Set(TextDirection value);

        public partial string? TextEncoding_Get();

        public partial void TextEncoding_Set(string? value);

        public partial Gravity TextGravity_Get();

        public partial void TextGravity_Set(Gravity value);

        public partial double TextInterlineSpacing_Get();

        public partial void TextInterlineSpacing_Set(double value);

        public partial double TextInterwordSpacing_Get();

        public partial void TextInterwordSpacing_Set(double value);

        public partial double TextKerning_Get();

        public partial void TextKerning_Set(double value);

        public partial IMagickColor<QuantumType>? TextUnderColor_Get();

        public partial void TextUnderColor_Set(IMagickColor<QuantumType>? value);

        [ReturnsVoid]
        public partial void SetAffine(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY);

        [Throws]
        [ReturnsVoid]
        public partial void SetFillPattern(IMagickImage value);

        [ReturnsVoid]
        public partial void SetStrokeDashArray(double[] dash, nuint length);

        [Throws]
        [ReturnsVoid]
        public partial void SetStrokePattern(IMagickImage value);

        [ReturnsVoid]
        public partial void SetText(string value);
    }
}
