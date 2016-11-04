//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Globalization;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
  /// <summary>
  /// Represents a percentage value.
  /// </summary>
  public struct Percentage : IEquatable<Percentage>, IComparable<Percentage>
  {
    private double _Value;

    internal static Percentage FromQuantum(double value)
    {
      return new Percentage((value / Quantum.Max) * 100);
    }

    internal QuantumType ToQuantum()
    {
      return (QuantumType)(Quantum.Max * (_Value / 100));
    }

    /// <summary>
    /// Initializes a new instance of the Percentage class using the specified value.
    /// (0% = 0.0, 100% = 100.0)
    /// </summary>
    /// <param name="value">The value (0% = 0.0, 100% = 100.0)</param>
    public Percentage(double value)
    {
      _Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the Percentage class using the specified value.
    /// (0% = 0, 100% = 100)
    /// </summary>
    /// <param name="value">The value (0% = 0, 100% = 100)</param>
    public Percentage(int value)
    {
      _Value = value;
    }

    /// <summary>
    /// Determines whether the specified Percentage instances are considered equal.
    /// </summary>
    /// <param name="left">The first Percentage to compare.</param>
    /// <param name="right"> The second Percentage to compare.</param>
    /// <returns></returns>
    public static bool operator ==(Percentage left, Percentage right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified Percentage instances are not considered equal.
    /// </summary>
    /// <param name="left">The first Percentage to compare.</param>
    /// <param name="right"> The second Percentage to compare.</param>
    /// <returns></returns>
    public static bool operator !=(Percentage left, Percentage right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Determines whether the first Percentage is more than the second Percentage.
    /// </summary>
    /// <param name="left">The first Percentage to compare.</param>
    /// <param name="right"> The second Percentage to compare.</param>
    /// <returns></returns>
    public static bool operator >(Percentage left, Percentage right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) == 1;
    }

    /// <summary>
    /// Determines whether the first Percentage is less than the second Percentage.
    /// </summary>
    /// <param name="left">The first Percentage to compare.</param>
    /// <param name="right"> The second Percentage to compare.</param>
    /// <returns></returns>
    public static bool operator <(Percentage left, Percentage right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) == -1;
    }

    /// <summary>
    /// Determines whether the first Percentage is less than or equal to the second Percentage.
    /// </summary>
    /// <param name="left">The first Percentage to compare.</param>
    /// <param name="right"> The second Percentage to compare.</param>
    /// <returns></returns>
    public static bool operator >=(Percentage left, Percentage right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether the first Percentage is less than or equal to the second Percentage.
    /// </summary>
    /// <param name="left">The first Percentage to compare.</param>
    /// <param name="right"> The second Percentage to compare.</param>
    /// <returns></returns>
    public static bool operator <=(Percentage left, Percentage right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Multiplies the value by the percentage.
    /// </summary>
    /// <param name="value"> The value to use.</param>
    /// <param name="percentage">The Percentage to use.</param>
    /// <returns></returns>
    public static double operator *(double value, Percentage percentage)
    {
      return percentage.Multiply(value);
    }

    /// <summary>
    /// Multiplies the value by the percentage.
    /// </summary>
    /// <param name="value"> The value to use.</param>
    /// <param name="percentage">The Percentage to use.</param>
    /// <returns></returns>
    public static int operator *(int value, Percentage percentage)
    {
      return percentage.Multiply(value);
    }

    /// <summary>
    /// Converts the specified double to an instance of this type.
    /// </summary>
    /// <param name="value">The value (0% = 0, 100% = 100)</param>
    public static explicit operator Percentage(double value)
    {
      return new Percentage(value);
    }

    /// <summary>
    /// Converts the specified int to an instance of this type.
    /// </summary>
    /// <param name="value">The value (0% = 0, 100% = 100)</param>
    public static explicit operator Percentage(int value)
    {
      return new Percentage(value);
    }

    /// <summary>
    /// Converts the specified Percentage to a double.
    /// </summary>
    /// <param name="percentage">The percentage to convert</param>
    /// <returns></returns>
    public static explicit operator double(Percentage percentage)
    {
      return percentage.ToDouble();
    }

    /// <summary>
    /// Converts the Percentage to a quantum type.
    /// </summary>
    /// <param name="percentage">The percentage to convert</param>
    /// <returns></returns>
    public static explicit operator int(Percentage percentage)
    {
      return percentage.ToInt32();
    }

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    /// <param name="other">The object to compare this percentage with.</param>
    public int CompareTo(Percentage other)
    {
      return _Value.CompareTo(other._Value);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current percentage.
    /// </summary>
    /// <param name="obj">The object to compare this percentage with.</param>
    public override bool Equals(object obj)
    {

      if (obj == null)
        return false;

      if (obj.GetType() == typeof(Percentage))
        return Equals((Percentage)obj);

      if (obj.GetType() == typeof(double))
        return _Value.Equals(obj);

      if (obj.GetType() == typeof(int))
        return ((int)_Value).Equals((int)obj);

      return false;
    }

    /// <summary>
    /// Determines whether the specified percentage is equal to the current percentage.
    /// </summary>
    /// <param name="other">The percentage to compare this percentage with.</param>
    public bool Equals(Percentage other)
    {
      return _Value.Equals(other._Value);
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    public override int GetHashCode()
    {
      return _Value.GetHashCode();
    }

    /// <summary>
    /// Multiplies the value by the percentage.
    /// </summary>
    public double Multiply(double value)
    {
      return (value * _Value) / 100.0;
    }

    /// <summary>
    /// Multiplies the value by the percentage.
    /// </summary>
    public int Multiply(int value)
    {
      return (int)((value * _Value) / 100.0);
    }

    /// <summary>
    /// Returns a double that represents the current percentage.
    /// </summary>
    public double ToDouble()
    {
      return _Value;
    }

    /// <summary>
    /// Returns an integer that represents the current percentage.
    /// </summary>
    public int ToInt32()
    {
      return (int)_Value;
    }

    /// <summary>
    /// Returns a string that represents the current percentage.
    /// </summary>
    public override string ToString()
    {
      return string.Format(CultureInfo.InvariantCulture, "{0:0.##}%", _Value);
    }
  }
}
