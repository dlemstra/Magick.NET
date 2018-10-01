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
