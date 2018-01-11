﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
        public class TheCreateThumbnailMethod
        {
            [TestMethod]
            public void ShouldCreateImage()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetExifProfile();
                    Assert.IsNotNull(profile);

                    using (IMagickImage thumbnail = profile.CreateThumbnail())
                    {
                        Assert.IsNotNull(thumbnail);
                        Assert.AreEqual(128, thumbnail.Width);
                        Assert.AreEqual(85, thumbnail.Height);
                        Assert.AreEqual(MagickFormat.Jpeg, thumbnail.Format);
                    }
                }
            }
        }
    }
}
