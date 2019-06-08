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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageInfoTests
    {
        [TestClass]
        public class TheOperators
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var imageInfo = CreateMagickImageInfo(MagickColors.Red, 10, 5);

                Assert.IsFalse(imageInfo == null);
                Assert.IsTrue(imageInfo != null);
                Assert.IsFalse(imageInfo < null);
                Assert.IsFalse(imageInfo <= null);
                Assert.IsTrue(imageInfo > null);
                Assert.IsTrue(imageInfo >= null);
                Assert.IsFalse(null == imageInfo);
                Assert.IsTrue(null != imageInfo);
                Assert.IsTrue(null < imageInfo);
                Assert.IsTrue(null <= imageInfo);
                Assert.IsFalse(null > imageInfo);
                Assert.IsFalse(null >= imageInfo);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenInstanceIsSpecified()
            {
                var first = CreateMagickImageInfo(MagickColors.Red, 11, 5);
                var second = CreateMagickImageInfo(MagickColors.Red, 10, 5);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
                Assert.IsFalse(first < second);
                Assert.IsFalse(first <= second);
                Assert.IsTrue(first > second);
                Assert.IsTrue(first >= second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenInstanceHasSameSize()
            {
                var first = CreateMagickImageInfo(MagickColors.Red, 10, 5);
                var second = CreateMagickImageInfo(MagickColors.Red, 5, 10);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
                Assert.IsFalse(first < second);
                Assert.IsTrue(first <= second);
                Assert.IsFalse(first > second);
                Assert.IsTrue(first >= second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenInstanceAreEqual()
            {
                var first = CreateMagickImageInfo(MagickColors.Red, 10, 5);
                var second = CreateMagickImageInfo(MagickColors.Red, 10, 5);

                Assert.IsTrue(first == second);
                Assert.IsFalse(first != second);
                Assert.IsFalse(first < second);
                Assert.IsTrue(first <= second);
                Assert.IsFalse(first > second);
                Assert.IsTrue(first >= second);
            }

            private MagickImageInfo CreateMagickImageInfo(MagickColor color, int width, int height)
            {
                using (var memStream = new MemoryStream())
                {
                    using (IMagickImage image = new MagickImage(color, width, height))
                    {
                        image.Format = MagickFormat.Png;
                        image.Write(memStream);
                        memStream.Position = 0;

                        return new MagickImageInfo(memStream);
                    }
                }
            }
        }
    }
}
