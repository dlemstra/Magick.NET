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

namespace Magick.NET.Tests.Shared.Types
{
    public partial class MagickImageInfoTests
    {
        [TestClass]
        public class TheEqualsMethod
        {
            [TestMethod]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                var imageInfo = CreateIMagickImageInfo(MagickColors.Red, 10, 10);

                Assert.IsFalse(imageInfo.Equals(null));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                var imageInfo = CreateIMagickImageInfo(MagickColors.Red, 10, 10);

                Assert.IsTrue(imageInfo.Equals(imageInfo));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                var imageInfo = CreateIMagickImageInfo(MagickColors.Red, 10, 10);

                Assert.IsTrue(imageInfo.Equals((object)imageInfo));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = CreateIMagickImageInfo(MagickColors.Red, 10, 10);
                var second = CreateIMagickImageInfo(MagickColors.Red, 10, 10);

                Assert.IsTrue(first.Equals(second));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = CreateIMagickImageInfo(MagickColors.Red, 10, 10);
                var second = CreateIMagickImageInfo(MagickColors.Red, 10, 10);

                Assert.IsTrue(first.Equals((object)second));
            }

            [TestMethod]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = CreateIMagickImageInfo(MagickColors.Red, 10, 10);
                var second = CreateIMagickImageInfo(MagickColors.Red, 5, 10);

                Assert.IsFalse(first.Equals(second));
            }

            [TestMethod]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = CreateIMagickImageInfo(MagickColors.Red, 10, 10);
                var second = CreateIMagickImageInfo(MagickColors.Red, 5, 10);

                Assert.IsFalse(first.Equals((object)second));
            }

            private IMagickImageInfo CreateIMagickImageInfo(MagickColor color, int width, int height)
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
