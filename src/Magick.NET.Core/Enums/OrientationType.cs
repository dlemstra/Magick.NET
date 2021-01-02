// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Specified the photo orientation of the image.
    /// </summary>
    public enum OrientationType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// TopLeft.
        /// </summary>
        TopLeft,

        /// <summary>
        /// TopRight.
        /// </summary>
        TopRight,

        /// <summary>
        /// BottomRight.
        /// </summary>
        BottomRight,

        /// <summary>
        /// BottomLeft.
        /// </summary>
        BottomLeft,

        /// <summary>
        /// LeftTop.
        /// </summary>
        LeftTop,

        /// <summary>
        /// RightTop.
        /// </summary>
        RightTop,

        /// <summary>
        /// RightBottom.
        /// </summary>
        RightBottom,

        /// <summary>
        /// LeftBotom.
        /// </summary>
        LeftBotom,
    }
}
