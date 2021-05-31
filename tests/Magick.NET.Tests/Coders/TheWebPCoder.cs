// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheWebPCoder
    {
        [Fact]
        public void ShouldReadAndWriteTheXmpProfile()
        {
            var xmpData = Encoding.UTF8.GetBytes(@"<x:xmpmeta xmlns:x=""adobe:ns:meta/"" x:xmptk=""XMPTk 2.8""><rdf:RDF xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""></rdf:RDF></x:xmpmeta>");

            using (var input = new MagickImage(Files.Builtin.Logo))
            {
                input.SetProfile(new XmpProfile(xmpData));

                var data = input.ToByteArray(MagickFormat.WebP);

                using (var output = new MagickImage(data))
                {
                    var profile = output.GetXmpProfile();

                    Assert.NotNull(profile);
                    Assert.Equal(xmpData, profile.ToByteArray());
                }
            }
        }
    }
}