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
    [TestClass]
    public partial class DrawableAffineTests
    {
        [TestMethod]
        public void Test_Reset()
        {
            DrawableAffine affine = new DrawableAffine();

            Assert.AreEqual(1.0, affine.ScaleX);
            Assert.AreEqual(1.0, affine.ScaleY);
            Assert.AreEqual(0.0, affine.ShearX);
            Assert.AreEqual(0.0, affine.ShearY);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);

            affine.ScaleX = 2.0;
            Assert.AreEqual(2.0, affine.ScaleX);
            Assert.AreEqual(1.0, affine.ScaleY);
            Assert.AreEqual(0.0, affine.ShearX);
            Assert.AreEqual(0.0, affine.ShearY);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);

            affine.ScaleY = 3.0;
            Assert.AreEqual(2.0, affine.ScaleX);
            Assert.AreEqual(3.0, affine.ScaleY);
            Assert.AreEqual(0.0, affine.ShearX);
            Assert.AreEqual(0.0, affine.ShearY);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);

            affine.ShearX = 4.0;
            Assert.AreEqual(2.0, affine.ScaleX);
            Assert.AreEqual(3.0, affine.ScaleY);
            Assert.AreEqual(4.0, affine.ShearX);
            Assert.AreEqual(0.0, affine.ShearY);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);

            affine.ShearY = 5.0;
            Assert.AreEqual(2.0, affine.ScaleX);
            Assert.AreEqual(3.0, affine.ScaleY);
            Assert.AreEqual(4.0, affine.ShearX);
            Assert.AreEqual(5.0, affine.ShearY);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);

            affine.TranslateX = 6.0;
            Assert.AreEqual(2.0, affine.ScaleX);
            Assert.AreEqual(3.0, affine.ScaleY);
            Assert.AreEqual(4.0, affine.ShearX);
            Assert.AreEqual(5.0, affine.ShearY);
            Assert.AreEqual(6.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);

            affine.TranslateY = 7.0;
            Assert.AreEqual(2.0, affine.ScaleX);
            Assert.AreEqual(3.0, affine.ScaleY);
            Assert.AreEqual(4.0, affine.ShearX);
            Assert.AreEqual(5.0, affine.ShearY);
            Assert.AreEqual(6.0, affine.TranslateX);
            Assert.AreEqual(7.0, affine.TranslateY);

            affine.Reset();
            Assert.AreEqual(1.0, affine.ScaleX);
            Assert.AreEqual(1.0, affine.ScaleY);
            Assert.AreEqual(0.0, affine.ShearX);
            Assert.AreEqual(0.0, affine.ShearY);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);
        }

        [TestMethod]
        public void Test_TransformOrigin()
        {
            DrawableAffine affine = new DrawableAffine();
            affine.TransformOrigin(4.0, 2.0);

            Assert.AreEqual(1.0, affine.ScaleX);
            Assert.AreEqual(1.0, affine.ScaleY);
            Assert.AreEqual(0.0, affine.ShearX);
            Assert.AreEqual(0.0, affine.ShearY);
            Assert.AreEqual(4.0, affine.TranslateX);
            Assert.AreEqual(2.0, affine.TranslateY);
        }

        [TestMethod]
        public void Test_TransformRatation()
        {
            DrawableAffine affine = new DrawableAffine();
            affine.TransformRotation(45.0);

            Assert.AreEqual(0.7071, affine.ScaleX, 0.0001);
            Assert.AreEqual(0.7071, affine.ScaleY, 0.0001);
            Assert.AreEqual(-0.7071, affine.ShearX, 0.0001);
            Assert.AreEqual(0.7071, affine.ShearY, 0.0001);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);
        }

        [TestMethod]
        public void Test_TransformScale()
        {
            DrawableAffine affine = new DrawableAffine();
            affine.TransformScale(4.0, 2.0);

            Assert.AreEqual(4.0, affine.ScaleX);
            Assert.AreEqual(2.0, affine.ScaleY);
            Assert.AreEqual(0.0, affine.ShearX);
            Assert.AreEqual(0.0, affine.ShearY);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);
        }

        [TestMethod]
        public void Test_TransformSkew()
        {
            DrawableAffine affine = new DrawableAffine();
            affine.TransformSkewX(4.0);

            Assert.AreEqual(1.0, affine.ScaleX);
            Assert.AreEqual(1.0, affine.ScaleY);
            Assert.AreEqual(0.0699, affine.ShearX, 0.0001);
            Assert.AreEqual(0.0, affine.ShearY);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);

            affine.TransformSkewY(2.0);

            Assert.AreEqual(1.0, affine.ScaleX);
            Assert.AreEqual(1.0024, affine.ScaleY, 0.0001);
            Assert.AreEqual(0.0699, affine.ShearX, 0.0001);
            Assert.AreEqual(0.0349, affine.ShearY, 0.0001);
            Assert.AreEqual(0.0, affine.TranslateX);
            Assert.AreEqual(0.0, affine.TranslateY);
        }
    }
}
