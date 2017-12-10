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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Profiles.Exif
{
    public partial class ExifProfileTests
    {
        [TestClass]
        public class TheToByteArrayMethod
        {
            [TestMethod]
            public void ShouldReturnEmptyArrayWhenEmpty()
            {
                var exifProfile = new ExifProfile();

                var bytes = exifProfile.ToByteArray();
                Assert.AreEqual(0, bytes.Length);
            }

            [TestMethod]
            public void ShouldReturnEmptyArrayWhenAllValuesAreInvalid()
            {
                var exifProfile = new ExifProfile();
                exifProfile.SetValue(ExifTag.ImageWidth, 42);

                var bytes = exifProfile.ToByteArray();
                bytes[16] = 42;

                exifProfile = new ExifProfile(bytes);

                var unkownTag = (ExifTag)298;
                var value = exifProfile.GetValue(unkownTag);
                Assert.AreEqual(42, value.Value);
                Assert.AreEqual("42", value.ToString());

                bytes = exifProfile.ToByteArray();
                Assert.AreEqual(0, bytes.Length);
            }
        }
    }
}
