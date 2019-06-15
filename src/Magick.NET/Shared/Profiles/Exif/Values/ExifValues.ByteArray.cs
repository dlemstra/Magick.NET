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
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.ClipPath"/> tag.
        /// </summary>
        public static ExifByteArray ClipPath => new ExifByteArray(ExifTag.ClipPath, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.VersionYear"/> tag.
        /// </summary>
        public static ExifByteArray VersionYear => new ExifByteArray(ExifTag.VersionYear, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.XMP"/> tag.
        /// </summary>
        public static ExifByteArray XMP => new ExifByteArray(ExifTag.XMP, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.CFAPattern2"/> tag.
        /// </summary>
        public static ExifByteArray CFAPattern2 => new ExifByteArray(ExifTag.CFAPattern2, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.TIFFEPStandardID"/> tag.
        /// </summary>
        public static ExifByteArray TIFFEPStandardID => new ExifByteArray(ExifTag.TIFFEPStandardID, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.XMP"/> tag.
        /// </summary>
        public static ExifByteArray XPTitle => new ExifByteArray(ExifTag.XPTitle, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.XPComment"/> tag.
        /// </summary>
        public static ExifByteArray XPComment => new ExifByteArray(ExifTag.XPComment, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.XPAuthor"/> tag.
        /// </summary>
        public static ExifByteArray XPAuthor => new ExifByteArray(ExifTag.XPAuthor, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.XPKeywords"/> tag.
        /// </summary>
        public static ExifByteArray XPKeywords => new ExifByteArray(ExifTag.XPKeywords, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.XPSubject"/> tag.
        /// </summary>
        public static ExifByteArray XPSubject => new ExifByteArray(ExifTag.XPSubject, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.XMP"/> tag.
        /// </summary>
        public static ExifByteArray GPSVersionID => new ExifByteArray(ExifTag.GPSVersionID, ExifDataType.Byte);
    }
}