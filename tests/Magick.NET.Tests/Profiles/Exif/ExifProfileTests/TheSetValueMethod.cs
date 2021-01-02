// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ExifProfileTests
    {
        public class TheSetValueMethod
        {
            [Fact]
            public void ShouldUpdateTheDataInTheProfile()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var image = new MagickImage(Files.ImageMagickJPG))
                    {
                        var profile = image.GetExifProfile();
                        Assert.Null(profile);

                        profile = new ExifProfile();
                        profile.SetValue(ExifTag.Copyright, "Dirk Lemstra");

                        image.SetProfile(profile);

                        profile = image.GetExifProfile();
                        Assert.NotNull(profile);

                        image.Write(memStream);
                    }

                    memStream.Position = 0;
                    using (var image = new MagickImage(memStream))
                    {
                        var profile = image.GetExifProfile();

                        Assert.NotNull(profile);
                        Assert.Single(profile.Values);

                        var value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.Copyright);
                        TestValue(value, "Dirk Lemstra");
                    }
                }
            }
        }
    }
}
