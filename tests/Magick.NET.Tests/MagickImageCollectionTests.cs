// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        [Fact]
        public void Test_ReadSettings()
        {
            var settings = new MagickReadSettings();
            settings.FontFamily = "Courier New";
            settings.FillColor = MagickColors.Gold;
            settings.FontPointsize = 80;
            settings.Format = MagickFormat.Text;
            settings.TextGravity = Gravity.Center;

            using (var images = new MagickImageCollection(Files.ImageMagickTXT, settings))
            {
                Assert.Equal(2, images.Count);
                ColorAssert.Equal(MagickColors.Gold, images[0], 348, 648);
            }

            using (var images = new MagickImageCollection())
            {
                images.Ping(Files.ImageMagickTXT, settings);

                Assert.Equal(2, images.Count);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    ColorAssert.Equal(MagickColors.Gold, images[0], 348, 648);
                });
            }
        }

        [Fact]
        public void Test_Remove()
        {
            using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                var first = images[0];
                images.Remove(first);

                Assert.Equal(2, images.Count);
                Assert.Equal(-1, images.IndexOf(first));

                first = images[0];
                images.RemoveAt(0);

                Assert.Single(images);
                Assert.Equal(-1, images.IndexOf(first));
            }
        }

        [Fact]
        public void Test_RePage()
        {
            using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                images[0].Page = new MagickGeometry("0x0+10+20");

                Assert.Equal(10, images[0].Page.X);
                Assert.Equal(20, images[0].Page.Y);

                images[0].Settings.Page = new MagickGeometry("0x0+10+20");

                Assert.Equal(10, images[0].Settings.Page.X);
                Assert.Equal(20, images[0].Settings.Page.Y);

                images.RePage();

                Assert.Equal(0, images[0].Page.X);
                Assert.Equal(0, images[0].Page.Y);

                Assert.Equal(10, images[0].Settings.Page.X);
                Assert.Equal(20, images[0].Settings.Page.Y);
            }
        }

        [Fact]
        public void Test_Reverse()
        {
            using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                var first = images.First();
                images.Reverse();

                var last = images.Last();
                Assert.True(last == first);
            }
        }

        [Fact]
        public void Test_ToBase64()
        {
            using (var images = new MagickImageCollection())
            {
                Assert.Equal(string.Empty, images.ToBase64());

                images.Read(Files.Builtin.Logo);
                Assert.Equal(1228800, images.ToBase64(MagickFormat.Rgb).Length);
            }
        }

        [Fact]
        public void Test_TrimBounds()
        {
            using (var images = new MagickImageCollection())
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    images.TrimBounds();
                });

                images.Add(Files.Builtin.Logo);
                images.Add(Files.Builtin.Wizard);
                images.TrimBounds();

                Assert.Equal(640, images[0].Page.Width);
                Assert.Equal(640, images[0].Page.Height);
                Assert.Equal(0, images[0].Page.X);
                Assert.Equal(0, images[0].Page.Y);

                Assert.Equal(640, images[1].Page.Width);
                Assert.Equal(640, images[1].Page.Height);
                Assert.Equal(0, images[0].Page.X);
                Assert.Equal(0, images[0].Page.Y);
            }
        }

        [Fact]
        public void Test_Write()
        {
            var fileSize = new FileInfo(Files.RoseSparkleGIF).Length;
            Assert.Equal(9891, fileSize);

            using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    images.Write(memStream);

                    Assert.Equal(fileSize, memStream.Length);
                }
            }

            var tempFile = new FileInfo(Path.GetTempFileName() + ".gif");
            try
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    images.Write(tempFile);

                    Assert.Equal(fileSize, tempFile.Length);
                }
            }
            finally
            {
                Cleanup.DeleteFile(tempFile);
            }
        }
    }
}
