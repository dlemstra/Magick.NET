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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSetProfileMethod
        {
            [TestClass]
            public class WithColorProfile
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenProfileIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("profile", () => image.SetProfile(null));
                    }
                }

                [TestMethod]
                public void ShouldSetTheIccProperties()
                {
                    using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.SetProfile(ColorProfile.SRGB);

                        Assert.AreEqual("sRGB IEC61966-2.1", image.GetAttribute("icc:description"));
                        Assert.AreEqual("IEC http://www.iec.ch", image.GetAttribute("icc:manufacturer"));
                        Assert.AreEqual("IEC 61966-2.1 Default RGB colour space - sRGB", image.GetAttribute("icc:model"));
                        Assert.AreEqual("Copyright (c) 1998 Hewlett-Packard Company", image.GetAttribute("icc:copyright"));
                    }
                }

                [TestMethod]
                public void ShouldUseIccAsTheDefaultColorProfileName()
                {
                    using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
                    {
                        var profile = image.GetColorProfile();
                        Assert.IsNull(profile);

                        image.SetProfile(ColorProfile.SRGB);

                        Assert.IsTrue(image.HasProfile("icc"));
                        Assert.IsFalse(image.HasProfile("icm"));
                    }
                }

                [TestMethod]
                public void ShouldUseTheCorrectProfileName()
                {
                    using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
                    {
                        var profile = image.GetColorProfile();
                        Assert.IsNull(profile);

                        image.SetProfile(new ImageProfile("icm", ColorProfile.SRGB.ToByteArray()));

                        profile = image.GetColorProfile();

                        Assert.IsNotNull(profile);
                        Assert.AreEqual("icm", profile.Name);
                        Assert.AreEqual(3144, profile.ToByteArray().Length);
                    }
                }

                [TestMethod]
                public void ShouldOverwriteExistingProfile()
                {
                    using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
                    {
                        image.SetProfile(ColorProfile.SRGB);

                        image.SetProfile(ColorProfile.AppleRGB);

                        var profile = image.GetColorProfile();
                        Assert.IsNotNull(profile);
                        Assert.AreEqual(ColorProfile.AppleRGB.ToByteArray().Length, profile.ToByteArray().Length);
                    }
                }

                [TestMethod]
                public void ShouldUseTheSpecifiedMode()
                {
                    using (IMagickImage quantumImage = new MagickImage(Files.PictureJPG))
                    {
                        quantumImage.SetProfile(ColorProfile.USWebCoatedSWOP);

                        using (IMagickImage highResImage = new MagickImage(Files.PictureJPG))
                        {
                            highResImage.SetProfile(ColorProfile.USWebCoatedSWOP, ColorTransformMode.HighRes);

                            var difference = quantumImage.Compare(highResImage, ErrorMetric.RootMeanSquared);

#if Q16HDRI
                            Assert.AreEqual(0.0, difference);
#else
                            Assert.AreNotEqual(0.0, difference);
#endif
                        }
                    }
                }
            }

            [TestClass]
            public class WithInterface
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenProfileIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("profile", () => image.SetProfile((IImageProfile)null));
                    }
                }

                [TestMethod]
                public void ShouldNotSetTheProfileWhenArrayIsNull()
                {
                    using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
                    {
                        image.SetProfile(new TestImageProfile("foo", null));

                        Assert.IsFalse(image.HasProfile("foo"));
                    }
                }

                [TestMethod]
                public void ShouldNotSetTheProfileWhenArrayIsEmpty()
                {
                    using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
                    {
                        image.SetProfile(new TestImageProfile("foo", new byte[0]));

                        Assert.IsFalse(image.HasProfile("foo"));
                    }
                }

                [TestMethod]
                public void ShouldOverwriteExistingProfile()
                {
                    var profileA = new TestImageProfile("foo", new byte[1]);
                    var profileB = new TestImageProfile("foo", new byte[2]);

                    using (IMagickImage image = new MagickImage(Files.SnakewarePNG))
                    {
                        image.SetProfile(profileA);

                        image.SetProfile(profileB);

                        var profile = image.GetProfile("foo");
                        Assert.IsNotNull(profile);
                        Assert.AreEqual(2, profile.ToByteArray().Length);
                    }
                }

                [TestMethod]
                public void ShouldSetTheIptcProfile()
                {
                    using (IMagickImage input = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                    {
                        var profile = input.GetIptcProfile();

                        profile.SetValue(IptcTag.ReferenceDate, new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.Zero));

                        // Remove the 8bim profile so we can overwrite the iptc profile.
                        input.RemoveProfile("8bim");
                        input.SetProfile(profile);

                        using (var memStream = new MemoryStream())
                        {
                            input.Write(memStream);
                            memStream.Position = 0;

                            using (IMagickImage output = new MagickImage(memStream))
                            {
                                profile = input.GetIptcProfile();

                                Assert.IsNotNull(profile);
                                Assert.AreEqual("20200102", profile.GetValue(IptcTag.ReferenceDate).Value);
                            }
                        }
                    }
                }
            }
        }
    }
}
