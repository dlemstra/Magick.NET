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
using ImageMagick.Formats.Png;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class PngReadDefinesTests
    {
        [TestClass]
        public class TheChunkMallocMaxProperty
        {
            [TestMethod]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PngReadDefines()
                    {
                        ChunkMallocMax = 20,
                    });

                    Assert.AreEqual("20", image.Settings.GetDefine(MagickFormat.Png, "chunk-malloc-max"));
                }
            }

            [TestMethod]
            public void ShouldLimitTheChunkSize()
            {
                var settings = new MagickReadSettings()
                {
                    Defines = new PngReadDefines()
                    {
                        ChunkMallocMax = 2,
                    },
                };

                var exception = ExceptionAssert.Throws<MagickCoderErrorException>(() =>
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(Files.SnakewarePNG, settings);
                    }
                });

                StringAssert.Contains(exception.Message, "IHDR: chunk data is too large");
            }
        }
    }
}
