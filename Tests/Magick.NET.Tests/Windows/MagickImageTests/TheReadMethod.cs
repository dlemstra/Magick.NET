// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheReadMethod
        {
            [TestMethod]
            public void ShouldReadAIFromNonSeekableStream()
            {
                using (NonSeekableStream stream = new NonSeekableStream(Files.Coders.CartoonNetworkStudiosLogoAI))
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(stream);
                    }
                }
            }

            [TestMethod]
            public void ShouldUseTheReadSettings()
            {
                using (IMagickImage image = new MagickImage())
                {
                    using (FileStream fs = File.OpenRead(Files.Logos.MagickNETSVG))
                    {
                        byte[] buffer = new byte[fs.Length + 1];
                        fs.Read(buffer, 0, (int)fs.Length);

                        using (MemoryStream memStream = new MemoryStream(buffer, 0, (int)fs.Length))
                        {
                            image.Read(memStream, new MagickReadSettings()
                            {
                                Density = new Density(72),
                            });

                            ColorAssert.AreEqual(new MagickColor("#231f20"), image, 129, 101);
                        }
                    }
                }
            }
        }
    }
}

#endif