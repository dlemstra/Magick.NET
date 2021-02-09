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

using System;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ThePdfCoder
    {
        [Fact]
        public void ShouldReadFileMultithreadedCorrectly()
        {
            if (!Ghostscript.IsAvailable)
                return;

            var results = new Task[3];

            for (int i = 0; i < results.Length; ++i)
            {
                results[i] = Task.Run(() =>
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(Files.Coders.CartoonNetworkStudiosLogoAI);

                        Assert.Equal(765, image.Width);
                        Assert.Equal(361, image.Height);
                        Assert.Equal(MagickFormat.Ai, image.Format);
                    }
                });
            }

            for (int i = 0; i < results.Length; ++i)
            {
                results[i].Wait();
            }
        }
    }
}
