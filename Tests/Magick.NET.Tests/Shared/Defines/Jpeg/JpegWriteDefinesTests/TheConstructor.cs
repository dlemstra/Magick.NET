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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class JpegWriteDefinesTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldNotSetAnyDefine()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Settings.SetDefines(new JpegWriteDefines()
                    {
                    });

                    Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "arithmetic-coding"));
                    Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "dct-method"));
                    Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "extent"));
                    Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "optimize-coding"));
                    Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "quality"));
                    Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "q-table"));
                    Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "sampling-factor"));
                }
            }

            [TestMethod]
            public void ShouldNotSetAnyDefineForEmptyValues()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Settings.SetDefines(new JpegWriteDefines()
                    {
                        QuantizationTables = string.Empty,
                        SamplingFactors = new MagickGeometry[] { },
                    });

                    Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "q-table"));
                    Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jpeg, "sampling-factor"));
                }
            }
        }
    }
}
