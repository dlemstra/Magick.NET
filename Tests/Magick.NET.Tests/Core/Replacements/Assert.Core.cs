//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

using System;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
  public static class Assert
  {
    public static void AreEqual<T>(T a, T b)
    {
      Xunit.Assert.Equal(a, b);
    }

    public static void AreEqual<T>(T a, T b, string message)
    {
      if (!object.Equals(a, b))
        Fail(message);
    }

    public static void AreEqual(double a, double b, double precision)
    {
      Xunit.Assert.False(Math.Abs(a - b) > precision);
    }

    public static void AreEqual(QuantumType a, QuantumType b, double precision)
    {
      AreEqual((double)a, (double)b, precision);
    }

    public static void AreEqual(double a, double b, double precision, string message)
    {
      if (Math.Abs(a - b) > precision)
        Fail(message);
    }

    public static void AreEqual(QuantumType a, QuantumType b, double precision, string message)
    {
      AreEqual((double)a, (double)b, precision, message);
    }

    public static void AreNotEqual<T>(T a, T b)
    {
      Xunit.Assert.NotEqual(a, b);
    }

    public static void AreNotEqual<T>(T a, T b, string message)
    {
      if (object.Equals(a, b))
        Fail(message);
    }

    public static void Fail(string message)
    {
      Xunit.Assert.True(false, message);
    }

    public static void Inconclusive(string message)
    {
    }

    public static void IsFalse(bool condition)
    {
      Xunit.Assert.False(condition);
    }

    public static void IsNull(object value)
    {
      Xunit.Assert.Null(value);
    }

    public static void IsNotNull(object value)
    {
      Xunit.Assert.NotNull(value);
    }

    public static void IsTrue(bool condition)
    {
      Xunit.Assert.True(condition);
    }

    public static void IsTrue(bool condition, string message, params object[] parameters)
    {
      if (!condition)
        Fail(string.Format(message, parameters));
    }
  }
}
