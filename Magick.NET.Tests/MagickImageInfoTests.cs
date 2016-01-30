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
using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class MagickImageInfoTests
  {
    private const string _Category = "MagickImageInfo";

    private MagickImageInfo CreateMagickImageInfo(MagickColor color, int width, int height)
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        using (MagickImage image = new MagickImage(color, width, height))
        {
          image.Format = MagickFormat.Png;
          image.Write(memStream);
          memStream.Position = 0;

          return new MagickImageInfo(memStream);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor()
    {
      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new MagickImageInfo(new byte[0]);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImageInfo((byte[])null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImageInfo((FileInfo)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImageInfo((Stream)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImageInfo((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        new MagickImageInfo(Files.Missing);
      });
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Count()
    {
      IEnumerable<MagickImageInfo> info = MagickImageInfo.ReadCollection(Files.RoseSparkleGIF);
      Assert.AreEqual(3, info.Count());

      MagickImageInfo first = info.First();
      Assert.AreEqual(ColorSpace.sRGB, first.ColorSpace);
      Assert.AreEqual(MagickFormat.Gif, first.Format);
      Assert.AreEqual(70, first.Width);
      Assert.AreEqual(46, first.Height);
      Assert.AreEqual(0, first.ResolutionX);
      Assert.AreEqual(0, first.ResolutionY);
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_IComparable()
    {
      MagickImageInfo first = CreateMagickImageInfo(MagickColors.Red, 10, 5);

      Assert.AreEqual(0, first.CompareTo(first));
      Assert.AreEqual(1, first.CompareTo(null));
      Assert.IsFalse(first < null);
      Assert.IsFalse(first <= null);
      Assert.IsTrue(first > null);
      Assert.IsTrue(first >= null);
      Assert.IsTrue(null < first);
      Assert.IsTrue(null <= first);
      Assert.IsFalse(null > first);
      Assert.IsFalse(null >= first);

      MagickImageInfo second = CreateMagickImageInfo(MagickColors.Green, 5, 5);

      Assert.AreEqual(1, first.CompareTo(second));
      Assert.IsFalse(first < second);
      Assert.IsFalse(first <= second);
      Assert.IsTrue(first > second);
      Assert.IsTrue(first >= second);

      second = CreateMagickImageInfo(MagickColors.Red, 5, 10);

      Assert.AreEqual(0, first.CompareTo(second));
      Assert.IsFalse(first == second);
      Assert.IsFalse(first < second);
      Assert.IsTrue(first <= second);
      Assert.IsFalse(first > second);
      Assert.IsTrue(first >= second);
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_IEquatable()
    {
      MagickImageInfo first = CreateMagickImageInfo(MagickColors.Red, 10, 10);

      Assert.IsFalse(first == null);
      Assert.IsFalse(first.Equals(null));
      Assert.IsTrue(first.Equals(first));
      Assert.IsTrue(first.Equals((object)first));

      MagickImageInfo second = CreateMagickImageInfo(MagickColors.Red, 10, 10);

      Assert.IsTrue(first == second);
      Assert.IsTrue(first.Equals(second));
      Assert.IsTrue(first.Equals((object)second));

      second = CreateMagickImageInfo(MagickColors.Green, 10, 10);

      Assert.IsTrue(first == second);
      Assert.IsTrue(first.Equals(second));
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Read()
    {
      MagickImageInfo imageInfo = new MagickImageInfo();

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        imageInfo.Read(new byte[0]);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        imageInfo.Read((byte[])null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        imageInfo.Read((FileInfo)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        imageInfo.Read((Stream)null);
      });

      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        imageInfo.Read((string)null);
      });

      ExceptionAssert.Throws<ArgumentException>(delegate ()
      {
        imageInfo.Read(Files.Missing);
      });

      imageInfo.Read(File.ReadAllBytes(Files.SnakewarePNG));

      using (FileStream fs = File.OpenRead(Files.SnakewarePNG))
      {
        imageInfo.Read(fs);
      }

      imageInfo.Read(Files.ImageMagickJPG);

      Assert.AreEqual(ColorSpace.sRGB, imageInfo.ColorSpace);
      Assert.AreEqual(CompressionMethod.JPEG, imageInfo.CompressionMethod);
      Assert.AreEqual(MagickFormat.Jpeg, imageInfo.Format);
      Assert.AreEqual(118, imageInfo.Height);
      Assert.AreEqual(Interlace.NoInterlace, imageInfo.Interlace);
      Assert.AreEqual(72, imageInfo.ResolutionX);
      Assert.AreEqual(72, imageInfo.ResolutionY);
      Assert.AreEqual(Resolution.PixelsPerInch, imageInfo.ResolutionUnits);
      Assert.AreEqual(123, imageInfo.Width);
    }
  }
}
