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
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageInfoFactoryTests
    {
        public partial class TheCreateMethod
        {
            [TestMethod]
            public void ShouldCreateMagickImageInfo()
            {
                var factory = new MagickImageInfoFactory();
                var info = factory.Create();

                Assert.IsInstanceOfType(info, typeof(MagickImageInfo));
                Assert.AreEqual(0, info.Width);
            }

            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { });
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageInfoFactory();
                    var data = File.ReadAllBytes(Files.ImageMagickJPG);

                    var info = factory.Create(data);

                    Assert.IsInstanceOfType(info, typeof(MagickImageInfo));
                    Assert.AreEqual(123, info.Width);
                }
            }

            [TestClass]
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create(null, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.Create(new byte[] { 215 }, -1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, -1);
                    });
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.Create((FileInfo)null);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageInfoFactory();
                    var file = new FileInfo(Files.ImageMagickJPG);

                    var info = factory.Create(file);

                    Assert.IsInstanceOfType(info, typeof(MagickImageInfo));
                    Assert.AreEqual(123, info.Width);
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.Create(string.Empty);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageInfoFactory();

                    var info = factory.Create(Files.ImageMagickJPG);

                    Assert.IsInstanceOfType(info, typeof(MagickImageInfo));
                    Assert.AreEqual(123, info.Width);
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.Create((Stream)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageInfoFactory();

                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.Create(new MemoryStream());
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageInfoFactory();

                    using (var stream = File.OpenRead(Files.ImageMagickJPG))
                    {
                        var info = factory.Create(stream);

                        Assert.IsInstanceOfType(info, typeof(MagickImageInfo));
                        Assert.AreEqual(123, info.Width);
                    }
                }
            }
        }
    }
}
