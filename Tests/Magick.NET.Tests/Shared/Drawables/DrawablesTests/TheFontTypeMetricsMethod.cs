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
    public partial class DrawablesTests
    {
        [TestClass]
        public class TheFontTypeMetricsMethod
        {
            [TestMethod]
            public void ShouldReturnTheCorrectTypeMetrics()
            {
                var drawables = new Drawables()
                    .Font("Arial")
                    .FontPointSize(15);

                var typeMetric = drawables.FontTypeMetrics("Magick.NET");

                Assert.IsNotNull(typeMetric);
                Assert.AreEqual(14, typeMetric.Ascent);
                Assert.AreEqual(-4, typeMetric.Descent);
                Assert.AreEqual(30, typeMetric.MaxHorizontalAdvance);
                Assert.AreEqual(18, typeMetric.TextHeight);
                Assert.AreEqual(82, typeMetric.TextWidth);
                Assert.AreEqual(-4.5625, typeMetric.UnderlinePosition);
                Assert.AreEqual(2.34375, typeMetric.UnderlineThickness);
            }

            [TestMethod]
            public void ShouldUseTheFontSize()
            {
                var drawables = new Drawables()
                    .Font("Arial")
                    .FontPointSize(150);

                var typeMetric = drawables.FontTypeMetrics("Magick.NET");

                Assert.IsNotNull(typeMetric);
                Assert.AreEqual(136, typeMetric.Ascent);
                Assert.AreEqual(-32, typeMetric.Descent);
                Assert.AreEqual(300, typeMetric.MaxHorizontalAdvance);
                Assert.AreEqual(168, typeMetric.TextHeight);
                Assert.AreEqual(816, typeMetric.TextWidth);
                Assert.AreEqual(-4.5625, typeMetric.UnderlinePosition);
                Assert.AreEqual(2.34375, typeMetric.UnderlineThickness);
            }
        }
    }
}
