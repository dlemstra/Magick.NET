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

using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Interface that  describes an Exif profile.
    /// </summary>
    public interface IExifProfile : IImageProfile
    {
        /// <summary>
        /// Gets or sets which parts will be written when the profile is added to an image.
        /// </summary>
        ExifParts Parts { get; set; }

        /// <summary>
        /// Gets the tags that where found but contained an invalid value.
        /// </summary>
        IEnumerable<ExifTag> InvalidTags { get; }

        /// <summary>
        /// Gets the values of this exif profile.
        /// </summary>
        IEnumerable<IExifValue> Values { get; }

        /// <summary>
        /// Returns the thumbnail in the exif profile when available.
        /// </summary>
        /// <returns>The thumbnail in the exif profile when available.</returns>
        IMagickImage CreateThumbnail();

        /// <summary>
        /// Returns the value with the specified tag.
        /// </summary>
        /// <param name="tag">The tag of the exif value.</param>
        /// <returns>The value with the specified tag.</returns>
        /// <typeparam name="TValueType">The data type of the tag.</typeparam>
        IExifValue<TValueType> GetValue<TValueType>(ExifTag<TValueType> tag);

        /// <summary>
        /// Removes the thumbnail in the exif profile.
        /// </summary>
        void RemoveThumbnail();

        /// <summary>
        /// Removes the value with the specified tag.
        /// </summary>
        /// <param name="tag">The tag of the exif value.</param>
        /// <returns>True when the value was fount and removed.</returns>
        bool RemoveValue(ExifTag tag);

        /// <summary>
        /// Sets the value of the specified tag.
        /// </summary>
        /// <param name="tag">The tag of the exif value.</param>
        /// <param name="value">The value.</param>
        /// <typeparam name="TValueType">The data type of the tag.</typeparam>
        void SetValue<TValueType>(ExifTag<TValueType> tag, TValueType value);
    }
}