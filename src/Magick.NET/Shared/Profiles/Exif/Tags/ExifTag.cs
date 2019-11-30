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
    /// Class that represents an exif tag from the Exif standard 2.31.
    /// </summary>
    public abstract partial class ExifTag : IEquatable<ExifTag>
    {
        private readonly ushort _value;

        internal ExifTag(ushort value) => _value = value;

        /// <summary>
        /// Converts the specified <see cref="ExifTag"/> to a <see cref="ushort"/>.
        /// </summary>
        /// <param name="tag">The <see cref="ExifTag"/> to convert.</param>
        public static explicit operator ushort(ExifTag tag) => tag?._value ?? (ushort)ExifTagValue.Unknown;

        /// <summary>
        /// Determines whether the specified <see cref="ExifTag"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ExifTag"/> to compare.</param>
        /// <param name="right"> The second <see cref="ExifTag"/> to compare.</param>
        public static bool operator ==(ExifTag left, ExifTag right) => Equals(left, right);

        /// <summary>
        /// Determines whether the specified <see cref="ExifTag"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ExifTag"/> to compare.</param>
        /// <param name="right"> The second <see cref="ExifTag"/> to compare.</param>
        public static bool operator !=(ExifTag left, ExifTag right) => !Equals(left, right);

        /// <summary>
        /// Determines whether the specified object is equal to the exif tag.
        /// </summary>
        /// <param name="obj">The object to compare this exif tag with.</param>
        /// <returns>True when the specified object is equal to the current exif tag.</returns>
        public override bool Equals(object obj)
        {
            if (obj is ExifTag value)
                return Equals(value);

            return false;
        }

        /// <summary>
        /// Determines whether the specified the exif tag is equal to the exif tag.
        /// </summary>
        /// <param name="other">The the exif tag to compare this exif tag with.</param>
        /// <returns>True when the specified object is equal to the current exif tag.</returns>
        public bool Equals(ExifTag other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return _value == other._value;
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode() => _value.GetHashCode();

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => ((ExifTagValue)_value).ToString();
    }
}