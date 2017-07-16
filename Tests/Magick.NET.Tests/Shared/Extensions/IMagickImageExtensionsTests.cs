// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using NSubstitute;

namespace Magick.NET.Tests.Shared.Extensions
{
    [TestClass]
    public class IMagickImageExtensionsTests
    {
        [TestMethod]
        public void GetInstance_IMagickImageIsNotINativeInstance_ThrowsException()
        {
            IMagickImage image = Substitute.For<IMagickImage>();
            ExceptionAssert.Throws<NotSupportedException>(() =>
            {
                image.GetInstance();
            });
        }

        [TestMethod]
        public void CreateErrorInfo_ValueIsNull_ReturnsNull()
        {
            IMagickImage image = null;
            Assert.IsNull(image.CreateErrorInfo());
        }

        [TestMethod]
        public void CreateErrorInfo_IMagickImageIsNotMagickImage_ThrowsException()
        {
            IMagickImage image = Substitute.For<IMagickImage>();
            ExceptionAssert.Throws<NotSupportedException>(() =>
            {
                image.CreateErrorInfo();
            });
        }

        [TestMethod]
        public void SetNext_ValueIsNull_ThrowsException()
        {
            IMagickImage image = null;
            ExceptionAssert.Throws<NotSupportedException>(() =>
            {
                image.SetNext(null);
            });
        }
    }
}
