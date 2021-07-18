// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheWarningEvent
        {
            [Fact]
            public void ShouldRaiseEventsForWarnings()
            {
                int count = 0;
                EventHandler<WarningEventArgs> warningDelegate = (sender, arguments) =>
                {
                    Assert.NotNull(sender);
                    Assert.NotNull(arguments);
                    Assert.NotNull(arguments.Message);
                    Assert.NotEqual(string.Empty, arguments.Message);
                    Assert.NotNull(arguments.Exception);

                    count++;
                };

                using (var image = new MagickImage())
                {
                    image.Warning += warningDelegate;
                    image.Read(Files.EightBimTIF);

                    Assert.NotEqual(0, count);

                    var expectedCount = count;
                    image.Warning -= warningDelegate;
                    image.Read(Files.EightBimTIF);

                    Assert.Equal(expectedCount, count);
                }
            }
        }
    }
}
