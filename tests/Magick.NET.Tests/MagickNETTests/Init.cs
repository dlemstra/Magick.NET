// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        private static void AssertConfigFiles(string path)
        {
            Assert.True(File.Exists(Path.Combine(path, "colors.xml")));
            Assert.True(File.Exists(Path.Combine(path, "configure.xml")));
            Assert.True(File.Exists(Path.Combine(path, "delegates.xml")));
            Assert.True(File.Exists(Path.Combine(path, "english.xml")));
            Assert.True(File.Exists(Path.Combine(path, "locale.xml")));
            Assert.True(File.Exists(Path.Combine(path, "log.xml")));
            Assert.True(File.Exists(Path.Combine(path, "policy.xml")));
            Assert.True(File.Exists(Path.Combine(path, "thresholds.xml")));
            Assert.True(File.Exists(Path.Combine(path, "type.xml")));
            Assert.True(File.Exists(Path.Combine(path, "type-ghostscript.xml")));
        }
    }
}
