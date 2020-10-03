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

using System.Linq;
using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheSupportedFormatsProperty
        {
            [Fact]
            public void ShouldContainNoFormatInformationWithMagickFormatSetToUnknown()
            {
                foreach (var formatInfo in MagickNET.SupportedFormats)
                {
                    if (formatInfo.Format == MagickFormat.Unknown)
                        throw new XunitException("Unknown format: " + formatInfo.Description + " (" + formatInfo.ModuleFormat + ")");
                }
            }

            [Fact]
            public void ShouldContainTheCorrectNumberOfFormats()
            {
#if WINDOWS_BUILD
                Assert.Equal(255, MagickNET.SupportedFormats.Count());
#else
                Assert.Equal(251, MagickNET.SupportedFormats.Count());
#endif
            }
        }
    }
}
