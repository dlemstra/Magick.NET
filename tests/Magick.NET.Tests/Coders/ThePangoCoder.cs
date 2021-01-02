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

using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ThePangoCoder
    {
        [Fact]
        public void IsThreadSafe()
        {
            string LoadImage()
            {
                using (var image = new MagickImage("pango:1"))
                {
                    return image.Signature;
                }
            }

            string signature = LoadImage();
            Parallel.For(1, 10, (int i) =>
            {
                Assert.Equal(signature, LoadImage());
            });
        }

        [Fact]
        public void CanReadFromLargePangoFile()
        {
            string fileName = "pango:<span font=\"Arial\">" + new string('*', 4500) + "</span>";
            using (var image = new MagickImage(fileName))
            {
            }
        }
    }
}
