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
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
  /// <summary>
  /// Class that represents an ImageMagick image.
  /// </summary>
  [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
  public sealed partial class MagickImage : IEquatable<MagickImage>, IComparable<MagickImage>
  {
    private ProgressDelegate _NativeProgress;
    private EventHandler<ProgressEventArgs> _Progress;
    private EventHandler<WarningEventArgs> _Warning;

    private MagickImage(NativeMagickImage instance, MagickSettings settings)
    {
      SetSettings(settings);
      SetInstance(instance);
    }

    private PointD CalculateContrastStretch(Percentage blackPoint, Percentage whitePoint)
    {
      double x = blackPoint.ToDouble();
      double y = whitePoint.ToDouble();

      double pixels = Width * Height;
      x *= (pixels / 100.0);
      y *= (pixels / 100.0);
      y = pixels - y;

      return new PointD(x, y);
    }

    private IEnumerable<MagickImage> CreateList(IntPtr images)
    {
      return CreateList(images, Settings.Clone());
    }

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "FrameCount")]
    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "MagickImage")]
    private MagickReadSettings CreateReadSettings(MagickReadSettings readSettings)
    {
      if (readSettings != null && readSettings.FrameCount.HasValue)
        Throw.IfFalse(nameof(readSettings), readSettings.FrameCount.Value == 1,
          "The FrameCount can only be set to 1 when a MagickImage is being read.");

      MagickReadSettings newReadSettings = null;
      if (readSettings == null)
        newReadSettings = new MagickReadSettings(Settings);
      else
        newReadSettings = new MagickReadSettings(readSettings);

      newReadSettings.FrameCount = 1;

      return newReadSettings;
    }

    private void Dispose(bool disposing)
    {
      DisposeInstance();

      if (disposing)
      {
        if (Settings != null)
          Settings.Artifact -= OnArtifact;
      }
    }

    private void DisposeInstance()
    {
      if (_NativeInstance == null)
        return;

      _NativeInstance.Warning -= OnWarning;
      _NativeInstance.Dispose();
    }

    private string FormatedFileSize()
    {
      decimal fileSize = FileSize;

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

    private static int GetExpectedLength(MagickReadSettings settings)
    {
      int length = settings.Width.Value * settings.Height.Value * settings.PixelStorage.Mapping.Length;
      switch (settings.PixelStorage.StorageType)
      {
        case StorageType.Char:
          return length;
        case StorageType.Double:
          return length * sizeof(double);
        case StorageType.Float:
          return length * sizeof(float);
        case StorageType.Long:
          return length * sizeof(int);
        case StorageType.LongLong:
          return length * sizeof(long);
        case StorageType.Quantum:
          return length * sizeof(QuantumType);
        case StorageType.Short:
          return length * sizeof(short);
        case StorageType.Undefined:
        default:
          throw new NotSupportedException();
      }
    }

    private void FloodFill(QuantumType alpha, int x, int y, bool invert)
    {
      MagickColor target;
      using (PixelCollection pixels = GetPixels())
      {
        target = pixels.GetPixel(x, y).ToColor();
        target.A = alpha;
      }

      _NativeInstance.FloodFill(Settings.Drawing, x, y, target, invert);
    }

    private void FloodFill(MagickColor color, int x, int y, bool invert)
    {
      Throw.IfNull(nameof(color), color);

      MagickColor target;
      using (PixelCollection pixels = GetPixels())
      {
        target = pixels.GetPixel(x, y).ToColor();
      }

      FloodFill(color, x, y, target, invert);
    }

    private void FloodFill(MagickColor color, int x, int y, MagickColor target, bool invert)
    {
      Throw.IfNull(nameof(color), color);
      Throw.IfNull(nameof(target), target);

      DrawingSettings settings = Settings.Drawing;

      using (MagickImage fillPattern = settings.FillPattern)
      {
        MagickColor filLColor = settings.FillColor;
        settings.FillColor = color;
        settings.FillPattern = null;

        _NativeInstance.FloodFill(settings, x, y, target, invert);

        settings.FillColor = filLColor;
        settings.FillPattern = fillPattern;
      }
    }

    private void FloodFill(MagickImage image, int x, int y, bool invert)
    {
      Throw.IfNull(nameof(image), image);

      MagickColor target;
      using (PixelCollection pixels = GetPixels())
      {
        target = pixels.GetPixel(x, y).ToColor();
      }

      FloodFill(image, x, y, target, invert);
    }

    private void FloodFill(MagickImage image, int x, int y, MagickColor target, bool invert)
    {
      Throw.IfNull(nameof(image), image);
      Throw.IfNull(nameof(target), target);

      DrawingSettings settings = Settings.Drawing;

      using (MagickImage fillPattern = settings.FillPattern)
      {
        MagickColor filLColor = settings.FillColor;
        settings.FillColor = null;
        settings.FillPattern = image;

        _NativeInstance.FloodFill(settings, x, y, target, invert);

        settings.FillColor = filLColor;
        settings.FillPattern = fillPattern;
      }
    }

    private void LevelColors(MagickColor blackColor, MagickColor whiteColor, bool invert)
    {
      LevelColors(blackColor, whiteColor, ImageMagick.Channels.RGB, invert);
    }

    private void LevelColors(MagickColor blackColor, MagickColor whiteColor, Channels channels, bool invert)
    {
      Throw.IfNull(nameof(blackColor), blackColor);
      Throw.IfNull(nameof(whiteColor), whiteColor);

      _NativeInstance.LevelColors(blackColor, whiteColor, channels, invert);
    }

    private void Opaque(MagickColor target, MagickColor fill, bool invert)
    {
      Throw.IfNull(nameof(target), target);
      Throw.IfNull(nameof(fill), fill);

      _NativeInstance.Opaque(target, fill, invert);
    }

    private ColorProfile GetColorProfile(string name)
    {
      StringInfo info = _NativeInstance.GetProfile(name);
      if (info == null || info.Datum == null)
        return null;

      return new ColorProfile(name, info.Datum);
    }

    private void OnArtifact(object sender, ArtifactEventArgs arguments)
    {
      if (arguments.Value == null)
        RemoveArtifact(arguments.Key);
      else
        SetArtifact(arguments.Key, arguments.Value);
    }

    private bool OnProgress(IntPtr origin, long offset, ulong extent, IntPtr userData)
    {
      if (_Progress == null)
        return true;

      string managedOrigin = UTF8Marshaler.NativeToManaged(origin);
      ProgressEventArgs eventArgs = new ProgressEventArgs(managedOrigin, (int)offset, (int)extent);
      _Progress(this, eventArgs);
      return eventArgs.Cancel ? false : true;
    }

    private void OnWarning(object sender, WarningEventArgs arguments)
    {
      if (_Warning != null)
        _Warning(this, arguments);
    }

    private void Read(byte[] data, MagickReadSettings readSettings, bool ping)
    {
      Throw.IfNullOrEmpty(nameof(data), data);

      MagickReadSettings newReadSettings = CreateReadSettings(readSettings);
      SetSettings(newReadSettings);

      if (newReadSettings.PixelStorage != null)
      {
        ReadPixels(data, readSettings);
        return;
      }

      Settings.Ping = ping;
      _NativeInstance.ReadBlob(Settings, data, data.Length);
    }

    private void ReadPixels(byte[] data, MagickReadSettings readSettings)
    {
      Throw.IfTrue(nameof(readSettings), readSettings.PixelStorage.StorageType == StorageType.Undefined, "Storage type should not be undefined.");
      Throw.IfNull(nameof(readSettings), readSettings.Width, "Width should be defined when pixel storage is set.");
      Throw.IfNull(nameof(readSettings), readSettings.Height, "Height should be defined when pixel storage is set.");
      Throw.IfNullOrEmpty(nameof(readSettings), readSettings.PixelStorage.Mapping, "Pixel storage mapping should be defined.");

      int length = GetExpectedLength(readSettings);
      Throw.IfTrue(nameof(data), data.Length != length, "The array length is " + data.Length + " but should be " + length + ".");

      _NativeInstance.ReadPixels(readSettings.Width.Value, readSettings.Height.Value, readSettings.PixelStorage.Mapping, readSettings.PixelStorage.StorageType, data);
    }

    private void Read(string fileName, MagickReadSettings readSettings, bool ping)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      Throw.IfInvalidFileName(filePath);

      MagickReadSettings newReadSettings = CreateReadSettings(readSettings);
      SetSettings(newReadSettings);

      if (newReadSettings.PixelStorage != null)
      {
        byte[] data = File.ReadAllBytes(filePath);
        ReadPixels(data, readSettings);
        return;
      }

      Settings.Ping = ping;
      Settings.FileName = filePath;

      _NativeInstance.ReadFile(Settings);
    }

    private void SetInstance(NativeMagickImage instance)
    {
      DisposeInstance();

      _NativeInstance = instance;
      _NativeInstance.Warning += OnWarning;
    }

    private void SetSettings(MagickSettings settings)
    {
      if (Settings != null)
        Settings.Artifact -= OnArtifact;

      Settings = settings;
      Settings.Artifact += OnArtifact;
    }

    internal static MagickImage Clone(MagickImage image)
    {
      return image != null ? image.Clone() : null;
    }

    internal static MagickImage Create(IntPtr image)
    {
      if (image == IntPtr.Zero)
        return null;

      NativeMagickImage instance = new NativeMagickImage(image);
      return new MagickImage(instance, new MagickSettings());
    }

    internal static MagickImage Create(IntPtr image, MagickSettings settings)
    {
      if (image == IntPtr.Zero)
        return null;

      NativeMagickImage instance = new NativeMagickImage(image);
      return new MagickImage(instance, settings.Clone());
    }

    internal static MagickErrorInfo CreateErrorInfo(MagickImage image)
    {
      if (image == null)
        return null;

      return new MagickErrorInfo(image._NativeInstance.MeanErrorPerPixel, image._NativeInstance.NormalizedMeanError, image._NativeInstance.NormalizedMaximumError);
    }

    internal static IEnumerable<MagickImage> CreateList(IntPtr images, MagickSettings settings)
    {
      Collection<MagickImage> result = new Collection<MagickImage>();

      IntPtr image = images;

      while (image != IntPtr.Zero)
      {
        IntPtr next = NativeMagickImage.GetNext(image);

        NativeMagickImage instance = new NativeMagickImage(image);
        instance.SetNext(IntPtr.Zero);

        result.Add(new MagickImage(instance, settings.Clone()));
        image = next;
      }

      return result;
    }

    internal int ChannelOffset(PixelChannel pixelChannel)
    {
      if (!_NativeInstance.HasChannel(pixelChannel))
        return -1;

      return (int)_NativeInstance.ChannelOffset(pixelChannel);
    }

    internal void SetNext(MagickImage image)
    {
      if (image == null)
        _NativeInstance.SetNext(IntPtr.Zero);
      else
        _NativeInstance.SetNext(MagickImage.GetInstance(image));
    }

    /// <summary>
    /// Finalizer
    /// </summary>
    ~MagickImage()
    {
      Dispose(false);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class.
    /// </summary>
    public MagickImage()
    {
      SetSettings(new MagickSettings());
      SetInstance(new NativeMagickImage(Settings));
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified byte array.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public MagickImage(byte[] data)
          : this()
    {
      Read(data);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified byte array.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public MagickImage(byte[] data, MagickReadSettings readSettings)
          : this()
    {
      Read(data, readSettings);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified file.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException"/>
    public MagickImage(FileInfo file)
          : this()
    {
      Read(file);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified file.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public MagickImage(FileInfo file, MagickReadSettings readSettings)
          : this()
    {
      Read(file, readSettings);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified width, height
    /// and color.
    /// </summary>
    /// <param name="color">The color to fill the image with.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public MagickImage(MagickColor color, int width, int height)
      : this()
    {
      Read(color, width, height);
      BackgroundColor = color;
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class by creating a copy of the specified
    /// image.
    /// </summary>
    /// <param name="image">The image to create a copy of.</param>
    public MagickImage(MagickImage image)
    {
      Throw.IfNull(nameof(image), image);

      SetSettings(image.Settings.Clone());
      SetInstance(new NativeMagickImage(image._NativeInstance.Clone()));
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified stream.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public MagickImage(Stream stream)
      : this()
    {
      Read(stream);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified stream.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public MagickImage(Stream stream, MagickReadSettings readSettings)
          : this()
    {
      Read(stream, readSettings);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified filename.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public MagickImage(string fileName)
          : this()
    {
      Read(fileName);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified filename
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException"/>
    public MagickImage(string fileName, int width, int height)
          : this()
    {
      Read(fileName, width, height);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified filename
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public MagickImage(string fileName, MagickReadSettings readSettings)
          : this()
    {
      Read(fileName, readSettings);
    }

    /// <summary>
    /// Determines whether the specified MagickImage instances are considered equal.
    /// </summary>
    /// <param name="left">The first MagickImage to compare.</param>
    /// <param name="right"> The second MagickImage to compare.</param>
    /// <returns></returns>
    public static bool operator ==(MagickImage left, MagickImage right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified MagickImage instances are not considered equal.
    /// </summary>
    /// <param name="left">The first MagickImage to compare.</param>
    /// <param name="right"> The second MagickImage to compare.</param>
    /// <returns></returns>
    public static bool operator !=(MagickImage left, MagickImage right)
    {
      return !Equals(left, right);
    }

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

    /// <summary>
    /// Converts this instance to a byte array.
    /// </summary>
    public static explicit operator byte[](MagickImage image)
    {
      Throw.IfNull(nameof(image), image);

      return image.ToByteArray();
    }

    /// <summary>
    /// Event that will be raised when progress is reported by this image.
    /// </summary>
    public event EventHandler<ProgressEventArgs> Progress
    {
      add
      {
        if (_Progress == null)
        {
          _NativeProgress = new ProgressDelegate(OnProgress);
          _NativeInstance.SetProgressDelegate(_NativeProgress);
        }

        _Progress += value;
      }
      remove
      {
        _Progress -= value;

        if (_Progress == null)
        {
          _NativeInstance.SetProgressDelegate(null);
          _NativeProgress = null;
        }
      }
    }

    /// <summary>
    /// Event that will we raised when a warning is thrown by ImageMagick.
    /// </summary>
    public event EventHandler<WarningEventArgs> Warning
    {
      add
      {
        _Warning += value;
      }
      remove
      {
        _Warning -= value;
      }
    }

    /// <summary>
    /// Transparent color.
    /// </summary>
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

    /// <summary>
    /// Time in 1/100ths of a second which must expire before splaying the next image in an
    /// animated sequence.
    /// </summary>
    public int AnimationDelay
    {
      get
      {
        return _NativeInstance.AnimationDelay;
      }
      set
      {
        _NativeInstance.AnimationDelay = value;
      }
    }

    /// <summary>
    /// Number of iterations to loop an animation (e.g. Netscape loop extension) for.
    /// </summary>
    public int AnimationIterations
    {
      get
      {
        return _NativeInstance.AnimationIterations;
      }
      set
      {
        _NativeInstance.AnimationIterations = value;
      }
    }

    /// <summary>
    /// The names of the artifacts.
    /// </summary>
    public IEnumerable<string> ArtifactNames
    {
      get
      {
        _NativeInstance.ResetArtifactIterator();
        string name = _NativeInstance.GetNextArtifactName();
        while (name != null)
        {
          yield return name;
          name = _NativeInstance.GetNextArtifactName();
        }
      }
    }

    /// <summary>
    /// The names of the attributes.
    /// </summary>
    public IEnumerable<string> AttributeNames
    {
      get
      {
        _NativeInstance.ResetAttributeIterator();
        string name = _NativeInstance.GetNextAttributeName();
        while (name != null)
        {
          yield return name;
          name = _NativeInstance.GetNextAttributeName();
        }
      }
    }

    /// <summary>
    /// Background color of the image.
    /// </summary>
    public MagickColor BackgroundColor
    {
      get
      {
        return _NativeInstance.BackgroundColor;
      }
      set
      {
        _NativeInstance.BackgroundColor = value;
        Settings.BackgroundColor = value;
      }
    }

    /// <summary>
    /// Height of the image before transformations.
    /// </summary>
    public int BaseHeight
    {
      get
      {
        return _NativeInstance.BaseHeight;
      }
    }

    /// <summary>
    /// Width of the image before transformations.
    /// </summary>
    public int BaseWidth
    {
      get
      {
        return _NativeInstance.BaseWidth;
      }
    }

    /// <summary>
    /// Use black point compensation.
    /// </summary>
    public bool BlackPointCompensation
    {
      get
      {
        return _NativeInstance.BlackPointCompensation;
      }
      set
      {
        _NativeInstance.BlackPointCompensation = value;
      }
    }

    /// <summary>
    /// Border color of the image.
    /// </summary>
    public MagickColor BorderColor
    {
      get
      {
        return _NativeInstance.BorderColor;
      }
      set
      {
        _NativeInstance.BorderColor = value;
      }
    }

    /// <summary>
    /// Return smallest bounding box enclosing non-border pixels. The current fuzz value is used
    /// when discriminating between pixels.
    /// </summary>
    public MagickGeometry BoundingBox
    {
      get
      {
        return MagickGeometry.FromRectangle(_NativeInstance.BoundingBox);
      }
    }

    /// <summary>
    /// Returns the number of channels that the image contains.
    /// </summary>
    public int ChannelCount
    {
      get
      {
        return _NativeInstance.ChannelCount;
      }
    }

    /// <summary>
    /// Returns the channels of the image.
    /// </summary>
    public IEnumerable<PixelChannel> Channels
    {
      get
      {
        if (_NativeInstance.HasChannel(PixelChannel.Red))
          yield return PixelChannel.Red;
        if (_NativeInstance.HasChannel(PixelChannel.Green))
          yield return PixelChannel.Green;
        if (_NativeInstance.HasChannel(PixelChannel.Blue))
          yield return PixelChannel.Blue;
        if (_NativeInstance.HasChannel(PixelChannel.Black))
          yield return PixelChannel.Black;
        if (_NativeInstance.HasChannel(PixelChannel.Alpha))
          yield return PixelChannel.Alpha;
      }
    }

    /// <summary>
    /// Chromaticity blue primary point.
    /// </summary>
    public PrimaryInfo ChromaBluePrimary
    {
      get
      {
        return _NativeInstance.ChromaBluePrimary;
      }
      set
      {
        _NativeInstance.ChromaBluePrimary = value;
      }
    }

    /// <summary>
    /// Chromaticity green primary point.
    /// </summary>
    public PrimaryInfo ChromaGreenPrimary
    {
      get
      {
        return _NativeInstance.ChromaGreenPrimary;
      }
      set
      {
        _NativeInstance.ChromaGreenPrimary = value;
      }
    }

    /// <summary>
    /// Chromaticity red primary point.
    /// </summary>
    public PrimaryInfo ChromaRedPrimary
    {
      get
      {
        return _NativeInstance.ChromaRedPrimary;
      }
      set
      {
        _NativeInstance.ChromaRedPrimary = value;
      }
    }

    /// <summary>
    /// Chromaticity white primary point.
    /// </summary>
    public PrimaryInfo ChromaWhitePoint
    {
      get
      {
        return _NativeInstance.ChromaWhitePoint;
      }
      set
      {
        _NativeInstance.ChromaWhitePoint = value;
      }
    }

    /// <summary>
    /// Image class (DirectClass or PseudoClass)
    /// NOTE: Setting a DirectClass image to PseudoClass will result in the loss of color information
    /// if the number of colors in the image is greater than the maximum palette size (either 256 (Q8)
    /// or 65536 (Q16).
    /// </summary>
    public ClassType ClassType
    {
      get
      {
        return _NativeInstance.ClassType;
      }
      set
      {
        _NativeInstance.ClassType = value;
      }
    }

    /// <summary>
    /// Colors within this distance are considered equal.
    /// </summary>
    public Percentage ColorFuzz
    {
      get
      {
        return Percentage.FromQuantum(_NativeInstance.ColorFuzz);
      }
      set
      {
        double newValue = value.ToQuantum();
        _NativeInstance.ColorFuzz = newValue;
        Settings.ColorFuzz = newValue;
      }
    }

    /// <summary>
    /// Colormap size (number of colormap entries).
    /// </summary>
    public int ColormapSize
    {
      get
      {
        return _NativeInstance.ColormapSize;
      }
      set
      {
        _NativeInstance.ColormapSize = value;
      }
    }

    /// <summary>
    /// Color space of the image.
    /// </summary>
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

    /// <summary>
    /// Color type of the image.
    /// </summary>
    public ColorType ColorType
    {
      get
      {
        if (Settings.ColorType != ColorType.Undefined)
          return Settings.ColorType;

        return _NativeInstance.ColorType;
      }
      set
      {
        _NativeInstance.ColorType = value;
        Settings.ColorType = value;
      }
    }

    /// <summary>
    /// Comment text of the image.
    /// </summary>
    public string Comment
    {
      get
      {
        return Settings.GetOption("Comment");
      }
      set
      {
        Settings.SetOption("Comment", value);
      }
    }

    /// <summary>
    /// Composition operator to be used when composition is implicitly used (such as for image flattening).
    /// </summary>
    public CompositeOperator Compose
    {
      get
      {
        return _NativeInstance.Compose;
      }
      set
      {
        _NativeInstance.Compose = value;
      }
    }

    /// <summary>
    /// Compression method to use.
    /// </summary>
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

    /// <summary>
    /// Vertical and horizontal resolution in pixels of the image.
    /// </summary>
    public Density Density
    {
      get
      {
        return new Density(_NativeInstance.ResolutionX, _NativeInstance.ResolutionY, _NativeInstance.ResolutionUnits);
      }
      set
      {
        if (value == null)
          return;

        _NativeInstance.ResolutionX = value.X;
        _NativeInstance.ResolutionY = value.Y;
        _NativeInstance.ResolutionUnits = value.Units;
      }
    }

    /// <summary>
    /// The depth (bits allocated to red/green/blue components).
    /// </summary>
    public int Depth
    {
      get
      {
        return _NativeInstance.Depth;
      }
      set
      {
        _NativeInstance.Depth = value;
      }
    }

    /// <summary>
    /// Preferred size of the image when encoding.
    /// </summary>
    /// <exception cref="MagickException"/>
    public MagickGeometry EncodingGeometry
    {
      get
      {
        return MagickGeometry.FromString(_NativeInstance.EncodingGeometry);
      }
    }

    /// <summary>
    /// Endianness (little like Intel or big like SPARC) for image formats which support
    /// endian-specific options.
    /// </summary>
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

    /// <summary>
    /// Original file name of the image (only available if read from disk).
    /// </summary>
    public string FileName
    {
      get
      {
        return _NativeInstance.FileName;
      }
    }

    /// <summary>
    /// Image file size.
    /// </summary>
    public long FileSize
    {
      get
      {
        return _NativeInstance.FileSize;
      }
    }

    /// <summary>
    /// Filter to use when resizing image.
    /// </summary>
    public FilterType FilterType
    {
      get
      {
        return _NativeInstance.FilterType;
      }
      set
      {
        _NativeInstance.FilterType = value;
      }
    }

    /// <summary>
    /// The format of the image.
    /// </summary>
    public MagickFormat Format
    {
      get
      {
        return EnumHelper.Parse(_NativeInstance.Format, MagickFormat.Unknown);
      }
      set
      {
        _NativeInstance.Format = EnumHelper.GetName(value);
        Settings.Format = value;
      }
    }

    /// <summary>
    /// The information about the format of the image.
    /// </summary>
    public MagickFormatInfo FormatInfo
    {
      get
      {
        return MagickNET.GetFormatInformation(Format);
      }
    }

    /// <summary>
    /// Gamma level of the image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public double Gamma
    {
      get
      {
        return _NativeInstance.Gamma;
      }
    }

    /// <summary>
    /// Gif disposal method.
    /// </summary>
    public GifDisposeMethod GifDisposeMethod
    {
      get
      {
        return _NativeInstance.GifDisposeMethod;
      }
      set
      {
        _NativeInstance.GifDisposeMethod = value;
      }
    }

    /// <summary>
    /// Image contains a clipping path.
    /// </summary>
    public bool HasClippingPath
    {
      get
      {
        return !string.IsNullOrEmpty(GetAttribute("8BIM:1999,2998:#1"));
      }
    }

    /// <summary>
    /// Image supports transparency (alpha channel).
    /// </summary>
    public bool HasAlpha
    {
      get
      {
        return _NativeInstance.HasAlpha;
      }
      set
      {
        if (_NativeInstance.HasAlpha != value)
        {
          if (value)
            Alpha(AlphaOption.Opaque);
          _NativeInstance.HasAlpha = value;
        }
      }
    }

    /// <summary>
    /// Height of the image.
    /// </summary>
    public int Height
    {
      get
      {
        return _NativeInstance.Height;
      }
    }

    /// <summary>
    /// Type of interlacing to use.
    /// </summary>
    public Interlace Interlace
    {
      get
      {
        return _NativeInstance.Interlace;
      }
      set
      {
        _NativeInstance.Interlace = value;
        Settings.Interlace = value;
      }
    }

    /// <summary>
    /// Pixel color interpolate method to use.
    /// </summary>
    public PixelInterpolateMethod Interpolate
    {
      get
      {
        return _NativeInstance.Interpolate;
      }
      set
      {
        _NativeInstance.Interpolate = value;
      }
    }

    /// <summary>
    /// Returns true if none of the pixels in the image have an alpha value other than
    /// OpaqueAlpha (QuantumRange).
    /// </summary>
    public bool IsOpaque
    {
      get
      {
        return _NativeInstance.IsOpaque;
      }
    }

    /// <summary>
    /// The label of the image.
    /// </summary>
    public string Label
    {
      get
      {
        return GetAttribute("Label");
      }
      set
      {
        if (value == null)
          RemoveAttribute("Label");
        else
          SetAttribute("Label", value);
      }
    }

    /// <summary>
    /// Photo orientation of the image.
    /// </summary>
    public OrientationType Orientation
    {
      get
      {
        return _NativeInstance.Orientation;
      }
      set
      {
        _NativeInstance.Orientation = value;
      }
    }

    /// <summary>
    /// Preferred size and location of an image canvas.
    /// </summary>
    public MagickGeometry Page
    {
      get
      {
        return MagickGeometry.FromRectangle(_NativeInstance.Page);
      }
      set
      {
        if (value == null)
          return;

        _NativeInstance.Page = MagickRectangle.FromGeometry(value, this);
      }
    }

    /// <summary>
    /// The names of the profiles.
    /// </summary>
    public IEnumerable<string> ProfileNames
    {
      get
      {
        _NativeInstance.ResetProfileIterator();
        string name = _NativeInstance.GetNextProfileName();
        while (name != null)
        {
          yield return name;
          name = _NativeInstance.GetNextProfileName();
        }
      }
    }

    /// <summary>
    /// JPEG/MIFF/PNG compression level (default 75).
    /// </summary>
    public int Quality
    {
      get
      {
        return _NativeInstance.Quality;
      }
      set
      {
        int quality = value < 1 ? 1 : value;
        quality = quality > 100 ? 100 : quality;

        _NativeInstance.Quality = quality;
        Settings.Quality = quality;
      }
    }

    /// <summary>
    /// Associate a mask with the image. The mask must be the same dimensions as the image and
    /// only contain the colors black and white. Pass null to unset an existing mask.
    /// </summary>
    public MagickImage ReadMask
    {
      get
      {
        return _NativeInstance.ReadMask;
      }
      set
      {
        _NativeInstance.ReadMask = value;
      }
    }

    /// <summary>
    /// The type of rendering intent.
    /// </summary>
    public RenderingIntent RenderingIntent
    {
      get
      {
        return _NativeInstance.RenderingIntent;
      }
      set
      {
        _NativeInstance.RenderingIntent = value;
      }
    }

    /// <summary>
    /// Settings for this MagickImage instance.
    /// </summary>
    public MagickSettings Settings
    {
      get;
      private set;
    }

    /// <summary>
    /// Returns the signature of this image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public string Signature
    {
      get
      {
        return _NativeInstance.Signature;
      }
    }

    /// <summary>
    /// Number of colors in the image.
    /// </summary>
    public int TotalColors
    {
      get
      {
        return _NativeInstance.TotalColors;
      }
    }

    /// <summary>
    /// Virtual pixel method.
    /// </summary>
    public VirtualPixelMethod VirtualPixelMethod
    {
      get
      {
        return _NativeInstance.VirtualPixelMethod;
      }
      set
      {
        _NativeInstance.VirtualPixelMethod = value;
      }
    }

    /// <summary>
    /// Width of the image.
    /// </summary>
    public int Width
    {
      get
      {
        return _NativeInstance.Width;
      }
    }

    /// <summary>
    /// Associate a mask with the image. The mask must be the same dimensions as the image and
    /// only contain the colors black and white. Pass null to unset an existing mask.
    /// </summary>
    public MagickImage WriteMask
    {
      get
      {
        return _NativeInstance.WriteMask;
      }
      set
      {
        _NativeInstance.WriteMask = value;
      }
    }

    /// <summary>
    /// Adaptive-blur image with the default blur factor (0x1).
    /// </summary>
    /// <exception cref="MagickException"/>
    public void AdaptiveBlur()
    {
      AdaptiveBlur(0.0, 1.0);
    }

    /// <summary>
    /// Adaptive-blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <exception cref="MagickException"/>
    public void AdaptiveBlur(double radius)
    {
      AdaptiveBlur(radius, 1.0);
    }

    /// <summary>
    /// Adaptive-blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException"/>
    public void AdaptiveBlur(double radius, double sigma)
    {
      _NativeInstance.AdaptiveBlur(radius, sigma);
    }

    /// <summary>
    /// Resize using mesh interpolation. It works well for small resizes of less than +/- 50%
    /// of the original image size. For larger resizing on images a full filtered and slower resize
    /// function should be used instead.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException"/>
    public void AdaptiveResize(int width, int height)
    {
      _NativeInstance.AdaptiveResize(width, height);
    }

    /// <summary>
    /// Resize using mesh interpolation. It works well for small resizes of less than +/- 50%
    /// of the original image size. For larger resizing on images a full filtered and slower resize
    /// function should be used instead.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException"/>
    public void AdaptiveResize(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      AdaptiveResize(geometry.Width, geometry.Height);
    }

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void AdaptiveSharpen()
    {
      AdaptiveSharpen(0.0, 1.0);
    }

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException"/>
    public void AdaptiveSharpen(Channels channels)
    {
      AdaptiveSharpen(0.0, 1.0, channels);
    }

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException"/>
    public void AdaptiveSharpen(double radius, double sigma)
    {
      AdaptiveSharpen(radius, sigma, ImageMagick.Channels.Default);
    }

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    public void AdaptiveSharpen(double radius, double sigma, Channels channels)
    {
      _NativeInstance.AdaptiveSharpen(radius, sigma, channels);
    }

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <exception cref="MagickException"/>
    public void AdaptiveThreshold(int width, int height)
    {
      AdaptiveThreshold(width, height, 0);
    }

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="bias">Constant to subtract from pixel neighborhood mean (+/-)(0-QuantumRange).</param>
    /// <exception cref="MagickException"/>
    public void AdaptiveThreshold(int width, int height, double bias)
    {
      _NativeInstance.AdaptiveThreshold(width, height, bias);
    }

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="biasPercentage">Constant to subtract from pixel neighborhood mean.</param>
    /// <exception cref="MagickException"/>
    public void AdaptiveThreshold(int width, int height, Percentage biasPercentage)
    {
      AdaptiveThreshold(width, height, biasPercentage.ToQuantum());
    }

    /// <summary>
    /// Add noise to image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <exception cref="MagickException"/>
    public void AddNoise(NoiseType noiseType)
    {
      AddNoise(noiseType, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Add noise to the specified channel of the image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <param name="channels">The channel(s) where the noise should be added.</param>
    /// <exception cref="MagickException"/>
    public void AddNoise(NoiseType noiseType, Channels channels)
    {
      AddNoise(noiseType, 1.0, channels);
    }

    /// <summary>
    /// Add noise to image with the specified noise type.
    /// </summary>
    /// <param name="attenuate">Attenuate the random distribution.</param>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <exception cref="MagickException"/>
    public void AddNoise(NoiseType noiseType, double attenuate)
    {
      AddNoise(noiseType, attenuate, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Add noise to the specified channel of the image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <param name="attenuate">Attenuate the random distribution.</param>
    /// <param name="channels">The channel(s) where the noise should be added.</param>
    /// <exception cref="MagickException"/>
    public void AddNoise(NoiseType noiseType, double attenuate, Channels channels)
    {
      _NativeInstance.AddNoise(noiseType, attenuate, channels);
    }

    /// <summary>
    /// Adds the specified profile to the image or overwrites it.
    /// </summary>
    /// <param name="profile">The profile to add or overwrite.</param>
    /// <exception cref="MagickException"/>
    public void AddProfile(ImageProfile profile)
    {
      AddProfile(profile, true);
    }

    /// <summary>
    /// Adds the specified profile to the image or overwrites it when overWriteExisting is true.
    /// </summary>
    /// <param name="profile">The profile to add or overwrite.</param>
    /// <param name="overwriteExisting">When set to false an existing profile with the same name
    /// won't be overwritten.</param>
    /// <exception cref="MagickException"/>
    public void AddProfile(ImageProfile profile, bool overwriteExisting)
    {
      Throw.IfNull(nameof(profile), profile);

      if (!overwriteExisting && _NativeInstance.HasProfile(profile.Name))
        return;

      byte[] datum = profile.ToByteArray();
      if (datum == null || datum.Length == 0)
        return;

      _NativeInstance.AddProfile(profile.Name, datum, datum.Length);
    }

    /// <summary>
    /// Affine Transform image.
    /// </summary>
    /// <param name="affineMatrix">The affine matrix to use.</param>
    /// <exception cref="MagickException"/>
    public void AffineTransform(DrawableAffine affineMatrix)
    {
      Throw.IfNull(nameof(affineMatrix), affineMatrix);

      _NativeInstance.AffineTransform(affineMatrix.ScaleX, affineMatrix.ScaleY, affineMatrix.ShearX, affineMatrix.ShearY, affineMatrix.TranslateX, affineMatrix.TranslateY);
    }

    /// <summary>
    /// Applies the specified alpha option.
    /// </summary>
    /// <param name="option">The option to use.</param>
    /// <exception cref="MagickException"/>
    public void Alpha(AlphaOption option)
    {
      _NativeInstance.SetAlpha(option);
    }

    /// <summary>
    /// Annotate using specified text, and bounding area.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <exception cref="MagickException"/>
    public void Annotate(string text, MagickGeometry boundingArea)
    {
      Annotate(text, boundingArea, Gravity.Northwest, 0.0);
    }

    /// <summary>
    /// Annotate using specified text, bounding area, and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException"/>
    public void Annotate(string text, MagickGeometry boundingArea, Gravity gravity)
    {
      Annotate(text, boundingArea, gravity, 0.0);
    }

    /// <summary>
    /// Annotate using specified text, bounding area, and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="angle">The rotation.</param>
    /// <exception cref="MagickException"/>
    public void Annotate(string text, MagickGeometry boundingArea, Gravity gravity, double angle)
    {
      Throw.IfNullOrEmpty(nameof(text), text);
      Throw.IfNull(nameof(boundingArea), boundingArea);

      _NativeInstance.Annotate(Settings.Drawing, text, MagickGeometry.ToString(boundingArea), gravity, angle);
    }

    /// <summary>
    /// Annotate with text (bounding area is entire image) and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException"/>
    public void Annotate(string text, Gravity gravity)
    {
      Throw.IfNullOrEmpty(nameof(text), text);

      _NativeInstance.AnnotateGravity(Settings.Drawing, text, gravity);
    }

    /// <summary>
    /// Extracts the 'mean' from the image and adjust the image to try make set its gamma.
    /// appropriatally.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void AutoGamma()
    {
      AutoGamma(ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Extracts the 'mean' from the image and adjust the image to try make set its gamma.
    /// appropriatally.
    /// </summary>
    /// <param name="channels">The channel(s) to set the gamma for.</param>
    /// <exception cref="MagickException"/>
    public void AutoGamma(Channels channels)
    {
      _NativeInstance.AutoGamma(channels);
    }

    /// <summary>
    /// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
    /// to the full quantum range.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void AutoLevel()
    {
      AutoLevel(ImageMagick.Channels.Default);
    }

    /// <summary>
    /// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
    /// to the full quantum range.
    /// </summary>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
    public void AutoLevel(Channels channels)
    {
      _NativeInstance.AutoLevel(channels);
    }

    /// <summary>
    /// Adjusts an image so that its orientation is suitable for viewing.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void AutoOrient()
    {
      _NativeInstance.AutoOrient();
    }

    /// <summary>
    /// Forces all pixels below the threshold into black while leaving all pixels at or above
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <exception cref="MagickException"/>
    public void BlackThreshold(Percentage threshold)
    {
      BlackThreshold(threshold, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Forces all pixels below the threshold into black while leaving all pixels at or above
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <param name="channels">The channel(s) to make black.</param>
    /// <exception cref="MagickException"/>
    public void BlackThreshold(Percentage threshold, Channels channels)
    {
      Throw.IfNegative(nameof(threshold), threshold);

      _NativeInstance.BlackThreshold(threshold.ToString(), channels);
    }

    /// <summary>
    /// Simulate a scene at nighttime in the moonlight.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void BlueShift()
    {
      BlueShift(1.5);
    }

    /// <summary>
    /// Simulate a scene at nighttime in the moonlight.
    /// </summary>
    /// <param name="factor">The factor to use.</param>
    /// <exception cref="MagickException"/>
    public void BlueShift(double factor)
    {
      _NativeInstance.BlueShift(factor);
    }

    /// <summary>
    /// Calculates the bit depth (bits allocated to red/green/blue components). Use the Depth
    /// property to get the current value.
    /// </summary>
    /// <exception cref="MagickException"/>
    public int BitDepth()
    {
      return BitDepth(ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Calculates the bit depth (bits allocated to red/green/blue components) of the specified channel.
    /// </summary>
    /// <param name="channels">The channel to get the depth for.</param>
    /// <exception cref="MagickException"/>
    public int BitDepth(Channels channels)
    {
      return _NativeInstance.GetBitDepth(channels);
    }

    /// <summary>
    /// Set the bit depth (bits allocated to red/green/blue components) of the specified channel.
    /// </summary>
    /// <param name="value">The depth.</param>
    /// <param name="channels">The channel to set the depth for.</param>
    /// <exception cref="MagickException"/>
    public void BitDepth(Channels channels, int value)
    {
      _NativeInstance.SetBitDepth(channels, value);
    }

    /// <summary>
    /// Set the bit depth (bits allocated to red/green/blue components).
    /// </summary>
    /// <param name="value">The depth.</param>
    /// <exception cref="MagickException"/>
    public void BitDepth(int value)
    {
      BitDepth(ImageMagick.Channels.Composite, value);
    }

    /// <summary>
    /// Blur image with the default blur factor (0x1).
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Blur()
    {
      Blur(0.0, 1.0);
    }

    /// <summary>
    /// Blur image the specified channel of the image with the default blur factor (0x1).
    /// </summary>
    /// <param name="channels">The channel(s) that should be blurred.</param>
    /// <exception cref="MagickException"/>
    public void Blur(Channels channels)
    {
      Blur(0.0, 1.0, channels);
    }

    /// <summary>
    /// Blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException"/>
    public void Blur(double radius, double sigma)
    {
      Blur(radius, sigma, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Blur image with specified blur factor and channel.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be blurred.</param>
    /// <exception cref="MagickException"/>
    public void Blur(double radius, double sigma, Channels channels)
    {
      _NativeInstance.Blur(radius, sigma, channels);
    }

    /// <summary>
    /// Border image (add border to image).
    /// </summary>
    /// <param name="size">The size of the border.</param>
    /// <exception cref="MagickException"/>
    public void Border(int size)
    {
      Border(size, size);
    }

    /// <summary>
    /// Border image (add border to image).
    /// </summary>
    /// <param name="width">The width of the border.</param>
    /// <param name="height">The height of the border.</param>
    /// <exception cref="MagickException"/>
    public void Border(int width, int height)
    {
      MagickRectangle rectangle = new MagickRectangle(0, 0, width, height);
      _NativeInstance.Border(rectangle);
    }

    /// <summary>
    /// Changes the brightness and/or contrast of an image. It converts the brightness and
    /// contrast parameters into slope and intercept and calls a polynomical function to apply
    /// to the image.
    /// </summary>
    /// <param name="brightness">The brightness.</param>
    /// <param name="contrast">The contrast.</param>
    /// <exception cref="MagickException"/>
    public void BrightnessContrast(Percentage brightness, Percentage contrast)
    {
      BrightnessContrast(brightness, contrast, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Changes the brightness and/or contrast of an image. It converts the brightness and
    /// contrast parameters into slope and intercept and calls a polynomical function to apply
    /// to the image.
    /// </summary>
    /// <param name="brightness">The brightness.</param>
    /// <param name="contrast">The contrast.</param>
    /// <param name="channels">The channel(s) that should be changed.</param>
    /// <exception cref="MagickException"/>
    public void BrightnessContrast(Percentage brightness, Percentage contrast, Channels channels)
    {
      _NativeInstance.BrightnessContrast(brightness.ToDouble(), contrast.ToDouble(), channels);
    }

    /// <summary>
    /// Uses a multi-stage algorithm to detect a wide range of edges in images.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void CannyEdge()
    {
      CannyEdge(0.0, 1.0, new Percentage(10), new Percentage(30));
    }

    /// <summary>
    /// Uses a multi-stage algorithm to detect a wide range of edges in images.
    /// </summary>
    /// <param name="radius">The radius of the gaussian smoothing filter.</param>
    /// <param name="sigma">The sigma of the gaussian smoothing filter.</param>
    /// <param name="lower">Percentage of edge pixels in the lower threshold.</param>
    /// <param name="upper">Percentage of edge pixels in the upper threshold.</param>
    /// <exception cref="MagickException"/>
    public void CannyEdge(double radius, double sigma, Percentage lower, Percentage upper)
    {
      _NativeInstance.CannyEdge(radius, sigma, lower.ToDouble() / 100, upper.ToDouble() / 100);
    }

    /// <summary>
    /// Charcoal effect image (looks like charcoal sketch).
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Charcoal()
    {
      Charcoal(0.0, 1.0);
    }

    /// <summary>
    /// Charcoal effect image (looks like charcoal sketch).
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException"/>
    public void Charcoal(double radius, double sigma)
    {
      _NativeInstance.Charcoal(radius, sigma);
    }

    /// <summary>
    /// Chop image (remove vertical and horizontal subregion of image).
    /// </summary>
    /// <param name="xOffset">The X offset from origin.</param>
    /// <param name="width">The width of the part to chop horizontally.</param>
    /// <param name="yOffset">The Y offset from origin.</param>
    /// <param name="height">The height of the part to chop vertically.</param>
    /// <exception cref="MagickException"/>
    public void Chop(int xOffset, int width, int yOffset, int height)
    {
      MagickGeometry geometry = new MagickGeometry(xOffset, yOffset, width, height);
      Chop(geometry);
    }

    /// <summary>
    /// Chop image (remove vertical or horizontal subregion of image) using the specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException"/>
    public void Chop(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.Chop(MagickRectangle.FromGeometry(geometry, this));
    }

    /// <summary>
    /// Chop image (remove horizontal subregion of image).
    /// </summary>
    /// <param name="offset">The X offset from origin.</param>
    /// <param name="width">The width of the part to chop horizontally.</param>
    /// <exception cref="MagickException"/>
    public void ChopHorizontal(int offset, int width)
    {
      MagickGeometry geometry = new MagickGeometry(offset, 0, width, 0);
      Chop(geometry);
    }

    /// <summary>
    /// Chop image (remove horizontal subregion of image).
    /// </summary>
    /// <param name="offset">The Y offset from origin.</param>
    /// <param name="height">The height of the part to chop vertically.</param>
    /// <exception cref="MagickException"/>
    public void ChopVertical(int offset, int height)
    {
      MagickGeometry geometry = new MagickGeometry(0, offset, 0, height);
      Chop(geometry);
    }

    /// <summary>
    /// Set each pixel whose value is below zero to zero and any the pixel whose value is above
    /// the quantum range to the quantum range (Quantum.Max) otherwise the pixel value
    /// remains unchanged.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Clamp()
    {
      _NativeInstance.Clamp();
    }

    /// <summary>
    /// Set each pixel whose value is below zero to zero and any the pixel whose value is above
    /// the quantum range to the quantum range (Quantum.Max) otherwise the pixel value
    /// remains unchanged.
    /// </summary>
    /// <param name="channels">The channel(s) to clamp.</param>
    /// <exception cref="MagickException"/>
    public void Clamp(Channels channels)
    {
      _NativeInstance.ClampChannel(channels);
    }

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Clip()
    {
      _NativeInstance.Clip();
    }

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use
    /// clipping path numbered by name.</param>
    /// <param name="inside">Specifies if operations take effect inside or outside the clipping
    /// path</param>
    /// <exception cref="MagickException"/>
    public void Clip(string pathName, bool inside)
    {
      Throw.IfNullOrEmpty(nameof(pathName), pathName);

      _NativeInstance.ClipPath(pathName, inside);
    }

    /// <summary>
    /// Creates a clone of the current image.
    /// </summary>
    public MagickImage Clone()
    {
      return new MagickImage(this);
    }

    /// <summary>
    /// Creates a clone of the current image with the specified geometry.
    /// </summary>
    /// <param name="geometry">The area to clone.</param>
    public MagickImage Clone(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      MagickImage clone = new MagickImage("xc:none", geometry.Width, geometry.Height);
      clone.CopyPixels(this, geometry, 0, 0);

      return clone;
    }

    /// <summary>
    /// Creates a clone of the current image.
    /// </summary>
    /// <param name="width">The width of the area to clone</param>
    /// <param name="height">The height of the area to clone</param>
    public MagickImage Clone(int width, int height)
    {
      return Clone(new MagickGeometry(width, height));
    }

    /// <summary>
    /// Creates a clone of the current image.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width of the area to clone</param>
    /// <param name="height">The height of the area to clone</param>
    public MagickImage Clone(int x, int y, int width, int height)
    {
      return Clone(new MagickGeometry(x, y, width, height));
    }

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException"/>
    public void Clut(MagickImage image)
    {
      Clut(image, PixelInterpolateMethod.Undefined);
    }

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException"/>
    public void Clut(MagickImage image, PixelInterpolateMethod method)
    {
      Clut(image, method, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="channels">The channel(s) to clut.</param>
    /// <exception cref="MagickException"/>
    public void Clut(MagickImage image, PixelInterpolateMethod method, Channels channels)
    {
      Throw.IfNull(nameof(image), image);

      _NativeInstance.Clut(image, method, channels);
    }

    /// <summary>
    /// Sets the alpha channel to the specified color.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <exception cref="MagickException"/>
    public void ColorAlpha(MagickColor color)
    {
      Throw.IfNull(nameof(color), color);

      if (!HasAlpha)
        return;

      using (MagickImage canvas = new MagickImage(color, Width, Height))
      {
        canvas.Composite(this, 0, 0, CompositeOperator.SrcOver);
        SetInstance(new NativeMagickImage(canvas._NativeInstance.Clone()));
      }
    }

    /// <summary>
    /// Applies the color decision list from the specified ASC CDL file.
    /// </summary>
    /// <param name="fileName">The file to read the ASC CDL information from.</param>
    /// <exception cref="MagickException"/>
    public void ColorDecisionList(string fileName)
    {
      Throw.IfNullOrEmpty(nameof(fileName), fileName);

      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      _NativeInstance.ColorDecisionList(filePath);
    }

    /// <summary>
    /// Colorize image with the specified color, using specified percent alpha.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="alpha">The alpha percentage.</param>
    /// <exception cref="MagickException"/>
    public void Colorize(MagickColor color, Percentage alpha)
    {
      Throw.IfNegative(nameof(alpha), alpha);

      Colorize(color, alpha, alpha, alpha);
    }

    /// <summary>
    /// Colorize image with the specified color, using specified percent alpha for red, green,
    /// and blue quantums
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="alphaRed">The alpha percentage for red.</param>
    /// <param name="alphaGreen">The alpha percentage for green.</param>
    /// <param name="alphaBlue">The alpha percentage for blue.</param>
    /// <exception cref="MagickException"/>
    public void Colorize(MagickColor color, Percentage alphaRed, Percentage alphaGreen, Percentage alphaBlue)
    {
      Throw.IfNull(nameof(color), color);
      Throw.IfNegative(nameof(alphaRed), alphaRed);
      Throw.IfNegative(nameof(alphaGreen), alphaGreen);
      Throw.IfNegative(nameof(alphaBlue), alphaBlue);

      string blend = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", alphaRed.ToInt32(), alphaGreen.ToInt32(), alphaBlue.ToInt32());

      _NativeInstance.Colorize(color, blend);
    }

    /// <summary>
    /// Apply a color matrix to the image channels.
    /// </summary>
    /// <param name="matrix">The color matrix to use.</param>
    /// <exception cref="MagickException"/>
    public void ColorMatrix(MagickColorMatrix matrix)
    {
      Throw.IfNull(nameof(matrix), matrix);

      _NativeInstance.ColorMatrix(matrix);
    }

    /// <summary>
    /// Compare current image with another image and returns error information.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <exception cref="MagickException"/>
    public MagickErrorInfo Compare(MagickImage image)
    {
      Throw.IfNull(nameof(image), image);

      if (_NativeInstance.SetColorMetric(image))
        return new MagickErrorInfo();

      return CreateErrorInfo(this);
    }

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <exception cref="MagickException"/>
    public double Compare(MagickImage image, ErrorMetric metric)
    {
      return Compare(image, metric, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <exception cref="MagickException"/>
    public double Compare(MagickImage image, ErrorMetric metric, Channels channels)
    {
      Throw.IfNull(nameof(image), image);

      return _NativeInstance.CompareDistortion(image, metric, channels);
    }

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="difference">The image that will contain the difference.</param>
    /// <exception cref="MagickException"/>
    public double Compare(MagickImage image, ErrorMetric metric, MagickImage difference)
    {
      return Compare(image, metric, difference, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="difference">The image that will contain the difference.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <exception cref="MagickException"/>
    public double Compare(MagickImage image, ErrorMetric metric, MagickImage difference, Channels channels)
    {
      Throw.IfNull(nameof(image), image);
      Throw.IfNull(nameof(difference), difference);

      double distortion;

      IntPtr result = _NativeInstance.Compare(image, metric, channels, out distortion);
      if (result != IntPtr.Zero)
        difference._NativeInstance.Instance = result;

      return distortion;
    }

    /// <summary>
    /// Compares the current instance with another image. Only the size of the image is compared.
    /// </summary>
    /// <param name="other">The object to compare this image with.</param>
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

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image)
    {
      Composite(image, CompositeOperator.In);
    }

    /// <summary>
    /// Compose an image onto another using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, CompositeOperator compose)
    {
      Composite(image, 0, 0, compose);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, CompositeOperator compose, string args)
    {
      Composite(image, 0, 0, compose, args);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, int x, int y)
    {
      Composite(image, x, y, CompositeOperator.In);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, int x, int y, CompositeOperator compose)
    {
      Composite(image, x, y, compose, null);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, int x, int y, CompositeOperator compose, string args)
    {
      Throw.IfNull(nameof(image), image);

      _NativeInstance.SetArtifact("compose:args", args);
      _NativeInstance.Composite(image, x, y, compose);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="offset">The offset from origin.</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, PointD offset)
    {
      Composite(image, offset, CompositeOperator.In);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="offset">The offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, PointD offset, CompositeOperator compose)
    {
      Composite(image, offset, compose, null);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="offset">The offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, PointD offset, CompositeOperator compose, string args)
    {
      Composite(image, (int)offset.X, (int)offset.Y, compose, args);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, Gravity gravity)
    {
      Composite(image, gravity, CompositeOperator.In);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, Gravity gravity, CompositeOperator compose)
    {
      _NativeInstance.SetArtifact("compose:args", null);
      _NativeInstance.CompositeGravity(image, gravity, compose);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException"/>
    public void Composite(MagickImage image, Gravity gravity, CompositeOperator compose, string args)
    {
      Throw.IfNull(nameof(image), image);

      _NativeInstance.SetArtifact("compose:args", args);
      _NativeInstance.CompositeGravity(image, gravity, compose);
    }

    /// <summary>
    /// Determines the connected-components of the image
    /// </summary>
    /// <param name="connectivity">How many neighbors to visit, choose from 4 or 8.</param>
    /// <exception cref="MagickException"/>
    public IEnumerable<ConnectedComponent> ConnectedComponents(int connectivity)
    {
      ConnectedComponentsSettings settings = new ConnectedComponentsSettings();
      settings.Connectivity = connectivity;
      return ConnectedComponents(settings);
    }

    /// <summary>
    /// Determines the connected-components of the image
    /// </summary>
    /// <param name="settings">The settings for this operation.</param>
    /// <exception cref="MagickException"/>
    public IEnumerable<ConnectedComponent> ConnectedComponents(ConnectedComponentsSettings settings)
    {
      Throw.IfNull(nameof(settings), settings);

      if (settings.AreaThreshold != null)
        SetArtifact("connected-components:area-threshold", settings.AreaThreshold.Value.ToString(CultureInfo.InvariantCulture));

      if (settings.MeanColor)
        SetArtifact("connected-components:mean-color", "true");

      IntPtr objects = IntPtr.Zero;

      try
      {
        _NativeInstance.ConnectedComponents(settings.Connectivity, out objects);
        return ConnectedComponent.Create(objects, ColormapSize);
      }
      finally
      {
        ConnectedComponent.DisposeList(objects);
      }
    }

    /// <summary>
    /// Contrast image (enhance intensity differences in image)
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Contrast()
    {
      Contrast(true);
    }

    /// <summary>
    /// Contrast image (enhance intensity differences in image)
    /// </summary>
    /// <param name="enhance">Use true to enhance the contrast and false to reduce the contrast.</param>
    /// <exception cref="MagickException"/>
    public void Contrast(bool enhance)
    {
      _NativeInstance.Contrast(enhance);
    }

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <exception cref="MagickException"/>
    public void ContrastStretch(Percentage blackPoint)
    {
      ContrastStretch(blackPoint, blackPoint);
    }

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <exception cref="MagickException"/>
    public void ContrastStretch(Percentage blackPoint, Percentage whitePoint)
    {
      ContrastStretch(blackPoint, whitePoint, ImageMagick.Channels.Default);
    }

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <param name="channels">The channel(s) to constrast stretch.</param>
    /// <exception cref="MagickException"/>
    public void ContrastStretch(Percentage blackPoint, Percentage whitePoint, Channels channels)
    {
      Throw.IfNegative(nameof(blackPoint), blackPoint);
      Throw.IfNegative(nameof(whitePoint), whitePoint);

      PointD contrast = CalculateContrastStretch(blackPoint, whitePoint);
      _NativeInstance.ContrastStretch(contrast.X, contrast.Y, channels);
    }

    /// <summary>
    /// Convolve image. Applies a user-specified convolution to the image.
    /// </summary>
    /// <param name="convolveMatrix">The convolution matrix.</param>
    /// <exception cref="MagickException"/>
    public void Convolve(ConvolveMatrix convolveMatrix)
    {
      Throw.IfNull(nameof(convolveMatrix), convolveMatrix);

      _NativeInstance.Convolve(convolveMatrix);
    }

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <exception cref="MagickException"/>
    public void CopyPixels(MagickImage source)
    {
      CopyPixels(source, ImageMagick.Channels.All);
    }

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException"/>
    public void CopyPixels(MagickImage source, Channels channels)
    {
      Throw.IfNull(nameof(source), source);

      MagickGeometry geometry = new MagickGeometry(0, 0, Math.Min(source.Width, Width), Math.Min(source.Height, Height));

      CopyPixels(source, geometry, 0, 0, channels);
    }

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <exception cref="MagickException"/>
    public void CopyPixels(MagickImage source, MagickGeometry geometry)
    {
      CopyPixels(source, geometry, ImageMagick.Channels.All);
    }

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException"/>
    public void CopyPixels(MagickImage source, MagickGeometry geometry, Channels channels)
    {
      CopyPixels(source, geometry, 0, 0, channels);
    }

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="offset">The offset to copy the pixels to.</param>
    /// <exception cref="MagickException"/>
    public void CopyPixels(MagickImage source, MagickGeometry geometry, PointD offset)
    {
      CopyPixels(source, geometry, offset, ImageMagick.Channels.All);
    }

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="offset">The offset to start the copy from.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException"/>
    public void CopyPixels(MagickImage source, MagickGeometry geometry, PointD offset, Channels channels)
    {
      CopyPixels(source, geometry, (int)offset.X, (int)offset.Y, channels);
    }

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="x">The X offset to start the copy from.</param>
    /// <param name="y">The Y offset to start the copy from.</param>
    /// <exception cref="MagickException"/>
    public void CopyPixels(MagickImage source, MagickGeometry geometry, int x, int y)
    {
      CopyPixels(source, geometry, x, y, ImageMagick.Channels.All);
    }

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="x">The X offset to copy the pixels to.</param>
    /// <param name="y">The Y offset to copy the pixels to.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException"/>
    public void CopyPixels(MagickImage source, MagickGeometry geometry, int x, int y, Channels channels)
    {
      Throw.IfNull(nameof(source), source);
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.CopyPixels(source, MagickRectangle.FromGeometry(geometry, this), new OffsetInfo(x, y), channels);
    }

    /// <summary>
    /// Crop image (subregion of original image). You should call RePage afterwards unless you
    /// need the Page information.
    /// </summary>
    /// <param name="geometry">The subregion to crop.</param>
    /// <exception cref="MagickException"/>
    public void Crop(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.Crop(MagickRectangle.FromGeometry(geometry, this));
    }

    /// <summary>
    /// Creates tiles of the current image in the specified dimension.
    /// </summary>
    /// <param name="width">The width of the tile.</param>
    /// <param name="height">The height of the tile.</param>
    public IEnumerable<MagickImage> CropToTiles(int width, int height)
    {
      return CropToTiles(new MagickGeometry(width, height));
    }

    /// <summary>
    /// Creates tiles of the current image in the specified dimension.
    /// </summary>
    /// <param name="geometry">The size of the tile.</param>
    public IEnumerable<MagickImage> CropToTiles(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      IntPtr images = _NativeInstance.CropToTiles(MagickGeometry.ToString(geometry));
      return CreateList(images);
    }

    /// <summary>
    /// Displaces an image's colormap by a given number of positions.
    /// </summary>
    /// <param name="amount">Displace the colormap this amount.</param>
    /// <exception cref="MagickException"/>
    public void CycleColormap(int amount)
    {
      _NativeInstance.CycleColormap(amount);
    }

    /// <summary>
    /// Crop image (subregion of original image) using CropPosition.Center. You should call
    /// RePage afterwards unless you need the Page information.
    /// </summary>
    /// <param name="width">The width of the subregion.</param>
    /// <param name="height">The height of the subregion.</param>
    /// <exception cref="MagickException"/>
    public void Crop(int width, int height)
    {
      Crop(width, height, Gravity.Center);
    }

    /// <summary>
    /// Crop image (subregion of original image) using CropPosition.Center. You should call
    /// RePage afterwards unless you need the Page information.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width of the subregion.</param>
    /// <param name="height">The height of the subregion.</param>
    /// <exception cref="MagickException"/>
    public void Crop(int x, int y, int width, int height)
    {
      Crop(new MagickGeometry(x, y, width, height));
    }

    /// <summary>
    /// Crop image (subregion of original image). You should call RePage afterwards unless you
    /// need the Page information.
    /// </summary>
    /// <param name="width">The width of the subregion.</param>
    /// <param name="height">The height of the subregion.</param>
    /// <param name="gravity">The position where the cropping should start from.</param>
    /// <exception cref="MagickException"/>
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

    /// <summary>
    /// Converts cipher pixels to plain pixels.
    /// </summary>
    /// <param name="passphrase">The password that was used to encrypt the image.</param>
    /// <exception cref="MagickException"/>
    public void Decipher(string passphrase)
    {
      Throw.IfNullOrEmpty(nameof(passphrase), passphrase);

      _NativeInstance.Decipher(passphrase);
    }

    /// <summary>
    /// Removes skew from the image. Skew is an artifact that occurs in scanned images because of
    /// the camera being misaligned, imperfections in the scanning or surface, or simply because
    /// the paper was not placed completely flat when scanned. The value of threshold ranges
    /// from 0 to QuantumRange.
    /// </summary>
    /// <param name="threshold">The threshold.</param>
    /// <exception cref="MagickException"/>
    public void Deskew(Percentage threshold)
    {
      Throw.IfNegative(nameof(threshold), threshold);

      _NativeInstance.Deskew(threshold.ToQuantum());
    }

    /// <summary>
    /// Despeckle image (reduce speckle noise).
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Despeckle()
    {
      _NativeInstance.Despeckle();
    }

    /// <summary>
    /// Determines the color type of the image. This method can be used to automatically make the
    /// type GrayScale.
    /// </summary>
    /// <exception cref="MagickException"/>
    public ColorType DetermineColorType()
    {
      return _NativeInstance.DetermineColorType();
    }

    /// <summary>
    /// Disposes the MagickImage instance.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Distorts an image using various distortion methods, by mapping color lookups of the source
    /// image to a new destination image of the same size as the source image.
    /// </summary>
    /// <param name="method">The distortion method to use.</param>
    /// <param name="arguments">An array containing the arguments for the distortion.</param>
    /// <exception cref="MagickException"/>
    public void Distort(DistortMethod method, params double[] arguments)
    {
      Distort(method, false, arguments);
    }

    /// <summary>
    /// Distorts an image using various distortion methods, by mapping color lookups of the source
    /// image to a new destination image usually of the same size as the source image, unless
    /// 'bestfit' is set to true.
    /// </summary>
    /// <param name="method">The distortion method to use.</param>
    /// <param name="bestfit">Attempt to 'bestfit' the size of the resulting image.</param>
    /// <param name="arguments">An array containing the arguments for the distortion.</param>
    /// <exception cref="MagickException"/>
    public void Distort(DistortMethod method, bool bestfit, params double[] arguments)
    {
      Throw.IfNullOrEmpty(nameof(arguments), arguments);

      _NativeInstance.Distort(method, bestfit, arguments, arguments.Length);
    }

    /// <summary>
    /// Draw on image using one or more drawables.
    /// </summary>
    /// <param name="drawables">The drawable(s) to draw on the image.</param>
    /// <exception cref="MagickException"/>
    public void Draw(Drawables drawables)
    {
      Draw((IEnumerable<IDrawable>)drawables);
    }

    /// <summary>
    /// Draw on image using one or more drawables.
    /// </summary>
    /// <param name="drawables">The drawable(s) to draw on the image.</param>
    /// <exception cref="MagickException"/>
    public void Draw(params IDrawable[] drawables)
    {
      Draw((IEnumerable<IDrawable>)drawables);
    }

    /// <summary>
    /// Draw on image using a collection of drawables.
    /// </summary>
    /// <param name="drawables">The drawables to draw on the image.</param>
    /// <exception cref="MagickException"/>
    public void Draw(IEnumerable<IDrawable> drawables)
    {
      Throw.IfNull(nameof(drawables), drawables);

      using (DrawingWand wand = new DrawingWand(this))
      {
        wand.Draw(drawables);
      }
    }

    /// <summary>
    /// Edge image (hilight edges in image).
    /// </summary>
    /// <param name="radius">The radius of the pixel neighborhood.</param>
    /// <exception cref="MagickException"/>
    public void Edge(double radius)
    {
      _NativeInstance.Edge(radius);
    }

    /// <summary>
    /// Emboss image (hilight edges with 3D effect) with default value (0x1).
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Emboss()
    {
      Emboss(0.0, 1.0);
    }

    /// <summary>
    /// Emboss image (hilight edges with 3D effect).
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException"/>
    public void Emboss(double radius, double sigma)
    {
      _NativeInstance.Emboss(radius, sigma);
    }

    /// <summary>
    /// Converts pixels to cipher-pixels.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Encipher(string passphrase)
    {
      Throw.IfNullOrEmpty(nameof(passphrase), passphrase);

      _NativeInstance.Encipher(passphrase);
    }

    /// <summary>
    /// Applies a digital filter that improves the quality of a noisy image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Enhance()
    {
      _NativeInstance.Enhance();
    }

    /// <summary>
    /// Applies a histogram equalization to the image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Equalize()
    {
      _NativeInstance.Equalize();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current image.
    /// </summary>
    /// <param name="obj">The object to compare this image with.</param>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as MagickImage);
    }

    /// <summary>
    /// Determines whether the specified image is equal to the current image.
    /// </summary>
    /// <param name="other">The image to compare this image with.</param>
    public bool Equals(MagickImage other)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (Width != other.Width || Height != other.Height)
        return false;

      return _NativeInstance.Equals(other);
    }

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateFunction">The function.</param>
    /// <param name="arguments">The arguments for the function.</param>
    /// <exception cref="MagickException"/>
    public void Evaluate(Channels channels, EvaluateFunction evaluateFunction, params double[] arguments)
    {
      Throw.IfNullOrEmpty(nameof(arguments), arguments);

      _NativeInstance.EvaluateFunction(channels, evaluateFunction, arguments, arguments.Length);
    }

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="MagickException"/>
    public void Evaluate(Channels channels, EvaluateOperator evaluateOperator, double value)
    {
      _NativeInstance.EvaluateOperator(channels, evaluateOperator, value);
    }

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="percentage">The value.</param>
    /// <exception cref="MagickException"/>
    public void Evaluate(Channels channels, EvaluateOperator evaluateOperator, Percentage percentage)
    {
      Evaluate(channels, evaluateOperator, percentage.ToQuantum());
    }

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="MagickException"/>
    public void Evaluate(Channels channels, MagickGeometry geometry, EvaluateOperator evaluateOperator, double value)
    {
      Throw.IfNull(nameof(geometry), geometry);
      Throw.IfTrue(nameof(geometry), geometry.IsPercentage, "Percentage is not supported.");

      _NativeInstance.EvaluateGeometry(channels, MagickRectangle.FromGeometry(geometry, this), evaluateOperator, value);
    }

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="percentage">The value.</param>
    /// <exception cref="MagickException"/>
    public void Evaluate(Channels channels, MagickGeometry geometry, EvaluateOperator evaluateOperator, Percentage percentage)
    {
      Evaluate(channels, geometry, evaluateOperator, percentage.ToQuantum());
    }

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <exception cref="MagickException"/>
    public void Extent(int width, int height)
    {
      Extent(new MagickGeometry(width, height));
    }

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <exception cref="MagickException"/>
    public void Extent(int x, int y, int width, int height)
    {
      Extent(new MagickGeometry(x, y, width, height));
    }

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException"/>
    public void Extent(int width, int height, MagickColor backgroundColor)
    {
      MagickGeometry geometry = new MagickGeometry(width, height);
      Extent(geometry, backgroundColor);
    }

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException"/>
    public void Extent(int width, int height, Gravity gravity)
    {
      MagickGeometry geometry = new MagickGeometry(width, height);
      Extent(geometry, gravity);
    }

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException"/>
    public void Extent(int width, int height, Gravity gravity, MagickColor backgroundColor)
    {
      MagickGeometry geometry = new MagickGeometry(width, height);
      Extent(geometry, gravity, backgroundColor);
    }

    /// <summary>
    /// Extend the image as defined by the rectangle.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <exception cref="MagickException"/>
    public void Extent(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      geometry.IgnoreAspectRatio = true;
      _NativeInstance.Extent(MagickGeometry.ToString(geometry));
    }

    /// <summary>
    /// Extend the image as defined by the geometry.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException"/>
    public void Extent(MagickGeometry geometry, MagickColor backgroundColor)
    {
      Throw.IfNull(nameof(backgroundColor), backgroundColor);

      BackgroundColor = backgroundColor;
      Extent(geometry);
    }

    /// <summary>
    /// Extend the image as defined by the geometry.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException"/>
    public void Extent(MagickGeometry geometry, Gravity gravity)
    {
      Throw.IfNull(nameof(geometry), geometry);

      geometry.IgnoreAspectRatio = true;
      _NativeInstance.ExtentGravity(MagickGeometry.ToString(geometry), gravity);
    }

    /// <summary>
    /// Extend the image as defined by the geometry.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException"/>
    public void Extent(MagickGeometry geometry, Gravity gravity, MagickColor backgroundColor)
    {
      Throw.IfNull(nameof(backgroundColor), backgroundColor);

      BackgroundColor = backgroundColor;
      Extent(geometry, gravity);
    }

    /// <summary>
    /// Flip image (reflect each scanline in the vertical direction).
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Flip()
    {
      _NativeInstance.Flip();
    }

    /// <summary>
    /// Floodfill pixels matching color (within fuzz factor) of target pixel(x,y) with replacement
    /// alpha value using method.
    /// </summary>
    /// <param name="alpha">The alpha to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void FloodFill(QuantumType alpha, int x, int y)
    {
      FloodFill(alpha, x, y, false);
    }

    /// <summary>
    /// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException"/>
    public void FloodFill(MagickColor color, int x, int y)
    {
      FloodFill(color, x, y, false);
    }

    /// <summary>
    /// Flood-fill color across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException"/>
    public void FloodFill(MagickColor color, int x, int y, MagickColor target)
    {
      FloodFill(color, x, y, target, false);
    }

    /// <summary>
    /// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="coordinate">The position of the pixel.</param>
    /// <exception cref="MagickException"/>
    public void FloodFill(MagickColor color, PointD coordinate)
    {
      FloodFill(color, (int)coordinate.X, (int)coordinate.Y, false);
    }

    /// <summary>
    /// Flood-fill color across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="coordinate">The position of the pixel.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException"/>
    public void FloodFill(MagickColor color, PointD coordinate, MagickColor target)
    {
      FloodFill(color, (int)coordinate.X, (int)coordinate.Y, target, false);
    }

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException"/>
    public void FloodFill(MagickImage image, int x, int y)
    {
      FloodFill(image, x, y, false);
    }

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException"/>
    public void FloodFill(MagickImage image, int x, int y, MagickColor target)
    {
      FloodFill(image, x, y, target, false);
    }

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="coordinate">The position of the pixel.</param>
    /// <exception cref="MagickException"/>
    public void FloodFill(MagickImage image, PointD coordinate)
    {
      FloodFill(image, (int)coordinate.X, (int)coordinate.Y, false);
    }

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="coordinate">The position of the pixel.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException"/>
    public void FloodFill(MagickImage image, PointD coordinate, MagickColor target)
    {
      FloodFill(image, (int)coordinate.X, (int)coordinate.Y, target, false);
    }

    /// <summary>
    /// Flop image (reflect each scanline in the horizontal direction).
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Flop()
    {
      _NativeInstance.Flop();
    }

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <exception cref="MagickException"/>
    public TypeMetric FontTypeMetrics(string text)
    {
      return FontTypeMetrics(text, false);
    }

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <param name="ignoreNewLines">Specifies if new lines should be ignored.</param>
    /// <exception cref="MagickException"/>
    public TypeMetric FontTypeMetrics(string text, bool ignoreNewLines)
    {
      Throw.IfNullOrEmpty(nameof(text), text);

      DrawingSettings settings = Settings.Drawing;

      settings.Text = text;
      IntPtr result = _NativeInstance.FontTypeMetrics(settings, ignoreNewLines);
      settings.Text = null;
      return TypeMetric.CreateInstance(result);
    }

    /// <summary>
    /// Formats the specified expression, more info here: http://www.imagemagick.org/script/escape.php.
    /// </summary>
    /// <exception cref="MagickException"/>
    public string FormatExpression(string expression)
    {
      Throw.IfNullOrEmpty(nameof(expression), expression);

      return _NativeInstance.FormatExpression(Settings, expression);
    }

    /// <summary>
    /// Frame image with the default geometry (25x25+6+6).
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Frame()
    {
      Frame(new MagickGeometry(6, 6, 25, 25));
    }

    /// <summary>
    /// Frame image with the specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry of the frame.</param>
    /// <exception cref="MagickException"/>
    public void Frame(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.Frame(MagickRectangle.FromGeometry(geometry, this));
    }

    /// <summary>
    /// Frame image with the specified with and height.
    /// </summary>
    /// <param name="width">The width of the frame.</param>
    /// <param name="height">The height of the frame.</param>
    /// <exception cref="MagickException"/>
    public void Frame(int width, int height)
    {
      Frame(new MagickGeometry(6, 6, width, height));
    }

    /// <summary>
    /// Frame image with the specified with, height, innerBevel and outerBevel.
    /// </summary>
    /// <param name="width">The width of the frame.</param>
    /// <param name="height">The height of the frame.</param>
    /// <param name="innerBevel">The inner bevel of the frame.</param>
    /// <param name="outerBevel">The outer bevel of the frame.</param>
    /// <exception cref="MagickException"/>
    public void Frame(int width, int height, int innerBevel, int outerBevel)
    {
      Frame(new MagickGeometry(innerBevel, outerBevel, width, height));
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified base64 string.
    /// </summary>
    /// <param name="value">The base64 string to load the image from.</param>
    public static MagickImage FromBase64(string value)
    {
      byte[] data = Convert.FromBase64String(value);
      return new MagickImage(data);
    }

    /// <summary>
    /// Applies a mathematical expression to the image.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <exception cref="MagickException"/>
    public void Fx(string expression)
    {
      Fx(expression, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Applies a mathematical expression to the image.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <param name="channels">The channel(s) to apply the expression to.</param>
    /// <exception cref="MagickException"/>
    public void Fx(string expression, Channels channels)
    {
      Throw.IfNullOrEmpty(nameof(expression), expression);

      _NativeInstance.Fx(expression, channels);
    }

    /// <summary>
    /// Gamma correct image.
    /// </summary>
    /// <param name="gamma">The image gamma.</param>
    /// <exception cref="MagickException"/>
    public void GammaCorrect(double gamma)
    {
      GammaCorrect(gamma, ImageMagick.Channels.Default);
    }

    /// <summary>
    /// Gamma correct image.
    /// </summary>
    /// <param name="gamma">The image gamma for the channel.</param>
    /// <param name="channels">The channel(s) to gamma correct.</param>
    /// <exception cref="MagickException"/>
    public void GammaCorrect(double gamma, Channels channels)
    {
      _NativeInstance.GammaCorrect(gamma, channels);
    }

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <param name="sigma">The standard deviation of the gaussian bell curve.</param>
    /// <exception cref="MagickException"/>
    public void GaussianBlur(double radius, double sigma)
    {
      GaussianBlur(radius, sigma, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <param name="sigma">The standard deviation of the gaussian bell curve.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException"/>
    public void GaussianBlur(double radius, double sigma, Channels channels)
    {
      _NativeInstance.GaussianBlur(radius, sigma, channels);
    }

    /// <summary>
    /// Retrieve the 8bim profile from the image.
    /// </summary>
    /// <exception cref="MagickException"/>
    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
    public EightBimProfile Get8BimProfile()
    {
      StringInfo info = _NativeInstance.GetProfile("8bim");
      if (info == null || info.Datum == null)
        return null;

      return new EightBimProfile(this, info.Datum);
    }

    /// <summary>
    /// Returns the value of a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    public string GetAttribute(string name)
    {
      Throw.IfNullOrEmpty(nameof(name), name);

      return _NativeInstance.GetAttribute(name);
    }

    /// <summary>
    /// Get color at colormap position index.
    /// </summary>
    /// <param name="index">The position index.</param>
    /// <exception cref="MagickException"/>
    public MagickColor GetColormap(int index)
    {
      return _NativeInstance.GetColormap(index);
    }

    /// <summary>
    /// Retrieve the color profile from the image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public ColorProfile GetColorProfile()
    {
      ColorProfile profile = GetColorProfile("icc");

      if (profile != null)
        return profile;

      return GetColorProfile("icm");
    }

    /// <summary>
    /// Returns the value of the artifact with the specified name.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    public string GetArtifact(string name)
    {
      Throw.IfNullOrEmpty(nameof(name), name);

      return _NativeInstance.GetArtifact(name);
    }

    /// <summary>
    /// Retrieve the exif profile from the image.
    /// </summary>
    /// <exception cref="MagickException"/>
    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
    public ExifProfile GetExifProfile()
    {
      StringInfo info = _NativeInstance.GetProfile("exif");
      if (info == null || info.Datum == null)
        return null;

      return new ExifProfile(info.Datum);
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    public override int GetHashCode()
    {
      return
        Width.GetHashCode() ^
        Height.GetHashCode() ^
        Signature.GetHashCode();
    }

    /// <summary>
    /// Retrieve the iptc profile from the image.
    /// </summary>
    /// <exception cref="MagickException"/>
    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
    public IptcProfile GetIptcProfile()
    {
      StringInfo info = _NativeInstance.GetProfile("iptc");
      if (info == null || info.Datum == null)
        return null;

      return new IptcProfile(info.Datum);
    }

    /// <summary>
    /// Returns a pixel collection that can be used to read or modify the pixels of this image.
    /// </summary>
    /// <exception cref="MagickException"/>
    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
    public PixelCollection GetPixels()
    {
      if (Settings.Ping)
        throw new InvalidOperationException("Image contains no pixel data.");

      return new PixelCollection(this);
    }

    /// <summary>
    /// Retrieve a named profile from the image.
    /// </summary>
    /// <param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
    /// <exception cref="MagickException"/>
    public ImageProfile GetProfile(string name)
    {
      Throw.IfNullOrEmpty(nameof(name), name);

      StringInfo info = _NativeInstance.GetProfile(name);
      if (info == null || info.Datum == null)
        return null;

      return new ImageProfile(name, info.Datum);
    }

    /// <summary>
    /// Retrieve the xmp profile from the image.
    /// </summary>
    /// <exception cref="MagickException"/>
    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
    public XmpProfile GetXmpProfile()
    {
      StringInfo info = _NativeInstance.GetProfile("xmp");
      if (info == null || info.Datum == null)
        return null;

      return new XmpProfile(info.Datum);
    }

    /// <summary>
    /// Converts the colors in the image to gray.
    /// </summary>
    /// <param name="method">The pixel intensity method to use.</param>
    /// <exception cref="MagickException"/>
    public void Grayscale(PixelIntensityMethod method)
    {
      _NativeInstance.Grayscale(method);
    }

    /// <summary>
    /// Apply a color lookup table (Hald CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException"/>
    public void HaldClut(MagickImage image)
    {
      Throw.IfNull(nameof(image), image);

      _NativeInstance.HaldClut(image);
    }

    /// <summary>
    /// Creates a color color histogram.
    /// </summary>
    /// <exception cref="MagickException"/>
    public Dictionary<MagickColor, int> Histogram()
    {
      IntPtr result = IntPtr.Zero;
      try
      {
        UIntPtr length;
        result = _NativeInstance.Histogram(out length);
        return MagickColorCollection.ToDictionary(result, (int)length);
      }
      finally
      {
        MagickColorCollection.DisposeList(result);
      }
    }

    /// <summary>
    /// Identifies lines in the image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void HoughLine()
    {
      HoughLine(0, 0, 40);
    }

    /// <summary>
    /// Identifies lines in the image.
    /// </summary>
    /// <param name="width">Find line pairs as local maxima in this neighborhood.</param>
    /// <param name="height">Find line pairs as local maxima in this neighborhood.</param>
    /// <param name="threshold">The line count threshold.</param>
    /// <exception cref="MagickException"/>
    public void HoughLine(int width, int height, int threshold)
    {
      _NativeInstance.HoughLine(width, height, threshold);
    }

    /// <summary>
    /// Implode image (special effect).
    /// </summary>
    /// <param name="amount">The extent of the implosion.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException"/>
    public void Implode(double amount, PixelInterpolateMethod method)
    {
      _NativeInstance.Implode(amount, method);
    }

    /// <summary>
    /// Floodfill pixels not matching color (within fuzz factor) of target pixel(x,y) with
    /// replacement alpha value using method.
    /// </summary>
    /// <param name="alpha">The alpha to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void InverseFloodFill(QuantumType alpha, int x, int y)
    {
      FloodFill(alpha, x, y, true);
    }

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException"/>
    public void InverseFloodFill(MagickColor color, int x, int y)
    {
      FloodFill(color, x, y, true);
    }

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException"/>
    public void InverseFloodFill(MagickColor color, int x, int y, MagickColor target)
    {
      FloodFill(color, x, y, target, true);
    }

    /// <summary>
    /// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="coordinate">The position of the pixel.</param>
    /// <exception cref="MagickException"/>
    public void InverseFloodFill(MagickColor color, PointD coordinate)
    {
      FloodFill(color, (int)coordinate.X, (int)coordinate.Y, true);
    }

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="coordinate">The position of the pixel.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException"/>
    public void InverseFloodFill(MagickColor color, PointD coordinate, MagickColor target)
    {
      FloodFill(color, (int)coordinate.X, (int)coordinate.Y, target, true);
    }

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException"/>
    public void InverseFloodFill(MagickImage image, int x, int y)
    {
      FloodFill(image, x, y, true);
    }

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException"/>
    public void InverseFloodFill(MagickImage image, int x, int y, MagickColor target)
    {
      FloodFill(image, x, y, target, true);
    }

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="coordinate">The position of the pixel.</param>
    /// <exception cref="MagickException"/>
    public void InverseFloodFill(MagickImage image, PointD coordinate)
    {
      FloodFill(image, (int)coordinate.X, (int)coordinate.Y, true);
    }

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="coordinate">The position of the pixel.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException"/>
    public void InverseFloodFill(MagickImage image, PointD coordinate, MagickColor target)
    {
      FloodFill(image, (int)coordinate.X, (int)coordinate.Y, target, true);
    }

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void InverseLevel(QuantumType blackPoint, QuantumType whitePoint)
    {
      InverseLevel(blackPoint, whitePoint, 1.0);
    }

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException"/>
    public void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage)
    {
      InverseLevel(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum());
    }

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void InverseLevel(QuantumType blackPoint, QuantumType whitePoint, Channels channels)
    {
      InverseLevel(blackPoint, whitePoint, 1.0, channels);
    }

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
    public void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, Channels channels)
    {
      InverseLevel(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum(), channels);
    }

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void InverseLevel(QuantumType blackPoint, QuantumType whitePoint, double midpoint)
    {
      _NativeInstance.Levelize(blackPoint, whitePoint, midpoint, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
    /// <exception cref="MagickException"/>
    public void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, double midpoint)
    {
      InverseLevel(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum(), midpoint);
    }

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void InverseLevel(QuantumType blackPoint, QuantumType whitePoint, double midpoint, Channels channels)
    {
      _NativeInstance.Levelize(blackPoint, whitePoint, midpoint, channels);
    }

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
    public void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, double midpoint, Channels channels)
    {
      InverseLevel(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum(), midpoint, channels);
    }

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from</param>
    /// <param name="whiteColor">The color to map white to/from</param>
    /// <exception cref="MagickException"/>
    public void InverseLevelColors(MagickColor blackColor, MagickColor whiteColor)
    {
      LevelColors(blackColor, whiteColor, true);
    }

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from</param>
    /// <param name="whiteColor">The color to map white to/from</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
    public void InverseLevelColors(MagickColor blackColor, MagickColor whiteColor, Channels channels)
    {
      LevelColors(blackColor, whiteColor, channels, true);
    }

    /// <summary>
    /// Changes any pixel that does not match the target with the color defined by fill.
    /// </summary>
    /// <param name="target">The color to replace.</param>
    /// <param name="fill">The color to replace opaque color with.</param>
    /// <exception cref="MagickException"/>
    public void InverseOpaque(MagickColor target, MagickColor fill)
    {
      Opaque(target, fill, true);
    }

    /// <summary>
    /// Add alpha channel to image, setting pixels that don't match the specified color to transparent.
    /// </summary>
    /// <param name="color">The color that should not be made transparent.</param>
    /// <exception cref="MagickException"/>
    public void InverseTransparent(MagickColor color)
    {
      Throw.IfNull(nameof(color), color);

      _NativeInstance.Transparent(color, true);
    }

    /// <summary>
    /// Add alpha channel to image, setting pixels that don't lie in between the given two colors to
    /// transparent.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void InverseTransparentChroma(MagickColor colorLow, MagickColor colorHigh)
    {
      Throw.IfNull(nameof(colorLow), colorLow);
      Throw.IfNull(nameof(colorHigh), colorHigh);

      _NativeInstance.TransparentChroma(colorLow, colorHigh, true);
    }

    /// <summary>
    /// An edge preserving noise reduction filter.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Kuwahara()
    {
      Kuwahara(0.0, 1.0);
    }

    /// <summary>
    /// An edge preserving noise reduction filter.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException"/>
    public void Kuwahara(double radius, double sigma)
    {
      _NativeInstance.Kuwahara(radius, sigma);
    }

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void Level(QuantumType blackPoint, QuantumType whitePoint)
    {
      Level(blackPoint, whitePoint, 1.0);
    }

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException"/>
    public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage)
    {
      Level(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum());
    }

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void Level(QuantumType blackPoint, QuantumType whitePoint, Channels channels)
    {
      Level(blackPoint, whitePoint, 1.0, channels);
    }

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
    public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, Channels channels)
    {
      Level(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum(), channels);
    }

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void Level(QuantumType blackPoint, QuantumType whitePoint, double gamma)
    {
      Level(blackPoint, whitePoint, gamma, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
    /// <exception cref="MagickException"/>
    public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma)
    {
      Level(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum(), gamma);
    }

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void Level(QuantumType blackPoint, QuantumType whitePoint, double gamma, Channels channels)
    {
      _NativeInstance.Level(blackPoint, whitePoint, gamma, channels);
    }

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
    public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma, Channels channels)
    {
      Level(blackPointPercentage.ToQuantum(), whitePointPercentage.ToQuantum(), gamma, channels);
    }

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from</param>
    /// <param name="whiteColor">The color to map white to/from</param>
    /// <exception cref="MagickException"/>
    public void LevelColors(MagickColor blackColor, MagickColor whiteColor)
    {
      LevelColors(blackColor, whiteColor, false);
    }

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from</param>
    /// <param name="whiteColor">The color to map white to/from</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException"/>
    public void LevelColors(MagickColor blackColor, MagickColor whiteColor, Channels channels)
    {
      LevelColors(blackColor, whiteColor, channels, false);
    }

    /// <summary>
    /// Discards any pixels below the black point and above the white point and levels the remaining pixels.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <exception cref="MagickException"/>
    public void LinearStretch(Percentage blackPoint, Percentage whitePoint)
    {
      Throw.IfNegative(nameof(blackPoint), blackPoint);
      Throw.IfNegative(nameof(whitePoint), whitePoint);

      _NativeInstance.LinearStretch(blackPoint.ToQuantum(), whitePoint.ToQuantum());
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException"/>
    public void LiquidRescale(int width, int height)
    {
      MagickGeometry geometry = new MagickGeometry(width, height);
      LiquidRescale(geometry);
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException"/>
    public void LiquidRescale(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.LiquidRescale(MagickGeometry.ToString(geometry));
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException"/>
    public void LiquidRescale(Percentage percentage)
    {
      LiquidRescale(percentage, percentage);
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException"/>
    public void LiquidRescale(Percentage percentageWidth, Percentage percentageHeight)
    {
      Throw.IfNegative(nameof(percentageWidth), percentageWidth);
      Throw.IfNegative(nameof(percentageHeight), percentageHeight);

      MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
      LiquidRescale(geometry);
    }

    /// <summary>
    /// Local contrast enhancement.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="strength">The strength of the blur mask.</param>
    /// <exception cref="MagickException"/>
    public void LocalContrast(double radius, Percentage strength)
    {
      _NativeInstance.LocalContrast(radius, strength.ToDouble());
    }

    /// <summary>
    /// Lower image (lighten or darken the edges of an image to give a 3-D lowered effect).
    /// </summary>
    /// <param name="size">The size of the edges.</param>
    /// <exception cref="MagickException"/>
    public void Lower(int size)
    {
      _NativeInstance.RaiseOrLower(size, false);
    }

    /// <summary>
    /// Magnify image by integral size.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Magnify()
    {
      _NativeInstance.Magnify();
    }

    /// <summary>
    /// Remap image colors with closest color from the specified colors.
    /// </summary>
    /// <param name="colors">The colors to use.</param>
    /// <exception cref="MagickException"/>
    public MagickErrorInfo Map(IEnumerable<MagickColor> colors)
    {
      Throw.IfNull(nameof(colors), colors);

      return Map(colors, new QuantizeSettings());
    }

    /// <summary>
    /// Remap image colors with closest color from the specified colors.
    /// </summary>
    /// <param name="colors">The colors to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <exception cref="MagickException"/>
    public MagickErrorInfo Map(IEnumerable<MagickColor> colors, QuantizeSettings settings)
    {
      Throw.IfNull(nameof(colors), colors);

      List<MagickColor> colorList = new List<MagickColor>(colors);
      if (colorList.Count == 0)
        throw new ArgumentException("Value cannot be empty.", nameof(colors));

      using (MagickImageCollection images = new MagickImageCollection())
      {
        foreach (MagickColor color in colorList)
          images.Add(new MagickImage(color, 1, 1));

        using (MagickImage image = images.AppendHorizontally())
        {
          return Map(image, settings);
        }
      }
    }

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException"/>
    public MagickErrorInfo Map(MagickImage image)
    {
      return Map(image, new QuantizeSettings());
    }

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <exception cref="MagickException"/>
    public MagickErrorInfo Map(MagickImage image, QuantizeSettings settings)
    {
      Throw.IfNull(nameof(image), image);
      Throw.IfNull(nameof(settings), settings);

      if (_NativeInstance.Map(image, settings))
        return new MagickErrorInfo();

      return CreateErrorInfo(this);
    }

    /// <summary>
    /// Filter image by replacing each pixel component with the median color in a circular neighborhood.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void MedianFilter()
    {
      MedianFilter(0);
    }

    /// <summary>
    /// Filter image by replacing each pixel component with the median color in a circular neighborhood.
    /// </summary>
    /// <param name="radius">The radius of the pixel neighborhood.</param>
    /// <exception cref="MagickException"/>
    public void MedianFilter(int radius)
    {
      Statistic(StatisticType.Median, radius, radius);
    }

    /// <summary>
    /// Reduce image by integral size.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Minify()
    {
      _NativeInstance.Minify();
    }

    /// <summary>
    /// Modulate percent brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <exception cref="MagickException"/>
    public void Modulate(Percentage brightness)
    {
      Modulate(brightness, new Percentage(100), new Percentage(100));
    }

    /// <summary>
    /// Modulate percent saturation and brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <param name="saturation">The saturation percentage.</param>
    /// <exception cref="MagickException"/>
    public void Modulate(Percentage brightness, Percentage saturation)
    {
      Modulate(brightness, saturation, new Percentage(100));
    }

    /// <summary>
    /// Modulate percent hue, saturation, and brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <param name="saturation">The saturation percentage.</param>
    /// <param name="hue">The hue percentage.</param>
    /// <exception cref="MagickException"/>
    public void Modulate(Percentage brightness, Percentage saturation, Percentage hue)
    {
      Throw.IfNegative(nameof(brightness), brightness);
      Throw.IfNegative(nameof(saturation), saturation);
      Throw.IfNegative(nameof(hue), hue);

      string modulate = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", brightness.ToDouble(), saturation.ToDouble(), hue.ToDouble());

      _NativeInstance.Modulate(modulate);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, Kernel kernel)
    {
      Morphology(method, kernel, "");
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, Kernel kernel, Channels channels)
    {
      Morphology(method, kernel, "", channels);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, Kernel kernel, Channels channels, int iterations)
    {
      Morphology(method, kernel, "", channels, iterations);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, Kernel kernel, int iterations)
    {
      Morphology(method, kernel, "", ImageMagick.Channels.Composite, iterations);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, Kernel kernel, string arguments)
    {
      Morphology(method, kernel, arguments, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, Kernel kernel, string arguments, Channels channels)
    {
      Morphology(method, kernel, arguments, channels, 1);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, Kernel kernel, string arguments, Channels channels, int iterations)
    {
      string newKernel = EnumHelper.GetName(kernel) + ":" + arguments;

      Morphology(method, newKernel, channels, iterations);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, Kernel kernel, string arguments, int iterations)
    {
      Morphology(method, kernel, arguments, ImageMagick.Channels.Composite, iterations);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, string userKernel)
    {
      Morphology(method, userKernel, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, string userKernel, Channels channels)
    {
      Morphology(method, userKernel, channels, 1);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, string userKernel, Channels channels, int iterations)
    {
      _NativeInstance.Morphology(method, userKernel, channels, iterations);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologyMethod method, string userKernel, int iterations)
    {
      Morphology(method, userKernel, ImageMagick.Channels.Composite, iterations);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology settings.
    /// </summary>
    /// <param name="settings">The morphology settings.</param>
    /// <exception cref="MagickException"/>
    public void Morphology(MorphologySettings settings)
    {
      Throw.IfNull(nameof(settings), settings);

      if (settings.ConvolveBias != null)
        SetArtifact("convolve:bias", settings.ConvolveBias.ToString());

      if (settings.ConvolveScale != null)
        SetArtifact("convolve:scale", settings.ConvolveScale.ToString());

      if (!string.IsNullOrEmpty(settings.UserKernel))
        Morphology(settings.Method, settings.UserKernel, settings.Channels, settings.Iterations);
      else
        Morphology(settings.Method, settings.Kernel, settings.KernelArguments, settings.Channels, settings.Iterations);
    }

    /// <summary>
    /// Returns the normalized moments of one or more image channels.
    /// </summary>
    /// <exception cref="MagickException"/>
    public Moments Moments()
    {
      IntPtr list = _NativeInstance.Moments();
      try
      {
        return new Moments(this, list);
      }
      finally
      {
        ImageMagick.Moments.DisposeList(list);
      }
    }

    /// <summary>
    /// Motion blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="angle">The angle the object appears to be comming from (zero degrees is from the right).</param>
    /// <exception cref="MagickException"/>
    public void MotionBlur(double radius, double sigma, double angle)
    {
      _NativeInstance.MotionBlur(radius, sigma, angle);
    }

    /// <summary>
    /// Negate colors in image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Negate()
    {
      Negate(false);
    }

    /// <summary>
    /// Negate colors in image.
    /// </summary>
    /// <param name="onlyGrayscale">Use true to negate only the grayscale colors.</param>
    /// <exception cref="MagickException"/>
    public void Negate(bool onlyGrayscale)
    {
      Negate(onlyGrayscale, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Negate colors in image for the specified channel.
    /// </summary>
    /// <param name="channels">The channel(s) that should be negated.</param>
    /// <param name="onlyGrayscale">Use true to negate only the grayscale colors.</param>
    /// <exception cref="MagickException"/>
    public void Negate(bool onlyGrayscale, Channels channels)
    {
      _NativeInstance.Negate(onlyGrayscale, channels);
    }

    /// <summary>
    /// Negate colors in image for the specified channel.
    /// </summary>
    /// <param name="channels">The channel(s) that should be negated.</param>
    /// <exception cref="MagickException"/>
    public void Negate(Channels channels)
    {
      Negate(false, channels);
    }

    /// <summary>
    /// Normalize image (increase contrast by normalizing the pixel values to span the full range
    /// of color values)
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Normalize()
    {
      _NativeInstance.Normalize();
    }

    /// <summary>
    /// Oilpaint image (image looks like oil painting)
    /// </summary>
    public void OilPaint()
    {
      OilPaint(3.0, 1.0);
    }

    /// <summary>
    /// Oilpaint image (image looks like oil painting)
    /// </summary>
    /// <param name="radius">The radius of the circular neighborhood.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException"/>
    public void OilPaint(double radius, double sigma)
    {
      _NativeInstance.OilPaint(radius, sigma);
    }

    /// <summary>
    /// Changes any pixel that matches target with the color defined by fill.
    /// </summary>
    /// <param name="target">The color to replace.</param>
    /// <param name="fill">The color to replace opaque color with.</param>
    /// <exception cref="MagickException"/>
    public void Opaque(MagickColor target, MagickColor fill)
    {
      Opaque(target, fill, false);
    }

    /// <summary>
    /// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
    /// multiple intensity levels.
    /// </summary>
    /// <param name="thresholdMap">A string containing the name of the threshold dither map to use,
    /// followed by zero or more numbers representing the number of color levels tho dither between.</param>
    /// <exception cref="MagickException"/>
    public void OrderedDither(string thresholdMap)
    {
      Throw.IfNullOrEmpty(nameof(thresholdMap), thresholdMap);

      OrderedDither(thresholdMap, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
    /// multiple intensity levels.
    /// </summary>
    /// <param name="thresholdMap">A string containing the name of the threshold dither map to use,
    /// followed by zero or more numbers representing the number of color levels tho dither between.</param>
    /// <param name="channels">The channel(s) to dither.</param>
    /// <exception cref="MagickException"/>
    public void OrderedDither(string thresholdMap, Channels channels)
    {
      Throw.IfNullOrEmpty(nameof(thresholdMap), thresholdMap);

      _NativeInstance.OrderedDither(thresholdMap, channels);
    }

    /// <summary>
    /// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
    /// otherwise the pixel value remains unchanged.
    /// </summary>
    /// <param name="epsilon">The epsilon threshold.</param>
    /// <exception cref="MagickException"/>
    public void Perceptible(double epsilon)
    {
      Perceptible(epsilon, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
    /// otherwise the pixel value remains unchanged.
    /// </summary>
    /// <param name="epsilon">The epsilon threshold.</param>
    /// <param name="channels">The channel(s) to perceptible.</param>
    /// <exception cref="MagickException"/>
    public void Perceptible(double epsilon, Channels channels)
    {
      _NativeInstance.Perceptible(epsilon, channels);
    }

    /// <summary>
    /// Returns the perceptual hash of this image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public PerceptualHash PerceptualHash()
    {
      IntPtr list = _NativeInstance.PerceptualHash();

      try
      {
        PerceptualHash hash = new PerceptualHash(this, list);
        if (!hash.Isvalid)
          return null;

        return hash;
      }
      finally
      {
        ImageMagick.PerceptualHash.DisposeList(list);
      }
    }

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <exception cref="MagickException"/>
    public void Ping(byte[] data)
    {
      Read(data, null, true);
    }

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException"/>
    public void Ping(FileInfo file)
    {
      Throw.IfNull(nameof(file), file);

      Ping(file.FullName);
    }

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public void Ping(string fileName)
    {
      Read(fileName, null, true);
    }

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public void Ping(Stream stream)
    {
      Read(StreamHelper.ToByteArray(stream), null, true);
    }

    /// <summary>
    /// Simulates a Polaroid picture.
    /// </summary>
    /// <param name="caption">The caption to put on the image.</param>
    /// <param name="angle">The angle of image.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException"/>
    public void Polaroid(string caption, double angle, PixelInterpolateMethod method)
    {
      Throw.IfNull(nameof(caption), caption);

      _NativeInstance.Polaroid(Settings.Drawing, caption, angle, method);
    }

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <exception cref="MagickException"/>
    public void Posterize(int levels)
    {
      Posterize(levels, DitherMethod.No);
    }

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="method">Dither method to use.</param>
    /// <exception cref="MagickException"/>
    public void Posterize(int levels, DitherMethod method)
    {
      Posterize(levels, method, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="method">Dither method to use.</param>
    /// <param name="channels">The channel(s) to posterize.</param>
    /// <exception cref="MagickException"/>
    public void Posterize(int levels, DitherMethod method, Channels channels)
    {
      _NativeInstance.Posterize(levels, method, channels);
    }

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="channels">The channel(s) to posterize.</param>
    /// <exception cref="MagickException"/>
    public void Posterize(int levels, Channels channels)
    {
      Posterize(levels, DitherMethod.No, channels);
    }

    /// <summary>
    /// Sets an internal option to preserve the color type.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void PreserveColorType()
    {
      ColorType = ColorType;
      SetAttribute("colorspace:auto-grayscale", "false");
    }

    /// <summary>
    /// Quantize image (reduce number of colors).
    /// </summary>
    /// <param name="settings">Quantize settings.</param>
    /// <exception cref="MagickException"/>
    public MagickErrorInfo Quantize(QuantizeSettings settings)
    {
      Throw.IfNull(nameof(settings), settings);

      _NativeInstance.Quantize(settings);

      if (settings.MeasureErrors)
        return CreateErrorInfo(this);
      else
        return null;
    }

    /// <summary>
    /// Raise image (lighten or darken the edges of an image to give a 3-D raised effect).
    /// </summary>
    /// <param name="size">The size of the edges.</param>
    /// <exception cref="MagickException"/>
    [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
    public void Raise(int size)
    {
      _NativeInstance.RaiseOrLower(size, true);
    }

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="percentageLow">The low threshold.</param>
    /// <param name="percentageHigh">The low threshold.</param>
    /// <exception cref="MagickException"/>
    public void RandomThreshold(Percentage percentageLow, Percentage percentageHigh)
    {
      RandomThreshold(percentageLow, percentageHigh, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="percentageLow">The low threshold.</param>
    /// <param name="percentageHigh">The low threshold.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException"/>
    public void RandomThreshold(Percentage percentageLow, Percentage percentageHigh, Channels channels)
    {
      Throw.IfNegative(nameof(percentageLow), percentageLow);
      Throw.IfNegative(nameof(percentageHigh), percentageHigh);

      RandomThreshold(percentageLow.ToQuantum(), percentageHigh.ToQuantum(), channels);
    }

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="low">The low threshold.</param>
    /// <param name="high">The low threshold.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void RandomThreshold(QuantumType low, QuantumType high)
    {
      RandomThreshold(low, high, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="low">The low threshold.</param>
    /// <param name="high">The low threshold.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException"/>
#if Q16
    [CLSCompliant(false)]
#endif
    public void RandomThreshold(QuantumType low, QuantumType high, Channels channels)
    {
      _NativeInstance.RandomThreshold(low, high, channels);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public void Read(byte[] data)
    {
      Read(data, null);
    }

    /// <summary>
    /// Read single vector image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void Read(byte[] data, MagickReadSettings readSettings)
    {
      Read(data, readSettings, false);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException"/>
    public void Read(FileInfo file)
    {
      Throw.IfNull(nameof(file), file);

      Read(file.FullName);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException"/>
    public void Read(FileInfo file, int width, int height)
    {
      Throw.IfNull(nameof(file), file);

      Read(file.FullName, width, height);
    }

    /// <summary>
    /// Read single vector image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void Read(FileInfo file, MagickReadSettings readSettings)
    {
      Throw.IfNull(nameof(file), file);

      Read(file.FullName, readSettings);
    }

    /// <summary>
    /// Read single vector image frame.
    /// </summary>
    /// <param name="color">The color to fill the image with.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException"/>
    public void Read(MagickColor color, int width, int height)
    {
      Throw.IfNull(nameof(color), color);

      Read("xc:" + color.ToShortString(), width, height);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public void Read(Stream stream)
    {
      Read(stream, null);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void Read(Stream stream, MagickReadSettings readSettings)
    {
      Throw.IfNull(nameof(stream), stream);

      Read(StreamHelper.ToByteArray(stream), readSettings);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public void Read(string fileName)
    {
      Read(fileName, null);
    }

    /// <summary>
    /// Read single vector image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException"/>
    public void Read(string fileName, int width, int height)
    {
      MagickReadSettings readSettings = new MagickReadSettings(Settings);
      readSettings.Width = width;
      readSettings.Height = height;

      Read(fileName, readSettings);
    }

    /// <summary>
    /// Read single vector image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void Read(string fileName, MagickReadSettings readSettings)
    {
      Read(fileName, readSettings, false);
    }

    /// <summary>
    /// Reduce noise in image using a noise peak elimination filter.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void ReduceNoise()
    {
      ReduceNoise(3);
    }

    /// <summary>
    /// Reduce noise in image using a noise peak elimination filter.
    /// </summary>
    /// <param name="order">The order to use.</param>
    /// <exception cref="MagickException"/>
    public void ReduceNoise(int order)
    {
      Statistic(StatisticType.Nonpeak, order, order);
    }

    /// <summary>
    /// Associates a mask with the image as defined by the specified region.
    /// </summary>
    /// <param name="region">The mask region.</param>
    public void RegionMask(MagickGeometry region)
    {
      Throw.IfNull(nameof(region), region);

      MagickRectangle magickRegion = MagickRectangle.FromGeometry(region, this);
      _NativeInstance.RegionMask(magickRegion);
    }

    /// <summary>
    /// Removes the artifact with the specified name.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    public void RemoveArtifact(string name)
    {
      _NativeInstance.RemoveArtifact(name);
    }

    /// <summary>
    /// Removes the attribute with the specified name.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    public void RemoveAttribute(string name)
    {
      _NativeInstance.RemoveAttribute(name);
    }

    /// <summary>
    /// Removes the region mask of the image.
    /// </summary>
    public void RemoveRegionMask()
    {
      _NativeInstance.RegionMask(null);
    }

    /// <summary>
    /// Remove a named profile from the image.
    /// </summary>
    /// <param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
    /// <exception cref="MagickException"/>
    public void RemoveProfile(string name)
    {
      Throw.IfNullOrEmpty(nameof(name), name);

      _NativeInstance.RemoveProfile(name);
    }

    /// <summary>
    /// Resets the page property of this image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void RePage()
    {
      Page = new MagickGeometry(0, 0, 0, 0);
    }

    /// <summary>
    /// Resize image in terms of its pixel size.
    /// </summary>
    /// <param name="resolutionX">The new X resolution.</param>
    /// <param name="resolutionY">The new Y resolution.</param>
    /// <exception cref="MagickException"/>
    public void Resample(double resolutionX, double resolutionY)
    {
      PointD density = new PointD(resolutionX, resolutionY);
      Resample(density);
    }

    /// <summary>
    /// Resize image in terms of its pixel size.
    /// </summary>
    /// <param name="density">The density to use.</param>
    /// <exception cref="MagickException"/>
    public void Resample(PointD density)
    {
      _NativeInstance.Resample(density.X, density.Y);
    }

    /// <summary>
    /// Resize image to specified size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException"/>
    public void Resize(int width, int height)
    {
      MagickGeometry geometry = new MagickGeometry(width, height);
      Resize(geometry);
    }

    /// <summary>
    /// Resize image to specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException"/>
    public void Resize(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.Resize(geometry.ToString());
    }

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException"/>
    public void Resize(Percentage percentage)
    {
      Resize(percentage, percentage);
    }

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException"/>
    public void Resize(Percentage percentageWidth, Percentage percentageHeight)
    {
      Throw.IfNegative(nameof(percentageWidth), percentageWidth);
      Throw.IfNegative(nameof(percentageHeight), percentageHeight);

      MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
      Resize(geometry);
    }

    /// <summary>
    /// Roll image (rolls image vertically and horizontally).
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <exception cref="MagickException"/>
    public void Roll(int x, int y)
    {
      _NativeInstance.Roll(x, y);
    }

    /// <summary>
    /// Rotate image counter-clockwise by specified number of degrees.
    /// </summary>
    /// <param name="degrees">The number of degrees to rotate.</param>
    /// <exception cref="MagickException"/>
    public void Rotate(double degrees)
    {
      _NativeInstance.Rotate(degrees);
    }

    /// <summary>
    /// Rotational blur image.
    /// </summary>
    /// <param name="angle">The angle to use.</param>
    /// <exception cref="MagickException"/>
    public void RotationalBlur(double angle)
    {
      RotationalBlur(angle, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Rotational blur image.
    /// </summary>
    /// <param name="angle">The angle to use.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException"/>
    public void RotationalBlur(double angle, Channels channels)
    {
      _NativeInstance.RotationalBlur(angle, channels);
    }

    /// <summary>
    /// Resize image by using simple ratio algorithm.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException"/>
    public void Scale(int width, int height)
    {
      MagickGeometry geometry = new MagickGeometry(width, height);
      Scale(geometry);
    }

    /// <summary>
    /// Resize image by using pixel sampling algorithm.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException"/>
    public void Sample(int width, int height)
    {
      MagickGeometry geometry = new MagickGeometry(width, height);
      Sample(geometry);
    }

    /// <summary>
    /// Resize image by using pixel sampling algorithm.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException"/>
    public void Sample(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.Sample(geometry.ToString());
    }

    /// <summary>
    /// Resize image by using pixel sampling algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException"/>
    public void Sample(Percentage percentage)
    {
      Sample(percentage, percentage);
    }

    /// <summary>
    /// Resize image by using pixel sampling algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException"/>
    public void Sample(Percentage percentageWidth, Percentage percentageHeight)
    {
      Throw.IfNegative(nameof(percentageWidth), percentageWidth);
      Throw.IfNegative(nameof(percentageHeight), percentageHeight);

      MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
      Sample(geometry);
    }

    /// <summary>
    /// Resize image by using simple ratio algorithm.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException"/>
    public void Scale(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.Scale(MagickGeometry.ToString(geometry));
    }

    /// <summary>
    /// Resize image by using simple ratio algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException"/>
    public void Scale(Percentage percentage)
    {
      Scale(percentage, percentage);
    }

    /// <summary>
    /// Resize image by using simple ratio algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException"/>
    public void Scale(Percentage percentageWidth, Percentage percentageHeight)
    {
      Throw.IfNegative(nameof(percentageWidth), percentageWidth);
      Throw.IfNegative(nameof(percentageHeight), percentageHeight);

      MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
      Scale(geometry);
    }

    /// <summary>
    /// Segment (coalesce similar image components) by analyzing the histograms of the color
    /// components and identifying units that are homogeneous with the fuzzy c-means technique.
    /// Also uses QuantizeColorSpace and Verbose image attributes.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Segment()
    {
      Segment(ColorSpace.Undefined, 1.0, 1.5);
    }

    /// <summary>
    /// Segment (coalesce similar image components) by analyzing the histograms of the color
    /// components and identifying units that are homogeneous with the fuzzy c-means technique.
    /// Also uses QuantizeColorSpace and Verbose image attributes.
    /// </summary>
    /// <param name="quantizeColorSpace">Quantize colorspace</param>
    /// <param name="clusterThreshold">This represents the minimum number of pixels contained in
    /// a hexahedra before it can be considered valid (expressed as a percentage).</param>
    /// <param name="smoothingThreshold">The smoothing threshold eliminates noise in the second
    /// derivative of the histogram. As the value is increased, you can expect a smoother second
    /// derivative</param>
    /// <exception cref="MagickException"/>
    public void Segment(ColorSpace quantizeColorSpace, double clusterThreshold, double smoothingThreshold)
    {
      _NativeInstance.Segment(quantizeColorSpace, clusterThreshold, smoothingThreshold);
    }

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="threshold">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <exception cref="MagickException"/>
    public void SelectiveBlur(double radius, double sigma, double threshold)
    {
      SelectiveBlur(radius, sigma, threshold, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="threshold">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException"/>
    public void SelectiveBlur(double radius, double sigma, double threshold, Channels channels)
    {
      _NativeInstance.SelectiveBlur(radius, sigma, threshold, channels);
    }

    /// <summary>
    /// Separates the channels from the image and returns it as grayscale images.
    /// </summary>
    /// <exception cref="MagickException"/>
    public IEnumerable<MagickImage> Separate()
    {
      return Separate(ImageMagick.Channels.All);
    }

    /// <summary>
    /// Separates the specified channels from the image and returns it as grayscale images.
    /// </summary>
    /// <param name="channels">The channel(s) to separates.</param>
    /// <exception cref="MagickException"/>
    public IEnumerable<MagickImage> Separate(Channels channels)
    {
      IntPtr images = _NativeInstance.Separate(channels);
      return CreateList(images);
    }

    /// <summary>
    /// Applies a special effect to the image, similar to the effect achieved in a photo darkroom
    /// by sepia toning.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void SepiaTone()
    {
      SepiaTone(new Percentage(80));
    }

    /// <summary>
    /// Applies a special effect to the image, similar to the effect achieved in a photo darkroom
    /// by sepia toning.
    /// </summary>
    /// <param name="threshold">The tone threshold.</param>
    /// <exception cref="MagickException"/>
    public void SepiaTone(Percentage threshold)
    {
      _NativeInstance.SepiaTone(threshold.ToQuantum());
    }

    /// <summary>
    /// Inserts the artifact with the specified name and value into the artifact tree of the image.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <param name="value">The value of the artifact.</param>
    /// <exception cref="MagickException"/>
    public void SetArtifact(string name, string value)
    {
      Throw.IfNullOrEmpty(nameof(name), name);
      Throw.IfNull(nameof(value), value);

      _NativeInstance.SetArtifact(name, value);
    }

    /// <summary>
    /// Lessen (or intensify) when adding noise to an image.
    /// </summary>
    /// <param name="attenuate">The attenuate value.</param>
    public void SetAttenuate(double attenuate)
    {
      SetArtifact("attenuate", attenuate.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Sets a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    /// <exception cref="MagickException"/>
    public void SetAttribute(string name, string value)
    {
      Throw.IfNullOrEmpty(nameof(name), name);
      Throw.IfNull(nameof(value), value);

      _NativeInstance.SetAttribute(name, value);
    }

    /// <summary>
    /// Set color at colormap position index.
    /// </summary>
    /// <param name="index">The position index.</param>
    /// <param name="color">The color.</param>
    /// <exception cref="MagickException"/>
    public void SetColormap(int index, MagickColor color)
    {
      Throw.IfNull(nameof(color), color);

      _NativeInstance.SetColormap(index, color);
    }

    /// <summary>
    /// When comparing images, emphasize pixel differences with this color.
    /// </summary>
    /// <param name="color">The color.</param>
    public void SetHighlightColor(MagickColor color)
    {
      Throw.IfNull(nameof(color), color);

      SetArtifact("highlight-color", color.ToString());
    }

    /// <summary>
    /// When comparing images, de-emphasize pixel differences with this color.
    /// </summary>
    /// <param name="color">The color.</param>
    public void SetLowlightColor(MagickColor color)
    {
      Throw.IfNull(nameof(color), color);

      SetArtifact("lowlight-color", color.ToString());
    }

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Shade()
    {
      Shade(30, 30);
    }

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <param name="azimuth">The light source direction.</param>
    /// <param name="elevation">The light source direction.</param>
    /// <exception cref="MagickException"/>
    public void Shade(double azimuth, double elevation)
    {
      Shade(azimuth, elevation, true);
    }

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <param name="azimuth">The light source direction.</param>
    /// <param name="elevation">The light source direction.</param>
    /// <param name="colorShading">Specify true to shade the intensity of each pixel.</param>
    /// <exception cref="MagickException"/>
    public void Shade(double azimuth, double elevation, bool colorShading)
    {
      Shade(azimuth, elevation, colorShading, ImageMagick.Channels.RGB);
    }

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <param name="azimuth">The light source direction.</param>
    /// <param name="elevation">The light source direction.</param>
    /// <param name="colorShading">Specify true to shade the intensity of each pixel.</param>
    /// <param name="channels">The channel(s) that should be shaded.</param>
    /// <exception cref="MagickException"/>
    public void Shade(double azimuth, double elevation, bool colorShading, Channels channels)
    {
      _NativeInstance.Shade(azimuth, elevation, colorShading, channels);
    }

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Shadow()
    {
      Shadow(5, 5, 0.5, new Percentage(80));
    }

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <param name="color">The color of the shadow.</param>
    /// <exception cref="MagickException"/>
    public void Shadow(MagickColor color)
    {
      Shadow(5, 5, 0.5, new Percentage(80), color);
    }

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <param name="x">the shadow x-offset.</param>
    /// <param name="y">the shadow y-offset.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="alpha">Transparency percentage.</param>
    /// <exception cref="MagickException"/>
    public void Shadow(int x, int y, double sigma, Percentage alpha)
    {
      _NativeInstance.Shadow(x, y, sigma, alpha.ToDouble());
    }

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <param name="x ">the shadow x-offset.</param>
    /// <param name="y">the shadow y-offset.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="alpha">Transparency percentage.</param>
    /// <param name="color">The color of the shadow.</param>
    /// <exception cref="MagickException"/>
    public void Shadow(int x, int y, double sigma, Percentage alpha, MagickColor color)
    {
      Throw.IfNull(nameof(color), color);

      MagickColor backgroundColor = BackgroundColor;
      BackgroundColor = color;
      _NativeInstance.Shadow(x, y, sigma, alpha.ToDouble());
      BackgroundColor = backgroundColor;
    }

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Sharpen()
    {
      Sharpen(0.0, 1.0);
    }

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException"/>
    public void Sharpen(Channels channels)
    {
      Sharpen(0.0, 1.0, channels);
    }

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException"/>
    public void Sharpen(double radius, double sigma)
    {
      Sharpen(radius, sigma, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    public void Sharpen(double radius, double sigma, Channels channels)
    {
      _NativeInstance.Sharpen(radius, sigma, channels);
    }

    /// <summary>
    /// Shave pixels from image edges.
    /// </summary>
    /// <param name="leftRight">The number of pixels to shave left and right.</param>
    /// <param name="topBottom">The number of pixels to shave top and bottom.</param>
    /// <exception cref="MagickException"/>
    public void Shave(int leftRight, int topBottom)
    {
      _NativeInstance.Shave(leftRight, topBottom);
    }

    /// <summary>
    /// Shear image (create parallelogram by sliding image by X or Y axis).
    /// </summary>
    /// <param name="xAngle">Specifies the number of degrees to shear the image.</param>
    /// <param name="yAngle">Specifies the number of degrees to shear the image.</param>
    /// <exception cref="MagickException"/>
    public void Shear(double xAngle, double yAngle)
    {
      _NativeInstance.Shear(xAngle, yAngle);
    }

    /// <summary>
    /// adjust the image contrast with a non-linear sigmoidal contrast algorithm
    /// </summary>
    /// <param name="contrast">The contrast</param>
    /// <exception cref="MagickException"/>
    public void SigmoidalContrast(double contrast)
    {
      SigmoidalContrast(true, contrast);
    }

    /// <summary>
    /// adjust the image contrast with a non-linear sigmoidal contrast algorithm
    /// </summary>
    /// <param name="sharpen">Specifies if sharpening should be used.</param>
    /// <param name="contrast">The contrast</param>
    /// <exception cref="MagickException"/>
    public void SigmoidalContrast(bool sharpen, double contrast)
    {
      SigmoidalContrast(sharpen, contrast, Quantum.Max / 2.0);
    }

    /// <summary>
    /// adjust the image contrast with a non-linear sigmoidal contrast algorithm
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <exception cref="MagickException"/>
    public void SigmoidalContrast(double contrast, double midpoint)
    {
      SigmoidalContrast(true, contrast, midpoint);
    }

    /// <summary>
    /// adjust the image contrast with a non-linear sigmoidal contrast algorithm
    /// </summary>
    /// <param name="sharpen">Specifies if sharpening should be used.</param>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <exception cref="MagickException"/>
    public void SigmoidalContrast(bool sharpen, double contrast, double midpoint)
    {
      _NativeInstance.SigmoidalContrast(sharpen, contrast, midpoint);
    }

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException"/>
    public void SparseColor(SparseColorMethod method, IEnumerable<SparseColorArg> args)
    {
      SparseColor(ImageMagick.Channels.Composite, method, args);
    }

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="channels">The channel(s) to use.</param>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException"/>
    public void SparseColor(Channels channels, SparseColorMethod method, IEnumerable<SparseColorArg> args)
    {
      Throw.IfNull(nameof(args), args);

      bool hasRed = EnumHelper.HasFlag(channels, ImageMagick.Channels.Red);
      bool hasGreen = EnumHelper.HasFlag(channels, ImageMagick.Channels.Green);
      bool hasBlue = EnumHelper.HasFlag(channels, ImageMagick.Channels.Blue);
      bool hasAlpha = HasAlpha && EnumHelper.HasFlag(channels, ImageMagick.Channels.Alpha);

      Throw.IfTrue(nameof(channels), !hasRed && !hasGreen && !hasBlue && !hasAlpha, "Invalid channels specified.");

      List<double> arguments = new List<double>();

      foreach (SparseColorArg arg in args)
      {
        arguments.Add(arg.X);
        arguments.Add(arg.Y);
        if (hasRed)
          arguments.Add(Quantum.ScaleToDouble(arg.Color.R));
        if (hasGreen)
          arguments.Add(Quantum.ScaleToDouble(arg.Color.G));
        if (hasBlue)
          arguments.Add(Quantum.ScaleToDouble(arg.Color.B));
        if (hasAlpha)
          arguments.Add(Quantum.ScaleToDouble(arg.Color.A));
      }

      Throw.IfTrue(nameof(args), arguments.Count == 0, "Value cannot be empty");

      _NativeInstance.SparseColor(channels, method, arguments.ToArray(), arguments.Count);
    }

    /// <summary>
    /// Simulates a pencil sketch.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Sketch()
    {
      Sketch(0.0, 1.0, 0.0);
    }

    /// <summary>
    /// Simulates a pencil sketch. We convolve the image with a Gaussian operator of the given
    /// radius and standard deviation (sigma). For reasonable results, radius should be larger than sigma.
    /// Use a radius of 0 and sketch selects a suitable radius for you.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="angle">Apply the effect along this angle.</param>
    /// <exception cref="MagickException"/>
    public void Sketch(double radius, double sigma, double angle)
    {
      _NativeInstance.Sketch(radius, sigma, angle);
    }

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process)
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Solarize()
    {
      Solarize(new Percentage(50.0));
    }

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process)
    /// </summary>
    /// <param name="factor">The factor to use.</param>
    /// <exception cref="MagickException"/>
    public void Solarize(double factor)
    {
      _NativeInstance.Solarize(factor);
    }

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process)
    /// </summary>
    /// <param name="factorPercentage">The factor to use.</param>
    /// <exception cref="MagickException"/>
    public void Solarize(Percentage factorPercentage)
    {
      _NativeInstance.Solarize(factorPercentage.ToQuantum());
    }

    /// <summary>
    /// Splice the background color into the image.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException"/>
    public void Splice(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.Splice(MagickRectangle.FromGeometry(geometry, this));
    }

    /// <summary>
    /// Spread pixels randomly within image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Spread()
    {
      Spread(Interpolate, 3);
    }

    /// <summary>
    /// Spread pixels randomly within image by specified amount.
    /// </summary>
    /// <param name="radius">Choose a random pixel in a neighborhood of this extent.</param>
    /// <exception cref="MagickException"/>
    public void Spread(double radius)
    {
      Spread(PixelInterpolateMethod.Undefined, radius);
    }

    /// <summary>
    /// Spread pixels randomly within image by specified amount.
    /// </summary>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="radius">Choose a random pixel in a neighborhood of this extent.</param>
    /// <exception cref="MagickException"/>
    public void Spread(PixelInterpolateMethod method, double radius)
    {
      _NativeInstance.Spread(method, radius);
    }

    /// <summary>
    /// Makes each pixel the min / max / median / mode / etc. of the neighborhood of the specified width
    /// and height.
    /// </summary>
    /// <param name="type">The statistic type.</param>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <exception cref="MagickException"/>
    public void Statistic(StatisticType type, int width, int height)
    {
      _NativeInstance.Statistic(type, width, height);
    }

    /// <summary>
    /// Returns image statistics.
    /// </summary>
    /// <exception cref="MagickException"/>
    public Statistics Statistics()
    {
      IntPtr list = _NativeInstance.Statistics();
      try
      {
        return new Statistics(this, list);
      }
      finally
      {
        ImageMagick.Statistics.DisposeList(list);
      }
    }

    /// <summary>
    /// Add a digital watermark to the image (based on second image)
    /// </summary>
    /// <param name="watermark">The image to use as a watermark.</param>
    /// <exception cref="MagickException"/>
    public void Stegano(MagickImage watermark)
    {
      Throw.IfNull(nameof(watermark), watermark);

      _NativeInstance.Stegano(watermark);
    }

    /// <summary>
    /// Create an image which appears in stereo when viewed with red-blue glasses (Red image on
    /// left, blue on right)
    /// </summary>
    /// <param name="rightImage">The image to use as the right part of the resulting image.</param>
    /// <exception cref="MagickException"/>
    public void Stereo(MagickImage rightImage)
    {
      Throw.IfNull(nameof(rightImage), rightImage);

      _NativeInstance.Stereo(rightImage);
    }

    /// <summary>
    /// Strips an image of all profiles and comments.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Strip()
    {
      _NativeInstance.Strip();
    }

    /// <summary>
    /// Swirl image (image pixels are rotated by degrees).
    /// </summary>
    /// <param name="degrees">The number of degrees.</param>
    /// <exception cref="MagickException"/>
    public void Swirl(double degrees)
    {
      Swirl(Interpolate, degrees);
    }

    /// <summary>
    /// Swirl image (image pixels are rotated by degrees).
    /// </summary>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="degrees">The number of degrees.</param>
    /// <exception cref="MagickException"/>
    public void Swirl(PixelInterpolateMethod method, double degrees)
    {
      _NativeInstance.Swirl(method, degrees);
    }

    /// <summary>
    /// Search for the specified image at EVERY possible location in this image. This is slow!
    /// very very slow.. It returns a similarity image such that an exact match location is
    /// completely white and if none of the pixels match, black, otherwise some gray level in-between.
    /// </summary>
    /// <param name="image">The image to search for.</param>
    /// <exception cref="MagickException"/>
    public MagickSearchResult SubImageSearch(MagickImage image)
    {
      return SubImageSearch(image, ErrorMetric.RootMeanSquared, -1);
    }

    /// <summary>
    /// Search for the specified image at EVERY possible location in this image. This is slow!
    /// very very slow.. It returns a similarity image such that an exact match location is
    /// completely white and if none of the pixels match, black, otherwise some gray level in-between.
    /// </summary>
    /// <param name="image">The image to search for.</param>
    /// <param name="metric">The metric to use.</param>
    /// <exception cref="MagickException"/>
    public MagickSearchResult SubImageSearch(MagickImage image, ErrorMetric metric)
    {
      return SubImageSearch(image, metric, -1);
    }

    /// <summary>
    /// Search for the specified image at EVERY possible location in this image. This is slow!
    /// very very slow.. It returns a similarity image such that an exact match location is
    /// completely white and if none of the pixels match, black, otherwise some gray level in-between.
    /// </summary>
    /// <param name="image">The image to search for.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="similarityThreshold">Minimum distortion for (sub)image match.</param>
    /// <exception cref="MagickException"/>
    public MagickSearchResult SubImageSearch(MagickImage image, ErrorMetric metric, double similarityThreshold)
    {
      Throw.IfNull(nameof(image), image);

      double similarityMetric;

      MagickRectangle rectangle;
      IntPtr result = _NativeInstance.SubImageSearch(image, metric, similarityThreshold, out rectangle, out similarityMetric);
      return new MagickSearchResult(Create(result, image.Settings), MagickGeometry.FromRectangle(rectangle), similarityMetric);
    }

    /// <summary>
    /// Channel a texture on image background.
    /// </summary>
    /// <param name="image">The image to use as a texture on the image background.</param>
    /// <exception cref="MagickException"/>
    public void Texture(MagickImage image)
    {
      Throw.IfNull(nameof(image), image);

      _NativeInstance.Texture(image);
    }

    /// <summary>
    /// Threshold image.
    /// </summary>
    /// <param name="percentage">The threshold percentage.</param>
    /// <exception cref="MagickException"/>
    public void Threshold(Percentage percentage)
    {
      _NativeInstance.Threshold(percentage.ToQuantum());
    }

    /// <summary>
    /// Resize image to thumbnail size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException"/>
    public void Thumbnail(int width, int height)
    {
      MagickGeometry geometry = new MagickGeometry(width, height);
      Thumbnail(geometry);
    }

    /// <summary>
    /// Resize image to thumbnail size.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException"/>
    public void Thumbnail(MagickGeometry geometry)
    {
      Throw.IfNull(nameof(geometry), geometry);

      _NativeInstance.Thumbnail(MagickGeometry.ToString(geometry));
    }

    /// <summary>
    /// Resize image to thumbnail size.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException"/>
    public void Thumbnail(Percentage percentage)
    {
      Thumbnail(percentage, percentage);
    }

    /// <summary>
    /// Resize image to thumbnail size.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException"/>
    public void Thumbnail(Percentage percentageWidth, Percentage percentageHeight)
    {
      Throw.IfNegative(nameof(percentageWidth), percentageWidth);
      Throw.IfNegative(nameof(percentageHeight), percentageHeight);

      MagickGeometry geometry = new MagickGeometry(percentageWidth, percentageHeight);
      Thumbnail(geometry);
    }

    /// <summary>
    /// Compose an image repeated across and down the image.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException"/>
    public void Tile(MagickImage image, CompositeOperator compose)
    {
      Tile(image, compose, null);
    }

    /// <summary>
    /// Compose an image repeated across and down the image.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException"/>
    public void Tile(MagickImage image, CompositeOperator compose, string args)
    {
      Throw.IfNull(nameof(image), image);

      for (int y = 0; y < Height; y += image.Height)
      {
        for (int x = 0; x < Width; x += image.Width)
        {
          Composite(image, x, y, compose, args);
        }
      }
    }

    /// <summary>
    /// Applies a color vector to each pixel in the image. The length of the vector is 0 for black
    /// and white and at its maximum for the midtones. The vector weighting function is
    /// f(x)=(1-(4.0*((x-0.5)*(x-0.5))))
    /// </summary>
    /// <param name="opacity">A color value used for tinting.</param>
    /// <exception cref="MagickException"/>
    public void Tint(string opacity)
    {
      Tint(opacity, Settings.FillColor);
    }

    /// <summary>
    /// Applies a color vector to each pixel in the image. The length of the vector is 0 for black
    /// and white and at its maximum for the midtones. The vector weighting function is
    /// f(x)=(1-(4.0*((x-0.5)*(x-0.5))))
    /// </summary>
    /// <param name="opacity">A color value used for tinting.</param>
    /// <param name="color">A color value used for tinting.</param>
    /// <exception cref="MagickException"/>
    public void Tint(string opacity, MagickColor color)
    {
      Throw.IfNullOrEmpty(nameof(opacity), opacity);
      Throw.IfNull(nameof(color), color);

      _NativeInstance.Tint(opacity, color);
    }

    /// <summary>
    /// Converts this instance to a base64 string.
    /// </summary>
    public string ToBase64()
    {
      byte[] bytes = ToByteArray();
      if (bytes == null)
        return "";

      return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Converts this instance to a base64 string.
    /// </summary>
    /// <param name="format">The format to use.</param>
    public string ToBase64(MagickFormat format)
    {
      byte[] bytes = ToByteArray(format);
      if (bytes == null)
        return "";

      return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Converts this instance to a byte array.
    /// </summary>
    public byte[] ToByteArray()
    {
      using (MemoryStream stream = new MemoryStream())
      {
        Write(stream);
        return stream.ToArray();
      }
    }

    /// <summary>
    /// Converts this instance to a byte array.
    /// </summary>
    /// <param name="format">The format to use.</param>
    public byte[] ToByteArray(MagickFormat format)
    {
      Format = format;
      return ToByteArray();
    }

    /// <summary>
    /// Returns a string that represents the current image.
    /// </summary>
    public override string ToString()
    {
      return string.Format(CultureInfo.InvariantCulture, "{0} {1}x{2} {3}-bit {4} {5}",
        Format, Width, Height, Depth, ColorSpace, FormatedFileSize());
    }

    /// <summary>
    ///  Transforms the image from the colorspace of the source profile to the target profile. The
    ///  source profile will only be used if the image does not contain a color profile. Nothing
    ///  will happen if the source profile has a different colorspace then that of the image.
    /// </summary>
    /// <param name="source">The source color profile.</param>
    /// <param name="target">The target color profile</param>
    public void TransformColorSpace(ColorProfile source, ColorProfile target)
    {
      Throw.IfNull(nameof(source), source);
      Throw.IfNull(nameof(target), target);

      if (source.ColorSpace != ColorSpace)
        return;

      AddProfile(source, false);
      AddProfile(target);
    }

    /// <summary>
    /// Add alpha channel to image, setting pixels matching color to transparent.
    /// </summary>
    /// <param name="color">The color to make transparent.</param>
    /// <exception cref="MagickException"/>
    public void Transparent(MagickColor color)
    {
      Throw.IfNull(nameof(color), color);

      _NativeInstance.Transparent(color, false);
    }

    /// <summary>
    /// Add alpha channel to image, setting pixels that lie in between the given two colors to
    /// transparent.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void TransparentChroma(MagickColor colorLow, MagickColor colorHigh)
    {
      Throw.IfNull(nameof(colorLow), colorLow);
      Throw.IfNull(nameof(colorHigh), colorHigh);

      _NativeInstance.TransparentChroma(colorLow, colorHigh, false);
    }

    /// <summary>
    /// Creates a horizontal mirror image by reflecting the pixels around the central y-axis while
    /// rotating them by 90 degrees.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Transpose()
    {
      _NativeInstance.Transpose();
    }

    /// <summary>
    /// Creates a vertical mirror image by reflecting the pixels around the central x-axis while
    /// rotating them by 270 degrees.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Transverse()
    {
      _NativeInstance.Transverse();
    }

    /// <summary>
    /// Trim edges that are the background color from the image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Trim()
    {
      _NativeInstance.Trim();
    }

    /// <summary>
    /// Returns the unique colors of an image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public MagickImage UniqueColors()
    {
      return Create(_NativeInstance.UniqueColors(), Settings);
    }

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException"/>
    public void UnsharpMask(double radius, double sigma)
    {
      UnsharpMask(radius, sigma, 1.0, 0.05);
    }

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException"/>
    public void UnsharpMask(double radius, double sigma, Channels channels)
    {
      UnsharpMask(radius, sigma, 1.0, 0.05, channels);
    }

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="amount">The percentage of the difference between the original and the blur image
    /// that is added back into the original.</param>
    /// <param name="threshold">The threshold in pixels needed to apply the diffence amount.</param>
    /// <exception cref="MagickException"/>
    public void UnsharpMask(double radius, double sigma, double amount, double threshold)
    {
      UnsharpMask(radius, sigma, amount, threshold, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="amount">The percentage of the difference between the original and the blur image
    /// that is added back into the original.</param>
    /// <param name="threshold">The threshold in pixels needed to apply the diffence amount.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException"/>
    public void UnsharpMask(double radius, double sigma, double amount, double threshold, Channels channels)
    {
      _NativeInstance.UnsharpMask(radius, sigma, amount, threshold, channels);
    }

    /// <summary>
    /// Softens the edges of the image in vignette style.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Vignette()
    {
      Vignette(0.0, 1.0, 0, 0);
    }

    /// <summary>
    /// Softens the edges of the image in vignette style.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="x">The x ellipse offset.</param>
    /// <param name="y">the y ellipse offset.</param>
    /// <exception cref="MagickException"/>
    public void Vignette(double radius, double sigma, int x, int y)
    {
      _NativeInstance.Vignette(radius, sigma, x, y);
    }

    /// <summary>
    /// Map image pixels to a sine wave.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Wave()
    {
      Wave(Interpolate, 25.0, 150.0);
    }

    /// <summary>
    /// Map image pixels to a sine wave.
    /// </summary>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="amplitude">The amplitude.</param>
    /// <param name="length">The length of the wave.</param>
    /// <exception cref="MagickException"/>
    public void Wave(PixelInterpolateMethod method, double amplitude, double length)
    {
      _NativeInstance.Wave(method, amplitude, length);
    }

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="threshold">The threshold for smoothing</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public void WaveletDenoise(QuantumType threshold)
    {
      WaveletDenoise(threshold, 0.0);
    }

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="threshold">The threshold for smoothing</param>
    /// <param name="softness">Attenuate the smoothing threshold.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public void WaveletDenoise(QuantumType threshold, double softness)
    {
      _NativeInstance.WaveletDenoise(threshold, softness);
    }

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="thresholdPercentage">The threshold for smoothing</param>
    public void WaveletDenoise(Percentage thresholdPercentage)
    {
      WaveletDenoise(thresholdPercentage.ToQuantum(), 0.0);
    }

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="thresholdPercentage">The threshold for smoothing</param>
    /// <param name="softness">Attenuate the smoothing threshold.</param>
    public void WaveletDenoise(Percentage thresholdPercentage, double softness)
    {
      WaveletDenoise(thresholdPercentage.ToQuantum(), softness);
    }

    /// <summary>
    /// Forces all pixels above the threshold into white while leaving all pixels at or below
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <exception cref="MagickException"/>
    public void WhiteThreshold(Percentage threshold)
    {
      WhiteThreshold(threshold, ImageMagick.Channels.Composite);
    }

    /// <summary>
    /// Forces all pixels above the threshold into white while leaving all pixels at or below
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <param name="channels">The channel(s) to make black.</param>
    /// <exception cref="MagickException"/>
    public void WhiteThreshold(Percentage threshold, Channels channels)
    {
      Throw.IfNegative(nameof(threshold), threshold);

      _NativeInstance.WhiteThreshold(threshold.ToString(), channels);
    }

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <exception cref="MagickException"/>
    public void Write(FileInfo file)
    {
      Throw.IfNull(nameof(file), file);

      Write(file.FullName);
      file.Refresh();
    }

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException"/>
    public void Write(FileInfo file, IWriteDefines defines)
    {
      Settings.SetDefines(defines);
      Write(file);
    }

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <exception cref="MagickException"/>
    public void Write(Stream stream)
    {
      Throw.IfNull(nameof(stream), stream);

      Settings.FileName = null;

      UIntPtr length;
      IntPtr data = _NativeInstance.WriteBlob(Settings, out length);
      MagickMemory.WriteBytes(data, length, stream);
    }

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException"/>
    public void Write(Stream stream, IWriteDefines defines)
    {
      Settings.SetDefines(defines);
      Format = defines.Format;
      Write(stream);
    }

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException"/>
    public void Write(Stream stream, MagickFormat format)
    {
      Format = format;
      Write(stream);
    }

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public void Write(string fileName)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);

      Throw.IfNullOrEmpty(nameof(fileName), filePath);

      _NativeInstance.FileName = filePath;
      _NativeInstance.WriteFile(Settings);
    }

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException"/>
    public void Write(string fileName, IWriteDefines defines)
    {
      Settings.SetDefines(defines);
      Write(fileName);
    }
  }
}