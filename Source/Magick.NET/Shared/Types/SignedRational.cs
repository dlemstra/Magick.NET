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
using System.Globalization;

namespace ImageMagick
{
    /// <summary>
    /// Represents a number that can be expressed as a fraction
    /// </summary>
    /// <remarks>
    /// This is a very simplified implementation of a rational number designed for use with metadata only.
    /// </remarks>
    public struct SignedRational : IEquatable<SignedRational>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignedRational"/> struct.
        /// </summary>
        /// <param name="value">The <see cref="double"/> to convert to an instance of this type.</param>
        public SignedRational(double value)
          : this(value, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedRational"/> struct.
        /// </summary>
        /// <param name="value">The <see cref="double"/> to convert to an instance of this type.</param>
        /// <param name="bestPrecision">Specifies if the instance should be created with the best precision possible.</param>
        public SignedRational(double value, bool bestPrecision)
        {
            BigRational rational = new BigRational(value, bestPrecision);

            Numerator = (int)rational.Numerator;
            Denominator = (int)rational.Denominator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedRational"/> struct.
        /// </summary>
        /// <param name="value">The integer to create the rational from.</param>
        public SignedRational(int value)
          : this(value, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedRational"/> struct.
        /// </summary>
        /// <param name="numerator">The number above the line in a vulgar fraction showing how many of the parts indicated by the denominator are taken.</param>
        /// <param name="denominator">The number below the line in a vulgar fraction; a divisor.</param>
        public SignedRational(int numerator, int denominator)
          : this(numerator, denominator, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedRational"/> struct.
        /// </summary>
        /// <param name="numerator">The number above the line in a vulgar fraction showing how many of the parts indicated by the denominator are taken.</param>
        /// <param name="denominator">The number below the line in a vulgar fraction; a divisor.</param>
        /// <param name="simplify">Specified if the rational should be simplified.</param>
        public SignedRational(int numerator, int denominator, bool simplify)
        {
            BigRational rational = new BigRational(numerator, denominator, simplify);

            Numerator = (int)rational.Numerator;
            Denominator = (int)rational.Denominator;
        }

        /// <summary>
        /// Gets the numerator of a number.
        /// </summary>
        public int Numerator
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the denominator of a number.
        /// </summary>
        public int Denominator
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified <see cref="SignedRational"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="SignedRational"/>  to compare.</param>
        /// <param name="right"> The second <see cref="SignedRational"/>  to compare.</param>
        public static bool operator ==(SignedRational left, SignedRational right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="SignedRational"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="SignedRational"/> to compare.</param>
        /// <param name="right"> The second <see cref="SignedRational"/> to compare.</param>
        public static bool operator !=(SignedRational left, SignedRational right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Converts the specified <see cref="double"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The <see cref="double"/> to convert to an instance of this type.</param>
        /// <returns>The <see cref="SignedRational"/>.</returns>
        public static SignedRational FromDouble(double value)
        {
            return new SignedRational(value, false);
        }

        /// <summary>
        /// Converts the specified <see cref="double"/> to an instance of this type.
        /// </summary>
        /// <param name="value">The <see cref="double"/> to convert to an instance of this type.</param>
        /// <param name="bestPrecision">Specifies if the instance should be created with the best precision possible.</param>
        /// <returns>The <see cref="SignedRational"/>.</returns>
        public static SignedRational FromDouble(double value, bool bestPrecision)
        {
            return new SignedRational(value, bestPrecision);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this <see cref="SignedRational"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare this <see cref="SignedRational"/> with.</param>
        /// <returns>True when the specified <see cref="object"/> is equal to this <see cref="SignedRational"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SignedRational)
                return Equals((SignedRational)obj);

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="SignedRational"/> is equal to this <see cref="SignedRational"/>.
        /// </summary>
        /// <param name="other">The <see cref="SignedRational"/> to compare this <see cref="SignedRational"/> with.</param>
        /// <returns>True when the specified <see cref="SignedRational"/> is equal to this <see cref="SignedRational"/>.</returns>
        public bool Equals(SignedRational other)
        {
            BigRational left = new BigRational(Numerator, Denominator);
            BigRational right = new BigRational(other.Numerator, other.Denominator);

            return left.Equals(right);
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            BigRational self = new BigRational(Numerator, Denominator);
            return self.GetHashCode();
        }

        /// <summary>
        /// Converts a rational number to the nearest <see cref="double"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double ToDouble()
        {
            return Numerator / (double)Denominator;
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of this value.</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using
        /// the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information.
        /// </param>
        /// <returns>A string representation of this value.</returns>
        public string ToString(IFormatProvider provider)
        {
            BigRational rational = new BigRational(Numerator, Denominator);
            return rational.ToString(provider);
        }
    }
}