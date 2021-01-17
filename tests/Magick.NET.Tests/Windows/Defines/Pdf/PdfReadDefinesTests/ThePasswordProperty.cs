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

#if WINDOWS_BUILD

using System.Linq;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests
{
    public partial class PdfReadDefinesTests
    {
        public class ThePasswordProperty
        {
            [Fact]
            public void ShouldSetTheDefineWhenValueIsSet()
            {
                using (var image = new MagickImage(MagickColors.Magenta, 1, 1))
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        Password = "test",
                    });

                    Assert.Equal("test", image.Settings.GetDefine("authenticate"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenValueIsNotSet()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PdfReadDefines
                    {
                        Password = null,
                    });

                    Assert.Null(image.Settings.GetDefine("authenticate"));
                }
            }

            [Fact]
            public void ShouldUseThePasswordToReadTheImage()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new PdfReadDefines
                    {
                        Password = "test",
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.Coders.PdfExamplePasswordOriginalPDF, settings);
                }
            }

            [Fact]
            public void ShouldNotBeAbleToOpenFileWithNullPassword()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new PdfReadDefines
                    {
                        Password = null,
                    },
                };

                using (var image = new MagickImage())
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

                        Assert.Contains("This file requires a password for access.", message);
                        return;
                    }

                    throw new XunitException("Exception should be thrown.");
                }
            }
        }
    }
}

#endif