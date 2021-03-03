// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests
{
    public class ConfigurationFilesTests
    {
        private ConfigurationFiles _default = ConfigurationFiles.Default;

        [Fact]
        public void Default_Colors_IsInitialized()
        {
            Assert.NotNull(_default.Colors);
            Assert.Equal("colors.xml", _default.Colors.FileName);
            Assert.NotNull(_default.Colors.Data);
            Assert.Contains("<colormap>", _default.Colors.Data);
        }

        [Fact]
        public void Default_Configure_IsInitialized()
        {
            Assert.NotNull(_default.Configure);
            Assert.Equal("configure.xml", _default.Configure.FileName);
            Assert.NotNull(_default.Configure.Data);
            Assert.Contains("<configuremap>", _default.Configure.Data);
        }

        [Fact]
        public void Default_Delegates_IsInitialized()
        {
            Assert.NotNull(_default.Delegates);
            Assert.Equal("delegates.xml", _default.Delegates.FileName);
            Assert.NotNull(_default.Delegates.Data);
            Assert.Contains("<delegatemap>", _default.Delegates.Data);
        }

        [Fact]
        public void Default_English_IsInitialized()
        {
            Assert.NotNull(_default.English);
            Assert.Equal("english.xml", _default.English.FileName);
            Assert.NotNull(_default.English.Data);
            Assert.Contains(@"<locale name=""english"">", _default.English.Data);
        }

        [Fact]
        public void Default_Locale_IsInitialized()
        {
            Assert.NotNull(_default.Locale);
            Assert.Equal("locale.xml", _default.Locale.FileName);
            Assert.NotNull(_default.Locale.Data);
            Assert.Contains(@"<localemap>", _default.Locale.Data);
        }

        [Fact]
        public void Default_Log_IsInitialized()
        {
            Assert.NotNull(_default.Log);
            Assert.Equal("log.xml", _default.Log.FileName);
            Assert.NotNull(_default.Log.Data);
            Assert.Contains(@"<logmap>", _default.Log.Data);
        }

        [Fact]
        public void Default_Policy_IsInitialized()
        {
            Assert.NotNull(_default.Policy);
            Assert.Equal("policy.xml", _default.Policy.FileName);
            Assert.NotNull(_default.Policy.Data);
            Assert.Contains(@"<policymap>", _default.Policy.Data);
        }

        [Fact]
        public void Default_Thresholds_IsInitialized()
        {
            Assert.NotNull(_default.Thresholds);
            Assert.Equal("thresholds.xml", _default.Thresholds.FileName);
            Assert.NotNull(_default.Thresholds.Data);
            Assert.Contains(@"<thresholds>", _default.Thresholds.Data);
        }

        [Fact]
        public void Default_Type_IsInitialized()
        {
            Assert.NotNull(_default.Type);
            Assert.Equal("type.xml", _default.Type.FileName);
            Assert.NotNull(_default.Type.Data);
            Assert.Contains(@"<typemap>", _default.Type.Data);
            Assert.Contains(@"<include file=""", _default.Type.Data);
        }

        [Fact]
        public void Default_TypeGhostscript_IsInitialized()
        {
            Assert.NotNull(_default.TypeGhostscript);
            Assert.Equal("type-ghostscript.xml", _default.TypeGhostscript.FileName);
            Assert.NotNull(_default.TypeGhostscript.Data);
            Assert.Contains(@"<typemap>", _default.TypeGhostscript.Data);
            Assert.Contains(@"<type name=""", _default.TypeGhostscript.Data);
        }
    }
}