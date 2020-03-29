// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageTests
    {
        public class TheSetArtifactMethod
        {
            [TestClass]
            public class WithBoolean
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenNameIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("name", () =>
                        {
                            image.SetArtifact(null, false);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenNameIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("name", () =>
                        {
                            image.SetArtifact(string.Empty, true);
                        });
                    }
                }

                [TestMethod]
                public void ShouldSetValue()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.SetArtifact("test", true);
                        Assert.AreEqual("true", image.GetArtifact("test"));
                    }
                }
            }

            [TestClass]
            public class WithString
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenNameIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("name", () =>
                        {
                            image.SetArtifact(null, "foo");
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenNameIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("name", () =>
                        {
                            image.SetArtifact(string.Empty, "foo");
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenValueIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("value", () =>
                        {
                            image.SetArtifact("foo", null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldSetEmptyValue()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.SetArtifact("test", string.Empty);
                        Assert.AreEqual(string.Empty, image.GetArtifact("test"));
                    }
                }

                [TestMethod]
                public void ShouldSetValue()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.SetArtifact("test", "123");
                        Assert.AreEqual("123", image.GetArtifact("test"));
                    }
                }
            }
        }
    }
}
