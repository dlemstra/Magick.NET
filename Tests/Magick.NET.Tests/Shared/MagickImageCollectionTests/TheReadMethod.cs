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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheReadMethod
        {
            [TestMethod]
            public void ShouldResetTheFormatAfterReadingFile()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (IMagickImageCollection input = new MagickImageCollection(Files.CirclePNG, readSettings))
                {
                    Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                }
            }

            [TestMethod]
            public void ShouldResetTheFormatAfterReadingStream()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (var stream = File.OpenRead(Files.CirclePNG))
                {
                    using (IMagickImageCollection input = new MagickImageCollection(stream, readSettings))
                    {
                        Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            [TestMethod]
            public void ShouldResetTheFormatAfterReadingBytes()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                var bytes = File.ReadAllBytes(Files.CirclePNG);

                using (IMagickImageCollection input = new MagickImageCollection(bytes, readSettings))
                {
                    Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                }
            }
        }
    }
}
