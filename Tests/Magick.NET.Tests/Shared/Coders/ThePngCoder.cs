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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Coders
{
    [TestClass]
    public class ThePngCoder
    {
        [TestMethod]
        public void ShouldThrowExceptionAndNotChangeTheOriginalImageWhenTheImageIsCorrupt()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Purple, 4, 2))
            {
                ExceptionAssert.Throws<MagickCoderErrorException>(() =>
                {
                    image.Read(Files.CorruptPNG);
                });

                Assert.AreEqual(4, image.Width);
                Assert.AreEqual(2, image.Height);
            }
        }

        [TestMethod]
        public void ShouldBeAbleToReadPngWithLargeIDAT()
        {
            using (IMagickImage image = new MagickImage(Files.VicelandPNG))
            {
                Assert.AreEqual(200, image.Width);
                Assert.AreEqual(28, image.Height);
            }
        }

        [TestMethod]
        public void ShouldNotRaiseWarningForValidModificationDateThatBecomes24Hours()
        {
            using (IMagickImage image = new MagickImage("logo:"))
            {
                image.Warning += HandleWarning;
                image.SetAttribute("date:modify", "2017-09-10T20:35:00+03:30");

                image.ToByteArray(MagickFormat.Png);
            }
        }

        [TestMethod]
        public void ShouldNotRaiseWarningForValidModificationDateThatBecomes60Minutes()
        {
            using (IMagickImage image = new MagickImage("logo:"))
            {
                image.Warning += HandleWarning;
                image.SetAttribute("date:modify", "2017-09-10T15:30:00+03:30");

                image.ToByteArray(MagickFormat.Png);
            }
        }

        [TestMethod]
        public void ShouldReadTheExifChunk()
        {
            using (IMagickImage input = new MagickImage(MagickColors.YellowGreen, 1, 1))
            {
                ExifProfile exifProfile = new ExifProfile();
                exifProfile.SetValue(ExifTag.ImageUniqueID, "Have a nice day");

                input.AddProfile(exifProfile);

                using (var memoryStream = new MemoryStream())
                {
                    input.Write(memoryStream, MagickFormat.Png);

                    memoryStream.Position = 0;

                    using (IMagickImage output = new MagickImage(memoryStream))
                    {
                        exifProfile = output.GetExifProfile();

                        Assert.IsNotNull(exifProfile);
                    }
                }
            }
        }

        [TestMethod]
        public void ShouldSetTheAnimationProperties()
        {
            using (IMagickImageCollection images = new MagickImageCollection(Files.Coders.TestMng))
            {
                Assert.AreEqual(8, images.Count);

                foreach (var image in images)
                {
                    Assert.AreEqual(20, image.AnimationDelay);
                    Assert.AreEqual(100, image.AnimationTicksPerSecond);
                }
            }
        }

        private void HandleWarning(object sender, WarningEventArgs e)
        {
            Assert.Fail("Warning was raised: " + e.Message);
        }
    }
}