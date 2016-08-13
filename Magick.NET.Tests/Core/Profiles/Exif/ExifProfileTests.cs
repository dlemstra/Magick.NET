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
using System.Collections;
using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class ExifProfileTests
  {
    private const string _Category = "ExifProfile";

    private static void TestProfile(ExifProfile profile)
    {
      Assert.IsNotNull(profile);

      Assert.AreEqual(44, profile.Values.Count());

      foreach (ExifValue value in profile.Values)
      {
        Assert.IsNotNull(value.Value);

        if (value.Tag == ExifTag.Software)
          Assert.AreEqual("Adobe Photoshop 7.0", value.ToString());

        if (value.Tag == ExifTag.XResolution)
          Assert.AreEqual(300.0, value.Value);

        if (value.Tag == ExifTag.GPSLatitude)
        {
          double[] pos = (double[])value.Value;
          Assert.AreEqual(54, pos[0]);
          Assert.AreEqual(59.38, pos[1]);
          Assert.AreEqual(0, pos[2]);
        }

        if (value.Tag == ExifTag.ShutterSpeedValue)
          Assert.AreEqual(9.5, value.Value);
      }
    }

    private static void TestValue(ExifValue value, string expected)
    {
      Assert.IsNotNull(value);
      Assert.AreEqual(expected, value.Value);
    }

    private static void TestValue(ExifValue value, double expected)
    {
      Assert.IsNotNull(value);
      Assert.AreEqual(expected, value.Value);
    }

    private static void TestValue(ExifValue value, double[] expected)
    {
      Assert.IsNotNull(value);
      CollectionAssert.AreEqual(expected, (ICollection)value.Value);
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor()
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
        {
          ExifProfile profile = image.GetExifProfile();
          Assert.IsNull(profile);

          profile = new ExifProfile();
          profile.SetValue(ExifTag.Copyright, "Dirk Lemstra");

          image.AddProfile(profile);

          profile = image.GetExifProfile();
          Assert.IsNotNull(profile);

          image.Write(memStream);
        }

        memStream.Position = 0;
        using (MagickImage image = new MagickImage(memStream))
        {
          ExifProfile profile = image.GetExifProfile();

          Assert.IsNotNull(profile);
          Assert.AreEqual(1, profile.Values.Count());

          ExifValue value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.Copyright);
          TestValue(value, "Dirk Lemstra");
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor_Empty()
    {
      using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
      {
        using (MemoryStream memStream = new MemoryStream())
        {
          ExifProfile profile = new ExifProfile(memStream);
          image.AddProfile(profile);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Fraction()
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        double exposureTime = 1.0 / 1600;

        using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
        {
          ExifProfile profile = image.GetExifProfile();

          profile.SetValue(ExifTag.ExposureTime, exposureTime);
          image.AddProfile(profile);

          image.Write(memStream);
        }

        memStream.Position = 0;
        using (MagickImage image = new MagickImage(memStream))
        {
          ExifProfile profile = image.GetExifProfile();

          Assert.IsNotNull(profile);

          ExifValue value = profile.GetValue(ExifTag.ExposureTime);
          Assert.IsNotNull(value);
          Assert.AreNotEqual(exposureTime, value.Value);
        }

        memStream.Position = 0;
        using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
        {
          ExifProfile profile = image.GetExifProfile();

          profile.SetValue(ExifTag.ExposureTime, exposureTime);
          profile.BestPrecision = true;
          image.AddProfile(profile);

          image.Write(memStream);
        }

        memStream.Position = 0;
        using (MagickImage image = new MagickImage(memStream))
        {
          ExifProfile profile = image.GetExifProfile();

          Assert.IsNotNull(profile);

          ExifValue value = profile.GetValue(ExifTag.ExposureTime);
          TestValue(value, exposureTime);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Infinity()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        ExifProfile profile = image.GetExifProfile();
        profile.SetValue(ExifTag.ExposureBiasValue, double.PositiveInfinity);
        image.AddProfile(profile);

        profile = image.GetExifProfile();
        ExifValue value = profile.GetValue(ExifTag.ExposureBiasValue);
        Assert.IsNotNull(value);
        Assert.AreEqual(double.PositiveInfinity, value.Value);

        profile.SetValue(ExifTag.ExposureBiasValue, double.NegativeInfinity);
        image.AddProfile(profile);

        profile = image.GetExifProfile();
        value = profile.GetValue(ExifTag.ExposureBiasValue);
        Assert.IsNotNull(value);
        Assert.AreEqual(double.NegativeInfinity, value.Value);

        profile.SetValue(ExifTag.FlashEnergy, double.NegativeInfinity);
        image.AddProfile(profile);

        profile = image.GetExifProfile();
        value = profile.GetValue(ExifTag.FlashEnergy);
        Assert.IsNotNull(value);
        Assert.AreEqual(double.PositiveInfinity, value.Value);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_SetValue()
    {
      double[] latitude = new double[] { 12.3, 4.56, 789.0 };

      using (MemoryStream memStream = new MemoryStream())
      {
        using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
        {
          ExifProfile profile = image.GetExifProfile();
          profile.SetValue(ExifTag.Software, "Magick.NET");

          ExifValue value = profile.GetValue(ExifTag.Software);
          TestValue(value, "Magick.NET");

          ExceptionAssert.Throws<ArgumentException>(delegate ()
          {
            value.Value = 15;
          });

          profile.SetValue(ExifTag.ShutterSpeedValue, 75.55);

          value = profile.GetValue(ExifTag.ShutterSpeedValue);
          TestValue(value, 75.55);

          ExceptionAssert.Throws<ArgumentException>(delegate ()
          {
            value.Value = 75;
          });

          profile.SetValue(ExifTag.XResolution, 150.0);

          value = profile.GetValue(ExifTag.XResolution);
          TestValue(value, 150.0);

          ExceptionAssert.Throws<ArgumentException>(delegate ()
          {
            value.Value = "Magick.NET";
          });

          image.Density = new Density(72);

          value = profile.GetValue(ExifTag.XResolution);
          TestValue(value, 150.0);

          value = profile.GetValue(ExifTag.ReferenceBlackWhite);
          Assert.IsNotNull(value);

          profile.SetValue(ExifTag.ReferenceBlackWhite, null);

          value = profile.GetValue(ExifTag.ReferenceBlackWhite);
          TestValue(value, (string)null);

          profile.SetValue(ExifTag.GPSLatitude, latitude);

          value = profile.GetValue(ExifTag.GPSLatitude);
          TestValue(value, latitude);

          image.AddProfile(profile);

          image.Write(memStream);
        }

        memStream.Position = 0;
        using (MagickImage image = new MagickImage(memStream))
        {
          ExifProfile profile = image.GetExifProfile();

          Assert.IsNotNull(profile);
          Assert.AreEqual(43, profile.Values.Count());

          ExifValue value = profile.GetValue(ExifTag.Software);
          TestValue(value, "Magick.NET");

          value = profile.GetValue(ExifTag.ShutterSpeedValue);
          TestValue(value, 75.55);

          value = profile.GetValue(ExifTag.XResolution);
          TestValue(value, 72.0);

          value = profile.GetValue(ExifTag.ReferenceBlackWhite);
          Assert.IsNull(value);

          value = profile.GetValue(ExifTag.GPSLatitude);
          TestValue(value, latitude);

          profile.Parts = ExifParts.ExifTags;

          image.AddProfile(profile);

          memStream.Position = 0;
          image.Write(memStream);
        }

        memStream.Position = 0;
        using (MagickImage image = new MagickImage(memStream))
        {
          ExifProfile profile = image.GetExifProfile();

          Assert.IsNotNull(profile);
          Assert.AreEqual(24, profile.Values.Count());

          Assert.IsNotNull(profile.GetValue(ExifTag.FNumber));
          Assert.IsTrue(profile.RemoveValue(ExifTag.FNumber));
          Assert.IsFalse(profile.RemoveValue(ExifTag.FNumber));
          Assert.IsNull(profile.GetValue(ExifTag.FNumber));

          Assert.AreEqual(23, profile.Values.Count());
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Thumbnail()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        ExifProfile profile = image.GetExifProfile();
        Assert.IsNotNull(profile);

        using (MagickImage thumbnail = profile.CreateThumbnail())
        {
          Assert.IsNotNull(thumbnail);
          Assert.AreEqual(128, thumbnail.Width);
          Assert.AreEqual(85, thumbnail.Height);
          Assert.AreEqual(MagickFormat.Jpeg, thumbnail.Format);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Values()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        ExifProfile profile = image.GetExifProfile();
        TestProfile(profile);

        using (MagickImage emptyImage = new MagickImage(Files.ImageMagickJPG))
        {
          Assert.IsNull(emptyImage.GetExifProfile());
          emptyImage.AddProfile(profile);

          profile = emptyImage.GetExifProfile();
          TestProfile(profile);
        }
      }
    }
  }
}
