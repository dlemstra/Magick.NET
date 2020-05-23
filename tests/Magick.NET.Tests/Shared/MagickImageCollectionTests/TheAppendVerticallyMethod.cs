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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheAppendAppendVerticallyMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        images.AppendVertically();
                    });
                }
            }

            [TestMethod]
            public void ShouldAppendTheImagesVertically()
            {
                int width = 70;
                int height = 46;

                using (var images = new MagickImageCollection())
                {
                    images.Read(Files.RoseSparkleGIF);

                    Assert.AreEqual(width, images[0].Width);
                    Assert.AreEqual(height, images[0].Height);

                    using (var image = images.AppendVertically())
                    {
                        Assert.AreEqual(width, image.Width);
                        Assert.AreEqual(height * 3, image.Height);
                    }
                }
            }
        }
    }
}
