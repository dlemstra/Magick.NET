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

#if Q8
                        AssertComponent(image, components[1], 2, 94, 297, 128, 151, 11783, color, 157, 371);
                        AssertComponent(image, components[2], 5, 99, 554, 128, 150, 11772, color, 162, 628);
                        AssertComponent(image, components[3], 4, 267, 432, 89, 139, 11792, color, 310, 501);
                        AssertComponent(image, components[4], 1, 301, 202, 148, 143, 11801, color, 374, 272);
                        AssertComponent(image, components[5], 6, 341, 622, 136, 150, 11793, color, 408, 696);
                        AssertComponent(image, components[6], 3, 434, 411, 88, 139, 11835, color, 477, 480);
#else
                        AssertComponent(image, components[1], 2, 94, 297, 128, 151, 11737, color, 157, 371);
                        AssertComponent(image, components[2], 5, 99, 554, 128, 150, 11734, color, 162, 628);
                        AssertComponent(image, components[3], 4, 267, 432, 89, 139, 11749, color, 310, 501);
                        AssertComponent(image, components[4], 1, 301, 202, 148, 143, 11755, color, 374, 272);
                        AssertComponent(image, components[5], 6, 341, 622, 136, 150, 11746, color, 408, 696);
                        AssertComponent(image, components[6], 3, 434, 411, 88, 139, 11793, color, 477, 480);
#endif
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
                        Assert.AreEqual(12, components.Length);

                        var color = new MagickColor("#010101010101");

                        AssertComponent(image, components[1], 597, 90, 293, 136, 162, 11624, color, 157, 372);
                        AssertComponent(image, components[2], 3439, 96, 550, 138, 162, 11739, color, 162, 628);
                        AssertComponent(image, components[3], 4122, 103, 604, 4, 2, 4, new MagickColor("#0B0B0B0B0B0B"), 104, 606);
                        AssertComponent(image, components[4], 4157, 107, 612, 3, 1, 4, new MagickColor("#080808080808"), 108, 613);
                        AssertComponent(image, components[5], 4233, 111, 620, 3, 1, 4, new MagickColor("#020202020202"), 112, 621);
                        AssertComponent(image, components[6], 5085, 150, 698, 3, 1, 4, new MagickColor("#424242424242"), 150, 698);
                        AssertComponent(image, components[7], 5132, 152, 702, 3, 1, 4, new MagickColor("#262626262626"), 153, 703);
                        AssertComponent(image, components[8], 2105, 268, 433, 89, 139, 11645, color, 311, 502);
                        AssertComponent(image, components[9], 17, 298, 198, 155, 151, 11622, color, 375, 273);
                        AssertComponent(image, components[10], 4202, 337, 618, 144, 158, 11675, color, 409, 696);
                        AssertComponent(image, components[11], 1703, 435, 412, 87, 138, 11629, color, 478, 481);
                    }
#endif
                }
            }

            private void AssertComponent(IMagickImage image, ConnectedComponent component, int id, int x, int y, int width, int height, int area, IMagickColor color, int centroidX, int centroidY)
            {
                var delta = 2;

                Assert.AreEqual(id, component.Id);
                Assert.AreEqual(x, component.X, delta);
                Assert.AreEqual(y, component.Y, delta);
                Assert.AreEqual(width, component.Width, delta);
                Assert.AreEqual(height, component.Height, delta);
                Assert.AreEqual(area, component.Area, delta);
                ColorAssert.AreEqual(color, component.Color);
                Assert.AreEqual(centroidX, component.Centroid.X, delta);
                Assert.AreEqual(centroidY, component.Centroid.Y, delta);

                using (IMagickImage componentImage = image.Clone())
                {
                    componentImage.Crop(component.ToGeometry(10));
                    Assert.AreEqual(width + 20, componentImage.Width, delta);
                    Assert.AreEqual(height + 20, componentImage.Height, delta);
                }
            }
        }
    }
}
