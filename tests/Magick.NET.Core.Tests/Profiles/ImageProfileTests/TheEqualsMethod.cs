// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ImageProfileTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenOtherIsNull()
        {
            var profile = new ImageProfile("test", Array.Empty<byte>());

            Assert.False(profile.Equals(null));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherAsObjectIsNull()
        {
            var profile = new ImageProfile("test", Array.Empty<byte>());

            Assert.False(profile.Equals((object?)null!));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsEqual()
        {
            var profile = new ImageProfile("test", [1, 2, 3]);
            var other = new ImageProfile("test", [1, 2, 3]);

            Assert.True(profile.Equals(other));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherAsObjectIsEqual()
        {
            var profile = new ImageProfile("test", [1, 2, 3]);
            var other = new ImageProfile("test", [1, 2, 3]);

            Assert.True(profile.Equals((object)other));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotEqual()
        {
            var profile = new ImageProfile("test", [1, 2, 3]);
            var other = new ImageProfile("test", [3, 2, 1]);

            Assert.False(profile.Equals(other));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherAsObjectIsNotEqual()
        {
            var profile = new ImageProfile("test", new byte[] { 1, 2, 3 });
            var other = new ImageProfile("test", new byte[] { 3, 2, 1 });

            Assert.False(profile.Equals((object)other));
        }
    }
}
