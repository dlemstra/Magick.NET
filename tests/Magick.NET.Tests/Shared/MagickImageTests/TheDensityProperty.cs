// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheDensityProperty
        {
            [TestMethod]
            public void ShouldNotChangeWhenValueIsNull()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    Assert.AreEqual(300, image.Density.X);

                    image.Density = null;

                    Assert.AreEqual(300, image.Density.X);
                }
            }

            [TestMethod]
            public void ShouldUpdateExifProfile()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                    {
                        var profile = image.GetExifProfile();
                        var value = profile.GetValue(ExifTag.XResolution);
                        Assert.AreEqual("300", value.ToString());

                        image.Density = new Density(72);

                        image.Write(memStream);
                    }

                    memStream.Position = 0;
                    using (var image = new MagickImage(memStream))
                    {
                        var profile = image.GetExifProfile();
                        var value = profile.GetValue(ExifTag.XResolution);

                        Assert.AreEqual("72", value.ToString());
                    }
                }
            }
        }
    }
}
