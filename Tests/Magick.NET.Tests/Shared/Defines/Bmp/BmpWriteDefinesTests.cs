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

using System.IO;
using ImageMagick;
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class BmpWriteDefinesTests
    {
        [TestMethod]
        public void Test_Empty()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(new BmpWriteDefines()
                {
                });

                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Bmp, "subtype"));
            }
        }

        [TestMethod]
        public void Test_SubType()
        {
            BmpWriteDefines defines = new BmpWriteDefines()
            {
                Subtype = BmpSubtype.RGB555,
            };

            using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
            {
                image.Format = MagickFormat.Bmp;
                image.ColorType = ColorType.TrueColor;

                long length;

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Write(memStream);
                    length = memStream.Length;
                }

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Write(memStream);
                    Assert.AreEqual(length, memStream.Length);
                }

                image.Settings.SetDefines(defines);

                using (MemoryStream memStream = new MemoryStream())
                {
                    image.Write(memStream);
                    Assert.IsTrue(memStream.Length < length);
                }
            }
        }
    }
}
