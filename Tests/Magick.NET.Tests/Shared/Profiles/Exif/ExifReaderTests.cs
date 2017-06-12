//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ExifReaderTests
    {
        [TestMethod]
        public void Read_DataIsEmpty_ReturnsEmptyCollection()
        {
            ExifReader reader = new ExifReader();
            byte[] data = new byte[] { };

            Collection<ExifValue> result = reader.Read(data);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Read_DataIsMinimal_ReturnsEmptyCollection()
        {
            ExifReader reader = new ExifReader();
            byte[] data = new byte[] { 69, 120, 105, 102, 0, 0 };

            Collection<ExifValue> result = reader.Read(data);

            Assert.AreEqual(0, result.Count);
        }
    }
}
