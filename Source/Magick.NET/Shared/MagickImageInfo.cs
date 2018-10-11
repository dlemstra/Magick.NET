// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Collections.Generic;
using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Class that contains basic information about an image.
    /// </summary>
    public sealed class MagickImageInfo : IMagickImageInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
        /// </summary>
        public MagickImageInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageInfo(byte[] data)
          : this()
        {
            Read(data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageInfo(FileInfo file)
          : this()
        {
            Read(file);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageInfo(Stream stream)
          : this()
        {
            Read(stream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageInfo"/> class.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageInfo(string fileName)
          : this()
        {
            Read(fileName);
        }

        /// <summary>
        /// Gets the color space of the image.
        /// </summary>
        public ColorSpace ColorSpace
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the compression method of the image.
        /// </summary>
        public CompressionMethod Compression
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the density of the image.
        /// </summary>
        public Density Density
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the original file name of the image (only available if read from disk).
        /// </summary>
        public string FileName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the format of the image.
        /// </summary>
        public MagickFormat Format
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of interlacing.
        /// </summary>
        public Interlace Interlace
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the JPEG/MIFF/PNG compression level.
        /// </summary>
        public int Quality
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified <see cref="MagickImageInfo"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="MagickImageInfo"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickImageInfo"/> to compare.</param>
        public static bool operator ==(MagickImageInfo left, MagickImageInfo right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="MagickImageInfo"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="MagickImageInfo"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickImageInfo"/> to compare.</param>
        public static bool operator !=(MagickImageInfo left, MagickImageInfo right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the first <see cref="MagickImageInfo"/> is more than the second <see cref="MagickImageInfo"/>.
        /// </summary>
        /// <param name="left">The first <see cref="MagickImageInfo"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickImageInfo"/> to compare.</param>
        public static bool operator >(MagickImageInfo left, MagickImageInfo right)
        {
            if (left is null)
                return right is null;

            return left.CompareTo(right) == 1;
        }

        /// <summary>
        /// Determines whether the first <see cref="MagickImageInfo"/> is less than the second <see cref="MagickImageInfo"/>.
        /// </summary>
        /// <param name="left">The first <see cref="MagickImageInfo"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickImageInfo"/> to compare.</param>
        public static bool operator <(MagickImageInfo left, MagickImageInfo right)
        {
            if (left is null)
                return !(right is null);

            return left.CompareTo(right) == -1;
        }

        /// <summary>
        /// Determines whether the first <see cref="MagickImageInfo"/> is less than or equal to the second <see cref="MagickImageInfo"/>.
        /// </summary>
        /// <param name="left">The first <see cref="MagickImageInfo"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickImageInfo"/> to compare.</param>
        public static bool operator >=(MagickImageInfo left, MagickImageInfo right)
        {
            if (left is null)
                return right is null;

            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Determines whether the first <see cref="MagickImageInfo"/> is less than or equal to the second <see cref="MagickImageInfo"/>.
        /// </summary>
        /// <param name="left">The first <see cref="MagickImageInfo"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickImageInfo"/> to compare.</param>
        public static bool operator <=(MagickImageInfo left, MagickImageInfo right)
        {
            if (left is null)
                return !(right is null);

            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Read basic information about an image with multiple frames/pages.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <returns>A <see cref="MagickImageInfo"/> iteration.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public static IEnumerable<IMagickImageInfo> ReadCollection(byte[] data)
        {
            using (IMagickImageCollection images = new MagickImageCollection())
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
        /// <param name="file">The file to read the frames from.</param>
        /// <returns>A <see cref="MagickImageInfo"/> iteration.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public static IEnumerable<IMagickImageInfo> ReadCollection(FileInfo file)
        {
            Throw.IfNull(nameof(file), file);

            return ReadCollection(file.FullName);
        }

        /// <summary>
        /// Read basic information about an image with multiple frames/pages.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A <see cref="MagickImageInfo"/> iteration.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public static IEnumerable<MagickImageInfo> ReadCollection(Stream stream)
        {
            using (IMagickImageCollection images = new MagickImageCollection())
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

        /// <summary>
        /// Read basic information about an image with multiple frames/pages.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A <see cref="MagickImageInfo"/> iteration.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public static IEnumerable<IMagickImageInfo> ReadCollection(string fileName)
        {
            using (IMagickImageCollection images = new MagickImageCollection())
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
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="other">The object to compare this image information with.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(IMagickImageInfo other)
        {
            if (other is null)
                return 1;

            int left = Width * Height;
            int right = other.Width * other.Height;

            if (left == right)
                return 0;

            return left < right ? -1 : 1;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="MagickImageInfo"/>.
        /// </summary>
        /// <param name="obj">The object to compare this image information with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="MagickImageInfo"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as IMagickImageInfo);
        }

        /// <summary>
        /// Determines whether the specified <see cref="MagickImageInfo"/> is equal to the current <see cref="MagickImageInfo"/>.
        /// </summary>
        /// <param name="other">The image to compare this <see cref="MagickImageInfo"/> with.</param>
        /// <returns>True when the specified <see cref="MagickImageInfo"/> is equal to the current <see cref="MagickImageInfo"/>.</returns>
        public bool Equals(IMagickImageInfo other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
              ColorSpace == other.ColorSpace &&
              Compression == other.Compression &&
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
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return
              ColorSpace.GetHashCode() ^
              Compression.GetHashCode() ^
              Density.GetHashCode() ^
              FileName.GetHashCode() ^
              Format.GetHashCode() ^
              Height.GetHashCode() ^
              Interlace.GetHashCode() ^
              Width.GetHashCode();
        }

        /// <summary>
        /// Read basic information about an image.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
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
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
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
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
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
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(string fileName)
        {
            using (MagickImage image = new MagickImage())
            {
                image.Ping(fileName);
                Initialize(image);
            }
        }

        private void Initialize(MagickImage image)
        {
            ColorSpace = image.ColorSpace;
            Compression = image.Compression;
            Density = image.Density;
            FileName = image.FileName;
            Format = image.Format;
            Height = image.Height;
            Interlace = image.Interlace;
            Quality = image.Quality;
            Width = image.Width;
        }
    }
}