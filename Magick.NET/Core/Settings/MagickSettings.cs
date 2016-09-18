//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace ImageMagick
{
  ///<summary>
  /// Class that contains various settings.
  ///</summary>
  public partial class MagickSettings
  {
    private string _Font;
    private double _FontPointsize;
    private Dictionary<string, string> _Options = new Dictionary<string, string>();

    private INativeInstance CreateNativeInstance()
    {
      string format = Format != MagickFormat.Unknown ? EnumHelper.GetName(Format).ToUpperInvariant() : null;
      string fileName = FileName;
      if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(format))
        fileName = format + ":" + fileName;

      NativeMagickSettings instance = new NativeMagickSettings();
      instance.BackgroundColor = BackgroundColor;
      instance.ColorSpace = ColorSpace;
      instance.ColorType = ColorType;
      instance.CompressionMethod = CompressionMethod;
      instance.Debug = Debug;
      instance.Density = Density != null ? Density.ToString(DensityUnit.Undefined) : null;
      instance.Endian = Endian;
      instance.Font = _Font;
      instance.FontPointsize = _FontPointsize;
      instance.Format = format;
      instance.Interlace = Interlace;
      instance.Monochrome = Monochrome;
      instance.Verbose = Verbose;

      instance.SetColorFuzz(ColorFuzz);
      instance.SetFileName(fileName);
      instance.SetNumberScenes(NumberScenes);
      instance.SetPage(MagickGeometry.ToString(Page));
      instance.SetPing(Ping);
      instance.SetQuality(Quality);
      instance.SetScene(Scene);
      instance.SetScenes(Scenes);
      instance.SetSize(Size);

      foreach (string key in _Options.Keys)
        instance.SetOption(key, _Options[key]);

      return instance;
    }

    private static MagickFormat GetModule(MagickFormat format)
    {
      MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(format);
      return formatInfo.Module;
    }

    private void SetOptionAndArtifact(string key, double value)
    {
      SetOptionAndArtifact(key, value.ToString(CultureInfo.InvariantCulture));
    }

    private void SetOptionAndArtifact(string key, string value)
    {
      SetOption(key, value);

      if (Artifact != null)
        Artifact(this, new ArtifactEventArgs(key, value));
    }

    internal MagickSettings()
    {
      using (NativeMagickSettings instance = new NativeMagickSettings())
      {
        BackgroundColor = instance.BackgroundColor;
        ColorSpace = instance.ColorSpace;
        ColorType = instance.ColorType;
        CompressionMethod = instance.CompressionMethod;
        Debug = instance.Debug;
        Density = Density.Create(instance.Density);
        Endian = instance.Endian;
        _Font = instance.Font;
        _FontPointsize = instance.FontPointsize;
        Format = EnumHelper.Parse(instance.Format, MagickFormat.Unknown);
        Interlace = instance.Interlace;
        Monochrome = instance.Monochrome;
        Verbose = instance.Verbose;
      }

      Drawing = new DrawingSettings();
    }

    internal event EventHandler<ArtifactEventArgs> Artifact;

    internal DrawingSettings Drawing
    {
      get;
      private set;
    }

    internal double ColorFuzz
    {
      get;
      set;
    }

    internal string FileName
    {
      get;
      set;
    }

    internal Interlace Interlace
    {
      get;
      set;
    }

    /// <summary>
    /// Number of scenes.
    /// </summary>
    protected int NumberScenes
    {
      get;
      set;
    }

    ///<summary>
    /// Use monochrome reader.
    ///</summary>
    protected bool Monochrome
    {
      get;
      set;
    }

    internal bool Ping
    {
      get;
      set;
    }

    internal int Quality
    {
      get;
      set;
    }

    /// <summary>
    /// The size of the image.
    /// </summary>
    protected string Size
    {
      get;
      set;
    }

    /// <summary>
    /// Active scene
    /// </summary>
    protected int Scene
    {
      get;
      set;
    }

    /// <summary>
    /// The scenes of the image.
    /// </summary>
    protected string Scenes
    {
      get;
      set;
    }

    internal MagickSettings Clone()
    {
      MagickSettings clone = new MagickSettings();
      clone.Copy(this);

      return clone;
    }

    /// <summary>
    /// Copies the settings from the specified settings.
    /// </summary>
    /// <param name="settings"></param>
    protected void Copy(MagickSettings settings)
    {
      if (settings == null)
        return;

      BackgroundColor = MagickColor.Clone(settings.BackgroundColor);
      ColorSpace = settings.ColorSpace;
      ColorType = settings.ColorType;
      CompressionMethod = settings.CompressionMethod;
      Debug = settings.Debug;
      Density = Density.Clone(settings.Density);
      Endian = settings.Endian;
      _Font = settings._Font;
      _FontPointsize = settings._FontPointsize;
      Format = settings.Format;
      Monochrome = settings.Monochrome;
      Page = MagickGeometry.Clone(settings.Page);
      Verbose = settings.Verbose;

      ColorFuzz = settings.ColorFuzz;
      Interlace = settings.Interlace;
      Ping = settings.Ping;
      Quality = settings.Quality;
      Size = settings.Size;

      foreach (string key in settings._Options.Keys)
        _Options[key] = settings._Options[key];

      Drawing = settings.Drawing.Clone();
    }

    internal string GetOption(string key)
    {
      Throw.IfNullOrEmpty(nameof(key), key);

      string result;
      if (_Options.TryGetValue(key, out result))
        return result;

      return null;
    }

    /// <summary>
    /// Creates a define string for the specified format and name.
    /// </summary>
    ///<param name="format">The format to set the define for.</param>
    /// <param name="name">The name of the define.</param>
    /// <returns></returns>
    protected static string ParseDefine(MagickFormat format, string name)
    {
      if (format == MagickFormat.Unknown)
        return name;
      else
        return EnumHelper.GetName(GetModule(format)) + ":" + name;
    }

    internal void SetOption(string key, string value)
    {
      _Options[key] = value;
    }

    /// <summary>
    /// Affine to use when annotating with text or drawing.
    /// </summary>
    public DrawableAffine Affine
    {
      get
      {
        return Drawing.Affine;
      }
      set
      {
        Drawing.Affine = value;
      }
    }

    ///<summary>
    /// Background color.
    ///</summary>
    public MagickColor BackgroundColor
    {
      get;
      set;
    }

    ///<summary>
    /// Border color.
    ///</summary>
    public MagickColor BorderColor
    {
      get
      {
        return Drawing.BorderColor;
      }
      set
      {
        Drawing.BorderColor = value;
      }
    }

    ///<summary>
    /// Color space.
    ///</summary>
    public ColorSpace ColorSpace
    {
      get;
      set;
    }

    ///<summary>
    /// Color type of the image.
    ///</summary>
    public ColorType ColorType
    {
      get;
      set;
    }

    ///<summary>
    /// Compression method to use.
    ///</summary>
    public CompressionMethod CompressionMethod
    {
      get;
      set;
    }

    ///<summary>
    /// Enable printing of debug messages from ImageMagick when a debugger is attached.
    ///</summary>
    public bool Debug
    {
      get;
      set;
    }

    ///<summary>
    /// Vertical and horizontal resolution in pixels.
    ///</summary>
    public Density Density
    {
      get;
      set;
    }

    ///<summary>
    /// Endianness (little like Intel or big like SPARC) for image formats which support
    /// endian-specific options.
    ///</summary>
    public Endian Endian
    {
      get;
      set;
    }

    ///<summary>
    /// Fill color
    ///</summary>
    public MagickColor FillColor
    {
      get
      {
        return Drawing.FillColor;
      }
      set
      {
        SetOptionAndArtifact("fill", MagickColor.ToString(value));
        Drawing.FillColor = value;
      }
    }

    ///<summary>
    /// Fill pattern
    ///</summary>
    public MagickImage FillPattern
    {
      get
      {
        return Drawing.FillPattern;
      }
      set
      {
        Drawing.FillPattern = value;
      }
    }

    ///<summary>
    /// Rule to use when filling drawn objects.
    ///</summary>
    public FillRule FillRule
    {
      get
      {
        return Drawing.FillRule;
      }
      set
      {
        Drawing.FillRule = value;
      }
    }

    ///<summary>
    /// Text rendering font.
    ///</summary>
    public string Font
    {
      get
      {
        return _Font;
      }
      set
      {
        _Font = value;
        Drawing.Font = value;
      }
    }

    ///<summary>
    /// Text font family.
    ///</summary>
    public string FontFamily
    {
      get
      {
        return GetOption("family");
      }
      set
      {
        SetOptionAndArtifact("family", value);
        Drawing.FontFamily = value;
      }
    }

    ///<summary>
    /// Font point size.
    ///</summary>
    public double FontPointsize
    {
      get
      {
        return _FontPointsize;
      }
      set
      {
        _FontPointsize = value;
        Drawing.FontPointsize = value;
      }
    }

    ///<summary>
    /// Font style.
    ///</summary>
    public FontStyleType FontStyle
    {
      get
      {
        return EnumHelper.Parse(GetOption("style"), FontStyleType.Undefined);
      }
      set
      {
        SetOptionAndArtifact("style", EnumHelper.GetName(value));
        Drawing.FontStyle = value;
      }
    }

    ///<summary>
    /// Font weight.
    ///</summary>
    public FontWeight FontWeight
    {
      get
      {
        string weight = GetOption("weight");
        if (string.IsNullOrEmpty(weight))
          return FontWeight.Undefined;

        int fontweight;
        int.TryParse(weight, NumberStyles.Number, CultureInfo.InvariantCulture, out fontweight);
        return EnumHelper.Parse(fontweight, FontWeight.Undefined);
      }
      set
      {
        SetOptionAndArtifact("weight", ((int)value).ToString(CultureInfo.InvariantCulture));
        Drawing.FontWeight = value;
      }
    }

    ///<summary>
    /// The format of the image.
    ///</summary>
    public MagickFormat Format
    {
      get;
      set;
    }

    ///<summary>
    /// Preferred size and location of an image canvas.
    ///</summary>
    public MagickGeometry Page
    {
      get;
      set;
    }

    ///<summary>
    /// Enabled/disable stroke anti-aliasing.
    ///</summary>
    public bool StrokeAntiAlias
    {
      get
      {
        return Drawing.StrokeAntiAlias;
      }
      set
      {
        Drawing.StrokeAntiAlias = value;
      }
    }

    ///<summary>
    /// Color to use when drawing object outlines.
    ///</summary>
    public MagickColor StrokeColor
    {
      get
      {
        return Drawing.StrokeColor;
      }
      set
      {
        SetOptionAndArtifact("stroke", MagickColor.ToString(value));
        Drawing.StrokeColor = value;
      }
    }

    ///<summary>
    /// Specify the pattern of dashes and gaps used to stroke paths. This represents a
    /// zero-terminated array of numbers that specify the lengths of alternating dashes and gaps
    /// in pixels. If a zero value is not found it will be added. If an odd number of values is
    /// provided, then the list of values is repeated to yield an even number of values.
    ///</summary>
    public IEnumerable<double> StrokeDashArray
    {
      get
      {
        return Drawing.StrokeDashArray;
      }
      set
      {
        Drawing.StrokeDashArray = value;
      }
    }

    ///<summary>
    /// While drawing using a dash pattern, specify distance into the dash pattern to start the
    /// dash (default 0).
    ///</summary>
    public double StrokeDashOffset
    {
      get
      {
        return Drawing.StrokeDashOffset;
      }
      set
      {
        Drawing.StrokeDashOffset = value;
      }
    }

    ///<summary>
    /// Specify the shape to be used at the end of open subpaths when they are stroked.
    ///</summary>
    public LineCap StrokeLineCap
    {
      get
      {
        return Drawing.StrokeLineCap;
      }
      set
      {
        Drawing.StrokeLineCap = value;
      }
    }

    ///<summary>
    /// Specify the shape to be used at the corners of paths (or other vector shapes) when they
    /// are stroked.
    ///</summary>
    public LineJoin StrokeLineJoin
    {
      get
      {
        return Drawing.StrokeLineJoin;
      }
      set
      {
        Drawing.StrokeLineJoin = value;
      }
    }

    ///<summary>
    /// Specify miter limit. When two line segments meet at a sharp angle and miter joins have
    /// been specified for 'lineJoin', it is possible for the miter to extend far beyond the thickness
    /// of the line stroking the path. The miterLimit' imposes a limit on the ratio of the miter
    /// length to the 'lineWidth'. The default value is 4.
    ///</summary>
    public int StrokeMiterLimit
    {
      get
      {
        return Drawing.StrokeMiterLimit;
      }
      set
      {
        Drawing.StrokeMiterLimit = value;
      }
    }

    ///<summary>
    /// Pattern image to use while stroking object outlines.
    ///</summary>
    public MagickImage StrokePattern
    {
      get
      {
        return Drawing.StrokePattern;
      }
      set
      {
        Drawing.StrokePattern = value;
      }
    }

    ///<summary>
    /// Stroke width for drawing lines, circles, ellipses, etc.
    ///</summary>
    public double StrokeWidth
    {
      get
      {
        return Drawing.StrokeWidth;
      }
      set
      {
        SetOptionAndArtifact("strokewidth", value);
        Drawing.StrokeWidth = value;
      }
    }

    ///<summary>
    /// Anti-alias Postscript and TrueType fonts (default true).
    ///</summary>
    public bool TextAntiAlias
    {
      get
      {
        return Drawing.TextAntiAlias;
      }
      set
      {
        Drawing.TextAntiAlias = value;
      }
    }

    ///<summary>
    /// Render text right-to-left or left-to-right. 
    ///</summary>
    public TextDirection TextDirection
    {
      get
      {
        return Drawing.TextDirection;
      }
      set
      {
        Drawing.TextDirection = value;
      }
    }

    ///<summary>
    /// Annotation text encoding (e.g. "UTF-16").
    ///</summary>
    public Encoding TextEncoding
    {
      get
      {
        return Drawing.TextEncoding;
      }
      set
      {
        Drawing.TextEncoding = value;
      }
    }

    ///<summary>
    /// Annotation text gravity.
    ///</summary>
    public Gravity TextGravity
    {
      get
      {
        return Drawing.TextGravity;
      }
      set
      {
        SetOptionAndArtifact("gravity", EnumHelper.GetName(value));
        Drawing.TextGravity = value;
      }
    }

    ///<summary>
    /// Text inter-line spacing.
    ///</summary>
    public double TextInterlineSpacing
    {
      get
      {
        return Drawing.TextInterlineSpacing;
      }
      set
      {
        SetOptionAndArtifact("interline-spacing", value);
        Drawing.TextInterlineSpacing = value;
      }
    }

    ///<summary>
    /// Text inter-word spacing.
    ///</summary>
    public double TextInterwordSpacing
    {
      get
      {
        return Drawing.TextInterwordSpacing;
      }
      set
      {
        SetOptionAndArtifact("interword-spacing", value);
        Drawing.TextInterwordSpacing = value;
      }
    }

    ///<summary>
    /// Text inter-character kerning.
    ///</summary>
    public double TextKerning
    {
      get
      {
        return Drawing.TextKerning;
      }
      set
      {
        SetOptionAndArtifact("kerning", value);
        Drawing.TextKerning = value;
      }
    }

    ///<summary>
    /// Text undercolor box.
    ///</summary>
    public MagickColor TextUnderColor
    {
      get
      {
        return Drawing.TextUnderColor;
      }
      set
      {
        SetOptionAndArtifact("undercolor", MagickColor.ToString(value));
        Drawing.TextUnderColor = value;
      }
    }

    ///<summary>
    /// Turn verbose output on/off.
    ///</summary>
    public bool Verbose
    {
      get;
      set;
    }

    ///<summary>
    /// Returns the value of a format-specific option.
    ///</summary>
    ///<param name="format">The format to get the option for.</param>
    ///<param name="name">The name of the option.</param>
    public string GetDefine(MagickFormat format, string name)
    {
      Throw.IfNullOrEmpty(nameof(name), name);

      return GetOption(ParseDefine(format, name));
    }

    ///<summary>
    /// Returns the value of a format-specific option.
    ///</summary>
    ///<param name="name">The name of the option.</param>
    public string GetDefine(string name)
    {
      Throw.IfNullOrEmpty(nameof(name), name);

      return GetOption(name);
    }

    ///<summary>
    /// Removes the define with the specified name.
    ///</summary>
    ///<param name="format">The format to set the define for.</param>
    ///<param name="name">The name of the define.</param>
    public void RemoveDefine(MagickFormat format, string name)
    {
      Throw.IfNullOrEmpty(nameof(name), name);

      string key = ParseDefine(format, name);
      if (_Options.ContainsKey(key))
        _Options.Remove(key);
    }

    ///<summary>
    /// Removes the define with the specified name.
    ///</summary>
    ///<param name="name">The name of the define.</param>
    public void RemoveDefine(string name)
    {
      Throw.IfNullOrEmpty(nameof(name), name);

      if (_Options.ContainsKey(name))
        _Options.Remove(name);
    }

    ///<summary>
    /// Sets a format-specific option.
    ///</summary>
    ///<param name="format">The format to set the define for.</param>
    ///<param name="name">The name of the define.</param>
    ///<param name="flag">The value of the define.</param>
    [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flag")]
    public void SetDefine(MagickFormat format, string name, bool flag)
    {
      SetDefine(format, name, flag ? "true" : "false");
    }

    ///<summary>
    /// Sets a format-specific option.
    ///</summary>
    ///<param name="format">The format to set the option for.</param>
    ///<param name="name">The name of the option.</param>
    ///<param name="value">The value of the option.</param>
    public void SetDefine(MagickFormat format, string name, string value)
    {
      Throw.IfNullOrEmpty(nameof(name), name);
      Throw.IfNull(nameof(value), value);

      SetOption(ParseDefine(format, name), value);
    }

    ///<summary>
    /// Sets a format-specific option.
    ///</summary>
    ///<param name="name">The name of the option.</param>
    ///<param name="value">The value of the option.</param>
    public void SetDefine(string name, string value)
    {
      Throw.IfNullOrEmpty(nameof(name), name);
      Throw.IfNull(nameof(value), value);

      SetOption(name, value);
    }

    ///<summary>
    /// Sets format-specific options with the specified defines.
    ///</summary>
    ///<param name="defines">The defines to set.</param>
    public void SetDefines([ValidatedNotNull] IDefines defines)
    {
      Throw.IfNull(nameof(defines), defines);

      foreach (IDefine define in defines.Defines)
      {
        SetDefine(define.Format, define.Name, define.Value);
      }
    }
  }
}
