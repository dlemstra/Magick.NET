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
    /// A value of the exif profile.
    /// </summary>
    public abstract class ExifValue : IExifValue, IEquatable<ExifTag>
    {
        internal ExifValue(ExifTag tag) => Tag = tag;

        internal ExifValue(ExifTagValue tag) => TagValue = tag;

        /// <summary>
        /// Gets the data type of the exif value.
        /// </summary>
        public abstract ExifDataType DataType { get; }

        /// <summary>
        /// Gets a value indicating whether the value is an array.
        /// </summary>
        public abstract bool IsArray { get; }

        /// <summary>
        /// Gets the tag of the exif value.
        /// </summary>
        public ExifTag Tag { get; }

        /// <summary>
        /// Gets the tag of the exif value.
        /// </summary>
        public ExifTagValue TagValue { get; }

        /// <summary>
        /// Determines whether the specified <see cref="ExifTag"/> and <see cref="ExifTag"/> are considered equal.
        /// </summary>
        /// <param name="left">The <see cref="ExifValue"/> to compare.</param>
        /// <param name="right"> The <see cref="ExifTag"/> to compare.</param>
        public static bool operator ==(ExifValue left, ExifTag right) => Equals(left, right);

        /// <summary>
        /// Determines whether the specified <see cref="ExifTag"/> and <see cref="ExifTag"/> are not considered equal.
        /// </summary>
        /// <param name="left">The <see cref="ExifValue"/> to compare.</param>
        /// <param name="right"> The <see cref="ExifTag"/> to compare.</param>
        public static bool operator !=(ExifValue left, ExifTag right) => !Equals(left, right);

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="ExifValue"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="ExifValue"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="ExifValue"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is ExifTag tag)
                return Equals(tag);

            if (obj is ExifValue value)
                return Tag.Equals(value.Tag) && Equals(GetValue(), value.GetValue());

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ExifTag"/> is equal to the current <see cref="ExifValue"/>.
        /// </summary>
        /// <param name="other">The <see cref="ExifTag"/> to compare this <see cref="ExifValue"/> with.</param>
        /// <returns>True when the specified <see cref="ExifTag"/> is equal to the current <see cref="ExifValue"/>.</returns>
        public bool Equals(ExifTag other) => Tag.Equals(other);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = Tag.GetHashCode();

            var value = GetValue();
            if (value == null)
                return hashCode;

            return hashCode ^= value.GetHashCode();
        }

        /// <summary>
        /// Gets the value of this exif value.
        /// </summary>
        /// <returns>The value of this exif value.</returns>
        public abstract object GetValue();

        /// <summary>
        /// Sets the value of this exif value.
        /// </summary>
        /// <param name="value">The value of this exif value.</param>
        /// <returns>A value indicating whether the value could be set.</returns>
        public abstract bool SetValue(object value);
    }
}