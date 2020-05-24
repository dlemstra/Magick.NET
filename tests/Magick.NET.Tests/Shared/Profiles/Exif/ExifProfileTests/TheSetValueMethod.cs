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

using System;
using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
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
                    using (var image = new MagickImage(Files.ImageMagickJPG))
                    {
                        var profile = image.GetExifProfile();
                        Assert.IsNull(profile);

                        profile = new ExifProfile();
                        profile.SetValue(ExifTag.Copyright, "Dirk Lemstra");

                        image.SetProfile(profile);

                        profile = image.GetExifProfile();
                        Assert.IsNotNull(profile);

                        image.Write(memStream);
                    }

                    memStream.Position = 0;
                    using (var image = new MagickImage(memStream))
                    {
                        var profile = image.GetExifProfile();

                        Assert.IsNotNull(profile);
                        Assert.AreEqual(1, profile.Values.Count());

                        var value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.Copyright);
                        TestValue(value, "Dirk Lemstra");
                    }
                }
            }
        }
    }
}
