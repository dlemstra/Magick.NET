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
    public partial class ExifProfileTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenStreamNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                {
                    new ExifProfile((Stream)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                {
                    new ExifProfile((string)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenDataNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                {
                    new ExifProfile((byte[])null);
                });
            }

            [TestMethod]
            public void ShouldAllowEmptyStream()
            {
                using (var image = new MagickImage())
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        var profile = new ExifProfile(memStream);
                        image.SetProfile(profile);
                    }
                }
            }

            [TestMethod]
            public void ShouldAllowEmptyData()
            {
                using (var image = new MagickImage())
                {
                    var data = new byte[] { };
                    var profile = new ExifProfile(data);
                    image.SetProfile(profile);
                }
            }
        }
    }
}
