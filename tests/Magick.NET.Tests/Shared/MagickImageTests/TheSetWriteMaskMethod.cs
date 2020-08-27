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
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheSetWriteMaskMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using (var image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                    {
                        image.SetWriteMask(null);
                    });
                }
            }

            [TestMethod]
            public void ShouldSetMaskForWholeImage()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (var imageMask = new MagickImage(MagickColors.White, 10, 15))
                    {
                        image.SetWriteMask(imageMask);

                        using (var mask = image.GetWriteMask())
                        {
                            Assert.IsNotNull(mask);
                            Assert.AreEqual(mask.Width, 640);
                            Assert.AreEqual(mask.Height, 480);
                            ColorAssert.AreEqual(MagickColors.White, mask, 9, 14);
                            ColorAssert.AreEqual(MagickColors.Black, mask, 10, 15);
                        }
                    }
                }
            }
        }
    }
}