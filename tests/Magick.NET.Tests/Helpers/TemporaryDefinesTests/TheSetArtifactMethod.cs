// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TemporaryDefinesTests
    {
        public class TheSetArtifactMethod
        {
            [Fact]
            public void ShouldSetArtificatWhenValueIsNotNull()
            {
                using (var image = new MagickImage())
                {
                    using (var temporaryDefines = new TemporaryDefines(image))
                    {
                        temporaryDefines.SetArtifact("foo", "bar");

                        Assert.Equal("bar", image.GetArtifact("foo"));
                    }
                }
            }

            [Fact]
            public void ShouldNotSetArtificatWhenValueIsNull()
            {
                using (var image = new MagickImage())
                {
                    using (var temporaryDefines = new TemporaryDefines(image))
                    {
                        temporaryDefines.SetArtifact("foo", (string)null);

                        Assert.Null(image.GetArtifact("foo"));
                    }
                }
            }

            [Fact]
            public void ShouldNotSetArtificatWhenValueIsEmpty()
            {
                using (var image = new MagickImage())
                {
                    using (var temporaryDefines = new TemporaryDefines(image))
                    {
                        temporaryDefines.SetArtifact("foo", string.Empty);

                        Assert.Null(image.GetArtifact("foo"));
                    }
                }
            }

            [Fact]
            public void ShouldSetTheBooleanValue()
            {
                using (var image = new MagickImage())
                {
                    using (var temporaryDefines = new TemporaryDefines(image))
                    {
                        temporaryDefines.SetArtifact("foo", true);

                        Assert.Equal("true", image.GetArtifact("foo"));
                    }
                }
            }

            [Fact]
            public void ShouldNotSetTheNullableValueWhenValueIsNull()
            {
                using (var image = new MagickImage())
                {
                    Channels? channels = null;

                    using (var temporaryDefines = new TemporaryDefines(image))
                    {
                        temporaryDefines.SetArtifact("foo", channels);

                        Assert.Null(image.GetArtifact("foo"));
                    }
                }
            }

            [Fact]
            public void ShouldSetTheNullableValue()
            {
                using (var image = new MagickImage())
                {
                    Channels? channels = Channels.Index;

                    using (var temporaryDefines = new TemporaryDefines(image))
                    {
                        temporaryDefines.SetArtifact("foo", channels);

                        Assert.Equal("Index", image.GetArtifact("foo"));
                    }
                }
            }

            [Fact]
            public void ShouldNotSetTheValueWhenDoubleIsNull()
            {
                using (var image = new MagickImage())
                {
                    double? value = null;

                    using (var temporaryDefines = new TemporaryDefines(image))
                    {
                        temporaryDefines.SetArtifact("foo", value);

                        Assert.Null(image.GetArtifact("foo"));
                    }
                }
            }

            [Fact]
            public void ShouldSetTheDoubleValue()
            {
                using (var image = new MagickImage())
                {
                    double? value = 1.25;

                    using (var temporaryDefines = new TemporaryDefines(image))
                    {
                        temporaryDefines.SetArtifact("foo", value);

                        Assert.Equal("1.25", image.GetArtifact("foo"));
                    }
                }
            }
        }
    }
}
