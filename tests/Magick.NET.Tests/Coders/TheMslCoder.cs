// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheMslCoder
    {
        [Fact]
        public void ShouldBeDisabled()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memStream))
                {
                    writer.Write(@"
                        <?xml version=""1.0"" encoding=""UTF-8""?>
                        <image>
                          <read filename=""/tmp/text.gif"" />
                        </image>");

                    writer.Flush();

                    memStream.Position = 0;

                    using (var image = new MagickImage())
                    {
                        var readSettings = new MagickReadSettings
                        {
                            Format = MagickFormat.Msl,
                        };

                        Assert.Throws<MagickPolicyErrorException>(() =>
                        {
                            image.Read(memStream, readSettings);
                        });
                    }
                }
            }
        }
    }
}