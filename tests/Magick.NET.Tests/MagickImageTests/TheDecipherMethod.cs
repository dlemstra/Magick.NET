// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheDecipherMethod
    {
        [Fact]
        public void ShouldReturnDifferentImageWhenPassphraseIsIncorrect()
        {
            using var original = new MagickImage(Files.SnakewarePNG);
            using var enciphered = original.Clone();
            enciphered.Encipher("All your base are belong to us");

            using var deciphered = enciphered.Clone();
            deciphered.Decipher("What you say!!");

            Assert.NotEqual(0.0, enciphered.Compare(deciphered, ErrorMetric.RootMeanSquared));
            Assert.NotEqual(0.0, original.Compare(deciphered, ErrorMetric.RootMeanSquared));
        }

        [Fact]
        public void ShouldChangeThePixelsToTheOriginalValues()
        {
            using var original = new MagickImage(Files.SnakewarePNG);
            using var enciphered = original.Clone();
            enciphered.Encipher("All your base are belong to us");

            using var deciphered = enciphered.Clone();
            deciphered.Decipher("All your base are belong to us");

            Assert.NotEqual(0.0, enciphered.Compare(deciphered, ErrorMetric.RootMeanSquared));
            Assert.Equal(0.0, original.Compare(deciphered, ErrorMetric.RootMeanSquared));
        }
    }
}
