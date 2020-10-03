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
using System.Linq;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class IptcValueTests
    {
        [Fact]
        public void Encoding_SetToNull_NotChanged()
        {
            var value = GetIptcValue();

            value.Encoding = null;

            Assert.NotNull(value.Encoding);
        }

        [Fact]
        public void Test_Encoding()
        {
            var value = GetIptcValue();

            Assert.Equal("Communications", value.Value);

            value.Encoding = Encoding.UTF32;
            Assert.NotEqual("Communications", value.Value);

            value.Value = "Communications";
            Assert.Equal("Communications", value.Value);

            value.Encoding = Encoding.UTF8;
            Assert.NotEqual("Communications", value.Value);
        }

        [Fact]
        public void Test_IEquatable()
        {
            var first = GetIptcValue();
            var second = GetIptcValue();

            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
        }

        [Fact]
        public void Test_Properties()
        {
            var value = GetIptcValue();

            Assert.Equal(IptcTag.Caption, value.Tag);
            Assert.Equal("Communications", value.ToString());
            Assert.Equal("Communications", value.Value);
            Assert.Equal(14, value.ToByteArray().Length);
        }

        [Fact]
        public void Test_ToString()
        {
            var value = GetIptcValue();

            Assert.Equal("Communications", value.ToString());
            Assert.Equal("Communications", value.ToString(Encoding.UTF8));
            Assert.NotEqual("Communications", value.ToString(Encoding.UTF32));

            value.Encoding = Encoding.UTF32;
            value.Value = "Test";
            Assert.Equal("Test", value.ToString());
            Assert.Equal("Test", value.ToString(Encoding.UTF32));
            Assert.NotEqual("Test", value.ToString(Encoding.UTF8));

            value.Value = string.Empty;
            Assert.Equal(string.Empty, value.ToString());
            value.Value = "Test";
            Assert.Equal("Test", value.ToString());
            value.Value = null;
            Assert.Equal(string.Empty, value.ToString());
        }

        private static IIptcValue GetIptcValue()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.GetIptcProfile();
                return profile.Values.ElementAt(1);
            }
        }
    }
}
