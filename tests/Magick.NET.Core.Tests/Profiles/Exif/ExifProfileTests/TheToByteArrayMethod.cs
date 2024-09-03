// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ExifProfileTests
{
    public class TheToByteArrayMethod
    {
        [Fact]
        public void ShouldReturnEmptyArrayWhenEmpty()
        {
            var profile = new ExifProfile();

            var bytes = profile.ToByteArray();
            Assert.Empty(bytes);
        }

        [Fact]
        public void ShouldReturnEmptyArrayWhenAllValuesAreInvalid()
        {
            var bytes = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 42, 1, 4, 0, 1, 0, 0, 0, 42, 0, 0, 0, 26, 0, 0, 0, 0, 0 };

            var profile = new ExifProfile(bytes);

            var unknownTag = new ExifTag<uint>((ExifTagValue)298);
            var value = profile.GetValue(unknownTag);

            Assert.NotNull(value);
            Assert.Equal(42U, value.GetValue());
            Assert.Equal("42", value.ToString());

            bytes = profile.ToByteArray();
            Assert.Empty(bytes);
        }

        [Fact]
        public void ShouldExcludeEmptyStrings()
        {
            var profile = new ExifProfile();
            profile.SetValue(ExifTag.ImageDescription, string.Empty);

            var data = profile.ToByteArray();

            var result = ExifReader.Read(data);

            Assert.Empty(result.Values);
        }
    }
}
