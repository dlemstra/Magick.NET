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

namespace ImageMagick
{
    /// <content/>
    public abstract partial class ExifTag
    {
        /// <summary>
        /// Gets the ClipPath exif tag.
        /// </summary>
        public static ExifTag<byte[]> ClipPath => new ExifTag<byte[]>(ExifTagValue.ClipPath);

        /// <summary>
        /// Gets the VersionYear exif tag.
        /// </summary>
        public static ExifTag<byte[]> VersionYear => new ExifTag<byte[]>(ExifTagValue.VersionYear);

        /// <summary>
        /// Gets the XMP exif tag.
        /// </summary>
        public static ExifTag<byte[]> XMP => new ExifTag<byte[]>(ExifTagValue.XMP);

        /// <summary>
        /// Gets the CFAPattern2 exif tag.
        /// </summary>
        public static ExifTag<byte[]> CFAPattern2 => new ExifTag<byte[]>(ExifTagValue.CFAPattern2);

        /// <summary>
        /// Gets the TIFFEPStandardID exif tag.
        /// </summary>
        public static ExifTag<byte[]> TIFFEPStandardID => new ExifTag<byte[]>(ExifTagValue.TIFFEPStandardID);

        /// <summary>
        /// Gets the XPTitle exif tag.
        /// </summary>
        public static ExifTag<byte[]> XPTitle => new ExifTag<byte[]>(ExifTagValue.XPTitle);

        /// <summary>
        /// Gets the XPComment exif tag.
        /// </summary>
        public static ExifTag<byte[]> XPComment => new ExifTag<byte[]>(ExifTagValue.XPComment);

        /// <summary>
        /// Gets the XPAuthor exif tag.
        /// </summary>
        public static ExifTag<byte[]> XPAuthor => new ExifTag<byte[]>(ExifTagValue.XPAuthor);

        /// <summary>
        /// Gets the XPKeywords exif tag.
        /// </summary>
        public static ExifTag<byte[]> XPKeywords => new ExifTag<byte[]>(ExifTagValue.XPKeywords);

        /// <summary>
        /// Gets the XPSubject exif tag.
        /// </summary>
        public static ExifTag<byte[]> XPSubject => new ExifTag<byte[]>(ExifTagValue.XPSubject);

        /// <summary>
        /// Gets the GPSVersionID exif tag.
        /// </summary>
        public static ExifTag<byte[]> GPSVersionID => new ExifTag<byte[]>(ExifTagValue.GPSVersionID);
    }
}