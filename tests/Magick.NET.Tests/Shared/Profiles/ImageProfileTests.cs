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

using System;
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ImageProfileTests
    {
        [TestMethod]
        public void Test_Constructor()
        {
            ExceptionAssert.Throws<ArgumentNullException>("name", () =>
            {
                new ImageProfile(null, Files.SnakewarePNG);
            });

            ExceptionAssert.Throws<ArgumentException>("name", () =>
            {
                new ImageProfile(string.Empty, Files.SnakewarePNG);
            });

            ExceptionAssert.Throws<ArgumentNullException>("data", () =>
            {
                new ImageProfile("name", (byte[])null);
            });

            ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
            {
                new ImageProfile("name", (Stream)null);
            });

            ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
            {
                new ImageProfile("name", (string)null);
            });

            ExceptionAssert.Throws<ArgumentException>("fileName", () =>
            {
                new ImageProfile("name", string.Empty);
            });
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var first = image.GetIptcProfile();

                Assert.IsFalse(first == null);
                Assert.IsFalse(first.Equals(null));
                Assert.IsTrue(first.Equals(first));
                Assert.IsTrue(first.Equals((object)first));

                var second = image.GetIptcProfile();
                Assert.IsNotNull(second);

                Assert.IsTrue(first.Equals(second));
                Assert.IsTrue(first.Equals((object)second));

                second = new IptcProfile(new byte[] { 0 });

                Assert.IsTrue(first != second);
                Assert.IsFalse(first.Equals(second));
            }
        }

        [TestMethod]
        public void Test_ToByteArray()
        {
            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.GetIptcProfile();
                Assert.IsNotNull(profile);

                Assert.AreEqual(273, profile.ToByteArray().Length);
            }
        }
    }
}
