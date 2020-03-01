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
        public class TheFormatExpressionMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenExpressionIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentNullException>("expression", () => image.FormatExpression(null));
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenExpressionIsEmpty()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentException>("expression", () => image.FormatExpression(string.Empty));
                }
            }

            [TestMethod]
            public void ShouldReturnProfiles()
            {
                using (IMagickImage image = new MagickImage(Files.InvitationTIF))
                {
                    Assert.AreEqual("sRGB IEC61966-2.1", image.FormatExpression("%[profile:icc]"));
                }
            }

            [TestMethod]
            public void ShouldReturnSignature()
            {
                using (IMagickImage image = new MagickImage(Files.RedPNG))
                {
                    Assert.AreEqual("92f59c51ad61b99b3c9ebd51f1c77b9c80c0478e2fdb7db47831376b1e4a00db", image.FormatExpression("%#"));
                }
            }

            [TestMethod]
            public void ShouldRaiseWarningForInvalidExpression()
            {
                int count = 0;
                EventHandler<WarningEventArgs> warningDelegate = (sender, arguments) =>
                {
                    Assert.IsNotNull(sender);
                    Assert.IsNotNull(arguments);
                    Assert.IsNotNull(arguments.Message);
                    Assert.AreNotEqual(string.Empty, arguments.Message);
                    Assert.IsNotNull(arguments.Exception);

                    count++;
                };

                using (IMagickImage image = new MagickImage(Files.RedPNG))
                {
                    image.Warning += warningDelegate;
                    var result = image.FormatExpression("%EOO");
                    image.Warning -= warningDelegate;

                    Assert.AreEqual("OO", result);
                    Assert.AreEqual(1, count);
                }
            }
        }
    }
}
