// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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

/// <summary>
/// Class that contains setting for the montage operation.
/// </summary>
public sealed partial class MontageSettings : IMontageSettings<QuantumType>
{
    /// <summary>
    /// Gets or sets the color of the background that thumbnails are composed on.
    /// </summary>
    public IMagickColor<QuantumType>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the frame border color.
    /// </summary>
    public IMagickColor<QuantumType>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the pixels between thumbnail and surrounding frame.
    /// </summary>
    public int BorderWidth { get; set; }

    /// <summary>
    /// Gets or sets the fill color.
    /// </summary>
    public IMagickColor<QuantumType>? FillColor { get; set; }

    /// <summary>
    /// Gets or sets the label font.
    /// </summary>
    public string? Font { get; set; }

    /// <summary>
    /// Gets or sets the font point size.
    /// </summary>
    public int FontPointsize { get; set; }

    /// <summary>
    /// Gets or sets the frame geometry (width &amp; height frame thickness).
    /// </summary>
    public IMagickGeometry? FrameGeometry { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail width &amp; height plus border width &amp; height.
    /// </summary>
    public IMagickGeometry? Geometry { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail position (e.g. <see cref="Gravity.Southwest"/>).
    /// </summary>
    public Gravity Gravity { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail label (applied to image prior to montage).
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether drop-shadows on thumbnails are enabled or disabled.
    /// </summary>
    public bool Shadow { get; set; }

    /// <summary>
    /// Gets or sets the outline color.
    /// </summary>
    public IMagickColor<QuantumType>? StrokeColor { get; set; }

    /// <summary>
    /// Gets or sets the background texture image.
    /// </summary>
    public string? TextureFileName { get; set; }

    /// <summary>
    /// Gets or sets the frame geometry (width &amp; height frame thickness).
    /// </summary>
    public IMagickGeometry? TileGeometry { get; set; }

    /// <summary>
    /// Gets or sets the montage title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the transparent color.
    /// </summary>
    public IMagickColor<QuantumType>? TransparentColor { get; set; }

    private static INativeInstance CreateNativeInstance(IMontageSettings<QuantumType> instance)
    {
        var result = new NativeMontageSettings();
        result.SetBackgroundColor(instance.BackgroundColor);
        result.SetBorderColor(instance.BorderColor);
        result.SetBorderWidth((uint)instance.BorderWidth);
        result.SetFillColor(instance.FillColor);
        result.SetFont(instance.Font);
        result.SetFontPointsize(instance.FontPointsize);
        result.SetFrameGeometry(instance.FrameGeometry?.ToString());
        result.SetGeometry(instance.Geometry?.ToString());
        result.SetGravity(instance.Gravity);
        result.SetShadow(instance.Shadow);
        result.SetStrokeColor(instance.StrokeColor);
        result.SetTextureFileName(instance.TextureFileName);
        result.SetTileGeometry(instance.TileGeometry?.ToString());
        result.SetTitle(instance.Title);

        return result;
    }
}
