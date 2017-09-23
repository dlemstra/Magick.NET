// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [TestClass]
    public sealed class SafePixelCollectionTests
    {
        [TestMethod]
        public void Channels_ReturnsChannelCountOfImage()
        {
            using (IMagickImage image = new MagickImage(Files.CMYKJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    Assert.AreEqual(image.ChannelCount, pixels.Channels);

                    image.HasAlpha = true;

                    Assert.AreEqual(image.ChannelCount, pixels.Channels);
                }
            }
        }

        [TestMethod]
        public void Indexer_OutsideImage_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.RedPNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException<ArgumentOutOfRangeException>("x", () =>
                    {
                        Pixel pixel = pixels[image.Width + 1, 0];
                    });
                }
            }
        }

        [TestMethod]
        public void Indexer_InsideImage_ReturnsPixel()
        {
            using (IMagickImage image = new MagickImage(Files.RedPNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    Pixel pixel = pixels[300, 100];

                    ColorAssert.AreEqual(MagickColors.Red, pixel.ToColor());
                }
            }
        }

        [TestMethod]
        public void IEnumerable_FindPixel_ReturnsCorrectPixel()
        {
            using (IMagickImage image = new MagickImage(Files.ConnectedComponentsPNG, 10, 10))
            {
                Pixel pixel = image.GetPixels().First(p => p.ToColor() == MagickColors.Black);
                Assert.IsNotNull(pixel);

                Assert.AreEqual(350, pixel.X);
                Assert.AreEqual(196, pixel.Y);
                Assert.AreEqual(2, pixel.Channels);
            }
        }

        [TestMethod]
        public void IEnumerable_ReturnsCorrectCount()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    Assert.AreEqual(50, pixels.Count());
                }
            }
        }

        [TestMethod]
        public void GetArea_XTooLow_ThrowsException()
        {
            GetArea_ThrowsException("x", -1, 0, 1, 1);
        }

        [TestMethod]
        public void GetArea_XTooHigh_ThrowsException()
        {
            GetArea_ThrowsException("x", 6, 0, 1, 1);
        }

        [TestMethod]
        public void GetArea_YTooLow_ThrowsException()
        {
            GetArea_ThrowsException("y", 0, -1, 1, 1);
        }

        [TestMethod]
        public void GetArea_YTooHigh_ThrowsException()
        {
            GetArea_ThrowsException("y", 0, 11, 1, 1);
        }

        [TestMethod]
        public void GetArea_WidthTooLow_ThrowsException()
        {
            GetArea_ThrowsException("width", 0, 0, -1, 1);
        }

        [TestMethod]
        public void GetArea_WidthZero_ThrowsException()
        {
            GetArea_ThrowsException("width", 0, 0, 0, 1);
        }

        [TestMethod]
        public void GetArea_HeightTooLow_ThrowsException()
        {
            GetArea_ThrowsException("height", 0, 0, 1, -1);
        }

        [TestMethod]
        public void GetArea_HeightZero_ThrowsException()
        {
            GetArea_ThrowsException("height", 0, 0, 1, 0);
        }

        [TestMethod]
        public void GetArea_WidthAndOffsetTooHigh_ThrowsException()
        {
            GetArea_ThrowsException("width", 4, 0, 2, 1);
        }

        [TestMethod]
        public void GetArea_HeightAndOffsetTooHigh_ThrowsException()
        {
            GetArea_ThrowsException("height", 0, 9, 1, 2);
        }

        [TestMethod]
        public void GetArea_ValidArea_ReturnsArea()
        {
            using (IMagickImage image = new MagickImage(Files.CirclePNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var area = pixels.GetArea(28, 28, 2, 3);
                    int length = 2 * 3 * 4; // width * height * channelCount
                    MagickColor color = new MagickColor(area[0], area[1], area[2], area[3]);

                    Assert.AreEqual(length, area.Length);
                    ColorAssert.AreEqual(new MagickColor("#ffffff9f"), color);
                }
            }
        }

        [TestMethod]
        public void GetArea_GeometryIsNull_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.RedPNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentNullException("geometry", () =>
                    {
                        pixels.GetArea(null);
                    });
                }
            }
        }

        [TestMethod]
        public void GetArea_ValidGeometry_ReturnsArea()
        {
            using (IMagickImage image = new MagickImage(Files.RedPNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var area = pixels.GetArea(new MagickGeometry(0, 0, 6, 5));
                    int length = 6 * 5 * 4; // width * height * channelCount
                    MagickColor color = new MagickColor(area[0], area[1], area[2], area[3]);

                    Assert.AreEqual(length, area.Length);
                    ColorAssert.AreEqual(MagickColors.Red, color);
                }
            }
        }

        [TestMethod]
        public void GetEnumerator_ReturnsEnumerator()
        {
            using (IMagickImage image = new MagickImage(Files.CirclePNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    IEnumerator<Pixel> enumerator = pixels.GetEnumerator();
                    Assert.IsNotNull(enumerator);
                }
            }
        }

        [TestMethod]
        public void GetEnumerator_InterfaceImplementation_ReturnsEnumerator()
        {
            using (IMagickImage image = new MagickImage(Files.CirclePNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    IEnumerable enumerable = pixels;
                    Assert.IsNotNull(enumerable.GetEnumerator());
                }
            }
        }

        [TestMethod]
        public void GetIndex_InvalidChannel_ReturnsMinusOne()
        {
            using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    int index = pixels.GetIndex(PixelChannel.Black);
                    Assert.AreEqual(-1, index);
                }
            }
        }

        [TestMethod]
        public void GetIndex_ValidChannel_ReturnsIndex()
        {
            using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    int index = pixels.GetIndex(PixelChannel.Green);
                    Assert.AreEqual(1, index);
                }
            }
        }

        [TestMethod]
        public void GetPixel_OutsideImage_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException<ArgumentOutOfRangeException>("x", () =>
                    {
                        pixels.GetPixel(image.Width + 1, 0);
                    });
                }
            }
        }

        [TestMethod]
        public void GetPixel_InsideImage_ReturnsPixel()
        {
            using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    Pixel pixel = pixels.GetPixel(55, 68);
                    ColorAssert.AreEqual(new MagickColor("#a8dff8ff"), pixel.ToColor());
                }
            }
        }

        [TestMethod]
        public void GetValue_XTooLow_ThrowsException()
        {
            GetValue_ThrowsException("x", -1, 0);
        }

        [TestMethod]
        public void GetValue_XTooHigh_ThrowsException()
        {
            GetValue_ThrowsException("x", 6, 0);
        }

        [TestMethod]
        public void GetValue_YTooLow_ThrowsException()
        {
            GetValue_ThrowsException("y", 0, -1);
        }

        [TestMethod]
        public void GetValue_YTooHigh_ThrowsException()
        {
            GetValue_ThrowsException("y", 0, 11);
        }

        [TestMethod]
        public void GetValues_ReturnsAllPixels()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Purple, 4, 2))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = pixels.GetValues();
                    int length = 4 * 2 * 3;

                    Assert.AreEqual(length, values.Length);
                }
            }
        }

        [TestMethod]
        public void SetPixel_PixelIsNull_ThrowsException()
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
        public void SetPixel_PixelIsOutsideImage_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException<ArgumentOutOfRangeException>("y", () =>
                    {
                        pixels.SetPixel(new Pixel(0, image.Height + 1, 3));
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixel_PixelWithOneChannel_ChangesPixel()
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
        public void SetPixel_PixelHasTooManyChannels_ChangesPixel()
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
        public void SetPixel_CompletePixel_ChangesPixel()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    Pixel pixel = new Pixel(0, 0, new QuantumType[] { 0, 0, 0 });
                    pixels.SetPixel(pixel);

                    ColorAssert.AreEqual(MagickColors.Black, image, 0, 0);
                }
            }
        }

        [TestMethod]
        public void SetPixel_IEnumerablePixelIsNull_ThrowsException()
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
        public void SetPixel_MultipleIncompletePixels_ChangesPixels()
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
        public void SetPixel_MultipleCompletePixels_ChangesPixels()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    Pixel pixelA = new Pixel(0, 0, new QuantumType[] { Quantum.Max, 0, 0 });
                    Pixel pixelB = new Pixel(1, 0, new QuantumType[] { 0, Quantum.Max, 0 });
                    pixels.SetPixel(new Pixel[] { pixelA, pixelB });

                    ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Lime, image, 1, 0);
                }
            }
        }

        [TestMethod]
        public void SetPixelWithOffset_ArrayIsNull_ThrowsException()
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
        public void SetPixelWithOffset_ArrayIsEmpty_ThrowsException()
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
        public void SetPixelWithOffset_OutSideImage_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException<ArgumentOutOfRangeException>("x", () =>
                    {
                        pixels.SetPixel(image.Width + 1, 0, new QuantumType[] { 0 });
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixelWithOffset_OneChannel_ChangesPixel()
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
        public void SetPixelWithOffset_TooManyChannels_ChangesPixel()
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
        public void SetPixelWithOffset_CorrectNumberOfChannels_ChangesPixel()
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

        [TestMethod]
        public void SetPixelsWithByteArray_InvalidSize_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        pixels.SetPixels(new byte[] { 0, 0, 0, 0 });
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixelsWithByteArray_TooManyValues_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        var values = new byte[(image.Width * image.Height * image.ChannelCount) + 1];
                        pixels.SetPixels(values);
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixelsWithByteArray_MaxNumberOfValues_ChangesPixels()
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
        public void SetPixelsWithDoubleArray_InvalidSize_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        pixels.SetPixels(new double[] { 0, 0, 0, 0 });
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixelsWithDoubleArray_TooManyValues_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        var values = new double[(image.Width * image.Height * image.ChannelCount) + 1];
                        pixels.SetPixels(values);
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixelsWithDoubleArray_MaxNumberOfValues_ChangesPixels()
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
        public void SetPixelsWithIntArray_InvalidSize_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        pixels.SetPixels(new int[] { 0, 0, 0, 0 });
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixelsWithIntArray_TooManyValues_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        var values = new int[(image.Width * image.Height * image.ChannelCount) + 1];
                        pixels.SetPixels(values);
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixelsWithIntArray_MaxNumberOfValues_ChangesPixels()
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
        public void SetPixelsWithArray_InvalidSize_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        pixels.SetPixels(new QuantumType[] { 0, 0, 0, 0 });
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixelsWithArray_TooManyValues_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        var values = new QuantumType[(image.Width * image.Height * image.ChannelCount) + 1];
                        pixels.SetPixels(values);
                    });
                }
            }
        }

        [TestMethod]
        public void SetPixelsWithArray_MaxNumberOfValues_ChangesPixels()
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

        [TestMethod]
        public void SetAreaWithByteArray_InvalidSize_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        pixels.SetArea(10, 10, 1000, 1000, new byte[] { 0, 0, 0, 0 });
                    });
                }
            }
        }

        [TestMethod]
        public void SetAreaWithByteArray_TooManyValues_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        var values = new byte[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);
                    });
                }
            }
        }

        [TestMethod]
        public void SetAreaWithByteArray_MaxNumberOfValues_ChangesPixels()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = new byte[113 * 108 * image.ChannelCount];
                    pixels.SetArea(10, 10, 113, 108, values);

                    ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                }
            }
        }

        [TestMethod]
        public void SetAreaWithDoubleArray_InvalidSize_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        pixels.SetArea(10, 10, 1000, 1000, new double[] { 0, 0, 0, 0 });
                    });
                }
            }
        }

        [TestMethod]
        public void SetAreaWithDoubleArray_TooManyValues_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        var values = new double[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);
                    });
                }
            }
        }

        [TestMethod]
        public void SetAreaWithDoubleArray_MaxNumberOfValues_ChangesPixels()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = new double[113 * 108 * image.ChannelCount];
                    pixels.SetArea(10, 10, 113, 108, values);

                    ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                }
            }
        }

        [TestMethod]
        public void SetAreaWithIntArray_InvalidSize_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        pixels.SetArea(10, 10, 1000, 1000, new int[] { 0, 0, 0, 0 });
                    });
                }
            }
        }

        [TestMethod]
        public void SetAreaWithIntArray_TooManyValues_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        var values = new int[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);
                    });
                }
            }
        }

        [TestMethod]
        public void SetAreaWithIntArray_MaxNumberOfValues_ChangesPixels()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = new int[113 * 108 * image.ChannelCount];
                    pixels.SetArea(10, 10, 113, 108, values);

                    ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                }
            }
        }

        [TestMethod]
        public void SetAreaWithArray_InvalidSize_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        pixels.SetArea(10, 10, 1000, 1000, new QuantumType[] { 0, 0, 0, 0 });
                    });
                }
            }
        }

        [TestMethod]
        public void SetAreaWithArray_TooManyValues_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("values", () =>
                    {
                        var values = new QuantumType[(113 * 108 * image.ChannelCount) + image.ChannelCount];
                        pixels.SetArea(10, 10, 113, 108, values);
                    });
                }
            }
        }

        [TestMethod]
        public void SetAreaWithArray_MaxNumberOfValues_ChangesPixels()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = new QuantumType[113 * 108 * image.ChannelCount];
                    pixels.SetArea(10, 10, 113, 108, values);

                    ColorAssert.AreEqual(MagickColors.Black, image, image.Width - 1, image.Height - 1);
                }
            }
        }

        [TestMethod]
        public void ToArray_ReturnsAllPixels()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = pixels.ToArray();
                    int length = image.Width * image.Height * image.ChannelCount;

                    Assert.AreEqual(length, values.Length);
                }
            }
        }

        [TestMethod]
        public void ToByteArray_XTooLow_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException<ArgumentOutOfRangeException>("x", () =>
                    {
                        pixels.ToByteArray(-1, 0, 1, 1, "RGB");
                    });
                }
            }
        }

        [TestMethod]
        public void ToByteArray_AreaIsCorrect_ReturnsArray()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = pixels.ToByteArray(60, 60, 63, 58, "RGBA");
                    int length = 63 * 58 * 4;

                    Assert.AreEqual(length, values.Length);
                }
            }
        }

        [TestMethod]
        public void ToByteArray_GeometryIsNull_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentNullException("geometry", () =>
                    {
                        pixels.ToByteArray(null, "RGB");
                    });
                }
            }
        }

        [TestMethod]
        public void ToByteArray_GeometryIsValidMappingIsNull_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentNullException("mapping", () =>
                    {
                        pixels.ToByteArray(new MagickGeometry(1, 2, 3, 4), null);
                    });
                }
            }
        }

        [TestMethod]
        public void ToByteArray_GeometryIsValidMappingIsEmpty_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("mapping", () =>
                    {
                        pixels.ToByteArray(new MagickGeometry(1, 2, 3, 4), string.Empty);
                    });
                }
            }
        }

        [TestMethod]
        public void ToByteArray_GeometryIsCorrect_ReturnsArray()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = pixels.ToByteArray(new MagickGeometry(10, 10, 113, 108), "RG");
                    int length = 113 * 108 * 2;

                    Assert.AreEqual(length, values.Length);
                }
            }
        }

        [TestMethod]
        public void ToByteArray_MappingIsNull_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentNullException("mapping", () =>
                    {
                        pixels.ToByteArray(null);
                    });
                }
            }
        }

        [TestMethod]
        public void ToByteArray_MappingIsEmpty_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("mapping", () =>
                    {
                        pixels.ToByteArray(string.Empty);
                    });
                }
            }
        }

        [TestMethod]
        public void ToByteArray_InvalidMapping_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.Throws<MagickOptionErrorException>(() =>
                    {
                        pixels.ToByteArray("FOO");
                    });
                }
            }
        }

        [TestMethod]
        public void ToByteArray_TwoChannels_ReturnsArray()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = pixels.ToByteArray("RG");
                    int length = image.Width * image.Height * 2;

                    Assert.AreEqual(length, values.Length);
                }
            }
        }

        [TestMethod]
        public void ToShortArray_XTooLow_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException<ArgumentOutOfRangeException>("x", () =>
                    {
                        pixels.ToShortArray(-1, 0, 1, 1, "RGB");
                    });
                }
            }
        }

        [TestMethod]
        public void ToShortArray_AreaIsCorrect_ReturnsArray()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = pixels.ToShortArray(60, 60, 63, 58, "RGBA");
                    int length = 63 * 58 * 4;

                    Assert.AreEqual(length, values.Length);
                }
            }
        }

        [TestMethod]
        public void ToShortArray_GeometryIsNull_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentNullException("geometry", () =>
                    {
                        pixels.ToShortArray(null, "RGB");
                    });
                }
            }
        }

        [TestMethod]
        public void ToShortArray_GeometryIsValidMappingIsNull_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentNullException("mapping", () =>
                    {
                        pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), null);
                    });
                }
            }
        }

        [TestMethod]
        public void ToShortArray_GeometryIsValidMappingIsEmpty_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("mapping", () =>
                    {
                        pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), string.Empty);
                    });
                }
            }
        }

        [TestMethod]
        public void ToShortArray_GeometryIsCorrect_ReturnsArray()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = pixels.ToShortArray(new MagickGeometry(10, 10, 113, 108), "RG");
                    int length = 113 * 108 * 2;

                    Assert.AreEqual(length, values.Length);
                }
            }
        }

        [TestMethod]
        public void ToShortArray_MappingIsNull_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentNullException("mapping", () =>
                    {
                        pixels.ToShortArray(null);
                    });
                }
            }
        }

        [TestMethod]
        public void ToShortArray_MappingIsEmpty_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException("mapping", () =>
                    {
                        pixels.ToShortArray(string.Empty);
                    });
                }
            }
        }

        [TestMethod]
        public void ToShortArray_InvalidMapping_ThrowsException()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.Throws<MagickOptionErrorException>(() =>
                    {
                        pixels.ToShortArray("FOO");
                    });
                }
            }
        }

        [TestMethod]
        public void ToShortArray_TwoChannels_ReturnsArray()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    var values = pixels.ToShortArray("RG");
                    int length = image.Width * image.Height * 2;

                    Assert.AreEqual(length, values.Length);
                }
            }
        }

        private static void GetArea_ThrowsException(string paramName, int x, int y, int width, int height)
        {
            using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException<ArgumentOutOfRangeException>(paramName, () =>
                    {
                        pixels.GetArea(x, y, width, height);
                    });
                }
            }
        }

        private static void GetValue_ThrowsException(string paramName, int x, int y)
        {
            using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    ExceptionAssert.ThrowsArgumentException<ArgumentOutOfRangeException>(paramName, () =>
                    {
                        pixels.GetValue(x, y);
                    });
                }
            }
        }
    }
}
