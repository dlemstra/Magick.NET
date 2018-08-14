﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick
{
    /// <summary>
    /// PrimaryInfo information
    /// </summary>
    public partial class PrimaryInfo : IEquatable<PrimaryInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryInfo"/> class.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <param name="z">The z value.</param>
        public PrimaryInfo(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        private PrimaryInfo(NativePrimaryInfo instance)
        {
            X = instance.X;
            Y = instance.Y;
            Z = instance.Z;
        }

        /// <summary>
        /// Gets the X value.
        /// </summary>
        public double X
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Y value.
        /// </summary>
        public double Y
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Z value.
        /// </summary>
        public double Z
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified <see cref="PrimaryInfo"/> is equal to the current <see cref="PrimaryInfo"/>.
        /// </summary>
        /// <param name="other">The <see cref="PrimaryInfo"/> to compare this <see cref="PrimaryInfo"/> with.</param>
        /// <returns>True when the specified <see cref="PrimaryInfo"/> is equal to the current <see cref="PrimaryInfo"/>.</returns>
        public bool Equals(PrimaryInfo other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
              X == other.X &&
              Y == other.Y &&
              Z == other.Z;
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return
              X.GetHashCode() ^
              Y.GetHashCode() ^
              Z.GetHashCode();
        }

        private INativeInstance CreateNativeInstance()
        {
            NativePrimaryInfo instance = new NativePrimaryInfo();
            instance.X = X;
            instance.Y = Y;
            instance.Z = Z;
            return instance;
        }
    }
}
