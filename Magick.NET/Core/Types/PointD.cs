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

namespace ImageMagick
{
  ///<summary>
  /// Struct for a point with doubles.
  ///</summary>
  public struct PointD : IEquatable<PointD>
  {
    private double _X;
    private double _Y;

    private void Initialize(string value)
    {
      string[] values = value.Split('x');
      Throw.IfTrue(nameof(value), values.Length > 2, "Invalid point specified.");

      double x;
      Throw.IfFalse(nameof(value), double.TryParse(values[0], NumberStyles.Number, CultureInfo.InvariantCulture, out x), "Invalid point specified.");

      double y;
      if (values.Length == 2)
        Throw.IfFalse(nameof(value), double.TryParse(values[1], NumberStyles.Number, CultureInfo.InvariantCulture, out y), "Invalid point specified.");
      else
        y = x;

      _X = x;
      _Y = y;
    }

    internal static PointD FromPointInfo(PointInfo point)
    {
      if (point == null)
        return new PointD();

      return new PointD(point.X, point.Y);
    }

    ///<summary>
    /// Initializes a new instance of the PointD struct using the specified x and y.
    ///</summary>
    ///<param name="xy">The x and y.</param>
    public PointD(double xy)
    {
      _X = xy;
      _Y = xy;
    }

    ///<summary>
    /// Initializes a new instance of the PointD struct using the specified x and y.
    ///</summary>
    ///<param name="x">The x.</param>
    ///<param name="y">The y.</param>
    public PointD(double x, double y)
    {
      _X = x;
      _Y = y;
    }

    ///<summary>
    /// Initializes a new instance of the PointD class using the specified string.
    ///</summary>
    ///<param name="value">PointD specifications in the form: &lt;x&gt;x&lt;y&gt; (where x, y are numbers)</param>
    public PointD(string value)
      : this()
    {
      Throw.IfNullOrEmpty(nameof(value), value);

      Initialize(value);
    }

    ///<summary>
    /// The x-coordinate of this Point.
    ///</summary>
    public double X
    {
      get
      {
        return _X;
      }
    }

    ///<summary>
    /// The y-coordinate of this Point.
    ///</summary>
    public double Y
    {
      get
      {
        return _Y;
      }
    }

    /// <summary>
    /// Determines whether the specified PointD instances are considered equal.
    /// </summary>
    /// <param name="left">The first PointD to compare.</param>
    /// <param name="right"> The second PointD to compare.</param>
    /// <returns></returns>
    public static bool operator ==(PointD left, PointD right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified PointD instances are not considered equal.
    /// </summary>
    /// <param name="left">The first PointD to compare.</param>
    /// <param name="right"> The second PointD to compare.</param>
    /// <returns></returns>
    public static bool operator !=(PointD left, PointD right)
    {
      return !Equals(left, right);
    }

    ///<summary>
    /// Determines whether the specified object is equal to the current point.
    ///</summary>
    ///<param name="obj">The object to compare this point with.</param>
    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;

      if (obj.GetType() != typeof(PointD))
        return false;

      return Equals((PointD)obj);
    }

    ///<summary>
    /// Determines whether the specified point is equal to the current point.
    ///</summary>
    ///<param name="other">The point to compare this point with.</param>
    public bool Equals(PointD other)
    {
      return
        X == other.X &&
        Y == other.Y;
    }

    ///<summary>
    /// Serves as a hash of this type.
    ///</summary>
    public override int GetHashCode()
    {
      return
        X.GetHashCode() ^
        Y.GetHashCode();
    }

    ///<summary>
    /// Returns a string that represents the current PointD.
    ///</summary>
    public override string ToString()
    {
      return string.Format(CultureInfo.InvariantCulture, "{0}x{1}", _X, _Y);
    }
  }
}