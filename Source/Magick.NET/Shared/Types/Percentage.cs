//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Globalization;

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
    /// <summary>
    /// Represents a percentage value.
    /// </summary>
    public struct Percentage : IEquatable<Percentage>, IComparable<Percentage>
    {
        private double _Value;

        internal static Percentage FromQuantum(double value)
        {
            return new Percentage((value / Quantum.Max) * 100);
        }

        internal double ToQuantum()
        {
            return Quantum.Max * (_Value / 100);
        }

        internal QuantumType ToQuantumType()
        {
            return (QuantumType)(Quantum.Max * (_Value / 100));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Percentage"/> struct.
        /// </summary>
        /// <param name="value">The value (0% = 0.0, 100% = 100.0)</param>
        public Percentage(double value)
        {
            _Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Percentage"/> struct.
        /// </summary>
        /// <param name="value">The value (0% = 0, 100% = 100)</param>
        public Percentage(int value)
        {
            _Value = value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Percentage"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
        /// <param name="right"> The second <see cref="Percentage"/> to compare.</param>
        public static bool operator ==(Percentage left, Percentage right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Percentage"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
        /// <param name="right"> The second <see cref="Percentage"/> to compare.</param>
        public static bool operator !=(Percentage left, Percentage right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the first <see cref="Percentage"/> is more than the second <see cref="Percentage"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
        /// <param name="right"> The second <see cref="Percentage"/> to compare.</param>
        public static bool operator >(Percentage left, Percentage right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.CompareTo(right) == 1;
        }

        /// <summary>
        /// Determines whether the first <see cref="Percentage"/> is less than the second <see cref="Percentage"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
        /// <param name="right"> The second <see cref="Percentage"/> to compare.</param>
        public static bool operator <(Percentage left, Percentage right)
        {
            if (ReferenceEquals(left, null))
                return !ReferenceEquals(right, null);

            return left.CompareTo(right) == -1;
        }

        /// <summary>
        /// Determines whether the first <see cref="Percentage"/> is less than or equal to the second <see cref="Percentage"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
        /// <param name="right"> The second <see cref="Percentage"/> to compare.</param>
        public static bool operator >=(Percentage left, Percentage right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Determines whether the first <see cref="Percentage"/> is less than or equal to the second <see cref="Percentage"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
        /// <param name="right"> The second <see cref="Percentage"/> to compare.</param>
        public static bool operator <=(Percentage left, Percentage right)
        {
            if (ReferenceEquals(left, null))
                return !ReferenceEquals(right, null);

            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Multiplies the value by the <see cref="Percentage"/>.
        /// </summary>
        /// <param name="value"> The value to use.</param>
        /// <param name="percentage">The <see cref="Percentage"/> to use.</param>
        public static double operator *(double value, Percentage percentage)
        {
            return percentage.Multiply(value);
        }

        /// <summary>
        /// Multiplies the value by the <see cref="Percentage"/>.
        /// </summary>
        /// <param name="value"> The value to use.</param>
        /// <param name="percentage">The <see cref="Percentage"/> to use.</param>
        public static int operator *(int value, Percentage percentage)
        {
            return percentage.Multiply(value);
        }

        /// <summary>
        /// Converts the specified double to an instance of this type.
        /// </summary>
        /// <param name="value">The value (0% = 0, 100% = 100)</param>
        public static explicit operator Percentage(double value)
        {
            return new Percentage(value);
        }

        /// <summary>
        /// Converts the specified int to an instance of this type.
        /// </summary>
        /// <param name="value">The value (0% = 0, 100% = 100)</param>
        public static explicit operator Percentage(int value)
        {
            return new Percentage(value);
        }

        /// <summary>
        /// Converts the specified <see cref="Percentage"/> to a double.
        /// </summary>
        /// <param name="percentage">The <see cref="Percentage"/> to convert</param>
        public static explicit operator double(Percentage percentage)
        {
            return percentage.ToDouble();
        }

        /// <summary>
        /// Converts the <see cref="Percentage"/> to a quantum type.
        /// </summary>
        /// <param name="percentage">The <see cref="Percentage"/> to convert</param>
        public static explicit operator int(Percentage percentage)
        {
            return percentage.ToInt32();
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="other">The object to compare this <see cref="Percentage"/> with.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(Percentage other)
        {
            return _Value.CompareTo(other._Value);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="Percentage"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="Percentage"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="Percentage"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() == typeof(Percentage))
                return Equals((Percentage)obj);

            if (obj.GetType() == typeof(double))
                return _Value.Equals(obj);

            if (obj.GetType() == typeof(int))
                return ((int)_Value).Equals((int)obj);

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Percentage"/> is equal to the current <see cref="Percentage"/>.
        /// </summary>
        /// <param name="other">The <see cref="Percentage"/> to compare this <see cref="Percentage"/> with.</param>
        /// <returns>True when the specified <see cref="Percentage"/> is equal to the current <see cref="Percentage"/>.</returns>
        public bool Equals(Percentage other)
        {
            return _Value.Equals(other._Value);
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return _Value.GetHashCode();
        }

        /// <summary>
        /// Multiplies the value by the percentage.
        /// </summary>
        /// <param name="value">The value to use.</param>
        /// <returns>the new value.</returns>
        public double Multiply(double value)
        {
            return (value * _Value) / 100.0;
        }

        /// <summary>
        /// Multiplies the value by the percentage.
        /// </summary>
        /// <param name="value">The value to use.</param>
        /// <returns>the new value.</returns>
        public int Multiply(int value)
        {
            return (int)((value * _Value) / 100.0);
        }

        /// <summary>
        /// Returns a double that represents the current percentage.
        /// </summary>
        /// <returns>A double that represents the current percentage.</returns>
        public double ToDouble()
        {
            return _Value;
        }

        /// <summary>
        /// Returns an integer that represents the current percentage.
        /// </summary>
        /// <returns>An integer that represents the current percentage.</returns>
        public int ToInt32()
        {
            return (int)_Value;
        }

        /// <summary>
        /// Returns a string that represents the current percentage.
        /// </summary>
        /// <returns>A string that represents the current percentage.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:0.##}%", _Value);
        }
    }
}
