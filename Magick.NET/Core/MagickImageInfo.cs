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
using System.IO;

namespace ImageMagick
{
  /// <summary>
  /// Class that contains basic information about an image.
  /// </summary>
  public sealed class MagickImageInfo : IEquatable<MagickImageInfo>, IComparable<MagickImageInfo>
  {
    /// <summary>
    /// Initializes a new instance of the MagickImageInfo class.
    /// </summary>
    public MagickImageInfo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageInfo class using the specified blob.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <exception cref="MagickException"/>
    public MagickImageInfo(byte[] data)
      : this()
    {
      Read(data);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageInfo class using the specified filename.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException"/>
    public MagickImageInfo(FileInfo file)
      : this()
    {
      Read(file);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageInfo class using the specified stream.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public MagickImageInfo(Stream stream)
      : this()
    {
      Read(stream);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageInfo class using the specified filename.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public MagickImageInfo(string fileName)
      : this()
    {
      Read(fileName);
    }

    /// <summary>
    /// Determines whether the specified MagickImageInfo instances are considered equal.
    /// </summary>
    /// <param name="left">The first MagickImageInfo to compare.</param>
    /// <param name="right"> The second MagickImageInfo to compare.</param>
    /// <returns></returns>
    public static bool operator ==(MagickImageInfo left, MagickImageInfo right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified MagickImageInfo instances are not considered equal.
    /// </summary>
    /// <param name="left">The first MagickImageInfo to compare.</param>
    /// <param name="right"> The second MagickImageInfo to compare.</param>
    /// <returns></returns>
    public static bool operator !=(MagickImageInfo left, MagickImageInfo right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Determines whether the first MagickImageInfo is more than the second MagickImageInfo.
    /// </summary>
    /// <param name="left">The first MagickImageInfo to compare.</param>
    /// <param name="right"> The second MagickImageInfo to compare.</param>
    /// <returns></returns>
    public static bool operator >(MagickImageInfo left, MagickImageInfo right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) == 1;
    }

    /// <summary>
    /// Determines whether the first MagickImageInfo is less than the second MagickImageInfo.
    /// </summary>
    /// <param name="left">The first MagickImageInfo to compare.</param>
    /// <param name="right"> The second MagickImageInfo to compare.</param>
    /// <returns></returns>
    public static bool operator <(MagickImageInfo left, MagickImageInfo right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) == -1;
    }

    /// <summary>
    /// Determines whether the first MagickImageInfo is less than or equal to the second MagickImageInfo.
    /// </summary>
    /// <param name="left">The first MagickImageInfo to compare.</param>
    /// <param name="right"> The second MagickImageInfo to compare.</param>
    /// <returns></returns>
    public static bool operator >=(MagickImageInfo left, MagickImageInfo right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether the first MagickImageInfo is less than or equal to the second MagickImageInfo.
    /// </summary>
    /// <param name="left">The first MagickImageInfo to compare.</param>
    /// <param name="right"> The second MagickImageInfo to compare.</param>
    /// <returns></returns>
    public static bool operator <=(MagickImageInfo left, MagickImageInfo right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Color space of the image.
    /// </summary>
    public ColorSpace ColorSpace
    {
      get;
      private set;
    }

    /// <summary>
    /// Compression method of the image.
    /// </summary>
    public CompressionMethod CompressionMethod
    {
      get;
      private set;
    }

    /// <summary>
    /// The density of the image.
    /// </summary>
    public Density Density
    {
      get;
      private set;
    }

    /// <summary>
    /// Original file name of the image (only available if read from disk).
    /// </summary>
    public string FileName
    {
      get;
      private set;
    }

    /// <summary>
    /// The format of the image.
    /// </summary>
    public MagickFormat Format
    {
      get;
      private set;
    }

    /// <summary>
    /// Height of the image.
    /// </summary>
    public int Height
    {
      get;
      private set;
    }

    /// <summary>
    /// Type of interlacing.
    /// </summary>
    public Interlace Interlace
    {
      get;
      private set;
    }

    /// <summary>
    /// Height of the image.
    /// </summary>
    public int Width
    {
      get;
      private set;
    }

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    /// <param name="other">The object to compare this image information with.</param>
    public int CompareTo(MagickImageInfo other)
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
    /// Determines whether the specified object is equal to the current image information.
    /// </summary>
    /// <param name="obj">The object to compare this image information with.</param>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as MagickImageInfo);
    }

    /// <summary>
    /// Determines whether the specified geometry is equal to the current image information.
    /// </summary>
    /// <param name="other">The image to compare this image information with.</param>
    public bool Equals(MagickImageInfo other)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      return
        ColorSpace == other.ColorSpace &&
        CompressionMethod == other.CompressionMethod &&
        Density == other.Density &&
        FileName == other.FileName &&
        Format == other.Format &&
        Height == other.Height &&
        Interlace == other.Interlace &&
        Width == other.Width;
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    public override int GetHashCode()
    {
      return
        ColorSpace.GetHashCode() ^
        CompressionMethod.GetHashCode() ^
        Density.GetHashCode() ^
        FileName.GetHashCode() ^
        Format.GetHashCode() ^
        Height.GetHashCode() ^
        Interlace.GetHashCode() ^
        Width.GetHashCode();
    }

    private void Initialize(MagickImage image)
    {
      ColorSpace = image.ColorSpace;
      CompressionMethod = image.CompressionMethod;
      Density = image.Density;
      FileName = image.FileName;
      Format = image.Format;
      Height = image.Height;
      Interlace = image.Interlace;
      Width = image.Width;
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <exception cref="MagickException"/>
    public void Read(byte[] data)
    {
      using (MagickImage image = new MagickImage())
      {
        image.Ping(data);
        Initialize(image);
      }
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException"/>
    public void Read(FileInfo file)
    {
      using (MagickImage image = new MagickImage())
      {
        image.Ping(file);
        Initialize(image);
      }
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public void Read(Stream stream)
    {
      using (MagickImage image = new MagickImage())
      {
        image.Ping(stream);
        Initialize(image);
      }
    }

    /// <summary>
    /// Read basic information about an image.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public void Read(string fileName)
    {
      using (MagickImage image = new MagickImage())
      {
        image.Ping(fileName);
        Initialize(image);
      }
    }

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <exception cref="MagickException"/>
    public static IEnumerable<MagickImageInfo> ReadCollection(byte[] data)
    {
      using (MagickImageCollection images = new MagickImageCollection())
      {
        images.Ping(data);
        foreach (MagickImage image in images)
        {
          MagickImageInfo info = new MagickImageInfo();
          info.Initialize(image);
          yield return info;
        }
      }
    }

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public static IEnumerable<MagickImageInfo> ReadCollection(string fileName)
    {
      using (MagickImageCollection images = new MagickImageCollection())
      {
        images.Ping(fileName);
        foreach (MagickImage image in images)
        {
          MagickImageInfo info = new MagickImageInfo();
          info.Initialize(image);
          yield return info;
        }
      }
    }

    /// <summary>
    /// Read basic information about an image with multiple frames/pages.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public static IEnumerable<MagickImageInfo> ReadCollection(Stream stream)
    {
      using (MagickImageCollection images = new MagickImageCollection())
      {
        images.Ping(stream);
        foreach (MagickImage image in images)
        {
          MagickImageInfo info = new MagickImageInfo();
          info.Initialize(image);
          yield return info;
        }
      }
    }
  }
}