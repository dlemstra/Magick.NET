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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheKmeansMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentNullException>("settings", () => image.Kmeans(null));
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenNumberColorsIsNegative()
            {
                var settings = new KmeansSettings()
                {
                    NumberColors = -1,
                };

                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentException>("settings", () => image.Kmeans(settings));
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMaxIterationsIsNegative()
            {
                var settings = new KmeansSettings()
                {
                    MaxIterations = -1,
                };

                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentException>("settings", () => image.Kmeans(settings));
                }
            }

            [TestMethod]
            public void ShouldSetSeedColorsAsAnArtifact()
            {
                var settings = new KmeansSettings()
                {
                    SeedColors = "red;green",
                    NumberColors = 1,
                };

                using (IMagickImage image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    image.Kmeans(settings);

                    Assert.AreEqual("red;green", image.GetArtifact("kmeans:seed-colors"));
                }
            }

            [TestMethod]
            public void ShouldReduceTheNumberOfColors()
            {
                var settings = new KmeansSettings()
                {
                    NumberColors = 5,
                };

                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Kmeans(settings);

                    ColorAssert.AreEqual(new MagickColor("#f0fb6f8c3098"), image, 430, 225);
                }
            }
        }
    }
}
