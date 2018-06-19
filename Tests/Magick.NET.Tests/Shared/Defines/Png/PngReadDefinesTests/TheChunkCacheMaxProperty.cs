// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace Magick.NET.Tests.Shared.Defines.Png
{
    public partial class PngReadDefinesTests
    {
        [TestClass]
        public class TheChunkCacheMaxProperty
        {
            [TestMethod]
            public void ShouldSetTheDefine()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Settings.SetDefines(new PngReadDefines()
                    {
                        ChunkCacheMax = 10,
                    });

                    Assert.AreEqual("10", image.Settings.GetDefine(MagickFormat.Png, "chunk-cache-max"));
                }
            }

            [TestMethod]
            public void ShouldLimitTheNumberOfChunks()
            {
                var warning = string.Empty;

                var settings = new MagickReadSettings()
                {
                    Defines = new PngReadDefines()
                    {
                        ChunkCacheMax = 2,
                    },
                };

                using (IMagickImage image = new MagickImage())
                {
                    image.Warning += (object sender, WarningEventArgs e) =>
                    {
                        warning = e.Message;
                    };
                    image.Read(Files.SnakewarePNG, settings);
                }

                StringAssert.Contains(warning, "tEXt: no space in chunk cache");
            }
        }
    }
}
