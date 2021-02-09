// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifValueTests
    {
        public class TheValueProperty
        {
            [Fact]
            public void ShouldReturnFalseWhenValueIsInvalidDataType1()
            {
                var profile = new ExifProfile();
                profile.SetValue(ExifTag.Software, "Magick.NET");

                IExifValue value = profile.GetValue(ExifTag.Software);

                Assert.False(value.SetValue(10.5));
            }

            [Fact]
            public void ShouldReturnFalseWhenValueIsInvalidDataType2()
            {
                var profile = new ExifProfile();
                profile.SetValue(ExifTag.ShutterSpeedValue, new SignedRational(75.55));

                IExifValue value = profile.GetValue(ExifTag.ShutterSpeedValue);

                Assert.False(value.SetValue(75));
            }

            [Fact]
            public void ShouldReturnFalseWhenValueIsInvalidDataType3()
            {
                var profile = new ExifProfile();
                profile.SetValue(ExifTag.XResolution, new Rational(150.0));

                IExifValue value = profile.GetValue(ExifTag.XResolution);
                Assert.NotNull(value);
                Assert.Equal("150", value.ToString());

                Assert.False(value.SetValue("Magick.NET"));
            }
        }
    }
}
