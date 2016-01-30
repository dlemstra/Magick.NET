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
  /// PrimaryInfo information
  /// </summary>
  public partial class PrimaryInfo : IEquatable<PrimaryInfo>
  {
    private PrimaryInfo(NativePrimaryInfo instance)
    {
      X = instance.X;
      Y = instance.Y;
      Z = instance.Z;
    }

    private INativeInstance CreateNativeInstance()
    {
      NativePrimaryInfo instance = new NativePrimaryInfo();
      instance.X = X;
      instance.Y = Y;
      instance.Z = Z;
      return instance;
    }
    ///<summary>
    /// Initializes a new instance of the PrimaryInfo class.
    ///</summary>
    ///<param name="x">The x value.</param>
    ///<param name="y">The y value.</param>
    ///<param name="z">The z value.</param>
    public PrimaryInfo(double x, double y, double z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    /// <summary>
    /// X value.
    /// </summary>
    public double X
    {
      get;
      private set;
    }

    /// <summary>
    /// Y value.
    /// </summary>
    public double Y
    {
      get;
      private set;
    }

    /// <summary>
    /// Z value.
    /// </summary>
    public double Z
    {
      get;
      private set;
    }

    ///<summary>
    /// Determines whether the specified primary info is equal to the current primary info.
    ///</summary>
    ///<param name="other">The primary info to compare this primary info with.</param>
    public bool Equals(PrimaryInfo other)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      return
        X == other.X &&
        Y == other.Y &&
        Z == other.Z;
    }

    ///<summary>
    /// Serves as a hash of this type.
    ///</summary>
    public override int GetHashCode()
    {
      return
        X.GetHashCode() ^
        Y.GetHashCode() ^
        Z.GetHashCode();
    }
  }
}