//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

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
    [ExcludeFromCodeCoverage]
    internal static class ColorAssert
    {
        private static void AreEqual(MagickColor expected, Pixel actual)
        {
            AreEqual(expected, actual.ToColor());
        }

        private static void AreEqual(QuantumType expected, QuantumType actual, MagickColor actualColor, float delta, string channel)
        {
#if (Q16HDRI)
            if (double.IsNaN(actual))
                actual = 0;
#endif

            Assert.AreEqual(expected, actual, delta, channel + " is not equal (" + actualColor.ToString() + ")");
        }

        private static void AreNotEqual(MagickColor expected, Pixel actual)
        {
            AreNotEqual(expected, actual.ToColor());
        }

        public static void AreEqual(MagickColor expected, MagickColor actual)
        {
            Assert.IsNotNull(actual);

#if (Q16HDRI)
            /* Allow difference of 1 due to rounding issues */
            QuantumType delta = 1;
#else
            QuantumType delta = 0;
#endif

            AreEqual(expected.R, actual.R, actual, delta, "R");
            AreEqual(expected.G, actual.G, actual, delta, "G");
            AreEqual(expected.B, actual.B, actual, delta, "B");
            AreEqual(expected.A, actual.A, actual, delta, "A");
        }

        public static void AreEqual(MagickColor expected, IMagickImage image, int x, int y)
        {
            using (PixelCollection pixels = image.GetPixels())
            {
                AreEqual(expected, pixels.GetPixel(x, y));
            }
        }

        public static void AreNotEqual(MagickColor notExpected, MagickColor actual)
        {
            if (notExpected.R == actual.R && notExpected.G == actual.G &&
               notExpected.B == actual.B && notExpected.A == actual.A)
                Assert.Fail("Colors are the same (" + actual.ToString() + ")");
        }

        public static void AreNotEqual(MagickColor notExpected, IMagickImage image, int x, int y)
        {
            using (PixelCollection collection = image.GetPixels())
            {
                AreNotEqual(notExpected, collection.GetPixel(x, y));
            }
        }

        public static void IsTransparent(float alpha)
        {
            Assert.AreEqual(0, alpha);
        }

        public static void IsNotTransparent(float alpha)
        {
            Assert.AreEqual(Quantum.Max, alpha);
        }
    }
}
