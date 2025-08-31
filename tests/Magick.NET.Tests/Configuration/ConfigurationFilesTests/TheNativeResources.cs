// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class TheNativeResources
    {
        [Fact]
        public void ShouldBeEmbeddedInTheNativeLibrary()
        {
            if (!Runtime.IsWindows)
                Assert.Skip("The embedded resources are only available on Windows.");

            foreach (var configurationFile in ConfigurationFiles.Default.All)
            {
                var data = MagickNET.GetWindowsResource(configurationFile.FileName);

                Assert.NotNull(data);
                Assert.Equal(configurationFile.Data, data);
            }
        }
    }
}
