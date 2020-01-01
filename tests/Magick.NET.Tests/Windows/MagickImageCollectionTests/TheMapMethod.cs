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

#if WINDOWS_BUILD

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class TheMapMethod
        {
            [TestMethod]
            public void ShouldDitherWhenSpecifiedInSettings()
            {
                using (IMagickImageCollection colors = new MagickImageCollection())
                {
                    colors.Add(new MagickImage(MagickColors.Red, 1, 1));
                    colors.Add(new MagickImage(MagickColors.Green, 1, 1));

                    using (IMagickImage remapImage = colors.AppendHorizontally())
                    {
                        using (IMagickImageCollection collection = new MagickImageCollection())
                        {
                            collection.Read(Files.RoseSparkleGIF);

                            QuantizeSettings settings = new QuantizeSettings
                            {
                                DitherMethod = DitherMethod.FloydSteinberg,
                            };

                            collection.Map(remapImage, settings);

                            ColorAssert.AreEqual(MagickColors.Red, collection[0], 60, 17);
                            ColorAssert.AreEqual(MagickColors.Green, collection[0], 37, 24);

                            ColorAssert.AreEqual(MagickColors.Red, collection[1], 58, 30);
                            ColorAssert.AreEqual(MagickColors.Green, collection[1], 36, 26);

                            ColorAssert.AreEqual(MagickColors.Red, collection[2], 60, 40);
                            ColorAssert.AreEqual(MagickColors.Green, collection[2], 17, 21);
                        }
                    }
                }
            }
        }
    }
}

#endif