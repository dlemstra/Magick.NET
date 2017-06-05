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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Magick.NET.Tests
{
  [TestClass]
  public partial class MagickFactoryTests
  {
    [TestMethod]
    public void CreateCollection_ReturnsMagickImageCollection()
    {
      MagickFactory factory = new MagickFactory();
      using (IMagickImageCollection collection = factory.CreateCollection())
      {
        Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
        Assert.AreEqual(0, collection.Count);
      }
    }

    [TestMethod]
    public void CreateCollection_WithBytes_ReturnsMagickImageCollection()
    {
      var data = File.ReadAllBytes(Files.RoseSparkleGIF);

      MagickFactory factory = new MagickFactory();
      using (IMagickImageCollection collection = factory.CreateCollection(data))
      {
        Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
        Assert.AreEqual(3, collection.Count);
      }
    }

    [TestMethod]
    public void CreateCollection_WithBytesAndSettings_ReturnsMagickImageCollection()
    {
      var data = File.ReadAllBytes(Files.RoseSparkleGIF);
      var readSettings = new MagickReadSettings
      {
        BackgroundColor = MagickColors.Firebrick
      };


      MagickFactory factory = new MagickFactory();
      using (IMagickImageCollection collection = factory.CreateCollection(data, readSettings))
      {
        Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
        Assert.AreEqual(3, collection.Count);
        Assert.AreEqual(MagickColors.Firebrick, collection.First().Settings.BackgroundColor);
      }
    }

    [TestMethod]
    public void CreateCollection_WithFileInfo_ReturnsMagickImageCollection()
    {
      var file = new FileInfo(Files.RoseSparkleGIF);

      MagickFactory factory = new MagickFactory();
      using (IMagickImageCollection collection = factory.CreateCollection(file))
      {
        Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
        Assert.AreEqual(3, collection.Count);
      }
    }

    [TestMethod]
    public void CreateCollection_WithFileInfoAndSettings_ReturnsMagickImageCollection()
    {
      var file = new FileInfo(Files.RoseSparkleGIF);
      var readSettings = new MagickReadSettings
      {
        BackgroundColor = MagickColors.Firebrick
      };

      MagickFactory factory = new MagickFactory();
      using (IMagickImageCollection collection = factory.CreateCollection(file, readSettings))
      {
        Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
        Assert.AreEqual(3, collection.Count);
        Assert.AreEqual(MagickColors.Firebrick, collection.First().Settings.BackgroundColor);
      }
    }

    [TestMethod]
    public void CreateCollection_IEnumerableImages_ReturnsMagickImageCollection()
    {
      var image = new MagickImage(Files.ImageMagickJPG);
      var images = new MagickImage[] { image };

      MagickFactory factory = new MagickFactory();
      using (IMagickImageCollection collection = factory.CreateCollection(images))
      {
        Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
        Assert.AreEqual(1, collection.Count);
        Assert.AreEqual(image, collection.First());
      }

      ExceptionAssert.Throws<ObjectDisposedException>(() =>
      {
        Assert.IsFalse(image.HasAlpha);
      });
    }

    [TestMethod]
    public void CreateCollection_WithStream_ReturnsMagickImageCollection()
    {
      using (var stream = File.OpenRead(Files.RoseSparkleGIF))
      {
        MagickFactory factory = new MagickFactory();
        using (IMagickImageCollection collection = factory.CreateCollection(stream))
        {
          Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
          Assert.AreEqual(3, collection.Count);
        }
      }
    }

    [TestMethod]
    public void CreateCollection_WithStreamAndSettings_ReturnsMagickImageCollection()
    {
      using (var stream = File.OpenRead(Files.RoseSparkleGIF))
      {
        var readSettings = new MagickReadSettings
        {
          BackgroundColor = MagickColors.Firebrick
        };

        MagickFactory factory = new MagickFactory();
        using (IMagickImageCollection collection = factory.CreateCollection(stream, readSettings))
        {
          Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
          Assert.AreEqual(3, collection.Count);
          Assert.AreEqual(MagickColors.Firebrick, collection.First().Settings.BackgroundColor);
        }
      }
    }

    [TestMethod]
    public void CreateCollection_WithFileName_ReturnsMagickImageCollection()
    {
      MagickFactory factory = new MagickFactory();
      using (IMagickImageCollection collection = factory.CreateCollection(Files.RoseSparkleGIF))
      {
        Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
        Assert.AreEqual(3, collection.Count);
      }
    }

    [TestMethod]
    public void CreateCollection_WithFileNameAndSettings_ReturnsMagickImageCollection()
    {
      var readSettings = new MagickReadSettings
      {
        BackgroundColor = MagickColors.Firebrick
      };

      MagickFactory factory = new MagickFactory();
      using (IMagickImageCollection collection = factory.CreateCollection(Files.RoseSparkleGIF, readSettings))
      {
        Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
        Assert.AreEqual(3, collection.Count);
        Assert.AreEqual(MagickColors.Firebrick, collection.First().Settings.BackgroundColor);
      }
    }

    [TestMethod]
    public void CreateImage_ReturnsMagickImage()
    {
      MagickFactory factory = new MagickFactory();
      using (IMagickImage image = factory.CreateImage())
      {
        Assert.IsInstanceOfType(image, typeof(MagickImage));
        Assert.AreEqual(0, image.Width);
      }
    }

    [TestMethod]
    public void CreateImage_WithBytes_ReturnsMagickImage()
    {
      var data = File.ReadAllBytes(Files.ImageMagickJPG);

      MagickFactory factory = new MagickFactory();
      using (IMagickImage image = factory.CreateImage(data))
      {
        Assert.IsInstanceOfType(image, typeof(MagickImage));
        Assert.AreEqual(123, image.Width);
      }
    }

    [TestMethod]
    public void CreateImage_WithBytesAndSettings_ReturnsMagickImage()
    {
      var data = File.ReadAllBytes(Files.ImageMagickJPG);
      var readSettings = new MagickReadSettings
      {
        BackgroundColor = MagickColors.Goldenrod
      };

      MagickFactory factory = new MagickFactory();
      using (IMagickImage image = factory.CreateImage(data, readSettings))
      {
        Assert.IsInstanceOfType(image, typeof(MagickImage));
        Assert.AreEqual(123, image.Width);
        Assert.AreEqual(MagickColors.Goldenrod, image.Settings.BackgroundColor);
      }
    }

    [TestMethod]
    public void CreateImage_WithFileInfo_ReturnsMagickImage()
    {
      var file = new FileInfo(Files.ImageMagickJPG);

      MagickFactory factory = new MagickFactory();
      using (IMagickImage image = factory.CreateImage(file))
      {
        Assert.IsInstanceOfType(image, typeof(MagickImage));
        Assert.AreEqual(123, image.Width);
      }
    }

    [TestMethod]
    public void CreateImage_WithFileInfoAndSettings_ReturnsMagickImage()
    {
      var file = new FileInfo(Files.ImageMagickJPG);
      var readSettings = new MagickReadSettings
      {
        BackgroundColor = MagickColors.Goldenrod
      };

      MagickFactory factory = new MagickFactory();
      using (IMagickImage image = factory.CreateImage(file, readSettings))
      {
        Assert.IsInstanceOfType(image, typeof(MagickImage));
        Assert.AreEqual(123, image.Width);
        Assert.AreEqual(MagickColors.Goldenrod, image.Settings.BackgroundColor);
      }
    }

    [TestMethod]
    public void CreateImage_WithColor_ReturnsMagickImage()
    {
      var color = MagickColors.Goldenrod;

      MagickFactory factory = new MagickFactory();
      using (IMagickImage image = factory.CreateImage(color, 10, 5))
      {
        Assert.IsInstanceOfType(image, typeof(MagickImage));
        Assert.AreEqual(10, image.Width);
        Assert.AreEqual(5, image.Height);

        using (PixelCollection pixels = image.GetPixels())
        {
          ColorAssert.AreEqual(MagickColors.Goldenrod, image, 0, 0);
        }
      }
    }

    [TestMethod]
    public void CreateImage_WithStream_ReturnsMagickImage()
    {
      using (var stream = File.OpenRead(Files.ImageMagickJPG))
      {
        MagickFactory factory = new MagickFactory();
        using (IMagickImage image = factory.CreateImage(stream))
        {
          Assert.IsInstanceOfType(image, typeof(MagickImage));
          Assert.AreEqual(123, image.Width);
        }
      }
    }

    [TestMethod]
    public void CreateImage_WithStreamAndSettings_ReturnsMagickImage()
    {
      using (var stream = File.OpenRead(Files.ImageMagickJPG))
      {
        var readSettings = new MagickReadSettings
        {
          BackgroundColor = MagickColors.Goldenrod
        };

        MagickFactory factory = new MagickFactory();
        using (IMagickImage image = factory.CreateImage(stream, readSettings))
        {
          Assert.IsInstanceOfType(image, typeof(MagickImage));
          Assert.AreEqual(123, image.Width);
          Assert.AreEqual(MagickColors.Goldenrod, image.Settings.BackgroundColor);
        }
      }
    }

    [TestMethod]
    public void CreateImage_WithFileName_ReturnsMagickImage()
    {
      MagickFactory factory = new MagickFactory();
      using (IMagickImage image = factory.CreateImage(Files.ImageMagickJPG))
      {
        Assert.IsInstanceOfType(image, typeof(MagickImage));
        Assert.AreEqual(123, image.Width);
      }
    }

    [TestMethod]
    public void CreateImage_WithFileNameAndSettings_ReturnsMagickImage()
    {
      var readSettings = new MagickReadSettings
      {
        BackgroundColor = MagickColors.Goldenrod
      };

      MagickFactory factory = new MagickFactory();
      using (IMagickImage image = factory.CreateImage(Files.ImageMagickJPG, readSettings))
      {
        Assert.IsInstanceOfType(image, typeof(MagickImage));
        Assert.AreEqual(123, image.Width);
        Assert.AreEqual(MagickColors.Goldenrod, image.Settings.BackgroundColor);
      }
    }

    [TestMethod]
    public void CreateImage_WithFileNameAndSize_ReturnsMagickImage()
    {
      MagickFactory factory = new MagickFactory();
      using (IMagickImage image = factory.CreateImage("label:Dirk", 5, 10))
      {
        Assert.IsInstanceOfType(image, typeof(MagickImage));
        Assert.AreEqual(5, image.Width);
        Assert.AreEqual(10, image.Height);
      }
    }

    [TestMethod]
    public void CreateImageInfo_ReturnsMagickImageInfo()
    {
      MagickFactory factory = new MagickFactory();
      IMagickImageInfo imageInfo = factory.CreateImageInfo();

      Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
      Assert.AreEqual(0, imageInfo.Width);
    }

    [TestMethod]
    public void CreateImageInfo_WithBytes_ReturnsMagickImageInfo()
    {
      var data = File.ReadAllBytes(Files.ImageMagickJPG);

      MagickFactory factory = new MagickFactory();
      IMagickImageInfo imageInfo = factory.CreateImageInfo(data);

      Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
      Assert.AreEqual(123, imageInfo.Width);
    }

    [TestMethod]
    public void CreateImageInfo_WithBytesAndSettings_ReturnsMagickImageInfo()
    {
      var data = new byte[] { 255 };
      MagickReadSettings readSettings = CreateReadSettings();

      MagickFactory factory = new MagickFactory();
      IMagickImageInfo imageInfo = factory.CreateImageInfo(data, readSettings);

      Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
      Assert.AreEqual(1, imageInfo.Width);
    }

    [TestMethod]
    public void CreateImageInfo_WithFileInfo_ReturnsMagickImageInfo()
    {
      var file = new FileInfo(Files.ImageMagickJPG);

      MagickFactory factory = new MagickFactory();
      IMagickImageInfo imageInfo = factory.CreateImageInfo(file);

      Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
      Assert.AreEqual(123, imageInfo.Width);
    }

    [TestMethod]
    public void CreateImageInfo_WithFileInfoAndSettings_ReturnsMagickImageInfo()
    {
      var data = new byte[] { 255 };
      var readSettings = CreateReadSettings();

      using (TemporaryFile file = new TemporaryFile(data))
      {
        MagickFactory factory = new MagickFactory();
        IMagickImageInfo imageInfo = factory.CreateImageInfo(file.FileInfo, readSettings);

        Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
        Assert.AreEqual(1, imageInfo.Width);
      }
    }

    [TestMethod]
    public void CreateImageInfo_WithStream_ReturnsMagickImageInfo()
    {
      using (var stream = File.OpenRead(Files.ImageMagickJPG))
      {
        MagickFactory factory = new MagickFactory();
        IMagickImageInfo imageInfo = factory.CreateImageInfo(stream);

        Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
        Assert.AreEqual(123, imageInfo.Width);
      }
    }

    [TestMethod]
    public void CreateImageInfo_WithStreamAndSettings_ReturnsMagickImageInfo()
    {
      var data = new byte[] { 255 };
      var readSettings = CreateReadSettings();

      using (TemporaryFile file = new TemporaryFile(data))
      {
        using (var stream = file.FileInfo.OpenRead())
        {
          MagickFactory factory = new MagickFactory();
          IMagickImageInfo imageInfo = factory.CreateImageInfo(stream, readSettings);

          Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
          Assert.AreEqual(1, imageInfo.Width);
        }
      }
    }

    [TestMethod]
    public void CreateImageInfo_WithFileName_ReturnsMagickImageInfo()
    {
      MagickFactory factory = new MagickFactory();
      IMagickImageInfo imageInfo = factory.CreateImageInfo(Files.ImageMagickJPG);

      Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
      Assert.AreEqual(123, imageInfo.Width);
    }

    [TestMethod]
    public void CreateImageInfo_WithFileNameAndSettings_ReturnsMagickImageInfo()
    {
      var data = new byte[] { 255 };
      var readSettings = CreateReadSettings();

      using (TemporaryFile file = new TemporaryFile(data))
      {
        MagickFactory factory = new MagickFactory();
        IMagickImageInfo imageInfo = factory.CreateImageInfo(file.FileInfo.FullName, readSettings);

        Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
        Assert.AreEqual(1, imageInfo.Width);
      }
    }

    private static MagickReadSettings CreateReadSettings()
    {
      return new MagickReadSettings
      {
        Width = 1,
        Height = 1,
        PixelStorage = new PixelStorageSettings()
        {
          Mapping = "R",
          StorageType = StorageType.Char
        }
      };
    }
  }
}
