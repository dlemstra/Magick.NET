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
    public partial class IConnectedComponentsSettingsExtensionsTests
    {
        public class TheRemoveImageArtifactsMethod
        {
            [Fact]
            public void ShouldRemoveTheAngleThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        AngleThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveTheAreaThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        AreaThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveTheCircularityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        CircularityThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveTheDiameterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        DiameterThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveTheEccentricityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        EccentricityThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveTheMajorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        MajorAxisThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveMeanColor()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        MeanColor = true,
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveTheMinorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        MinorAxisThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveThePerimeterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        PerimeterThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }
        }
    }
}
