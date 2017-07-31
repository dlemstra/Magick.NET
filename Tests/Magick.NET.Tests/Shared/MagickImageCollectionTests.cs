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
using System.Collections.Generic;
using System.IO;
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
    public partial class MagickImageCollectionTests
    {
        [TestMethod]
        public void Test_AddRange()
        {
            using (IMagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                Assert.AreEqual(3, collection.Count);

                collection.AddRange(Files.RoseSparkleGIF);
                Assert.AreEqual(6, collection.Count);

                collection.AddRange(collection);
                Assert.AreEqual(12, collection.Count);

                List<IMagickImage> images = new List<IMagickImage>();
                images.Add(new MagickImage("xc:red", 100, 100));
                collection.AddRange(images);
                Assert.AreEqual(13, collection.Count);
            }
        }

        [TestMethod]
        public void Test_Append()
        {
            int width = 70;
            int height = 46;

            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.AppendHorizontally();
                });

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.AppendVertically();
                });

                collection.Read(Files.RoseSparkleGIF);

                Assert.AreEqual(width, collection[0].Width);
                Assert.AreEqual(height, collection[0].Height);

                using (IMagickImage image = collection.AppendHorizontally())
                {
                    Assert.AreEqual(width * 3, image.Width);
                    Assert.AreEqual(height, image.Height);
                }

                using (IMagickImage image = collection.AppendVertically())
                {
                    Assert.AreEqual(width, image.Width);
                    Assert.AreEqual(height * 3, image.Height);
                }
            }
        }

        [TestMethod]
        public void Test_Clone()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                collection.Add(Files.Builtin.Logo);
                collection.Add(Files.Builtin.Rose);
                collection.Add(Files.Builtin.Wizard);

                using (IMagickImageCollection clones = collection.Clone())
                {
                    Assert.AreEqual(collection[0], clones[0]);
                    Assert.AreEqual(collection[1], clones[1]);
                    Assert.AreEqual(collection[2], clones[2]);
                }
            }
        }

        [TestMethod]
        public void Test_Coalesce()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Coalesce();
                });

                collection.Read(Files.RoseSparkleGIF);

                using (PixelCollection pixels = collection[1].GetPixels())
                {
                    MagickColor color = pixels.GetPixel(53, 3).ToColor();
                    Assert.AreEqual(0, color.A);
                }

                collection.Coalesce();

                using (PixelCollection pixels = collection[1].GetPixels())
                {
                    MagickColor color = pixels.GetPixel(53, 3).ToColor();
                    Assert.AreEqual(Quantum.Max, color.A);
                }
            }
        }

        [TestMethod]
        public void Test_Combine_sRGB()
        {
            using (IMagickImage rose = new MagickImage(Files.Builtin.Rose))
            {
                using (IMagickImageCollection collection = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        collection.Combine();
                    });

                    collection.AddRange(rose.Separate(Channels.RGB));

                    Assert.AreEqual(3, collection.Count);

                    IMagickImage image = collection.Merge();
                    Assert.AreNotEqual(rose.TotalColors, image.TotalColors);
                    image.Dispose();

                    image = collection.Combine();
                    Assert.AreEqual(rose.TotalColors, image.TotalColors);
                }
            }
        }

        [TestMethod]
        public void Test_Combine_CMYK()
        {
            using (IMagickImage cmyk = new MagickImage(Files.CMYKJPG))
            {
                using (IMagickImageCollection collection = new MagickImageCollection())
                {
                    collection.AddRange(cmyk.Separate(Channels.CMYK));

                    Assert.AreEqual(4, collection.Count);

                    IMagickImage image = collection.Combine(ColorSpace.CMYK);
                    Assert.AreEqual(0.0, cmyk.Compare(image, ErrorMetric.RootMeanSquared));
                }
            }
        }

        [TestMethod]
        public void Test_Constructor()
        {
            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                new MagickImageCollection(new byte[0]);
            });

            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                new MagickImageCollection((byte[])null);
            });

            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                new MagickImageCollection((Stream)null);
            });

            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                new MagickImageCollection((string)null);
            });

            ExceptionAssert.Throws<MagickBlobErrorException>("error/blob.c/OpenBlob", () =>
            {
                new MagickImageCollection(Files.Missing);
            });
        }

        [TestMethod]
        public void Test_CopyTo()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                collection.Add(new MagickImage(Files.SnakewarePNG));
                collection.Add(new MagickImage(Files.RoseSparkleGIF));

                MagickImage[] images = new MagickImage[collection.Count];
                collection.CopyTo(images, 0);

                Assert.AreEqual(collection[0], images[0]);
                Assert.AreNotEqual(collection[0], images[1]);

                collection.CopyTo(images, 1);
                Assert.AreEqual(collection[0], images[0]);
                Assert.AreEqual(collection[0], images[1]);

                images = new MagickImage[collection.Count + 1];
                collection.CopyTo(images, 0);

                images = new MagickImage[1];
                collection.CopyTo(images, 0);

                ExceptionAssert.Throws<ArgumentNullException>(() =>
                {
                    collection.CopyTo(null, -1);
                });

                ExceptionAssert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    collection.CopyTo(images, -1);
                });
            }
        }

        [TestMethod]
        public void Test_Deconstruct()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Deconstruct();
                });

                collection.Add(new MagickImage(MagickColors.Red, 20, 20));

                using (IMagickImageCollection frames = new MagickImageCollection())
                {
                    frames.Add(new MagickImage(MagickColors.Red, 10, 20));
                    frames.Add(new MagickImage(MagickColors.Purple, 10, 20));

                    collection.Add(frames.AppendHorizontally());
                }

                Assert.AreEqual(20, collection[1].Width);
                Assert.AreEqual(20, collection[1].Height);
                Assert.AreEqual(new MagickGeometry(0, 0, 10, 20), collection[1].Page);
                ColorAssert.AreEqual(MagickColors.Red, collection[1], 3, 3);

                collection.Deconstruct();

                Assert.AreEqual(10, collection[1].Width);
                Assert.AreEqual(20, collection[1].Height);
                Assert.AreEqual(new MagickGeometry(10, 0, 10, 20), collection[1].Page);
                ColorAssert.AreEqual(MagickColors.Purple, collection[1], 3, 3);
            }
        }

        [TestMethod]
        public void Test_Dispose()
        {
            IMagickImage image = new MagickImage(MagickColors.Red, 10, 10);

            IMagickImageCollection collection = new MagickImageCollection();
            collection.Add(image);
            collection.Dispose();

            Assert.AreEqual(0, collection.Count);
            ExceptionAssert.Throws<ObjectDisposedException>(() =>
            {
                image.Flip();
            });
        }

        [TestMethod]
        public void Test_Evaluate()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Evaluate(EvaluateOperator.Exponential);
                });

                collection.Add(new MagickImage(MagickColors.Yellow, 40, 10));

                using (IMagickImageCollection frames = new MagickImageCollection())
                {
                    frames.Add(new MagickImage(MagickColors.Green, 10, 10));
                    frames.Add(new MagickImage(MagickColors.White, 10, 10));
                    frames.Add(new MagickImage(MagickColors.Black, 10, 10));
                    frames.Add(new MagickImage(MagickColors.Yellow, 10, 10));

                    collection.Add(frames.AppendHorizontally());
                }

                using (IMagickImage image = collection.Evaluate(EvaluateOperator.Min))
                {
                    ColorAssert.AreEqual(MagickColors.Green, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Yellow, image, 10, 0);
                    ColorAssert.AreEqual(MagickColors.Black, image, 20, 0);
                    ColorAssert.AreEqual(MagickColors.Yellow, image, 30, 0);
                }
            }
        }

        [TestMethod]
        public void Test_Flatten()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Flatten();
                });

                collection.Add(new MagickImage(MagickColors.Brown, 10, 10));
                IMagickImage center = new MagickImage(MagickColors.Fuchsia, 4, 4);
                center.Page = new MagickGeometry(3, 3, 4, 4);
                collection.Add(center);

                using (IMagickImage image = collection.Flatten())
                {
                    ColorAssert.AreEqual(MagickColors.Brown, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Fuchsia, image, 5, 5);
                }
            }
        }

        [TestMethod]
        public void Test_Index()
        {
            using (IMagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    collection[i].Resize(35, 23);
                    Assert.AreEqual(35, collection[i].Width);

                    collection[i] = collection[i];
                    Assert.AreEqual(35, collection[i].Width);

                    collection[i] = null;
                }
            }
        }

        [TestMethod]
        public void Test_Map()
        {
            using (IMagickImageCollection colors = new MagickImageCollection())
            {
                colors.Add(new MagickImage(MagickColors.Red, 1, 1));
                colors.Add(new MagickImage(MagickColors.Green, 1, 1));

                using (IMagickImage remapImage = colors.AppendHorizontally())
                {
                    using (IMagickImageCollection collection = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<InvalidOperationException>(() =>
                        {
                            collection.Map(null);
                        });

                        ExceptionAssert.Throws<InvalidOperationException>(() =>
                        {
                            collection.Map(remapImage);
                        });

                        collection.Read(Files.RoseSparkleGIF);

                        ExceptionAssert.Throws<ArgumentNullException>(() =>
                        {
                            collection.Map(null);
                        });

                        QuantizeSettings settings = new QuantizeSettings();
                        settings.DitherMethod = DitherMethod.FloydSteinberg;

                        collection.Map(remapImage, settings);

                        ColorAssert.AreEqual(MagickColors.Red, collection[0], 60, 17);
                        ColorAssert.AreEqual(MagickColors.Green, collection[0], 37, 24);

                        ColorAssert.AreEqual(MagickColors.Red, collection[1], 58, 30);
                        ColorAssert.AreEqual(MagickColors.Green, collection[1], 36, 26);

                        ColorAssert.AreEqual(MagickColors.Red, collection[2], 60, 40);
                        ColorAssert.AreEqual(MagickColors.Green, collection[2], 17, 21);
                    }
                }
            }
        }

        [TestMethod]
        public void Test_Merge()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Merge();
                });

                collection.Read(Files.RoseSparkleGIF);

                using (IMagickImage first = collection.Merge())
                {
                    Assert.AreEqual(collection[0].Width, first.Width);
                    Assert.AreEqual(collection[0].Height, first.Height);
                }
            }
        }

        [TestMethod]
        public void Test_Montage()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                MontageSettings settings = new MontageSettings();
                settings.Geometry = new MagickGeometry(string.Format("{0}x{1}", 200, 200));
                settings.TileGeometry = new MagickGeometry(string.Format("{0}x", 2));

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Montage(settings);
                });

                for (int i = 0; i < 9; i++)
                    collection.Add(Files.Builtin.Logo);

                using (IMagickImage montageResult = collection.Montage(settings))
                {
                    Assert.IsNotNull(montageResult);
                    Assert.AreEqual(400, montageResult.Width);
                    Assert.AreEqual(1000, montageResult.Height);
                }
            }
        }

        [TestMethod]
        public void Test_Morph()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Morph(10);
                });

                collection.Add(Files.Builtin.Logo);

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Morph(10);
                });

                collection.AddRange(Files.Builtin.Wizard);

                collection.Morph(4);
                Assert.AreEqual(6, collection.Count);
            }
        }

        [TestMethod]
        public void Test_Mosaic()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Mosaic();
                });

                collection.Add(Files.SnakewarePNG);
                collection.Add(Files.ImageMagickJPG);

                using (IMagickImage mosaic = collection.Mosaic())
                {
                    Assert.AreEqual(286, mosaic.Width);
                    Assert.AreEqual(118, mosaic.Height);
                }
            }
        }

        [TestMethod]
        public void Test_Optimize()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Optimize();
                });

                collection.Add(new MagickImage(MagickColors.Red, 11, 11));

                IMagickImage image = new MagickImage(MagickColors.Red, 11, 11);
                using (var pixels = image.GetPixels())
                {
                    pixels.Set(5, 5, new QuantumType[] { 0, Quantum.Max, 0 });
                }

                collection.Add(image);
                collection.Optimize();

                Assert.AreEqual(1, collection[1].Width);
                Assert.AreEqual(1, collection[1].Height);
                Assert.AreEqual(5, collection[1].Page.X);
                Assert.AreEqual(5, collection[1].Page.Y);
                ColorAssert.AreEqual(MagickColors.Lime, collection[1], 0, 0);
            }
        }

        [TestMethod]
        public void Test_OptimizePlus()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.OptimizePlus();
                });

                collection.Add(new MagickImage(MagickColors.Red, 11, 11));
                /* the second image will not be removed if it is a duplicate so we
                   need to add an extra one. */
                collection.Add(new MagickImage(MagickColors.Red, 11, 11));
                collection.Add(new MagickImage(MagickColors.Red, 11, 11));

                IMagickImage image = new MagickImage(MagickColors.Red, 11, 11);
                using (var pixels = image.GetPixels())
                {
                    pixels.Set(5, 5, new QuantumType[] { 0, Quantum.Max, 0 });
                }

                collection.Add(image);
                collection.OptimizePlus();

                Assert.AreEqual(3, collection.Count);

                Assert.AreEqual(1, collection[1].Width);
                Assert.AreEqual(1, collection[1].Height);
                Assert.AreEqual(-1, collection[1].Page.X);
                Assert.AreEqual(-1, collection[1].Page.Y);
                ColorAssert.AreEqual(MagickColors.Red, collection[1], 0, 0);

                Assert.AreEqual(1, collection[2].Width);
                Assert.AreEqual(1, collection[2].Height);
                Assert.AreEqual(5, collection[2].Page.X);
                Assert.AreEqual(5, collection[2].Page.Y);
                ColorAssert.AreEqual(MagickColors.Lime, collection[2], 0, 0);
            }
        }

        [TestMethod]
        public void Test_OptimizeTransparency()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.OptimizeTransparency();
                });

                collection.Add(new MagickImage(MagickColors.Red, 11, 11));

                IMagickImage image = new MagickImage(MagickColors.Red, 11, 11);
                using (var pixels = image.GetPixels())
                {
                    pixels.Set(5, 5, new QuantumType[] { 0, Quantum.Max, 0 });
                }

                collection.Add(image);
                collection.OptimizeTransparency();

                Assert.AreEqual(11, collection[1].Width);
                Assert.AreEqual(11, collection[1].Height);
                Assert.AreEqual(0, collection[1].Page.X);
                Assert.AreEqual(0, collection[1].Page.Y);
                ColorAssert.AreEqual(MagickColors.Lime, collection[1], 5, 5);
                ColorAssert.AreEqual(new MagickColor("#f000"), collection[1], 4, 4);
            }
        }

        [TestMethod]
        public void Test_Quantize()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.Quantize();
                });

                collection.Add(Files.FujiFilmFinePixS1ProJPG);

                ExceptionAssert.Throws<ArgumentNullException>(() =>
                {
                    collection.Quantize(null);
                });

                QuantizeSettings settings = new QuantizeSettings();
                settings.Colors = 3;

                MagickErrorInfo errorInfo = collection.Quantize(settings);
                Assert.IsNull(errorInfo);

#if Q8
                ColorAssert.AreEqual(new MagickColor("#2b414f"), collection[0], 66, 115);
                ColorAssert.AreEqual(new MagickColor("#7b929f"), collection[0], 179, 123);
                ColorAssert.AreEqual(new MagickColor("#44739f"), collection[0], 188, 135);
#elif Q16 || Q16HDRI
                ColorAssert.AreEqual(new MagickColor("#447073169f39"), collection[0], 66, 115);
                ColorAssert.AreEqual(new MagickColor("#7b4292c29f25"), collection[0], 179, 123);
                ColorAssert.AreEqual(new MagickColor("#2aef41654efc"), collection[0], 188, 135);
#else
#error Not implemented!
#endif
            }
        }

        [TestMethod]
        public void Test_Smush()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.SmushHorizontal(5);
                });

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.SmushVertical(6);
                });

                collection.AddRange(Files.RoseSparkleGIF);

                using (IMagickImage image = collection.SmushHorizontal(20))
                {
                    Assert.AreEqual((70 * 3) + (20 * 2), image.Width);
                    Assert.AreEqual(46, image.Height);
                }

                using (IMagickImage image = collection.SmushVertical(40))
                {
                    Assert.AreEqual(70, image.Width);
                    Assert.AreEqual((46 * 3) + (40 * 2), image.Height);
                }
            }
        }

        [TestMethod]
        public void Test_Ping()
        {
            IMagickImageCollection collection = new MagickImageCollection();

            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                collection.Ping(new byte[0]);
            });

            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                collection.Ping((byte[])null);
            });

            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                collection.Ping((Stream)null);
            });

            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                collection.Ping((string)null);
            });

            ExceptionAssert.Throws<MagickBlobErrorException>("error/blob.c/OpenBlob", () =>
            {
                collection.Ping(Files.Missing);
            });

            collection.Ping(Files.FujiFilmFinePixS1ProJPG);
            Test_Ping(collection);
            Assert.AreEqual(600, collection[0].Width);
            Assert.AreEqual(400, collection[0].Height);

            collection.Ping(new FileInfo(Files.FujiFilmFinePixS1ProJPG));
            Test_Ping(collection);
            Assert.AreEqual(600, collection[0].Width);
            Assert.AreEqual(400, collection[0].Height);

            collection.Ping(File.ReadAllBytes(Files.FujiFilmFinePixS1ProJPG));
            Test_Ping(collection);
            Assert.AreEqual(600, collection[0].Width);
            Assert.AreEqual(400, collection[0].Height);

            collection.Read(Files.SnakewarePNG);
            Assert.AreEqual(286, collection[0].Width);
            Assert.AreEqual(67, collection[0].Height);
            using (PixelCollection pixels = collection[0].GetPixels())
            {
                Assert.AreEqual(38324, pixels.ToArray().Length);
            }

            collection.Dispose();
        }

        [TestMethod]
        public void Test_Read()
        {
            IMagickImageCollection collection = new MagickImageCollection();

            ExceptionAssert.Throws<ArgumentException>(() =>
            {
                collection.Read(new byte[0]);
            });

            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                collection.Read((byte[])null);
            });

            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                collection.Read((Stream)null);
            });

            ExceptionAssert.Throws<ArgumentNullException>(() =>
            {
                collection.Read((string)null);
            });

            ExceptionAssert.Throws<MagickBlobErrorException>("error/blob.c/OpenBlob", () =>
            {
                collection.Read(Files.Missing);
            });

            collection.Read(File.ReadAllBytes(Files.RoseSparkleGIF));
            Assert.AreEqual(3, collection.Count);

            using (FileStream fs = File.OpenRead(Files.RoseSparkleGIF))
            {
                collection.Read(fs);
                Assert.AreEqual(3, collection.Count);
            }

            collection.Read(Files.RoseSparkleGIF);
            Test_Read(collection);

            collection.Read(new FileInfo(Files.RoseSparkleGIF));

            collection.Dispose();
        }

        [TestMethod]
        public void Test_ReadSettings()
        {
            MagickReadSettings settings = new MagickReadSettings();
            settings.FontFamily = "Courier New";
            settings.FillColor = MagickColors.Gold;
            settings.FontPointsize = 80;
            settings.Format = MagickFormat.Text;
            settings.TextGravity = Gravity.Center;

            using (IMagickImageCollection images = new MagickImageCollection(Files.ImageMagickTXT, settings))
            {
                Assert.AreEqual(2, images.Count);
                ColorAssert.AreEqual(MagickColors.Gold, images[0], 348, 648);
            }

            using (IMagickImageCollection images = new MagickImageCollection())
            {
                images.Ping(Files.ImageMagickTXT, settings);

                Assert.AreEqual(2, images.Count);

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    ColorAssert.AreEqual(MagickColors.Gold, images[0], 348, 648);
                });
            }
        }

        [TestMethod]
        public void Test_Remove()
        {
            using (IMagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                IMagickImage first = collection[0];
                collection.Remove(first);

                Assert.AreEqual(2, collection.Count);
                Assert.AreEqual(-1, collection.IndexOf(first));

                first = collection[0];
                collection.RemoveAt(0);

                Assert.AreEqual(1, collection.Count);
                Assert.AreEqual(-1, collection.IndexOf(first));
            }
        }

        [TestMethod]
        public void Test_RePage()
        {
            using (IMagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                collection[0].Page = new MagickGeometry("0x0+10+20");

                Assert.AreEqual(10, collection[0].Page.X);
                Assert.AreEqual(20, collection[0].Page.Y);

                collection[0].Settings.Page = new MagickGeometry("0x0+10+20");

                Assert.AreEqual(10, collection[0].Settings.Page.X);
                Assert.AreEqual(20, collection[0].Settings.Page.Y);

                collection.RePage();

                Assert.AreEqual(0, collection[0].Page.X);
                Assert.AreEqual(0, collection[0].Page.Y);

                Assert.AreEqual(10, collection[0].Settings.Page.X);
                Assert.AreEqual(20, collection[0].Settings.Page.Y);
            }
        }

        [TestMethod]
        public void Test_Reverse()
        {
            using (IMagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                IMagickImage first = collection.First();
                collection.Reverse();

                IMagickImage last = collection.Last();
                Assert.IsTrue(last == first);
            }
        }

        [TestMethod]
        public void Test_ToBase64()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                Assert.AreEqual(string.Empty, collection.ToBase64());

                collection.Read(Files.Builtin.Logo);
                Assert.AreEqual(1228800, collection.ToBase64(MagickFormat.Rgb).Length);
            }
        }

        [TestMethod]
        public void Test_TrimBounds()
        {
            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    collection.TrimBounds();
                });

                collection.Add(Files.Builtin.Logo);
                collection.Add(Files.Builtin.Wizard);
                collection.TrimBounds();

                Assert.AreEqual(640, collection[0].Page.Width);
                Assert.AreEqual(640, collection[0].Page.Height);
                Assert.AreEqual(0, collection[0].Page.X);
                Assert.AreEqual(0, collection[0].Page.Y);

                Assert.AreEqual(640, collection[1].Page.Width);
                Assert.AreEqual(640, collection[1].Page.Height);
                Assert.AreEqual(0, collection[0].Page.X);
                Assert.AreEqual(0, collection[0].Page.Y);
            }
        }

        [TestMethod]
        public void Test_Warning()
        {
            int count = 0;
            EventHandler<WarningEventArgs> warningDelegate = (sender, arguments) =>
            {
                Assert.IsNotNull(sender);
                Assert.IsNotNull(arguments);
                Assert.IsNotNull(arguments.Message);
                Assert.IsNotNull(arguments.Exception);
                Assert.AreNotEqual(string.Empty, arguments.Message);

                count++;
            };

            using (IMagickImageCollection collection = new MagickImageCollection())
            {
                collection.Warning += warningDelegate;
                collection.Read(Files.EightBimTIF);

                Assert.AreNotEqual(0, count);

                int expectedCount = count;
                collection.Warning -= warningDelegate;
                collection.Read(Files.EightBimTIF);

                Assert.AreEqual(expectedCount, count);
            }
        }

        [TestMethod]
        public void Test_Write()
        {
            long fileSize;
            using (IMagickImage image = new MagickImage(Files.RoseSparkleGIF))
            {
                fileSize = image.FileSize;
            }

            Assert.AreEqual(fileSize, 9891);

            using (IMagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    collection.Write(memStream);

                    Assert.AreEqual(fileSize, memStream.Length);
                }
            }

            FileInfo tempFile = new FileInfo(Path.GetTempFileName() + ".gif");
            try
            {
                using (IMagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    collection.Write(tempFile);

                    Assert.AreEqual(fileSize, tempFile.Length);
                }
            }
            finally
            {
                Cleanup.DeleteFile(tempFile);
            }
        }

        private static void Test_Ping(IMagickImageCollection collection)
        {
            Assert.AreEqual(1, collection.Count);

            ExceptionAssert.Throws<InvalidOperationException>(() =>
            {
                collection[0].GetPixels();
            });

            ImageProfile profile = collection[0].Get8BimProfile();
            Assert.IsNotNull(profile);
        }

        private static void Test_Read(IMagickImageCollection collection)
        {
            Assert.AreEqual(3, collection.Count);
            foreach (MagickImage image in collection)
            {
                Assert.AreEqual(70, image.Width);
                Assert.AreEqual(46, image.Height);
            }
        }
    }
}
