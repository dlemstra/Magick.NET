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

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheWriteMethod
        {
            [TestMethod]
            public void ShouldUseTheFileExtension()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (IMagickImageCollection input = new MagickImageCollection(Files.CirclePNG, readSettings))
                {
                    using (var tempFile = new TemporaryFile(".jpg"))
                    {
                        input.Write(tempFile);

                        using (IMagickImageCollection output = new MagickImageCollection(tempFile))
                        {
                            Assert.AreEqual(MagickFormat.Jpeg, output[0].Format);
                        }
                    }
                }
            }
        }
    }
}
