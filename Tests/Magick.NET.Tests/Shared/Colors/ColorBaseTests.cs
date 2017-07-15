// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public abstract class ColorBaseTests<TColor>
      where TColor : ColorBase
    {
        protected static void Test_IComparable(TColor first)
        {
            Assert.AreEqual(0, first.CompareTo(first));
            Assert.AreEqual(1, first.CompareTo(null));
            Assert.IsFalse(first < null);
            Assert.IsFalse(first <= null);
            Assert.IsTrue(first > null);
            Assert.IsTrue(first >= null);
            Assert.IsTrue(null < first);
            Assert.IsTrue(null <= first);
            Assert.IsFalse(null > first);
            Assert.IsFalse(null >= first);
        }

        protected static void Test_IComparable_Equal(TColor first, TColor second)
        {
            Assert.AreEqual(0, first.CompareTo(second));
            Assert.IsFalse(first < second);
            Assert.IsTrue(first <= second);
            Assert.IsFalse(first > second);
            Assert.IsTrue(first >= second);
        }

        protected static void Test_IComparable_FirstLower(TColor first, TColor second)
        {
            Assert.AreEqual(-1, first.CompareTo(second));
            Assert.IsTrue(first < second);
            Assert.IsTrue(first <= second);
            Assert.IsFalse(first > second);
            Assert.IsFalse(first >= second);
        }

        protected static void Test_IEquatable_NotEqual(TColor first, TColor second)
        {
            Assert.IsTrue(first != second);
            Assert.IsFalse(first.Equals(second));
        }

        protected static void Test_IEquatable_Equal(TColor first, TColor second)
        {
            Assert.IsTrue(first == second);
            Assert.IsTrue(first.Equals(second));
        }

        protected static void Test_IEquatable_NullAndSelf(TColor first)
        {
            Assert.IsFalse(first == null);
            Assert.IsFalse(first.Equals(null));
            Assert.IsTrue(first.Equals(first));
            Assert.IsTrue(first.Equals((object)first));
        }

        protected static void Test_ToString(TColor color, MagickColor expected)
        {
            Assert.AreEqual(color.ToString(), expected.ToString());
        }
    }
}
