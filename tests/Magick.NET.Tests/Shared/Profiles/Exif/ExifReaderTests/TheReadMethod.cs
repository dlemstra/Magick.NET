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

using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Profiles.Exif
{
    public partial class ExifReaderTests
    {
        [TestClass]
        public class TheReadMethod
        {
            [TestMethod]
            public void ShouldReturnEmptyCollectionWhenDataIsEmpty()
            {
                var reader = new ExifReader();
                var data = new byte[] { };

                var result = reader.Read(data);

                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public void ShouldReturnEmptyCollectionWhenDataHasNoValues()
            {
                var reader = new ExifReader();
                var data = new byte[] { 69, 120, 105, 102, 0, 0 };

                var result = reader.Read(data);

                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public void ShouldCheckArraySize()
            {
                var reader = new ExifReader();
                var data = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 148, 1, 1, 0, 255, 255, 255, 255, 26, 0, 0, 0, 31, 0, 0, 0, 42 };

                var result = reader.Read(data);

                Assert.AreEqual(0, result.Count);
                Assert.AreEqual(1, reader.InvalidTags.Count());
            }
        }
    }
}
