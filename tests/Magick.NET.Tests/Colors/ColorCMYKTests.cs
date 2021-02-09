// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ColorCMYKTests : ColorBaseTests<ColorCMYK>
    {
        [Fact]
        public void Test_IComparable()
        {
            ColorCMYK first = new ColorCMYK(0, 0, 0, 0);

            AssertIComparable(first);

            ColorCMYK second = new ColorCMYK(Quantum.Max, 0, 0, 0);

            Test_IComparable_FirstLower(first, second);

            second = new ColorCMYK(0, 0, 0, 0);

            Test_IComparable_Equal(first, second);
        }

        [Fact]
        public void Test_IEquatable()
        {
            ColorCMYK first = new ColorCMYK(0, Quantum.Max, 0, 0);

            Test_IEquatable_NullAndSelf(first);

            ColorCMYK second = new ColorCMYK(0, Quantum.Max, 0, 0);

            Test_IEquatable_Equal(first, second);

            second = new ColorCMYK(0, 0, Quantum.Max, 0);

            Test_IEquatable_NotEqual(first, second);
        }

        [Fact]
        public void Test_ImplicitOperator()
        {
            ColorCMYK expected = new ColorCMYK(Quantum.Max, 0, 0, 0);
            ColorCMYK actual = new MagickColor(Quantum.Max, 0, 0, 0, Quantum.Max);
            Assert.Equal(expected, actual);

            var magickColor = actual.ToMagickColor();
            Assert.Equal(magickColor, new MagickColor(Quantum.Max, 0, 0, 0, Quantum.Max));

            Assert.Null(ColorCMYK.FromMagickColor(null));
        }

        [Fact]
        public void Test_ToString()
        {
            ColorCMYK color = new ColorCMYK(Quantum.Max, 0, 0, 0);
            AssertToString(color, new MagickColor(Quantum.Max, 0, 0, 0, Quantum.Max));
        }

        [Fact]
        public void Test_Properties()
        {
            ColorCMYK color = new ColorCMYK(0, 0, 0, 0);

            color.C = 1;
            Assert.Equal(1, color.C);
            Assert.Equal(0, color.M);
            Assert.Equal(0, color.Y);
            Assert.Equal(0, color.K);
            Assert.Equal(Quantum.Max, color.A);

            color.M = 2;
            Assert.Equal(1, color.C);
            Assert.Equal(2, color.M);
            Assert.Equal(0, color.Y);
            Assert.Equal(0, color.K);
            Assert.Equal(Quantum.Max, color.A);

            color.Y = 3;
            Assert.Equal(1, color.C);
            Assert.Equal(2, color.M);
            Assert.Equal(3, color.Y);
            Assert.Equal(0, color.K);
            Assert.Equal(Quantum.Max, color.A);

            color.K = 4;
            Assert.Equal(1, color.C);
            Assert.Equal(2, color.M);
            Assert.Equal(3, color.Y);
            Assert.Equal(4, color.K);
            Assert.Equal(Quantum.Max, color.A);

            color.A = 5;
            Assert.Equal(1, color.C);
            Assert.Equal(2, color.M);
            Assert.Equal(3, color.Y);
            Assert.Equal(4, color.K);
            Assert.Equal(5, color.A);
        }

        [Fact]
        public void Test_ColorCMYK()
        {
            ColorCMYK first = new ColorCMYK(0, 0, 0, 0);

            MagickColor second = new MagickColor("cmyk(0,0,0,0)");
            Assert.Equal(second, first.ToMagickColor());

            second = new MagickColor("#fff");
            Assert.NotEqual(second, first.ToMagickColor());

            second = new MagickColor("white");
            Assert.NotEqual(second, first.ToMagickColor());

            first = new ColorCMYK(0, 0, Quantum.Max, 0);

            second = new MagickColor("cmyk(0,0,100%,0)");
            Assert.Equal(second, first.ToMagickColor());

            first = new ColorCMYK(0, 0, Quantum.Max, 0, 0);

            second = new MagickColor("cmyka(0,0,100%,0,0)");
            Assert.Equal(second, first.ToMagickColor());

            first = new ColorCMYK((Percentage)0, (Percentage)100, (Percentage)0, (Percentage)100);
            Assert.Equal(0, first.C);
            Assert.Equal(Quantum.Max, first.M);
            Assert.Equal(0, first.Y);
            Assert.Equal(Quantum.Max, first.K);
            Assert.Equal(Quantum.Max, first.A);

            first = new ColorCMYK((Percentage)100, (Percentage)0, (Percentage)100, (Percentage)0, (Percentage)100);
            Assert.Equal(Quantum.Max, first.C);
            Assert.Equal(0, first.M);
            Assert.Equal(Quantum.Max, first.Y);
            Assert.Equal(0, first.K);
            Assert.Equal(Quantum.Max, first.A);

            first = new ColorCMYK("#0ff0");
            Assert.Equal(0, first.C);
            Assert.Equal(Quantum.Max, first.M);
            Assert.Equal(Quantum.Max, first.Y);
            Assert.Equal(0, first.K);
            Assert.Equal(Quantum.Max, first.A);

            first = new ColorCMYK("#ff00ff00");
            Assert.Equal(Quantum.Max, first.C);
            Assert.Equal(0, first.M);
            Assert.Equal(Quantum.Max, first.Y);
            Assert.Equal(0, first.K);
            Assert.Equal(Quantum.Max, first.A);

            first = new ColorCMYK("#0000ffff0000ffff");
            Assert.Equal(0, first.C);
            Assert.Equal(Quantum.Max, first.M);
            Assert.Equal(0, first.Y);
            Assert.Equal(Quantum.Max, first.K);
            Assert.Equal(Quantum.Max, first.A);

            Assert.Throws<ArgumentException>("color", () =>
            {
                new ColorCMYK("white");
            });

            Assert.Throws<ArgumentException>("color", () =>
            {
                new ColorCMYK("#ff00ff");
            });

            Assert.Throws<ArgumentException>("color", () =>
            {
                new ColorCMYK("#ffff0000fffff");
            });

            Assert.Throws<ArgumentException>("color", () =>
            {
                new ColorCMYK("#ffff0000fffff0000fffff");
            });

            Assert.Throws<ArgumentException>("color", () =>
            {
                new ColorCMYK("#fff");
            });
        }
    }
}
