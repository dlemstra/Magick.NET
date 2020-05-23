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
        public partial class TheMapMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        images.Map(null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmptyAndImageIsNotNull()
            {
                using (IMagickImageCollection colors = new MagickImageCollection())
                {
                    colors.Add(new MagickImage(MagickColors.Red, 1, 1));
                    colors.Add(new MagickImage(MagickColors.Green, 1, 1));

                    using (var remapImage = colors.AppendHorizontally())
                    {
                        using (IMagickImageCollection collection = new MagickImageCollection())
                        {
                            ExceptionAssert.Throws<InvalidOperationException>(() =>
                            {
                                collection.Map(remapImage);
                            });
                        }
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    images.Read(Files.RoseSparkleGIF);

                    ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                    {
                        images.Map(null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    images.Read(Files.RoseSparkleGIF);

                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        images.Map(images[0], null);
                    });
                }
            }
        }
    }
}