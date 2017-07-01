// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Text;

namespace ImageMagick
{
    /// <summary>
    /// A value of the 8bim profile.
    /// </summary>
    public sealed class EightBimValue : IEquatable<EightBimValue>
    {
        private readonly byte[] _data;

        internal EightBimValue(short id, byte[] data)
        {
            ID = id;
            _data = data;
        }

        /// <summary>
        /// Gets the ID of the 8bim value
        /// </summary>
        public short ID
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified <see cref="EightBimValue"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="EightBimValue"/> to compare.</param>
        /// <param name="right"> The second <see cref="EightBimValue"/> to compare.</param>
        public static bool operator ==(EightBimValue left, EightBimValue right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="EightBimValue"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="EightBimValue"/> to compare.</param>
        /// <param name="right"> The second <see cref="EightBimValue"/> to compare.</param>
        public static bool operator !=(EightBimValue left, EightBimValue right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="EightBimValue"/>.
        /// </summary>
        /// <param name="obj">The object to compare this 8bim value with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="EightBimValue"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as EightBimValue);
        }

        /// <summary>
        /// Determines whether the specified <see cref="EightBimValue"/> is equal to the current <see cref="EightBimValue"/>.
        /// </summary>
        /// <param name="other">The <see cref="EightBimValue"/> to compare this <see cref="EightBimValue"/> with.</param>
        /// <returns>True when the specified <see cref="EightBimValue"/> is equal to the current <see cref="EightBimValue"/>.</returns>
        public bool Equals(EightBimValue other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (ID != other.ID)
                return false;

            if (ReferenceEquals(_data, null))
                return ReferenceEquals(other._data, null);

            if (ReferenceEquals(other._data, null))
                return false;

            if (_data.Length != other._data.Length)
                return false;

            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i] != other._data[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return
              _data.GetHashCode() ^
              ID.GetHashCode();
        }

        /// <summary>
        /// Converts this instance to a byte array.
        /// </summary>
        /// <returns>A <see cref="byte"/> array.</returns>
        public byte[] ToByteArray()
        {
            byte[] data = new byte[_data.Length];
            Array.Copy(_data, 0, data, 0, _data.Length);
            return data;
        }

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        /// <returns>A string that represents the current value.</returns>
        public override string ToString()
        {
            return ToString(Encoding.UTF8);
        }

        /// <summary>
        /// Returns a string that represents the current value with the specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>A string that represents the current value with the specified encoding.</returns>
        public string ToString(Encoding encoding)
        {
            Throw.IfNull(nameof(encoding), encoding);

            return encoding.GetString(_data);
        }
    }
}