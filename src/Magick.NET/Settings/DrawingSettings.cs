// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Text;

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

internal sealed partial class DrawingSettings
{
    private double[]? _strokeDashArray;

    internal DrawingSettings()
    {
        using var instance = new NativeDrawingSettings();
        BorderColor = instance.BorderColor_Get();
        FillColor = instance.FillColor_Get();
        FillRule = instance.FillRule_Get();
        Font = instance.Font_Get();
        FontFamily = instance.FontFamily_Get();
        FontPointsize = instance.FontPointsize_Get();
        FontStyle = instance.FontStyle_Get();
        FontWeight = (FontWeight)instance.FontWeight_Get();
        StrokeAntiAlias = instance.StrokeAntiAlias_Get();
        StrokeColor = instance.StrokeColor_Get();
        StrokeDashOffset = instance.StrokeDashOffset_Get();
        StrokeLineCap = instance.StrokeLineCap_Get();
        StrokeLineJoin = instance.StrokeLineJoin_Get();
        StrokeMiterLimit = (uint)instance.StrokeMiterLimit_Get();
        StrokeWidth = instance.StrokeWidth_Get();
        TextAntiAlias = instance.TextAntiAlias_Get();
        TextDirection = instance.TextDirection_Get();
        TextEncoding = GetTextEncoding(instance);
        TextGravity = instance.TextGravity_Get();
        TextInterlineSpacing = instance.TextInterlineSpacing_Get();
        TextInterwordSpacing = instance.TextInterwordSpacing_Get();
        TextKerning = instance.TextKerning_Get();
        TextUnderColor = instance.TextUnderColor_Get();
    }

    public IDrawableAffine? Affine { get; set; }

    public IMagickColor<QuantumType>? BorderColor { get; set; }

    public IMagickColor<QuantumType>? FillColor { get; set; }

    public IMagickImage<QuantumType>? FillPattern { get; set; }

    public FillRule FillRule { get; set; }

    public string? Font { get; set; }

    public string? FontFamily { get; set; }

    public double FontPointsize { get; set; }

    public FontStyleType FontStyle { get; set; }

    public FontWeight FontWeight { get; set; }

    public bool StrokeAntiAlias { get; set; }

    public IMagickColor<QuantumType>? StrokeColor { get; set; }

    public IEnumerable<double>? StrokeDashArray
    {
        get => _strokeDashArray;
        set
        {
            if (value is not null)
                _strokeDashArray = new List<double>(value).ToArray();
        }
    }

    public double StrokeDashOffset { get; set; }

    public LineCap StrokeLineCap { get; set; }

    public LineJoin StrokeLineJoin { get; set; }

    public uint StrokeMiterLimit { get; set; }

    public IMagickImage<QuantumType>? StrokePattern { get; set; }

    public double StrokeWidth { get; set; }

    public string? Text { get; set; }

    public bool TextAntiAlias { get; set; }

    public TextDirection TextDirection { get; set; }

    public Encoding? TextEncoding { get; set; }

    public Gravity TextGravity { get; set; }

    public double TextInterlineSpacing { get; set; }

    public double TextInterwordSpacing { get; set; }

    public double TextKerning { get; set; }

    public IMagickColor<QuantumType>? TextUnderColor { get; set; }

    internal DrawingSettings Clone()
    {
        return new DrawingSettings
        {
            BorderColor = MagickColor.Clone(BorderColor),
            FillColor = MagickColor.Clone(FillColor),
            FillRule = FillRule,
            Font = Font,
            FontFamily = FontFamily,
            FontPointsize = FontPointsize,
            FontStyle = FontStyle,
            FontWeight = FontWeight,
            StrokeAntiAlias = StrokeAntiAlias,
            StrokeColor = MagickColor.Clone(StrokeColor),
            StrokeDashOffset = StrokeDashOffset,
            StrokeLineCap = StrokeLineCap,
            StrokeLineJoin = StrokeLineJoin,
            StrokeMiterLimit = StrokeMiterLimit,
            StrokeWidth = StrokeWidth,
            TextAntiAlias = TextAntiAlias,
            TextDirection = TextDirection,
            TextEncoding = TextEncoding,
            TextGravity = TextGravity,
            TextInterlineSpacing = TextInterlineSpacing,
            TextInterwordSpacing = TextInterwordSpacing,
            TextKerning = TextKerning,
            TextUnderColor = MagickColor.Clone(TextUnderColor),

            Affine = Affine,
            FillPattern = MagickImage.Clone(FillPattern),
            _strokeDashArray = _strokeDashArray is not null ? (double[])_strokeDashArray.Clone() : null,
            StrokePattern = MagickImage.Clone(StrokePattern),
            Text = Text,
        };
    }

    private static Encoding? GetTextEncoding(NativeDrawingSettings instance)
    {
        var name = instance.TextEncoding_Get();
        if (name is null || name.Length == 0)
            return null;

        try
        {
            return Encoding.GetEncoding(name);
        }
        catch (ArgumentException)
        {
            return null;
        }
    }

    private NativeDrawingSettings CreateNativeInstance()
    {
        var instance = new NativeDrawingSettings();
        instance.BorderColor_Set(BorderColor);
        instance.FillColor_Set(FillColor);
        instance.FillRule_Set(FillRule);
        instance.Font_Set(Font);
        instance.FontFamily_Set(FontFamily);
        instance.FontPointsize_Set(FontPointsize);
        instance.FontStyle_Set(FontStyle);
        instance.FontWeight_Set((uint)FontWeight);
        instance.StrokeAntiAlias_Set(StrokeAntiAlias);
        instance.StrokeColor_Set(StrokeColor);
        instance.StrokeDashOffset_Set(StrokeDashOffset);
        instance.StrokeLineCap_Set(StrokeLineCap);
        instance.StrokeLineJoin_Set(StrokeLineJoin);
        instance.StrokeMiterLimit_Set(StrokeMiterLimit);
        instance.StrokeWidth_Set(StrokeWidth);
        instance.TextAntiAlias_Set(TextAntiAlias);
        instance.TextDirection_Set(TextDirection);
        if (TextEncoding is not null)
            instance.TextEncoding_Set(TextEncoding.WebName);
        instance.TextGravity_Set(TextGravity);
        instance.TextInterlineSpacing_Set(TextInterlineSpacing);
        instance.TextInterwordSpacing_Set(TextInterwordSpacing);
        instance.TextKerning_Set(TextKerning);
        instance.TextUnderColor_Set(TextUnderColor);

        if (Affine is not null)
            instance.SetAffine(Affine.ScaleX, Affine.ScaleY, Affine.ShearX, Affine.ShearY, Affine.TranslateX, Affine.TranslateY);
        if (FillPattern is not null)
            instance.SetFillPattern(FillPattern);
        if (_strokeDashArray is not null)
            instance.SetStrokeDashArray(_strokeDashArray, (uint)_strokeDashArray.Length);
        if (StrokePattern is not null)
            instance.SetStrokePattern(StrokePattern);
        if (Text is not null && Text.Length > 0)
            instance.SetText(Text);

        return instance;
    }
}
