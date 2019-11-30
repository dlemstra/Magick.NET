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
    /// Exif value that contains a <see cref="double"/> array.
    /// </summary>
    public sealed class ExifDoubleArray : ExifArrayValue<double>
    {
        internal ExifDoubleArray(ExifTag<double[]> tag)
            : base(tag)
        {
        }

        internal ExifDoubleArray(ExifTagValue tag)
            : base(tag)
        {
        }

        /// <summary>
        /// Gets the data type of the exif value.
        /// </summary>
        public override ExifDataType DataType => ExifDataType.Double;

        internal static ExifDoubleArray Create(ExifTagValue tag, double[] value) => new ExifDoubleArray(tag) { Value = value };
    }
}
