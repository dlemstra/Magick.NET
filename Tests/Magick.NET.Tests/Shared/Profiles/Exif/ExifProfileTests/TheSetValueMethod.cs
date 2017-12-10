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
using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Profiles.Exif
{
    public partial class ExifProfileTests
    {
        [TestClass]
        public class TheSetValueMethod
        {
            [TestMethod]
            public void ShouldUpdateTheDataInTheProfile()
            {
                using (var memStream = new MemoryStream())
                {
                    using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                    {
                        var profile = image.GetExifProfile();
                        Assert.IsNull(profile);

                        profile = new ExifProfile();
                        profile.SetValue(ExifTag.Copyright, "Dirk Lemstra");

                        image.AddProfile(profile);

                        profile = image.GetExifProfile();
                        Assert.IsNotNull(profile);

                        image.Write(memStream);
                    }

                    memStream.Position = 0;
                    using (IMagickImage image = new MagickImage(memStream))
                    {
                        var profile = image.GetExifProfile();

                        Assert.IsNotNull(profile);
                        Assert.AreEqual(1, profile.Values.Count());

                        ExifValue value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.Copyright);
                        TestValue(value, "Dirk Lemstra");
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenExifTagIsInvalid()
            {
                var exifProfile = new ExifProfile();

                ExceptionAssert.Throws<NotSupportedException>(() =>
                {
                    exifProfile.SetValue((ExifTag)42, 42);
                });
            }

            [TestMethod]
            public void ShouldCorrectlyHandleFraction()
            {
                var profile = new ExifProfile();
                profile.SetValue(ExifTag.ShutterSpeedValue, new SignedRational(75.55));

                var value = profile.GetValue(ExifTag.ShutterSpeedValue);

                Assert.IsNotNull(value);
                Assert.AreEqual("1511/20", value.ToString());
            }

            [TestMethod]
            public void ShouldCorrectlyHandleArray()
            {
                Rational[] latitude = new Rational[] { new Rational(12.3), new Rational(4.56), new Rational(789.0) };

                var profile = new ExifProfile();
                profile.SetValue(ExifTag.GPSLatitude, latitude);

                var value = profile.GetValue(ExifTag.GPSLatitude);

                value = profile.GetValue(ExifTag.GPSLatitude);

                Assert.IsNotNull(value);
                Rational[] values = (Rational[])value.Value;
                Assert.IsNotNull(values);
                CollectionAssert.AreEqual(latitude, values);
            }

            [TestMethod]
            public void ShouldAllowNullValues()
            {
                var profile = new ExifProfile();
                profile.SetValue(ExifTag.ReferenceBlackWhite, null);

                var value = profile.GetValue(ExifTag.ReferenceBlackWhite);
                TestValue(value, null);
            }
        }
    }
}
