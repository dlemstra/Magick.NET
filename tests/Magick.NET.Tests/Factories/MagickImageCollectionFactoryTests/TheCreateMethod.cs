// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionFactoryTests
{
    public partial class TheCreateMethod
    {
        public class WithoutArguments
        {
            [Fact]
            public void ShouldCreateMagickImageCollection()
            {
                var factory = new MagickImageCollectionFactory();

                using var images = factory.Create();
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentNullException>("data", () => factory.Create((byte[])null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Array.Empty<byte>()));
            }

            [Fact]
            public void ShouldCreateMagickImageCollection()
            {
                var factory = new MagickImageCollectionFactory();
                var data = File.ReadAllBytes(Files.ImageMagickJPG);

                using var images = factory.Create(data);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentNullException>("data", () => factory.Create(null, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Array.Empty<byte>(), 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenOffsetIsNegative()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("offset", () => factory.Create(new byte[] { 215 }, -1, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("count", () => factory.Create(new byte[] { 215 }, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsNegative()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("count", () => factory.Create(new byte[] { 215 }, 0, -1));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var factory = new MagickImageCollectionFactory();
                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var images = factory.Create(bytes, 10, bytes.Length - 10);
                Assert.Single(images);
            }
        }

        public class WithByteArrayAndOffsetAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("data", () => factory.Create(null, 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Array.Empty<byte>(), 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenOffsetIsNegative()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("offset", () => factory.Create(new byte[] { 215 }, -1, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("count", () => factory.Create(new byte[] { 215 }, 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsNegative()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("count", () => factory.Create(new byte[] { 215 }, 0, -1, settings));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var images = factory.Create(bytes, 10, bytes.Length - 10, settings);
                Assert.Single(images);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageCollectionFactory();
                var bytes = File.ReadAllBytes(Files.CirclePNG);

                using var image = factory.Create(bytes, 0, bytes.Length, null);
            }
        }

        public class WithByteArrayAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("data", () => factory.Create((byte[])null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Array.Empty<byte>(), settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var images = factory.Create(bytes, null);
            }

            [Fact]
            public void ShouldCreateMagickImageCollection()
            {
                var factory = new MagickImageCollectionFactory();
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var settings = new MagickReadSettings
                {
                    BackgroundColor = MagickColors.Goldenrod,
                };

                using var image = factory.Create(data, settings);
                Assert.IsType<MagickImageCollection>(image);
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentNullException>("file", () => factory.Create((FileInfo)null));
            }

            [Fact]
            public void ShouldCreateMagickImage()
            {
                var factory = new MagickImageCollectionFactory();
                var file = new FileInfo(Files.ImageMagickJPG);

                using var images = factory.Create(file);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("file", () => factory.Create((FileInfo)null, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                using var images = factory.Create(new FileInfo(Files.CirclePNG), null);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentNullException>("fileName", () => factory.Create((string)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("fileName", () => factory.Create(string.Empty));
            }

            [Fact]
            public void ShouldCreateMagickImage()
            {
                var factory = new MagickImageCollectionFactory();

                using var images = factory.Create(Files.ImageMagickJPG);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("fileName", () => factory.Create((string)null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("fileName", () => factory.Create(string.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                using var images = factory.Create(Files.CirclePNG, null);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentNullException>("stream", () => factory.Create((Stream)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("stream", () => factory.Create(new MemoryStream()));
            }

            [Fact]
            public void ShouldCreateMagickImage()
            {
                var factory = new MagickImageCollectionFactory();

                using var stream = File.OpenRead(Files.ImageMagickJPG);
                using var images = factory.Create(stream);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("stream", () => factory.Create((Stream)null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("stream", () => factory.Create(new MemoryStream(), settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                using var fileStream = File.OpenRead(Files.CirclePNG);
                using var images = factory.Create(fileStream, null);
                Assert.IsType<MagickImageCollection>(images);
            }
        }
    }
}
