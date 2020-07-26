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
    public partial class IComplexSettingsExtensionsTests
    {
        [TestClass]
        public class TheSetImageArtifactsMethod
        {
            [TestMethod]
            public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ComplexSettings();

                    settings.SetImageArtifacts(image);

                    Assert.IsNull(image.GetArtifact("complex:snr"));
                }
            }

            [TestMethod]
            public void ShouldSetTheSignalToNoiseRatio()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ComplexSettings()
                    {
                        SignalToNoiseRatio = 1.2,
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.2", image.GetArtifact("complex:snr"));
                }
            }
        }
    }
}
