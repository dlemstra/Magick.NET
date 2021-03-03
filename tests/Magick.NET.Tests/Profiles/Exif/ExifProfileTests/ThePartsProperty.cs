// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ExifProfileTests
    {
        public class ThePartsProperty
        {
            [Fact]
            public void ShouldFilterTheTagsWhenWritten()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                    {
                        var profile = image.GetExifProfile();
                        Assert.Equal(44, profile.Values.Count());

                        profile.Parts = ExifParts.ExifTags;
                        image.SetProfile(profile);

                        image.Write(memStream);
                    }

                    memStream.Position = 0;
                    using (var image = new MagickImage(memStream))
                    {
                        var profile = image.GetExifProfile();
                        Assert.Equal(24, profile.Values.Count());
                    }
                }
            }
        }
    }
}
