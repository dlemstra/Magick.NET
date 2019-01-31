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
using System.Collections.Generic;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        [TestClass]
        public class TheGetFormatInformationMethod
        {
            [TestMethod]
            public void ShouldReturnFormatInfoForAllFormats()
            {
                List<string> missingFormats = new List<string>();

                foreach (MagickFormat format in Enum.GetValues(typeof(MagickFormat)))
                {
                    if (format == MagickFormat.Unknown)
                        continue;

                    MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(format);
                    if (formatInfo == null)
                        missingFormats.Add(format.ToString());
                }

                if (missingFormats.Count > 0)
                    Assert.Fail("Cannot find MagickFormatInfo for: " + string.Join(", ", missingFormats.ToArray()));
            }
        }
    }
}
