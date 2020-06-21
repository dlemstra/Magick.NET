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
    public partial class IConnectedComponentsSettingsExtensionsTests
    {
        [TestClass]
        public class TheSetImageArtifactsMethod
        {
            [TestMethod]
            public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings();

                    settings.SetImageArtifacts(image);

                    Assert.IsNull(image.GetArtifact("connected-components:angle-threshold"));
                    Assert.IsNull(image.GetArtifact("connected-components:area-threshold"));
                    Assert.IsNull(image.GetArtifact("connected-components:circularity-threshold"));
                    Assert.IsNull(image.GetArtifact("connected-components:diameter-threshold"));
                    Assert.IsNull(image.GetArtifact("connected-components:eccentricity-threshold"));
                    Assert.IsNull(image.GetArtifact("connected-components:major-axis-threshold"));
                    Assert.IsNull(image.GetArtifact("connected-components:mean-color"));
                    Assert.IsNull(image.GetArtifact("connected-components:minor-axis-threshold"));
                    Assert.IsNull(image.GetArtifact("connected-components:perimeter-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheAngleThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        AngleThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.5", image.GetArtifact("connected-components:angle-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMinumunAndMaximumAngleThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        AngleThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.2-3.4", image.GetArtifact("connected-components:angle-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheAreaThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        AreaThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.5", image.GetArtifact("connected-components:area-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMinumunAndMaximumAreaThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        AreaThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.2-3.4", image.GetArtifact("connected-components:area-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheCircularityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        CircularityThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.5", image.GetArtifact("connected-components:circularity-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMinumunAndMaximumCircularityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        CircularityThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.2-3.4", image.GetArtifact("connected-components:circularity-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheDiameterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        DiameterThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.5", image.GetArtifact("connected-components:diameter-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMinumunAndMaximumDiameterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        DiameterThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.2-3.4", image.GetArtifact("connected-components:diameter-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheEccentricityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        EccentricityThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.5", image.GetArtifact("connected-components:eccentricity-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMinumunAndMaximumEccentricityThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        EccentricityThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.2-3.4", image.GetArtifact("connected-components:eccentricity-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMajorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        MajorAxisThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.5", image.GetArtifact("connected-components:major-axis-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMinumunAndMaximumMajorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        MajorAxisThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.2-3.4", image.GetArtifact("connected-components:major-axis-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetMeanColor()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        MeanColor = true,
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("true", image.GetArtifact("connected-components:mean-color"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMinorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        MinorAxisThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.5", image.GetArtifact("connected-components:minor-axis-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMinumunAndMaximumMinorAxisThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        MinorAxisThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.2-3.4", image.GetArtifact("connected-components:minor-axis-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetThePerimeterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        PerimeterThreshold = new Threshold(1.5),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.5", image.GetArtifact("connected-components:perimeter-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetTheMinumunAndMaximumPerimeterThreshold()
            {
                using (var image = new MagickImage())
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        PerimeterThreshold = new Threshold(1.2, 3.4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.AreEqual("1.2-3.4", image.GetArtifact("connected-components:perimeter-threshold"));
                }
            }
        }
    }
}
