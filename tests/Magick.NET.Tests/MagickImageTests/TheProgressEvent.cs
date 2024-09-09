// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheProgressEvent
    {
        [Fact]
        public void ShouldStopMethodExecutionWhenCancelIsSetToTrue()
        {
            var progress = new Percentage(0);
            void ProgressEvent(object? sender, ProgressEventArgs arguments)
            {
                Assert.NotNull(sender);
                Assert.NotNull(arguments);
                Assert.NotNull(arguments.Origin);
                Assert.False(arguments.Cancel);

                progress = arguments.Progress;
                arguments.Cancel = true;
            }

            using var image = new MagickImage(Files.Builtin.Logo);
            image.Progress += ProgressEvent;
            image.Flip();

            Assert.InRange((int)progress, 0, 2);
        }

        [Fact]
        public void ShouldNotStopMethodExecutionWhenCancelIsSetToFalse()
        {
            var progress = new Percentage(0);
            void ProgressEvent(object? sender, ProgressEventArgs arguments)
            {
                Assert.NotNull(sender);
                Assert.NotNull(arguments);
                Assert.NotNull(arguments.Origin);
                Assert.False(arguments.Cancel);

                progress = arguments.Progress;
                arguments.Cancel = false;
            }

            using var image = new MagickImage(Files.Builtin.Logo);
            image.Progress += ProgressEvent;
            image.Flip();

            Assert.Equal(100, (int)progress);
        }
    }
}
