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

namespace ImageMagick
{
  /// <summary>
  /// Base class for colors
  /// </summary>
  public abstract class ColorBase : IEquatable<ColorBase>, IComparable<ColorBase>
  {
    /// <summary>
    /// Initializes a new instance of the ColorBase class using the specified color.
    /// </summary>
    /// <param name="color">The color to use.</param>
    protected ColorBase(MagickColor color)
    {
      Value = color;
    }

    /// <summary>
    /// The actual color of this instance.
    /// </summary>
    protected MagickColor Value
    {
      get;
      private set;
    }

    /// <summary>
    /// Updates the color value from an inherited class.
    /// </summary>
    protected virtual void UpdateValue()
    {
    }

    /// <summary>
    /// Determines whether the specified ColorBase instances are considered equal.
    /// </summary>
    /// <param name="left">The first ColorBase to compare.</param>
    /// <param name="right"> The second ColorBase to compare.</param>
    /// <returns></returns>
    public static bool operator ==(ColorBase left, ColorBase right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified ColorBase instances are not considered equal.
    /// </summary>
    /// <param name="left">The first ColorBase to compare.</param>
    /// <param name="right"> The second ColorBase to compare.</param>
    /// <returns></returns>
    public static bool operator !=(ColorBase left, ColorBase right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Determines whether the first ColorBase is more than the second ColorBase.
    /// </summary>
    /// <param name="left">The first ColorBase to compare.</param>
    /// <param name="right"> The second ColorBase to compare.</param>
    /// <returns></returns>
    public static bool operator >(ColorBase left, ColorBase right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) == 1;
    }

    /// <summary>
    /// Determines whether the first ColorBase is less than the second ColorBase.
    /// </summary>
    /// <param name="left">The first ColorBase to compare.</param>
    /// <param name="right"> The second ColorBase to compare.</param>
    /// <returns></returns>
    public static bool operator <(ColorBase left, ColorBase right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) == -1;
    }

    /// <summary>
    /// Determines whether the first ColorBase is more than or equal to the second ColorBase.
    /// </summary>
    /// <param name="left">The first ColorBase to compare.</param>
    /// <param name="right"> The second ColorBase to compare.</param>
    /// <returns></returns>
    public static bool operator >=(ColorBase left, ColorBase right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether the first ColorBase is less than or equal to the second ColorBase.
    /// </summary>
    /// <param name="left">The first ColorBase to compare.</param>
    /// <param name="right"> The second ColorBase to compare.</param>
    /// <returns></returns>
    public static bool operator <=(ColorBase left, ColorBase right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Converts the specified color to a MagickColor instance.
    /// </summary>
    /// <param name="color">The color to use.</param>
    public static implicit operator MagickColor(ColorBase color)
    {
      if (color == null)
        return null;

      return color.ToMagickColor();
    }

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    /// <param name="other">The object to compare this color with.</param>
    public int CompareTo(ColorBase other)
    {
      if (ReferenceEquals(other, null))
        return 1;

      UpdateValue();
      other.UpdateValue();

      return Value.CompareTo(other.Value);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current color.
    /// </summary>
    /// <param name="obj">The object to compare this color with.</param>
    public override bool Equals(object obj)
    {
      return Equals(obj as ColorBase);
    }

    /// <summary>
    /// Determines whether the specified geometry is equal to the current color.
    /// </summary>
    /// <param name="other">The color to compare this color with.</param>
    public bool Equals(ColorBase other)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      UpdateValue();
      other.UpdateValue();

      return Value.Equals(other.Value);
    }

    /// <summary>
    /// Determines whether the specified geometry is fuzzy equal to the current color.
    /// </summary>
    /// <param name="other">The color to compare this color with.</param>
    /// <param name="fuzz">The fuzz factor.</param>
    public bool FuzzyEquals(ColorBase other, Percentage fuzz)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      UpdateValue();
      other.UpdateValue();

      return Value.FuzzyEquals(other.Value, fuzz);
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    public override int GetHashCode()
    {
      return Value.GetHashCode();
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent MagickColor.
    /// </summary>
    public MagickColor ToMagickColor()
    {
      UpdateValue();

      return new MagickColor(Value);
    }

    /// <summary>
    /// Converts the value of this instance to a hexadecimal string.
    /// </summary>
    public override string ToString()
    {
      return ToMagickColor().ToString();
    }
  }
}