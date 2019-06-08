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
using System.Globalization;

namespace ImageMagick
{
    /// <summary>
    /// Struct for a point with doubles.
    /// </summary>
    public struct PointD : IEquatable<PointD>
    {
        private double _x;
        private double _y;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointD"/> struct.
        /// </summary>
        /// <param name="xy">The x and y.</param>
        public PointD(double xy)
        {
            _x = xy;
            _y = xy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointD"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public PointD(double x, double y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointD"/> struct.
        /// </summary>
        /// <param name="value">PointD specifications in the form: &lt;x&gt;x&lt;y&gt; (where x, y are numbers).</param>
        public PointD(string value)
          : this()
        {
            Throw.IfNullOrEmpty(nameof(value), value);

            Initialize(value);
        }

        /// <summary>
        /// Gets the x-coordinate of this Point.
        /// </summary>
        public double X
        {
            get
            {
                return _x;
            }
        }

        /// <summary>
        /// Gets the y-coordinate of this Point.
        /// </summary>
        public double Y
        {
            get
            {
                return _y;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="PointD"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="PointD"/> to compare.</param>
        /// <param name="right"> The second <see cref="PointD"/> to compare.</param>
        public static bool operator ==(PointD left, PointD right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="PointD"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="PointD"/> to compare.</param>
        /// <param name="right"> The second <see cref="PointD"/> to compare.</param>
        public static bool operator !=(PointD left, PointD right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="PointD"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="PointD"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="PointD"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(PointD))
                return false;

            return Equals((PointD)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="PointD"/> is equal to the current <see cref="PointD"/>.
        /// </summary>
        /// <param name="other">The <see cref="PointD"/> to compare this <see cref="PointD"/> with.</param>
        /// <returns>True when the specified <see cref="PointD"/> is equal to the current <see cref="PointD"/>.</returns>
        public bool Equals(PointD other)
        {
            return
              X == other.X &&
              Y == other.Y;
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return
              X.GetHashCode() ^
              Y.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current PointD.
        /// </summary>
        /// <returns>A string that represents the current PointD.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}x{1}", _x, _y);
        }

        internal static PointD FromPointInfo(PointInfo point)
        {
            if (point == null)
                return default(PointD);

            return new PointD(point.X, point.Y);
        }

        private void Initialize(string value)
        {
            string[] values = value.Split('x');
            Throw.IfTrue(nameof(value), values.Length > 2, "Invalid point specified.");

            Throw.IfFalse(nameof(value), double.TryParse(values[0], NumberStyles.Number, CultureInfo.InvariantCulture, out double x), "Invalid point specified.");

            double y;
            if (values.Length == 2)
                Throw.IfFalse(nameof(value), double.TryParse(values[1], NumberStyles.Number, CultureInfo.InvariantCulture, out y), "Invalid point specified.");
            else
                y = x;

            _x = x;
            _y = y;
        }
    }
}