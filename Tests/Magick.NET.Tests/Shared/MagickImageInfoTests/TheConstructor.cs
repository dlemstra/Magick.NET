// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageInfoTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenDataIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("data", () => new MagickImageInfo((byte[])null));
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("data", () => new MagickImageInfo(new byte[0]));
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("file", () => new MagickImageInfo((FileInfo)null));
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("stream", () => new MagickImageInfo((Stream)null));
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("fileName", () => new MagickImageInfo((string)null));
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("fileName", () => new MagickImageInfo(string.Empty));
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                {
                    new MagickImageInfo(Files.Missing);
                }, "error/blob.c/OpenBlob");
            }
        }
    }
}
