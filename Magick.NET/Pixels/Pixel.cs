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
	///=============================================================================================
	///<summary>
	/// Class that can be used to access an individual pixel of an image.
	///</summary>
	public sealed class Pixel : IEquatable<Pixel>
	{
		//===========================================================================================
		private int _X;
		private int _Y;
		//===========================================================================================
		private static void CheckChannels(int channels)
		{
			Throw.IfTrue("value", channels < 1 || channels > 5, "Invalid number of channels (supported sizes are 1-5).");
		}
		//===========================================================================================
		private Pixel()
		{
		}
		//===========================================================================================
		private void Initialize(int x, int y, QuantumType[] value)
		{
			_X = x;
			_Y = y;
			Value = value;
		}
		//===========================================================================================
		internal QuantumType[] Value
		{
			get;
			private set;
		}
		//===========================================================================================
		internal static Pixel Create(int x, int y, QuantumType[] value)
		{
			Pixel pixel = new Pixel();
			pixel.Initialize(x, y, value);
			return pixel;
		}
		///==========================================================================================
		///<summary>
		/// Creates a new Pixel instance.
		///</summary>
		///<param name="x">The X coordinate of the pixel.</param>
		///<param name="y">The Y coordinate of the pixel.</param>
		///<param name="value">The value of the pixel.</param>
#if Q16
		[CLSCompliant(false)]
#endif
		public Pixel(int x, int y, QuantumType[] value)
		{
			Throw.IfNull("value", value);

			CheckChannels(value.Length);
			Initialize(x, y, value);
		}
		///==========================================================================================
		///<summary>
		/// Creates a new Pixel instance.
		///</summary>
		///<param name="x">The X coordinate of the pixel.</param>
		///<param name="y">The Y coordinate of the pixel.</param>
		///<param name="channels">The number of channels.</param>
		public Pixel(int x, int y, int channels)
		{
			CheckChannels(channels);
			Initialize(x, y, new QuantumType[channels]);
		}
		///==========================================================================================
		///<summary>
		/// Returns the value of the specified channel.
		///</summary>
#if Q16
		[CLSCompliant(false)]
#endif
		public QuantumType this[int channel]
		{
			get
			{
				return GetChannel(channel);
			}
			set
			{
				SetChannel(channel, value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the number of channels that the pixel contains.
		///</summary>
		public int Channels
		{
			get
			{
				return Value.Length;
			}
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate of the pixel.
		///</summary>
		public int X
		{
			get
			{
				return _X;
			}
			set
			{
				if (value < 0)
					return;

				_X = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate of the pixel.
		///</summary>
		public int Y
		{
			get
			{
				return _Y;
			}
			set
			{
				if (value < 0)
					return;

				_Y = value;
			}
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified Pixel instances are considered equal.
		/// </summary>
		/// <param name="left">The first Pixel to compare.</param>
		/// <param name="right"> The second Pixel to compare.</param>
		/// <returns></returns>
		public static bool operator ==(Pixel left, Pixel right)
		{
			return object.Equals(left, right);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified Pixel instances are considered equal.
		/// </summary>
		/// <param name="left">The first Pixel to compare.</param>
		/// <param name="right"> The second Pixel to compare.</param>
		/// <returns></returns>
		public static bool operator !=(Pixel left, Pixel right)
		{
			return !object.Equals(left, right);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current pixel.
		///</summary>
		///<param name="obj">The object to compare pixel color with.</param>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;

			return Equals(obj as Pixel);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified pixel is equal to the current pixel.
		///</summary>
		///<param name="other">The pixel to compare this color with.</param>
		public bool Equals(Pixel other)
		{
			if (ReferenceEquals(other, null))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (Channels != other.Channels)
				return false;

			for (int i = 0; i < Value.Length; i++)
			{
				if (Value[i] != other.Value[i])
					return false;
			}

			return true;
		}
		///==========================================================================================
		///<summary>
		/// Returns the value of the specified channel.
		///</summary>
		///<param name="channel">The channel to get the value of.</param>
#if Q16
		[CLSCompliant(false)]
#endif
		public QuantumType GetChannel(int channel)
		{
			if (channel < 0 || channel >= Value.Length)
				return 0;

			return Value[channel];
		}
		///==========================================================================================
		///<summary>
		/// Serves as a hash of this type.
		///</summary>
		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
		///==========================================================================================
		///<summary>
		/// Set the value of the specified channel.
		///</summary>
		///<param name="channel">The channel to set the value of.</param>
		///<param name="value">The value.</param>
#if Q16
		[CLSCompliant(false)]
#endif
		public void SetChannel(int channel, QuantumType value)
		{
			if (channel < 0 || channel >= Value.Length)
				return;

			Value[channel] = value;
		}
		///==========================================================================================
		///<summary>
		/// Converts the pixel to a color. Assumes the pixel is RGBA.
		///</summary>
		public MagickColor ToColor()
		{
			if (Value.Length == 3)
				return new MagickColor(Value[0], Value[1], Value[2]);

			if (Value.Length == 4)
				return new MagickColor(Value[0], Value[1], Value[2], Value[3]);

			return null;
		}
		//===========================================================================================
	}
	//==============================================================================================
}