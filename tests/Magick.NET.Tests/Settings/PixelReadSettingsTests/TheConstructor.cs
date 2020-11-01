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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PixelReadSettingsTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldSetTheReadSettings()
            {
                var settings = new PixelReadSettings();

                Assert.NotNull(settings.ReadSettings);
            }

            [Fact]
            public void ShouldSetTheMappingCorrectly()
            {
                var settings = new PixelReadSettings(1, 2, StorageType.Int64, PixelMapping.CMYK);

                Assert.NotNull(settings.ReadSettings);
                Assert.Equal(1, settings.ReadSettings.Width);
                Assert.Equal(2, settings.ReadSettings.Height);
                Assert.Equal(StorageType.Int64, settings.StorageType);
                Assert.Equal("CMYK", settings.Mapping);
            }

            [Fact]
            public void ShouldSetTheProperties()
            {
                var settings = new PixelReadSettings(3, 4, StorageType.Quantum, "CMY");

                Assert.NotNull(settings.ReadSettings);
                Assert.Equal(3, settings.ReadSettings.Width);
                Assert.Equal(4, settings.ReadSettings.Height);
                Assert.Equal(StorageType.Quantum, settings.StorageType);
                Assert.Equal("CMY", settings.Mapping);
            }
        }
    }
}
