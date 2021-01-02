// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSetArtifactMethod
        {
            public class WithBoolean
            {
                [Fact]
                public void ShouldThrowExceptionWhenNameIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("name", () =>
                        {
                            image.SetArtifact(null, false);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenNameIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("name", () =>
                        {
                            image.SetArtifact(string.Empty, true);
                        });
                    }
                }

                [Fact]
                public void ShouldSetValue()
                {
                    using (var image = new MagickImage())
                    {
                        image.SetArtifact("test", true);
                        Assert.Equal("true", image.GetArtifact("test"));
                    }
                }
            }

            public class WithString
            {
                [Fact]
                public void ShouldThrowExceptionWhenNameIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("name", () =>
                        {
                            image.SetArtifact(null, "foo");
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenNameIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("name", () =>
                        {
                            image.SetArtifact(string.Empty, "foo");
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenValueIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("value", () =>
                        {
                            image.SetArtifact("foo", null);
                        });
                    }
                }

                [Fact]
                public void ShouldSetEmptyValue()
                {
                    using (var image = new MagickImage())
                    {
                        image.SetArtifact("test", string.Empty);
                        Assert.Equal(string.Empty, image.GetArtifact("test"));
                    }
                }

                [Fact]
                public void ShouldSetValue()
                {
                    using (var image = new MagickImage())
                    {
                        image.SetArtifact("test", "123");
                        Assert.Equal("123", image.GetArtifact("test"));
                    }
                }
            }
        }
    }
}
