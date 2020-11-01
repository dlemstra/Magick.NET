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

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheMslCoder
    {
        [Fact]
        public void ShouldBeDisabled()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memStream))
                {
                    writer.Write(@"
                        <?xml version=""1.0"" encoding=""UTF-8""?>
                        <image>
                          <read filename=""/tmp/text.gif"" />
                        </image>");

                    writer.Flush();

                    memStream.Position = 0;

                    using (var image = new MagickImage())
                    {
                        var readSettings = new MagickReadSettings
                        {
                            Format = MagickFormat.Msl,
                        };

                        Assert.Throws<MagickPolicyErrorException>(() =>
                        {
                            image.Read(memStream, readSettings);
                        });
                    }
                }
            }
        }
    }
}