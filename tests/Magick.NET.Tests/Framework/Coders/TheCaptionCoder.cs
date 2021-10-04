// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETCORE

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheCaptionCoder
    {
        [Fact]
        public void ShouldAddCorrectLineBreaks1()
        {
            var caption = "caption:Text 2 Verylongtext";
            var readSettings = new MagickReadSettings
            {
                FontPointsize = 23,
                FillColor = MagickColors.Blue,
                Width = 180,
                Height = 85,
            };

            using (var image = new MagickImage(caption, readSettings))
            {
                ColorAssert.Equal(MagickColors.Blue, image, 55, 20);
            }
        }

        [Fact]
        public void ShouldAddCorrectLineBreaks2()
        {
            var caption = "caption:tex1_124x40_3a277be1b9da51b7_2d0d8f84dc3ccc36_8";
            var readSettings = new MagickReadSettings
            {
                BackgroundColor = MagickColors.Transparent,
                FontPointsize = 39,
                FillColor = MagickColors.Red,
                TextUnderColor = MagickColors.Green,
                TextGravity = Gravity.Center,
                Width = 450,
            };

            using (var image = new MagickImage(caption, readSettings))
            {
                ColorAssert.Equal(MagickColors.Green, image, 158, 55);
                ColorAssert.Equal(MagickColors.Green, image, 179, 81);
                ColorAssert.Equal(MagickColors.Red, image, 443, 25);
                ColorAssert.Equal(MagickColors.Red, image, 392, 55);
                ColorAssert.Equal(MagickColors.Red, image, 231, 116);
                ColorAssert.Equal(new MagickColor("#0000"), image, 170, 93);
            }
        }

        [Fact]
        public void ShouldAddCorrectLineBreaks3()
        {
            var caption = "caption:Dans votre vie, vous mangerez environ 30 000 kilos de nourriture, l’équivalent du poids de 6 éléphants.";
            var readSettings = new MagickReadSettings
            {
                TextGravity = Gravity.Center,
                Width = 465,
                Height = 101,
            };

            using (var image = new MagickImage(caption, readSettings))
            {
                ColorAssert.Equal(MagickColors.Black, image, 415, 27);
                ColorAssert.Equal(MagickColors.Black, image, 426, 52);
                ColorAssert.Equal(MagickColors.Black, image, 316, 83);
                ColorAssert.Equal(MagickColors.White, image, 250, 52);
            }
        }

        [Fact]
        public void ShouldAddCorrectLineBreaks4()
        {
            var caption = "caption:This does not wrap";
            var readSettings = new MagickReadSettings
            {
                FontPointsize = 50,
                Width = 400,
            };

            using (var image = new MagickImage(caption, readSettings))
            {
                ColorAssert.Equal(MagickColors.White, image, 321, 30);
                ColorAssert.Equal(MagickColors.Black, image, 86, 86);
            }
        }

        [Fact]
        public void ShouldAddCorrectLineBreaks5()
        {
            var caption = "caption:A";
            var readSettings = new MagickReadSettings
            {
                BackgroundColor = MagickColors.Transparent,
                FontPointsize = 72,
                TextGravity = Gravity.West,
                FillColor = MagickColors.Black,
                Width = 40,
            };

            using (var image = new MagickImage(caption, readSettings))
            {
                Assert.Equal(83, image.Height);

                ColorAssert.Equal(MagickColors.Black, image, 39, 46);
                ColorAssert.Equal(new MagickColor("#0000"), image, 39, 65);
            }
        }

        [Fact]
        public void ShouldAddCorrectLineBreaks6()
        {
            var caption = "caption:AAA";
            var readSettings = new MagickReadSettings
            {
                BackgroundColor = MagickColors.Transparent,
                FontPointsize = 72,
                TextGravity = Gravity.West,
                FillColor = MagickColors.Black,
                Width = 40,
            };

            using (var image = new MagickImage(caption, readSettings))
            {
                Assert.Equal(249, image.Height);

                ColorAssert.Equal(MagickColors.Black, image, 39, 47);
                ColorAssert.Equal(new MagickColor("#0000"), image, 39, 66);
                ColorAssert.Equal(MagickColors.Black, image, 39, 129);
                ColorAssert.Equal(new MagickColor("#0000"), image, 39, 148);
                ColorAssert.Equal(MagickColors.Black, image, 39, 211);
                ColorAssert.Equal(new MagickColor("#0000"), image, 39, 230);
            }
        }
    }
}

#endif
