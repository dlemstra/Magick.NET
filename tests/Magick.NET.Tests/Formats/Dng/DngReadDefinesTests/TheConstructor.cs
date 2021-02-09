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
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DngReadDefinesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldNotSetAnyDefines()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new DngReadDefines());

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dng, "use_camera_wb"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dng, "use_auto_wb"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dng, "no_auto_bright"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dng, "output_color"));
                }
            }
        }
    }
}
