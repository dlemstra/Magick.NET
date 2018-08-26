// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("data", () =>
                {
                    new MagickImageCollection((byte[])null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("data", () =>
                {
                    new MagickImageCollection(new byte[0]);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFilIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("file", () =>
                {
                    new MagickImageCollection((FileInfo)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                {
                    new MagickImageCollection((Stream)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                {
                    new MagickImageCollection((string)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("fileName", () =>
                {
                    new MagickImageCollection(string.Empty);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                {
                    new MagickImageCollection(Files.Missing);
                }, "error/blob.c/OpenBlob");
            }
        }
    }
}
