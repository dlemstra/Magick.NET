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

namespace Magick.NET.Tests
{
    public partial class IConnectedComponentsSettingsExtensionsTests
    {
        public class TheSetImageArtifactsMethod
        {
            [Fact]
            public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings();

                    settings.SetImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldSetTheAngleThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        AngleThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.5", image.GetArtifact("connected-components:angle-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheMinumunAndMaximumAngleThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        AngleThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:angle-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheAreaThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        AreaThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.5", image.GetArtifact("connected-components:area-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheMinumunAndMaximumAreaThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        AreaThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:area-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheCircularityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        CircularityThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.5", image.GetArtifact("connected-components:circularity-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheMinumunAndMaximumCircularityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        CircularityThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:circularity-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheDiameterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        DiameterThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.5", image.GetArtifact("connected-components:diameter-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheMinumunAndMaximumDiameterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        DiameterThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:diameter-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheEccentricityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        EccentricityThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.5", image.GetArtifact("connected-components:eccentricity-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheMinumunAndMaximumEccentricityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        EccentricityThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:eccentricity-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheMajorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        MajorAxisThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.5", image.GetArtifact("connected-components:major-axis-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheMinumunAndMaximumMajorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        MajorAxisThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:major-axis-threshold"));
                }
            }

            [Fact]
            public void ShouldSetMeanColor()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        MeanColor = true,
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("true", image.GetArtifact("connected-components:mean-color"));
                }
            }

            [Fact]
            public void ShouldSetTheMinorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        MinorAxisThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.5", image.GetArtifact("connected-components:minor-axis-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheMinumunAndMaximumMinorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        MinorAxisThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:minor-axis-threshold"));
                }
            }

            [Fact]
            public void ShouldSetThePerimeterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        PerimeterThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.5", image.GetArtifact("connected-components:perimeter-threshold"));
                }
            }

            [Fact]
            public void ShouldSetTheMinumunAndMaximumPerimeterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings
                    {
                        PerimeterThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:perimeter-threshold"));
                }
            }
        }
    }
}
