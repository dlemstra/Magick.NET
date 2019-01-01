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

using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class PdfReadDefinesTests
    {
        [TestClass]
        public class ThePasswordProperty
        {
            [TestMethod]
            public void ShouldSetThePassword()
            {
                MagickReadSettings settings = new MagickReadSettings()
                {
                    Defines = new PdfReadDefines()
                    {
                        Password = "test",
                    },
                };

                using (IMagickImage image = new MagickImage())
                {
                    image.Read(Files.Coders.PdfExamplePasswordOriginalPDF, settings);
                }
            }

            [TestMethod]
            public void ShouldNotBeAbleToOpenFileWithNullPassword()
            {
                MagickReadSettings settings = new MagickReadSettings()
                {
                    Defines = new PdfReadDefines()
                    {
                        Password = null,
                    },
                };

                using (IMagickImage image = new MagickImage())
                {
                    try
                    {
                        image.Read(Files.Coders.PdfExamplePasswordOriginalPDF, settings);
                    }
                    catch (MagickDelegateErrorException exception)
                    {
                        var message = exception.Message;

                        var relatedException = exception.RelatedExceptions.FirstOrDefault();
                        if (relatedException != null)
                            message += relatedException.Message;

                        StringAssert.Contains(message, "This file requires a password for access.");
                        return;
                    }

                    Assert.Fail("Exception should be thrown.");
                }
            }
        }
    }
}

#endif