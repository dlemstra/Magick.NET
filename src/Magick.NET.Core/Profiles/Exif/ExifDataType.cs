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
    /// <summary>
    /// Specifies exif data types.
    /// </summary>
    public enum ExifDataType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Byte.
        /// </summary>
        Byte,

        /// <summary>
        /// String.
        /// </summary>
        String,

        /// <summary>
        /// Short.
        /// </summary>
        Short,

        /// <summary>
        /// Long.
        /// </summary>
        Long,

        /// <summary>
        /// Rational.
        /// </summary>
        Rational,

        /// <summary>
        /// SignedByte.
        /// </summary>
        SignedByte,

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// SignedShort.
        /// </summary>
        SignedShort,

        /// <summary>
        /// SignedLong.
        /// </summary>
        SignedLong,

        /// <summary>
        /// SignedRational.
        /// </summary>
        SignedRational,

        /// <summary>
        /// Float.
        /// </summary>
        Float,

        /// <summary>
        /// Double.
        /// </summary>
        Double,
    }
}