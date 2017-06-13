//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ExifValueTests
    {
        private static ExifValue GetExifValue()
        {
            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                ExifProfile profile = image.GetExifProfile();
                Assert.IsNotNull(profile);

                return profile.Values.First();
            }
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            ExifValue first = GetExifValue();
            ExifValue second = GetExifValue();

            Assert.IsTrue(first == second);
            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(first.Equals((object)second));
        }

        [TestMethod]
        public void Test_InvalidTag()
        {
            var exifProfile = new ExifProfile();

            ExceptionAssert.Throws<NotSupportedException>(() =>
            {
                exifProfile.SetValue((ExifTag)42, 42);
            });
        }

        [TestMethod]
        public void Test_Properties()
        {
            ExifValue value = GetExifValue();

            Assert.AreEqual(ExifDataType.Ascii, value.DataType);
            Assert.AreEqual(ExifTag.ImageDescription, value.Tag);
            Assert.AreEqual(false, value.IsArray);
            Assert.AreEqual("Communications", value.ToString());
            Assert.AreEqual("Communications", value.Value);
        }

        [TestMethod]
        public void Test_UnknownExifTag()
        {
            var exifProfile = new ExifProfile();
            exifProfile.SetValue(ExifTag.ImageWidth, 42);

            var bytes = exifProfile.ToByteArray();
            bytes[16] = 42;

            exifProfile = new ExifProfile(bytes);

            ExifTag unkownTag = (ExifTag)298;
            ExifValue value = exifProfile.GetValue(unkownTag);
            Assert.AreEqual(42, value.Value);
            Assert.AreEqual("42", value.ToString());

            bytes = exifProfile.ToByteArray();
            Assert.AreEqual(0, bytes.Length);
        }
    }
}
