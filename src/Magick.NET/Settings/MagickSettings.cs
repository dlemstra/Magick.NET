// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ImageMagick.Drawing;

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
/// Class that contains various settings.
/// </summary>
public partial class MagickSettings : IMagickSettings<QuantumType>
{
    private readonly Dictionary<string, string?> _options = new();

    private double _fontPointsize;

    internal MagickSettings()
    {
        using var instance = NativeMagickSettings.Create();
        AntiAlias = instance.AntiAlias_Get();
        BackgroundColor = instance.BackgroundColor_Get();
        ColorSpace = instance.ColorSpace_Get();
        ColorType = instance.ColorType_Get();
        Compression = instance.Compression_Get();
        Debug = instance.Debug_Get();
        Density = CreateDensity(instance.Density_Get());
        Depth = (uint)instance.Depth_Get();
        Endian = instance.Endian_Get();
        Extract = MagickGeometry.FromString(instance.Extract_Get());
        _fontPointsize = instance.FontPointsize_Get();
        Format = EnumHelper.Parse(instance.Format_Get(), MagickFormat.Unknown);
        Interlace = instance.Interlace_Get();
        Monochrome = instance.Monochrome_Get();
        Verbose = instance.Verbose_Get();
        Drawing = new DrawingSettings();
    }

    internal event EventHandler<ArtifactEventArgs>? Artifact;

    /// <summary>
    /// Gets or sets the affine to use when annotating with text or drawing.
    /// </summary>
    public IDrawableAffine? Affine
    {
        get => Drawing.Affine;
        set => Drawing.Affine = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether anti-aliasing should be enabled (default true).
    /// </summary>
    public bool AntiAlias { get; set; }

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    public IMagickColor<QuantumType>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the border color.
    /// </summary>
    public IMagickColor<QuantumType>? BorderColor
    {
        get => Drawing.BorderColor;
        set => Drawing.BorderColor = value;
    }

    /// <summary>
    /// Gets or sets the color space.
    /// </summary>
    public ColorSpace ColorSpace { get; set; }

    /// <summary>
    /// Gets or sets the color type of the image.
    /// </summary>
    public ColorType ColorType { get; set; }

    /// <summary>
    /// Gets or sets the compression method to use.
    /// </summary>
    public CompressionMethod Compression { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether printing of debug messages from ImageMagick is enabled when a debugger is attached.
    /// </summary>
    public bool Debug { get; set; }

    /// <summary>
    /// Gets or sets the vertical and horizontal resolution in pixels.
    /// </summary>
    public Density? Density { get; set; }

    /// <summary>
    /// Gets or sets the depth (bits allocated to red/green/blue components).
    /// </summary>
    public uint Depth { get; set; }

    /// <summary>
    /// Gets or sets the endianness (little like Intel or big like SPARC) for image formats which support
    /// endian-specific options.
    /// </summary>
    public Endian Endian { get; set; }

    /// <summary>
    /// Gets or sets the fill color.
    /// </summary>
    public IMagickColor<QuantumType>? FillColor
    {
        get => Drawing.FillColor;
        set
        {
            SetOptionAndArtifact("fill", value?.ToString());
            Drawing.FillColor = value;
        }
    }

    /// <summary>
    /// Gets or sets the fill pattern.
    /// </summary>
    public IMagickImage<QuantumType>? FillPattern
    {
        get => Drawing.FillPattern;
        set => Drawing.FillPattern = value;
    }

    /// <summary>
    /// Gets or sets the rule to use when filling drawn objects.
    /// </summary>
    public FillRule FillRule
    {
        get => Drawing.FillRule;
        set => Drawing.FillRule = value;
    }

    /// <summary>
    /// Gets or sets the text rendering font.
    /// </summary>
    public string? Font
    {
        get => Drawing.Font;
        set
        {
            Drawing.Font = value;
        }
    }

    /// <summary>
    /// Gets or sets the text font family.
    /// </summary>
    public string? FontFamily
    {
        get => Drawing.FontFamily;
        set
        {
            SetOptionAndArtifact("family", value);
            Drawing.FontFamily = value;
        }
    }

    /// <summary>
    /// Gets or sets the font point size.
    /// </summary>
    public double FontPointsize
    {
        get => _fontPointsize;
        set
        {
            _fontPointsize = value;
            Drawing.FontPointsize = value;
        }
    }

    /// <summary>
    /// Gets or sets the font style.
    /// </summary>
    public FontStyleType FontStyle
    {
        get => Drawing.FontStyle;
        set
        {
            SetOptionAndArtifact("style", value);
            Drawing.FontStyle = value;
        }
    }

    /// <summary>
    /// Gets or sets the font weight.
    /// </summary>
    public FontWeight FontWeight
    {
        get => Drawing.FontWeight;
        set
        {
            SetOptionAndArtifact("weight", ((int)value).ToString(CultureInfo.InvariantCulture));
            Drawing.FontWeight = value;
        }
    }

    /// <summary>
    /// Gets or sets the the format of the image.
    /// </summary>
    public MagickFormat Format { get; set; }

    /// <summary>
    /// Gets or sets the interlace method.
    /// </summary>
    public Interlace Interlace { get; set; }

    /// <summary>
    /// Gets or sets the preferred size and location of an image canvas.
    /// </summary>
    public IMagickGeometry? Page { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether stroke anti-aliasing is enabled or disabled.
    /// </summary>
    public bool StrokeAntiAlias
    {
        get => Drawing.StrokeAntiAlias;
        set => Drawing.StrokeAntiAlias = value;
    }

    /// <summary>
    /// Gets or sets the color to use when drawing object outlines.
    /// </summary>
    public IMagickColor<QuantumType>? StrokeColor
    {
        get => Drawing.StrokeColor;
        set
        {
            SetOptionAndArtifact("stroke", value?.ToString());
            Drawing.StrokeColor = value;
        }
    }

    /// <summary>
    /// Gets or sets the pattern of dashes and gaps used to stroke paths. This represents a
    /// zero-terminated array of numbers that specify the lengths of alternating dashes and gaps
    /// in pixels. If a zero value is not found it will be added. If an odd number of values is
    /// provided, then the list of values is repeated to yield an even number of values.
    /// </summary>
    public IEnumerable<double>? StrokeDashArray
    {
        get => Drawing.StrokeDashArray;
        set => Drawing.StrokeDashArray = value;
    }

    /// <summary>
    /// Gets or sets the distance into the dash pattern to start the dash (default 0) while
    /// drawing using a dash pattern,.
    /// </summary>
    public double StrokeDashOffset
    {
        get => Drawing.StrokeDashOffset;
        set => Drawing.StrokeDashOffset = value;
    }

    /// <summary>
    /// Gets or sets the shape to be used at the end of open subpaths when they are stroked.
    /// </summary>
    public LineCap StrokeLineCap
    {
        get => Drawing.StrokeLineCap;
        set => Drawing.StrokeLineCap = value;
    }

    /// <summary>
    /// Gets or sets the shape to be used at the corners of paths (or other vector shapes) when they
    /// are stroked.
    /// </summary>
    public LineJoin StrokeLineJoin
    {
        get => Drawing.StrokeLineJoin;
        set => Drawing.StrokeLineJoin = value;
    }

    /// <summary>
    /// Gets or sets the miter limit. When two line segments meet at a sharp angle and miter joins have
    /// been specified for 'lineJoin', it is possible for the miter to extend far beyond the thickness
    /// of the line stroking the path. The miterLimit' imposes a limit on the ratio of the miter
    /// length to the 'lineWidth'. The default value is 4.
    /// </summary>
    public uint StrokeMiterLimit
    {
        get => Drawing.StrokeMiterLimit;
        set => Drawing.StrokeMiterLimit = value;
    }

    /// <summary>
    /// Gets or sets the pattern image to use while stroking object outlines.
    /// </summary>
    public IMagickImage<QuantumType>? StrokePattern
    {
        get => Drawing.StrokePattern;
        set => Drawing.StrokePattern = value;
    }

    /// <summary>
    /// Gets or sets the stroke width for drawing lines, circles, ellipses, etc.
    /// </summary>
    public double StrokeWidth
    {
        get => Drawing.StrokeWidth;
        set
        {
            SetOptionAndArtifact("strokewidth", value);
            Drawing.StrokeWidth = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether Postscript and TrueType fonts should be anti-aliased (default true).
    /// </summary>
    public bool TextAntiAlias
    {
        get => Drawing.TextAntiAlias;
        set => Drawing.TextAntiAlias = value;
    }

    /// <summary>
    /// Gets or sets text direction (right-to-left or left-to-right).
    /// </summary>
    public TextDirection TextDirection
    {
        get => Drawing.TextDirection;
        set => Drawing.TextDirection = value;
    }

    /// <summary>
    /// Gets or sets the text annotation encoding (e.g. "UTF-16").
    /// </summary>
    public Encoding? TextEncoding
    {
        get => Drawing.TextEncoding;
        set => Drawing.TextEncoding = value;
    }

    /// <summary>
    /// Gets or sets the text annotation gravity.
    /// </summary>
    public Gravity TextGravity
    {
        get => Drawing.TextGravity;
        set
        {
            SetOptionAndArtifact("gravity", value);
            Drawing.TextGravity = value;
        }
    }

    /// <summary>
    /// Gets or sets the text inter-line spacing.
    /// </summary>
    public double TextInterlineSpacing
    {
        get => Drawing.TextInterlineSpacing;
        set
        {
            SetOptionAndArtifact("interline-spacing", value);
            Drawing.TextInterlineSpacing = value;
        }
    }

    /// <summary>
    /// Gets or sets the text inter-word spacing.
    /// </summary>
    public double TextInterwordSpacing
    {
        get => Drawing.TextInterwordSpacing;
        set
        {
            SetOptionAndArtifact("interword-spacing", value);
            Drawing.TextInterwordSpacing = value;
        }
    }

    /// <summary>
    /// Gets or sets the text inter-character kerning.
    /// </summary>
    public double TextKerning
    {
        get => Drawing.TextKerning;
        set
        {
            SetOptionAndArtifact("kerning", value);
            Drawing.TextKerning = value;
        }
    }

    /// <summary>
    /// Gets or sets the text undercolor box.
    /// </summary>
    public IMagickColor<QuantumType>? TextUnderColor
    {
        get => Drawing.TextUnderColor;
        set
        {
            SetOptionAndArtifact("undercolor", value?.ToString());
            Drawing.TextUnderColor = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether verbose output os turned on or off.
    /// </summary>
    public bool Verbose { get; set; }

    internal DrawingSettings Drawing { get; private set; }

    internal double ColorFuzz { get; set; }

    internal string? FileName { get; set; }

    internal bool Ping { get; set; }

    internal uint Quality { get; set; }

    /// <summary>
    /// Gets or sets the specified area to extract from the image.
    /// </summary>
    protected IMagickGeometry? Extract { get; set; }

    /// <summary>
    /// Gets or sets the number of scenes.
    /// </summary>
    protected uint NumberScenes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a monochrome reader should be used.
    /// </summary>
    protected bool Monochrome { get; set; }

    /// <summary>
    /// Gets or sets the size of the image.
    /// </summary>
    protected string? Size { get; set; }

    /// <summary>
    /// Gets or sets the active scene.
    /// </summary>
    protected uint Scene { get; set; }

    /// <summary>
    /// Gets or sets scenes of the image.
    /// </summary>
    protected string? Scenes { get; set; }

    /// <summary>
    /// Returns the value of a format-specific option.
    /// </summary>
    /// <param name="format">The format to get the option for.</param>
    /// <param name="name">The name of the option.</param>
    /// <returns>The value of a format-specific option.</returns>
    public string? GetDefine(MagickFormat format, string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        return GetOption(ParseDefine(format, name));
    }

    /// <summary>
    /// Returns the value of a format-specific option.
    /// </summary>
    /// <param name="name">The name of the option.</param>
    /// <returns>The value of a format-specific option.</returns>
    public string? GetDefine(string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        return GetOption(name);
    }

    /// <summary>
    /// Removes the define with the specified name.
    /// </summary>
    /// <param name="format">The format to set the define for.</param>
    /// <param name="name">The name of the define.</param>
    public void RemoveDefine(MagickFormat format, string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        var key = ParseDefine(format, name);
        if (_options.ContainsKey(key))
            _options.Remove(key);
    }

    /// <summary>
    /// Removes the define with the specified name.
    /// </summary>
    /// <param name="name">The name of the define.</param>
    public void RemoveDefine(string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        if (_options.ContainsKey(name))
            _options.Remove(name);
    }

    /// <summary>
    /// Sets a format-specific option.
    /// </summary>
    /// <param name="format">The format to set the define for.</param>
    /// <param name="name">The name of the define.</param>
    /// <param name="flag">The value of the define.</param>
    public void SetDefine(MagickFormat format, string name, bool flag)
        => SetDefine(format, name, flag ? "true" : "false");

    /// <summary>
    /// Sets a format-specific option.
    /// </summary>
    /// <param name="format">The format to set the define for.</param>
    /// <param name="name">The name of the define.</param>
    /// <param name="value">The value of the define.</param>
    public void SetDefine(MagickFormat format, string name, int value)
        => SetDefine(format, name, value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Sets a format-specific option.
    /// </summary>
    /// <param name="format">The format to set the option for.</param>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value of the option.</param>
    public void SetDefine(MagickFormat format, string name, string value)
    {
        Throw.IfNullOrEmpty(nameof(name), name);
        Throw.IfNull(nameof(value), value);

        SetOption(ParseDefine(format, name), value);
    }

    /// <summary>
    /// Sets a format-specific option.
    /// </summary>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value of the option.</param>
    public void SetDefine(string name, string value)
    {
        Throw.IfNullOrEmpty(nameof(name), name);
        Throw.IfNull(nameof(value), value);

        SetOption(name, value);
    }

    /// <summary>
    /// Sets format-specific options with the specified defines.
    /// </summary>
    /// <param name="defines">The defines to set.</param>
    public void SetDefines(IDefines defines)
    {
        Throw.IfNull(nameof(defines), defines);

        foreach (var define in defines.Defines)
        {
            if (define is not null)
                SetDefine(define.Format, define.Name, define.Value);
        }
    }

    internal MagickSettings Clone()
    {
        var clone = new MagickSettings();
        clone.CopyFrom(this);

        return clone;
    }

    /// <summary>
    /// Copies the settings from the specified <see cref="MagickSettings"/>.
    /// </summary>
    /// <param name="settings">The settings to copy the data from.</param>
    protected void CopyFrom(MagickSettings settings)
    {
        if (settings is null)
            return;

        AntiAlias = settings.AntiAlias;
        BackgroundColor = MagickColor.Clone(settings.BackgroundColor);
        ColorSpace = settings.ColorSpace;
        ColorType = settings.ColorType;
        Compression = settings.Compression;
        Debug = settings.Debug;
        Density = CloneDensity(settings.Density);
        Depth = settings.Depth;
        Endian = settings.Endian;
        Extract = MagickGeometry.Clone(settings.Extract);
        Font = settings.Font;
        _fontPointsize = settings._fontPointsize;
        Format = settings.Format;
        Monochrome = settings.Monochrome;
        Page = MagickGeometry.Clone(settings.Page);
        Verbose = settings.Verbose;

        ColorFuzz = settings.ColorFuzz;
        Interlace = settings.Interlace;
        Ping = settings.Ping;
        Quality = settings.Quality;
        Size = settings.Size;

        foreach (var key in settings._options.Keys)
            _options[key] = settings._options[key];

        Drawing = settings.Drawing.Clone();
    }

    /// <summary>
    /// Gets an image option.
    /// </summary>
    /// <param name="key">The key of the option.</param>
    /// <returns>The value of the option.</returns>
    protected string? GetOption(string key)
    {
        Throw.IfNullOrEmpty(nameof(key), key);

        if (_options.TryGetValue(key, out var result))
            return result;

        return null;
    }

    /// <summary>
    /// Sets an image option.
    /// </summary>
    /// <param name="key">The key of the option.</param>
    /// <param name="value">The value of the option.</param>
    protected void SetOption(string key, string? value)
        => _options[key] = value;

    private static string ParseDefine(MagickFormat format, string name)
    {
        if (format == MagickFormat.Unknown)
            return name;

        var module = GetModule(format);
        return EnumHelper.GetName(module) + ":" + name;
    }

    private static MagickFormat GetModule(MagickFormat format)
    {
        var formatInfo = MagickFormatInfo.Create(format);
        if (formatInfo is null)
            return format;

        return formatInfo.ModuleFormat;
    }

    private static Density? CreateDensity(string? value)
    {
        if (value is null || value.Length == 0)
            return null;

        return new Density(value);
    }

    private static Density? CloneDensity(Density? density)
    {
        if (density is null)
            return null;

        return new Density(density.X, density.Y, density.Units);
    }

    private static NativeMagickSettings CreateNativeInstance(IMagickSettings<QuantumType> instance)
    {
        var settings = (MagickSettings)instance;

        var format = settings.GetFormat();
        var fileName = settings.FileName;
        if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(format))
            fileName = format + ":" + fileName;

        var result = NativeMagickSettings.Create();
        result.AntiAlias_Set(settings.AntiAlias);
        result.BackgroundColor_Set(settings.BackgroundColor);
        result.ColorSpace_Set(settings.ColorSpace);
        result.ColorType_Set(settings.ColorType);
        result.Compression_Set(settings.Compression);
        result.Debug_Set(settings.Debug);
        result.Density_Set(settings.Density?.ToString(DensityUnit.Undefined));
        result.Depth_Set(settings.Depth);
        result.Endian_Set(settings.Endian);
        result.Extract_Set(settings.Extract?.ToString());
        result.FontPointsize_Set(settings._fontPointsize);
        result.Format_Set(format);
        result.Interlace_Set(settings.Interlace);
        result.Monochrome_Set(settings.Monochrome);
        result.Verbose_Set(settings.Verbose);

        result.SetColorFuzz(settings.ColorFuzz);
        result.SetFileName(fileName);
        result.SetFont(settings.Font);
        result.SetNumberScenes(settings.NumberScenes);
        result.SetPage(settings.Page?.ToString());
        result.SetPing(settings.Ping);
        result.SetQuality(settings.Quality);
        result.SetScene(settings.Scene);
        result.SetScenes(settings.Scenes);
        result.SetSize(settings.Size);

        foreach (var key in settings._options.Keys)
            result.SetOption(key, settings._options[key]);

        return result;
    }

    private string? GetFormat()
        => Format switch
        {
            MagickFormat.Unknown => null,
            MagickFormat.ThreeFr => "3FR",
            MagickFormat.ThreeG2 => "3G2",
            MagickFormat.ThreeGp => "3GP",
            MagickFormat.RadialGradient => "RADIAL-GRADIENT",
            MagickFormat.SparseColor => "SPARSE-COLOR",
            _ => EnumHelper.GetName(Format).ToUpperInvariant(),
        };

    private void SetOptionAndArtifact(string key, double value)
        => SetOptionAndArtifact(key, value.ToString(CultureInfo.InvariantCulture));

    private void SetOptionAndArtifact(string key, Enum value)
        => SetOptionAndArtifact(key, EnumHelper.GetName(value).ToLowerInvariant());

    private void SetOptionAndArtifact(string key, string? value)
    {
        SetOption(key, value);

        Artifact?.Invoke(this, new ArtifactEventArgs(key, value));
    }
}
