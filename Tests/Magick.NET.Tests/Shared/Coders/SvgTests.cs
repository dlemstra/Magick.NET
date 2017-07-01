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
using System.IO;

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class SvgTests
    {
        [TestMethod]
        public void Test_Dimensions()
        {
            using (IMagickImage image = new MagickImage())
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.Width = 100;

                image.Read(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(100, image.Width);
                Assert.AreEqual(48, image.Height);

                settings.Width = null;
                settings.Height = 200;

                image.Read(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(416, image.Width);
                Assert.AreEqual(200, image.Height);

                settings.Width = 300;
                settings.Height = 300;

                image.Read(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(300, image.Width);
                Assert.AreEqual(144, image.Height);

                image.Ping(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(300, image.Width);
                Assert.AreEqual(144, image.Height);

                using (FileStream fs = File.OpenRead(Files.Logos.MagickNETSVG))
                {
                    fs.Seek(55, SeekOrigin.Begin);

                    ExceptionAssert.Throws<MagickMissingDelegateErrorException>(() =>
                    {
                        new MagickImageInfo(fs, settings);
                    });

                    fs.Seek(55, SeekOrigin.Begin);

                    settings.Format = MagickFormat.Svg;

                    MagickImageInfo info = new MagickImageInfo(fs, settings);
                    Assert.AreEqual(300, info.Width);
                    Assert.AreEqual(144, info.Height);
                }
            }
        }
    }
}