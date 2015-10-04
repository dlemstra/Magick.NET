//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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

namespace ImageMagick.Drawables.Paths
{
  ///<summary>
  /// Encapsulation of the PathArc object.
  ///</summary>
  public interface IPathArc
  {
    ///<summary>
    /// The X radius.
    ///</summary>
    double RadiusX
    {
      get;
    }

    ///<summary>
    /// The Y radius.
    ///</summary>
    double RadiusY
    {
      get;
    }

    ///<summary>
    /// Indicates how the ellipse as a whole is rotated relative to the current coordinate system.
    ///</summary>
    double RotationX
    {
      get;
    }

    ///<summary>
    /// If true then draw the larger of the available arcs.
    ///</summary>
    bool UseLargeArc
    {
      get;
    }

    ///<summary>
    /// If true then draw the arc matching a clock-wise rotation.
    ///</summary>
    bool UseSweep
    {
      get;
    }

    ///<summary>
    /// The X offset from origin.
    ///</summary>
    double X
    {
      get;
    }

    ///<summary>
    /// The Y offset from origin.
    ///</summary>
    double Y
    {
      get;
    }
  }
}