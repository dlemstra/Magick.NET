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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ImageStatistics object.
	///</summary>
	public sealed class Statistics : IEquatable<Statistics>
	{
		//===========================================================================================
		private Dictionary<PixelChannel, ChannelStatistics> _Channels;
		///==========================================================================================
		/// <summary>
		/// Initializes a new instance of the Statistics class.
		/// </summary>
		/// <param name="channels">The channel statistics.</param>
		public Statistics(Dictionary<PixelChannel, ChannelStatistics> channels)
		{
			_Channels = channels;
		}
		///==========================================================================================
		///<summary>
		/// Statistics for the all the channels.
		///</summary>
		public ChannelStatistics Composite()
		{
			return GetChannel(PixelChannel.Composite);
		}
		///==========================================================================================
		///<summary>
		/// Statistics for the specified channel.
		///</summary>
		public ChannelStatistics GetChannel(PixelChannel channel)
		{
			ChannelStatistics channelStatistics;
			_Channels.TryGetValue(channel, out channelStatistics);
			return channelStatistics;
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified Statistics instances are considered equal.
		/// </summary>
		/// <param name="left">The first Statistics to compare.</param>
		/// <param name="right"> The second Statistics to compare.</param>
		/// <returns></returns>
		public static bool operator ==(Statistics left, Statistics right)
		{
			return object.Equals(left, right);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified Statistics instances are not considered equal.
		/// </summary>
		/// <param name="left">The first Statistics to compare.</param>
		/// <param name="right"> The second Statistics to compare.</param>
		/// <returns></returns>
		public static bool operator !=(Statistics left, Statistics right)
		{
			return !object.Equals(left, right);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current image statistics.
		///</summary>
		///<param name="obj">The object to compare this image statistics with.</param>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			return Equals(obj as Statistics);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified image statistics is equal to the current image statistics.
		///</summary>
		///<param name="other">The image statistics to compare this image statistics with.</param>
		public bool Equals(Statistics other)
		{

			if (ReferenceEquals(other, null))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (_Channels.Count != other._Channels.Count)
				return false;

			foreach (PixelChannel channel in _Channels.Keys)
			{
				if (!other._Channels.ContainsKey(channel))
					return false;

				if (!_Channels[channel].Equals(other._Channels[channel]))
					return false;
			}

			return true;
		}
		///==========================================================================================
		///<summary>
		/// Serves as a hash of this type.
		///</summary>
		public override int GetHashCode()
		{
			int hashCode = _Channels.GetHashCode();

			foreach (PixelChannel channel in _Channels.Keys)
			{
				hashCode = hashCode ^ _Channels[channel].GetHashCode();
			}

			return hashCode;
		}
		//===========================================================================================
	}
	//==============================================================================================
}