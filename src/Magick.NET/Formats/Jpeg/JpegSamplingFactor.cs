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

namespace ImageMagick.Formats.Jpeg
{
    /// <summary>
    /// Specifies the sampling factor.
    /// </summary>
    public enum JpegSamplingFactor
    {
        /// <summary>
        /// 4:4:4.
        /// </summary>
        Ratio444,

        /// <summary>
        /// 4:2:2.
        /// </summary>
        Ratio422,

        /// <summary>
        /// 4:1:1.
        /// </summary>
        Ratio411,

        /// <summary>
        /// 4:4:0.
        /// </summary>
        Ratio440,

        /// <summary>
        /// 4:2:0.
        /// </summary>
        Ratio420,

        /// <summary>
        /// 4:1:0.
        /// </summary>
        Ratio410,
    }
}