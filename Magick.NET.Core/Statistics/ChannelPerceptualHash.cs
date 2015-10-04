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
  /// Encapsulation of the ImageMagick ChannelPerceptualHash object.
  ///</summary>
  public sealed class ChannelPerceptualHash
  {
    private double[] _SrgbHuPhash;
    private double[] _HclpHuPhash;
    private string _Hash;

    /// <summary>
    /// Initializes a new instance of the PerceptualHash class 
    /// </summary>
    /// <param name="channel">The channel.></param>
    /// <param name="srgbHuPhash">SRGB hu perceptual hash.</param>
    /// <param name="hclpHuPhash">Hclp hu perceptual hash.</param>
    /// <param name="hash">A string representation of this hash.</param>
    public ChannelPerceptualHash(PixelChannel channel, double[] srgbHuPhash, double[] hclpHuPhash, string hash)
    {
      Channel = channel;
      _SrgbHuPhash = srgbHuPhash;
      _HclpHuPhash = hclpHuPhash;
      _Hash = hash;
    }

    ///<summary>
    /// The channel.
    ///</summary>
    public PixelChannel Channel
    {
      get;
      private set;
    }

    ///<summary>
    /// SRGB hu perceptual hash.
    ///</summary>
    public double SrgbHuPhash(int index)
    {
      Throw.IfOutOfRange("index", index, 7);

      return _SrgbHuPhash[index];
    }

    ///<summary>
    /// Hclp hu perceptual hash.
    ///</summary>
    public double HclpHuPhash(int index)
    {
      Throw.IfOutOfRange("index", index, 7);

      return _HclpHuPhash[index];
    }

    ///<summary>
    /// Returns the sum squared difference between this hash and the other hash.
    ///</summary>
    ///<param name="other">The ChannelPerceptualHash to get the distance of.</param>
    public double SumSquaredDistance(ChannelPerceptualHash other)
    {
      Throw.IfNull("other", other);

      double ssd = 0.0;

      for (int i = 0; i < 7; i++)
      {
        ssd += ((_SrgbHuPhash[i] - other._SrgbHuPhash[i]) * (_SrgbHuPhash[i] - other._SrgbHuPhash[i]));
        ssd += ((_HclpHuPhash[i] - other._HclpHuPhash[i]) * (_HclpHuPhash[i] - other._HclpHuPhash[i]));
      }

      return ssd;
    }

    ///<summary>
    /// Returns a string representation of this hash.
    ///</summary>
    public override string ToString()
    {
      return _Hash;
    }
  }
}