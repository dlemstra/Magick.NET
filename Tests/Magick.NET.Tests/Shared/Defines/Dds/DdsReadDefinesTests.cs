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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class DdsReadDefinesTests
    {
        [TestMethod]
        public void WithEmptyDefines_DoesNotSetAnyDefines()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(new DdsReadDefines()
                {
                });

                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Dds, "skip-mipmaps"));
            }
        }

        [TestMethod]
        public void SkipMipmaps_SetToFalse_ReadsMipmaps()
        {
            MagickReadSettings settings = new MagickReadSettings()
            {
                Defines = new DdsReadDefines()
                {
                    SkipMipmaps = false,
                },
            };

            using (IMagickImageCollection images = new MagickImageCollection())
            {
                images.Read(Files.Coders.TestDds, settings);

                Assert.AreEqual(5, images.Count);
                Assert.AreEqual("False", images[0].Settings.GetDefine(MagickFormat.Dds, "skip-mipmaps"));
            }
        }
    }
}
