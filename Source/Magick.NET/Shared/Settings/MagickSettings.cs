// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace ImageMagick
{
    /// <summary>
    /// Class that contains various settings.
    /// </summary>
    public partial class MagickSettings
    {
        private readonly Dictionary<string, string> _options = new Dictionary<string, string>();

        private string _font;
        private double _fontPointsize;

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
                Extract = MagickGeometry.FromString(instance.Extract);
                _font = instance.Font;
                _fontPointsize = instance.FontPointsize;
                Format = EnumHelper.Parse(instance.Format, MagickFormat.Unknown);
                Interlace = instance.Interlace;
                Monochrome = instance.Monochrome;
                Verbose = instance.Verbose;
            }

            Drawing = new DrawingSettings();
        }

        internal event EventHandler<ArtifactEventArgs> Artifact;

        /// <summary>
        /// Gets or sets the affine to use when annotating with text or drawing.
        /// </summary>
        public DrawableAffine Affine
        {
            get { return Drawing.Affine; }
            set { Drawing.Affine = value; }
        }

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public MagickColor BackgroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the border color.
        /// </summary>
        public MagickColor BorderColor
        {
            get { return Drawing.BorderColor; }
            set { Drawing.BorderColor = value; }
        }

        /// <summary>
        /// Gets or sets the color space.
        /// </summary>
        public ColorSpace ColorSpace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color type of the image.
        /// </summary>
        public ColorType ColorType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the compression method to use.
        /// </summary>
        public CompressionMethod CompressionMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether printing of debug messages from ImageMagick is enabled when a debugger is attached.
        /// </summary>
        public bool Debug
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the vertical and horizontal resolution in pixels.
        /// </summary>
        public Density Density
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the endianness (little like Intel or big like SPARC) for image formats which support
        /// endian-specific options.
        /// </summary>
        public Endian Endian
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fill color.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the fill pattern.
        /// </summary>
        public IMagickImage FillPattern
        {
            get { return Drawing.FillPattern; }
            set { Drawing.FillPattern = value; }
        }

        /// <summary>
        /// Gets or sets the rule to use when filling drawn objects.
        /// </summary>
        public FillRule FillRule
        {
            get { return Drawing.FillRule; }
            set { Drawing.FillRule = value; }
        }

        /// <summary>
        /// Gets or sets the text rendering font.
        /// </summary>
        public string Font
        {
            get
            {
                return _font;
            }

            set
            {
                _font = value;
                Drawing.Font = value;
            }
        }

        /// <summary>
        /// Gets or sets the text font family.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the font point size.
        /// </summary>
        public double FontPointsize
        {
            get
            {
                return _fontPointsize;
            }

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

        /// <summary>
        /// Gets or sets the font weight.
        /// </summary>
        public FontWeight FontWeight
        {
            get
            {
                string weight = GetOption("weight");
                if (string.IsNullOrEmpty(weight))
                    return FontWeight.Undefined;

                if (!int.TryParse(weight, NumberStyles.Number, CultureInfo.InvariantCulture, out int fontweight))
                    return FontWeight.Undefined;

                return EnumHelper.Parse(fontweight, FontWeight.Undefined);
            }

            set
            {
                SetOptionAndArtifact("weight", ((int)value).ToString(CultureInfo.InvariantCulture));
                Drawing.FontWeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the the format of the image.
        /// </summary>
        public MagickFormat Format
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the preferred size and location of an image canvas.
        /// </summary>
        public MagickGeometry Page
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether  stroke anti-aliasing is enabled or disabled.
        /// </summary>
        public bool StrokeAntiAlias
        {
            get { return Drawing.StrokeAntiAlias; }
            set { Drawing.StrokeAntiAlias = value; }
        }

        /// <summary>
        /// Gets or sets the color to use when drawing object outlines.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the pattern of dashes and gaps used to stroke paths. This represents a
        /// zero-terminated array of numbers that specify the lengths of alternating dashes and gaps
        /// in pixels. If a zero value is not found it will be added. If an odd number of values is
        /// provided, then the list of values is repeated to yield an even number of values.
        /// </summary>
        public IEnumerable<double> StrokeDashArray
        {
            get { return Drawing.StrokeDashArray; }
            set { Drawing.StrokeDashArray = value; }
        }

        /// <summary>
        /// Gets or sets the distance into the dash pattern to start the dash (default 0) while
        /// drawing using a dash pattern,.
        /// </summary>
        public double StrokeDashOffset
        {
            get { return Drawing.StrokeDashOffset; }
            set { Drawing.StrokeDashOffset = value; }
        }

        /// <summary>
        /// Gets or sets the shape to be used at the end of open subpaths when they are stroked.
        /// </summary>
        public LineCap StrokeLineCap
        {
            get { return Drawing.StrokeLineCap; }
            set { Drawing.StrokeLineCap = value; }
        }

        /// <summary>
        /// Gets or sets the shape to be used at the corners of paths (or other vector shapes) when they
        /// are stroked.
        /// </summary>
        public LineJoin StrokeLineJoin
        {
            get { return Drawing.StrokeLineJoin; }
            set { Drawing.StrokeLineJoin = value; }
        }

        /// <summary>
        /// Gets or sets the miter limit. When two line segments meet at a sharp angle and miter joins have
        /// been specified for 'lineJoin', it is possible for the miter to extend far beyond the thickness
        /// of the line stroking the path. The miterLimit' imposes a limit on the ratio of the miter
        /// length to the 'lineWidth'. The default value is 4.
        /// </summary>
        public int StrokeMiterLimit
        {
            get { return Drawing.StrokeMiterLimit; }
            set { Drawing.StrokeMiterLimit = value; }
        }

        /// <summary>
        /// Gets or sets the pattern image to use while stroking object outlines.
        /// </summary>
        public IMagickImage StrokePattern
        {
            get { return Drawing.StrokePattern; }
            set { Drawing.StrokePattern = value; }
        }

        /// <summary>
        /// Gets or sets the stroke width for drawing lines, circles, ellipses, etc.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether Postscript and TrueType fonts should be anti-aliased (default true).
        /// </summary>
        public bool TextAntiAlias
        {
            get { return Drawing.TextAntiAlias; }
            set { Drawing.TextAntiAlias = value; }
        }

        /// <summary>
        /// Gets or sets text direction (right-to-left or left-to-right).
        /// </summary>
        public TextDirection TextDirection
        {
            get { return Drawing.TextDirection; }
            set { Drawing.TextDirection = value; }
        }

        /// <summary>
        /// Gets or sets the text annotation encoding (e.g. "UTF-16").
        /// </summary>
        public Encoding TextEncoding
        {
            get { return Drawing.TextEncoding; }
            set { Drawing.TextEncoding = value; }
        }

        /// <summary>
        /// Gets or sets the text annotation gravity.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the text inter-line spacing.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the text inter-word spacing.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the text inter-character kerning.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the text undercolor box.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether verbose output os turned on or off.
        /// </summary>
        public bool Verbose
        {
            get;
            set;
        }

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
        /// Gets or sets the specified area to extract from the image.
        /// </summary>
        protected MagickGeometry Extract
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of scenes.
        /// </summary>
        protected int NumberScenes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a monochrome reader should be used.
        /// </summary>
        protected bool Monochrome
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the size of the image.
        /// </summary>
        protected string Size
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the active scene.
        /// </summary>
        protected int Scene
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets scenes of the image.
        /// </summary>
        protected string Scenes
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the value of a format-specific option.
        /// </summary>
        /// <param name="format">The format to get the option for.</param>
        /// <param name="name">The name of the option.</param>
        /// <returns>The value of a format-specific option.</returns>
        public string GetDefine(MagickFormat format, string name)
        {
            Throw.IfNullOrEmpty(nameof(name), name);

            return GetOption(ParseDefine(format, name));
        }

        /// <summary>
        /// Returns the value of a format-specific option.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <returns>The value of a format-specific option.</returns>
        public string GetDefine(string name)
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

            string key = ParseDefine(format, name);
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
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flag", Justification = "We use flag to avoid name conflict with other overload in MagickScript.")]
        public void SetDefine(MagickFormat format, string name, bool flag)
        {
            SetDefine(format, name, flag ? "true" : "false");
        }

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
        public void SetDefines([ValidatedNotNull] IDefines defines)
        {
            Throw.IfNull(nameof(defines), defines);

            foreach (IDefine define in defines.Defines)
            {
                if (define != null)
                    SetDefine(define.Format, define.Name, define.Value);
            }
        }

        internal MagickSettings Clone()
        {
            MagickSettings clone = new MagickSettings();
            clone.Copy(this);

            return clone;
        }

        internal string GetOption(string key)
        {
            Throw.IfNullOrEmpty(nameof(key), key);

            if (_options.TryGetValue(key, out string result))
                return result;

            return null;
        }

        internal void SetOption(string key, string value)
        {
            _options[key] = value;
        }

        /// <summary>
        /// Creates a define string for the specified format and name.
        /// </summary>
        /// <param name="format">The format to set the define for.</param>
        /// <param name="name">The name of the define.</param>
        /// <returns>A string for the specified format and name.</returns>
        protected static string ParseDefine(MagickFormat format, string name)
        {
            if (format == MagickFormat.Unknown)
                return name;
            else
                return EnumHelper.GetName(GetModule(format)) + ":" + name;
        }

        /// <summary>
        /// Copies the settings from the specified <see cref="MagickSettings"/>.
        /// </summary>
        /// <param name="settings">The settings to copy the data from.</param>
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
            Extract = MagickGeometry.Clone(settings.Extract);
            _font = settings._font;
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

            foreach (string key in settings._options.Keys)
                _options[key] = settings._options[key];

            Drawing = settings.Drawing.Clone();
        }

        private static MagickFormat GetModule(MagickFormat format)
        {
            MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(format);
            return formatInfo.Module;
        }

        private INativeInstance CreateNativeInstance()
        {
            string format = GetFormat();
            string fileName = FileName;
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(format))
                fileName = format + ":" + fileName;

            NativeMagickSettings instance = new NativeMagickSettings();
            instance.BackgroundColor = BackgroundColor;
            instance.ColorSpace = ColorSpace;
            instance.ColorType = ColorType;
            instance.CompressionMethod = CompressionMethod;
            instance.Debug = Debug;
            instance.Density = Density?.ToString(DensityUnit.Undefined);
            instance.Endian = Endian;
            instance.Extract = MagickGeometry.ToString(Extract);
            instance.Font = _font;
            instance.FontPointsize = _fontPointsize;
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

            foreach (string key in _options.Keys)
                instance.SetOption(key, _options[key]);

            return instance;
        }

        private string GetFormat()
        {
            switch (Format)
            {
                case MagickFormat.Unknown:
                    return null;
                case MagickFormat.ThreeFr:
                    return "3FR";
                case MagickFormat.ThreeG2:
                    return "3G2";
                case MagickFormat.ThreeGp:
                    return "3GP";
                default:
                    return EnumHelper.GetName(Format).ToUpperInvariant();
            }
        }

        private void SetOptionAndArtifact(string key, double value)
        {
            SetOptionAndArtifact(key, value.ToString(CultureInfo.InvariantCulture));
        }

        private void SetOptionAndArtifact(string key, string value)
        {
            SetOption(key, value);

            Artifact?.Invoke(this, new ArtifactEventArgs(key, value));
        }
    }
}
