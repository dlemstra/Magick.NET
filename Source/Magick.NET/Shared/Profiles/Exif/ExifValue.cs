// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace ImageMagick
{
    /// <summary>
    /// A value of the exif profile.
    /// </summary>
    public sealed class ExifValue : IEquatable<ExifValue>
    {
        private object _value;

        internal ExifValue(ExifTag tag, ExifDataType dataType, bool isArray)
        {
            Tag = tag;
            DataType = dataType;
            IsArray = isArray;

            if (dataType == ExifDataType.Ascii)
                IsArray = false;
        }

        internal ExifValue(ExifTag tag, ExifDataType dataType, object value, bool isArray)
          : this(tag, dataType, isArray)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the data type of the exif value.
        /// </summary>
        public ExifDataType DataType
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the value is an array.
        /// </summary>
        public bool IsArray
        {
            get;
        }

        /// <summary>
        /// Gets the tag of the exif value.
        /// </summary>
        public ExifTag Tag
        {
            get;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }

            set
            {
                CheckValue(value);
                _value = value;
            }
        }

        internal bool HasValue
        {
            get
            {
                if (_value == null)
                    return false;

                if (DataType == ExifDataType.Ascii)
                    return ((string)_value).Length > 0;

                return true;
            }
        }

        internal int Length
        {
            get
            {
                if (_value == null)
                    return 4;

                int size = (int)(GetSize(DataType) * NumberOfComponents);

                return size < 4 ? 4 : size;
            }
        }

        internal int NumberOfComponents
        {
            get
            {
                if (DataType == ExifDataType.Ascii)
                    return Encoding.UTF8.GetBytes((string)_value).Length;

                if (IsArray)
                    return ((Array)_value).Length;

                return 1;
            }
        }

        /// <summary>
        /// Converts the string representation of a value to the correct representation based on the EXIF tag to generate.
        /// </summary>
        /// <param name="exifTag">The EXIF tag of the resulting EXIF value</param>
        /// <param name="value">The value to try and set</param>
        /// <param name="result">The newly created ExifValue</param>
        /// <returns>Indicates whether the conversion was successful or not.</returns>
        public static bool TryParse(ExifTag exifTag, string value, out ExifValue result)
        {
            var exifDefinition = (ExifDefinition)ExifDefinition.ExifDefinitions[exifTag];
            Throw.IfTrue(nameof(exifTag), exifDefinition == null || exifTag == ExifTag.Unknown, "Invalid Tag");

            // ReSharper disable once PossibleNullReferenceException - Doesn't recognise Throw.IfTrue
            var internalResult = new ExifValue(exifDefinition.ExifTag, exifDefinition.ExifDataType, exifDefinition.IsArray);

            switch (exifDefinition.ExifDataType)
            {
                case ExifDataType.Byte:
                    if (exifDefinition.IsArray)
                    {
                        internalResult.Value = Encoding.UTF8.GetBytes(value);
                    }
                    else
                    {
                        internalResult.Value = Convert.ToByte(value);
                    }

                    break;
                case ExifDataType.Ascii:
                    internalResult.Value = value;
                    break;
                case ExifDataType.Short:
                    if (ushort.TryParse(value, out var ushortResult))
                        internalResult.Value = ushortResult;

                    break;
                case ExifDataType.Long:
                    if (ulong.TryParse(value, out var ulongResult))
                        internalResult.Value = ulongResult;

                    break;
                case ExifDataType.SignedRational:
                case ExifDataType.Rational:
                    if (double.TryParse(value, out var doubleResult))
                        internalResult.Value = Rational.FromDouble(doubleResult);

                    break;
                case ExifDataType.SignedByte:
                    internalResult.Value = Convert.ToByte(value);
                    break;
                case ExifDataType.SignedShort:
                    if (short.TryParse(value, out var shortResult))
                        internalResult.Value = shortResult;

                    break;
                case ExifDataType.SignedLong:
                    if (long.TryParse(value, out var longResult))
                        internalResult.Value = longResult;

                    break;
                case ExifDataType.SingleFloat:
                    if (float.TryParse(value, out var singlefloatResult))
                        internalResult.Value = singlefloatResult;

                    break;
                case ExifDataType.DoubleFloat:
                    if (double.TryParse(value, out var doubleFloatResult))
                        internalResult.Value = doubleFloatResult;

                    break;
            }

            result = internalResult.HasValue
                ? null
                : internalResult;

            return internalResult.HasValue;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ExifValue"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ExifValue"/> to compare.</param>
        /// <param name="right"> The second <see cref="ExifValue"/>to compare.</param>
        public static bool operator ==(ExifValue left, ExifValue right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ExifValue"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ExifValue"/> to compare.</param>
        /// <param name="right"> The second <see cref="ExifValue"/> to compare.</param>
        public static bool operator !=(ExifValue left, ExifValue right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="ExifValue"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="ExifValue"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="ExifValue"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as ExifValue);
        }

        /// <summary>
        /// Determines whether the specified exif value is equal to the current <see cref="ExifValue"/>.
        /// </summary>
        /// <param name="other">The exif value to compare this <see cref="ExifValue"/> with.</param>
        /// <returns>True when the specified exif value is equal to the current <see cref="ExifValue"/>.</returns>
        public bool Equals(ExifValue other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
              Tag == other.Tag &&
              DataType == other.DataType &&
              Equals(_value, other._value);
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            int hashCode = Tag.GetHashCode() ^ DataType.GetHashCode();
            return _value != null ? hashCode ^ _value.GetHashCode() : hashCode;
        }

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        /// <returns>A string that represents the current value.</returns>
        public override string ToString()
        {
            if (_value == null)
                return null;

            if (DataType == ExifDataType.Ascii)
                return (string)_value;

            if (!IsArray)
                return ToString(_value);

            StringBuilder sb = new StringBuilder();
            foreach (object value in (Array)_value)
            {
                sb.Append(ToString(value));
                sb.Append(" ");
            }

            return sb.ToString();
        }

        internal static ExifValue Create(ExifTag tag, object value)
        {
            Throw.IfTrue(nameof(tag), tag == ExifTag.Unknown, "Invalid Tag");

            var isValid = TryParse(tag, value.ToString(), out var result);
            if (result == null || !isValid)
                throw new NotSupportedException();

            return result;
        }

        internal static uint GetSize(ExifDataType dataType)
        {
            switch (dataType)
            {
                case ExifDataType.Ascii:
                case ExifDataType.Byte:
                case ExifDataType.SignedByte:
                case ExifDataType.Undefined:
                    return 1;
                case ExifDataType.Short:
                case ExifDataType.SignedShort:
                    return 2;
                case ExifDataType.Long:
                case ExifDataType.SignedLong:
                case ExifDataType.SingleFloat:
                    return 4;
                case ExifDataType.DoubleFloat:
                case ExifDataType.Rational:
                case ExifDataType.SignedRational:
                    return 8;
                default:
                    throw new NotSupportedException(dataType.ToString());
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Cannot avoid it here.")]
        private void CheckValue(object value)
        {
            if (value == null)
                return;

            Type type = value.GetType();

            if (DataType == ExifDataType.Ascii)
            {
                Throw.IfFalse(nameof(value), type == typeof(string), "Value should be a string.");
                return;
            }

            if (type.IsArray)
            {
                Throw.IfTrue(nameof(value), !IsArray, "Value should not be an array.");
                type = type.GetElementType();
            }
            else
            {
                Throw.IfTrue(nameof(value), IsArray, "Value should be an array.");
            }

            switch (DataType)
            {
                case ExifDataType.Byte:
                    Throw.IfFalse(nameof(value), type == typeof(byte), "Value should be a byte{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.DoubleFloat:
                    Throw.IfFalse(nameof(value), type == typeof(double), "Value should be a double{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.Long:
                    Throw.IfFalse(nameof(value), type == typeof(uint), "Value should be an unsigned int{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.Rational:
                    Throw.IfFalse(nameof(value), type == typeof(Rational), "Value should be a rational{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.Short:
                    Throw.IfFalse(nameof(value), type == typeof(ushort), "Value should be an unsigned short{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SignedByte:
                    Throw.IfFalse(nameof(value), type == typeof(sbyte), "Value should be a signed byte{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SignedLong:
                    Throw.IfFalse(nameof(value), type == typeof(int), "Value should be an int{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SignedRational:
                    Throw.IfFalse(nameof(value), type == typeof(SignedRational), "Value should be a signed rational{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SignedShort:
                    Throw.IfFalse(nameof(value), type == typeof(short), "Value should be a short{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SingleFloat:
                    Throw.IfFalse(nameof(value), type == typeof(float), "Value should be a float{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.Undefined:
                    Throw.IfFalse(nameof(value), type == typeof(byte), "Value should be a byte array.");
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private string ToString(object value)
        {
            string description = ExifTagDescriptionAttribute.GetDescription(Tag, value);
            if (description != null)
                return description;

            switch (DataType)
            {
                case ExifDataType.Ascii:
                    return (string)value;
                case ExifDataType.Byte:
                    return ((byte)value).ToString("X2", CultureInfo.InvariantCulture);
                case ExifDataType.DoubleFloat:
                    return ((double)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.Long:
                    return ((uint)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.Rational:
                    return ((Rational)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.Short:
                    return ((ushort)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.SignedByte:
                    return ((sbyte)value).ToString("X2", CultureInfo.InvariantCulture);
                case ExifDataType.SignedLong:
                    return ((int)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.SignedRational:
                    return ((SignedRational)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.SignedShort:
                    return ((short)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.SingleFloat:
                    return ((float)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.Undefined:
                    return ((byte)value).ToString("X2", CultureInfo.InvariantCulture);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
