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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace ImageMagick
{
  ///<summary>
  /// Class that contains various settings.
  ///</summary>
  public sealed partial class MagickSettings
  {
    private MagickSettings(NativeMagickSettings instance)
    {
      _NativeInstance = instance;
      Drawing = new DrawingSettings();
    }

    private void ApplyColorSpace(MagickReadSettings readSettings)
    {
      if (readSettings.ColorSpace != null)
        ColorSpace = readSettings.ColorSpace.Value;
    }

    private void ApplyDefines(MagickReadSettings readSettings)
    {
      foreach (IDefine define in readSettings.AllDefines)
      {
        _NativeInstance.SetOption(GetDefineKey(define), define.Value);
      }
    }

    private void ApplyDensity(MagickReadSettings readSettings)
    {
      if (!readSettings.Density.HasValue)
        return;

      Density = readSettings.Density.Value;
    }

    private void ApplyDimensions(MagickReadSettings readSettings)
    {
      if (!readSettings.Width.HasValue || !readSettings.Height.HasValue)
        return;

      _NativeInstance.SetSize(readSettings.Width + "x" + readSettings.Height);
    }

    private void ApplyFormat(MagickReadSettings readSettings)
    {
      if (!readSettings.Format.HasValue)
        return;

      Format = readSettings.Format.Value;
    }

    private void ApplyFrame(MagickReadSettings readSettings)
    {
      if (!readSettings.FrameIndex.HasValue && !readSettings.FrameCount.HasValue)
        return;

      _NativeInstance.SetScenes(readSettings.Scenes);
      _NativeInstance.SetScene((readSettings.FrameIndex.HasValue ? readSettings.FrameIndex.Value : 0));
      _NativeInstance.SetNumberScenes((readSettings.FrameCount.HasValue ? readSettings.FrameCount.Value : 1));
    }

    private void ApplyUseMonochrome(MagickReadSettings readSettings)
    {
      if (readSettings.UseMonochrome.HasValue)
        _NativeInstance.SetMonochrome(readSettings.UseMonochrome.Value);
    }

    private void Dispose(bool disposing)
    {
      if (_NativeInstance != null)
        _NativeInstance.Dispose();

      if (disposing)
      {
        if (Drawing != null)
          Drawing.Dispose();
      }
    }

    private static string GetDefineKey(IDefine define)
    {
      if (define.Format == MagickFormat.Unknown)
        return define.Name;

      return EnumHelper.GetName(define.Format) + ":" + define.Name;
    }

    private static MagickFormat GetModule(MagickFormat format)
    {
      MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(format);
      return formatInfo.Module;
    }

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MagickFormat")]
    private static string ParseDefine(MagickFormat format, string name)
    {
      if (format == MagickFormat.Unknown)
        return name;
      else
        return EnumHelper.GetName(GetModule(format)) + ":" + name;
    }

    private void SetOptionAndArtifact(string key, string value)
    {
      _NativeInstance.SetOption(key, value);

      if (Artifact != null)
        Artifact(this, new ArtifactEventArgs(key, value));
    }

    internal MagickSettings()
      : this(new NativeMagickSettings())
    {
    }

    internal event EventHandler<ArtifactEventArgs> Artifact;

    internal DrawingSettings Drawing
    {
      get;
      private set;
    }

    internal string FileName
    {
      get
      {
        return _NativeInstance.FileName;
      }
      set
      {
        _NativeInstance.FileName = value;
      }
    }

    internal bool Ping
    {
      get
      {
        return _NativeInstance.Ping;
      }
      set
      {
        _NativeInstance.Ping = value;
      }
    }

    internal void Apply(MagickReadSettings readSettings)
    {
      FileName = null;

      if (readSettings == null)
        return;

      ApplyColorSpace(readSettings);
      ApplyDefines(readSettings);
      ApplyDensity(readSettings);
      ApplyDimensions(readSettings);
      ApplyFormat(readSettings);
      ApplyFrame(readSettings);
      ApplyUseMonochrome(readSettings);
    }

    internal MagickSettings Clone()
    {
      return new MagickSettings(new NativeMagickSettings(_NativeInstance.Clone()));
    }

    internal string GetDefine(MagickFormat format, string name)
    {
      Throw.IfNullOrEmpty("name", name);

      return _NativeInstance.GetOption(ParseDefine(format, name));
    }

    internal string GetOption(string key)
    {
      Throw.IfNullOrEmpty("key", key);

      return _NativeInstance.GetOption(key);
    }

    internal void RemoveDefine(MagickFormat format, string name)
    {
      Throw.IfNullOrEmpty("name", name);

      _NativeInstance.RemoveOption(ParseDefine(format, name));
    }

    internal void SetColorFuzz(double value)
    {
      _NativeInstance.SetColorFuzz(value);
    }

    internal void SetDefine(MagickFormat format, string name, string value)
    {
      Throw.IfNullOrEmpty("name", name);
      Throw.IfNull("value", value);

      _NativeInstance.SetOption(ParseDefine(format, name), value);
    }

    internal void SetOption(string key, string value)
    {
      Throw.IfNullOrEmpty("key", key);
      Throw.IfNull("value", value);

      _NativeInstance.SetOption(key, value);
    }

    internal void SetQuality(int value)
    {
      _NativeInstance.SetQuality(value);
    }

    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    void IDisposable.Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Finalizer
    /// </summary>
    ~MagickSettings()
    {
      Dispose(false);
    }

    ///<summary>
    /// Join images into a single multi-image file.
    ///</summary>
    public bool Adjoin
    {
      get
      {
        return _NativeInstance.Adjoin;
      }
      set
      {
        _NativeInstance.Adjoin = value;
      }
    }

    ///<summary>
    /// Transparent color.
    ///</summary>
    public MagickColor AlphaColor
    {
      get
      {
        return _NativeInstance.AlphaColor;
      }
      set
      {
        _NativeInstance.AlphaColor = value;
      }
    }

    ///<summary>
    /// Background color.
    ///</summary>
    public MagickColor BackgroundColor
    {
      get
      {
        return _NativeInstance.BackgroundColor;
      }
      set
      {
        if (value == null)
          return;

        _NativeInstance.BackgroundColor = value;
      }
    }

    ///<summary>
    /// Border color.
    ///</summary>
    public MagickColor BorderColor
    {
      get
      {
        return _NativeInstance.BorderColor;
      }
      set
      {
        _NativeInstance.BorderColor = value;
        Drawing.BorderColor = value;
      }
    }

    ///<summary>
    /// Color space.
    ///</summary>
    public ColorSpace ColorSpace
    {
      get
      {
        return _NativeInstance.ColorSpace;
      }
      set
      {
        _NativeInstance.ColorSpace = value;
      }
    }

    ///<summary>
    /// Color type of the image.
    ///</summary>
    public ColorType ColorType
    {
      get
      {
        return _NativeInstance.ColorType;
      }
      set
      {
        _NativeInstance.ColorType = value;
      }
    }

    ///<summary>
    /// Compression method to use.
    ///</summary>
    public CompressionMethod CompressionMethod
    {
      get
      {
        return _NativeInstance.CompressionMethod;
      }
      set
      {
        _NativeInstance.CompressionMethod = value;
      }
    }

    ///<summary>
    /// Enable printing of debug messages from ImageMagick when a debugger is attached.
    ///</summary>
    public bool Debug
    {
      get
      {
        return _NativeInstance.Debug;
      }
      set
      {
        _NativeInstance.Debug = value;
      }
    }

    ///<summary>
    /// Vertical and horizontal resolution in pixels.
    ///</summary>
    public PointD Density
    {
      get
      {
        return PointD.Create(_NativeInstance.Density);
      }
      set
      {
        _NativeInstance.Density = value.ToString();
      }
    }

    ///<summary>
    /// Endianness (little like Intel or big like SPARC) for image formats which support
    /// endian-specific options.
    ///</summary>
    public Endian Endian
    {
      get
      {
        return _NativeInstance.Endian;
      }
      set
      {
        _NativeInstance.Endian = value;
      }
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
        return _NativeInstance.Font;
      }
      set
      {
        _NativeInstance.Font = value;
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
        return _NativeInstance.GetOption("family");
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
        return _NativeInstance.FontPointsize;
      }
      set
      {
        _NativeInstance.FontPointsize = value;
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
        return EnumHelper.Parse(_NativeInstance.GetOption("style"), FontStyleType.Undefined);
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
        string weight = _NativeInstance.GetOption("weight");

        int fontweight;
        int.TryParse(weight, NumberStyles.Number, CultureInfo.InvariantCulture, out fontweight);
        return EnumHelper.Parse(fontweight, ImageMagick.FontWeight.Undefined);
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
      get
      {
        return EnumHelper.Parse(_NativeInstance.Format, MagickFormat.Unknown);
      }
      set
      {
        _NativeInstance.Format = EnumHelper.GetName(value);
      }
    }

    ///<summary>
    /// Preferred size and location of an image canvas.
    ///</summary>
    public MagickGeometry Page
    {
      get
      {
        string value = _NativeInstance.Page;
        if (string.IsNullOrEmpty(value))
          return new MagickGeometry(0, 0, 0, 0);

        return new MagickGeometry(value);
      }
      set
      {
        _NativeInstance.Page = MagickGeometry.ToString(value);
      }
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
    [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
    public double[] StrokeDashArray
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
        SetOption("gravity", EnumHelper.GetName(value));
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
        Drawing.TextUnderColor = value;
      }
    }

    ///<summary>
    /// Turn verbose output on/off.
    ///</summary>
    public bool Verbose
    {
      get
      {
        return _NativeInstance.Verbose;
      }
      set
      {
        _NativeInstance.Verbose = value;
      }
    }

    ///<summary>
    /// Reset transformation parameters to default.
    ///</summary>
    ///<exception cref="MagickException"/>
    public void ResetTransform()
    {
      Drawing.ResetTransform();
    }

    ///<summary>
    /// Origin of coordinate system to use when annotating with text or drawing.
    ///</summary>
    ///<param name="x">The X coordinate.</param>
    ///<param name="y">The Y coordinate.</param>
    ///<exception cref="MagickException"/>
    public void SetTransformOrigin(double x, double y)
    {
      Drawing.SetTransformOrigin(x, y);
    }

    ///<summary>
    /// Rotation to use when annotating with text or drawing.
    ///</summary>
    ///<param name="angle">The angle.</param>
    ///<exception cref="MagickException"/>
    public void SetTransformRotation(double angle)
    {
      Drawing.SetTransformRotation(angle);
    }

    ///<summary>
    /// Scale to use when annotating with text or drawing.
    ///</summary>
    ///<param name="x">The X coordinate scaling element.</param>
    ///<param name="y">The Y coordinate scaling element.</param>
    ///<exception cref="MagickException"/>
    public void SetTransformScale(double x, double y)
    {
      Drawing.SetTransformScale(x, y);
    }

    ///<summary>
    /// Skew to use in X axis when annotating with text or drawing.
    ///</summary>
    ///<param name="value">The X skew.</param>
    ///<exception cref="MagickException"/>
    public void SetTransformSkewX(double value)
    {
      Drawing.SetTransformSkewX(value);
    }

    ///<summary>
    /// Skew to use in Y axis when annotating with text or drawing.
    ///</summary>
    ///<param name="value">The Y skew.</param>
    ///<exception cref="MagickException"/>
    public void SetTransformSkewY(double value)
    {
      Drawing.SetTransformSkewY(value);
    }
  }
}
