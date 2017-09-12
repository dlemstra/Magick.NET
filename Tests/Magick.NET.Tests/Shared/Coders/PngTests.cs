// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class PNGTests
    {
        [TestMethod]
        public void CorruptImage_TryhRead_ThrowsExceptionAndDoesNotChangeOriginalImage()
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
        public void PngWithLargeIDAT_ImageCanBeRead()
        {
            using (IMagickImage image = new MagickImage(Files.VicelandPNG))
            {
                Assert.AreEqual(200, image.Width);
                Assert.AreEqual(28, image.Height);
            }
        }

        [TestMethod]
        public void PNGWithValidModificationDateThatBecomes24Hours_NoWarningIsRaised()
        {
            using (IMagickImage image = new MagickImage("logo:"))
            {
                image.Warning += HandleWarning;
                image.SetAttribute("date:modify", "2017-09-10T20:35:00+03:30");

                image.ToByteArray(MagickFormat.Png);
            }
        }

        [TestMethod]
        public void PNGWithValidModificationDateThatBecomes60Minutes_NoWarningIsRaised()
        {
            using (IMagickImage image = new MagickImage("logo:"))
            {
                image.Warning += HandleWarning;
                image.SetAttribute("date:modify", "2017-09-10T15:30:00+03:30");

                image.ToByteArray(MagickFormat.Png);
            }
        }

        private void HandleWarning(object sender, WarningEventArgs e)
        {
            Assert.Fail("Warning was raised: " + e.Message);
        }
    }
}