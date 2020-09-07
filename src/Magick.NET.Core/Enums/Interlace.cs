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
    /// Specifies the interlace types.
    /// </summary>
    public enum Interlace
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// NoInterlace.
        /// </summary>
        NoInterlace,

        /// <summary>
        /// Line.
        /// </summary>
        Line,

        /// <summary>
        /// Plane.
        /// </summary>
        Plane,

        /// <summary>
        /// Partition.
        /// </summary>
        Partition,

        /// <summary>
        /// Gif.
        /// </summary>
        Gif,

        /// <summary>
        /// Jpeg.
        /// </summary>
        Jpeg,

        /// <summary>
        /// Png.
        /// </summary>
        Png,
    }
}