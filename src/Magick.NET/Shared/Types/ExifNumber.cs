// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick
{
    /// <summary>
    /// Represents an exif number.
    /// </summary>
    public struct ExifNumber : IEquatable<ExifNumber>
    {
        private readonly uint _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExifNumber"/> struct.
        /// </summary>
        /// <param name="value">The value of the number.</param>
        public ExifNumber(uint value) => _value = value;

        /// <summary>
        /// Converts the specified <see cref="int"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator ExifNumber(int value) => new ExifNumber((uint)value);

        /// <summary>
        /// Converts the specified <see cref="uint"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator ExifNumber(uint value) => new ExifNumber(value);

        /// <summary>
        /// Converts the specified <see cref="short"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator ExifNumber(short value) => new ExifNumber((uint)value);

        /// <summary>
        /// Converts the specified <see cref="ushort"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator ExifNumber(ushort value) => new ExifNumber(value);

        /// <summary>
        /// Converts the specified <see cref="ExifNumber"/> to a <see cref="uint"/>.
        /// </summary>
        /// <param name="number">The <see cref="ExifNumber"/> to convert.</param>
        public static explicit operator uint(ExifNumber number) => number._value;

        /// <summary>
        /// Converts the specified <see cref="ExifNumber"/> to a <see cref="ushort"/>.
        /// </summary>
        /// <param name="number">The <see cref="ExifNumber"/> to convert.</param>
        public static explicit operator ushort(ExifNumber number) => (ushort)number._value;

        /// <summary>
        /// Determines whether the specified <see cref="ExifNumber"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ExifNumber"/> to compare.</param>
        /// <param name="right"> The second <see cref="ExifNumber"/> to compare.</param>
        public static bool operator ==(ExifNumber left, ExifNumber right) => Equals(left, right);

        /// <summary>
        /// Determines whether the specified <see cref="ExifNumber"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ExifNumber"/> to compare.</param>
        /// <param name="right"> The second <see cref="ExifNumber"/> to compare.</param>
        public static bool operator !=(ExifNumber left, ExifNumber right) => !Equals(left, right);

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="ExifNumber"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="ExifNumber"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="ExifNumber"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() == typeof(ExifNumber))
                return Equals((ExifNumber)obj);

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ExifNumber"/> is equal to the current <see cref="ExifNumber"/>.
        /// </summary>
        /// <param name="other">The <see cref="ExifNumber"/> to compare this <see cref="ExifNumber"/> with.</param>
        /// <returns>True when the specified <see cref="ExifNumber"/> is equal to the current <see cref="ExifNumber"/>.</returns>
        public bool Equals(ExifNumber other) => _value.Equals(other._value);

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode() => _value.GetHashCode();
    }
}