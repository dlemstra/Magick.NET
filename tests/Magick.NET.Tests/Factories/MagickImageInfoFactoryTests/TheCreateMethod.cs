// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Factories;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageInfoFactoryTests
{
    public partial class TheCreateMethod
    {
        public class WithoutArguments
        {
            [Fact]
            public void ShouldCreateMagickImageInfo()
            {
                var factory = new MagickImageInfoFactory();
                var info = factory.Create();

                Assert.IsType<MagickImageInfo>(info);
                Assert.Equal(0, info.Width);
            }
        }

        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentNullException>("data", () => factory.Create((byte[])null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Array.Empty<byte>()));
            }

            [Fact]
            public void ShouldCreateMagickImageInfo()
            {
                var factory = new MagickImageInfoFactory();
                var data = File.ReadAllBytes(Files.ImageMagickJPG);

                var info = factory.Create(data);

                Assert.IsType<MagickImageInfo>(info);
                Assert.Equal(123, info.Width);
            }
        }

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentNullException>("data", () => factory.Create(null, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Array.Empty<byte>(), 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenOffsetIsNegative()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentException>("offset", () => factory.Create(new byte[] { 215 }, -1, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentException>("count", () => factory.Create(new byte[] { 215 }, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsNegative()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentException>("count", () => factory.Create(new byte[] { 215 }, 0, -1));
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentNullException>("file", () => factory.Create((FileInfo)null));
            }

            [Fact]
            public void ShouldCreateMagickImageInfo()
            {
                var factory = new MagickImageInfoFactory();
                var file = new FileInfo(Files.ImageMagickJPG);

                var info = factory.Create(file);

                Assert.IsType<MagickImageInfo>(info);
                Assert.Equal(123, info.Width);
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentNullException>("fileName", () => factory.Create((string)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentException>("fileName", () => factory.Create(string.Empty));
            }

            [Fact]
            public void ShouldCreateMagickImageInfo()
            {
                var factory = new MagickImageInfoFactory();

                var info = factory.Create(Files.ImageMagickJPG);

                Assert.IsType<MagickImageInfo>(info);
                Assert.Equal(123, info.Width);
            }
        }

        public class WithFileNameAndReadSettings
        {
            [Fact]
            public void ShouldUseTheReadSettings()
            {
                var factory = new MagickImageInfoFactory();
                var settings = new MagickReadSettings(new BmpReadDefines
                {
                    IgnoreFileSize = true,
                });

                var info = factory.Create(Files.Coders.InvalidCrcBMP, settings);

                Assert.IsType<MagickImageInfo>(info);
                Assert.Equal(1, info.Width);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentNullException>("stream", () => factory.Create((Stream)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var factory = new MagickImageInfoFactory();

                Assert.Throws<ArgumentException>("stream", () => factory.Create(new MemoryStream()));
            }

            [Fact]
            public void ShouldCreateMagickImageInfo()
            {
                var factory = new MagickImageInfoFactory();

                using var stream = File.OpenRead(Files.ImageMagickJPG);
                var info = factory.Create(stream);

                Assert.IsType<MagickImageInfo>(info);
                Assert.Equal(123, info.Width);
            }
        }
    }
}
