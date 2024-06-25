// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Used to obtain font metrics for text string given current font, pointsize, and density settings.
/// </summary>
public sealed partial class TypeMetric : ITypeMetric
{
    private TypeMetric(NativeTypeMetric instance)
    {
        Ascent = instance.Ascent_Get();
        Descent = instance.Descent_Get();
        MaxHorizontalAdvance = instance.MaxHorizontalAdvance_Get();
        TextHeight = instance.TextHeight_Get();
        TextWidth = instance.TextWidth_Get();
        UnderlinePosition = instance.UnderlinePosition_Get();
        UnderlineThickness = instance.UnderlineThickness_Get();
    }

    /// <summary>
    /// Gets the ascent, the distance in pixels from the text baseline to the highest/upper grid coordinate
    /// used to place an outline point.
    /// </summary>
    public double Ascent { get; }

    /// <summary>
    /// Gets the descent, the distance in pixels from the baseline to the lowest grid coordinate used to
    /// place an outline point. Always a negative value.
    /// </summary>
    public double Descent { get; }

    /// <summary>
    /// Gets the maximum horizontal advance in pixels.
    /// </summary>
    public double MaxHorizontalAdvance { get; }

    /// <summary>
    /// Gets the text height in pixels.
    /// </summary>
    public double TextHeight { get; }

    /// <summary>
    /// Gets the text width in pixels.
    /// </summary>
    public double TextWidth { get; }

    /// <summary>
    /// Gets the underline position.
    /// </summary>
    public double UnderlinePosition { get; }

    /// <summary>
    /// Gets the underline thickness.
    /// </summary>
    public double UnderlineThickness { get; }

    internal static void Dispose(IntPtr instance)
        => NativeTypeMetric.DisposeInstance(instance);
}
