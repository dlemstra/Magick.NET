// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.IO;

namespace ImageMagick
{
    internal static class ImageOptimizerHelper
    {
        public static void CheckFormat(IMagickImage image, MagickFormat expectedFormat)
        {
            MagickFormat format = image.FormatInfo.Module;
            if (format != expectedFormat)
                throw new MagickCorruptImageErrorException("Invalid image format: " + format.ToString());
        }

        internal static void CheckStream([ValidatedNotNull] Stream stream)
        {
            Throw.IfNullOrEmpty(nameof(stream), stream);
            Throw.IfFalse(nameof(stream), stream.CanRead, "The stream should be readable.");
            Throw.IfFalse(nameof(stream), stream.CanWrite, "The stream should be writeable.");
            Throw.IfFalse(nameof(stream), stream.CanSeek, "The stream should be seekable.");
        }
    }
}
