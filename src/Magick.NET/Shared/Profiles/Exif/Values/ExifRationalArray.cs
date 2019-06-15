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
    /// Exif value that contains a <see cref="T:Rational[]"/>.
    /// </summary>
    public sealed class ExifRationalArray : ExifArrayValue<Rational>
    {
        internal ExifRationalArray(ExifTag tag)
            : base(tag, ExifDataType.Rational)
        {
        }

        internal static ExifRationalArray Create(ExifTag tag, Rational[] value) => new ExifRationalArray(tag) { Value = value };
    }
}
