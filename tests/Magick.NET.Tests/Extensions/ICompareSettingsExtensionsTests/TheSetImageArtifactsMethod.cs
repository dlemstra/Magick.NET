// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public partial class ICompareSettingsExtensionsTests
    {
        public class TheSetImageArtifactsMethod : ICompareSettingsExtensionsTests
        {
            [Fact]
            public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    var settings = new CompareSettings();

                    settings.SetImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldSetTheHighlightColor()
            {
                using (var image = new MagickImage())
                {
                    var settings = new CompareSettings
                    {
                        HighlightColor = MagickColors.Magenta,
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);

#if Q8
                    Assert.Equal("#FF00FFFF", image.GetArtifact("compare:highlight-color"));
#else
                    Assert.Equal("#FFFF0000FFFFFFFF", image.GetArtifact("compare:highlight-color"));
#endif
                }
            }

            [Fact]
            public void ShouldSetTheLowlightColor()
            {
                using (var image = new MagickImage())
                {
                    var settings = new CompareSettings
                    {
                        LowlightColor = MagickColors.Magenta,
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);

#if Q8
                    Assert.Equal("#FF00FFFF", image.GetArtifact("compare:lowlight-color"));
#else
                    Assert.Equal("#FFFF0000FFFFFFFF", image.GetArtifact("compare:lowlight-color"));
#endif
                }
            }

            [Fact]
            public void ShouldSetTheMasklightColor()
            {
                using (var image = new MagickImage())
                {
                    var settings = new CompareSettings
                    {
                        MasklightColor = MagickColors.Magenta,
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);

#if Q8
                    Assert.Equal("#FF00FFFF", image.GetArtifact("compare:masklight-color"));
#else
                    Assert.Equal("#FFFF0000FFFFFFFF", image.GetArtifact("compare:masklight-color"));
#endif
                }
            }
        }
    }
}
