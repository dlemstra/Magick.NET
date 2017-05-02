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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class PixelStorageSettingsTests
  {
    private static MagickReadSettings CreateSettings()
    {
      MagickReadSettings settings = new MagickReadSettings();
      settings.PixelStorage = new PixelStorageSettings(StorageType.Double, "RGBA");
      settings.Width = 2;
      settings.Height = 1;

      return settings;
    }

    [TestMethod]
    public void Test_Collection_Exceptions()
    {
      using (IMagickImageCollection collection = new MagickImageCollection())
      {
        MagickReadSettings settings = new MagickReadSettings();
        settings.PixelStorage = new PixelStorageSettings();

        ExceptionAssert.Throws<ArgumentException>(delegate ()
        {
          collection.Read(Files.RoseSparkleGIF, settings);
        });
      }
    }

    [TestMethod]
    public void Test_Image_Exceptions()
    {
      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        MagickReadSettings settings = CreateSettings();
        settings.PixelStorage.StorageType = StorageType.Undefined;
        new MagickImage(Files.SnakewarePNG, settings);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        MagickReadSettings settings = CreateSettings();
        settings.PixelStorage.Mapping = null;
        new MagickImage(Files.SnakewarePNG, settings);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        MagickReadSettings settings = CreateSettings();
        settings.PixelStorage.Mapping = "";
        new MagickImage(Files.SnakewarePNG, settings);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        MagickReadSettings settings = CreateSettings();
        settings.Width = null;
        new MagickImage(Files.SnakewarePNG, settings);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        MagickReadSettings settings = CreateSettings();
        settings.Height = null;
        new MagickImage(Files.SnakewarePNG, settings);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        MagickReadSettings settings = CreateSettings();
        byte[] data = new byte[] { 0 };
        new MagickImage(data, settings);
      });
    }

    [TestMethod]
    public void Test_Image_Read()
    {
      MagickReadSettings settings = CreateSettings();

      using (MagickImage image = new MagickImage())
      {
        byte[] data = new byte[]
          {
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
          };
        image.Read(data, settings);

        Assert.AreEqual(2, image.Width);
        Assert.AreEqual(1, image.Height);

        using (PixelCollection pixels = image.GetPixels())
        {
          Pixel pixel = pixels.GetPixel(0, 0);
          Assert.AreEqual(4, pixel.Channels);
          Assert.AreEqual(0, pixel.GetChannel(0));
          Assert.AreEqual(0, pixel.GetChannel(1));
          Assert.AreEqual(0, pixel.GetChannel(2));
          Assert.AreEqual(Quantum.Max, pixel.GetChannel(3));

          pixel = pixels.GetPixel(1, 0);
          Assert.AreEqual(4, pixel.Channels);
          Assert.AreEqual(0, pixel.GetChannel(0));
          Assert.AreEqual(Quantum.Max, pixel.GetChannel(1));
          Assert.AreEqual(0, pixel.GetChannel(2));
          Assert.AreEqual(0, pixel.GetChannel(3));

          ExceptionAssert.Throws<ArgumentOutOfRangeException>(delegate ()
          {
            pixels.GetPixel(0, 1);
          });
        }
      }
    }
  }
}