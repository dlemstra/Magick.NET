// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class TemporaryDefinesTests
{
    public class TheSetArtifactMethod
    {
        [Fact]
        public void ShouldSetArtifactWhenValueIsNotNull()
        {
            using var image = new MagickImage();
            using var temporaryDefines = new TemporaryDefines(image);
            temporaryDefines.SetArtifact("foo", "bar");

            Assert.Equal("bar", image.GetArtifact("foo"));
        }

        [Fact]
        public void ShouldNotSetArtifactWhenValueIsNull()
        {
            using var image = new MagickImage();
            using var temporaryDefines = new TemporaryDefines(image);
            temporaryDefines.SetArtifact("foo", (string)null);

            Assert.Null(image.GetArtifact("foo"));
        }

        [Fact]
        public void ShouldNotSetArtifactWhenValueIsEmpty()
        {
            using var image = new MagickImage();
            using var temporaryDefines = new TemporaryDefines(image);
            temporaryDefines.SetArtifact("foo", string.Empty);

            Assert.Null(image.GetArtifact("foo"));
        }

        [Fact]
        public void ShouldSetTheBooleanValue()
        {
            using var image = new MagickImage();
            using var temporaryDefines = new TemporaryDefines(image);
            temporaryDefines.SetArtifact("foo", true);

            Assert.Equal("true", image.GetArtifact("foo"));
        }

        [Fact]
        public void ShouldNotSetTheNullableValueWhenValueIsNull()
        {
            using var image = new MagickImage();
            using var temporaryDefines = new TemporaryDefines(image);
            temporaryDefines.SetArtifact("foo", (Channels?)null);

            Assert.Null(image.GetArtifact("foo"));
        }

        [Fact]
        public void ShouldSetTheNullableValue()
        {
            using var image = new MagickImage();
            using var temporaryDefines = new TemporaryDefines(image);
            temporaryDefines.SetArtifact("foo", Channels.Index);

            Assert.Equal("Index", image.GetArtifact("foo"));
        }

        [Fact]
        public void ShouldNotSetTheValueWhenDoubleIsNull()
        {
            using var image = new MagickImage();
            using var temporaryDefines = new TemporaryDefines(image);
            temporaryDefines.SetArtifact("foo", (double?)null);

            Assert.Null(image.GetArtifact("foo"));
        }

        [Fact]
        public void ShouldSetTheDoubleValue()
        {
            using var image = new MagickImage();
            using var temporaryDefines = new TemporaryDefines(image);
            temporaryDefines.SetArtifact("foo", 1.25);

            Assert.Equal("1.25", image.GetArtifact("foo"));
        }
    }
}
