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
    public partial class ExifProfileTests
    {
        [TestClass]
        public class ThePartsProperty
        {
            [TestMethod]
            public void ShouldFilterTheTagsWhenWritten()
            {
                using (var memStream = new MemoryStream())
                {
                    using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                    {
                        var profile = image.GetExifProfile();
                        Assert.AreEqual(44, profile.Values.Count());

                        profile.Parts = ExifParts.ExifTags;
                        image.AddProfile(profile);

                        image.Write(memStream);
                    }

                    memStream.Position = 0;
                    using (IMagickImage image = new MagickImage(memStream))
                    {
                        var profile = image.GetExifProfile();
                        Assert.AreEqual(24, profile.Values.Count());
                    }
                }
            }
        }
    }
}
