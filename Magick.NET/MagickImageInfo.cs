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
using System.IO;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains basic information about an image.
	///</summary>
	public sealed class MagickImageInfo : IEquatable<MagickImageInfo>, IComparable<MagickImageInfo>
	{
		//===========================================================================================
		private Wrapper.MagickImageInfo _Instance;
		//===========================================================================================
		private MagickImageInfo(Wrapper.MagickImageInfo instance)
		{
			_Instance = instance;
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageInfo class.
		///</summary>
		public MagickImageInfo()
		{
			_Instance = new Wrapper.MagickImageInfo();
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageInfo class using the specified blob.
		///</summary>
		///<param name="data">The byte array to read the information from.</param>
		///<exception cref="MagickException"/>
		public MagickImageInfo(Byte[] data)
			: this()
		{
			Read(data);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageInfo class using the specified filename.
		///</summary>
		///<param name="file">The file to read the image from.</param>
		///<exception cref="MagickException"/>
		public MagickImageInfo(FileInfo file)
			: this()
		{
			Read(file);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageInfo class using the specified stream.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		public MagickImageInfo(Stream stream)
			: this()
		{
			Read(stream);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageInfo class using the specified filename.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		public MagickImageInfo(string fileName)
			: this()
		{
			Read(fileName);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified MagickImageInfo instances are considered equal.
		/// </summary>
		/// <param name="left">The first MagickImageInfo to compare.</param>
		/// <param name="right"> The second MagickImageInfo to compare.</param>
		/// <returns></returns>
		public static bool operator ==(MagickImageInfo left, MagickImageInfo right)
		{
			return object.Equals(left, right);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified MagickImageInfo instances are not considered equal.
		/// </summary>
		/// <param name="left">The first MagickImageInfo to compare.</param>
		/// <param name="right"> The second MagickImageInfo to compare.</param>
		/// <returns></returns>
		public static bool operator !=(MagickImageInfo left, MagickImageInfo right)
		{
			return !object.Equals(left, right);
		}
		///==========================================================================================
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
		///==========================================================================================
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
		///==========================================================================================
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
		///==========================================================================================
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
		}
		///==========================================================================================
		///<summary>
		/// Original file name of the image (only available if read from disk).
		///</summary>
		public String FileName
		{
			get
			{
				return _Instance.FileName;
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
		/// Units of image resolution.
		///</summary>
		public Resolution ResolutionUnits
		{
			get
			{
				return _Instance.ResolutionUnits;
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
		/// Height of the image.
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
		/// Compares the current instance with another object of the same type.
		///</summary>
		///<param name="other">The object to compare this image information with.</param>
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
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current image information.
		///</summary>
		///<param name="obj">The object to compare this image information with.</param>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;

			return Equals(obj as MagickImageInfo);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified geometry is equal to the current image information.
		///</summary>
		///<param name="other">The image to compare this image information with.</param>
		public bool Equals(MagickImageInfo other)
		{
			if (ReferenceEquals(other, null))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			return
				ColorSpace == other.ColorSpace &&
				Format == other.Format &&
				Height == other.Height &&
				ResolutionUnits == other.ResolutionUnits &&
				ResolutionX == other.ResolutionX &&
				ResolutionY == other.ResolutionY &&
				Width == other.Width;
		}
		///==========================================================================================
		///<summary>
		/// Serves as a hash of this type.
		///</summary>
		public override int GetHashCode()
		{
			return
				ColorSpace.GetHashCode() ^
				Format.GetHashCode() ^
				Height.GetHashCode() ^
				ResolutionUnits.GetHashCode() ^
				ResolutionX.GetHashCode() ^
				ResolutionY.GetHashCode() ^
				Width.GetHashCode();
		}
		///==========================================================================================
		///<summary>
		/// Read basic information about an image.
		///</summary>
		///<param name="data">The byte array to read the information from.</param>
		///<exception cref="MagickException"/>
		public void Read(Byte[] data)
		{
			_Instance.Read(data);
		}
		///==========================================================================================
		///<summary>
		/// Read basic information about an image.
		///</summary>
		///<param name="file">The file to read the image from.</param>
		///<exception cref="MagickException"/>
		public void Read(FileInfo file)
		{
			Throw.IfNull("file", file);

			_Instance.Read(file.FullName);
		}
		///==========================================================================================
		///<summary>
		/// Read basic information about an image.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		public void Read(Stream stream)
		{
			_Instance.Read(stream);
		}
		///==========================================================================================
		///<summary>
		/// Read basic information about an image.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		public void Read(string fileName)
		{
			string filePath = FileHelper.CheckForBaseDirectory(fileName);
			_Instance.Read(filePath);
		}
		///==========================================================================================
		///<summary>
		/// Read basic information about an image with multiple frames/pages.
		///</summary>
		///<param name="data">The byte array to read the information from.</param>
		///<exception cref="MagickException"/>
		public static IEnumerable<MagickImageInfo> ReadCollection(Byte[] data)
		{
			foreach (Wrapper.MagickImageInfo info in Wrapper.MagickImageInfo.ReadCollection(data))
			{
				yield return new MagickImageInfo(info);
			}
		}
		///==========================================================================================
		///<summary>
		/// Read basic information about an image with multiple frames/pages.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		public static IEnumerable<MagickImageInfo> ReadCollection(string fileName)
		{
			string filePath = FileHelper.CheckForBaseDirectory(fileName);

			foreach (Wrapper.MagickImageInfo info in Wrapper.MagickImageInfo.ReadCollection(filePath))
			{
				yield return new MagickImageInfo(info);
			}
		}
		///==========================================================================================
		///<summary>
		/// Read basic information about an image with multiple frames/pages.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		public static IEnumerable<MagickImageInfo> ReadCollection(Stream stream)
		{
			foreach (Wrapper.MagickImageInfo info in Wrapper.MagickImageInfo.ReadCollection(stream))
			{
				yield return new MagickImageInfo(info);
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}