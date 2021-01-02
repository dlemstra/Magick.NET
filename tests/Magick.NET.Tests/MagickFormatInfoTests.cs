// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Xunit;

namespace Magick.NET.Tests
{
    public class MagickFormatInfoTests
    {
        [Fact]
        public void Test_IEquatable()
        {
            var first = MagickFormatInfo.Create(MagickFormat.Png);
            var second = MagickNET.GetFormatInformation(Files.SnakewarePNG);

            Assert.True(first == second);
            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
        }

        [Fact]
        public void Test_Properties()
        {
            var formatInfo = MagickNET.GetFormatInformation(MagickFormat.Gradient);
            Assert.NotNull(formatInfo);
            Assert.Equal(MagickFormat.Gradient, formatInfo.Format);
            Assert.True(formatInfo.CanReadMultithreaded);
            Assert.True(formatInfo.CanWriteMultithreaded);
            Assert.Equal("Gradual linear passing from one shade to another", formatInfo.Description);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.False(formatInfo.IsWritable);
            Assert.Null(formatInfo.MimeType);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Jp2);
            Assert.NotNull(formatInfo);
            Assert.Equal(MagickFormat.Jp2, formatInfo.Format);
            Assert.True(formatInfo.CanReadMultithreaded);
            Assert.True(formatInfo.CanWriteMultithreaded);
            Assert.Equal("JPEG-2000 File Format Syntax", formatInfo.Description);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.True(formatInfo.IsWritable);
            Assert.Equal("image/jp2", formatInfo.MimeType);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Jpg);
            Assert.NotNull(formatInfo);
            Assert.True(formatInfo.CanReadMultithreaded);
            Assert.True(formatInfo.CanWriteMultithreaded);
            Assert.Equal("Joint Photographic Experts Group JFIF format", formatInfo.Description);
            Assert.Equal(MagickFormat.Jpg, formatInfo.Format);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.True(formatInfo.IsWritable);
            Assert.Equal("image/jpeg", formatInfo.MimeType);
            Assert.Equal(MagickFormat.Jpeg, formatInfo.ModuleFormat);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Png);
            Assert.NotNull(formatInfo);
            Assert.True(formatInfo.CanReadMultithreaded);
            Assert.True(formatInfo.CanWriteMultithreaded);
            Assert.Equal("Portable Network Graphics", formatInfo.Description);
            Assert.Equal(MagickFormat.Png, formatInfo.Format);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.True(formatInfo.IsWritable);
            Assert.Equal("image/png", formatInfo.MimeType);
            Assert.Equal(MagickFormat.Png, formatInfo.ModuleFormat);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Pango);
            Assert.NotNull(formatInfo);
            Assert.False(formatInfo.CanReadMultithreaded);
            Assert.False(formatInfo.CanWriteMultithreaded);
            Assert.Equal("Pango Markup Language", formatInfo.Description);
            Assert.Equal(MagickFormat.Pango, formatInfo.Format);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.False(formatInfo.IsWritable);
            Assert.Null(formatInfo.MimeType);
            Assert.Equal(MagickFormat.Pango, formatInfo.ModuleFormat);
        }

        [Fact]
        public void Test_Unregister()
        {
            var formatInfo = MagickNET.GetFormatInformation(MagickFormat.X3f);
            Assert.NotNull(formatInfo);
            Assert.True(formatInfo.Unregister());

            var settings = new MagickReadSettings
            {
                Format = MagickFormat.X3f,
            };

            Assert.Throws<MagickMissingDelegateErrorException>(() =>
            {
                var image = new MagickImage();
                image.Read(new byte[] { 1, 2, 3, 4 }, settings);
            });
        }
    }
}
