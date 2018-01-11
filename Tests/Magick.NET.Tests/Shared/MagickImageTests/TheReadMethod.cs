﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheReadMethod
        {
            [TestMethod]
            public void ShouldUseTheCorrectReaderWhenReadingFromStreamAndFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                        {
                            image.Read(stream, settings);
                        }, "ReadPNGImage");
                    }
                }
            }

            [TestMethod]
            public void ShouldUseTheCorrectReaderWhenReadingFromBytesAndFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        image.Read(bytes, settings);
                    }, "ReadPNGImage");
                }
            }

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
        }
    }
}
