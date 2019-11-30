// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class ExifProfileTests
    {
        [TestClass]
        public class TheToByteArrayMethod
        {
            [TestMethod]
            public void ShouldReturnEmptyArrayWhenEmpty()
            {
                var profile = new ExifProfile();

                var bytes = profile.ToByteArray();
                Assert.AreEqual(0, bytes.Length);
            }

            [TestMethod]
            public void ShouldReturnEmptyArrayWhenAllValuesAreInvalid()
            {
                var bytes = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 42, 1, 4, 0, 1, 0, 0, 0, 42, 0, 0, 0, 26, 0, 0, 0, 0, 0 };

                var profile = new ExifProfile(bytes);

                var unkownTag = (ExifTagValue)298;
                var value = profile.GetValue(unkownTag);
                Assert.AreEqual(42U, value.GetValue());
                Assert.AreEqual("42", value.ToString());

                bytes = profile.ToByteArray();
                Assert.AreEqual(0, bytes.Length);
            }

            [TestMethod]
            public void ShouldReturnOriginalDataWhenNotParsed()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetExifProfile();

                    var bytes = profile.ToByteArray();
                    Assert.AreEqual(4706, bytes.Length);
                }
            }

            [TestMethod]
            public void ShouldPreserveTheThumbnail()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetExifProfile();
                    Assert.IsNotNull(profile);

                    var bytes = profile.ToByteArray();

                    profile = new ExifProfile(bytes);

                    using (IMagickImage thumbnail = profile.CreateThumbnail())
                    {
                        Assert.IsNotNull(thumbnail);
                        Assert.AreEqual(128, thumbnail.Width);
                        Assert.AreEqual(85, thumbnail.Height);
                        Assert.AreEqual(MagickFormat.Jpeg, thumbnail.Format);
                    }
                }
            }

            [TestMethod]
            public void ShouldExcludeNullValues()
            {
                var profile = new ExifProfile();
                profile.SetValue(ExifTagValue.ImageDescription, null);

                var data = profile.ToByteArray();

                var reader = new ExifReader();
                var result = reader.Read(data);

                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public void ShouldExcludeEmptyStrings()
            {
                var profile = new ExifProfile();
                profile.SetValue(ExifTagValue.ImageDescription, string.Empty);

                var data = profile.ToByteArray();

                var reader = new ExifReader();
                var result = reader.Read(data);

                Assert.AreEqual(0, result.Count);
            }
        }
    }
}
