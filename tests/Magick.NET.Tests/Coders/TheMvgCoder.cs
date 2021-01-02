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

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheMvgCoder
    {
        [Fact]
        public void ShouldBeDisabled()
        {
            using (var memStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memStream))
                {
                    writer.Write(@"push graphic-context
                      viewbox 0 0 640 480
                      image over 0,0 0,0 ""label:Magick.NET""
                      pop graphic-context");

                    writer.Flush();

                    memStream.Position = 0;

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<MagickMissingDelegateErrorException>(() =>
                        {
                            image.Read(memStream);
                        });

                        memStream.Position = 0;

                        Assert.Throws<MagickPolicyErrorException>(() =>
                        {
                            var settings = new MagickReadSettings
                            {
                                Format = MagickFormat.Mvg,
                            };

                            image.Read(memStream, settings);
                        });
                    }
                }
            }
        }
    }
}