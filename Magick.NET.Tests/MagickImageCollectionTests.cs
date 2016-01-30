//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class MagickImageCollectionTests
  {
    private const string _Category = "MagickImageCollection";

    private static void Test_Ping(MagickImageCollection collection)
    {
      Assert.AreEqual(1, collection.Count);

      ExceptionAssert.Throws<InvalidOperationException>(delegate ()
      {
        collection[0].GetPixels();
      });

      ImageProfile profile = collection[0].Get8BimProfile();
      Assert.IsNotNull(profile);
    }

    private static void Test_Read(MagickImageCollection collection)
    {
      Assert.AreEqual(3, collection.Count);
      foreach (MagickImage image in collection)
      {
        Assert.AreEqual(70, image.Width);
        Assert.AreEqual(46, image.Height);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_AddRange()
    {
      using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
      {
        Assert.AreEqual(3, collection.Count);

        collection.AddRange(Files.RoseSparkleGIF);
        Assert.AreEqual(6, collection.Count);

        collection.AddRange(collection);
        Assert.AreEqual(12, collection.Count);

        List<MagickImage> images = new List<MagickImage>();
        images.Add(new MagickImage("xc:red", 100, 100));
        collection.AddRange(images);
        Assert.AreEqual(13, collection.Count);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Append()
    {
      int width = 70;
      int height = 46;

      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.AppendHorizontally();
        });

        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.AppendVertically();
        });

        collection.Read(Files.RoseSparkleGIF);

        Assert.AreEqual(width, collection[0].Width);
        Assert.AreEqual(height, collection[0].Height);

        using (MagickImage image = collection.AppendHorizontally())
        {
          Assert.AreEqual(width * 3, image.Width);
          Assert.AreEqual(height, image.Height);
        }

        using (MagickImage image = collection.AppendVertically())
        {
          Assert.AreEqual(width, image.Width);
          Assert.AreEqual(height * 3, image.Height);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Clone()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        collection.Add(Files.Builtin.Logo);
        collection.Add(Files.Builtin.Rose);
        collection.Add(Files.Builtin.Wizard);

        using (MagickImageCollection clones = collection.Clone())
        {
          Assert.AreEqual(collection[0], clones[0]);
          Assert.AreEqual(collection[1], clones[1]);
          Assert.AreEqual(collection[2], clones[2]);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Coalesce()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
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

    [TestMethod, TestCategory(_Category)]
    public void Test_Combine()
    {
      using (MagickImage rose = new MagickImage(Files.Builtin.Rose))
      {
        using (MagickImageCollection collection = new MagickImageCollection())
        {
          ExceptionAssert.Throws<InvalidOperationException>(delegate ()
          {
            collection.Combine();
          });

          collection.AddRange(rose.Separate(Channels.RGB));

          Assert.AreEqual(3, collection.Count);

          MagickImage image = collection.Merge();
          Assert.AreNotEqual(rose.TotalColors, image.TotalColors);
          image.Dispose();

          image = collection.Combine();
          Assert.AreEqual(rose.TotalColors, image.TotalColors);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor()
    {
      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new MagickImageCollection(new byte[0]);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImageCollection((byte[])null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImageCollection((Stream)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImageCollection((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new MagickImageCollection(Files.Missing);
      });
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_CopyTo()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
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

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          collection.CopyTo(null, -1);
        });

        ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
        {
          collection.CopyTo(images, -1);
        });
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Deconstruct()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Deconstruct();
        });

        collection.Add(new MagickImage(MagickColors.Red, 20, 20));

        using (MagickImageCollection frames = new MagickImageCollection())
        {
          frames.Add(new MagickImage(MagickColors.Red, 10, 20));
          frames.Add(new MagickImage(MagickColors.Purple, 10, 20));

          collection.Add(frames.AppendHorizontally());
        }

        Assert.AreEqual(20, collection[1].Width);
        Assert.AreEqual(20, collection[1].Height);
        Assert.AreEqual(new MagickGeometry(0, 0, 20, 20), collection[1].Page);
        ColorAssert.AreEqual(MagickColors.Red, collection[1], 3, 3);

        collection.Deconstruct();

        Assert.AreEqual(10, collection[1].Width);
        Assert.AreEqual(20, collection[1].Height);
        Assert.AreEqual(new MagickGeometry(10, 0, 20, 20), collection[1].Page);
        ColorAssert.AreEqual(MagickColors.Purple, collection[1], 3, 3);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Dispose()
    {
      MagickImage image = new MagickImage(MagickColors.Red, 10, 10);

      MagickImageCollection collection = new MagickImageCollection();
      collection.Add(image);
      collection.Dispose();

      Assert.AreEqual(0, collection.Count);
      ExceptionAssert.Throws<ObjectDisposedException>(delegate ()
      {
        image.Flip();
      });
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Evaluate()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Evaluate(EvaluateOperator.Exponential);
        });

        collection.Add(new MagickImage(MagickColors.Yellow, 40, 10));

        using (MagickImageCollection frames = new MagickImageCollection())
        {
          frames.Add(new MagickImage(MagickColors.Green, 10, 10));
          frames.Add(new MagickImage(MagickColors.White, 10, 10));
          frames.Add(new MagickImage(MagickColors.Black, 10, 10));
          frames.Add(new MagickImage(MagickColors.Yellow, 10, 10));

          collection.Add(frames.AppendHorizontally());
        }

        using (MagickImage image = collection.Evaluate(EvaluateOperator.Min))
        {
          ColorAssert.AreEqual(MagickColors.Green, image, 0, 0);
          ColorAssert.AreEqual(MagickColors.Yellow, image, 10, 0);
          ColorAssert.AreEqual(MagickColors.Black, image, 20, 0);
          ColorAssert.AreEqual(MagickColors.Yellow, image, 30, 0);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Flatten()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Flatten();
        });

        collection.Add(new MagickImage(MagickColors.Brown, 10, 10));
        MagickImage center = new MagickImage(MagickColors.Fuchsia, 4, 4);
        center.Page = new MagickGeometry(3, 3, 4, 4);
        collection.Add(center);

        using (MagickImage image = collection.Flatten())
        {
          ColorAssert.AreEqual(MagickColors.Brown, image, 0, 0);
          ColorAssert.AreEqual(MagickColors.Fuchsia, image, 5, 5);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Index()
    {
      using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
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

    [TestMethod, TestCategory(_Category)]
    public void Test_Map()
    {
      using (MagickImageCollection colors = new MagickImageCollection())
      {
        colors.Add(new MagickImage(MagickColors.Red, 1, 1));
        colors.Add(new MagickImage(MagickColors.Green, 1, 1));

        using (MagickImage remapImage = colors.AppendHorizontally())
        {
          using (MagickImageCollection collection = new MagickImageCollection())
          {
            ExceptionAssert.Throws<InvalidOperationException>(delegate ()
            {
              collection.Map(null);
            });

            ExceptionAssert.Throws<InvalidOperationException>(delegate ()
            {
              collection.Map(remapImage);
            });

            collection.Read(Files.RoseSparkleGIF);

            ExceptionAssert.Throws<ArgumentNullException>(delegate ()
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

    [TestMethod, TestCategory(_Category)]
    public void Test_Merge()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Merge();
        });

        collection.Read(Files.RoseSparkleGIF);

        using (MagickImage first = collection.Merge())
        {
          Assert.AreEqual(collection[0].Width, first.Width);
          Assert.AreEqual(collection[0].Height, first.Height);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Montage()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        MontageSettings settings = new MontageSettings();
        settings.Geometry = new MagickGeometry(string.Format("{0}x{1}", 200, 200));
        settings.TileGeometry = new MagickGeometry(string.Format("{0}x", 2));

        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Montage(settings);
        });

        for (int i = 0; i < 9; i++)
          collection.Add(Files.Builtin.Logo);

        using (MagickImage montageResult = collection.Montage(settings))
        {
          Assert.IsNotNull(montageResult);
          Assert.AreEqual(400, montageResult.Width);
          Assert.AreEqual(1000, montageResult.Height);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Morph()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Morph(10);
        });

        collection.Add(Files.Builtin.Logo);

        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Morph(10);
        });

        collection.AddRange(Files.Builtin.Wizard);

        collection.Morph(4);
        Assert.AreEqual(6, collection.Count);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Mosaic()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Mosaic();
        });

        collection.Add(Files.SnakewarePNG);
        collection.Add(Files.ImageMagickJPG);

        using (MagickImage mosaic = collection.Mosaic())
        {
          Assert.AreEqual(286, mosaic.Width);
          Assert.AreEqual(118, mosaic.Height);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Optimize()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Optimize();
        });

        collection.Add(Files.RoseSparkleGIF);
        collection.Coalesce();
        collection.Optimize();

        Assert.Inconclusive("Needs implementation.");
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_OptimizePlus()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.OptimizePlus();
        });

        collection.Add(Files.RoseSparkleGIF);
        collection.Coalesce();
        collection.OptimizePlus();

        Assert.Inconclusive("Needs implementation.");
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_OptimizeTransparency()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.OptimizeTransparency();
        });

        collection.Add(Files.RoseSparkleGIF);
        collection.Coalesce();
        collection.OptimizeTransparency();

        Assert.Inconclusive("Needs implementation.");
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Quantize()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.Quantize();
        });

        collection.Add(Files.RoseSparkleGIF);
        collection.Quantize();
        Assert.Inconclusive("Needs implementation.");
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Smush()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.SmushHorizontal(5);
        });

        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
        {
          collection.SmushVertical(6);
        });

        collection.AddRange(Files.RoseSparkleGIF);

        using (MagickImage image = collection.SmushHorizontal(20))
        {
          Assert.AreEqual((70 * 3) + (20 * 2), image.Width);
          Assert.AreEqual(46, image.Height);
        }

        using (MagickImage image = collection.SmushVertical(40))
        {
          Assert.AreEqual(70, image.Width);
          Assert.AreEqual((46 * 3) + (40 * 2), image.Height);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Ping()
    {
      MagickImageCollection collection = new MagickImageCollection();

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        collection.Ping(new byte[0]);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        collection.Ping((byte[])null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        collection.Ping((Stream)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        collection.Ping((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
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

    [TestMethod, TestCategory(_Category)]
    public void Test_Read()
    {
      MagickImageCollection collection = new MagickImageCollection();

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        collection.Read(new byte[0]);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        collection.Read((byte[])null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        collection.Read((Stream)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        collection.Read((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
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

    [TestMethod, TestCategory(_Category)]
    public void Test_Remove()
    {
      using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
      {
        MagickImage first = collection[0];
        collection.Remove(first);

        Assert.AreEqual(2, collection.Count);
        Assert.AreEqual(-1, collection.IndexOf(first));

        first = collection[0];
        collection.RemoveAt(0);

        Assert.AreEqual(1, collection.Count);
        Assert.AreEqual(-1, collection.IndexOf(first));
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_RePage()
    {
      using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
      {
        collection[0].Page = new MagickGeometry("0x0+10+10");

        Assert.AreEqual(10, collection[0].Page.X);
        Assert.AreEqual(10, collection[0].Page.Y);

        collection[0].Settings.Page = new MagickGeometry("0x0+10+10");

        Assert.AreEqual(10, collection[0].Settings.Page.X);
        Assert.AreEqual(10, collection[0].Settings.Page.Y);

        collection.RePage();

        Assert.AreEqual(0, collection[0].Page.X);
        Assert.AreEqual(0, collection[0].Page.Y);

        Assert.AreEqual(0, collection[0].Settings.Page.X);
        Assert.AreEqual(0, collection[0].Settings.Page.Y);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Reverse()
    {
      using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
      {
        MagickImage first = collection.First();
        collection.Reverse();

        MagickImage last = collection.Last();
        Assert.IsTrue(last == first);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_ToBase64()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        Assert.AreEqual("", collection.ToBase64());

        collection.Read(Files.Builtin.Logo);
        Assert.AreEqual(1228800, collection.ToBase64(MagickFormat.Rgb).Length);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_ToBitmap()
    {
      using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
      {
        Assert.AreEqual(3, collection.Count);

        Bitmap bitmap = collection.ToBitmap();
        Assert.IsNotNull(bitmap);
        Assert.AreEqual(3, bitmap.GetFrameCount(FrameDimension.Page));
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_TrimBounds()
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        ExceptionAssert.Throws<InvalidOperationException>(delegate ()
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

    [TestMethod, TestCategory(_Category)]
    public void Test_Warning()
    {
      int count = 0;
      EventHandler<WarningEventArgs> warningDelegate = delegate (object sender, WarningEventArgs arguments)
      {
        Assert.IsNotNull(sender);
        Assert.IsNotNull(arguments);
        Assert.IsNotNull(arguments.Message);
        Assert.IsNotNull(arguments.Exception);
        Assert.AreNotEqual("", arguments.Message);

        count++;
      };

      using (MagickImageCollection collection = new MagickImageCollection())
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

    [TestMethod, TestCategory(_Category)]
    public void Test_Write()
    {
      long fileSize;
      using (MagickImage image = new MagickImage(Files.RoseSparkleGIF))
      {
        fileSize = image.FileSize;
      }

      using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
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
        using (MagickImageCollection collection = new MagickImageCollection(Files.RoseSparkleGIF))
        {
          collection.Write(tempFile);

          Assert.AreEqual(fileSize, tempFile.Length);
        }
      }
      finally
      {
        if (tempFile.Exists)
          tempFile.Delete();
      }
    }
  }
}

