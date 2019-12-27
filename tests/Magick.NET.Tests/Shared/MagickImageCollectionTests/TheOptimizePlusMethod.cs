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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheOptimizePlusMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() => images.OptimizePlus());
                }
            }

            [TestMethod]
            public void ShouldRemoveDuplicateImages()
            {
                using (IMagickImageCollection collection = new MagickImageCollection())
                {
                    collection.Add(new MagickImage(MagickColors.Red, 11, 11));
                    /* The second image will not be removed if it is a duplicate so we need to add an extra one. */
                    collection.Add(new MagickImage(MagickColors.Red, 11, 11));
                    collection.Add(new MagickImage(MagickColors.Red, 11, 11));

                    var image = new MagickImage(MagickColors.Red, 11, 11);
                    using (var pixels = image.GetPixels())
                    {
                        pixels.SetPixel(5, 5, new QuantumType[] { 0, Quantum.Max, 0 });
                    }

                    collection.Add(image);
                    collection.OptimizePlus();

                    Assert.AreEqual(3, collection.Count);

                    Assert.AreEqual(1, collection[1].Width);
                    Assert.AreEqual(1, collection[1].Height);
                    Assert.AreEqual(-1, collection[1].Page.X);
                    Assert.AreEqual(-1, collection[1].Page.Y);
                    ColorAssert.AreEqual(new MagickColor("#FF000000"), collection[1], 0, 0);

                    Assert.AreEqual(1, collection[2].Width);
                    Assert.AreEqual(1, collection[2].Height);
                    Assert.AreEqual(5, collection[2].Page.X);
                    Assert.AreEqual(5, collection[2].Page.Y);
                    ColorAssert.AreEqual(MagickColors.Lime, collection[2], 0, 0);
                }
            }
        }
    }
}
