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
using NSubstitute;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public class IMagickImageExtensionsTests
    {
        [Fact]
        public void GetInstance_IMagickImageIsNotINativeInstance_ThrowsException()
        {
            var image = Substitute.For<IMagickImage<QuantumType>>();
            Assert.Throws<NotSupportedException>(() =>
            {
                image.GetInstance();
            });
        }

        [Fact]
        public void CreateErrorInfo_ValueIsNull_ReturnsNull()
        {
            IMagickImage<QuantumType> image = null;
            Assert.Null(image.CreateErrorInfo());
        }

        [Fact]
        public void CreateErrorInfo_IMagickImageIsNotMagickImage_ThrowsException()
        {
            IMagickImage<QuantumType> image = Substitute.For<IMagickImage<QuantumType>>();
            Assert.Throws<NotSupportedException>(() =>
            {
                image.CreateErrorInfo();
            });
        }

        [Fact]
        public void SetNext_ValueIsNull_ThrowsException()
        {
            IMagickImage<QuantumType> image = null;
            Assert.Throws<NotSupportedException>(() =>
            {
                image.SetNext(null);
            });
        }
    }
}
