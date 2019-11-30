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

namespace ImageMagick
{
    /// <summary>
    /// A value of the exif profile.
    /// </summary>
    public abstract class ExifValue : IExifValue
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