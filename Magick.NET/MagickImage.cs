//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
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

#if !NET20
using System.Windows.Media.Imaging;
#endif

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that represents an ImageMagick image.
	///</summary>
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public sealed class MagickImage : IDisposable, IEquatable<MagickImage>, IComparable<MagickImage>
	{
		//===========================================================================================
		private Wrapper.MagickImage _Instance;
		private EventHandler<WarningEventArgs> _WarningEvent;
		//===========================================================================================
		private MagickImage(Wrapper.MagickImage instance)
		{
			_Instance = instance;
		}
		//===========================================================================================
		PointD CalculateContrastStretch(Percentage blackPoint, Percentage whitePoint)
		{
			double x = blackPoint.ToDouble();
			double y = whitePoint.ToDouble();

			double pixels = Width * Height;
			x *= (pixels / 100.0);
			y *= (pixels / 100.0);
			y = pixels - y;

			return new PointD(x, y);
		}
		//===========================================================================================
		private void DisposeInstance()
		{
			_Instance.Dispose();
		}
		//===========================================================================================
		private void FloodFill(MagickColor color, int x, int y, bool invert)
		{
			Throw.IfNull("color", color);

			_Instance.FloodFill(MagickColor.GetInstance(color), x, y, invert);
		}
		//===========================================================================================
		private void FloodFill(MagickColor color, int x, int y, MagickColor borderColor, bool inverse)
		{
			Throw.IfNull("color", color);
			Throw.IfNull("borderColor", borderColor);

			_Instance.FloodFill(MagickColor.GetInstance(color), x, y, MagickColor.GetInstance(borderColor), inverse);
		}
		//===========================================================================================
		private void FloodFill(MagickColor color, MagickGeometry geometry, bool inverse)
		{
			Throw.IfNull("color", color);
			Throw.IfNull("geometry", geometry);

			_Instance.FloodFill(MagickColor.GetInstance(color), MagickGeometry.GetInstance(geometry), inverse);
		}
		//===========================================================================================
		private void FloodFill(MagickColor color, MagickGeometry geometry, MagickColor borderColor, bool inverse)
		{
			Throw.IfNull("color", color);
			Throw.IfNull("borderColor", borderColor);
			Throw.IfNull("geometry", geometry);

			_Instance.FloodFill(MagickColor.GetInstance(color), MagickGeometry.GetInstance(geometry),
				MagickColor.GetInstance(borderColor), inverse);
		}
		//===========================================================================================
		private void FloodFill(MagickImage image, int x, int y, bool inverse)
		{
			Throw.IfNull("image", image);

			_Instance.FloodFill(GetInstance(image), x, y, inverse);
		}
		//===========================================================================================
		private void FloodFill(MagickImage image, int x, int y, MagickColor borderColor, bool inverse)
		{
			Throw.IfNull("image", image);
			Throw.IfNull("borderColor", borderColor);

			_Instance.FloodFill(GetInstance(image), x, y, MagickColor.GetInstance(borderColor), inverse);
		}
		//===========================================================================================
		private void FloodFill(MagickImage image, MagickGeometry geometry, bool inverse)
		{
			Throw.IfNull("image", image);
			Throw.IfNull("geometry", geometry);

			_Instance.FloodFill(GetInstance(image), MagickGeometry.GetInstance(geometry), inverse);
		}
		//===========================================================================================
		private void FloodFill(MagickImage image, MagickGeometry geometry, MagickColor borderColor, bool inverse)
		{
			Throw.IfNull("image", image);
			Throw.IfNull("borderColor", borderColor);
			Throw.IfNull("geometry", geometry);

			_Instance.FloodFill(GetInstance(image), MagickGeometry.GetInstance(geometry),
				MagickColor.GetInstance(borderColor), inverse);
		}
		//==============================================================================================
		private string FormatedFileSize()
		{
			Decimal fileSize = FileSize;

			string suffix = "";
			if (fileSize > 1073741824)
			{
				fileSize /= 1073741824;
				suffix = "GB";
			}
			else if (fileSize > 1048576)
			{
				fileSize /= 1048576;
				suffix = "MB";
			}
			else if (fileSize > 1024)
			{
				fileSize /= 1024;
				suffix = "kB";
			}

			return string.Format(CultureInfo.InvariantCulture, "{0:N2}{1}", fileSize, suffix);
		}
		//===========================================================================================
		private ColorProfile GetColorProfile(string name)
		{
			Byte[] data = _Instance.GetProfile(name);
			if (data == null)
				return null;

			return new ColorProfile(name, data);
		}
		//===========================================================================================
		private static MagickFormat GetModule(MagickFormat format)
		{
			MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(format);
			return formatInfo.Module;
		}
		//===========================================================================================
		private static bool IsSupportedImageFormat(ImageFormat format)
		{
			return
				format.Guid.Equals(ImageFormat.Bmp.Guid) ||
				format.Guid.Equals(ImageFormat.Gif.Guid) ||
				format.Guid.Equals(ImageFormat.Icon.Guid) ||
				format.Guid.Equals(ImageFormat.Jpeg.Guid) ||
				format.Guid.Equals(ImageFormat.Png.Guid) ||
				format.Guid.Equals(ImageFormat.Tiff.Guid);
		}
		//===========================================================================================
		private void LevelColors(MagickColor blackColor, MagickColor whiteColor, bool inverse)
		{
			Throw.IfNull("blackColor", blackColor);
			Throw.IfNull("whiteColor", whiteColor);

			_Instance.LevelColors(MagickColor.GetInstance(blackColor), MagickColor.GetInstance(whiteColor),
				inverse);
		}
		//===========================================================================================
		private void LevelColors(MagickColor blackColor, MagickColor whiteColor, Channels channels, bool inverse)
		{
			Throw.IfNull("blackColor", blackColor);
			Throw.IfNull("whiteColor", whiteColor);

			_Instance.LevelColors(MagickColor.GetInstance(blackColor), MagickColor.GetInstance(whiteColor),
				channels, inverse);
		}
		//===========================================================================================
		private void OnWarning(object sender, WarningEventArgs arguments)
		{
			if (_WarningEvent != null)
				_WarningEvent(this, arguments);
		}
		//===========================================================================================
		private void Opaque(MagickColor target, MagickColor fill, bool inverse)
		{
			Throw.IfNull("target", target);
			Throw.IfNull("fill", fill);

			_Instance.Opaque(MagickColor.GetInstance(target), MagickColor.GetInstance(fill), inverse);
		}
		//===========================================================================================
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MagickFormat")]
		private static Tuple<string, string> ParseDefine(MagickFormat format, string name)
		{
			if (format == MagickFormat.Unknown)
			{
				string[] info = name.Split(':');
				if (info.Length == 2)
					return new Tuple<string, string>(info[0], info[1]);
				else
					throw new InvalidOperationException("Invalid use of MagickFormat.Unknown");
			}
			else
				return new Tuple<string, string>(Enum.GetName(typeof(MagickFormat), GetModule(format)), name);
		}
		//===========================================================================================
		internal static MagickImage Create(Wrapper.MagickImage value)
		{
			if (value == null)
				return null;

			return new MagickImage(value);
		}
		//===========================================================================================
		internal static Wrapper.MagickImage GetInstance(MagickImage value)
		{
			if (value == null)
				return null;

			return value._Instance;
		}
		//===========================================================================================
		internal static MagickFormat GetFormat(ImageFormat format)
		{
			if (format == ImageFormat.Bmp)
				return MagickFormat.Bmp;
			else if (format == ImageFormat.Gif)
				return MagickFormat.Gif;
			else if (format == ImageFormat.Icon)
				return MagickFormat.Icon;
			else if (format == ImageFormat.Jpeg)
				return MagickFormat.Jpeg;
			else if (format == ImageFormat.Png)
				return MagickFormat.Png;
			else if (format == ImageFormat.Tiff)
				return MagickFormat.Tiff;
			else
				throw new NotSupportedException("Unsupported image format: " + format.ToString());
		}
		///==========================================================================================
		/// <summary>
		/// Finalizer
		/// </summary>
		~MagickImage()
		{
			DisposeInstance();
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class.
		///</summary>
		public MagickImage()
		{
			_Instance = new Wrapper.MagickImage();
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified byte array.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<exception cref="MagickException"/>
		public MagickImage(Byte[] data)
			: this()
		{
			Read(data);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified byte array.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		public MagickImage(Byte[] data, MagickReadSettings readSettings)
			: this()
		{
			Read(data, readSettings);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified bitmap.
		///</summary>
		///<param name="bitmap">The bitmap to use.</param>
		///<exception cref="MagickException"/>
		public MagickImage(Bitmap bitmap)
			: this()
		{
			Read(bitmap);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified file.
		///</summary>
		///<param name="file">The file to read the image from.</param>
		///<exception cref="MagickException"/>
		public MagickImage(FileInfo file)
			: this()
		{
			Read(file);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified file.
		///</summary>
		///<param name="file">The file to read the image from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		public MagickImage(FileInfo file, MagickReadSettings readSettings)
			: this()
		{
			Read(file, readSettings);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified width, height
		/// and color.
		///</summary>
		///<param name="color">The color to fill the image with.</param>
		///<param name="width">The width.</param>
		///<param name="height">The height.</param>
		public MagickImage(MagickColor color, int width, int height)
			: this()
		{
			Read(color, width, height);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class by creating a copy of the specified
		/// image.
		///</summary>
		///<param name="image">The image to create a copy of.</param>
		public MagickImage(MagickImage image)
			: this(new Wrapper.MagickImage(GetInstance(image)))
		{
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified stream.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		public MagickImage(Stream stream)
			: this()
		{
			Read(stream);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified stream.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		public MagickImage(Stream stream, MagickReadSettings readSettings)
			: this()
		{
			Read(stream, readSettings);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified filename.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		public MagickImage(string fileName)
			: this()
		{
			Read(fileName);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified filename
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<param name="width">The width.</param>
		///<param name="height">The height.</param>
		///<exception cref="MagickException"/>
		public MagickImage(string fileName, int width, int height)
			: this()
		{
			Read(fileName, width, height);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified filename
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		public MagickImage(string fileName, MagickReadSettings readSettings)
			: this()
		{
			Read(fileName, readSettings);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified MagickImage instances are considered equal.
		/// </summary>
		/// <param name="left">The first MagickImage to compare.</param>
		/// <param name="right"> The second MagickImage to compare.</param>
		/// <returns></returns>
		public static bool operator ==(MagickImage left, MagickImage right)
		{
			return object.Equals(left, right);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified MagickImage instances are not considered equal.
		/// </summary>
		/// <param name="left">The first MagickImage to compare.</param>
		/// <param name="right"> The second MagickImage to compare.</param>
		/// <returns></returns>
		public static bool operator !=(MagickImage left, MagickImage right)
		{
			return !object.Equals(left, right);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the first MagickImage is more than the second MagickImage.
		/// </summary>
		/// <param name="left">The first MagickImage to compare.</param>
		/// <param name="right"> The second MagickImage to compare.</param>
		/// <returns></returns>
		public static bool operator >(MagickImage left, MagickImage right)
		{
			if (ReferenceEquals(left, null))
				return ReferenceEquals(right, null);

			return left.CompareTo(right) == 1;
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the first MagickImage is less than the second MagickImage.
		/// </summary>
		/// <param name="left">The first MagickImage to compare.</param>
		/// <param name="right"> The second MagickImage to compare.</param>
		/// <returns></returns>
		public static bool operator <(MagickImage left, MagickImage right)
		{
			if (ReferenceEquals(left, null))
				return !ReferenceEquals(right, null);

			return left.CompareTo(right) == -1;
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the first MagickImage is more than or equal to the second MagickImage.
		/// </summary>
		/// <param name="left">The first MagickImage to compare.</param>
		/// <param name="right"> The second MagickImage to compare.</param>
		/// <returns></returns>
		public static bool operator >=(MagickImage left, MagickImage right)
		{
			if (ReferenceEquals(left, null))
				return ReferenceEquals(right, null);

			return left.CompareTo(right) >= 0;
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the first MagickImage is less than or equal to the second MagickImage.
		/// </summary>
		/// <param name="left">The first MagickImage to compare.</param>
		/// <param name="right"> The second MagickImage to compare.</param>
		/// <returns></returns>
		public static bool operator <=(MagickImage left, MagickImage right)
		{
			if (ReferenceEquals(left, null))
				return !ReferenceEquals(right, null);

			return left.CompareTo(right) <= 0;
		}
		///==========================================================================================
		///<summary>
		/// Converts this instance to a byte array.
		///</summary>
		public static explicit operator Byte[](MagickImage image)
		{
			Throw.IfNull("image", image);

			return image.ToByteArray();
		}
		///==========================================================================================
		///<summary>
		/// Event that will we raised when a warning is thrown by ImageMagick.
		///</summary>
		public event EventHandler<WarningEventArgs> Warning
		{
			add
			{
				_Instance.Warning += OnWarning;
				_WarningEvent += value;
			}
			remove
			{
				_Instance.Warning -= OnWarning;
				_WarningEvent -= value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Join images into a single multi-image file.
		///</summary>
		public bool Adjoin
		{
			get
			{
				return _Instance.Adjoin;
			}
			set
			{
				_Instance.Adjoin = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Transparent color.
		///</summary>
		public MagickColor AlphaColor
		{
			get
			{
				return MagickColor.Create(_Instance.AlphaColor);
			}
			set
			{
				_Instance.AlphaColor = MagickColor.GetInstance(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Time in 1/100ths of a second which must expire before splaying the next image in an
		/// animated sequence.
		///</summary>
		public int AnimationDelay
		{
			get
			{
				return _Instance.AnimationDelay;
			}
			set
			{
				_Instance.AnimationDelay = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Number of iterations to loop an animation (e.g. Netscape loop extension) for.
		///</summary>
		public int AnimationIterations
		{
			get
			{
				return _Instance.AnimationIterations;
			}
			set
			{
				_Instance.AnimationIterations = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Anti-alias Postscript and TrueType fonts (default true).
		///</summary>
		public bool AntiAlias
		{
			get
			{
				return _Instance.AntiAlias;
			}
			set
			{
				_Instance.AntiAlias = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The names of the artifacts.
		///</summary>
		public IEnumerable<string> ArtifactNames
		{
			get
			{
				return _Instance.ArtifactNames;
			}
		}
		///==========================================================================================
		///<summary>
		/// The names of the attributes.
		///</summary>
		public IEnumerable<string> AttributeNames
		{
			get
			{
				return _Instance.AttributeNames;
			}
		}
		///==========================================================================================
		///<summary>
		/// Background color of the image.
		///</summary>
		public MagickColor BackgroundColor
		{
			get
			{
				return MagickColor.Create(_Instance.BackgroundColor);
			}
			set
			{
				_Instance.BackgroundColor = MagickColor.GetInstance(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Height of the image before transformations.
		///</summary>
		public int BaseHeight
		{
			get
			{
				return _Instance.BaseHeight;
			}
		}
		///==========================================================================================
		///<summary>
		/// Width of the image before transformations.
		///</summary>
		public int BaseWidth
		{
			get
			{
				return _Instance.BaseWidth;
			}
		}
		///==========================================================================================
		///<summary>
		/// Use black point compensation.
		///</summary>
		public bool BlackPointCompensation
		{
			get
			{
				return _Instance.BlackPointCompensation;
			}
			set
			{
				_Instance.BlackPointCompensation = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Return smallest bounding box enclosing non-border pixels. The current fuzz value is used
		/// when discriminating between pixels.
		///</summary>
		public MagickGeometry BoundingBox
		{
			get
			{
				return MagickGeometry.Create(_Instance.BoundingBox);
			}
		}
		///==========================================================================================
		///<summary>
		/// Border color of the image.
		///</summary>
		public MagickColor BorderColor
		{
			get
			{
				return MagickColor.Create(_Instance.BorderColor);
			}
			set
			{
				_Instance.BorderColor = MagickColor.GetInstance(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Text bounding-box base color.
		///</summary>
		public MagickColor BoxColor
		{
			get
			{
				return MagickColor.Create(_Instance.BoxColor);
			}
			set
			{
				_Instance.BoxColor = MagickColor.GetInstance(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the channels of the image.
		///</summary>
		public IEnumerable<PixelChannel> Channels
		{
			get
			{
				return _Instance.Channels;
			}
		}
		///==========================================================================================
		///<summary>
		/// Image class (DirectClass or PseudoClass)
		/// NOTE: Setting a DirectClass image to PseudoClass will result in the loss of color information
		/// if the number of colors in the image is greater than the maximum palette size (either 256 (Q8)
		/// or 65536 (Q16).
		///</summary>
		public ClassType ClassType
		{
			get
			{
				return _Instance.ClassType;
			}
			set
			{
				_Instance.ClassType = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Colors within this distance are considered equal.
		///</summary>
		public Percentage ColorFuzz
		{
			get
			{
				return Percentage.FromQuantum(_Instance.ColorFuzz);
			}
			set
			{
				_Instance.ColorFuzz = value.ToQuantum();
			}
		}
		///==========================================================================================
		///<summary>
		/// Colormap size (number of colormap entries).
		///</summary>
		public int ColorMapSize
		{
			get
			{
				return _Instance.ColorMapSize;
			}
			set
			{
				_Instance.ColorMapSize = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Color space of the image.
		///</summary>
		public ColorSpace ColorSpace
		{
			get
			{
				return _Instance.ColorSpace;
			}
			set
			{
				_Instance.ColorSpace = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Color type of the image.
		///</summary>
		public ColorType ColorType
		{
			get
			{
				return _Instance.ColorType;
			}
			set
			{
				_Instance.ColorType = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Comment text of the image.
		///</summary>
		public string Comment
		{
			get
			{
				return _Instance.Comment;
			}
			set
			{
				_Instance.Comment = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Composition operator to be used when composition is implicitly used (such as for image flattening).
		///</summary>
		public CompositeOperator Compose
		{
			get
			{
				return _Instance.Compose;
			}
			set
			{
				_Instance.Compose = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Compression method to use.
		///</summary>
		public CompressionMethod CompressionMethod
		{
			get
			{
				return _Instance.CompressionMethod;
			}
			set
			{
				_Instance.CompressionMethod = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Enable printing of debug messages from ImageMagick when a debugger is attached.
		///</summary>
		public bool Debug
		{
			get
			{
				return _Instance.Debug;
			}
			set
			{
				_Instance.Debug = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Vertical and horizontal resolution in pixels of the image.
		///</summary>
		public PointD Density
		{
			get
			{
				return _Instance.Density;
			}
			set
			{
				_Instance.Density = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The depth (bits allocated to red/green/blue components).
		///</summary>
		public int Depth
		{
			get
			{
				return _Instance.Depth;
			}
			set
			{
				_Instance.Depth = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Preferred size of the image when encoding.
		///</summary>
		///<exception cref="MagickException"/>
		public MagickGeometry EncodingGeometry
		{
			get
			{
				return MagickGeometry.Create(_Instance.EncodingGeometry);
			}
		}
		///==========================================================================================
		///<summary>
		/// Endianness (little like Intel or big like SPARC) for image formats which support
		/// endian-specific options.
		///</summary>
		public Endian Endian
		{
			get
			{
				return _Instance.Endian;
			}
			set
			{
				_Instance.Endian = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Original file name of the image (only available if read from disk).
		///</summary>
		public string FileName
		{
			get
			{
				return _Instance.FileName;
			}
		}
		///==========================================================================================
		///<summary>
		/// Image file size.
		///</summary>
		public long FileSize
		{
			get
			{
				return _Instance.FileSize;
			}
		}
		///==========================================================================================
		///<summary>
		/// Color to use when drawing inside an object.
		///</summary>
		public MagickColor FillColor
		{
			get
			{
				return MagickColor.Create(_Instance.FillColor);
			}
			set
			{
				_Instance.FillColor = MagickColor.GetInstance(value);

				string colorName = ReferenceEquals(value, null) ? null : value.ToString();
				_Instance.SetOption("fill", colorName);
			}
		}
		///==========================================================================================
		///<summary>
		/// Pattern to use while filling drawn objects.
		///</summary>
		public MagickImage FillPattern
		{
			get
			{
				return Create(_Instance.FillPattern);
			}
			set
			{
				_Instance.FillPattern = GetInstance(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Rule to use when filling drawn objects.
		///</summary>
		public FillRule FillRule
		{
			get
			{
				return _Instance.FillRule;
			}
			set
			{
				_Instance.FillRule = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Filter to use when resizing image.
		///</summary>
		public FilterType FilterType
		{
			get
			{
				return _Instance.FilterType;
			}
			set
			{
				_Instance.FilterType = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// FlashPix viewing parameters.
		///</summary>
		public string FlashPixView
		{
			get
			{
				return _Instance.FlashPixView;
			}
			set
			{
				_Instance.FlashPixView = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Text rendering font.
		///</summary>
		public string Font
		{
			get
			{
				return _Instance.Font;
			}
			set
			{
				_Instance.Font = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Font point size.
		///</summary>
		public double FontPointsize
		{
			get
			{
				return _Instance.FontPointsize;
			}
			set
			{
				_Instance.FontPointsize = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The format of the image.
		///</summary>
		public MagickFormat Format
		{
			get
			{
				return _Instance.Format;
			}
			set
			{
				_Instance.Format = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The information about the format of the image.
		///</summary>
		public MagickFormatInfo FormatInfo
		{
			get
			{
				return MagickNET.GetFormatInformation(Format);
			}
		}
		///==========================================================================================
		///<summary>
		/// Gamma level of the image.
		///</summary>
		///<exception cref="MagickException"/>
		public double Gamma
		{
			get
			{
				return _Instance.Gamma;
			}
		}
		///==========================================================================================
		///<summary>
		/// Gif disposal method.
		///</summary>
		public GifDisposeMethod GifDisposeMethod
		{
			get
			{
				return _Instance.GifDisposeMethod;
			}
			set
			{
				_Instance.GifDisposeMethod = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Image supports transparency (alpha channel).
		///</summary>
		public bool HasAlpha
		{
			get
			{
				return _Instance.HasAlpha;
			}
			set
			{
				_Instance.HasAlpha = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Image contains a clipping path.
		///</summary>
		public bool HasClippingPath
		{
			get
			{
				return !string.IsNullOrEmpty(GetAttribute("8BIM:1999,2998:#1"));
			}
		}
		///==========================================================================================
		///<summary>
		/// Height of the image.
		///</summary>
		public int Height
		{
			get
			{
				return _Instance.Height;
			}
		}
		///==========================================================================================
		///<summary>
		/// Type of interlacing to use.
		///</summary>
		public Interlace Interlace
		{
			get
			{
				return _Instance.Interlace;
			}
			set
			{
				_Instance.Interlace = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Pixel color interpolate method to use.
		///</summary>
		public PixelInterpolateMethod Interpolate
		{
			get
			{
				return _Instance.Interpolate;
			}
			set
			{
				_Instance.Interpolate = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns true if none of the pixels in the image have an alpha value other than
		/// OpaqueAlpha (QuantumRange).
		///</summary>
		public bool IsOpaque
		{
			get
			{
				return _Instance.IsOpaque;
			}
		}
		///==========================================================================================
		///<summary>
		/// The label of the image.
		///</summary>
		public string Label
		{
			get
			{
				return _Instance.Label;
			}
			set
			{
				_Instance.Label = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Associate a mask with the image. The mask must be the same dimensions as the image. Pass
		/// null to unset an existing mask.
		///</summary>
		public MagickImage Mask
		{
			get
			{
				return Create(_Instance.Mask);
			}
			set
			{
				_Instance.Mask = GetInstance(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Photo orientation of the image.
		///</summary>
		public OrientationType Orientation
		{
			get
			{
				return _Instance.Orientation;
			}
			set
			{
				_Instance.Orientation = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Preferred size and location of an image canvas.
		///</summary>
		public MagickGeometry Page
		{
			get
			{
				return MagickGeometry.Create(_Instance.Page);
			}
			set
			{
				_Instance.Page = MagickGeometry.GetInstance(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Units of image resolution.
		///</summary>
		public Resolution ResolutionUnits
		{
			get
			{
				return _Instance.ResolutionUnits;
			}
			set
			{
				_Instance.ResolutionUnits = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The names of the profiles.
		///</summary>
		public IEnumerable<string> ProfileNames
		{
			get
			{
				return _Instance.ProfileNames;
			}
		}
		///==========================================================================================
		///<summary>
		/// JPEG/MIFF/PNG compression level (default 75).
		///</summary>
		public int Quality
		{
			get
			{
				return _Instance.Quality;
			}
			set
			{
				_Instance.Quality = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The type of rendering intent.
		///</summary>
		public RenderingIntent RenderingIntent
		{
			get
			{
				return _Instance.RenderingIntent;
			}
			set
			{
				_Instance.RenderingIntent = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The X resolution of the image.
		///</summary>
		public double ResolutionX
		{
			get
			{
				return _Instance.ResolutionX;
			}
		}
		///==========================================================================================
		///<summary>
		/// The Y resolution of the image.
		///</summary>
		public double ResolutionY
		{
			get
			{
				return _Instance.ResolutionY;
			}
		}
		///==========================================================================================
		///<summary>
		/// Enabled/disable stroke anti-aliasing.
		///</summary>
		public bool StrokeAntiAlias
		{
			get
			{
				return _Instance.StrokeAntiAlias;
			}
			set
			{
				_Instance.StrokeAntiAlias = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the signature of this image.
		///</summary>
		///<exception cref="MagickException"/>
		public string Signature
		{
			get
			{
				return _Instance.Signature;
			}
		}
		///==========================================================================================
		///<summary>
		/// Color to use when drawing object outlines.
		///</summary>
		public MagickColor StrokeColor
		{
			get
			{
				return MagickColor.Create(_Instance.StrokeColor);
			}
			set
			{
				_Instance.StrokeColor = MagickColor.GetInstance(value);

				string colorName = ReferenceEquals(value, null) ? null : value.ToString();
				_Instance.SetOption("stroke", colorName);
			}
		}
		///==========================================================================================
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
				return _Instance.StrokeDashArray;
			}
			set
			{
				_Instance.StrokeDashArray = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// While drawing using a dash pattern, specify distance into the dash pattern to start the
		/// dash (default 0).
		///</summary>
		public double StrokeDashOffset
		{
			get
			{
				return _Instance.StrokeDashOffset;
			}
			set
			{
				_Instance.StrokeDashOffset = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Specify the shape to be used at the end of open subpaths when they are stroked.
		///</summary>
		public LineCap StrokeLineCap
		{
			get
			{
				return _Instance.StrokeLineCap;
			}
			set
			{
				_Instance.StrokeLineCap = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Specify the shape to be used at the corners of paths (or other vector shapes) when they
		/// are stroked.
		///</summary>
		public LineJoin StrokeLineJoin
		{
			get
			{
				return _Instance.StrokeLineJoin;
			}
			set
			{
				_Instance.StrokeLineJoin = value;
			}
		}
		///==========================================================================================
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
				return _Instance.StrokeMiterLimit;
			}
			set
			{
				_Instance.StrokeMiterLimit = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Pattern image to use while stroking object outlines.
		///</summary>
		public MagickImage StrokePattern
		{
			get
			{
				return Create(_Instance.StrokePattern);
			}
			set
			{
				_Instance.StrokePattern = GetInstance(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Stroke width for drawing lines, circles, ellipses, etc.
		///</summary>
		public double StrokeWidth
		{
			get
			{
				return _Instance.StrokeWidth;
			}
			set
			{
				_Instance.StrokeWidth = value;

				_Instance.SetOption("strokeWidth", value.ToString(CultureInfo.InvariantCulture));
			}
		}
		///==========================================================================================
		///<summary>
		/// Render text right-to-left or left-to-right. 
		///</summary>
		public TextDirection TextDirection
		{
			get
			{
				return _Instance.TextDirection;
			}
			set
			{
				_Instance.TextDirection = value;

				_Instance.SetOption("direction", value.ToString());
			}
		}
		///==========================================================================================
		///<summary>
		/// Annotation text encoding (e.g. "UTF-16").
		///</summary>
		public Encoding TextEncoding
		{
			get
			{
				string encoding = _Instance.TextEncoding;

				if (string.IsNullOrEmpty(encoding))
					return null;

				try
				{
					return Encoding.GetEncoding(encoding);
				}
				catch (ArgumentException)
				{
					return null;
				}
			}
			set
			{
				string name = value != null ? value.WebName : null;

				_Instance.TextEncoding = name;

				_Instance.SetOption("encoding", name);
			}
		}
		///==========================================================================================
		///<summary>
		/// Annotation text gravity.
		///</summary>
		public Gravity TextGravity
		{
			get
			{
				return _Instance.TextGravity;
			}
			set
			{
				_Instance.TextGravity = value;

				_Instance.SetOption("gravity", value.ToString());
			}
		}
		///==========================================================================================
		///<summary>
		/// Text inter-line spacing.
		///</summary>
		public double TextInterlineSpacing
		{
			get
			{
				return _Instance.TextInterlineSpacing;
			}
			set
			{
				_Instance.TextInterlineSpacing = value;

				_Instance.SetOption("interline-spacing", value.ToString(CultureInfo.InvariantCulture));
			}
		}
		///==========================================================================================
		///<summary>
		/// Text inter-word spacing.
		///</summary>
		public double TextInterwordSpacing
		{
			get
			{
				return _Instance.TextInterwordSpacing;
			}
			set
			{
				_Instance.TextInterwordSpacing = value;

				_Instance.SetOption("interword-spacing", value.ToString(CultureInfo.InvariantCulture));
			}
		}
		///==========================================================================================
		///<summary>
		/// Text inter-character kerning.
		///</summary>
		public double TextKerning
		{
			get
			{
				return _Instance.TextKerning;
			}
			set
			{
				_Instance.TextKerning = value;

				_Instance.SetOption("kerning", value.ToString(CultureInfo.InvariantCulture));
			}
		}
		///==========================================================================================
		///<summary>
		/// Number of colors in the image.
		///</summary>
		public int TotalColors
		{
			get
			{
				return _Instance.TotalColors;
			}
		}
		///==========================================================================================
		///<summary>
		/// Turn verbose output on/off.
		///</summary>
		///==========================================================================================
		public bool Verbose
		{
			get
			{
				return _Instance.Verbose;
			}
			set
			{
				_Instance.Verbose = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Virtual pixel method.
		///</summary>
		public VirtualPixelMethod VirtualPixelMethod
		{
			get
			{
				return _Instance.VirtualPixelMethod;
			}
			set
			{
				_Instance.VirtualPixelMethod = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Width of the image.
		///</summary>
		public int Width
		{
			get
			{
				return _Instance.Width;
			}
		}
		///==========================================================================================
		///<summary>
		/// Adaptive-blur image with the default blur factor (0x1).
		///</summary>
		///<exception cref="MagickException"/>
		public void AdaptiveBlur()
		{
			AdaptiveBlur(0.0, 1.0);
		}
		///==========================================================================================
		///<summary>
		/// Adaptive-blur image with specified blur factor.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<exception cref="MagickException"/>
		public void AdaptiveBlur(double radius)
		{
			AdaptiveBlur(radius, 1.0);
		}
		///==========================================================================================
		///<summary>
		/// Adaptive-blur image with specified blur factor.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		public void AdaptiveBlur(double radius, double sigma)
		{
			_Instance.AdaptiveBlur(radius, sigma);
		}
		///==========================================================================================
		///<summary>
		/// Resize using mesh interpolation. It works well for small resizes of less than +/- 50%
		/// of the original image size. For larger resizing on images a full filtered and slower resize
		/// function should be used instead.
		///</summary>
		///<param name="width">The new width.</param>
		///<param name="height">The new height.</param>
		///<exception cref="MagickException"/>
		public void AdaptiveResize(int width, int height)
		{
			AdaptiveResize(new MagickGeometry(width, height));
		}
		///==========================================================================================
		///<summary>
		/// Resize using mesh interpolation. It works well for small resizes of less than +/- 50%
		/// of the original image size. For larger resizing on images a full filtered and slower resize
		/// function should be used instead.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		public void AdaptiveResize(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.AdaptiveResize(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Adaptively sharpens the image by sharpening more intensely near image edges and less
		/// intensely far from edges.
		///</summary>
		///<exception cref="MagickException"/>
		public void AdaptiveSharpen()
		{
			AdaptiveSharpen(0.0, 1.0);
		}
		///==========================================================================================
		///<summary>
		/// Adaptively sharpens the image by sharpening more intensely near image edges and less
		/// intensely far from edges.
		///</summary>
		///<param name="channels">The channel(s) that should be sharpened.</param>
		///<exception cref="MagickException"/>
		public void AdaptiveSharpen(Channels channels)
		{
			_Instance.AdaptiveSharpen(0.0, 1.0, channels);
		}
		///==========================================================================================
		///<summary>
		/// Adaptively sharpens the image by sharpening more intensely near image edges and less
		/// intensely far from edges.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		public void AdaptiveSharpen(double radius, double sigma)
		{
			_Instance.AdaptiveSharpen(radius, sigma);
		}
		///==========================================================================================
		///<summary>
		/// Adaptively sharpens the image by sharpening more intensely near image edges and less
		/// intensely far from edges.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="channels">The channel(s) that should be sharpened.</param>
		public void AdaptiveSharpen(double radius, double sigma, Channels channels)
		{
			_Instance.AdaptiveSharpen(radius, sigma, channels);
		}
		///==========================================================================================
		///<summary>
		/// Local adaptive threshold image.
		/// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm
		///</summary>
		///<param name="width">The width of the pixel neighborhood.</param>
		///<param name="height">The height of the pixel neighborhood.</param>
		///<exception cref="MagickException"/>
		public void AdaptiveThreshold(int width, int height)
		{
			AdaptiveThreshold(width, height, 0);
		}
		///==========================================================================================
		///<summary>
		/// Local adaptive threshold image.
		/// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm
		///</summary>
		///<param name="width">The width of the pixel neighborhood.</param>
		///<param name="height">The height of the pixel neighborhood.</param>
		///<param name="bias">Constant to subtract from pixel neighborhood mean (+/-)(0-QuantumRange).</param>
		///<exception cref="MagickException"/>
		public void AdaptiveThreshold(int width, int height, double bias)
		{
			_Instance.AdaptiveThreshold(width, height, bias);
		}
		///==========================================================================================
		///<summary>
		/// Local adaptive threshold image.
		/// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm
		///</summary>
		///<param name="width">The width of the pixel neighborhood.</param>
		///<param name="height">The height of the pixel neighborhood.</param>
		///<param name="biasPercentage">Constant to subtract from pixel neighborhood mean.</param>
		///<exception cref="MagickException"/>
		public void AdaptiveThreshold(int width, int height, Percentage biasPercentage)
		{
			AdaptiveThreshold(width, height, (double)biasPercentage.ToQuantum());
		}
		///==========================================================================================
		///<summary>
		/// Add noise to image with the specified noise type.
		///</summary>
		///<param name="noiseType">The type of noise that should be added to the image.</param>
		///<exception cref="MagickException"/>
		public void AddNoise(NoiseType noiseType)
		{
			_Instance.AddNoise(noiseType);
		}
		///==========================================================================================
		///<summary>
		/// Add noise to the specified channel of the image with the specified noise type.
		///</summary>
		///<param name="noiseType">The type of noise that should be added to the image.</param>
		///<param name="channels">The channel(s) where the noise should be added.</param>
		///<exception cref="MagickException"/>
		public void AddNoise(NoiseType noiseType, Channels channels)
		{
			_Instance.AddNoise(noiseType, channels);
		}
		///==========================================================================================
		///<summary>
		/// Adds the specified profile to the image or overwrites it.
		///</summary>
		///<param name="profile">The profile to add or overwrite.</param>
		///<exception cref="MagickException"/>
		public void AddProfile(ImageProfile profile)
		{
			AddProfile(profile, true);
		}
		///==========================================================================================
		///<summary>
		/// Adds the specified profile to the image or overwrites it when overWriteExisting is true.
		///</summary>
		///<param name="profile">The profile to add or overwrite.</param>
		///<param name="overwriteExisting">When set to false an existing profile with the same name
		/// won't be overwritten.</param>
		///<exception cref="MagickException"/>
		public void AddProfile(ImageProfile profile, bool overwriteExisting)
		{
			Throw.IfNull("profile", profile);

			if (!overwriteExisting && _Instance.GetProfile(profile.Name) != null)
				return;

			_Instance.AddProfile(profile.Name, profile.ToByteArray());
		}
		///==========================================================================================
		///<summary>
		/// Affine Transform image.
		///</summary>
		///<param name="affineMatrix">The affine matrix to use.</param>
		///<exception cref="MagickException"/>
		public void AffineTransform(DrawableAffine affineMatrix)
		{
			Throw.IfNull("affineMatrix", affineMatrix);

			_Instance.AffineTransform(affineMatrix);
		}
		///==========================================================================================
		///<summary>
		/// Applies the specified alpha option.
		///</summary>
		///<param name="option">The option to use.</param>
		///<exception cref="MagickException"/>
		public void Alpha(AlphaOption option)
		{
			_Instance.Alpha(option);
		}
		///==========================================================================================
		///<summary>
		/// Annotate using specified text, and bounding area.
		///</summary>
		///<param name="text">The text to use.</param>
		///<param name="boundingArea">The bounding area.</param>
		///<exception cref="MagickException"/>
		public void Annotate(string text, MagickGeometry boundingArea)
		{
			Annotate(text, boundingArea, Gravity.Northwest, 0.0);
		}
		///==========================================================================================
		///<summary>
		/// Annotate using specified text, bounding area, and placement gravity.
		///</summary>
		///<param name="text">The text to use.</param>
		///<param name="boundingArea">The bounding area.</param>
		///<param name="gravity">The placement gravity.</param>
		///<exception cref="MagickException"/>
		public void Annotate(string text, MagickGeometry boundingArea, Gravity gravity)
		{
			Annotate(text, boundingArea, gravity, 0.0);
		}
		///==========================================================================================
		///<summary>
		/// Annotate using specified text, bounding area, and placement gravity.
		///</summary>
		///<param name="text">The text to use.</param>
		///<param name="boundingArea">The bounding area.</param>
		///<param name="gravity">The placement gravity.</param>
		///<param name="degrees">The rotation.</param>
		///<exception cref="MagickException"/>
		public void Annotate(string text, MagickGeometry boundingArea, Gravity gravity, double degrees)
		{
			Throw.IfNullOrEmpty("text", text);
			Throw.IfNull("boundingArea", boundingArea);

			_Instance.Annotate(text, MagickGeometry.GetInstance(boundingArea), gravity, degrees);
		}
		///==========================================================================================
		///<summary>
		/// Annotate with text (bounding area is entire image) and placement gravity.
		///</summary>
		///<param name="text">The text to use.</param>
		///<param name="gravity">The placement gravity.</param>
		///<exception cref="MagickException"/>
		public void Annotate(string text, Gravity gravity)
		{
			Throw.IfNullOrEmpty("text", text);

			_Instance.Annotate(text, gravity);
		}
		///==========================================================================================
		///<summary>
		/// Extracts the 'mean' from the image and adjust the image to try make set its gamma 
		/// appropriatally.
		///</summary>
		///<exception cref="MagickException"/>
		public void AutoGamma()
		{
			_Instance.AutoGamma();
		}
		///==========================================================================================
		///<summary>
		/// Extracts the 'mean' from the image and adjust the image to try make set its gamma 
		/// appropriatally.
		///</summary>
		///<param name="channels">The channel(s) to set the gamma for.</param>
		///<exception cref="MagickException"/>
		public void AutoGamma(Channels channels)
		{
			_Instance.AutoGamma(channels);
		}
		///==========================================================================================
		///<summary>
		/// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
		/// to the full quantum range.
		///</summary>
		///<exception cref="MagickException"/>
		public void AutoLevel()
		{
			_Instance.AutoLevel();
		}
		///==========================================================================================
		///<summary>
		/// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
		/// to the full quantum range.
		///</summary>
		///<param name="channels">The channel(s) to level.</param>
		///<exception cref="MagickException"/>
		public void AutoLevel(Channels channels)
		{
			_Instance.AutoLevel(channels);
		}
		///==========================================================================================
		///<summary>
		/// Adjusts an image so that its orientation is suitable for viewing.
		///</summary>
		///<exception cref="MagickException"/>
		public void AutoOrient()
		{
			_Instance.AutoOrient();
		}
		///==========================================================================================
		///<summary>
		/// Calculates the bit depth (bits allocated to red/green/blue components). Use the Depth
		/// property to get the current value.
		///</summary>
		///<exception cref="MagickException"/>
		public int BitDepth()
		{
			return BitDepth(ImageMagick.Channels.Composite);
		}
		///==========================================================================================
		///<summary>
		/// Calculates the bit depth (bits allocated to red/green/blue components) of the specified channel.
		///</summary>
		///<param name="channels">The channel to get the depth for.</param>
		///<exception cref="MagickException"/>
		public int BitDepth(Channels channels)
		{
			return _Instance.BitDepth(channels);
		}
		///==========================================================================================
		///<summary>
		/// Set the bit depth (bits allocated to red/green/blue components) of the specified channel.
		///</summary>
		///<param name="value">The depth.</param>
		///<param name="channels">The channel to set the depth for.</param>
		///<exception cref="MagickException"/>
		public void BitDepth(Channels channels, int value)
		{
			_Instance.BitDepth(channels, value);
		}
		///==========================================================================================
		///<summary>
		/// Set the bit depth (bits allocated to red/green/blue components).
		///</summary>
		///<param name="value">The depth.</param>
		///<exception cref="MagickException"/>
		public void BitDepth(int value)
		{
			_Instance.BitDepth(ImageMagick.Channels.Composite, value);
		}
		///==========================================================================================
		///<summary>
		/// Forces all pixels below the threshold into black while leaving all pixels at or above
		/// the threshold unchanged.
		///</summary>
		///<param name="threshold">The threshold to use.</param>
		///<exception cref="MagickException"/>
		public void BlackThreshold(Percentage threshold)
		{
			Throw.IfNegative("threshold", threshold);

			_Instance.BlackThreshold(threshold.ToString());
		}
		///==========================================================================================
		///<summary>
		/// Forces all pixels below the threshold into black while leaving all pixels at or above
		/// the threshold unchanged.
		///</summary>
		///<param name="threshold">The threshold to use.</param>
		///<param name="channels">The channel(s) to make black.</param>
		///<exception cref="MagickException"/>
		public void BlackThreshold(Percentage threshold, Channels channels)
		{
			Throw.IfNegative("threshold", threshold);

			_Instance.BlackThreshold(threshold.ToString(), channels);
		}
		///==========================================================================================
		///<summary>
		/// Simulate a scene at nighttime in the moonlight.
		///</summary>
		///<exception cref="MagickException"/>
		public void BlueShift()
		{
			BlueShift(1.5);
		}
		///==========================================================================================
		///<summary>
		/// Simulate a scene at nighttime in the moonlight.
		///</summary>
		///<param name="factor">The factor to use.</param>
		///<exception cref="MagickException"/>
		public void BlueShift(double factor)
		{
			_Instance.BlueShift(factor);
		}
		///==========================================================================================
		///<summary>
		/// Blur image with the default blur factor (0x1).
		///</summary>
		///<exception cref="MagickException"/>
		public void Blur()
		{
			Blur(0.0, 1.0);
		}
		///==========================================================================================
		///<summary>
		/// Blur image the specified channel of the image with the default blur factor (0x1).
		///</summary>
		///<param name="channels">The channel(s) that should be blurred.</param>
		///<exception cref="MagickException"/>
		public void Blur(Channels channels)
		{
			Blur(0.0, 1.0, channels);
		}
		///==========================================================================================
		///<summary>
		/// Blur image with specified blur factor.
		///</summary>
		///<param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		public void Blur(double radius, double sigma)
		{
			_Instance.Blur(radius, sigma);
		}
		///==========================================================================================
		///<summary>
		/// Blur image with specified blur factor and channel.
		///</summary>
		///<param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="channels">The channel(s) that should be blurred.</param>
		///<exception cref="MagickException"/>
		public void Blur(double radius, double sigma, Channels channels)
		{
			_Instance.Blur(radius, sigma, channels);
		}
		///==========================================================================================
		///<summary>
		/// Border image (add border to image).
		///</summary>
		///<param name="size">The size of the border.</param>
		///<exception cref="MagickException"/>
		public void Border(int size)
		{
			Border(size, size);
		}
		///==========================================================================================
		///<summary>
		/// Border image (add border to image).
		///</summary>
		///<param name="width">The width of the border.</param>
		///<param name="height">The height of the border.</param>
		///<exception cref="MagickException"/>
		public void Border(int width, int height)
		{
			_Instance.Border(width, height);
		}
		///==========================================================================================
		///<summary>
		/// Changes the brightness and/or contrast of an image. It converts the brightness and
		/// contrast parameters into slope and intercept and calls a polynomical function to apply
		/// to the image.
		///</summary>
		///<param name="brightness">The brightness.</param>
		///<param name="contrast">The contrast.</param>
		///<exception cref="MagickException"/>
		public void BrightnessContrast(Percentage brightness, Percentage contrast)
		{
			_Instance.BrightnessContrast(brightness.ToDouble(), contrast.ToDouble());
		}
		///==========================================================================================
		///<summary>
		/// Changes the brightness and/or contrast of an image. It converts the brightness and
		/// contrast parameters into slope and intercept and calls a polynomical function to apply
		/// to the image.
		///</summary>
		///<param name="brightness">The brightness.</param>
		///<param name="contrast">The contrast.</param>
		///<param name="channels">The channel(s) that should be changed.</param>
		///<exception cref="MagickException"/>
		public void BrightnessContrast(Percentage brightness, Percentage contrast, Channels channels)
		{
			_Instance.BrightnessContrast(brightness.ToDouble(), contrast.ToDouble(), channels);
		}
		///==========================================================================================
		///<summary>
		/// Compares the current instance with another image. Only the size of the image is compared.
		///</summary>
		///<param name="other">The object to compare this image with.</param>
		public int CompareTo(MagickImage other)
		{
			if (ReferenceEquals(other, null))
				return 1;

			int left = (Width * Height);
			int right = (other.Width * other.Height);

			if (left == right)
				return 0;

			return left < right ? -1 : 1;
		}
		///==========================================================================================
		///<summary>
		/// Uses a multi-stage algorithm to detect a wide range of edges in images.
		///</summary>
		///<exception cref="MagickException"/>
		public void CannyEdge()
		{
			CannyEdge(0.0, 1.0, 10, 30);
		}
		///==========================================================================================
		///<summary>
		/// Uses a multi-stage algorithm to detect a wide range of edges in images.
		///</summary>
		///<param name="radius">The radius of the gaussian smoothing filter.</param>
		///<param name="sigma">The sigma of the gaussian smoothing filter.</param>
		///<param name="lower">Percentage of edge pixels in the lower threshold.</param>
		///<param name="upper">Percentage of edge pixels in the upper threshold.</param>
		///<exception cref="MagickException"/>
		public void CannyEdge(double radius, double sigma, Percentage lower, Percentage upper)
		{
			_Instance.CannyEdge(radius, sigma, lower.ToDouble() / 100, upper.ToDouble() / 100);
		}
		///==========================================================================================
		///<summary>
		/// Applies the color decision list from the specified ASC CDL file.
		///</summary>
		///<param name="fileName">The file to read the ASC CDL information from.</param>
		///<exception cref="MagickException"/>
		public void CDL(string fileName)
		{
			Throw.IfNull("fileName", fileName);

			String filePath = FileHelper.CheckForBaseDirectory(fileName);
			_Instance.CDL(filePath);
		}
		///==========================================================================================
		///<summary>
		/// Changes the ColorSpace of the image without applying a color profile.
		///</summary>
		public void ChangeColorSpace(ColorSpace value)
		{
			_Instance.ChangeColorSpace(value);
		}
		///==========================================================================================
		///<summary>
		/// Charcoal effect image (looks like charcoal sketch).
		///</summary>
		///<exception cref="MagickException"/>
		public void Charcoal()
		{
			Charcoal(0.0, 1.0);
		}
		///==========================================================================================
		///<summary>
		/// Charcoal effect image (looks like charcoal sketch).
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		public void Charcoal(double radius, double sigma)
		{
			_Instance.Charcoal(radius, sigma);
		}
		///==========================================================================================
		///<summary>
		/// Chop image (remove vertical and horizontal subregion of image).
		///</summary>
		///<param name="xOffset">The X offset from origin.</param>
		///<param name="width">The width of the part to chop horizontally.</param>
		///<param name="yOffset">The Y offset from origin.</param>
		///<param name="height">The height of the part to chop vertically.</param>
		///<exception cref="MagickException"/>
		public void Chop(int xOffset, int width, int yOffset, int height)
		{
			MagickGeometry geometry = new MagickGeometry(xOffset, yOffset, width, height);
			Chop(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Chop image (remove vertical or horizontal subregion of image) using the specified geometry.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		public void Chop(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Chop(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Chop image (remove horizontal subregion of image).
		///</summary>
		///<param name="offset">The X offset from origin.</param>
		///<param name="width">The width of the part to chop horizontally.</param>
		///<exception cref="MagickException"/>
		public void ChopHorizontal(int offset, int width)
		{
			MagickGeometry geometry = new MagickGeometry(offset, 0, width, 0);
			Chop(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Chop image (remove horizontal subregion of image).
		///</summary>
		///<param name="offset">The Y offset from origin.</param>
		///<param name="height">The height of the part to chop vertically.</param>
		///<exception cref="MagickException"/>
		public void ChopVertical(int offset, int height)
		{
			MagickGeometry geometry = new MagickGeometry(0, offset, 0, height);
			Chop(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Chromaticity blue primary point.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		public void ChromaBluePrimary(double x, double y)
		{
			_Instance.ChromaBluePrimary(x, y);
		}
		///==========================================================================================
		///<summary>
		/// Chromaticity green primary point.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		public void ChromaGreenPrimary(double x, double y)
		{
			_Instance.ChromaGreenPrimary(x, y);
		}
		///==========================================================================================
		///<summary>
		/// Chromaticity red primary point.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		public void ChromaRedPrimary(double x, double y)
		{
			_Instance.ChromaRedPrimary(x, y);
		}
		///==========================================================================================
		///<summary>
		/// Chromaticity red primary point.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		public void ChromaWhitePoint(double x, double y)
		{
			_Instance.ChromaWhitePoint(x, y);
		}
		///==========================================================================================
		///<summary>
		/// Set each pixel whose value is below zero to zero and any the pixel whose value is above
		/// the quantum range to the quantum range (e.g. 65535) otherwise the pixel value
		/// remains unchanged.
		///</summary>
		///<exception cref="MagickException"/>
		public void Clamp()
		{
			_Instance.Clamp();
		}
		///==========================================================================================
		///<summary>
		/// Set each pixel whose value is below zero to zero and any the pixel whose value is above
		/// the quantum range to the quantum range (e.g. 65535) otherwise the pixel value
		/// remains unchanged.
		///</summary>
		///<param name="channels">The channel(s) to clamp.</param>
		///<exception cref="MagickException"/>
		public void Clamp(Channels channels)
		{
			_Instance.Clamp(channels);
		}
		///==========================================================================================
		///<summary>
		/// Sets the image clip mask based on any clipping path information if it exists.
		///</summary>
		///<exception cref="MagickException"/>
		public void Clip()
		{
			_Instance.Clip();
		}
		///==========================================================================================
		///<summary>
		/// Sets the image clip mask based on any clipping path information if it exists.
		///</summary>
		///<param name="pathName">Name of clipping path resource. If name is preceded by #, use
		/// clipping path numbered by name.</param>
		///<param name="inside">Specifies if operations take effect inside or outside the clipping
		/// path</param>
		///<exception cref="MagickException"/>
		public void Clip(string pathName, bool inside)
		{
			Throw.IfNullOrEmpty("pathName", pathName);

			_Instance.Clip(pathName, inside);
		}
		///==========================================================================================
		///<summary>
		/// Creates a clone of the current image.
		///</summary>
		public MagickImage Clone()
		{
			return new MagickImage(_Instance.Clone());
		}
		///==========================================================================================
		///<summary>
		/// Creates a clone of the current image with the specified geometry.
		///</summary>
		///<param name="geometry">The area to clone.</param>
		public MagickImage Clone(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			MagickImage clone = new MagickImage("null:", geometry.Width, geometry.Height);
			clone._Instance.CopyPixels(_Instance, MagickGeometry.GetInstance(geometry), new Coordinate(0, 0));

			return clone;
		}
		///==========================================================================================
		///<summary>
		/// Creates a clone of the current image.
		///</summary>
		///<param name="width">The width of the area to clone</param>
		///<param name="height">The height of the area to clone</param>
		public MagickImage Clone(int width, int height)
		{
			return Clone(new MagickGeometry(width, height));
		}
		///==========================================================================================
		///<summary>
		/// Creates a clone of the current image.
		///</summary>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<param name="width">The width of the area to clone</param>
		///<param name="height">The height of the area to clone</param>
		public MagickImage Clone(int x, int y, int width, int height)
		{
			return Clone(new MagickGeometry(x, y, width, height));
		}
		///==========================================================================================
		///<summary>
		/// Apply a color lookup table (CLUT) to the image.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="method">Pixel interpolate method.</param>
		///<exception cref="MagickException"/>
		public void Clut(MagickImage image, PixelInterpolateMethod method)
		{
			Throw.IfNull("image", image);

			_Instance.Clut(GetInstance(image), method);
		}
		///==========================================================================================
		///<summary>
		/// Apply a color lookup table (CLUT) to the image.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="method">Pixel interpolate method.</param>
		///<param name="channels">The channel(s) to clut.</param>
		///<exception cref="MagickException"/>
		public void Clut(MagickImage image, PixelInterpolateMethod method, Channels channels)
		{
			Throw.IfNull("image", image);

			_Instance.Clut(GetInstance(image), method, channels);
		}
		///==========================================================================================
		///<summary>
		/// Sets the alpha channel to the specified color.
		///</summary>
		///<param name="color">The color to use.</param>
		///<exception cref="MagickException"/>
		public void ColorAlpha(MagickColor color)
		{
			Throw.IfNull("color", color);

			_Instance.ColorAlpha(MagickColor.GetInstance(color));
		}
		///==========================================================================================
		///<summary>
		/// Colorize image with the specified color, using specified percent alpha.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="alpha">The alpha percentage.</param>
		///<exception cref="MagickException"/>
		public void Colorize(MagickColor color, Percentage alpha)
		{
			Throw.IfNegative("alpha", alpha);

			Colorize(color, alpha, alpha, alpha);
		}
		///==========================================================================================
		///<summary>
		/// Colorize image with the specified color, using specified percent alpha for red, green,
		/// and blue quantums
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="alphaRed">The alpha percentage for red.</param>
		///<param name="alphaGreen">The alpha percentage for green.</param>
		///<param name="alphaBlue">The alpha percentage for blue.</param>
		///<exception cref="MagickException"/>
		public void Colorize(MagickColor color, Percentage alphaRed, Percentage alphaGreen, Percentage alphaBlue)
		{
			Throw.IfNull("color", color);
			Throw.IfNegative("alphaRed", alphaRed);
			Throw.IfNegative("alphaGreen", alphaGreen);
			Throw.IfNegative("alphaBlue", alphaBlue);

			_Instance.Colorize(MagickColor.GetInstance(color), alphaRed.ToInt32(), alphaGreen.ToInt32(), alphaBlue.ToInt32());
		}
		///==========================================================================================
		///<summary>
		/// Get color at colormap position index.
		///</summary>
		///<param name="index">The position index.</param>
		///<exception cref="MagickException"/>
		public MagickColor ColorMap(int index)
		{
			return MagickColor.Create(_Instance.ColorMap(index));
		}
		///==========================================================================================
		///<summary>
		/// Set color at colormap position index.
		///</summary>
		///<param name="index">The position index.</param>
		///<param name="color">The color.</param>
		///<exception cref="MagickException"/>
		public void ColorMap(int index, MagickColor color)
		{
			Throw.IfNull("color", color);

			_Instance.ColorMap(index, MagickColor.GetInstance(color));
		}
		///==========================================================================================
		///<summary>
		/// Apply a color matrix to the image channels.
		///</summary>
		///<param name="matrix">The color matrix to use.</param>
		///<exception cref="MagickException"/>
		public void ColorMatrix(ColorMatrix matrix)
		{
			Throw.IfNull("matrix", matrix);

			_Instance.ColorMatrix(matrix);
		}
		///==========================================================================================
		///<summary>
		/// Compare current image with another image and returns error information.
		///</summary>
		///<param name="image">The other image to compare with this image.</param>
		///<exception cref="MagickException"/>
		public MagickErrorInfo Compare(MagickImage image)
		{
			Throw.IfNull("image", image);

			return _Instance.Compare(GetInstance(image));
		}
		///==========================================================================================
		///<summary>
		/// Returns the distortion based on the specified metric.
		///</summary>
		///<param name="image">The other image to compare with this image.</param>
		///<param name="metric">The metric to use.</param>
		///<exception cref="MagickException"/>
		public double Compare(MagickImage image, ErrorMetric metric)
		{
			return Compare(image, metric, ImageMagick.Channels.Composite);
		}
		///==========================================================================================
		///<summary>
		/// Returns the distortion based on the specified metric.
		///</summary>
		///<param name="image">The other image to compare with this image.</param>
		///<param name="metric">The metric to use.</param>
		///<param name="channels">The channel(s) to compare.</param>
		///<exception cref="MagickException"/>
		public double Compare(MagickImage image, ErrorMetric metric, Channels channels)
		{
			Throw.IfNull("image", image);

			return _Instance.Compare(GetInstance(image), metric, channels);
		}
		///==========================================================================================
		///<summary>
		/// Returns the distortion based on the specified metric.
		///</summary>
		///<param name="image">The other image to compare with this image.</param>
		///<param name="metric">The metric to use.</param>
		///<param name="difference">The image that will contain the difference.</param>
		///<exception cref="MagickException"/>
		public double Compare(MagickImage image, ErrorMetric metric, MagickImage difference)
		{
			return Compare(image, metric, difference, ImageMagick.Channels.Composite);
		}
		///==========================================================================================
		///<summary>
		/// Returns the distortion based on the specified metric.
		///</summary>
		///<param name="image">The other image to compare with this image.</param>
		///<param name="metric">The metric to use.</param>
		///<param name="difference">The image that will contain the difference.</param>
		///<param name="channels">The channel(s) to compare.</param>
		///<exception cref="MagickException"/>
		public double Compare(MagickImage image, ErrorMetric metric, MagickImage difference, Channels channels)
		{
			Throw.IfNull("image", image);
			Throw.IfNull("difference", difference);

			return _Instance.Compare(GetInstance(image), metric, GetInstance(difference), channels);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="compose">The algorithm to use.</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, CompositeOperator compose)
		{
			Composite(image, 0, 0, compose);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the 'In' operator.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, int x, int y)
		{
			Composite(image, x, y, ImageMagick.CompositeOperator.In);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<param name="compose">The algorithm to use.</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, int x, int y, CompositeOperator compose)
		{
			Composite(image, x, y, compose, null);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<param name="compose">The algorithm to use.</param>
		///<param name="args">The arguments for the algorithm (compose:args).</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, int x, int y, CompositeOperator compose, string args)
		{
			Throw.IfNull("image", image);

			if (!string.IsNullOrEmpty(args))
				SetArtifact("compose:args", args);

			_Instance.Composite(GetInstance(image), x, y, compose);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the 'In' operator.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="offset">The offset from origin.</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, MagickGeometry offset)
		{
			Composite(image, offset, ImageMagick.CompositeOperator.In);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="offset">The offset from origin.</param>
		///<param name="compose">The algorithm to use.</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, MagickGeometry offset, CompositeOperator compose)
		{
			Composite(image, offset, compose, null);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="offset">The offset from origin.</param>
		///<param name="compose">The algorithm to use.</param>
		///<param name="args">The arguments for the algorithm (compose:args).</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, MagickGeometry offset, CompositeOperator compose, string args)
		{
			Throw.IfNull("image", image);
			Throw.IfNull("offset", offset);

			if (!string.IsNullOrEmpty(args))
				SetArtifact("compose:args", args);

			_Instance.Composite(GetInstance(image), MagickGeometry.GetInstance(offset), compose);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the 'In' operator.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="gravity">The placement gravity.</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, Gravity gravity)
		{
			Composite(image, gravity, ImageMagick.CompositeOperator.In);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="gravity">The placement gravity.</param>
		///<param name="compose">The algorithm to use.</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, Gravity gravity, CompositeOperator compose)
		{
			Composite(image, gravity, compose, null);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="gravity">The placement gravity.</param>
		///<param name="compose">The algorithm to use.</param>
		///<param name="args">The arguments for the algorithm (compose:args).</param>
		///<exception cref="MagickException"/>
		public void Composite(MagickImage image, Gravity gravity, CompositeOperator compose, string args)
		{
			Throw.IfNull("image", image);

			if (!string.IsNullOrEmpty(args))
				SetArtifact("compose:args", args);

			_Instance.Composite(GetInstance(image), gravity, compose);
		}
		///==========================================================================================
		///<summary>
		/// Determines the connected-components of the image
		///</summary>
		///<param name="connectivity">How many neighbors to visit, choose from 4 or 8.</param>
		///<exception cref="MagickException"/>
		public void ConnectedComponents(int connectivity)
		{
			_Instance.ConnectedComponents(connectivity);
		}
		///==========================================================================================
		///<summary>
		/// Contrast image (enhance intensity differences in image)
		///</summary>
		///<exception cref="MagickException"/>
		public void Contrast()
		{
			Contrast(true);
		}
		///==========================================================================================
		///<summary>
		/// Contrast image (enhance intensity differences in image)
		///</summary>
		///<param name="enhance">Use true to enhance the contrast and false to reduce the contrast.</param>
		///<exception cref="MagickException"/>
		public void Contrast(bool enhance)
		{
			_Instance.Contrast(enhance);
		}
		///==========================================================================================
		///<summary>
		/// A simple image enhancement technique that attempts to improve the contrast in an image by
		/// 'stretching' the range of intensity values it contains to span a desired range of values.
		/// It differs from the more sophisticated histogram equalization in that it can only apply a
		/// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
		///</summary>
		///<param name="blackPoint">The black point.</param>
		///<exception cref="MagickException"/>
		public void ContrastStretch(Percentage blackPoint)
		{
			ContrastStretch(blackPoint, blackPoint);
		}
		///==========================================================================================
		///<summary>
		/// A simple image enhancement technique that attempts to improve the contrast in an image by
		/// 'stretching' the range of intensity values it contains to span a desired range of values.
		/// It differs from the more sophisticated histogram equalization in that it can only apply a
		/// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
		///</summary>
		///<param name="blackPoint">The black point.</param>
		///<param name="whitePoint">The white point.</param>
		///<exception cref="MagickException"/>
		public void ContrastStretch(Percentage blackPoint, Percentage whitePoint)
		{
			Throw.IfNegative("blackPoint", blackPoint);
			Throw.IfNegative("whitePoint", whitePoint);

			PointD contrast = CalculateContrastStretch(blackPoint, whitePoint);
			_Instance.ContrastStretch(contrast);
		}
		///==========================================================================================
		///<summary>
		/// A simple image enhancement technique that attempts to improve the contrast in an image by
		/// 'stretching' the range of intensity values it contains to span a desired range of values.
		/// It differs from the more sophisticated histogram equalization in that it can only apply a
		/// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
		///</summary>
		///<param name="blackPoint">The black point.</param>
		///<param name="whitePoint">The white point.</param>
		///<param name="channels">The channel(s) to constrast stretch.</param>
		///<exception cref="MagickException"/>
		public void ContrastStretch(Percentage blackPoint, Percentage whitePoint, Channels channels)
		{
			Throw.IfNegative("blackPoint", blackPoint);
			Throw.IfNegative("whitePoint", whitePoint);

			PointD contrast = CalculateContrastStretch(blackPoint, whitePoint);
			_Instance.ContrastStretch(contrast, channels);
		}
		///==========================================================================================
		///<summary>
		/// Convolve image. Applies a user-specified convolution to the image.
		///</summary>
		///<exception cref="MagickException"/>
		public void Convolve(ConvolveMatrix convolveMatrix)
		{
			Throw.IfNull("convolveMatrix", convolveMatrix);

			_Instance.Convolve(convolveMatrix);
		}
		///==========================================================================================
		///<summary>
		/// Copies pixels from the source image as defined by the geometry the destination image at
		/// the specified offset.
		///</summary>
		///<exception cref="MagickException"/>
		public void CopyPixels(MagickImage source, MagickGeometry geometry, Coordinate offset)
		{
			Throw.IfNull("source", source);
			Throw.IfNull("geometry", geometry);

			_Instance.CopyPixels(MagickImage.GetInstance(source), MagickGeometry.GetInstance(geometry), offset);
		}
		///==========================================================================================
		///<summary>
		/// Crop image (subregion of original image). You should call RePage afterwards unless you
		/// need the Page information.
		///</summary>
		///<param name="geometry">The subregion to crop.</param>
		///<exception cref="MagickException"/>
		public void Crop(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Crop(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Crop image (subregion of original image) using CropPosition.Center. You should call
		/// RePage afterwards unless you need the Page information.
		///</summary>
		///<param name="width">The width of the subregion.</param>
		///<param name="height">The height of the subregion.</param>
		///<exception cref="MagickException"/>
		public void Crop(int width, int height)
		{
			Crop(width, height, Gravity.Center);
		}
		///==========================================================================================
		///<summary>
		/// Crop image (subregion of original image). You should call RePage afterwards unless you
		/// need the Page information.
		///</summary>
		///<param name="width">The width of the subregion.</param>
		///<param name="height">The height of the subregion.</param>
		///<param name="gravity">The position where the cropping should start from.</param>
		///<exception cref="MagickException"/>
		public void Crop(int width, int height, Gravity gravity)
		{
			int imageWidth = Width;
			int imageHeight = Height;

			int newWidth = width > imageWidth ? imageWidth : width;
			int newHeight = height > imageHeight ? imageHeight : height;

			if (newWidth == imageWidth && newHeight == imageHeight)
				return;

			MagickGeometry geometry = new MagickGeometry(newWidth, newHeight);
			switch (gravity)
			{
				case Gravity.North:
					geometry.X = (imageWidth - newWidth) / 2;
					break;
				case Gravity.Northeast:
					geometry.X = imageWidth - newWidth;
					break;
				case Gravity.East:
					geometry.X = imageWidth - newWidth;
					geometry.Y = (imageHeight - newHeight) / 2;
					break;
				case Gravity.Southeast:
					geometry.X = imageWidth - newWidth;
					geometry.Y = imageHeight - newHeight;
					break;
				case Gravity.South:
					geometry.X = (imageWidth - newWidth) / 2;
					geometry.Y = imageHeight - newHeight;
					break;
				case Gravity.Southwest:
					geometry.Y = imageHeight - newHeight;
					break;
				case Gravity.West:
					geometry.Y = (imageHeight - newHeight) / 2;
					break;
				case Gravity.Northwest:
					break;
				case Gravity.Center:
					geometry.X = (imageWidth - newWidth) / 2;
					geometry.Y = (imageHeight - newHeight) / 2;
					break;
			}

			Crop(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Creates tiles of the current image in the specified dimension.
		///</summary>
		///<param name="width">The width of the tile.</param>
		///<param name="height">The height of the tile.</param>
		public IEnumerable<MagickImage> CropToTiles(int width, int height)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			return CropToTiles(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Creates tiles of the current image in the specified dimension.
		///</summary>
		///<param name="geometry">The size of the tile.</param>
		public IEnumerable<MagickImage> CropToTiles(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			foreach (Wrapper.MagickImage image in _Instance.CropToTiles(MagickGeometry.GetInstance(geometry)))
			{
				yield return Create(image);
			}
		}
		///==========================================================================================
		///<summary>
		/// Displaces an image's colormap by a given number of positions.
		///</summary>
		///<param name="amount">Displace the colormap this amount.</param>
		///<exception cref="MagickException"/>
		public void CycleColormap(int amount)
		{
			_Instance.CycleColormap(amount);
		}
		///==========================================================================================
		///<summary>
		/// Converts cipher pixels to plain pixels.
		///</summary>
		///<param name="passphrase">The password that was used to encrypt the image.</param>
		///<exception cref="MagickException"/>
		public void Decipher(string passphrase)
		{
			Throw.IfNullOrEmpty("passphrase", passphrase);

			_Instance.Decipher(passphrase);
		}
		///==========================================================================================
		///<summary>
		/// Removes skew from the image. Skew is an artifact that occurs in scanned images because of
		/// the camera being misaligned, imperfections in the scanning or surface, or simply because
		/// the paper was not placed completely flat when scanned. The value of threshold ranges
		/// from 0 to QuantumRange.
		///</summary>
		///<param name="threshold">The threshold.</param>
		///<exception cref="MagickException"/>
		public void Deskew(Percentage threshold)
		{
			Throw.IfNegative("threshold", threshold);

			_Instance.Deskew(threshold.ToQuantum());
		}
		///==========================================================================================
		///<summary>
		/// Despeckle image (reduce speckle noise).
		///</summary>
		///<exception cref="MagickException"/>
		public void Despeckle()
		{
			_Instance.Despeckle();
		}
		///==========================================================================================
		///<summary>
		/// Determines the color type of the image. This method can be used to automatically make the
		/// type GrayScale.
		///</summary>
		///<exception cref="MagickException"/>
		public ColorType DetermineColorType()
		{
			return _Instance.DetermineColorType();
		}
		///==========================================================================================
		/// <summary>
		/// Disposes the MagickImage instance.
		/// </summary>
		public void Dispose()
		{
			DisposeInstance();
			GC.SuppressFinalize(this);
		}
		///==========================================================================================
		///<summary>
		/// Distorts an image using various distortion methods, by mapping color lookups of the source
		/// image to a new destination image of the same size as the source image.
		///</summary>
		///<param name="method">The distortion method to use.</param>
		///<param name="arguments">An array containing the arguments for the distortion.</param>
		///<exception cref="MagickException"/>
		public void Distort(DistortMethod method, params double[] arguments)
		{
			Distort(method, false, arguments);
		}
		///==========================================================================================
		///<summary>
		/// Distorts an image using various distortion methods, by mapping color lookups of the source
		/// image to a new destination image usually of the same size as the source image, unless
		/// 'bestfit' is set to true.
		///</summary>
		///<param name="method">The distortion method to use.</param>
		///<param name="bestfit">Attempt to 'bestfit' the size of the resulting image.</param>
		///<param name="arguments">An array containing the arguments for the distortion.</param>
		///<exception cref="MagickException"/>
		public void Distort(DistortMethod method, bool bestfit, params double[] arguments)
		{
			Throw.IfNullOrEmpty("arguments", arguments);

			_Instance.Distort(method, bestfit, arguments);
		}
		///==========================================================================================
		///<summary>
		/// Draw on image using one or more drawables.
		///</summary>
		///<param name="drawables">The drawable(s) to draw on the image.</param>
		///<exception cref="MagickException"/>
		public void Draw(params IDrawable[] drawables)
		{
			Draw((IEnumerable<IDrawable>)drawables);
		}
		///==========================================================================================
		///<summary>
		/// Draw on image using a collection of drawables.
		///</summary>
		///<param name="drawables">The drawables to draw on the image.</param>
		///<exception cref="MagickException"/>
		public void Draw(IEnumerable<IDrawable> drawables)
		{
			Throw.IfNull("drawables", drawables);

			_Instance.Draw(drawables);
		}
		///==========================================================================================
		///<summary>
		/// Edge image (hilight edges in image).
		///</summary>
		///<param name="radius">The radius of the pixel neighborhood.</param>
		///<exception cref="MagickException"/>
		public void Edge(double radius)
		{
			_Instance.Edge(radius);
		}
		///==========================================================================================
		///<summary>
		/// Emboss image (hilight edges with 3D effect) with default value (0x1).
		///</summary>
		///<exception cref="MagickException"/>
		public void Emboss()
		{
			Emboss(0.0, 1.0);
		}
		///==========================================================================================
		///<summary>
		/// Emboss image (hilight edges with 3D effect).
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		public void Emboss(double radius, double sigma)
		{
			_Instance.Emboss(radius, sigma);
		}
		///==========================================================================================
		///<summary>
		/// Converts pixels to cipher-pixels.
		///</summary>
		///<exception cref="MagickException"/>
		public void Encipher(string passphrase)
		{
			Throw.IfNullOrEmpty("passphrase", passphrase);

			_Instance.Encipher(passphrase);
		}
		///==========================================================================================
		///<summary>
		/// Applies a digital filter that improves the quality of a noisy image.
		///</summary>
		///<exception cref="MagickException"/>
		public void Enhance()
		{
			_Instance.Enhance();
		}
		///==========================================================================================
		///<summary>
		/// Applies a histogram equalization to the image.
		///</summary>
		///<exception cref="MagickException"/>
		public void Equalize()
		{
			_Instance.Equalize();
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current image.
		///</summary>
		///<param name="obj">The object to compare this image with.</param>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;

			return Equals(obj as MagickImage);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified image is equal to the current image.
		///</summary>
		///<param name="other">The image to compare this image with.</param>
		public bool Equals(MagickImage other)
		{
			if (ReferenceEquals(other, null))
				return false;

			return _Instance.Equals(other._Instance);
		}
		///==========================================================================================
		///<summary>
		/// Apply an arithmetic or bitwise operator to the image pixel quantums.
		///</summary>
		///<param name="channels">The channel(s) to apply the operator on.</param>
		///<param name="evaluateOperator">The operator.</param>
		///<param name="value">The value.</param>
		///<exception cref="MagickException"/>
		public void Evaluate(Channels channels, EvaluateOperator evaluateOperator, double value)
		{
			_Instance.Evaluate(channels, evaluateOperator, value);
		}
		///==========================================================================================
		///<summary>
		/// Apply an arithmetic or bitwise operator to the image pixel quantums.
		///</summary>
		///<param name="channels">The channel(s) to apply the operator on.</param>
		///<param name="geometry">The geometry to use.</param>
		///<param name="evaluateOperator">The operator.</param>
		///<param name="value">The value.</param>
		///<exception cref="MagickException"/>
		public void Evaluate(Channels channels, MagickGeometry geometry, EvaluateOperator evaluateOperator, double value)
		{
			Throw.IfNull("geometry", geometry);
			Throw.IfTrue("geometry", geometry.IsPercentage, "Percentage is not supported.");

			_Instance.Evaluate(channels, MagickGeometry.GetInstance(geometry), evaluateOperator, value);
		}
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the width and height.
		///</summary>
		///<param name="width">The width to extend the image to.</param>
		///<param name="height">The height to extend the image to.</param>
		///<exception cref="MagickException"/>
		public void Extent(int width, int height)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			Extent(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the width and height.
		///</summary>
		///<param name="width">The width to extend the image to.</param>
		///<param name="height">The height to extend the image to.</param>
		///<param name="backgroundColor">The background color to use.</param>
		///<exception cref="MagickException"/>
		public void Extent(int width, int height, MagickColor backgroundColor)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			Extent(geometry, backgroundColor);
		}
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the width and height.
		///</summary>
		///<param name="width">The width to extend the image to.</param>
		///<param name="height">The height to extend the image to.</param>
		///<param name="gravity">The placement gravity.</param>
		///<exception cref="MagickException"/>
		public void Extent(int width, int height, Gravity gravity)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			Extent(geometry, gravity);
		}
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the width and height.
		///</summary>
		///<param name="width">The width to extend the image to.</param>
		///<param name="height">The height to extend the image to.</param>
		///<param name="gravity">The placement gravity.</param>
		///<param name="backgroundColor">The background color to use.</param>
		///<exception cref="MagickException"/>
		public void Extent(int width, int height, Gravity gravity, MagickColor backgroundColor)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			Extent(geometry, gravity, backgroundColor);
		}
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the geometry.
		///</summary>
		///<param name="geometry">The geometry to extend the image to.</param>
		///<exception cref="MagickException"/>
		public void Extent(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Extent(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the geometry.
		///</summary>
		///<param name="geometry">The geometry to extend the image to.</param>
		///<param name="backgroundColor">The background color to use.</param>
		///<exception cref="MagickException"/>
		public void Extent(MagickGeometry geometry, MagickColor backgroundColor)
		{
			Throw.IfNull("geometry", geometry);
			Throw.IfNull("backgroundColor", backgroundColor);

			_Instance.Extent(MagickGeometry.GetInstance(geometry), MagickColor.GetInstance(backgroundColor));
		}
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the geometry.
		///</summary>
		///<param name="geometry">The geometry to extend the image to.</param>
		///<param name="gravity">The placement gravity.</param>
		///<exception cref="MagickException"/>
		public void Extent(MagickGeometry geometry, Gravity gravity)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Extent(MagickGeometry.GetInstance(geometry), gravity);
		}
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the geometry.
		///</summary>
		///<param name="geometry">The geometry to extend the image to.</param>
		///<param name="gravity">The placement gravity.</param>
		///<param name="backgroundColor">The background color to use.</param>
		///<exception cref="MagickException"/>
		public void Extent(MagickGeometry geometry, Gravity gravity, MagickColor backgroundColor)
		{
			Throw.IfNull("geometry", geometry);
			Throw.IfNull("backgroundColor", backgroundColor);

			_Instance.Extent(MagickGeometry.GetInstance(geometry), gravity, MagickColor.GetInstance(backgroundColor));
		}
		///==========================================================================================
		///<summary>
		/// Flip image (reflect each scanline in the vertical direction).
		///</summary>
		///<exception cref="MagickException"/>
		public void Flip()
		{
			_Instance.Flip();
		}
		///==========================================================================================
		///<summary>
		/// Floodfill pixels matching color (within fuzz factor) of target pixel(x,y) with replacement
		/// alpha value using method.
		///</summary>
		///<param name="alpha">The alpha to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<exception cref="MagickException"/>
		public void FloodFill(int alpha, int x, int y)
		{
			_Instance.FloodFill(alpha, x, y, false);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<exception cref="MagickException"/>
		public void FloodFill(MagickColor color, int x, int y)
		{
			FloodFill(color, x, y, false);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		public void FloodFill(MagickColor color, int x, int y, MagickColor borderColor)
		{
			FloodFill(color, x, y, borderColor, false);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="geometry">The position of the pixel.</param>
		///<exception cref="MagickException"/>
		public void FloodFill(MagickColor color, MagickGeometry geometry)
		{
			FloodFill(color, geometry, false);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill color across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="geometry">The position of the pixel.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		public void FloodFill(MagickColor color, MagickGeometry geometry, MagickColor borderColor)
		{
			FloodFill(color, geometry, borderColor, false);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<exception cref="MagickException"/>
		public void FloodFill(MagickImage image, int x, int y)
		{
			FloodFill(image, x, y, false);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		public void FloodFill(MagickImage image, int x, int y, MagickColor borderColor)
		{
			FloodFill(image, x, y, borderColor, false);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="geometry">The position of the pixel.</param>
		///<exception cref="MagickException"/>
		public void FloodFill(MagickImage image, MagickGeometry geometry)
		{
			FloodFill(image, geometry, false);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="geometry">The position of the pixel.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		public void FloodFill(MagickImage image, MagickGeometry geometry, MagickColor borderColor)
		{
			FloodFill(image, geometry, borderColor, false);
		}
		///==========================================================================================
		///<summary>
		/// Flop image (reflect each scanline in the horizontal direction).
		///</summary>
		///<exception cref="MagickException"/>
		public void Flop()
		{
			_Instance.Flop();
		}
		///==========================================================================================
		///<summary>
		/// Obtain font metrics for text string given current font, pointsize, and density settings.
		///</summary>
		///<param name="text">The text to get the font metrics for.</param>
		///<exception cref="MagickException"/>
		public TypeMetric FontTypeMetrics(string text)
		{
			return FontTypeMetrics(text, false);
		}
		///==========================================================================================
		///<summary>
		/// Obtain font metrics for text string given current font, pointsize, and density settings.
		///</summary>
		///<param name="text">The text to get the font metrics for.</param>
		///<param name="ignoreNewLines">Specifies if new lines should be ignored.</param>
		///<exception cref="MagickException"/>
		public TypeMetric FontTypeMetrics(string text, bool ignoreNewLines)
		{
			Throw.IfNullOrEmpty("text", text);

			return _Instance.FontTypeMetrics(text, ignoreNewLines);
		}
		///==========================================================================================
		///<summary>
		/// Formats the specified expression, more info here: http://www.imagemagick.org/script/escape.php.
		///</summary>
		///<exception cref="MagickException"/>
		public String FormatExpression(string expression)
		{
			Throw.IfNullOrEmpty("expression", expression);

			return _Instance.FormatExpression(expression);
		}
		///==========================================================================================
		///<summary>
		/// Frame image with the default geometry (25x25+6+6).
		///</summary>
		///<exception cref="MagickException"/>
		public void Frame()
		{
			Frame(new MagickGeometry(6, 6, 25, 25));
		}
		///==========================================================================================
		///<summary>
		/// Frame image with the specified geometry.
		///</summary>
		///<param name="geometry">The geometry of the frame.</param>
		///<exception cref="MagickException"/>
		public void Frame(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Frame(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Frame image with the specified with and height.
		///</summary>
		///<param name="width">The width of the frame.</param>
		///<param name="height">The height of the frame.</param>
		///<exception cref="MagickException"/>
		public void Frame(int width, int height)
		{
			Frame(new MagickGeometry(6, 6, width, height));
		}
		///==========================================================================================
		///<summary>
		/// Frame image with the specified with, height, innerBevel and outerBevel.
		///</summary>
		///<param name="width">The width of the frame.</param>
		///<param name="height">The height of the frame.</param>
		///<param name="innerBevel">The inner bevel of the frame.</param>
		///<param name="outerBevel">The outer bevel of the frame.</param>
		///<exception cref="MagickException"/>
		public void Frame(int width, int height, int innerBevel, int outerBevel)
		{
			Frame(new MagickGeometry(innerBevel, outerBevel, width, height));
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified base64 string.
		///</summary>
		///<param name="value">The base64 string to load the image from.</param>
		public static MagickImage FromBase64(string value)
		{
			Byte[] data = Convert.FromBase64String(value);
			return new MagickImage(data);
		}
		///==========================================================================================
		///<summary>
		/// Applies a mathematical expression to the image.
		///</summary>
		///<param name="expression">The expression to apply.</param>
		///<exception cref="MagickException"/>
		public void Fx(string expression)
		{
			Throw.IfNullOrEmpty("expression", expression);

			_Instance.Fx(expression);
		}
		///==========================================================================================
		///<summary>
		/// Applies a mathematical expression to the image.
		///</summary>
		///<param name="expression">The expression to apply.</param>
		///<param name="channels">The channel(s) to apply the expression to.</param>
		///<exception cref="MagickException"/>
		public void Fx(string expression, Channels channels)
		{
			Throw.IfNullOrEmpty("expression", expression);

			_Instance.Fx(expression, channels);
		}
		///==========================================================================================
		///<summary>
		/// Gamma correct image.
		///</summary>
		///<param name="gamma">The image gamma.</param>
		///<exception cref="MagickException"/>
		public void GammaCorrect(double gamma)
		{
			_Instance.GammaCorrect(gamma);
		}
		///==========================================================================================
		///<summary>
		/// Gamma correct image.
		///</summary>
		///<param name="gammaRed">The image gamma for the red channel.</param>
		///<param name="gammaGreen">The image gamma for the green channel.</param>
		///<param name="gammaBlue">The image gamma for the blue channel.</param>
		///<exception cref="MagickException"/>
		public void GammaCorrect(double gammaRed, double gammaGreen, double gammaBlue)
		{
			_Instance.GammaCorrect(gammaRed, gammaGreen, gammaBlue);
		}
		///==========================================================================================
		///<summary>
		/// Gaussian blur image.
		///</summary>
		///<param name="width">The number of neighbor pixels to be included in the convolution.</param>
		///<param name="sigma">The standard deviation of the gaussian bell curve.</param>
		///<exception cref="MagickException"/>
		public void GaussianBlur(double width, double sigma)
		{
			_Instance.GaussianBlur(width, sigma);
		}
		///==========================================================================================
		///<summary>
		/// Gaussian blur image.
		///</summary>
		///<param name="width">The number of neighbor pixels to be included in the convolution.</param>
		///<param name="sigma">The standard deviation of the gaussian bell curve.</param>
		///<param name="channels">The channel(s) to blur.</param>
		///<exception cref="MagickException"/>
		public void GaussianBlur(double width, double sigma, Channels channels)
		{
			_Instance.GaussianBlur(width, sigma, channels);
		}
		///==========================================================================================
		///<summary>
		/// Returns the value of the artifact with the specified name.
		///</summary>
		///<param name="name">The name of the artifact.</param>
		public string GetArtifact(string name)
		{
			Throw.IfNullOrEmpty("name", name);

			return _Instance.GetArtifact(name);
		}
		///==========================================================================================
		///<summary>
		/// Returns the value of a named image attribute.
		///</summary>
		///<param name="name">The name of the attribute.</param>
		public string GetAttribute(string name)
		{
			Throw.IfNullOrEmpty("name", name);

			return _Instance.GetAttribute(name);
		}
		///==========================================================================================
		///<summary>
		/// Retrieve the color profile from the image.
		///</summary>
		///<exception cref="MagickException"/>
		public ColorProfile GetColorProfile()
		{
			ColorProfile profile = GetColorProfile("icc");

			if (profile != null)
				return profile;

			return GetColorProfile("icm");
		}
		///==========================================================================================
		///<summary>
		/// Returns the value of a format-specific option.
		///</summary>
		///<param name="format">The format to get the option for.</param>
		///<param name="name">The name of the option.</param>
		public string GetDefine(MagickFormat format, string name)
		{
			Throw.IfNullOrEmpty("name", name);

			Tuple<string, string> define = ParseDefine(format, name);

			return _Instance.GetDefine(define.Item1, define.Item2);
		}
		///==========================================================================================
		///<summary>
		/// Retrieve the 8bim profile from the image.
		///</summary>
		///<exception cref="MagickException"/>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public EightBimProfile Get8BimProfile()
		{
			Byte[] data = _Instance.GetProfile("8bim");
			if (data == null)
				return null;

			return new EightBimProfile(this, data);
		}
		///==========================================================================================
		///<summary>
		/// Retrieve the exif profile from the image.
		///</summary>
		///<exception cref="MagickException"/>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public ExifProfile GetExifProfile()
		{
			Byte[] data = _Instance.GetProfile("exif");
			if (data == null)
				return null;

			return new ExifProfile(data);
		}
		///==========================================================================================
		///<summary>
		/// Serves as a hash of this type.
		///</summary>
		public override int GetHashCode()
		{
			return
				Width.GetHashCode() ^
				Height.GetHashCode() ^
				Signature.GetHashCode();
		}
		///==========================================================================================
		///<summary>
		/// Retrieve the iptc profile from the image.
		///</summary>
		///<exception cref="MagickException"/>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public IptcProfile GetIptcProfile()
		{
			Byte[] data = _Instance.GetProfile("iptc");
			if (data == null)
				return null;

			return new IptcProfile(data);
		}
		///==========================================================================================
		///<summary>
		/// Retrieve a named profile from the image.
		///</summary>
		///<param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
		///<exception cref="MagickException"/>
		public ImageProfile GetProfile(string name)
		{
			Throw.IfNullOrEmpty("name", name);

			Byte[] data = _Instance.GetProfile(name);
			if (data == null)
				return null;

			return new ImageProfile(name, data);
		}
		///==========================================================================================
		///<summary>
		/// Returns a read-only pixel collection that can be used to access the pixels of this image.
		///</summary>
		///<exception cref="MagickException"/>
		public PixelCollection GetReadOnlyPixels()
		{
			return GetReadOnlyPixels(0, 0, Width, Height);
		}
		///==========================================================================================
		///<summary>
		/// Returns a read-only pixel collection that can be used to access the pixels of this image.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="width">The width of the pixel area.</param>
		///<param name="height">The height of the pixel area.</param>
		///<exception cref="MagickException"/>
		public PixelCollection GetReadOnlyPixels(int x, int y, int width, int height)
		{
			return new PixelCollection(_Instance.GetReadOnlyPixels(x, y, width, height));
		}
		///==========================================================================================
		///<summary>
		/// Returns a writable pixel collection that can be used to access the pixels of this image.
		///</summary>
		///<exception cref="MagickException"/>
		public WritablePixelCollection GetWritablePixels()
		{
			return GetWritablePixels(0, 0, Width, Height);
		}
		///==========================================================================================
		///<summary>
		/// Returns a writable pixel collection that can be used to access the pixels of this image.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="width">The width of the pixel area.</param>
		///<param name="height">The height of the pixel area.</param>
		///<exception cref="MagickException"/>
		public WritablePixelCollection GetWritablePixels(int x, int y, int width, int height)
		{
			return new WritablePixelCollection(_Instance.GetWritablePixels(x, y, width, height));
		}
		///==========================================================================================
		///<summary>
		/// Retrieve the xmp profile from the image.
		///</summary>
		///<exception cref="MagickException"/>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public XmpProfile GetXmpProfile()
		{
			Byte[] data = _Instance.GetProfile("xmp");
			if (data == null)
				return null;

			return new XmpProfile(data);
		}
		///==========================================================================================
		///<summary>
		/// Converts the colors in the image to gray.
		///</summary>
		///<param name="method">The pixel intensity method to use.</param>
		///<exception cref="MagickException"/>
		public void Grayscale(PixelIntensityMethod method)
		{
			_Instance.Grayscale(method);
		}
		///==========================================================================================
		///<summary>
		/// Apply a color lookup table (Hald CLUT) to the image.
		///</summary>
		///<param name="image">The image to use.</param>
		///<exception cref="MagickException"/>
		public void HaldClut(MagickImage image)
		{
			Throw.IfNull("image", image);

			_Instance.HaldClut(GetInstance(image));
		}
		///==========================================================================================
		///<summary>
		/// Creates a color color histogram.
		///</summary>
		///<exception cref="MagickException"/>
		public Dictionary<MagickColor, int> Histogram()
		{
			Dictionary<MagickColor, int> result = new Dictionary<MagickColor, int>();

			foreach (Tuple<Wrapper.MagickColor, int> item in _Instance.Histogram())
			{
				result[MagickColor.Create(item.Item1)] = item.Item2;
			}

			return result;
		}
		///==========================================================================================
		///<summary>
		/// Identifies lines in the image.
		///</summary>
		///<exception cref="MagickException"/>
		public void HoughLine()
		{
			HoughLine(0, 0, 40);
		}
		///==========================================================================================
		///<summary>
		/// Identifies lines in the image.
		///</summary>
		///<param name="width">Find line pairs as local maxima in this neighborhood.</param>
		///<param name="height">Find line pairs as local maxima in this neighborhood.</param>
		///<param name="threshold">The line count threshold.</param>
		///<exception cref="MagickException"/>
		public void HoughLine(int width, int height, int threshold)
		{
			_Instance.HoughLine(width, height, threshold);
		}
		///==========================================================================================
		///<summary>
		/// Implode image (special effect).
		///</summary>
		///<param name="factor">The extent of the implosion.</param>
		///<exception cref="MagickException"/>
		public void Implode(double factor)
		{
			_Instance.Implode(factor);
		}
		///==========================================================================================
		///<summary>
		/// Floodfill pixels not matching color (within fuzz factor) of target pixel(x,y) with
		/// replacement alpha value using method.
		///</summary>
		///<param name="alpha">The alpha to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<exception cref="MagickException"/>
		public void InverseFloodFill(int alpha, int x, int y)
		{
			_Instance.FloodFill(alpha, x, y, true);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that do not match the color of the target pixel and are
		/// neighbors of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<exception cref="MagickException"/>
		public void InverseFloodFill(MagickColor color, int x, int y)
		{
			FloodFill(color, x, y, true);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that do not match the color of the target pixel and are
		/// neighbors of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		public void InverseFloodFill(MagickColor color, int x, int y, MagickColor borderColor)
		{
			FloodFill(color, x, y, borderColor, true);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="geometry">The position of the pixel.</param>
		///<exception cref="MagickException"/>
		public void InverseFloodFill(MagickColor color, MagickGeometry geometry)
		{
			FloodFill(color, geometry, true);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that do not match the color of the target pixel and are
		/// neighbors of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="geometry">The position of the pixel.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		public void InverseFloodFill(MagickColor color, MagickGeometry geometry, MagickColor borderColor)
		{
			FloodFill(color, geometry, borderColor, true);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that do not match the color of the target pixel and are
		/// neighbors of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<exception cref="MagickException"/>
		public void InverseFloodFill(MagickImage image, int x, int y)
		{
			FloodFill(image, x, y, true);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		public void InverseFloodFill(MagickImage image, int x, int y, MagickColor borderColor)
		{
			FloodFill(image, x, y, borderColor, true);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that do not match the color of the target pixel and are
		/// neighbors of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="geometry">The position of the pixel.</param>
		///<exception cref="MagickException"/>
		public void InverseFloodFill(MagickImage image, MagickGeometry geometry)
		{
			FloodFill(image, geometry, true);
		}
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that do not match the color of the target pixel and are
		/// neighbors of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="geometry">The position of the pixel.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		public void InverseFloodFill(MagickImage image, MagickGeometry geometry, MagickColor borderColor)
		{
			FloodFill(image, geometry, borderColor, true);
		}
		///==========================================================================================
		///<summary>
		/// Implements the inverse discrete Fourier transform (DFT) of the image as a magnitude phase.
		///</summary>
		///<param name="image">The image to use.</param>
		///<exception cref="MagickException"/>
		public void InverseFourierTransform(MagickImage image)
		{
			InverseFourierTransform(image, true);
		}
		///==========================================================================================
		///<summary>
		/// Implements the inverse discrete Fourier transform (DFT) of the image either as a magnitude
		/// phase or real / imaginary image pair.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="magnitude">Magnitude phase or real / imaginary image pair.</param>
		///<exception cref="MagickException"/>
		public void InverseFourierTransform(MagickImage image, bool magnitude)
		{
			Throw.IfNull("image", image);

			_Instance.InverseFourierTransform(GetInstance(image), magnitude);
		}
		///==========================================================================================
		///<summary>
		/// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
		/// level values on a channel by channel bases, as per level(). The given colors allows you to
		/// specify different level ranges for each of the color channels separately.
		///</summary>
		///<param name="blackColor">The color to map black to/from</param>
		///<param name="whiteColor">The color to map white to/from</param>
		///<exception cref="MagickException"/>
		public void InverseLevelColors(MagickColor blackColor, MagickColor whiteColor)
		{
			LevelColors(blackColor, whiteColor, true);
		}
		///==========================================================================================
		///<summary>
		/// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
		/// level values on a channel by channel bases, as per level(). The given colors allows you to
		/// specify different level ranges for each of the color channels separately.
		///</summary>
		///<param name="blackColor">The color to map black to/from</param>
		///<param name="whiteColor">The color to map white to/from</param>
		///<param name="channels">The channel(s) to level.</param>
		///<exception cref="MagickException"/>
		public void InverseLevelColors(MagickColor blackColor, MagickColor whiteColor, Channels channels)
		{
			LevelColors(blackColor, whiteColor, channels, true);
		}
		///==========================================================================================
		///<summary>
		/// Changes any pixel that does not match the target with the color defined by fill.
		///</summary>
		///<param name="target">The color to replace.</param>
		///<param name="fill">The color to replace opaque color with.</param>
		///<exception cref="MagickException"/>
		public void InverseOpaque(MagickColor target, MagickColor fill)
		{
			Opaque(target, fill, true);
		}
		///==========================================================================================
		///<summary>
		/// An edge preserving noise reduction filter.
		///</summary>
		///<exception cref="MagickException"/>
		public void Kuwahara()
		{
			Kuwahara(0.0, 1.0);
		}
		///==========================================================================================
		///<summary>
		/// An edge preserving noise reduction filter.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		public void Kuwahara(double radius, double sigma)
		{
			_Instance.Kuwahara(radius, sigma);
		}
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range. Uses a midpoint of 1.0.
		///</summary>
		///<param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<exception cref="MagickException"/>
#if Q16
		[CLSCompliant(false)]
#endif
		public void Level(QuantumType blackPoint, QuantumType whitePoint)
		{
			Level(blackPoint, whitePoint, 1.0);
		}
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range. Uses a midpoint of 1.0.
		///</summary>
		///<param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<exception cref="MagickException"/>
		public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage)
		{
			Level(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum());
		}
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range. Uses a midpoint of 1.0.
		///</summary>
		///<param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<param name="channels">The channel(s) to level.</param>
		///<exception cref="MagickException"/>
#if Q16
		[CLSCompliant(false)]
#endif
		public void Level(QuantumType blackPoint, QuantumType whitePoint, Channels channels)
		{
			Level(blackPoint, whitePoint, 1.0, channels);
		}
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range. Uses a midpoint of 1.0.
		///</summary>
		///<param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<param name="channels">The channel(s) to level.</param>
		///<exception cref="MagickException"/>
		public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, Channels channels)
		{
			Level(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum(), channels);
		}
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range.
		///</summary>
		///<param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
		///<exception cref="MagickException"/>
#if Q16
		[CLSCompliant(false)]
#endif
		public void Level(QuantumType blackPoint, QuantumType whitePoint, double midpoint)
		{
			_Instance.Level(blackPoint, whitePoint, midpoint);
		}
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range.
		///</summary>
		///<param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
		///<exception cref="MagickException"/>
		public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double midpoint)
		{
			Level(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum(), midpoint);
		}
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range.
		///</summary>
		///<param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
		///<param name="channels">The channel(s) to level.</param>
		///<exception cref="MagickException"/>
#if Q16
		[CLSCompliant(false)]
#endif
		public void Level(QuantumType blackPoint, QuantumType whitePoint, double midpoint, Channels channels)
		{
			_Instance.Level(blackPoint, whitePoint, midpoint, channels);
		}
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range.
		///</summary>
		///<param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
		///<param name="channels">The channel(s) to level.</param>
		///<exception cref="MagickException"/>
		public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double midpoint, Channels channels)
		{
			Level(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum(), midpoint, channels);
		}
		///==========================================================================================
		///<summary>
		/// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
		/// level values on a channel by channel bases, as per level(). The given colors allows you to
		/// specify different level ranges for each of the color channels separately.
		///</summary>
		///<param name="blackColor">The color to map black to/from</param>
		///<param name="whiteColor">The color to map white to/from</param>
		///<exception cref="MagickException"/>
		public void LevelColors(MagickColor blackColor, MagickColor whiteColor)
		{
			LevelColors(blackColor, whiteColor, false);
		}
		///==========================================================================================
		///<summary>
		/// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
		/// level values on a channel by channel bases, as per level(). The given colors allows you to
		/// specify different level ranges for each of the color channels separately.
		///</summary>
		///<param name="blackColor">The color to map black to/from</param>
		///<param name="whiteColor">The color to map white to/from</param>
		///<param name="channels">The channel(s) to level.</param>
		///<exception cref="MagickException"/>
		public void LevelColors(MagickColor blackColor, MagickColor whiteColor, Channels channels)
		{
			LevelColors(blackColor, whiteColor, channels, false);
		}
		///==========================================================================================
		///<summary>
		/// Discards any pixels below the black point and above the white point and levels the remaining pixels.
		///</summary>
		///<param name="blackPoint">The black point.</param>
		///<param name="whitePoint">The white point.</param>
		///<exception cref="MagickException"/>
		public void LinearStretch(Percentage blackPoint, Percentage whitePoint)
		{
			Throw.IfNegative("blackPoint", blackPoint);
			Throw.IfNegative("whitePoint", whitePoint);

			_Instance.LinearStretch(blackPoint.ToQuantum(), whitePoint.ToQuantum());
		}
		///==========================================================================================
		///<summary>
		/// Rescales image with seam carving.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		public void LiquidRescale(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.LiquidRescale(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Lower image (lighten or darken the edges of an image to give a 3-D lowered effect).
		///</summary>
		///<param name="size">The size of the edges.</param>
		///<exception cref="MagickException"/>
		public void Lower(int size)
		{
			_Instance.RaiseOrLower(size, false);
		}
		///==========================================================================================
		///<summary>
		/// Magnify image by integral size.
		///</summary>
		///<exception cref="MagickException"/>
		public void Magnify()
		{
			_Instance.Magnify();
		}
		///==========================================================================================
		///<summary>
		/// Remap image colors with closest color from reference image.
		///</summary>
		///<param name="image">The image to use.</param>
		///<exception cref="MagickException"/>
		public MagickErrorInfo Map(MagickImage image)
		{
			return Map(image, new QuantizeSettings());
		}
		///==========================================================================================
		///<summary>
		/// Remap image colors with closest color from reference image.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="settings">Quantize settings.</param>
		///<exception cref="MagickException"/>
		public MagickErrorInfo Map(MagickImage image, QuantizeSettings settings)
		{
			Throw.IfNull("image", image);
			Throw.IfNull("settings", settings);

			return _Instance.Map(MagickImage.GetInstance(image), settings);
		}
		///==========================================================================================
		///<summary>
		/// Filter image by replacing each pixel component with the median color in a circular neighborhood.
		///</summary>
		///<exception cref="MagickException"/>
		public void MedianFilter()
		{
			MedianFilter(0.0);
		}
		///==========================================================================================
		///<summary>
		/// Filter image by replacing each pixel component with the median color in a circular neighborhood.
		///</summary>
		///<param name="radius">The radius of the pixel neighborhood.</param>
		///<exception cref="MagickException"/>
		public void MedianFilter(double radius)
		{
			_Instance.MedianFilter(radius);
		}
		///==========================================================================================
		///<summary>
		/// Reduce image by integral size.
		///</summary>
		///<exception cref="MagickException"/>
		public void Minify()
		{
			_Instance.Minify();
		}
		///==========================================================================================
		///<summary>
		/// Modulate percent brightness of an image.
		///</summary>
		///<param name="brightness">The brightness percentage.</param>
		///<exception cref="MagickException"/>
		public void Modulate(Percentage brightness)
		{
			Modulate(brightness, new Percentage(100), new Percentage(100));
		}
		///==========================================================================================
		///<summary>
		/// Modulate percent saturation and brightness of an image.
		///</summary>
		///<param name="brightness">The brightness percentage.</param>
		///<param name="saturation">The saturation percentage.</param>
		///<exception cref="MagickException"/>
		public void Modulate(Percentage brightness, Percentage saturation)
		{
			Modulate(brightness, saturation, new Percentage(100));
		}
		///==========================================================================================
		///<summary>
		/// Modulate percent hue, saturation, and brightness of an image.
		///</summary>
		///<param name="brightness">The brightness percentage.</param>
		///<param name="saturation">The saturation percentage.</param>
		///<param name="hue">The hue percentage.</param>
		///<exception cref="MagickException"/>
		public void Modulate(Percentage brightness, Percentage saturation, Percentage hue)
		{
			Throw.IfNegative("brightness", brightness);
			Throw.IfNegative("saturation", saturation);
			Throw.IfNegative("hue", hue);

			_Instance.Modulate(brightness.ToDouble(), saturation.ToDouble(), hue.ToDouble());
		}
		///==========================================================================================
		///<summary>
		/// Returns the normalized moments of one or more image channels.
		///</summary>
		///<exception cref="MagickException"/>
		public Moments Moments()
		{
			return _Instance.Moments();
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="kernel">Built-in kernel.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, Kernel kernel)
		{
			Morphology(method, kernel, "");
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="kernel">Built-in kernel.</param>
		///<param name="channels">The channels to apply the kernel to.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, Kernel kernel, Channels channels)
		{
			Morphology(method, kernel, "", channels);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="kernel">Built-in kernel.</param>
		///<param name="channels">The channels to apply the kernel to.</param>
		///<param name="iterations">The number of iterations.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, Kernel kernel, Channels channels, int iterations)
		{
			Morphology(method, kernel, "", channels, iterations);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="kernel">Built-in kernel.</param>
		///<param name="iterations">The number of iterations.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, Kernel kernel, int iterations)
		{
			_Instance.Morphology(method, kernel, "", iterations);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="kernel">Built-in kernel.</param>
		///<param name="arguments">Kernel arguments.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, Kernel kernel, string arguments)
		{
			_Instance.Morphology(method, kernel, arguments);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="kernel">Built-in kernel.</param>
		///<param name="arguments">Kernel arguments.</param>
		///<param name="channels">The channels to apply the kernel to.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, Kernel kernel, string arguments, Channels channels)
		{
			_Instance.Morphology(method, kernel, arguments, channels);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="kernel">Built-in kernel.</param>
		///<param name="arguments">Kernel arguments.</param>
		///<param name="channels">The channels to apply the kernel to.</param>
		///<param name="iterations">The number of iterations.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, Kernel kernel, string arguments, Channels channels, int iterations)
		{
			_Instance.Morphology(method, kernel, arguments, channels, iterations);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="kernel">Built-in kernel.</param>
		///<param name="arguments">Kernel arguments.</param>
		///<param name="iterations">The number of iterations.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, Kernel kernel, string arguments, int iterations)
		{
			_Instance.Morphology(method, kernel, arguments, iterations);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="userKernel">User suplied kernel.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, string userKernel)
		{
			_Instance.Morphology(method, userKernel);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="userKernel">User suplied kernel.</param>
		///<param name="channels">The channels to apply the kernel to.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, string userKernel, Channels channels)
		{
			_Instance.Morphology(method, userKernel, channels);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="userKernel">User suplied kernel.</param>
		///<param name="channels">The channels to apply the kernel to.</param>
		///<param name="iterations">The number of iterations.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, string userKernel, Channels channels, int iterations)
		{
			_Instance.Morphology(method, userKernel, channels, iterations);
		}
		///==========================================================================================
		///<summary>
		/// Applies a kernel to the image according to the given mophology method.
		///</summary>
		///<param name="method">The morphology method.</param>
		///<param name="userKernel">User suplied kernel.</param>
		///<param name="iterations">The number of iterations.</param>
		///<exception cref="MagickException"/>
		public void Morphology(MorphologyMethod method, string userKernel, int iterations)
		{
			_Instance.Morphology(method, userKernel, iterations);
		}
		///==========================================================================================
		///<summary>
		/// Motion blur image with specified blur factor.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="angle">The angle the object appears to be comming from (zero degrees is from the right).</param>
		///<exception cref="MagickException"/>
		public void MotionBlur(double radius, double sigma, double angle)
		{
			_Instance.MotionBlur(radius, sigma, angle);
		}
		///==========================================================================================
		///<summary>
		/// Negate colors in image.
		///</summary>
		///<exception cref="MagickException"/>
		public void Negate()
		{
			Negate(false);
		}
		///==========================================================================================
		///<summary>
		/// Negate colors in image.
		///</summary>
		///<param name="onlyGrayscale">Use true to negate only the grayscale colors.</param>
		///<exception cref="MagickException"/>
		public void Negate(bool onlyGrayscale)
		{
			_Instance.Negate(onlyGrayscale);
		}
		///==========================================================================================
		///<summary>
		/// Negate colors in image for the specified channel.
		///</summary>
		///<param name="channels">The channel(s) that should be negated.</param>
		///<exception cref="MagickException"/>
		public void Negate(Channels channels)
		{
			Negate(channels, false);
		}
		///==========================================================================================
		///<summary>
		/// Negate colors in image for the specified channel.
		///</summary>
		///<param name="channels">The channel(s) that should be negated.</param>
		///<param name="onlyGrayscale">Use true to negate only the grayscale colors.</param>
		///<exception cref="MagickException"/>
		public void Negate(Channels channels, bool onlyGrayscale)
		{
			_Instance.Negate(channels, onlyGrayscale);
		}
		///==========================================================================================
		///<summary>
		/// Normalize image (increase contrast by normalizing the pixel values to span the full range
		/// of color values)
		///</summary>
		///<exception cref="MagickException"/>
		public void Normalize()
		{
			_Instance.Normalize();
		}
		///==========================================================================================
		///<summary>
		/// Oilpaint image (image looks like oil painting)
		///</summary>
		public void OilPaint()
		{
			OilPaint(3.0);
		}
		///==========================================================================================
		///<summary>
		/// Oilpaint image (image looks like oil painting)
		///</summary>
		///<param name="radius">The radius of the circular neighborhood.</param>
		///<exception cref="MagickException"/>
		public void OilPaint(double radius)
		{
			_Instance.OilPaint(radius);
		}
		///==========================================================================================
		///<summary>
		/// Changes any pixel that matches target with the color defined by fill.
		///</summary>
		///<param name="target">The color to replace.</param>
		///<param name="fill">The color to replace opaque color with.</param>
		///<exception cref="MagickException"/>
		public void Opaque(MagickColor target, MagickColor fill)
		{
			Opaque(target, fill, false);
		}
		///==========================================================================================
		///<summary>
		/// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
		/// multiple intensity levels.
		///</summary>
		///<param name="thresholdMap">A string containing the name of the threshold dither map to use,
		///followed by zero or more numbers representing the number of color levels tho dither between.</param>
		///<exception cref="MagickException"/>
		public void OrderedDither(string thresholdMap)
		{
			Throw.IfNullOrEmpty("thresholdMap", thresholdMap);

			_Instance.OrderedDither(thresholdMap);
		}
		///==========================================================================================
		///<summary>
		/// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
		/// multiple intensity levels.
		///</summary>
		///<param name="thresholdMap">A string containing the name of the threshold dither map to use,
		///followed by zero or more numbers representing the number of color levels tho dither between.</param>
		///<param name="channels">The channel(s) to dither.</param>
		///<exception cref="MagickException"/>
		public void OrderedDither(string thresholdMap, Channels channels)
		{
			Throw.IfNullOrEmpty("thresholdMap", thresholdMap);

			_Instance.OrderedDither(thresholdMap, channels);
		}
		///==========================================================================================
		///<summary>
		/// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
		/// otherwise the pixel value remains unchanged.
		///</summary>
		///<param name="epsilon">The epsilon threshold.</param>
		///<exception cref="MagickException"/>
		public void Perceptible(double epsilon)
		{
			_Instance.Perceptible(epsilon);
		}
		///==========================================================================================
		///<summary>
		/// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
		/// otherwise the pixel value remains unchanged.
		///</summary>
		///<param name="epsilon">The epsilon threshold.</param>
		///<param name="channels">The channel(s) to perceptible.</param>
		///<exception cref="MagickException"/>
		public void Perceptible(double epsilon, Channels channels)
		{
			_Instance.Perceptible(epsilon, channels);
		}
		///==========================================================================================
		///<summary>
		/// Returns the perceptual hash of this image.
		///</summary>
		///<exception cref="MagickException"/>
		public PerceptualHash PerceptualHash()
		{
			PerceptualHash hash = new PerceptualHash(_Instance.PerceptualHash());
			if (!hash.Isvalid)
				return null;

			return hash;
		}
		///==========================================================================================
		///<summary>
		/// Reads only metadata and not the pixel data.
		///</summary>
		///<param name="data">The byte array to read the information from.</param>
		///<exception cref="MagickException"/>
		public void Ping(Byte[] data)
		{
			_Instance.Ping(data);
		}
		///==========================================================================================
		///<summary>
		/// Reads only metadata and not the pixel data.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		public void Ping(string fileName)
		{
			_Instance.Ping(fileName);
		}
		///==========================================================================================
		///<summary>
		/// Reads only metadata and not the pixel data.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		public void Ping(Stream stream)
		{
			_Instance.Ping(stream);
		}
		///==========================================================================================
		///<summary>
		/// Simulates a Polaroid picture.
		///</summary>
		///<param name="caption">The caption to put on the image.</param>
		///<param name="angle">The angle of image.</param>
		///<param name="method">Pixel interpolate method.</param>
		///<exception cref="MagickException"/>
		public void Polaroid(string caption, double angle, PixelInterpolateMethod method)
		{
			Throw.IfNull("caption", caption);

			_Instance.Polaroid(caption, angle, method);
		}
		///==========================================================================================
		///<summary>
		/// Reduces the image to a limited number of colors for a "poster" effect.
		///</summary>
		///<param name="levels">Number of color levels allowed in each channel.</param>
		///<exception cref="MagickException"/>
		public void Posterize(int levels)
		{
			Posterize(levels, DitherMethod.No);
		}
		///==========================================================================================
		///<summary>
		/// Reduces the image to a limited number of colors for a "poster" effect.
		///</summary>
		///<param name="levels">Number of color levels allowed in each channel.</param>
		///<param name="method">Dither method to use.</param>
		///<exception cref="MagickException"/>
		public void Posterize(int levels, DitherMethod method)
		{
			_Instance.Posterize(levels, method);
		}
		///==========================================================================================
		///<summary>
		/// Reduces the image to a limited number of colors for a "poster" effect.
		///</summary>
		///<param name="levels">Number of color levels allowed in each channel.</param>
		///<param name="method">Dither method to use.</param>
		///<param name="channels">The channel(s) to posterize.</param>
		///<exception cref="MagickException"/>
		public void Posterize(int levels, DitherMethod method, Channels channels)
		{
			_Instance.Posterize(levels, method, channels);
		}
		///==========================================================================================
		///<summary>
		/// Reduces the image to a limited number of colors for a "poster" effect.
		///</summary>
		///<param name="levels">Number of color levels allowed in each channel.</param>
		///<param name="channels">The channel(s) to posterize.</param>
		///<exception cref="MagickException"/>
		public void Posterize(int levels, Channels channels)
		{
			Posterize(levels, DitherMethod.No, channels);
		}
		///==========================================================================================
		///<summary>
		/// Sets an internal option to preserve the color type.
		///</summary>
		///<exception cref="MagickException"/>
		public void PreserveColorType()
		{
			ColorType = ColorType;
		}
		///==========================================================================================
		///<summary>
		/// Quantize image (reduce number of colors).
		///</summary>
		///<param name="settings">Quantize settings.</param>
		///<exception cref="MagickException"/>
		public MagickErrorInfo Quantize(QuantizeSettings settings)
		{
			Throw.IfNull("settings", settings);

			return _Instance.Quantize(settings);
		}
		///==========================================================================================
		///<summary>
		/// Raise image (lighten or darken the edges of an image to give a 3-D raised effect).
		///</summary>
		///<param name="size">The size of the edges.</param>
		///<exception cref="MagickException"/>
		[SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
		public void Raise(int size)
		{
			_Instance.RaiseOrLower(size, true);
		}
		///==========================================================================================
		///<summary>
		/// Changes the value of individual pixels based on the intensity of each pixel compared to a
		/// random threshold. The result is a low-contrast, two color image.
		///</summary>
		///<param name="percentageLow">The low threshold.</param>
		///<param name="percentageHigh">The low threshold.</param>
		///<exception cref="MagickException"/>
		public void RandomThreshold(Percentage percentageLow, Percentage percentageHigh)
		{
			Throw.IfNegative("percentageLow", percentageLow);
			Throw.IfNegative("percentageHigh", percentageHigh);

			_Instance.RandomThreshold(percentageLow.ToQuantum(), percentageHigh.ToQuantum(), true);
		}
		///==========================================================================================
		///<summary>
		/// Changes the value of individual pixels based on the intensity of each pixel compared to a
		/// random threshold. The result is a low-contrast, two color image.
		///</summary>
		///<param name="percentageLow">The low threshold.</param>
		///<param name="percentageHigh">The low threshold.</param>
		///<param name="channels">The channel(s) to use.</param>
		///<exception cref="MagickException"/>
		public void RandomThreshold(Percentage percentageLow, Percentage percentageHigh, Channels channels)
		{
			Throw.IfNegative("percentageLow", percentageLow);
			Throw.IfNegative("percentageHigh", percentageHigh);

			_Instance.RandomThreshold(percentageLow.ToQuantum(), percentageHigh.ToQuantum(), channels, true);
		}
		///==========================================================================================
		///<summary>
		/// Changes the value of individual pixels based on the intensity of each pixel compared to a
		/// random threshold. The result is a low-contrast, two color image.
		///</summary>
		///<param name="low">The low threshold.</param>
		///<param name="high">The low threshold.</param>
		///<exception cref="MagickException"/>
#if Q16
		[CLSCompliant(false)]
#endif
		public void RandomThreshold(QuantumType low, QuantumType high)
		{
			_Instance.RandomThreshold(low, high, false);
		}
		///==========================================================================================
		///<summary>
		/// Changes the value of individual pixels based on the intensity of each pixel compared to a
		/// random threshold. The result is a low-contrast, two color image.
		///</summary>
		///<param name="low">The low threshold.</param>
		///<param name="high">The low threshold.</param>
		///<param name="channels">The channel(s) to use.</param>
		///<exception cref="MagickException"/>
#if Q16
		[CLSCompliant(false)]
#endif
		public void RandomThreshold(QuantumType low, QuantumType high, Channels channels)
		{
			_Instance.RandomThreshold(low, high, channels, false);
		}
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<exception cref="MagickException"/>
		public void Read(Byte[] data)
		{
			Read(data, null);
		}
		///==========================================================================================
		///<summary>
		/// Read single vector image frame.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		public void Read(Byte[] data, MagickReadSettings readSettings)
		{
			_Instance.Read(data, readSettings);
		}
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="bitmap">The bitmap to read the image from.</param>
		///<exception cref="MagickException"/>
		public void Read(Bitmap bitmap)
		{
			Throw.IfNull("bitmap", bitmap);

			using (MemoryStream memStream = new MemoryStream())
			{
				if (IsSupportedImageFormat(bitmap.RawFormat))
					bitmap.Save(memStream, bitmap.RawFormat);
				else
					bitmap.Save(memStream, ImageFormat.Bmp);

				memStream.Position = 0;
				Read(memStream);
			}
		}
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="file">The file to read the image from.</param>
		///<exception cref="MagickException"/>
		public void Read(FileInfo file)
		{
			Throw.IfNull("file", file);

			Read(file.FullName);
		}
		///==========================================================================================
		///<summary>
		/// Read single vector image frame.
		///</summary>
		///<param name="file">The file to read the image from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		public void Read(FileInfo file, MagickReadSettings readSettings)
		{
			Throw.IfNull("file", file);

			Read(file.FullName, readSettings);
		}
		///==========================================================================================
		///<summary>
		/// Read single vector image frame.
		///</summary>
		///<param name="color">The color to fill the image with.</param>
		///<param name="width">The width.</param>
		///<param name="height">The height.</param>
		///<exception cref="MagickException"/>
		public void Read(MagickColor color, int width, int height)
		{
			_Instance.Read(MagickColor.GetInstance(color), width, height);
		}
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		public void Read(Stream stream)
		{
			Read(stream, null);
		}
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		public void Read(Stream stream, MagickReadSettings readSettings)
		{
			_Instance.Read(stream, readSettings);
		}
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		public void Read(string fileName)
		{
			Read(fileName, null);
		}
		///==========================================================================================
		///<summary>
		/// Read single vector image frame.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<param name="width">The width.</param>
		///<param name="height">The height.</param>
		///<exception cref="MagickException"/>
		public void Read(string fileName, int width, int height)
		{
			string filePath = FileHelper.CheckForBaseDirectory(fileName);
			_Instance.Read(filePath, width, height);
		}
		///==========================================================================================
		///<summary>
		/// Read single vector image frame.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		public void Read(string fileName, MagickReadSettings readSettings)
		{
			string filePath = FileHelper.CheckForBaseDirectory(fileName);
			_Instance.Read(filePath, readSettings);
		}
		///==========================================================================================
		///<summary>
		/// Reduce noise in image using a noise peak elimination filter.
		///</summary>
		///<exception cref="MagickException"/>
		public void ReduceNoise()
		{
			ReduceNoise(3);
		}
		///==========================================================================================
		///<summary>
		/// Reduce noise in image using a noise peak elimination filter.
		///</summary>
		///<param name="order">The order to use.</param>
		///<exception cref="MagickException"/>
		public void ReduceNoise(int order)
		{
			_Instance.ReduceNoise(order);
		}
		///==========================================================================================
		///<summary>
		/// Removes the define with the specified name.
		///</summary>
		///<param name="format">The format to set the option for.</param>
		///<param name="name">The name of the option.</param>
		public void RemoveDefine(MagickFormat format, string name)
		{
			Throw.IfNullOrEmpty("name", name);

			Tuple<string, string> define = ParseDefine(format, name);

			_Instance.RemoveDefine(define.Item1, define.Item2);
		}
		///==========================================================================================
		///<summary>
		/// Remove a named profile from the image.
		///</summary>
		///<param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
		///<exception cref="MagickException"/>
		public void RemoveProfile(string name)
		{
			Throw.IfNullOrEmpty("name", name);

			_Instance.RemoveProfile(name);
		}
		///==========================================================================================
		///<summary>
		/// Resets the page property of this image.
		///</summary>
		///<exception cref="MagickException"/>
		public void RePage()
		{
			_Instance.RePage();
		}
		///==========================================================================================
		///<summary>
		/// Resize image in terms of its pixel size.
		///</summary>
		///<param name="resolutionX">The new X resolution.</param>
		///<param name="resolutionY">The new Y resolution.</param>
		///<exception cref="MagickException"/>
		public void Resample(double resolutionX, double resolutionY)
		{
			PointD density = new PointD(resolutionX, resolutionY);
			Resample(density);
		}
		///==========================================================================================
		///<summary>
		/// Resize image in terms of its pixel size.
		///</summary>
		///<param name="density">The density to use.</param>
		///<exception cref="MagickException"/>
		public void Resample(PointD density)
		{
			_Instance.Resample(density);
		}
		///==========================================================================================
		///<summary>
		/// Resize image to specified size.
		///</summary>
		///<param name="width">The new width.</param>
		///<param name="height">The new height.</param>
		///<exception cref="MagickException"/>
		public void Resize(int width, int height)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			Resize(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Resize image to specified geometry.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		public void Resize(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Resize(geometry._Instance);
		}
		///==========================================================================================
		///<summary>
		/// Resize image to specified percentage.
		///</summary>
		///<param name="percentage">The percentage.</param>
		///<exception cref="MagickException"/>
		public void Resize(Percentage percentage)
		{
			Resize(percentage, percentage);
		}
		///==========================================================================================
		///<summary>
		/// Resize image to specified percentage.
		///</summary>
		///<param name="percentageWidth">The percentage of the width.</param>
		///<param name="percentageHeight">The percentage of the height.</param>
		///<exception cref="MagickException"/>
		public void Resize(Percentage percentageWidth, Percentage percentageHeight)
		{
			Throw.IfNegative("percentageWidth", percentageWidth);
			Throw.IfNegative("percentageHeight", percentageHeight);

			MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
			Resize(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Roll image (rolls image vertically and horizontally).
		///</summary>
		///<param name="xOffset">The X offset from origin.</param>
		///<param name="yOffset">The Y offset from origin.</param>
		///<exception cref="MagickException"/>
		public void Roll(int xOffset, int yOffset)
		{
			_Instance.Roll(xOffset, yOffset);
		}
		///==========================================================================================
		///<summary>
		/// Rotate image counter-clockwise by specified number of degrees.
		///</summary>
		///<param name="degrees">The number of degrees to rotate.</param>
		///<exception cref="MagickException"/>
		public void Rotate(double degrees)
		{
			_Instance.Rotate(degrees);
		}
		///==========================================================================================
		///<summary>
		/// Rotational blur image.
		///</summary>
		///<param name="angle">The angle to use.</param>
		///<exception cref="MagickException"/>
		public void RotationalBlur(double angle)
		{
			_Instance.RotationalBlur(angle);
		}
		///==========================================================================================
		///<summary>
		/// Rotational blur image.
		///</summary>
		///<param name="angle">The angle to use.</param>
		///<param name="channels">The channel(s) to use.</param>
		///<exception cref="MagickException"/>
		public void RotationalBlur(double angle, Channels channels)
		{
			_Instance.RotationalBlur(angle, channels);
		}
		///==========================================================================================
		///<summary>
		/// Resize image by using pixel sampling algorithm.
		///</summary>
		///<param name="width">The new width.</param>
		///<param name="height">The new height.</param>
		///<exception cref="MagickException"/>
		public void Sample(int width, int height)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			Sample(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Resize image by using pixel sampling algorithm.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		public void Sample(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Sample(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Resize image by using pixel sampling algorithm to the specified percentage.
		///</summary>
		///<param name="percentage">The percentage.</param>
		///<exception cref="MagickException"/>
		public void Sample(Percentage percentage)
		{
			Sample(percentage, percentage);
		}
		///==========================================================================================
		///<summary>
		/// Resize image by using pixel sampling algorithm to the specified percentage.
		///</summary>
		///<param name="percentageWidth">The percentage of the width.</param>
		///<param name="percentageHeight">The percentage of the height.</param>
		///<exception cref="MagickException"/>
		public void Sample(Percentage percentageWidth, Percentage percentageHeight)
		{
			Throw.IfNegative("percentageWidth", percentageWidth);
			Throw.IfNegative("percentageHeight", percentageHeight);

			MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
			Sample(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Resize image by using simple ratio algorithm.
		///</summary>
		///<param name="width">The new width.</param>
		///<param name="height">The new height.</param>
		///<exception cref="MagickException"/>
		public void Scale(int width, int height)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			Scale(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Resize image by using simple ratio algorithm.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		public void Scale(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Scale(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Resize image by using simple ratio algorithm to the specified percentage.
		///</summary>
		///<param name="percentage">The percentage.</param>
		///<exception cref="MagickException"/>
		public void Scale(Percentage percentage)
		{
			Scale(percentage, percentage);
		}
		///==========================================================================================
		///<summary>
		/// Resize image by using simple ratio algorithm to the specified percentage.
		///</summary>
		///<param name="percentageWidth">The percentage of the width.</param>
		///<param name="percentageHeight">The percentage of the height.</param>
		///<exception cref="MagickException"/>
		public void Scale(Percentage percentageWidth, Percentage percentageHeight)
		{
			Throw.IfNegative("percentageWidth", percentageWidth);
			Throw.IfNegative("percentageHeight", percentageHeight);

			MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
			Scale(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Segment (coalesce similar image components) by analyzing the histograms of the color
		/// components and identifying units that are homogeneous with the fuzzy c-means technique.
		/// Also uses QuantizeColorSpace and Verbose image attributes.
		///</summary>
		///<exception cref="MagickException"/>
		public void Segment()
		{
			Segment(ColorSpace.Undefined, 1.0, 1.5);
		}
		///==========================================================================================
		///<summary>
		/// Segment (coalesce similar image components) by analyzing the histograms of the color
		/// components and identifying units that are homogeneous with the fuzzy c-means technique.
		/// Also uses QuantizeColorSpace and Verbose image attributes.
		///</summary>
		///<param name="quantizeColorSpace">Quantize colorspace</param>
		///<param name="clusterThreshold">This represents the minimum number of pixels contained in
		/// a hexahedra before it can be considered valid (expressed as a percentage).</param>
		///<param name="smoothingThreshold">The smoothing threshold eliminates noise in the second
		/// derivative of the histogram. As the value is increased, you can expect a smoother second
		/// derivative</param>
		///<exception cref="MagickException"/>
		public void Segment(ColorSpace quantizeColorSpace, double clusterThreshold, double smoothingThreshold)
		{
			_Instance.Segment(quantizeColorSpace, clusterThreshold, smoothingThreshold);
		}
		///==========================================================================================
		///<summary>
		/// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
		/// that sharpens everything with contrast above a certain threshold.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
		///<param name="threshold">Only pixels within this contrast threshold are included in the blur operation.</param>
		///<exception cref="MagickException"/>
		public void SelectiveBlur(double radius, double sigma, double threshold)
		{
			_Instance.SelectiveBlur(radius, sigma, threshold);
		}
		///==========================================================================================
		///<summary>
		/// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
		/// that sharpens everything with contrast above a certain threshold.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
		///<param name="threshold">Only pixels within this contrast threshold are included in the blur operation.</param>
		///<param name="channels">The channel(s) to blur.</param>
		///<exception cref="MagickException"/>
		public void SelectiveBlur(double radius, double sigma, double threshold, Channels channels)
		{
			_Instance.SelectiveBlur(radius, sigma, threshold, channels);
		}
		///==========================================================================================
		///<summary>
		/// Separates the channels from the image and returns it as grayscale images.
		///</summary>
		///<exception cref="MagickException"/>
		public IEnumerable<MagickImage> Separate()
		{
			return Separate(ImageMagick.Channels.All);
		}
		///==========================================================================================
		///<summary>
		/// Separates the specified channels from the image and returns it as grayscale images.
		///</summary>
		///<param name="channels">The channel(s) to separates.</param>
		///<exception cref="MagickException"/>
		public IEnumerable<MagickImage> Separate(Channels channels)
		{
			foreach (Wrapper.MagickImage image in _Instance.Separate(channels))
			{
				yield return MagickImage.Create(image);
			}
		}
		///==========================================================================================
		///<summary>
		/// Applies a special effect to the image, similar to the effect achieved in a photo darkroom
		/// by sepia toning.
		///</summary>
		///<exception cref="MagickException"/>
		public void SepiaTone()
		{
			SepiaTone(new Percentage(80));
		}
		///==========================================================================================
		///<summary>
		/// Applies a special effect to the image, similar to the effect achieved in a photo darkroom
		/// by sepia toning.
		///</summary>
		///<param name="threshold">The tone threshold.</param>
		///<exception cref="MagickException"/>
		public void SepiaTone(Percentage threshold)
		{
			_Instance.SepiaTone(threshold.ToQuantum());
		}
		///==========================================================================================
		///<summary>
		/// Inserts the artifact with the specified name and value into the artifact tree of the image.
		///</summary>
		///<param name="name">The name of the artifact.</param>
		///<param name="value">The value of the artifact.</param>
		///<exception cref="MagickException"/>
		public void SetArtifact(string name, string value)
		{
			Throw.IfNullOrEmpty("name", name);
			Throw.IfNull("value", value);

			_Instance.SetArtifact(name, value);
		}
		///==========================================================================================
		///<summary>
		/// Lessen (or intensify) when adding noise to an image.
		///</summary>
		///<param name="attenuate">The attenuate value.</param>
		public void SetAttenuate(double attenuate)
		{
			_Instance.SetAttenuate(attenuate);
		}
		///==========================================================================================
		///<summary>
		/// Sets a named image attribute.
		///</summary>
		///<param name="name">The name of the attribute.</param>
		///<param name="value">The value of the attribute.</param>
		///<exception cref="MagickException"/>
		public void SetAttribute(string name, string value)
		{
			Throw.IfNullOrEmpty("name", name);
			Throw.IfNull("value", value);

			_Instance.SetAttribute(name, value);
		}
		///==========================================================================================
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
		///==========================================================================================
		///<summary>
		/// Sets a format-specific option.
		///</summary>
		///<param name="format">The format to set the define for.</param>
		///<param name="name">The name of the define.</param>
		///<param name="value">The value of the define.</param>
		public void SetDefine(MagickFormat format, string name, string value)
		{
			Throw.IfNullOrEmpty("name", name);
			Throw.IfNull("value", value);

			Tuple<string, string> define = ParseDefine(format, name);

			_Instance.SetDefine(define.Item1, define.Item2, value);
		}
		///==========================================================================================
		///<summary>
		/// Sets format-specific options with the specified defines.
		///</summary>
		///<param name="defines">The defines to set.</param>
		public void SetDefines(IDefines defines)
		{
			Throw.IfNull("defines", defines);

			foreach (IDefine define in defines.Defines)
			{
				SetDefine(define.Format, define.Name, define.Value);
			}
		}
		///==========================================================================================
		///<summary>
		/// When comparing images, emphasize pixel differences with this color.
		///</summary>
		///<param name="color">The color.</param>
		public void SetHighlightColor(MagickColor color)
		{
			Throw.IfNull("color", color);

			_Instance.SetHighlightColor(MagickColor.GetInstance(color));
		}
		///==========================================================================================
		///<summary>
		/// When comparing images, de-emphasize pixel differences with this color.
		///</summary>
		///<param name="color">The color.</param>
		public void SetLowlightColor(MagickColor color)
		{
			Throw.IfNull("color", color);

			_Instance.SetLowlightColor(MagickColor.GetInstance(color));
		}
		///==========================================================================================
		///<summary>
		/// Shade image using distant light source.
		///</summary>
		///<exception cref="MagickException"/>
		public void Shade()
		{
			Shade(30, 30, false);
		}
		///==========================================================================================
		///<summary>
		/// Shade image using distant light source.
		///</summary>
		///<param name="azimuth">The light source direction.</param>
		///<param name="elevation">The light source direction.</param>
		///<param name="colorShading">Specify true to shade the intensity of each pixel.</param>
		///<exception cref="MagickException"/>
		public void Shade(double azimuth, double elevation, bool colorShading)
		{
			_Instance.Shade(azimuth, elevation, colorShading);
		}
		///==========================================================================================
		///<summary>
		/// Simulate an image shadow.
		///</summary>
		///<exception cref="MagickException"/>
		public void Shadow()
		{
			Shadow(5, 5, 0.5, 0.8);
		}
		///==========================================================================================
		///<summary>
		/// Simulate an image shadow.
		///</summary>
		///<param name="color">The color of the shadow.</param>
		///<exception cref="MagickException"/>
		public void Shadow(MagickColor color)
		{
			Shadow(5, 5, 0.5, 0.8, color);
		}
		///==========================================================================================
		///<summary>
		/// Simulate an image shadow.
		///</summary>
		///<param name="x">the shadow x-offset.</param>
		///<param name="y">the shadow y-offset.</param>
		///<param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
		///<param name="alpha">Transparency percentage.</param>
		///<exception cref="MagickException"/>
		public void Shadow(int x, int y, double sigma, Percentage alpha)
		{
			_Instance.Shadow(x, y, sigma, alpha.ToDouble());
		}
		///==========================================================================================
		///<summary>
		/// Simulate an image shadow.
		///</summary>
		///<param name="x">the shadow x-offset.</param>
		///<param name="y">the shadow y-offset.</param>
		///<param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
		///<param name="alpha">Transparency percentage.</param>
		///<param name="color">The color of the shadow.</param>
		///<exception cref="MagickException"/>
		public void Shadow(int x, int y, double sigma, Percentage alpha, MagickColor color)
		{
			Throw.IfNull("color", color);

			_Instance.Shadow(x, y, sigma, alpha.ToDouble(), MagickColor.GetInstance(color));
		}
		///==========================================================================================
		///<summary>
		/// Sharpen pixels in image.
		///</summary>
		///<exception cref="MagickException"/>
		public void Sharpen()
		{
			Sharpen(0.0, 1.0);
		}
		///==========================================================================================
		///<summary>
		/// Sharpen pixels in image.
		///</summary>
		///<param name="channels">The channel(s) that should be sharpened.</param>
		///<exception cref="MagickException"/>
		public void Sharpen(Channels channels)
		{
			Sharpen(0.0, 1.0, channels);
		}
		///==========================================================================================
		///<summary>
		/// Sharpen pixels in image.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		public void Sharpen(double radius, double sigma)
		{
			_Instance.Sharpen(radius, sigma);
		}
		///==========================================================================================
		///<summary>
		/// Sharpen pixels in image.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="channels">The channel(s) that should be sharpened.</param>
		public void Sharpen(double radius, double sigma, Channels channels)
		{
			_Instance.Sharpen(radius, sigma, channels);
		}
		///==========================================================================================
		///<summary>
		/// Shave pixels from image edges.
		///</summary>
		///<param name="leftRight">The number of pixels to shave left and right.</param>
		///<param name="topBottom">The number of pixels to shave top and bottom.</param>
		///<exception cref="MagickException"/>
		public void Shave(int leftRight, int topBottom)
		{
			_Instance.Shave(leftRight, topBottom);
		}
		///==========================================================================================
		///<summary>
		/// Shear image (create parallelogram by sliding image by X or Y axis).
		///</summary>
		///<param name="xAngle">Specifies the number of degrees to shear the image.</param>
		///<param name="yAngle">Specifies the number of degrees to shear the image.</param>
		///<exception cref="MagickException"/>
		public void Shear(double xAngle, double yAngle)
		{
			_Instance.Shear(xAngle, yAngle);
		}
		///==========================================================================================
		///<summary>
		/// adjust the image contrast with a non-linear sigmoidal contrast algorithm
		///</summary>
		///<param name="sharpen">Specifies if sharpening should be used.</param>
		///<param name="contrast">The contrast</param>
		///<exception cref="MagickException"/>
		public void SigmoidalContrast(bool sharpen, double contrast)
		{
			SigmoidalContrast(sharpen, contrast, Quantum.Max / 2.0);
		}
		///==========================================================================================
		///<summary>
		/// adjust the image contrast with a non-linear sigmoidal contrast algorithm
		///</summary>
		///<param name="sharpen">Specifies if sharpening should be used.</param>
		///<param name="contrast">The contrast to use.</param>
		///<param name="midpoint">The midpoint to use.</param>
		///<exception cref="MagickException"/>
		public void SigmoidalContrast(bool sharpen, double contrast, double midpoint)
		{
			_Instance.SigmoidalContrast(sharpen, contrast, midpoint);
		}
		///==========================================================================================
		///<summary>
		/// Simulates a pencil sketch.
		///</summary>
		///<exception cref="MagickException"/>
		public void Sketch()
		{
			Sketch(0.0, 1.0, 0.0);
		}
		///==========================================================================================
		///<summary>
		/// Simulates a pencil sketch. We convolve the image with a Gaussian operator of the given
		/// radius and standard deviation (sigma). For reasonable results, radius should be larger than sigma.
		/// Use a radius of 0 and sketch selects a suitable radius for you.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="angle">Apply the effect along this angle.</param>
		///<exception cref="MagickException"/>
		public void Sketch(double radius, double sigma, double angle)
		{
			_Instance.Sketch(radius, sigma, angle);
		}
		///==========================================================================================
		///<summary>
		/// Solarize image (similar to effect seen when exposing a photographic film to light during
		/// the development process)
		///</summary>
		///<exception cref="MagickException"/>
		public void Solarize()
		{
			Solarize(50.0);
		}
		///==========================================================================================
		///<summary>
		/// Solarize image (similar to effect seen when exposing a photographic film to light during
		/// the development process)
		///</summary>
		///<param name="factor">The factor to use.</param>
		///<exception cref="MagickException"/>
		public void Solarize(double factor)
		{
			_Instance.Solarize(factor);
		}
		///==========================================================================================
		///<summary>
		/// Sparse color image, given a set of coordinates, interpolates the colors found at those
		/// coordinates, across the whole image, using various methods.
		///</summary>
		///<param name="method">The sparse color method to use.</param>
		///<param name="args">The sparse color arguments.</param>
		///<exception cref="MagickException"/>
		public void SparseColor(SparseColorMethod method, IEnumerable<SparseColorArg> args)
		{
			SparseColor(ImageMagick.Channels.Composite, method, args);
		}
		///==========================================================================================
		///<summary>
		/// Sparse color image, given a set of coordinates, interpolates the colors found at those
		/// coordinates, across the whole image, using various methods.
		///</summary>
		///<param name="channels">The channel(s) to use.</param>
		///<param name="method">The sparse color method to use.</param>
		///<param name="args">The sparse color arguments.</param>
		///<exception cref="MagickException"/>
		public void SparseColor(Channels channels, SparseColorMethod method, IEnumerable<SparseColorArg> args)
		{
			Throw.IfNull("args", args);
#if NET20
			Collection<Internal.ISparseColorArg> newArgs = new Collection<Internal.ISparseColorArg>();
			foreach (SparseColorArg arg in args)
			{
				newArgs.Add((Internal.ISparseColorArg)arg);
			}

			_Instance.SparseColor(channels, method, newArgs);
#else
			_Instance.SparseColor(channels, method, (IEnumerable<Internal.ISparseColorArg>)args);
#endif
		}
		///==========================================================================================
		///<summary>
		/// Splice the background color into the image.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		public void Splice(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Splice(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Spread pixels randomly within image.
		///</summary>
		///<exception cref="MagickException"/>
		public void Spread()
		{
			Spread(3);
		}
		///==========================================================================================
		///<summary>
		/// Spread pixels randomly within image by specified amount.
		///</summary>
		///<exception cref="MagickException"/>
		public void Spread(int amount)
		{
			_Instance.Spread(amount);
		}
		///==========================================================================================
		///<summary>
		/// Returns image statistics.
		///</summary>
		///<exception cref="MagickException"/>
		public Statistics Statistics()
		{
			return _Instance.Statistics();
		}
		///==========================================================================================
		///<summary>
		/// Add a digital watermark to the image (based on second image)
		///</summary>
		///<param name="watermark">The image to use as a watermark.</param>
		///<exception cref="MagickException"/>
		public void Stegano(MagickImage watermark)
		{
			Throw.IfNull("watermark", watermark);

			_Instance.Stegano(GetInstance(watermark));
		}
		///==========================================================================================
		///<summary>
		/// Create an image which appears in stereo when viewed with red-blue glasses (Red image on
		/// left, blue on right)
		///</summary>
		///<param name="rightImage">The image to use as the right part of the resulting image.</param>
		///<exception cref="MagickException"/>
		public void Stereo(MagickImage rightImage)
		{
			Throw.IfNull("rightImage", rightImage);

			_Instance.Stereo(GetInstance(rightImage));
		}
		///==========================================================================================
		///<summary>
		/// Strips an image of all profiles and comments.
		///</summary>
		///<exception cref="MagickException"/>
		public void Strip()
		{
			_Instance.Strip();
		}
		///==========================================================================================
		///<summary>
		/// Search for the specified image at EVERY possible location in this image. This is slow!
		/// very very slow.. It returns a similarity image such that an exact match location is
		/// completely white and if none of the pixels match, black, otherwise some gray level in-between.
		///</summary>
		///<param name="image">The image to search for.</param>
		///<exception cref="MagickException"/>
		public MagickSearchResult SubImageSearch(MagickImage image)
		{
			return SubImageSearch(image, ErrorMetric.RootMeanSquared, -1);
		}
		///==========================================================================================
		///<summary>
		/// Search for the specified image at EVERY possible location in this image. This is slow!
		/// very very slow.. It returns a similarity image such that an exact match location is
		/// completely white and if none of the pixels match, black, otherwise some gray level in-between.
		///</summary>
		///<param name="image">The image to search for.</param>
		///<param name="metric">The metric to use.</param>
		///<exception cref="MagickException"/>
		public MagickSearchResult SubImageSearch(MagickImage image, ErrorMetric metric)
		{
			return SubImageSearch(image, metric, -1);
		}
		///==========================================================================================
		///<summary>
		/// Search for the specified image at EVERY possible location in this image. This is slow!
		/// very very slow.. It returns a similarity image such that an exact match location is
		/// completely white and if none of the pixels match, black, otherwise some gray level in-between.
		///</summary>
		///<param name="image">The image to search for.</param>
		///<param name="metric">The metric to use.</param>
		///<param name="similarityThreshold">Minimum distortion for (sub)image match.</param>
		///<exception cref="MagickException"/>
		public MagickSearchResult SubImageSearch(MagickImage image, ErrorMetric metric, double similarityThreshold)
		{
			Throw.IfNull("image", image);

			return new MagickSearchResult(_Instance.SubImageSearch(GetInstance(image), metric, similarityThreshold));
		}
		///==========================================================================================
		///<summary>
		/// Swirl image (image pixels are rotated by degrees).
		///</summary>
		///<param name="degrees">The number of degrees.</param>
		///<exception cref="MagickException"/>
		public void Swirl(double degrees)
		{
			_Instance.Swirl(degrees);
		}
		///==========================================================================================
		///<summary>
		/// Channel a texture on image background.
		///</summary>
		///<param name="image">The image to use as a texture on image background.</param>
		///<exception cref="MagickException"/>
		public void Texture(MagickImage image)
		{
			Throw.IfNull("image", image);

			_Instance.Texture(GetInstance(image));
		}
		///==========================================================================================
		///<summary>
		/// Threshold image.
		///</summary>
		///<param name="percentage">The threshold percentage.</param>
		///<exception cref="MagickException"/>
		public void Threshold(Percentage percentage)
		{
			_Instance.Threshold(percentage.ToQuantum());
		}
		///==========================================================================================
		///<summary>
		/// Resize image to thumbnail size.
		///</summary>
		///<param name="width">The new width.</param>
		///<param name="height">The new height.</param>
		///<exception cref="MagickException"/>
		public void Thumbnail(int width, int height)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			Thumbnail(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Resize image to thumbnail size.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		public void Thumbnail(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Thumbnail(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Resize image to thumbnail size.
		///</summary>
		///<param name="percentage">The percentage.</param>
		///<exception cref="MagickException"/>
		public void Thumbnail(Percentage percentage)
		{
			Thumbnail(percentage, percentage);
		}
		///==========================================================================================
		///<summary>
		/// Resize image to thumbnail size.
		///</summary>
		///<param name="percentageWidth">The percentage of the width.</param>
		///<param name="percentageHeight">The percentage of the height.</param>
		///<exception cref="MagickException"/>
		public void Thumbnail(Percentage percentageWidth, Percentage percentageHeight)
		{
			Throw.IfNegative("percentageWidth", percentageWidth);
			Throw.IfNegative("percentageHeight", percentageHeight);

			MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
			Thumbnail(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image repeated across and down the image.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="compose">The algorithm to use.</param>
		///<exception cref="MagickException"/>
		public void Tile(MagickImage image, CompositeOperator compose)
		{
			Tile(image, compose, null);
		}
		///==========================================================================================
		///<summary>
		/// Compose an image repeated across and down the image.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="compose">The algorithm to use.</param>
		///<param name="args">The arguments for the algorithm (compose:args).</param>
		///<exception cref="MagickException"/>
		public void Tile(MagickImage image, CompositeOperator compose, string args)
		{
			Throw.IfNull("image", image);

			for (int y = 0; y < Height; y += image.Height)
			{
				for (int x = 0; x < Width; x += image.Width)
				{
					Composite(image, x, y, compose, args);
				}
			}
		}
		///==========================================================================================
		///<summary>
		/// Applies a color vector to each pixel in the image. The length of the vector is 0 for black
		/// and white and at its maximum for the midtones. The vector weighting function is
		/// f(x)=(1-(4.0*((x-0.5)*(x-0.5))))
		///</summary>
		///<param name="opacity">A color value used for tinting.</param>
		///<exception cref="MagickException"/>
		public void Tint(string opacity)
		{
			Throw.IfNullOrEmpty("opacity", opacity);

			_Instance.Tint(opacity);
		}
		///==========================================================================================
		///<summary>
		/// Converts this instance to a base64 string.
		///</summary>
		public string ToBase64()
		{
			Byte[] bytes = ToByteArray();
			if (bytes == null)
				return "";

			return Convert.ToBase64String(bytes);
		}
		///==========================================================================================
		///<summary>
		/// Converts this instance to a base64 string.
		///</summary>
		///<param name="format">The format to use.</param>
		public string ToBase64(MagickFormat format)
		{
			Byte[] bytes = ToByteArray(format);
			if (bytes == null)
				return "";

			return Convert.ToBase64String(bytes);
		}
		///==========================================================================================
		///<summary>
		/// Converts this instance to a bitmap using ImageFormat.Bitmap.
		///</summary>
		public Bitmap ToBitmap()
		{
			return _Instance.ToBitmap();
		}
		///==========================================================================================
		///<summary>
		/// Converts this instance to a bitmap using the specified ImageFormat. Supported formats are:
		/// Bmp, Gif, Icon, Jpeg, Png, Tiff.
		///</summary>
		public Bitmap ToBitmap(ImageFormat imageFormat)
		{
			Format = GetFormat(imageFormat);

			MemoryStream memStream = new MemoryStream();
			Write(memStream);
			memStream.Position = 0;
			// Do not dispose the memStream, the bitmap owns it.
			return new Bitmap(memStream);
		}
#if !NET20
		///==========================================================================================
		///<summary>
		/// Converts this instance to a BitmapSource.
		///</summary>
		public BitmapSource ToBitmapSource()
		{
			return _Instance.ToBitmapSource();
		}
#endif
		///==========================================================================================
		///<summary>
		/// Converts this instance to a byte array.
		///</summary>
		public Byte[] ToByteArray()
		{
			return _Instance.ToByteArray();
		}
		///==========================================================================================
		///<summary>
		/// Converts this instance to a byte array.
		///</summary>
		///<param name="format">The format to use.</param>
		public Byte[] ToByteArray(MagickFormat format)
		{
			Format = format;
			return _Instance.ToByteArray();
		}
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current image.
		///</summary>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}x{2} {3}-bit {4} {5}",
				Format, Width, Height, Depth, ColorSpace, FormatedFileSize());
		}
		///==========================================================================================
		///<summary>
		/// Transform image based on image geometry.
		///</summary>
		///<param name="imageGeometry">The image geometry.</param>
		///<exception cref="MagickException"/>
		public void Transform(MagickGeometry imageGeometry)
		{
			Throw.IfNull("imageGeometry", imageGeometry);

			_Instance.Transform(MagickGeometry.GetInstance(imageGeometry));
		}
		///==========================================================================================
		///<summary>
		/// Transform image based on image geometry.
		///</summary>
		///<param name="imageGeometry">The image geometry.</param>
		///<param name="cropGeometry">The crop geometry.</param>
		///<exception cref="MagickException"/>
		public void Transform(MagickGeometry imageGeometry, MagickGeometry cropGeometry)
		{
			Throw.IfNull("imageGeometry", imageGeometry);
			Throw.IfNull("cropGeometry", cropGeometry);

			_Instance.Transform(MagickGeometry.GetInstance(imageGeometry), MagickGeometry.GetInstance(cropGeometry));
		}
		///==========================================================================================
		/// <summary>
		///  Transforms the image from the colorspace of the source profile to the target profile. The
		///  source profile will only be used if the image does not contain a color profile. Nothing
		///  will happen if the source profile has a different colorspace then that of the image.
		/// </summary>
		/// <param name="source">The source color profile.</param>
		/// <param name="target">The target color profile</param>
		public void TransformColorSpace(ColorProfile source, ColorProfile target)
		{
			Throw.IfNull("source", source);
			Throw.IfNull("target", target);

			if (source.ColorSpace != ColorSpace)
				return;

			AddProfile(source, false);
			AddProfile(target);
		}
		///==========================================================================================
		///<summary>
		/// Origin of coordinate system to use when annotating with text or drawing.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<exception cref="MagickException"/>
		public void TransformOrigin(double x, double y)
		{
			_Instance.TransformOrigin(x, y);
		}
		///==========================================================================================
		///<summary>
		/// Rotation to use when annotating with text or drawing.
		///</summary>
		///<param name="angle">The angle.</param>
		///<exception cref="MagickException"/>
		public void TransformRotation(double angle)
		{
			_Instance.TransformRotation(angle);
		}
		///==========================================================================================
		///<summary>
		/// Reset transformation parameters to default.
		///</summary>
		///<exception cref="MagickException"/>
		public void TransformReset()
		{
			_Instance.TransformReset();
		}
		///==========================================================================================
		///<summary>
		/// Scale to use when annotating with text or drawing.
		///</summary>
		///<param name="scaleX">The X coordinate scaling element.</param>
		///<param name="scaleY">The Y coordinate scaling element.</param>
		///<exception cref="MagickException"/>
		public void TransformScale(double scaleX, double scaleY)
		{
			_Instance.TransformScale(scaleX, scaleY);
		}
		///==========================================================================================
		///<summary>
		/// Skew to use in X axis when annotating with text or drawing.
		///</summary>
		///<param name="skewX">The X skew.</param>
		///<exception cref="MagickException"/>
		public void TransformSkewX(double skewX)
		{
			_Instance.TransformSkewX(skewX);
		}
		///==========================================================================================
		///<summary>
		/// Skew to use in Y axis when annotating with text or drawing.
		///</summary>
		///<param name="skewY">The Y skew.</param>
		///<exception cref="MagickException"/>
		public void TransformSkewY(double skewY)
		{
			_Instance.TransformSkewY(skewY);
		}
		///==========================================================================================
		///<summary>
		/// Add alpha channel to image, setting pixels matching color to transparent.
		///</summary>
		///<param name="color">The color to make transparent.</param>
		///<exception cref="MagickException"/>
		public void Transparent(MagickColor color)
		{
			Throw.IfNull("color", color);

			_Instance.Transparent(MagickColor.GetInstance(color));
		}
		///==========================================================================================
		///<summary>
		/// Add alpha channel to image, setting pixels that lie in between the given two colors to
		/// transparent.
		///</summary>
		///<exception cref="MagickException"/>
		public void TransparentChroma(MagickColor colorLow, MagickColor colorHigh)
		{
			Throw.IfNull("colorLow", colorLow);
			Throw.IfNull("colorHigh", colorHigh);

			_Instance.TransparentChroma(MagickColor.GetInstance(colorLow), MagickColor.GetInstance(colorHigh));
		}
		///==========================================================================================
		///<summary>
		/// Creates a horizontal mirror image by reflecting the pixels around the central y-axis while
		/// rotating them by 90 degrees.
		///</summary>
		///<exception cref="MagickException"/>
		public void Transpose()
		{
			_Instance.Transpose();
		}
		///==========================================================================================
		///<summary>
		/// Creates a vertical mirror image by reflecting the pixels around the central x-axis while
		/// rotating them by 270 degrees.
		///</summary>
		///<exception cref="MagickException"/>
		public void Transverse()
		{
			_Instance.Transverse();
		}
		///==========================================================================================
		///<summary>
		/// Trim edges that are the background color from the image.
		///</summary>
		///<exception cref="MagickException"/>
		public void Trim()
		{
			_Instance.Trim();
		}
		///==========================================================================================
		///<summary>
		/// Returns the unique colors of an image.
		///</summary>
		///<exception cref="MagickException"/>
		public MagickImage UniqueColors()
		{
			return Create(_Instance.UniqueColors());
		}
		///==========================================================================================
		///<summary>
		/// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		public void Unsharpmask(double radius, double sigma)
		{
			Unsharpmask(radius, sigma, 1.0, 0.05);
		}
		///==========================================================================================
		///<summary>
		/// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="channels">The channel(s) that should be sharpened.</param>
		///<exception cref="MagickException"/>
		public void Unsharpmask(double radius, double sigma, Channels channels)
		{
			Unsharpmask(radius, sigma, 1.0, 0.05, channels);
		}
		///==========================================================================================
		///<summary>
		/// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="amount">The percentage of the difference between the original and the blur image
		/// that is added back into the original.</param>
		///<param name="threshold">The threshold in pixels needed to apply the diffence amount.</param>
		///<exception cref="MagickException"/>
		public void Unsharpmask(double radius, double sigma, double amount, double threshold)
		{
			_Instance.Unsharpmask(radius, sigma, amount, threshold);
		}
		///==========================================================================================
		///<summary>
		/// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="amount">The percentage of the difference between the original and the blur image
		/// that is added back into the original.</param>
		///<param name="threshold">The threshold in pixels needed to apply the diffence amount.</param>
		///<param name="channels">The channel(s) that should be sharpened.</param>
		///<exception cref="MagickException"/>
		public void Unsharpmask(double radius, double sigma, double amount, double threshold, Channels channels)
		{
			_Instance.Unsharpmask(radius, sigma, amount, threshold, channels);
		}
		///==========================================================================================
		///<summary>
		/// Softens the edges of the image in vignette style.
		///</summary>
		///<exception cref="MagickException"/>
		public void Vignette()
		{
			Vignette(0.0, 1.0, 0, 0);
		}
		///==========================================================================================
		///<summary>
		/// Softens the edges of the image in vignette style.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="x">The x ellipse offset.</param>
		///<param name="y">the y ellipse offset.</param>
		///<exception cref="MagickException"/>
		public void Vignette(double radius, double sigma, int x, int y)
		{
			_Instance.Vignette(radius, sigma, x, y);
		}
		///==========================================================================================
		///<summary>
		/// Map image pixels to a sine wave.
		///</summary>
		///<exception cref="MagickException"/>
		public void Wave()
		{
			Wave(25.0, 150.0);
		}
		///==========================================================================================
		///<summary>
		/// Map image pixels to a sine wave.
		///</summary>
		///<param name="amplitude">The amplitude.</param>
		///<param name="length">The length of the wave.</param>
		///<exception cref="MagickException"/>
		public void Wave(double amplitude, double length)
		{
			_Instance.Wave(amplitude, length);
		}
		///==========================================================================================
		///<summary>
		/// Forces all pixels above the threshold into white while leaving all pixels at or below
		/// the threshold unchanged.
		///</summary>
		///<param name="threshold">The threshold to use.</param>
		///<exception cref="MagickException"/>
		public void WhiteThreshold(Percentage threshold)
		{
			Throw.IfNegative("threshold", threshold);

			_Instance.WhiteThreshold(threshold.ToString());
		}
		///==========================================================================================
		///<summary>
		/// Forces all pixels above the threshold into white while leaving all pixels at or below
		/// the threshold unchanged.
		///</summary>
		///<param name="threshold">The threshold to use.</param>
		///<param name="channels">The channel(s) to make black.</param>
		///<exception cref="MagickException"/>
		public void WhiteThreshold(Percentage threshold, Channels channels)
		{
			Throw.IfNegative("threshold", threshold);

			_Instance.WhiteThreshold(threshold.ToString(), channels);
		}
		///==========================================================================================
		///<summary>
		/// Writes the image to the specified file.
		///</summary>
		///<param name="file">The file to write the image to.</param>
		///<exception cref="MagickException"/>
		public void Write(FileInfo file)
		{
			Throw.IfNull("file", file);

			Write(file.FullName);
			file.Refresh();
		}
		///==========================================================================================
		///<summary>
		/// Writes the image to the specified stream.
		///</summary>
		///<param name="stream">The stream to write the image data to.</param>
		///<exception cref="MagickException"/>
		public void Write(Stream stream)
		{
			_Instance.Write(stream);
		}
		///==========================================================================================
		///<summary>
		/// Writes the image to the specified stream.
		///</summary>
		///<param name="stream">The stream to write the image data to.</param>
		///<param name="format">The format to use.</param>
		///<exception cref="MagickException"/>
		public void Write(Stream stream, MagickFormat format)
		{
			Format = format;
			Write(stream);
		}
		///==========================================================================================
		///<summary>
		/// Writes the image to the specified file name.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		public void Write(string fileName)
		{
			string filePath = FileHelper.CheckForBaseDirectory(fileName);
			_Instance.Write(filePath);
		}
		///==========================================================================================
		///<summary>
		/// Zoom image to specified size.
		///</summary>
		///<param name="width">The new width.</param>
		///<param name="height">The new height.</param>
		///<exception cref="MagickException"/>
		public void Zoom(int width, int height)
		{
			MagickGeometry geometry = new MagickGeometry(width, height);
			Zoom(geometry);
		}
		///==========================================================================================
		///<summary>
		/// Zoom image to specified size.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		public void Zoom(MagickGeometry geometry)
		{
			Throw.IfNull("geometry", geometry);

			_Instance.Zoom(MagickGeometry.GetInstance(geometry));
		}
		///==========================================================================================
		///<summary>
		/// Zoom image to specified size.
		///</summary>
		///<param name="percentage">The percentage.</param>
		///<exception cref="MagickException"/>
		public void Zoom(Percentage percentage)
		{
			Zoom(percentage, percentage);
		}
		///==========================================================================================
		///<summary>
		/// Zoom image to specified size.
		///</summary>
		///<param name="percentageWidth">The percentage of the width.</param>
		///<param name="percentageHeight">The percentage of the height.</param>
		///<exception cref="MagickException"/>
		public void Zoom(Percentage percentageWidth, Percentage percentageHeight)
		{
			Throw.IfNegative("percentageWidth", percentageWidth);
			Throw.IfNegative("percentageHeight", percentageHeight);

			MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
			Zoom(geometry);
		}
		//===========================================================================================
	}
	//==============================================================================================
}