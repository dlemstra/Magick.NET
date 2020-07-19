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
    public partial class TheSetDoubleAreaMethod
    {
        [TestClass]
        public class TheSetAreaMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("values", () =>
                        {
                            pixels.SetDoubleArea(10, 10, 1000, 1000, null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrayHasInvalidSize()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            pixels.SetDoubleArea(10, 10, 1000, 1000, new double[] { 0, 0, 0, 0 });
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrayHasTooManyValues()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            var values = new double[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                            pixels.SetDoubleArea(10, 10, 113, 108, values);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = new double[113 * 108 * image.ChannelCount];
                        pixels.SetDoubleArea(10, 10, 113, 108, values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrayIsSpecifiedAndGeometryIsNull()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("geometry", () =>
                        {
                            pixels.SetDoubleArea(null, new double[] { 0 });
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenGeometryAndArrayAreSpecified()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = new double[113 * 108 * image.ChannelCount];
                        pixels.SetDoubleArea(new MagickGeometry(10, 10, 113, 108), values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }
        }
    }
}