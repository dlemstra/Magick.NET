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
    /// Contains the possible exif values.
    /// </summary>
    public static partial class ExifValues
    {
        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.ClipPath"/> tag.
        /// </summary>
        public static ExifByteArray ClipPath => new ExifByteArray(ExifTagValue.ClipPath, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.VersionYear"/> tag.
        /// </summary>
        public static ExifByteArray VersionYear => new ExifByteArray(ExifTagValue.VersionYear, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.XMP"/> tag.
        /// </summary>
        public static ExifByteArray XMP => new ExifByteArray(ExifTagValue.XMP, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.CFAPattern2"/> tag.
        /// </summary>
        public static ExifByteArray CFAPattern2 => new ExifByteArray(ExifTagValue.CFAPattern2, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.TIFFEPStandardID"/> tag.
        /// </summary>
        public static ExifByteArray TIFFEPStandardID => new ExifByteArray(ExifTagValue.TIFFEPStandardID, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.XMP"/> tag.
        /// </summary>
        public static ExifByteArray XPTitle => new ExifByteArray(ExifTagValue.XPTitle, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.XPComment"/> tag.
        /// </summary>
        public static ExifByteArray XPComment => new ExifByteArray(ExifTagValue.XPComment, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.XPAuthor"/> tag.
        /// </summary>
        public static ExifByteArray XPAuthor => new ExifByteArray(ExifTagValue.XPAuthor, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.XPKeywords"/> tag.
        /// </summary>
        public static ExifByteArray XPKeywords => new ExifByteArray(ExifTagValue.XPKeywords, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.XPSubject"/> tag.
        /// </summary>
        public static ExifByteArray XPSubject => new ExifByteArray(ExifTagValue.XPSubject, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.XMP"/> tag.
        /// </summary>
        public static ExifByteArray GPSVersionID => new ExifByteArray(ExifTagValue.GPSVersionID, ExifDataType.Byte);
    }
}