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

namespace Magick.NET.Tests.Core.Helpers
{
    [TestClass]
    public class EnumHelperTests
    {
        [TestMethod]
        public void Test_Parse()
        {
            ExifDataType? dataType = EnumHelper.Parse(4, ExifDataType.Undefined);
            Assert.AreEqual(ExifDataType.Long, dataType);

            dataType = EnumHelper.Parse(42, ExifDataType.Long);
            Assert.AreEqual(ExifDataType.Long, dataType);

            dataType = EnumHelper.Parse("", ExifDataType.Byte);
            Assert.AreEqual(ExifDataType.Byte, dataType);

            dataType = EnumHelper.Parse("Long", ExifDataType.Undefined);
            Assert.AreEqual(ExifDataType.Long, dataType);

            dataType = EnumHelper.Parse("Longer", ExifDataType.Short);
            Assert.AreEqual(ExifDataType.Short, dataType);

            dataType = EnumHelper.Parse<ExifDataType>("");
            Assert.IsNull(dataType);

            dataType = EnumHelper.Parse<ExifDataType>("Long");
            Assert.AreEqual(ExifDataType.Long, dataType);

            dataType = EnumHelper.Parse<ExifDataType>("Longer");
            Assert.IsNull(dataType);

            dataType = (ExifDataType?)EnumHelper.Parse(typeof(ExifDataType), "");
            Assert.IsNull(dataType);

            dataType = (ExifDataType?)EnumHelper.Parse(typeof(ExifDataType), "Long");
            Assert.AreEqual(ExifDataType.Long, dataType);

            dataType = (ExifDataType?)EnumHelper.Parse(typeof(ExifDataType), "Longer");
            Assert.IsNull(dataType);
        }
    }
}
