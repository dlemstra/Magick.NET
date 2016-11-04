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
using System.Collections.Generic;

namespace ImageMagick
{
  /// <summary>
  /// Contains the he perceptual hash of one or more image channels.
  /// </summary>
  public sealed partial class PerceptualHash
  {
    private Dictionary<PixelChannel, ChannelPerceptualHash> _Channels;

    private PerceptualHash()
    {
      _Channels = new Dictionary<PixelChannel, ChannelPerceptualHash>();
    }

    private static ChannelPerceptualHash CreateChannelPerceptualHash(MagickImage image, IntPtr list, PixelChannel channel)
    {
      IntPtr instance = NativePerceptualHash.GetInstance(image, list, channel);
      if (instance == IntPtr.Zero)
        return null;

      return new ChannelPerceptualHash(channel, instance);
    }

    private void AddChannel(MagickImage image, IntPtr list, PixelChannel channel)
    {
      ChannelPerceptualHash instance = CreateChannelPerceptualHash(image, list, channel);
      if (instance != null)
        _Channels.Add(instance.Channel, instance);
    }

    internal static void DisposeList(IntPtr list)
    {
      if (list != IntPtr.Zero)
        NativePerceptualHash.DisposeList(list);
    }

    internal PerceptualHash(MagickImage image, IntPtr list)
      : this()
    {
      if (list == IntPtr.Zero)
        return;

      AddChannel(image, list, PixelChannel.Red);
      AddChannel(image, list, PixelChannel.Green);
      AddChannel(image, list, PixelChannel.Blue);
    }

    internal bool Isvalid
    {
      get
      {
        return _Channels.ContainsKey(PixelChannel.Red) &&
          _Channels.ContainsKey(PixelChannel.Green) &&
          _Channels.ContainsKey(PixelChannel.Blue);
      }
    }

    /// <summary>
    /// Initializes a new instance of the PerceptualHash class with the specified hash.
    /// </summary>
    public PerceptualHash(string perceptualHash)
      : this()
    {
      Throw.IfNullOrEmpty(nameof(perceptualHash), perceptualHash);
      Throw.IfFalse(nameof(perceptualHash), perceptualHash.Length == 210, "Invalid hash size.");

      _Channels[PixelChannel.Red] = new ChannelPerceptualHash(PixelChannel.Red, perceptualHash.Substring(0, 70));
      _Channels[PixelChannel.Green] = new ChannelPerceptualHash(PixelChannel.Green, perceptualHash.Substring(70, 70));
      _Channels[PixelChannel.Blue] = new ChannelPerceptualHash(PixelChannel.Blue, perceptualHash.Substring(140, 70));
    }

    /// <summary>
    /// Perceptual hash for the specified channel.
    /// </summary>
    public ChannelPerceptualHash GetChannel(PixelChannel channel)
    {
      ChannelPerceptualHash perceptualHash;
      _Channels.TryGetValue(channel, out perceptualHash);
      return perceptualHash;
    }

    /// <summary>
    /// Returns the sum squared difference between this hash and the other hash.
    /// </summary>
    /// <param name="other">The PerceptualHash to get the distance of.</param>
    public double SumSquaredDistance(PerceptualHash other)
    {
      Throw.IfNull(nameof(other), other);

      return
        _Channels[PixelChannel.Red].SumSquaredDistance(other._Channels[PixelChannel.Red]) +
        _Channels[PixelChannel.Green].SumSquaredDistance(other._Channels[PixelChannel.Green]) +
        _Channels[PixelChannel.Blue].SumSquaredDistance(other._Channels[PixelChannel.Blue]);
    }

    /// <summary>
    /// Returns a string representation of this hash.
    /// </summary>
    public override string ToString()
    {
      return
        _Channels[PixelChannel.Red].ToString() +
        _Channels[PixelChannel.Green].ToString() +
        _Channels[PixelChannel.Blue].ToString();
    }
  }
}