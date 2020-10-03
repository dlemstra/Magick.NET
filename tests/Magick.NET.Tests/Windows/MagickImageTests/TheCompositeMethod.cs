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

#if WINDOWS_BUILD

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheCompositeMethod
        {
            public partial class WithImageAndCompositeOperator
            {
                [Fact]
                public void ShouldCopyTheAlphaChannelWithCopyAlpha()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        BackgroundColor = MagickColors.None,
                        FillColor = MagickColors.White,
                        FontPointsize = 100,
                    };

                    using (var image = new MagickImage("label:Test", readSettings))
                    {
                        using (var alpha = image.Clone())
                        {
                            alpha.Alpha(AlphaOption.Extract);
                            alpha.Shade(130, 30);
                            alpha.Composite(image, CompositeOperator.CopyAlpha);

                            ColorAssert.Equal(new MagickColor("#7fff7fff7fff0000"), alpha, 0, 0);
                            ColorAssert.Equal(new MagickColor("#7fff7fff7fffffff"), alpha, 30, 30);
                        }
                    }
                }
            }
        }
    }
}

#endif