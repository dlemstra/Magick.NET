// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ExifProfileTests
    {
        public class TheSetValueMethod
        {
            [Fact]
            public void ShouldUpdateTheDataInTheProfile()
            {
                using (var memStream = new MemoryStream())
                {
                    using (var image = new MagickImage(Files.ImageMagickJPG))
                    {
                        var profile = image.GetExifProfile();
                        Assert.Null(profile);

                        profile = new ExifProfile();
                        profile.SetValue(ExifTag.Copyright, "Dirk Lemstra");

                        image.SetProfile(profile);

                        profile = image.GetExifProfile();
                        Assert.NotNull(profile);

                        image.Write(memStream);
                    }

                    memStream.Position = 0;
                    using (var image = new MagickImage(memStream))
                    {
                        var profile = image.GetExifProfile();

                        Assert.NotNull(profile);
                        Assert.Single(profile.Values);

                        var value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.Copyright);
                        TestValue(value, "Dirk Lemstra");
                    }
                }
            }
        }
    }
}
