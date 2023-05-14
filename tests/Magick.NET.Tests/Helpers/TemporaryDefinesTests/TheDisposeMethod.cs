// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class TemporaryDefinesTests
{
    public class TheDisposeMethod
    {
        [Fact]
        public void ShouldRemoveArtifactsThatWereSet()
        {
            static void SetTemporaryArtifact(MagickImage image)
            {
                using var temporaryDefines = new TemporaryDefines(image);
                temporaryDefines.SetArtifact("bar", "foo");
            }

            using var image = new MagickImage();
            image.SetArtifact("foo", "bar");
            SetTemporaryArtifact(image);

            Assert.Null(image.GetArtifact("bar"));
            Assert.Equal("bar", image.GetArtifact("foo"));
        }
    }
}
