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

using System.IO;
using ImageMagick;

namespace Magick.NET.Tests
{
    public partial class TiffWriteDefinesTests
    {
        private static IMagickImage WriteTiff(IMagickImage image)
        {
            using (var memStream = new MemoryStream())
            {
                image.Format = MagickFormat.Tiff;
                image.Write(memStream);
                memStream.Position = 0;
                return new MagickImage(memStream);
            }
        }
    }
}
