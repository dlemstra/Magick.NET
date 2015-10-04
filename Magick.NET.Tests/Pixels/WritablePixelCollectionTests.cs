//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Drawing;
using System.IO;
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
  public sealed class WritablePixelCollectionTests
  {
    private const string _Category = "WritablePixelCollection";

    private static void Test_PixelColor(PixelCollection pixels, Color color)
    {
      var values = pixels.GetValue(0, 0);
      Assert.AreEqual(3, values.Length);

      MagickColor magickColor = new MagickColor(values[0], values[1], values[2]);
      ColorAssert.AreEqual(color, magickColor);
    }

    private static void Test_PixelColor(WritablePixelCollection pixels, Color color)
    {
      var values = pixels.GetValue(0, 0);
      Assert.AreEqual(3, values.Length);

      MagickColor magickColor = new MagickColor(values[0], values[1], values[2]);
      ColorAssert.AreEqual(color, magickColor);
    }

    private static void TestPixels(MagickImage image, MagickColor color)
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        image.Format = MagickFormat.Png;
        image.Write(memStream);
        memStream.Position = 0;

        using (MagickImage output = new MagickImage(memStream))
        {
          using (PixelCollection pixels = image.GetReadOnlyPixels())
          {
            for (int i = 0; i < 10; i++)
              ColorAssert.AreEqual(color, pixels.GetPixel(i, 0).ToColor());
          }
        }
      }
    }

    private static void Test_Set(WritablePixelCollection pixels, QuantumType[] value)
    {
      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        pixels.Set(value);
      });
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Dimensions()
    {
      using (MagickImage image = new MagickImage(Color.Red, 5, 10))
      {
        using (WritablePixelCollection pixels = image.GetWritablePixels())
        {
          Assert.AreEqual(5, pixels.Width);
          Assert.AreEqual(10, pixels.Height);
          Assert.AreEqual(3, pixels.Channels);
          Assert.AreEqual(5 * 10 * pixels.Channels, pixels.GetValues().Length);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_GetValue()
    {
      using (MagickImage image = new MagickImage(Color.Red, 5, 10))
      {
        using (WritablePixelCollection pixels = image.GetWritablePixels())
        {
          Test_PixelColor(pixels, Color.Red);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Set()
    {
      using (MagickImage image = new MagickImage(Color.Red, 5, 10))
      {
        using (WritablePixelCollection pixels = image.GetWritablePixels())
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
          Test_PixelColor(pixels, Color.Black);
          pixels.Write();
        }

        using (PixelCollection pixels = image.GetReadOnlyPixels())
        {
          Test_PixelColor(pixels, Color.Black);
        }

        using (WritablePixelCollection pixels = image.GetWritablePixels())
        {
          pixels.Set(new uint[] { 4294967295, 0, 0 });
          Test_PixelColor(pixels, Color.Red);
          pixels.Set(new ushort[] { 0, 0, 65535 });
          Test_PixelColor(pixels, Color.Blue);
          pixels.Set(new byte[] { 0, 255, 0 });
          Test_PixelColor(pixels, Color.Lime);
        }

        using (WritablePixelCollection pixels = image.GetWritablePixels())
        {
          for (int x = 0; x < pixels.Width; x++)
          {
            for (int y = 0; y < pixels.Height; y++)
            {
              pixels.Set(x, y, new QuantumType[] { 0, 0, 0 });
            }
          }
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Write()
    {
      using (MagickImage image = new MagickImage(Color.Red, 10, 1))
      {
        using (WritablePixelCollection pixels = image.GetWritablePixels())
        {
          byte[] bytes = new byte[10 * pixels.Channels];
          for (int i = 0; i < bytes.Length; i++)
            bytes[i] = 0;

          pixels.Set(bytes);
          pixels.Write();
        }

        TestPixels(image, new MagickColor(0, 0, 0));

        using (WritablePixelCollection pixels = image.GetWritablePixels())
        {
          foreach (Pixel pixel in pixels)
          {
            pixel.SetChannel(2, Quantum.Max);
          }

          pixels.Write();
        }

        TestPixels(image, Color.Blue);
      }
    }
  }
}
