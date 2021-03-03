// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheCloneMethod
        {
            [Fact]
            public void ShouldReturnEmptyCollectionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    using (var clones = images.Clone())
                    {
                        Assert.Empty(clones);
                    }
                }
            }

            [Fact]
            public void ShouldCloneTheImagesInTheCollection()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Add(Files.Builtin.Logo);
                    images.Add(Files.Builtin.Rose);
                    images.Add(Files.Builtin.Wizard);

                    using (var clones = images.Clone())
                    {
                        Assert.False(ReferenceEquals(images[0], clones[0]));
                        Assert.False(ReferenceEquals(images[1], clones[1]));
                        Assert.False(ReferenceEquals(images[2], clones[2]));
                    }
                }
            }
        }
    }
}
