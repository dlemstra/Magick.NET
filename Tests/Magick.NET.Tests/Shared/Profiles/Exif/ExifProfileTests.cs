// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Profiles
{
    // TODO: Move methods to another class
    [TestClass]
    public partial class ExifProfileTests
    {
        [TestMethod]
        public void Test_Fraction()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                double exposureTime = 1.0 / 1600;

                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    ExifProfile profile = image.GetExifProfile();

                    profile.SetValue(ExifTag.ExposureTime, new Rational(exposureTime));
                    image.AddProfile(profile);

                    image.Write(memStream);
                }

                memStream.Position = 0;
                using (IMagickImage image = new MagickImage(memStream))
                {
                    ExifProfile profile = image.GetExifProfile();

                    Assert.IsNotNull(profile);

                    ExifValue value = profile.GetValue(ExifTag.ExposureTime);
                    Assert.IsNotNull(value);
                    Assert.AreNotEqual(exposureTime, ((Rational)value.Value).ToDouble());
                }

                memStream.Position = 0;
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    ExifProfile profile = image.GetExifProfile();

                    profile.SetValue(ExifTag.ExposureTime, new Rational(exposureTime, true));
                    image.AddProfile(profile);

                    image.Write(memStream);
                }

                memStream.Position = 0;
                using (IMagickImage image = new MagickImage(memStream))
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
            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
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
        public void Test_Values()
        {
            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                ExifProfile profile = image.GetExifProfile();
                TestProfile(profile);

                using (IMagickImage emptyImage = new MagickImage(Files.ImageMagickJPG))
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
            using (IMagickImage image = new MagickImage(Files.ExifUndefType))
            {
                ExifProfile profile = image.GetExifProfile();
                Assert.IsNotNull(profile);

                foreach (ExifValue value in profile.Values)
                {
                    if (value.DataType == ExifDataType.Undefined)
                        Assert.AreEqual(4, value.NumberOfComponents);
                }
            }
        }

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
    }
}
