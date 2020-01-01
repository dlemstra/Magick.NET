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

using ImageMagick;

namespace Magick.NET.Samples
{
    /// <summary>
    /// You need to put the executable dcraw.exe into the directory that contains the Magick.NET dll.
    /// The zip file ImageMagick-6.X.X-X-Q16-x86-windows.zip that you can download from
    /// http://www.imagemagick.org/script/binary-releases.php#windows contains this file.
    /// </summary>
    public static class ReadRawImageFromCameraSamples
    {
        public static void ConvertCR2ToJPG()
        {
            using (MagickImage image = new MagickImage(SampleFiles.StillLifeCR2))
            {
                image.Write(SampleFiles.OutputDirectory + "StillLife.jpg");
            }
        }
    }
}
