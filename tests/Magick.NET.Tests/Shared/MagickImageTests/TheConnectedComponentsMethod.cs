// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheConnectedComponentsMethod
        {
            [TestMethod]
            public void ShouldNotSetTheAttributesWhenOnlyTheComponentsAreSpecified()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    image.ConnectedComponents(4).ToArray();

                    Assert.IsNull(image.GetArtifact("connected-components:area-threshold"));
                    Assert.IsNull(image.GetArtifact("connected-components:mean-color"));
                }
            }

            [TestMethod]
            public void ShouldSetTheAreaThreshold()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        AreaThreshold = 1.5,
                    };
                    image.ConnectedComponents(settings).ToArray();

                    Assert.AreEqual("1.5", image.GetArtifact("connected-components:area-threshold"));
                }
            }

            [TestMethod]
            public void ShouldSetMeanColor()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    var settings = new ConnectedComponentsSettings()
                    {
                        MeanColor = true,
                    };
                    image.ConnectedComponents(settings).ToArray();

                    Assert.AreEqual("true", image.GetArtifact("connected-components:mean-color"));
                }
            }

            [TestMethod]
            public void ShouldReturnTheConnectedComponents()
            {
                using (IMagickImage image = new MagickImage(Files.ConnectedComponentsPNG))
                {
                    using (IMagickImage temp = image.Clone())
                    {
                        temp.Blur(0, 10);
                        temp.Threshold((Percentage)50);

                        var components = temp.ConnectedComponents(4).OrderBy(component => component.X).ToArray();
                        Assert.AreEqual(7, components.Length);

                        var color = MagickColors.Black;

                        AssertComponent(image, components[1], 2, 94, 297, 128, 151, color, 157, 371);
                        AssertComponent(image, components[2], 5, 99, 554, 128, 150, color, 162, 628);
                        AssertComponent(image, components[3], 4, 267, 432, 89, 139, color, 310, 501);
                        AssertComponent(image, components[4], 1, 301, 202, 148, 143, color, 374, 272);
                        AssertComponent(image, components[5], 6, 341, 622, 136, 150, color, 408, 696);
                        AssertComponent(image, components[6], 3, 434, 411, 88, 139, color, 477, 480);
                    }

#if !Q8
                    using (IMagickImage temp = image.Clone())
                    {
                        var settings = new ConnectedComponentsSettings()
                        {
                            Connectivity = 4,
                            MeanColor = true,
                            AreaThreshold = 400,
                        };

                        var components = temp.ConnectedComponents(settings).OrderBy(component => component.X).ToArray();
                        Assert.AreEqual(13, components.Length);

                        var color1 = new MagickColor("#010101010101");
                        var color2 = MagickColors.Black;

                        AssertComponent(image, components[1], 597, 90, 293, 139, 162, color1, 157, 372);
                        AssertComponent(image, components[2], 3439, 96, 550, 138, 162, color1, 162, 628);
                        AssertComponent(image, components[3], 4367, 213, 633, 1, 2, color2, 213, 633);
                        AssertComponent(image, components[4], 4412, 215, 637, 3, 1, color2, 215, 637);
                        AssertComponent(image, components[5], 4453, 217, 641, 3, 1, color2, 217, 641);
                        AssertComponent(image, components[6], 4495, 219, 645, 3, 1, color2, 219, 645);
                        AssertComponent(image, components[7], 4538, 221, 647, 3, 1, color2, 221, 649);
                        AssertComponent(image, components[8], 2105, 268, 433, 89, 139, color1, 311, 502);
                        AssertComponent(image, components[9], 17, 298, 198, 155, 151, color1, 375, 273);
                        AssertComponent(image, components[10], 4202, 337, 618, 148, 158, color1, 409, 696);
                        AssertComponent(image, components[11], 314, 410, 247, 2, 1, color2, 410, 247);
                        AssertComponent(image, components[12], 1703, 434, 411, 88, 140, color1, 477, 480);
                    }
#endif
                }
            }

            private void AssertComponent(IMagickImage image, ConnectedComponent component, int id, int x, int y, int width, int height, MagickColor color, int centroidX, int centroidY)
            {
                var delta = 2;

                Assert.AreEqual(id, component.Id);
                Assert.AreEqual(x, component.X, delta);
                Assert.AreEqual(y, component.Y, delta);
                Assert.AreEqual(width, component.Width, delta);
                Assert.AreEqual(height, component.Height, delta);
                ColorAssert.AreEqual(color, component.Color);
                Assert.AreEqual(centroidX, component.Centroid.X, delta);
                Assert.AreEqual(centroidY, component.Centroid.Y, delta);

                using (IMagickImage area = image.Clone())
                {
                    area.Crop(component.ToGeometry(10));
                    Assert.AreEqual(width + 20, area.Width, delta);
                    Assert.AreEqual(height + 20, area.Height, delta);
                }
            }
        }
    }
}
