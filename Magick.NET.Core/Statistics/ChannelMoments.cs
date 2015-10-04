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

namespace ImageMagick
{
  ///<summary>
  /// Encapsulation of the ImageMagick ChannelMoments object.
  ///</summary>
  public sealed class ChannelMoments
  {
    private double[] _HuInvariants;

    /// <summary>
    /// Initializes a new instance of the ChannelMoments class.
    /// </summary>
    /// <param name="channel">The channel of the moment.</param>
    /// <param name="centroid">The centroid.</param>
    /// <param name="ellipseAxis">The ellipse axis.</param>
    /// <param name="ellipseAngle">The ellipse angle.</param>
    /// <param name="ellipseEccentricity">The ellipse eccentricity.</param>
    /// <param name="ellipseIntensity">The ellipse intensity.</param>
    /// <param name="huInvariants">The Hu invariants.</param>
    public ChannelMoments(PixelChannel channel, PointD centroid, PointD ellipseAxis, double ellipseAngle,
      double ellipseEccentricity, double ellipseIntensity, double[] huInvariants)
    {
      Channel = channel;
      Centroid = centroid;
      EllipseAxis = ellipseAxis;
      EllipseAngle = ellipseAngle;
      EllipseEccentricity = ellipseEccentricity;
      EllipseIntensity = ellipseIntensity;
      _HuInvariants = huInvariants;
    }

    ///<summary>
    /// The centroid.
    ///</summary>
    public PointD Centroid
    {
      get;
      private set;
    }

    ///<summary>
    /// The channel of this moment.
    ///</summary>
    public PixelChannel Channel
    {
      get;
      private set;
    }

    ///<summary>
    /// The ellipse axis.
    ///</summary>
    public PointD EllipseAxis
    {
      get;
      private set;
    }

    ///<summary>
    /// The ellipse angle.
    ///</summary>
    public double EllipseAngle
    {
      get;
      private set;
    }

    ///<summary>
    /// The ellipse eccentricity.
    ///</summary>
    public double EllipseEccentricity
    {
      get;
      private set;
    }

    ///<summary>
    /// The ellipse intensity.
    ///</summary>
    public double EllipseIntensity
    {
      get;
      private set;
    }

    ///<summary>
    /// The Hu invariants.
    ///</summary>
    public double HuInvariants(int index)
    {
      Throw.IfOutOfRange("index", index, 8);

      return _HuInvariants[index];
    }
  }
}