// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCopyPixelsMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenSourceIsNull()
        {
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("source", () => destination.CopyPixels(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSourceIsNullAndChannelIsSpecified()
        {
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("source", () => destination.CopyPixels(null!, Channels.Red));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSourceIsNullAndGeometryIsSpecified()
        {
            using var source = new MagickImage();
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("source", () => destination.CopyPixels(null!, new MagickGeometry(10, 10)));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSourceIsNullAndGeometryAndChannelsAreSpecified()
        {
            using var source = new MagickImage();
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("source", () => destination.CopyPixels(null!, new MagickGeometry(10, 10), Channels.Black));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSourceIsNullAndGeometryAndXYAreSpecified()
        {
            using var source = new MagickImage();
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("source", () => destination.CopyPixels(null!, new MagickGeometry(10, 10), 0, 0));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSourceIsNullAndGeometryAndXYAndChannelsAreSpecified()
        {
            using var source = new MagickImage();
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("source", () => destination.CopyPixels(null!, new MagickGeometry(10, 10), 0, 0, Channels.Black));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNull()
        {
            using var source = new MagickImage();
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("geometry", () => destination.CopyPixels(source, null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNullAndChannelIsSpecified()
        {
            using var source = new MagickImage();
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("geometry", () => destination.CopyPixels(source, null!, Channels.Green));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNullAndXYIsSpecified()
        {
            using var source = new MagickImage();
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("geometry", () => destination.CopyPixels(source, null!, 0, 0));
        }

        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNullAndXYAndChannelsAreSpecified()
        {
            using var source = new MagickImage();
            using var destination = new MagickImage();

            Assert.Throws<ArgumentNullException>("geometry", () => destination.CopyPixels(source, null!, 0, 0, Channels.Green));
        }

        [Fact]
        public void ShouldThrowExceptionWhenWidthIsTooHigh()
        {
            using var source = new MagickImage(MagickColors.White, 100, 100);
            using var destination = new MagickImage(MagickColors.Black, 50, 50);

            var exception = Assert.Throws<MagickOptionErrorException>(() => destination.CopyPixels(source, new MagickGeometry(51, 50), 0, 0));
            Assert.Contains("geometry does not contain image", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsTooHigh()
        {
            using var source = new MagickImage(MagickColors.White, 100, 100);
            using var destination = new MagickImage(MagickColors.Black, 50, 50);

            var exception = Assert.Throws<MagickOptionErrorException>(() => destination.CopyPixels(source, new MagickGeometry(50, 51), 0, 0));

            Assert.Contains("geometry does not contain image", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenXIsTooHigh()
        {
            using var source = new MagickImage(MagickColors.White, 100, 100);
            using var destination = new MagickImage(MagickColors.Black, 50, 50);

            var exception = Assert.Throws<MagickOptionErrorException>(() => destination.CopyPixels(source, new MagickGeometry(50, 50), 1, 0));

            Assert.Contains("geometry does not contain image", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenYIsTooHigh()
        {
            using var source = new MagickImage(MagickColors.White, 100, 100);
            using var destination = new MagickImage(MagickColors.Black, 50, 50);

            var exception = Assert.Throws<MagickOptionErrorException>(() => destination.CopyPixels(source, new MagickGeometry(50, 50), 0, 1));

            Assert.Contains("geometry does not contain image", exception.Message);
        }

        [Fact]
        public void ShouldCopyThePixelsOfTheSpecifiedArea()
        {
            using var source = new MagickImage(MagickColors.White, 100, 100);
            using var destination = new MagickImage(MagickColors.Black, 50, 50);
            destination.CopyPixels(source, new MagickGeometry(25, 25), 25, 25);

            ColorAssert.Equal(MagickColors.Black, destination, 0, 0);
            ColorAssert.Equal(MagickColors.Black, destination, 24, 24);
            ColorAssert.Equal(MagickColors.White, destination, 25, 25);
            ColorAssert.Equal(MagickColors.White, destination, 49, 49);

            destination.CopyPixels(source, new MagickGeometry(25, 25), 0, 25, Channels.Green);

            ColorAssert.Equal(MagickColors.Black, destination, 0, 0);
            ColorAssert.Equal(MagickColors.Black, destination, 24, 24);
            ColorAssert.Equal(MagickColors.White, destination, 25, 25);
            ColorAssert.Equal(MagickColors.White, destination, 49, 49);
            ColorAssert.Equal(MagickColors.Lime, destination, 0, 25);
            ColorAssert.Equal(MagickColors.Lime, destination, 24, 49);
        }
    }
}
