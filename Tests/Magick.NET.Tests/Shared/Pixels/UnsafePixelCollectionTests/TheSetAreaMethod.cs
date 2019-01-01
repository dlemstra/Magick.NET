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
    public partial class UnsafePixelCollectionTests
    {
        [TestClass]
        public class TheSetAreaMethod
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenByteArrayIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(10, 10, 1000, 1000, (byte[])null);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenByteArrayHasInvalidSize()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(10, 10, 1000, 1000, new byte[] { 0, 0, 0, 0 });
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenByteArrayHasTooManyValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new byte[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenByteArrayHasMaxNumberOfValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new byte[113 * 108 * image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenByteArrayIsSpecifiedAndGeometryIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(null, new byte[] { 0 });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenGeometryAndByteArrayAreSpecified()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new byte[113 * 108 * image.ChannelCount];
                        pixels.SetArea(new MagickGeometry(10, 10, 113, 108), values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenDoubleArrayIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(10, 10, 1000, 1000, (double[])null);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenDoubleArrayHasInvalidSize()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(10, 10, 1000, 1000, new double[] { 0, 0, 0, 0 });
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenDoubleArrayHasTooManyValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new double[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenDoubleArrayHasMaxNumberOfValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new double[113 * 108 * image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenDoubleArrayIsSpecifiedAndGeometryIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(null, new double[] { 0 });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenGeometryAndDoubleArrayAreSpecified()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new double[113 * 108 * image.ChannelCount];
                        pixels.SetArea(new MagickGeometry(10, 10, 113, 108), values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenIntArrayIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(10, 10, 1000, 1000, (int[])null);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenIntArrayHasInvalidSize()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(10, 10, 1000, 1000, new int[] { 0, 0, 0, 0 });
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenIntArrayHasTooManyValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new int[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenIntArrayHasMaxNumberOfValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new int[113 * 108 * image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenIntArrayIsSpecifiedAndGeometryIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(null, new int[] { 0 });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenGeometryAndIntArrayAreSpecified()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new int[113 * 108 * image.ChannelCount];
                        pixels.SetArea(new MagickGeometry(10, 10, 113, 108), values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenArrayIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(10, 10, 1000, 1000, (QuantumType[])null);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenArrayHasInvalidSize()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(10, 10, 1000, 1000, new QuantumType[] { 0, 0, 0, 0 });
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenArrayHasTooManyValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new QuantumType[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new QuantumType[113 * 108 * image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenArrayIsSpecifiedAndGeometryIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        pixels.SetArea(null, new QuantumType[] { 0 });
                    }
                }
            }

            [TestMethod]
            public void ShouldChangePixelsWhenGeometryAndArrayAreSpecified()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = new QuantumType[113 * 108 * image.ChannelCount];
                        pixels.SetArea(new MagickGeometry(10, 10, 113, 108), values);

                        ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                    }
                }
            }
        }
    }
}
