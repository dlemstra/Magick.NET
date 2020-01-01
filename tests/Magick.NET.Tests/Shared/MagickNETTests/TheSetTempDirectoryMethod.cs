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
    public partial class MagickNETTests
    {
        [TestClass]
        public class TheSetTempDirectoryMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenPathIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("path", () =>
                {
                    MagickNET.SetTempDirectory(null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPathIsInvalid()
            {
                ExceptionAssert.Throws<ArgumentException>("path", () =>
                {
                    MagickNET.SetTempDirectory("Invalid");
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenPathIsCorrect()
            {
                MagickNET.SetTempDirectory(Path.GetTempPath());
            }
        }
    }
}
