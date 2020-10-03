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
using ImageMagick.Formats.Pdf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class PdfReadDefinesTests
    {
        [TestClass]
        public class TheUseCropBoxProperty
        {
            [TestMethod]
            public void ShouldSetTheDefineWhenValueIsTrue()
            {
                using (var image = new MagickImage(MagickColors.Magenta, 1, 1))
                {
                    image.Settings.SetDefines(new PdfReadDefines()
                    {
                        UseCropBox = true,
                    });

                    Assert.AreEqual("true", image.Settings.GetDefine(MagickFormat.Pdf, "use-cropbox"));
                }
            }

            [TestMethod]
            public void ShouldSetTheDefineWhenValueIsFalse()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfReadDefines()
                    {
                        UseCropBox = false,
                    });

                    Assert.AreEqual("false", image.Settings.GetDefine(MagickFormat.Pdf, "use-cropbox"));
                }
            }

            [TestMethod]
            public void ShouldNotSetTheDefineWhenValueIsNotSet()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfReadDefines()
                    {
                        UseCropBox = null,
                    });

                    Assert.IsNull(image.Settings.GetDefine(MagickFormat.Pdf, "use-cropbox"));
                }
            }
        }
    }
}
