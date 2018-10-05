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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class ExifValueTests
    {
        [TestClass]
        public class TheTrySetValueMethod
        {
            [TestMethod]
            public void ShouldReturnTheByteValue()
            {
                throw new NotImplementedException();
            }

            [TestMethod]
            public void ShouldReturnTheAsciiValue()
            {
                ExifValue.TryParse(ExifTag.ImageDescription, "Image description", out var result);

                Assert.IsTrue(IsExifValueValid(result));
            }

            [TestMethod]
            public void ShouldReturnTheShortValue()
            {
                ExifValue.TryParse(ExifTag.ColorMap, "1", out var result);

                Assert.IsTrue(IsExifValueValid(result));
            }

            [TestMethod]
            public void ShouldReturnTheLongValue()
            {
                ExifValue.TryParse(ExifTag.ImageWidth, "1", out var result);

                Assert.IsTrue(IsExifValueValid(result));
            }

            [TestMethod]
            public void ShouldReturnTheRationalValue()
            {
                throw new NotImplementedException();
            }

            [TestMethod]
            public void ShouldReturnTheSignedByteValue()
            {
                throw new NotImplementedException();
            }

            [TestMethod]
            public void ShouldReturnTheSignedShortValue()
            {
                ExifValue.TryParse(ExifTag.XClipPathUnits, "-1", out var result);

                Assert.IsTrue(IsExifValueValid(result));
            }

            [TestMethod]
            public void ShouldReturnTheSignedLongValue()
            {
                throw new NotImplementedException();
            }

            [TestMethod]
            public void ShouldReturnTheSignedRationalValue()
            {
                throw new NotImplementedException();
            }

            [TestMethod]
            public void ShouldReturnTheSingleFloatValue()
            {
                throw new NotImplementedException();
            }

            [TestMethod]
            public void ShouldReturnTheDoubleFloatValue()
            {
                throw new NotImplementedException();
            }

            [TestMethod]
            public void ShouldReturnFalse()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Test if the ExifValue value is valid given the data type.
            /// </summary>
            /// <returns>Value to indicate the ExifValue data type and value are valid/consistent.</returns>
            private bool IsExifValueValid(ExifValue value)
            {
                return value.HasValue;
            }
        }
    }
}
