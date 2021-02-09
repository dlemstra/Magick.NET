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
    public partial class PngReadDefinesTests
    {
        public class TheChunkCacheMaxProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PngReadDefines
                    {
                        ChunkCacheMax = 10,
                    });

                    Assert.Equal("10", image.Settings.GetDefine(MagickFormat.Png, "chunk-cache-max"));
                }
            }

            [Fact]
            public void ShouldLimitTheNumberOfChunks()
            {
                var warning = string.Empty;

                var settings = new MagickReadSettings
                {
                    Defines = new PngReadDefines
                    {
                        ChunkCacheMax = 2,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Warning += (object sender, WarningEventArgs e) =>
                    {
                        warning = e.Message;
                    };
                    image.Read(Files.SnakewarePNG, settings);
                }

                Assert.Contains("tEXt: no space in chunk cache", warning);
            }
        }
    }
}
