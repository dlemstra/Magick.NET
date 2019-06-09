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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class EndianReaderTests
    {
        [TestClass]
        public class TheReadStringMethod : EndianReaderTests
        {
            [TestMethod]
            public void ShouldReturnNullWhenBufferIsNotLongEnough()
            {
                var reader = new EndianReader(new byte[1] { 0 });

                var result = reader.ReadString(2);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void ShouldReadString()
            {
                var reader = new EndianReader(new byte[12] { 77, 97, 103, 105, 99, 107, 46, 78, 69, 84, 0, 77 });

                var result = reader.ReadString(10);

                Assert.AreEqual("Magick.NET", result);
            }

            [TestMethod]
            public void ShouldChangeTheIndex()
            {
                var reader = new EndianReader(new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

                reader.ReadString(10);

                Assert.AreEqual(10U, reader.Index);
            }
        }
    }
}
