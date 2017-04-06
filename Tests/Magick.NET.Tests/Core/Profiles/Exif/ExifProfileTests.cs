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

namespace Magick.NET.Tests
{
  [TestClass]
  public class ExifProfileTests
  {
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
          Assert.AreEqual(new Rational(300, 1), (Rational)value.Value);

        if (value.Tag == ExifTag.GPSLatitude)
        {
          Rational[] pos = (Rational[])value.Value;
          Assert.AreEqual(54, pos[0].ToDouble());
          Assert.AreEqual(59.38, pos[1].ToDouble());
          Assert.AreEqual(0, pos[2].ToDouble());
        }

        if (value.Tag == ExifTag.ShutterSpeedValue)
          Assert.AreEqual(9.5, ((SignedRational)value.Value).ToDouble());
      }
    }

    private static void TestValue(ExifValue value, string expected)
    {
      Assert.IsNotNull(value);
      Assert.AreEqual(expected, value.Value);
    }

    private static void TestValue(ExifValue value, Rational[] expected)
    {
      Assert.IsNotNull(value);
      Rational[] values = (Rational[])value.Value;
      Assert.IsNotNull(values);
      CollectionAssert.AreEqual(expected, values);
    }

    private static void TestRationalValue(ExifValue value, string expected)
    {
      Assert.IsNotNull(value);
      Assert.AreEqual(expected, value.ToString());
    }

    [TestMethod]
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

    [TestMethod]
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

    [TestMethod]
    public void Test_Fraction()
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        double exposureTime = 1.0 / 1600;

        using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
        {
          ExifProfile profile = image.GetExifProfile();

          profile.SetValue(ExifTag.ExposureTime, new Rational(exposureTime));
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
          Assert.AreNotEqual(exposureTime, ((Rational)value.Value).ToDouble());
        }

        memStream.Position = 0;
        using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
        {
          ExifProfile profile = image.GetExifProfile();

          profile.SetValue(ExifTag.ExposureTime, new Rational(exposureTime, true));
          image.AddProfile(profile);

          image.Write(memStream);
        }

        memStream.Position = 0;
        using (MagickImage image = new MagickImage(memStream))
        {
          ExifProfile profile = image.GetExifProfile();

          Assert.IsNotNull(profile);

          ExifValue value = profile.GetValue(ExifTag.ExposureTime);
          Assert.AreEqual(exposureTime, ((Rational)value.Value).ToDouble());
        }
      }
    }

    [TestMethod]
    public void Test_Infinity()
    {
      using (MagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        ExifProfile profile = image.GetExifProfile();
        profile.SetValue(ExifTag.ExposureBiasValue, new SignedRational(double.PositiveInfinity));
        image.AddProfile(profile);

        profile = image.GetExifProfile();
        ExifValue value = profile.GetValue(ExifTag.ExposureBiasValue);
        Assert.IsNotNull(value);
        Assert.AreEqual(double.PositiveInfinity, ((SignedRational)value.Value).ToDouble());

        profile.SetValue(ExifTag.ExposureBiasValue, new SignedRational(double.NegativeInfinity));
        image.AddProfile(profile);

        profile = image.GetExifProfile();
        value = profile.GetValue(ExifTag.ExposureBiasValue);
        Assert.IsNotNull(value);
        Assert.AreEqual(double.NegativeInfinity, ((SignedRational)value.Value).ToDouble());

        profile.SetValue(ExifTag.FlashEnergy, new Rational(double.NegativeInfinity));
        image.AddProfile(profile);

        profile = image.GetExifProfile();
        value = profile.GetValue(ExifTag.FlashEnergy);
        Assert.IsNotNull(value);
        Assert.AreEqual(double.PositiveInfinity, ((Rational)value.Value).ToDouble());
      }
    }

    [TestMethod]
    public void Test_SetValue()
    {
      Rational[] latitude = new Rational[] { new Rational(12.3), new Rational(4.56), new Rational(789.0) };

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

          profile.SetValue(ExifTag.ShutterSpeedValue, new SignedRational(75.55));

          value = profile.GetValue(ExifTag.ShutterSpeedValue);
          TestRationalValue(value, "1511/20");

          ExceptionAssert.Throws<ArgumentException>(delegate ()
          {
            value.Value = 75;
          });

          profile.SetValue(ExifTag.XResolution, new Rational(150.0));

          value = profile.GetValue(ExifTag.XResolution);
          TestRationalValue(value, "150");

          ExceptionAssert.Throws<ArgumentException>(delegate ()
          {
            value.Value = "Magick.NET";
          });

          image.Density = new Density(72);

          value = profile.GetValue(ExifTag.XResolution);
          TestRationalValue(value, "150");

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
          TestRationalValue(value, "1511/20");

          value = profile.GetValue(ExifTag.XResolution);
          TestRationalValue(value, "72");

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

    [TestMethod]
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

    [TestMethod]
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

    [TestMethod]
    public void Test_ExifTypeUndefined()
    {
      using (MagickImage image = new MagickImage(Files.ExifUndefType))
      {
        ExifProfile profile = image.GetExifProfile();
        Assert.IsNotNull(profile);

        foreach(ExifValue value in profile.Values)
        {
          if (value.DataType == ExifDataType.Undefined)
            Assert.AreEqual(4, value.NumberOfComponents);
        }
      }
    }
  }
}
