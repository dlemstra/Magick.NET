// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Sets the font family, style, weight and stretch to use when annotating with text.
/// </summary>
public sealed class DrawableFont : IDrawableFont, IDrawingWand
{
    private static readonly string[] _fontExtensions = new string[] { ".ttf", ".ttc", ".pfb", ".pfm", ".otf" };

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableFont"/> class.
    /// </summary>
    /// <param name="family">The font family or the full path to the font file.</param>
    public DrawableFont(string family)
      : this(family, FontStyleType.Any, FontWeight.Normal, FontStretch.Normal)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableFont"/> class.
    /// </summary>
    /// <param name="family">The font family or the full path to the font file.</param>
    /// <param name="style">The style of the font.</param>
    /// <param name="weight">The weight of the font.</param>
    /// <param name="stretch">The font stretching type.</param>
    public DrawableFont(string family, FontStyleType style, FontWeight weight, FontStretch stretch)
    {
        Throw.IfNullOrEmpty(nameof(family), family);

        Family = family;
        Style = style;
        Weight = weight;
        Stretch = stretch;
    }

    /// <summary>
    /// Gets or sets the font family or the full path to the font file.
    /// </summary>
    public string Family { get; set; }

    /// <summary>
    /// Gets or sets the style of the font.
    /// </summary>
    public FontStyleType Style { get; set; }

    /// <summary>
    /// Gets or sets the weight of the font.
    /// </summary>
    public FontWeight Weight { get; set; }

    /// <summary>
    /// Gets or sets the font stretching.
    /// </summary>
    public FontStretch Stretch { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
    {
        if (wand is null)
            return;

        foreach (var extension in _fontExtensions)
        {
            if (Family.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
            {
                wand.Font(Family);
                return;
            }
        }

        wand.FontFamily(Family, Style, Weight, Stretch);
    }
}
