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
    /// Exif value that contains a <see cref="float"/> array.
    /// </summary>
    public sealed class ExifFloatArray : ExifArrayValue<float>
    {
        internal ExifFloatArray(ExifTag tag)
            : base(tag, ExifDataType.Float)
        {
        }

        internal static ExifFloatArray Create(ExifTag tag, float[] value) => new ExifFloatArray(tag) { Value = value };
    }
}
