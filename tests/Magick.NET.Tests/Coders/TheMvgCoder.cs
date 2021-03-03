// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheMvgCoder
    {
        [Fact]
        public void ShouldBeDisabled()
        {
            using (var memStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memStream))
                {
                    writer.Write(@"push graphic-context
                      viewbox 0 0 640 480
                      image over 0,0 0,0 ""label:Magick.NET""
                      pop graphic-context");

                    writer.Flush();

                    memStream.Position = 0;

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<MagickMissingDelegateErrorException>(() =>
                        {
                            image.Read(memStream);
                        });

                        memStream.Position = 0;

                        Assert.Throws<MagickPolicyErrorException>(() =>
                        {
                            var settings = new MagickReadSettings
                            {
                                Format = MagickFormat.Mvg,
                            };

                            image.Read(memStream, settings);
                        });
                    }
                }
            }
        }
    }
}