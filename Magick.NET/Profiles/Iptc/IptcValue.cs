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
using System.Text;

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// A value of the iptc profile.
	/// </summary>
	public sealed class IptcValue : IEquatable<IptcValue>
	{
		//===========================================================================================
		private Byte[] _Data;
		private Encoding _Encoding;
		//===========================================================================================
		internal IptcValue(IptcTag tag, Byte[] value)
		{
			Throw.IfNull("value", value);

			Tag = tag;
			_Data = value;
			_Encoding = System.Text.Encoding.Default;
		}
		//===========================================================================================
		internal IptcValue(IptcTag tag, Encoding encoding, String value)
		{
			Tag = tag;
			_Encoding = encoding;
			Value = value;
		}
		//===========================================================================================
		internal int Length
		{
			get
			{
				return _Data.Length;
			}
		}
		//===========================================================================================
		/// <summary>
		/// The encoding to use for the Value.
		/// </summary>
		public Encoding Encoding
		{
			get
			{

				return _Encoding;
			}
			set
			{
				Throw.IfNull("value", value);

				_Encoding = value;
			}
		}
		//===========================================================================================
		/// <summary>
		/// The tag of the iptc value.
		/// </summary>
		public IptcTag Tag
		{
			get;
			private set;
		}
		//===========================================================================================
		/// <summary>
		/// The value.
		/// </summary>
		public string Value
		{
			get
			{
				return _Encoding.GetString(_Data);
			}
			set
			{

				if (string.IsNullOrEmpty(value))
					_Data = new Byte[0];
				else
					_Data = _Encoding.GetBytes(value);
			}
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified IptcValue instances are considered equal.
		/// </summary>
		/// <param name="left">The first IptcValue to compare.</param>
		/// <param name="right"> The second IptcValue to compare.</param>
		/// <returns></returns>
		public static bool operator ==(IptcValue left, IptcValue right)
		{
			return object.Equals(left, right);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified ImageProfile instances are not considered equal.
		/// </summary>
		/// <param name="left">The first IptcValue to compare.</param>
		/// <param name="right"> The second IptcValue to compare.</param>
		/// <returns></returns>
		public static bool operator !=(IptcValue left, IptcValue right)
		{
			return !object.Equals(left, right);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current iptc value.
		///</summary>
		///<param name="obj">The object to compare this iptc value with.</param>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;

			return Equals(obj as IptcValue);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified iptc value is equal to the current iptc value.
		///</summary>
		///<param name="other">The iptc value to compare this iptc value with.</param>
		public bool Equals(IptcValue other)
		{
			if (ReferenceEquals(other, null))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (Tag != other.Tag)
				return false;

			if (ReferenceEquals(_Data, null))
				return ReferenceEquals(other._Data, null);

			if (ReferenceEquals(other._Data, null))
				return false;

			if (_Data.Length != other._Data.Length)
				return false;

			for (int i = 0; i < _Data.Length; i++)
			{
				if (_Data[i] != other._Data[i])
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
			return
				_Data.GetHashCode() ^
				Tag.GetHashCode();
		}
		///==========================================================================================
		///<summary>
		/// Converts this instance to a byte array.
		///</summary>
		public Byte[] ToByteArray()
		{
			Byte[] result = new Byte[_Data.Length];
			_Data.CopyTo(result, 0);
			return result;
		}
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current value.
		///</summary>
		public override string ToString()
		{
			return Value;
		}
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current value with the specified encoding.
		///</summary>
		public string ToString(Encoding encoding)
		{
			Throw.IfNull("encoding", encoding);

			return encoding.GetString(_Data);
		}
		//===========================================================================================
	}
	//==============================================================================================
}