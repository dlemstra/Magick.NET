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

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheHasProfileMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenNameIsNull()
            {
                using (var image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentNullException>("name", () => image.HasProfile(null));
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenNameIsEmpty()
            {
                using (var image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentException>("name", () => image.HasProfile(string.Empty));
                }
            }

            [TestMethod]
            public void ShouldReturnTrueWhenImageHasProfileWithTheSpecifiedName()
            {
                using (var image = new MagickImage(Files.InvitationTIF))
                {
                    Assert.IsTrue(image.HasProfile("icc"));
                }
            }

            [TestMethod]
            public void ShouldReturnFalseWhenImageDoesNotHaveProfileWithTheSpecifiedName()
            {
                using (var image = new MagickImage(Files.InvitationTIF))
                {
                    Assert.IsFalse(image.HasProfile("foo"));
                }
            }
        }
    }
}
