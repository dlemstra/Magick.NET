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

using System.Globalization;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains information about an image format.
	///</summary>
	public sealed class MagickFormatInfo
	{
		//===========================================================================================
		private Wrapper.MagickFormatInfo _Instance;
		//===========================================================================================
		private MagickFormatInfo(Wrapper.MagickFormatInfo instance)
		{
			_Instance = instance;
		}
		//===========================================================================================
		internal static MagickFormatInfo Create(Wrapper.MagickFormatInfo value)
		{
			if (value == null)
				return null;

			return new MagickFormatInfo(value);
		}
		///==========================================================================================
		///<summary>
		/// The format can read multithreaded.
		///</summary>
		public bool CanReadMultithreaded
		{
			get
			{
				return _Instance.CanReadMultithreaded;
			}
		}
		///==========================================================================================
		///<summary>
		/// The format can write multithreaded.
		///</summary>
		public bool CanWriteMultithreaded
		{
			get
			{
				return _Instance.CanWriteMultithreaded;
			}
		}
		///==========================================================================================
		///<summary>
		/// The description of the format.
		///</summary>
		public string Description
		{
			get
			{
				return _Instance.Description;
			}
		}
		///==========================================================================================
		///<summary>
		/// The format.
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
		/// Format supports multiple frames.
		///</summary>
		public bool IsMultiFrame
		{
			get
			{
				return _Instance.IsMultiFrame;
			}
		}
		///==========================================================================================
		///<summary>
		/// Format is readable.
		///</summary>
		public bool IsReadable
		{
			get
			{
				return _Instance.IsReadable;
			}
		}
		///==========================================================================================
		///<summary>
		/// Format is writable.
		///</summary>
		public bool IsWritable
		{
			get
			{
				return _Instance.IsWritable;
			}
		}
		///==========================================================================================
		///<summary>
		/// The mime type.
		///</summary>
		public string MimeType
		{
			get
			{
				return _Instance.MimeType;
			}
		}
		///==========================================================================================
		///<summary>
		/// The module.
		///</summary>
		public MagickFormat Module
		{
			get
			{
				return _Instance.Module;
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current format.
		///</summary>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1} ({2}R{3}W{4}M)", Format, Description,
				IsReadable ? "+" : "-", IsWritable ? "+" : "-", IsMultiFrame ? "+" : "-");
		}
		///==========================================================================================
		///<summary>
		/// Unregisters this format.
		///</summary>
		public bool Unregister()
		{
			return _Instance.Unregister();
		}
		//===========================================================================================
	}
	//==============================================================================================
}