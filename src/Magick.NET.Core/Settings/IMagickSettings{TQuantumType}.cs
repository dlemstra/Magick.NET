// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Text;
using ImageMagick.Drawing;

namespace ImageMagick;

/// <summary>
/// Class that contains various settings.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IMagickSettings<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets or sets the affine to use when annotating with text or drawing.
    /// </summary>
    IDrawableAffine? Affine { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether anti-aliasing should be enabled (default true).
    /// </summary>
    bool AntiAlias { get; set; }

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    IMagickColor<TQuantumType>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the border color.
    /// </summary>
    IMagickColor<TQuantumType>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the color space.
    /// </summary>
    ColorSpace ColorSpace { get; set; }

    /// <summary>
    /// Gets or sets the color type of the image.
    /// </summary>
    ColorType ColorType { get; set; }

    /// <summary>
    /// Gets or sets the compression method to use.
    /// </summary>
    CompressionMethod Compression { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether printing of debug messages from ImageMagick is enabled when a debugger is attached.
    /// </summary>
    bool Debug { get; set; }

    /// <summary>
    /// Gets or sets the vertical and horizontal resolution in pixels.
    /// </summary>
    Density? Density { get; set; }

    /// <summary>
    /// Gets or sets the depth (bits allocated to red/green/blue components).
    /// </summary>
    int Depth { get; set; }

    /// <summary>
    /// Gets or sets the endianness (little like Intel or big like SPARC) for image formats which support
    /// endian-specific options.
    /// </summary>
    Endian Endian { get; set; }

    /// <summary>
    /// Gets or sets the fill color.
    /// </summary>
    IMagickColor<TQuantumType>? FillColor { get; set; }

    /// <summary>
    /// Gets or sets the fill pattern.
    /// </summary>
    IMagickImage<TQuantumType>? FillPattern { get; set; }

    /// <summary>
    /// Gets or sets the rule to use when filling drawn objects.
    /// </summary>
    FillRule FillRule { get; set; }

    /// <summary>
    /// Gets or sets the text rendering font.
    /// </summary>
    string? Font { get; set; }

    /// <summary>
    /// Gets or sets the text font family.
    /// </summary>
    string? FontFamily { get; set; }

    /// <summary>
    /// Gets or sets the font point size.
    /// </summary>
    double FontPointsize { get; set; }

    /// <summary>
    /// Gets or sets the font style.
    /// </summary>
    FontStyleType FontStyle { get; set; }

    /// <summary>
    /// Gets or sets the font weight.
    /// </summary>
    FontWeight FontWeight { get; set; }

    /// <summary>
    /// Gets or sets the the format of the image.
    /// </summary>
    MagickFormat Format { get; set; }

    /// <summary>
    /// Gets or sets the interlace method.
    /// </summary>
    Interlace Interlace { get; set; }

    /// <summary>
    /// Gets or sets the preferred size and location of an image canvas.
    /// </summary>
    IMagickGeometry? Page { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether stroke anti-aliasing is enabled or disabled.
    /// </summary>
    bool StrokeAntiAlias { get; set; }

    /// <summary>
    /// Gets or sets the color to use when drawing object outlines.
    /// </summary>
    IMagickColor<TQuantumType>? StrokeColor { get; set; }

    /// <summary>
    /// Gets or sets the pattern of dashes and gaps used to stroke paths. This represents a
    /// zero-terminated array of numbers that specify the lengths of alternating dashes and gaps
    /// in pixels. If a zero value is not found it will be added. If an odd number of values is
    /// provided, then the list of values is repeated to yield an even number of values.
    /// </summary>
    IEnumerable<double>? StrokeDashArray { get; set; }

    /// <summary>
    /// Gets or sets the distance into the dash pattern to start the dash (default 0) while
    /// drawing using a dash pattern,.
    /// </summary>
    double StrokeDashOffset { get; set; }

    /// <summary>
    /// Gets or sets the shape to be used at the end of open subpaths when they are stroked.
    /// </summary>
    LineCap StrokeLineCap { get; set; }

    /// <summary>
    /// Gets or sets the shape to be used at the corners of paths (or other vector shapes) when they
    /// are stroked.
    /// </summary>
    LineJoin StrokeLineJoin { get; set; }

    /// <summary>
    /// Gets or sets the miter limit. When two line segments meet at a sharp angle and miter joins have
    /// been specified for 'lineJoin', it is possible for the miter to extend far beyond the thickness
    /// of the line stroking the path. The miterLimit' imposes a limit on the ratio of the miter
    /// length to the 'lineWidth'. The default value is 4.
    /// </summary>
    int StrokeMiterLimit { get; set; }

    /// <summary>
    /// Gets or sets the pattern image to use while stroking object outlines.
    /// </summary>
    IMagickImage<TQuantumType>? StrokePattern { get; set; }

    /// <summary>
    /// Gets or sets the stroke width for drawing lines, circles, ellipses, etc.
    /// </summary>
    double StrokeWidth { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Postscript and TrueType fonts should be anti-aliased (default true).
    /// </summary>
    bool TextAntiAlias { get; set; }

    /// <summary>
    /// Gets or sets text direction (right-to-left or left-to-right).
    /// </summary>
    TextDirection TextDirection { get; set; }

    /// <summary>
    /// Gets or sets the text annotation encoding (e.g. "UTF-16").
    /// </summary>
    Encoding? TextEncoding { get; set; }

    /// <summary>
    /// Gets or sets the text annotation gravity.
    /// </summary>
    Gravity TextGravity { get; set; }

    /// <summary>
    /// Gets or sets the text inter-line spacing.
    /// </summary>
    double TextInterlineSpacing { get; set; }

    /// <summary>
    /// Gets or sets the text inter-word spacing.
    /// </summary>
    double TextInterwordSpacing { get; set; }

    /// <summary>
    /// Gets or sets the text inter-character kerning.
    /// </summary>
    double TextKerning { get; set; }

    /// <summary>
    /// Gets or sets the text undercolor box.
    /// </summary>
    IMagickColor<TQuantumType>? TextUnderColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether verbose output os turned on or off.
    /// </summary>
    bool Verbose { get; set; }

    /// <summary>
    /// Returns the value of a format-specific option.
    /// </summary>
    /// <param name="format">The format to get the option for.</param>
    /// <param name="name">The name of the option.</param>
    /// <returns>The value of a format-specific option.</returns>
    string? GetDefine(MagickFormat format, string name);

    /// <summary>
    /// Returns the value of a format-specific option.
    /// </summary>
    /// <param name="name">The name of the option.</param>
    /// <returns>The value of a format-specific option.</returns>
    string? GetDefine(string name);

    /// <summary>
    /// Removes the define with the specified name.
    /// </summary>
    /// <param name="format">The format to set the define for.</param>
    /// <param name="name">The name of the define.</param>
    void RemoveDefine(MagickFormat format, string name);

    /// <summary>
    /// Removes the define with the specified name.
    /// </summary>
    /// <param name="name">The name of the define.</param>
    void RemoveDefine(string name);

    /// <summary>
    /// Sets a format-specific option.
    /// </summary>
    /// <param name="format">The format to set the define for.</param>
    /// <param name="name">The name of the define.</param>
    /// <param name="flag">The value of the define.</param>
    void SetDefine(MagickFormat format, string name, bool flag);

    /// <summary>
    /// Sets a format-specific option.
    /// </summary>
    /// <param name="format">The format to set the option for.</param>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value of the option.</param>
    void SetDefine(MagickFormat format, string name, int value);

    /// <summary>
    /// Sets a format-specific option.
    /// </summary>
    /// <param name="format">The format to set the option for.</param>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value of the option.</param>
    void SetDefine(MagickFormat format, string name, string value);

    /// <summary>
    /// Sets a format-specific option.
    /// </summary>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value of the option.</param>
    void SetDefine(string name, string value);

    /// <summary>
    /// Sets format-specific options with the specified defines.
    /// </summary>
    /// <param name="defines">The defines to set.</param>
    void SetDefines(IDefines defines);
}
