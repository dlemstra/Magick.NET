//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
  public sealed class PixelCollectionTests
  {
    private static void TestPixels(MagickImage image, MagickColor color)
    {
      TestPixels(image, color, color);
    }

    private static void TestPixels(MagickImage image, MagickColor firstRow, MagickColor secondRow)
    {
      using (PixelCollection pixels = image.GetPixels())
      {
        for (int y = 0; y < 2; y++)
          for (int x = 0; x < 10; x++)
            ColorAssert.AreEqual(y == 0 ? firstRow : secondRow, pixels.GetPixel(x, y).ToColor());
      }

      using (MemoryStream memStream = new MemoryStream())
      {
        image.Format = MagickFormat.Bmp;
        image.Write(memStream);
        memStream.Position = 0;

        using (MagickImage output = new MagickImage(memStream))
        {
          using (PixelCollection pixels = output.GetPixels())
          {
            for (int y = 0; y < 2; y++)
              for (int x = 0; x < 10; x++)
                ColorAssert.AreEqual(y == 0 ? firstRow : secondRow, pixels.GetPixel(x, y).ToColor());
          }
        }
      }
    }

    private static void Test_Set(PixelCollection pixels, QuantumType[] value)
    {
      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        pixels.Set(value);
      });
    }

    private static void Test_PixelColor(PixelCollection pixels, MagickColor color)
    {
      Test_PixelColor(pixels, 0, 0, color);
    }

    private static void Test_PixelColor(PixelCollection pixels, int x, int y, MagickColor color)
    {
      var values = pixels.GetValue(x, y);
      Assert.AreEqual(3, values.Length);

      MagickColor magickColor = new MagickColor(values[0], values[1], values[2]);
      ColorAssert.AreEqual(color, magickColor);
    }

    [TestMethod]
    public void Test_Enumerator()
    {
      using (MagickImage image = new MagickImage(Files.ConnectedComponentsPNG, 10, 10))
      {
        Pixel pixel = image.GetPixels().First(p => p.ToColor() == MagickColors.Black);
        Assert.IsNotNull(pixel);

        Pixel otherPixel = null;

        using (var pixels = image.GetPixels())
        {
          for (int y = 0; y < image.Height; y++)
          {
            for (int x = 0; x < image.Width; x++)
            {
              otherPixel = pixels.GetPixel(x, y);
              if (otherPixel.ToColor() == MagickColors.Black)
                break;
            }
            if (otherPixel.ToColor() == MagickColors.Black)
              break;
          }
        }

        Assert.IsNotNull(otherPixel);

        Assert.AreEqual(pixel, otherPixel);
        Assert.AreEqual(350, pixel.X);
        Assert.AreEqual(196, pixel.Y);
        Assert.AreEqual(2, pixel.Channels);
      }
    }

    [TestMethod]
    public void Test_GetArea()
    {
      using (MagickImage image = new MagickImage(MagickColors.Fuchsia, 10, 10))
      {
        using (PixelCollection pixels = image.GetPixels())
        {
          pixels.Set(3, 3, new QuantumType[] { 0, 0, 0 });

          var valuesA = pixels.GetArea(2, 2, 4, 4);
          Assert.AreEqual(48, valuesA.Length);

          var pixelB = pixels.GetArea(new MagickGeometry(3, 3, 1, 1));
          Assert.AreEqual(3, pixelB.Length);

          var pixelA = valuesA.Skip(15).Take(3).ToArray();

          CollectionAssert.AreEqual(pixelA, pixelB);
        }
      }
    }

    [TestMethod]
    public void Test_GetValue()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 5, 10))
      {
        using (PixelCollection pixels = image.GetPixels())
        {
          var values = pixels.GetValue(0, 0);
          Assert.AreEqual(3, values.Length);

          MagickColor color = new MagickColor(values[0], values[1], values[2]);
          ColorAssert.AreEqual(MagickColors.Red, color);
        }
      }
    }

    [TestMethod]
    public void Test_GetValues()
    {
      using (MagickImage image = new MagickImage(MagickColors.PowderBlue, 1, 1))
      {
        Assert.AreEqual(3, image.ChannelCount);

        using (PixelCollection pixels = image.GetPixels())
        {
          var values = pixels.GetValues();
          Assert.AreEqual(3, values.Length);

          MagickColor color = new MagickColor(values[0], values[1], values[2]);
          ColorAssert.AreEqual(MagickColors.PowderBlue, color);
        }
      }
    }

    [TestMethod]
    public void Test_IEnumerable()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 5, 10))
      {
        using (PixelCollection pixels = image.GetPixels())
        {
          Assert.AreEqual(50, pixels.Count());
        }
      }
    }

    [TestMethod]
    public void Test_IndexOutOfRange()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 5, 10))
      {
        using (PixelCollection pixels = image.GetPixels())
        {
          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetArea(4, 0, 2, 1);
          });

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetArea(new MagickGeometry(0, 9, 1, 2));
          });

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetArea(-1, 0, 1, 1);
          });

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetArea(0, -1, 1, 1);
          });

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetArea(0, 0, -1, 1);
          });

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetArea(0, 0, 1, -1);
          });

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetValue(5, 0);
          });

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetValue(-1, 0);
          });

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetValue(0, -1);
          });

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetValue(0, 10);
          });
        }
      }
    }

    [TestMethod]
    public void Test_Set()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 5, 10))
      {
        using (PixelCollection pixels = image.GetPixels())
        {
          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            pixels.Set((QuantumType[])null);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            pixels.Set((Pixel)null);
          });

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            pixels.Set((Pixel[])null);
          });

          Assert.AreEqual(3, pixels.Channels);
          Test_Set(pixels, new QuantumType[] { });
          Test_Set(pixels, new QuantumType[] { 0 });
          Test_Set(pixels, new QuantumType[] { 0, 0 });

          pixels.Set(new QuantumType[] { 0, 0, 0 });
          Test_PixelColor(pixels, MagickColors.Black);
        }

        using (PixelCollection pixels = image.GetPixels())
        {
          Test_PixelColor(pixels, MagickColors.Black);
        }

        using (PixelCollection pixels = image.GetPixels())
        {
          pixels.Set(new uint[] { 100000, 0, 0 });
          Test_PixelColor(pixels, MagickColors.Red);
          pixels.Set(new ushort[] { 0, 0, 65535 });
          Test_PixelColor(pixels, MagickColors.Blue);
          pixels.Set(new byte[] { 0, 255, 0 });
          Test_PixelColor(pixels, MagickColors.Lime);
        }

        using (PixelCollection pixels = image.GetPixels())
        {
          pixels.SetArea(3, 3, 1, 1, new uint[] { 100000, 0, 0 });
          Test_PixelColor(pixels, 3, 3, MagickColors.Red);
          pixels.SetArea(3, 3, 1, 1, new ushort[] { 0, 0, 65535 });
          Test_PixelColor(pixels, 3, 3, MagickColors.Blue);
          pixels.SetArea(3, 3, 1, 1, new byte[] { 0, 255, 0 });
          Test_PixelColor(pixels, 3, 3, MagickColors.Lime);
        }

        using (PixelCollection pixels = image.GetPixels())
        {
          for (int x = 0; x < image.Width; x++)
          {
            for (int y = 0; y < image.Height; y++)
            {
              pixels.Set(x, y, new QuantumType[] { 0, 0, 0 });
            }
          }
        }
      }
    }

    [TestMethod]
    public void Test_SetResult()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 10, 2))
      {
        using (PixelCollection pixels = image.GetPixels())
        {
          QuantumType[] newPixels = new QuantumType[20 * pixels.Channels];
          for (int i = 0; i < newPixels.Length; i++)
            newPixels[i] = Quantum.Max;

          pixels.Set(newPixels);
        }

        TestPixels(image, new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max));
      }

      using (MagickImage image = new MagickImage(MagickColors.Black, 10, 2))
      {
        using (PixelCollection pixels = image.GetPixels())
        {
          Assert.AreEqual(20, pixels.Count());

          foreach (Pixel pixel in pixels.Take(10))
          {
            pixel.SetChannel(2, Quantum.Max);
          }

          foreach (Pixel pixel in pixels.Skip(10))
          {
            pixel.SetChannel(0, Quantum.Max);
          }
        }

        TestPixels(image, MagickColors.Blue, MagickColors.Red);
      }
    }

    [TestMethod]
    public void Test_ToByteArray()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 10, 10))
      {
        using (PixelCollection pixels = image.GetPixels())
        {
          var bytes = pixels.ToByteArray(0, 0, 1, 1, "BGR");
          Assert.AreEqual(3, bytes.Length);
          CollectionAssert.AreEqual(new byte[] { 0, 0, 255 }, bytes);

          bytes = pixels.ToByteArray(0, 0, 1, 1, "BG");
          Assert.AreEqual(2, bytes.Length);
          CollectionAssert.AreEqual(new byte[] { 0, 0 }, bytes);
        }
      }
    }

    [TestMethod]
    public void Test_ToShortArray()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 10, 10))
      {
        using (PixelCollection pixels = image.GetPixels())
        {
          var shorts = pixels.ToShortArray(0, 0, 1, 1, "BGR");
          Assert.AreEqual(3, shorts.Length);
          CollectionAssert.AreEqual(new ushort[] { 0, 0, 65535 }, shorts);

          shorts = pixels.ToShortArray(0, 0, 1, 1, "BG");
          Assert.AreEqual(2, shorts.Length);
          CollectionAssert.AreEqual(new ushort[] { 0, 0 }, shorts);
        }
      }
    }
  }
}
