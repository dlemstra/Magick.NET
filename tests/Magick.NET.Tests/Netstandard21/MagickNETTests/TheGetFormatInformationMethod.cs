// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public partial class TheGetFormatInformationMethod
        {
            public class WithReadOnlySequence
            {
                [Fact]
                public void ShouldThrowExceptionWhenSequencIsEmpty()
                {
                    var exception = Assert.Throws<ArgumentException>(() => MagickNET.GetFormatInformation(ReadOnlySequence<byte>.Empty));

                    Assert.Equal("data", exception.ParamName);
                }

                [Fact]
                public void ShouldReturnNullWhenFormatCannotBeDetermined()
                {
                    var formatInfo = MagickNET.GetFormatInformation(new ReadOnlySequence<byte>(new byte[] { 42 }));

                    Assert.Null(formatInfo);
                }

                [Fact]
                public void ShouldReturnTheCorrectInfoForTheJpgFormat()
                {
                    var bytes = new ReadOnlySequence<byte>(File.ReadAllBytes(Files.EightBimTIF));
                    var formatInfo = MagickNET.GetFormatInformation(bytes);

                    Assert.NotNull(formatInfo);
                    Assert.Equal(MagickFormat.Tiff, formatInfo.Format);
                }
            }

            public class WithReadonlySpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenSpanIsEmpty()
                {
                    var exception = Assert.Throws<ArgumentException>(() => MagickNET.GetFormatInformation(Span<byte>.Empty));

                    Assert.Equal("data", exception.ParamName);
                }

                [Fact]
                public void ShouldReturnNullWhenFormatCannotBeDetermined()
                {
                    var formatInfo = MagickNET.GetFormatInformation(new Span<byte>(new byte[] { 42 }));

                    Assert.Null(formatInfo);
                }

                [Fact]
                public void ShouldReturnTheCorrectInfoForTheJpgFormat()
                {
                    var bytes = new Span<byte>(File.ReadAllBytes(Files.MagickNETIconPNG));
                    var formatInfo = MagickNET.GetFormatInformation(bytes);

                    Assert.NotNull(formatInfo);
                    Assert.Equal(MagickFormat.Png, formatInfo.Format);
                }
            }
        }
    }
}

#endif
