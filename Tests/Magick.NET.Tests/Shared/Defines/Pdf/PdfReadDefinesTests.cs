//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class PdfReadDefinesTests
    {
        [TestMethod]
        public void Test_Empty()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(new PdfReadDefines()
                {
                });

                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Pdf, "fit-page"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Pdf, "use-cropbox"));
                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Pdf, "use-trimbox"));
            }
        }

        [TestMethod]
        public void Test_UseCropBox_UseTrimBox()
        {
            PdfReadDefines defines = new PdfReadDefines()
            {
                UseCropBox = true,
                UseTrimBox = false
            };

            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(defines);

                Assert.AreEqual("True", image.Settings.GetDefine(MagickFormat.Pdf, "use-cropbox"));
                Assert.AreEqual("False", image.Settings.GetDefine(MagickFormat.Pdf, "use-trimbox"));
            }
        }

        [TestMethod]
        public void Test_FitPage()
        {
            MagickReadSettings settings = new MagickReadSettings()
            {
                Defines = new PdfReadDefines()
                {
                    FitPage = new MagickGeometry(50, 40)
                }
            };

            using (IMagickImage image = new MagickImage())
            {
                image.Read(Files.Coders.CartoonNetworkStudiosLogoAI, settings);

                Assert.IsTrue(image.Width <= 50);
                Assert.IsTrue(image.Height <= 40);
            }
        }
    }
}
