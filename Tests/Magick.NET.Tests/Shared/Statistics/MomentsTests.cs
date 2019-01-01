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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class MomentsTests
    {
        [TestMethod]
        public void Test_Moments()
        {
            using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
            {
                Moments moments = image.Moments();
                Assert.IsNotNull(moments);
                ChannelMoments first = moments.GetChannel(PixelChannel.Red);
                Assert.IsNotNull(first);

                Assert.AreEqual(PixelChannel.Red, first.Channel);
                Assert.AreEqual(56.59, first.Centroid.X, 0.01);
                Assert.AreEqual(56.00, first.Centroid.Y, 0.01);
                Assert.AreEqual(148.92, first.EllipseAngle, 0.01);
                Assert.AreEqual(73.53, first.EllipseAxis.X, 0.01);
                Assert.AreEqual(66.82, first.EllipseAxis.Y, 0.01);
                Assert.AreEqual(0.30, first.EllipseEccentricity, 0.01);
                Assert.AreEqual(0.79, first.EllipseIntensity, 0.01);

                double[] expected = new double[] { 0.2004, 0.0003, 0.0001, 0.0, 0.0, 0.0, 0.0, 0.0 };
                for (int i = 0; i < 8; i++)
                {
                    Assert.AreEqual(expected[i], first.HuInvariants(i), 0.0001);
                }

                moments = image.Moments();
                ChannelMoments second = moments.GetChannel(PixelChannel.Red);

                Assert.IsTrue(first.Centroid == second.Centroid);
                Assert.IsTrue(first.Centroid.Equals(second.Centroid));
                Assert.IsTrue(first.Centroid.Equals((object)second.Centroid));

                ExceptionAssert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    first.HuInvariants(9);
                });
            }
        }
    }
}
