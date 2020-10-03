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
    public partial class IDistortSettingsExtensionsTests
    {
        public class TheRemoveImageArtifactsMethod
        {
            [Fact]
            public void ShouldRemoveScale()
            {
                using (var image = new MagickImage())
                {
                    var settings = new DistortSettings()
                    {
                        Scale = 4.2,
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveViewport()
            {
                using (var image = new MagickImage())
                {
                    var settings = new DistortSettings()
                    {
                        Viewport = new MagickGeometry(1, 2, 3, 4),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }
        }
    }
}
