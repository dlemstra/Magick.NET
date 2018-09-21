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

#if !NETCORE

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class TheCaptionCoder
    {
        [TestMethod]
        public void ShouldAddCorrectLineBreaks1()
        {
            var caption = "caption:Text 2 Verylongtext";
            var readSettings = new MagickReadSettings()
            {
                FontPointsize = 23,
                FillColor = MagickColors.Blue,
                Width = 180,
                Height = 85,
            };

            using (IMagickImage image = new MagickImage(caption, readSettings))
            {
                ColorAssert.AreEqual(MagickColors.Blue, image, 55, 20);
            }
        }

        [TestMethod]
        public void ShouldAddCorrectLineBreaks2()
        {
            var caption = "caption:tex1_124x40_3a277be1b9da51b7_2d0d8f84dc3ccc36_8";
            var readSettings = new MagickReadSettings()
            {
                BackgroundColor = MagickColors.Transparent,
                FontPointsize = 39,
                FillColor = MagickColors.Red,
                TextUnderColor = MagickColors.Green,
                TextGravity = Gravity.Center,
                Width = 450,
            };

            using (IMagickImage image = new MagickImage(caption, readSettings))
            {
                ColorAssert.AreEqual(MagickColors.Green, image, 170, 67);
                ColorAssert.AreEqual(MagickColors.Red, image, 444, 26);
                ColorAssert.AreEqual(MagickColors.Red, image, 395, 55);
                ColorAssert.AreEqual(MagickColors.Red, image, 230, 116);
                ColorAssert.AreEqual(new MagickColor("#0000"), image, 170, 94);
            }
        }

        [TestMethod]
        public void ShouldAddCorrectLineBreaks3()
        {
            var caption = "caption:Dans votre vie, vous mangerez environ 30 000 kilos de nourriture, l’équivalent du poids de 6 éléphants.";
            var readSettings = new MagickReadSettings()
            {
                TextGravity = Gravity.Center,
                Width = 465,
                Height = 101,
            };

            using (IMagickImage image = new MagickImage(caption, readSettings))
            {
                ColorAssert.AreEqual(MagickColors.Black, image, 439, 23);
                ColorAssert.AreEqual(MagickColors.Black, image, 265, 42);
                ColorAssert.AreEqual(MagickColors.Black, image, 229, 68);
                ColorAssert.AreEqual(MagickColors.White, image, 151, 77);
            }
        }

        [TestMethod]
        public void ShouldAddCorrectLineBreaks4()
        {
            var caption = "caption:This does not wrap";
            var readSettings = new MagickReadSettings()
            {
                FontPointsize = 50,
                Width = 400,
            };

            using (IMagickImage image = new MagickImage(caption, readSettings))
            {
                ColorAssert.AreEqual(MagickColors.White, image, 321, 30);
                ColorAssert.AreEqual(MagickColors.Black, image, 86, 86);
            }
        }

        [TestMethod]
        public void ShouldAddCorrectLineBreaks5()
        {
            var caption = "caption:A";
            var readSettings = new MagickReadSettings()
            {
                BackgroundColor = MagickColors.Transparent,
                FontPointsize = 72,
                TextGravity = Gravity.West,
                FillColor = MagickColors.Black,
                Width = 40,
            };

            using (IMagickImage image = new MagickImage(caption, readSettings))
            {
                Assert.AreEqual(83, image.Height);

                ColorAssert.AreEqual(MagickColors.Black, image, 39, 58);
                ColorAssert.AreEqual(new MagickColor("#0000"), image, 39, 63);
            }
        }

        [TestMethod]
        public void ShouldAddCorrectLineBreaks6()
        {
            var caption = "caption:AAA";
            var readSettings = new MagickReadSettings()
            {
                BackgroundColor = MagickColors.Transparent,
                FontPointsize = 72,
                TextGravity = Gravity.West,
                FillColor = MagickColors.Black,
                Width = 40,
            };

            using (IMagickImage image = new MagickImage(caption, readSettings))
            {
                Assert.AreEqual(249, image.Height);

                ColorAssert.AreEqual(MagickColors.Black, image, 39, 58);
                ColorAssert.AreEqual(new MagickColor("#0000"), image, 39, 63);
                ColorAssert.AreEqual(MagickColors.Black, image, 39, 127);
                ColorAssert.AreEqual(new MagickColor("#0000"), image, 39, 145);
                ColorAssert.AreEqual(MagickColors.Black, image, 39, 210);
                ColorAssert.AreEqual(new MagickColor("#0000"), image, 39, 227);
            }
        }
    }
}

#endif
