// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public class ThePolicy
    {
        /// <summary>
        /// The policy is initialized with <see cref="TestInitializer.ModifyPolicy"/> at the start of all tests.
        /// </summary>
        [Fact]
        public void ShouldCauseAnExceptionWhenThePalmCoderIsDisabled()
        {
            using var tempFile = new TemporaryFile("test.palm");
            using var fs = tempFile.File.OpenWrite();
            var bytes = new byte[4] { 0, 0, 0, 0 };
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            using var image = new MagickImage();

            Assert.Throws<MagickPolicyErrorException>(() => image.Read(tempFile.File));
        }
    }
}
