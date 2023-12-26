// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCommentProperty
    {
        [Fact]
        public void ShouldGetTheCommentAttribute()
        {
            using var image = new MagickImage();
            image.SetAttribute("comment", "foo");

            Assert.Equal("foo", image.Comment);
        }

        [Fact]
        public void ShouldSetTheCommentAttribute()
        {
            using var image = new MagickImage();
            image.Comment = "foo";

            Assert.Equal("foo", image.GetAttribute("comment"));
        }

        [Fact]
        public void ShouldRemoveTheCommentAttributeWhenSetToNull()
        {
            using var image = new MagickImage();
            image.SetAttribute("comment", "foo");

            image.Comment = null;

            Assert.Null(image.GetAttribute("comment"));
        }

        [Fact]
        public void ShouldBeStoredAsItxtInPngFileWhenStringContainsNonAnsiCharacters()
        {
            using var image = new MagickImage(MagickColors.Purple, 1, 1);
            using var tempFile = new TemporaryFile("test.png");

            var comment = "Hello";
            image.Comment = comment;
            image.Write(tempFile.File);

            image.Read(tempFile.File);
            Assert.Equal(comment, image.Comment);

            var pngText = image.GetAttribute("png:text");
            Assert.Equal("4 tEXt/zTXt/iTXt chunks were found", pngText);

            comment = "Hello 😉";
            image.Comment = comment;
            image.Write(tempFile.File);

            image.Read(tempFile.File);
            Assert.Equal(comment, image.Comment);

            pngText = image.GetAttribute("png:text");
            Assert.Equal("3 tEXt/zTXt/iTXt chunks were found", pngText);
        }
    }
}
