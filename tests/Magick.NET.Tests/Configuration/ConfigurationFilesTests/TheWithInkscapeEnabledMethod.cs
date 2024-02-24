// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class TheWithInkscapeEnabledMethod
    {
        [Fact]
        public void ShouldReturnConfigurationWithInkScapeEnabled()
        {
            var defaultConfiguration = ConfigurationFiles.Default;
            defaultConfiguration.Colors.Data = "colors";
            defaultConfiguration.Configure.Data = "configure";
            defaultConfiguration.Delegates.Data = defaultConfiguration.Delegates.Data.Replace("\"browse\"", "\"foobar\"");
            defaultConfiguration.English.Data = "english";
            defaultConfiguration.Locale.Data = "locale";
            defaultConfiguration.Log.Data = "log";
            defaultConfiguration.Policy.Data = "policy";
            defaultConfiguration.Thresholds.Data = "thresholds";
            defaultConfiguration.Type.Data = "type";
            defaultConfiguration.TypeGhostscript.Data = "typeghostscript";

            var configuration = defaultConfiguration.WithInkscapeEnabled();

            Assert.Contains(" <delegate decode=\"svg:decode\" stealth=\"True\" command=\"&quot;inkscape&quot; &quot;%s&quot; --export-filename=&quot;%s&quot; --export-dpi=&quot;%s&quot; --export-background=&quot;%s&quot; --export-background-opacity=&quot;%s&quot; &gt; &quot;%s&quot; 2&gt;&amp;1\"/>", configuration.Delegates.Data);
            Assert.Contains("\"foobar\"", configuration.Delegates.Data);
            Assert.Equal("colors", configuration.Colors.Data);
            Assert.Equal("configure", configuration.Configure.Data);
            Assert.Equal("english", configuration.English.Data);
            Assert.Equal("locale", configuration.Locale.Data);
            Assert.Equal("log", configuration.Log.Data);
            Assert.Equal("policy", configuration.Policy.Data);
            Assert.Equal("thresholds", configuration.Thresholds.Data);
            Assert.Equal("type", configuration.Type.Data);
            Assert.Equal("typeghostscript", configuration.TypeGhostscript.Data);
        }
    }
}
