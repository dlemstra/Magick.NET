// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
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
        /// Gets the PixelScale exif tag.
        /// </summary>
        public static ExifTag<double[]> PixelScale { get; } = new ExifTag<double[]>(ExifTagValue.PixelScale);

        /// <summary>
        /// Gets the IntergraphMatrix exif tag.
        /// </summary>
        public static ExifTag<double[]> IntergraphMatrix { get; } = new ExifTag<double[]>(ExifTagValue.IntergraphMatrix);

        /// <summary>
        /// Gets the ModelTiePoint exif tag.
        /// </summary>
        public static ExifTag<double[]> ModelTiePoint { get; } = new ExifTag<double[]>(ExifTagValue.ModelTiePoint);

        /// <summary>
        /// Gets the ModelTransform exif tag.
        /// </summary>
        public static ExifTag<double[]> ModelTransform { get; } = new ExifTag<double[]>(ExifTagValue.ModelTransform);
    }
}