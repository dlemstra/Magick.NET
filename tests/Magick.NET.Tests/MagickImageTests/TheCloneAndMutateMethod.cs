// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCloneAndMutateMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenNoImageIsRead()
        {
            using var image = new MagickImage();

            Assert.Throws<MagickCorruptImageErrorException>(() => image.CloneAndMutate(static mutator => mutator.Resize(50, 50)));
        }

        [Fact]
        public void ShouldThrowExceptionWhenNoActionIsExecuted()
        {
            using var image = new MagickImage();

            Assert.Throws<InvalidOperationException>(() => image.CloneAndMutate(_ => { }));
        }

        [Fact]
        public void ShouldThrowExceptionWhenMultipleActionsAreExecuted()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            using var clone = image.CloneAndMutate(mutator =>
            {
                mutator.Resize(100, 100);
                Assert.Throws<InvalidOperationException>(() => mutator.Resize(50, 50));
            });
        }

        [Fact]
        public void ShouldCloneAndMutateTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var clone = image.CloneAndMutate(static mutator => mutator.Resize(100, 100));

            Assert.NotEqual(image, clone);
            Assert.False(ReferenceEquals(image, clone));
            Assert.Equal(100U, clone.Width);
            Assert.Equal(75U, clone.Height);
        }
    }
}
