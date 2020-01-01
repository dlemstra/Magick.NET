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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class TiffReadDefinesTests
    {
        [TestClass]
        public class TheIgnoreExifPopertiesProperty
        {
            [TestMethod]
            public void ShouldSetTheDefine()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffReadDefines()
                    {
                        IgnoreExifPoperties = true,
                    });

                    Assert.AreEqual("False", image.Settings.GetDefine(MagickFormat.Tiff, "exif-properties"));
                }
            }

            [TestMethod]
            public void ShouldNotSetTheDefineWhenTheValueIsFalse()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffReadDefines()
                    {
                        IgnoreExifPoperties = false,
                    });

                    Assert.IsNull(image.Settings.GetDefine(MagickFormat.Tiff, "exif-properties"));
                }
            }

            [TestMethod]
            public void ShouldIgnoreTheExifProperties()
            {
                MagickReadSettings settings = new MagickReadSettings()
                {
                    Defines = new TiffReadDefines()
                    {
                        IgnoreExifPoperties = true,
                    },
                };

                using (IMagickImage image = new MagickImage())
                {
                    image.Read(Files.InvitationTIF);
                    Assert.IsNotNull(image.GetAttribute("exif:PixelXDimension"));

                    image.Read(Files.InvitationTIF, settings);
                    Assert.IsNull(image.GetAttribute("exif:PixelXDimension"));
                }
            }
        }
    }
}
