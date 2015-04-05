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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to set the limits to the resources that are being used.
	///</summary>
	public static class ResourceLimits
	{
		///==========================================================================================
		///<summary>
		/// Pixel cache limit in bytes. Requests for memory above this limit will fail.
		///</summary>
		[CLSCompliant(false)]
		public static ulong Disk
		{
			get
			{
				return Wrapper.ResourceLimits.Disk;
			}
			set
			{
				Wrapper.ResourceLimits.Disk = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The maximum width of an image.
		///</summary>
		[CLSCompliant(false)]
		public static ulong Height
		{
			get
			{
				return Wrapper.ResourceLimits.Height;
			}
			set
			{
				Wrapper.ResourceLimits.Height = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Pixel cache limit in bytes. Once this memory limit is exceeded, all subsequent pixels cache
		/// operations are to/from disk.
		///</summary>
		[CLSCompliant(false)]
		public static ulong Memory
		{
			get
			{
				return Wrapper.ResourceLimits.Memory;
			}
			set
			{
				Wrapper.ResourceLimits.Memory = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Limits the number of threads used in multithreaded operations.
		///</summary>
		[CLSCompliant(false)]
		public static ulong Thread
		{
			get
			{
				return Wrapper.ResourceLimits.Thread;
			}
			set
			{
				Wrapper.ResourceLimits.Thread = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Periodically yield the CPU for at least the time specified in milliseconds.
		///</summary>
		[CLSCompliant(false)]
		public static ulong Throttle
		{
			get
			{
				return Wrapper.ResourceLimits.Throttle;
			}
			set
			{
				Wrapper.ResourceLimits.Throttle = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The maximum width of an image.
		///</summary>
		[CLSCompliant(false)]
		public static ulong Width
		{
			get
			{
				return Wrapper.ResourceLimits.Width;
			}
			set
			{
				Wrapper.ResourceLimits.Width = value;
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}