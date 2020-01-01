// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public struct Number : IEquatable<Number>, IComparable<Number>
    {
        private readonly uint _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="value">The value of the number.</param>
        public Number(uint value) => _value = value;

        /// <summary>
        /// Converts the specified <see cref="int"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator Number(int value) => new Number((uint)value);

        /// <summary>
        /// Converts the specified <see cref="uint"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator Number(uint value) => new Number(value);

        /// <summary>
        /// Converts the specified <see cref="short"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator Number(short value) => new Number((uint)value);

        /// <summary>
        /// Converts the specified <see cref="ushort"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator Number(ushort value) => new Number(value);

        /// <summary>
        /// Converts the specified <see cref="Number"/> to a <see cref="uint"/>.
        /// </summary>
        /// <param name="number">The <see cref="Number"/> to convert.</param>
        public static explicit operator uint(Number number) => number._value;

        /// <summary>
        /// Converts the specified <see cref="Number"/> to a <see cref="ushort"/>.
        /// </summary>
        /// <param name="number">The <see cref="Number"/> to convert.</param>
        public static explicit operator ushort(Number number) => (ushort)number._value;

        /// <summary>
        /// Determines whether the specified <see cref="Number"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="Number"/> to compare.</param>
        /// <param name="right"> The second <see cref="Number"/> to compare.</param>
        public static bool operator ==(Number left, Number right) => Equals(left, right);

        /// <summary>
        /// Determines whether the specified <see cref="Number"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="Number"/> to compare.</param>
        /// <param name="right"> The second <see cref="Number"/> to compare.</param>
        public static bool operator !=(Number left, Number right) => !Equals(left, right);

        /// <summary>
        /// Determines whether the first <see cref="Number"/> is more than the second <see cref="Number"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Number"/> to compare.</param>
        /// <param name="right"> The second <see cref="Number"/> to compare.</param>
        public static bool operator >(Number left, Number right) => left.CompareTo(right) == 1;

        /// <summary>
        /// Determines whether the first <see cref="Number"/> is less than the second <see cref="Number"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Number"/> to compare.</param>
        /// <param name="right"> The second <see cref="Number"/> to compare.</param>
        public static bool operator <(Number left, Number right) => left.CompareTo(right) == -1;

        /// <summary>
        /// Determines whether the first <see cref="Number"/> is more than or equal to the second <see cref="Number"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Number"/> to compare.</param>
        /// <param name="right"> The second <see cref="Number"/> to compare.</param>
        public static bool operator >=(Number left, Number right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Determines whether the first <see cref="Number"/> is less than or equal to the second <see cref="Number"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Number"/> to compare.</param>
        /// <param name="right"> The second <see cref="Number"/> to compare.</param>
        public static bool operator <=(Number left, Number right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="other">The object to compare this color with.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(Number other) => _value.CompareTo(other._value);

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="Number"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="Number"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="Number"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() == typeof(Number))
                return Equals((Number)obj);

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Number"/> is equal to the current <see cref="Number"/>.
        /// </summary>
        /// <param name="other">The <see cref="Number"/> to compare this <see cref="Number"/> with.</param>
        /// <returns>True when the specified <see cref="Number"/> is equal to the current <see cref="Number"/>.</returns>
        public bool Equals(Number other) => _value.Equals(other._value);

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance, which consists of a sequence of digits ranging from 0 to 9, without a sign or leading zeros.</returns>
        public string ToString(IFormatProvider provider) => _value.ToString(provider);
    }
}