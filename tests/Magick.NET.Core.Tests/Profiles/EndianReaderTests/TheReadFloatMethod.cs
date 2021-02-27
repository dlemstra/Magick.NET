// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class EndianReaderTests
    {
        public class TheReadFloatMethod : EndianReaderTests
        {
            [Fact]
            public void ShouldReturnNullWhenBufferIsNotLongEnough()
            {
                var reader = new EndianReader(new byte[1] { 0 });

                var result = reader.ReadFloat();

                Assert.Null(result);
            }

            [Fact]
            public void ShouldReadSingleBigEndian()
            {
                var reader = new EndianReader(new byte[4] { 69, 169, 215, 43 });

                var result = reader.ReadFloat();

                Assert.Equal(5434.896f, result);
            }

            [Fact]
            public void ShouldReadSingleLittleEndian()
            {
                var reader = new EndianReader(new byte[4] { 43, 215, 169, 69 });
                reader.IsLittleEndian = true;

                var result = reader.ReadFloat();

                Assert.Equal(5434.896f, result);
            }

            [Fact]
            public void ShouldChangeTheIndex()
            {
                var reader = new EndianReader(new byte[4] { 0, 0, 0, 0 });

                reader.ReadFloat();

                Assert.Equal(4U, reader.Index);
            }
        }
    }
}
