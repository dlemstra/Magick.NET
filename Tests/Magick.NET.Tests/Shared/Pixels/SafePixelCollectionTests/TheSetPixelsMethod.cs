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
        public class TheSetPixelsMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("values", () =>
                        {
                            pixels.SetPixels((byte[])null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayHasInvalidSize()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            pixels.SetPixels(new byte[] { 0, 0, 0, 0 });
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsTooLong()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            var values = new byte[(image.Width * image.Height * image.ChannelCount) + 1];
                            pixels.SetPixels(values);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenByteArrayHasMaxNumberOfValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = new byte[image.Width * image.Height * image.ChannelCount];
                        pixels.SetPixels(values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenDoubleArrayIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("values", () =>
                        {
                            pixels.SetPixels((double[])null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenDoubleArrayHasInvalidSize()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            pixels.SetPixels(new double[] { 0, 0, 0, 0 });
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenDoubleArrayIsTooLong()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            var values = new double[(image.Width * image.Height * image.ChannelCount) + 1];
                            pixels.SetPixels(values);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenDoubleArrayHasMaxNumberOfValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = new double[image.Width * image.Height * image.ChannelCount];
                        pixels.SetPixels(values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenIntArrayIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("values", () =>
                        {
                            pixels.SetPixels((int[])null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenIntArrayHasInvalidSize()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            pixels.SetPixels(new int[] { 0, 0, 0, 0 });
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenIntArrayIsTooLong()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            var values = new int[(image.Width * image.Height * image.ChannelCount) + 1];
                            pixels.SetPixels(values);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenIntArrayHasMaxNumberOfValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = new int[image.Width * image.Height * image.ChannelCount];
                        pixels.SetPixels(values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
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
                        ExceptionAssert.ThrowsArgumentNullException("values", () =>
                        {
                            pixels.SetPixels((QuantumType[])null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrayHasInvalidSize()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            pixels.SetPixels(new QuantumType[] { 0, 0, 0, 0 });
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrayIsTooLong()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("values", () =>
                        {
                            var values = new QuantumType[(image.Width * image.Height * image.ChannelCount) + 1];
                            pixels.SetPixels(values);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = new QuantumType[image.Width * image.Height * image.ChannelCount];
                        pixels.SetPixels(values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }
        }
    }
}
