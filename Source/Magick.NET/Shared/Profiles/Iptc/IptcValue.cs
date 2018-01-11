﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;
using System.Text;

namespace ImageMagick
{
    /// <summary>
    /// A value of the iptc profile.
    /// </summary>
    public sealed class IptcValue : IEquatable<IptcValue>
    {
        private byte[] _data;
        private Encoding _encoding;

        internal IptcValue(IptcTag tag, byte[] value)
        {
            Throw.IfNull(nameof(value), value);

            Tag = tag;
            _data = value;
            _encoding = Encoding.UTF8;
        }

        internal IptcValue(IptcTag tag, Encoding encoding, string value)
        {
            Tag = tag;
            _encoding = encoding;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the encoding to use for the Value.
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                return _encoding;
            }

            set
            {
                if (value != null)
                    _encoding = value;
            }
        }

        /// <summary>
        /// Gets the tag of the iptc value.
        /// </summary>
        public IptcTag Tag
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value
        {
            get
            {
                return _encoding.GetString(_data);
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    _data = new byte[0];
                else
                    _data = _encoding.GetBytes(value);
            }
        }

        internal int Length
        {
            get
            {
                return _data.Length;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="IptcValue"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="IptcValue"/> to compare.</param>
        /// <param name="right"> The second <see cref="IptcValue"/> to compare.</param>
        public static bool operator ==(IptcValue left, IptcValue right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="IptcValue"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="IptcValue"/> to compare.</param>
        /// <param name="right"> The second <see cref="IptcValue"/> to compare.</param>
        public static bool operator !=(IptcValue left, IptcValue right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="IptcValue"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="IptcValue"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="IptcValue"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as IptcValue);
        }

        /// <summary>
        /// Determines whether the specified iptc value is equal to the current <see cref="IptcValue"/>.
        /// </summary>
        /// <param name="other">The iptc value to compare this <see cref="IptcValue"/> with.</param>
        /// <returns>True when the specified iptc value is equal to the current <see cref="IptcValue"/>.</returns>
        public bool Equals(IptcValue other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Tag != other.Tag)
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
              Tag.GetHashCode();
        }

        /// <summary>
        /// Converts this instance to a byte array.
        /// </summary>
        /// <returns>A <see cref="byte"/> array.</returns>
        public byte[] ToByteArray()
        {
            byte[] result = new byte[_data.Length];
            _data.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        /// <returns>A string that represents the current value.</returns>
        public override string ToString()
        {
            return Value;
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