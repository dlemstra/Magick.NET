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
using System.Text;

namespace ImageMagick
{
  /// <summary>
  /// A value of the 8bim profile.
  /// </summary>
  public sealed class EightBimValue : IEquatable<EightBimValue>
  {
    private byte[] _Data;

    internal EightBimValue(short id, byte[] data)
    {
      ID = id;
      _Data = data;
    }

    /// <summary>
    /// The ID of the 8bim value
    /// </summary>
    public short ID
    {
      get;
      private set;
    }

    /// <summary>
    /// Determines whether the specified EightBimValue instances are considered equal.
    /// </summary>
    /// <param name="left">The first EightBimValue to compare.</param>
    /// <param name="right"> The second EightBimValue to compare.</param>
    /// <returns></returns>
    public static bool operator ==(EightBimValue left, EightBimValue right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified EightBimValue instances are not considered equal.
    /// </summary>
    /// <param name="left">The first EightBimValue to compare.</param>
    /// <param name="right"> The second EightBimValue to compare.</param>
    /// <returns></returns>
    public static bool operator !=(EightBimValue left, EightBimValue right)
    {
      return !Equals(left, right);
    }

    ///<summary>
    /// Determines whether the specified object is equal to the current 8bim value.
    ///</summary>
    ///<param name="obj">The object to compare this 8bim value with.</param>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as EightBimValue);
    }

    ///<summary>
    /// Determines whether the specified 8bim value is equal to the current 8bim value.
    ///</summary>
    ///<param name="other">The exif value to compare this 8bim value with.</param>
    public bool Equals(EightBimValue other)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      if (ID != other.ID)
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

    ///<summary>
    /// Serves as a hash of this type.
    ///</summary>
    public override int GetHashCode()
    {

      return
        _Data.GetHashCode() ^
        ID.GetHashCode();
    }

    ///<summary>
    /// Converts this instance to a byte array.
    ///</summary>
    public byte[] ToByteArray()
    {
      byte[] data = new byte[_Data.Length];
      Array.Copy(_Data, 0, data, 0, _Data.Length);
      return data;
    }

    ///<summary>
    /// Returns a string that represents the current value.
    ///</summary>
    public override string ToString()
    {
      return ToString(Encoding.UTF8);
    }

    ///<summary>
    /// Returns a string that represents the current value with the specified encoding.
    ///</summary>
    public string ToString(Encoding encoding)
    {
      Throw.IfNull("encoding", encoding);

      return encoding.GetString(_Data);
    }
  }
}