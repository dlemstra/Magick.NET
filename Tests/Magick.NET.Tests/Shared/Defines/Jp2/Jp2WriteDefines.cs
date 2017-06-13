//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using ImageMagick;
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class Jp2WriteDefinesTests
    {
        [TestMethod]
        public void Test_Empty()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(new Jp2WriteDefines()
                {
                });

                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jp2, "number-resolutions"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jp2, "progression-order"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jp2, "quality"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jp2, "rate"));

                image.Settings.SetDefines(new Jp2WriteDefines()
                {
                    Quality = new float[] { },
                    Rate = new float[] { }
                });

                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jp2, "quality"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jp2, "rate"));
            }
        }

        [TestMethod]
        public void Test_NumberResolutions_ProgressionOrder()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(new Jp2WriteDefines()
                {
                    NumberResolutions = 4,
                    ProgressionOrder = Jp2ProgressionOrder.PCRL,
                    Quality = new float[] { 4, 2 },
                    Rate = new float[] { 2, 4 }
                });

                Assert.AreEqual("4", image.Settings.GetDefine(MagickFormat.Jp2, "number-resolutions"));
                Assert.AreEqual("PCRL", image.Settings.GetDefine(MagickFormat.Jp2, "progression-order"));
                Assert.AreEqual("4,2", image.Settings.GetDefine(MagickFormat.Jp2, "quality"));
                Assert.AreEqual("2,4", image.Settings.GetDefine(MagickFormat.Jp2, "rate"));
            }
        }
    }
}
