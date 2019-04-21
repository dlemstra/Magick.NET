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

using System;
using System.Collections.Generic;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class SafePixelCollectionTests
    {
        [TestClass]
        public class TheSetPixelMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenPixelIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("pixel", () =>
                        {
                            pixels.SetPixel((Pixel)null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPixelWidthIsOutsideImage()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentOutOfRangeException>("x", () =>
                        {
                            pixels.SetPixel(new Pixel(image.Width + 1, 0, 3));
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPixelHeightIsOutsideImage()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentOutOfRangeException>("y", () =>
                        {
                            pixels.SetPixel(new Pixel(0, image.Height + 1, 3));
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelWhenNotEnoughChannelsAreSupplied()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        Pixel pixel = new Pixel(0, 0, new QuantumType[] { 0 });
                        pixels.SetPixel(pixel);

                        ColorAssert.AreEqual(MagickColors.Cyan, image, 0, 0);
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelWhenTooManyChannelsAreSupplied()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        Pixel pixel = new Pixel(0, 0, new QuantumType[] { 0, 0, 0, 0 });
                        pixels.SetPixel(pixel);

                        ColorAssert.AreEqual(MagickColors.Black, image, 0, 0);
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelWhenCompletePixelIsSupplied()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        Pixel pixel = new Pixel(0, 0, new QuantumType[] { 0, Quantum.Max, 0 });
                        pixels.SetPixel(pixel);

                        ColorAssert.AreEqual(MagickColors.Lime, image, 0, 0);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenIEnumerablePixelIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("pixels", () =>
                        {
                            pixels.SetPixel((IEnumerable<Pixel>)null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenMultipleIncompletePixelsAreSupplied()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        Pixel pixelA = new Pixel(0, 0, new QuantumType[] { 0 });
                        Pixel pixelB = new Pixel(1, 0, new QuantumType[] { 0, 0 });
                        pixels.SetPixel(new Pixel[] { pixelA, pixelB });

                        ColorAssert.AreEqual(MagickColors.Cyan, image, 0, 0);
                        ColorAssert.AreEqual(MagickColors.Blue, image, 1, 0);
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenMultipleCompletePixelsAreSupplied()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        Pixel pixelA = new Pixel(0, 0, new QuantumType[] { Quantum.Max, 0, 0 });
                        Pixel pixelB = new Pixel(1, 0, new QuantumType[] { 0, 0, Quantum.Max });
                        pixels.SetPixel(new Pixel[] { pixelA, pixelB });

                        ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
                        ColorAssert.AreEqual(MagickColors.Blue, image, 1, 0);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("value", () =>
                        {
                            pixels.SetPixel(0, 0, null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentException("value", () =>
                        {
                            pixels.SetPixel(0, 0, new QuantumType[] { });
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenOffsetWidthIsOutsideImage()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentOutOfRangeException>("x", () =>
                        {
                            pixels.SetPixel(image.Width + 1, 0, new QuantumType[] { 0 });
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenOffsetHeightIsOutsideImage()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentOutOfRangeException>("y", () =>
                        {
                            pixels.SetPixel(0, image.Height + 1, new QuantumType[] { 0 });
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenOneChannelAndOffsetAreSupplied()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        pixels.SetPixel(0, 0, new QuantumType[] { 0 });

                        ColorAssert.AreEqual(MagickColors.Cyan, image, 0, 0);
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenTooManyChannelsAndOffsetAreSupplied()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        pixels.SetPixel(0, 0, new QuantumType[] { 0, 0, 0, 0 });

                        ColorAssert.AreEqual(MagickColors.Black, image, 0, 0);
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenCorrectNumberOfChannelsAndOffsetAreSupplied()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        pixels.SetPixel(0, 0, new QuantumType[] { 0, 0, 0 });

                        ColorAssert.AreEqual(MagickColors.Black, image, 0, 0);
                    }
                }
            }
        }
    }
}
