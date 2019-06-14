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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheAddProfileMethod
        {
            [TestMethod]
            public void ShouldSetTheIccProperties()
            {
                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    image.AddProfile(ColorProfile.SRGB);

                    Assert.AreEqual("sRGB IEC61966-2.1", image.GetAttribute("icc:description"));
                    Assert.AreEqual("IEC http://www.iec.ch", image.GetAttribute("icc:manufacturer"));
                    Assert.AreEqual("IEC 61966-2.1 Default RGB colour space - sRGB", image.GetAttribute("icc:model"));
                    Assert.AreEqual("Copyright (c) 1998 Hewlett-Packard Company", image.GetAttribute("icc:copyright"));
                }
            }

            [TestMethod]
            public void ShouldUseIccAsTheDefaultProfileName()
            {
                using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
                {
                    var profile = image.GetColorProfile();
                    Assert.IsNull(profile);

                    image.AddProfile(ColorProfile.SRGB);

                    Assert.IsNull(image.GetProfile("icm"));
                }
            }

            [TestMethod]
            public void ShouldUseTheCorrectProfileName()
            {
                using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
                {
                    var profile = image.GetColorProfile();
                    Assert.IsNull(profile);

                    image.AddProfile(new ImageProfile("icm", ColorProfile.SRGB.ToByteArray()));

                    profile = image.GetColorProfile();

                    Assert.IsNotNull(profile);
                    Assert.AreEqual("icm", profile.Name);
                    Assert.AreEqual(3144, profile.ToByteArray().Length);
                }
            }
        }
    }
}
