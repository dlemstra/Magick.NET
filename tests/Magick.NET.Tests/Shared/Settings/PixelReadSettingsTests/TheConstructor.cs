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
    public partial class PixelReadSettingsTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldSetTheReadSettings()
            {
                var settings = new PixelReadSettings();

                Assert.IsNotNull(settings.ReadSettings);
            }

            [TestMethod]
            public void ShouldSetTheMappingCorrectly()
            {
                var settings = new PixelReadSettings(1, 2, StorageType.Int64, PixelMapping.CMYK);

                Assert.IsNotNull(settings.ReadSettings);
                Assert.AreEqual(1, settings.ReadSettings.Width);
                Assert.AreEqual(2, settings.ReadSettings.Height);
                Assert.AreEqual(StorageType.Int64, settings.StorageType);
                Assert.AreEqual("CMYK", settings.Mapping);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var settings = new PixelReadSettings(3, 4, StorageType.Quantum, "CMY");

                Assert.IsNotNull(settings.ReadSettings);
                Assert.AreEqual(3, settings.ReadSettings.Width);
                Assert.AreEqual(4, settings.ReadSettings.Height);
                Assert.AreEqual(StorageType.Quantum, settings.StorageType);
                Assert.AreEqual("CMY", settings.Mapping);
            }
        }
    }
}
