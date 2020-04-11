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

#if WINDOWS_BUILD

using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class TheEpsCoder
    {
        [TestMethod]
        public void ShouldReadTwoImages()
        {
            using (IMagickImageCollection images = new MagickImageCollection(Files.Coders.SwedenHeartEPS))
            {
                Assert.AreEqual(2, images.Count);

                Assert.AreEqual(447, images[0].Width);
                Assert.AreEqual(420, images[0].Height);
                Assert.AreEqual(MagickFormat.Ept, images[0].Format);

                Assert.AreEqual(447, images[1].Width);
                Assert.AreEqual(420, images[1].Height);
                Assert.AreEqual(MagickFormat.Tiff, images[1].Format);
            }
        }

        [TestMethod]
        public void ShouldReadClipPathsInTiffPreview()
        {
            using (IMagickImageCollection images = new MagickImageCollection(Files.Coders.SwedenHeartEPS))
            {
                var profile = images[1].Get8BimProfile();

                Assert.AreEqual(1, profile.ClipPaths.Count());
            }
        }
    }
}

#endif