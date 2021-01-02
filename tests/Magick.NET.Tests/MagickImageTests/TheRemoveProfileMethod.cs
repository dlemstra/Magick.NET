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
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheRemoveProfileMethod
        {
            public class WithImageProfile
            {
                [Fact]
                public void ShouldThrowExceptionWhenProfileIsNull()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentNullException>("profile", () => image.RemoveProfile((IImageProfile)null));
                    }
                }

                [Fact]
                public void ShouldRemoveTheProfile()
                {
                    using (var image = new MagickImage(Files.PictureJPG))
                    {
                        var profile = image.GetColorProfile();
                        Assert.NotNull(profile);

                        image.RemoveProfile(profile);

                        profile = image.GetColorProfile();
                        Assert.Null(profile);
                    }
                }
            }

            public class WithString
            {
                [Fact]
                public void ShouldThrowExceptionWhenProfileIsNull()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentNullException>("name", () => image.RemoveProfile((string)null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenProfileIsEmpty()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentException>("name", () => image.RemoveProfile(string.Empty));
                    }
                }

                [Fact]
                public void ShouldRemoveTheProfile()
                {
                    using (var image = new MagickImage(Files.PictureJPG))
                    {
                        var profile = image.GetColorProfile();
                        Assert.NotNull(profile);
                        Assert.Equal("icc", profile.Name);

                        image.RemoveProfile(profile.Name);

                        profile = image.GetColorProfile();
                        Assert.Null(profile);
                    }
                }
            }
        }
    }
}
