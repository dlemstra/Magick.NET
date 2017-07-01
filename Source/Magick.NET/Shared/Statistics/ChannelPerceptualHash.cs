// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
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
    /// Contains the he perceptual hash of one image channel.
    /// </summary>
    public partial class ChannelPerceptualHash
    {
        private double[] _SrgbHuPhash;
        private double[] _HclpHuPhash;
        private string _Hash;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelPerceptualHash"/> class.
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

        internal ChannelPerceptualHash(PixelChannel channel)
        {
            Channel = channel;
            _HclpHuPhash = new double[7];
            _SrgbHuPhash = new double[7];
        }

        internal ChannelPerceptualHash(PixelChannel channel, IntPtr instance)
          : this(channel)
        {
            NativeChannelPerceptualHash nativeInstance = new NativeChannelPerceptualHash(instance);
            SetSrgbHuPhash(nativeInstance);
            SetHclpHuPhash(nativeInstance);
            SetHash();
        }

        internal ChannelPerceptualHash(PixelChannel channel, string hash)
          : this(channel)
        {
            ParseHash(hash);
        }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        public PixelChannel Channel
        {
            get;
            private set;
        }

        /// <summary>
        /// SRGB hu perceptual hash.
        /// </summary>
        /// <param name="index">The index to use.</param>
        /// <returns>The SRGB hu perceptual hash.</returns>
        public double SrgbHuPhash(int index)
        {
            Throw.IfOutOfRange(nameof(index), index, 7);

            return _SrgbHuPhash[index];
        }

        /// <summary>
        /// Hclp hu perceptual hash.
        /// </summary>
        /// <param name="index">The index to use.</param>
        /// <returns>The Hclp hu perceptual hash.</returns>
        public double HclpHuPhash(int index)
        {
            Throw.IfOutOfRange(nameof(index), index, 7);

            return _HclpHuPhash[index];
        }

        /// <summary>
        /// Returns the sum squared difference between this hash and the other hash.
        /// </summary>
        /// <param name="other">The <see cref="ChannelPerceptualHash"/> to get the distance of.</param>
        /// <returns>The sum squared difference between this hash and the other hash.</returns>
        public double SumSquaredDistance(ChannelPerceptualHash other)
        {
            Throw.IfNull(nameof(other), other);

            double ssd = 0.0;

            for (int i = 0; i < 7; i++)
            {
                ssd += (_SrgbHuPhash[i] - other._SrgbHuPhash[i]) * (_SrgbHuPhash[i] - other._SrgbHuPhash[i]);
                ssd += (_HclpHuPhash[i] - other._HclpHuPhash[i]) * (_HclpHuPhash[i] - other._HclpHuPhash[i]);
            }

            return ssd;
        }

        /// <summary>
        /// Returns a string representation of this hash.
        /// </summary>
        /// <returns>A string representation of this hash.</returns>
        public override string ToString()
        {
            return _Hash;
        }

        private void ParseHash(string hash)
        {
            _Hash = hash;

            for (int i = 0; i < 14; i++)
            {
                int hex;
                if (!int.TryParse(hash.Substring(i * 5, 5), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hex))
                    throw new ArgumentException("Invalid hash specified", nameof(hash));

                double value = (ushort)hex / Math.Pow(10.0, hex >> 17);
                if ((hex & (1 << 16)) != 0)
                    value = -value;
                if (i < 7)
                    _SrgbHuPhash[i] = value;
                else
                    _HclpHuPhash[i - 7] = value;
            }
        }

        private void SetHash()
        {
            _Hash = string.Empty;
            for (int i = 0; i < 14; i++)
            {
                double value;
                if (i < 7)
                    value = _SrgbHuPhash[i];
                else
                    value = _HclpHuPhash[i - 7];

                int hex = 0;
                while (hex < 7 && Math.Abs(value * 10) < 65536)
                {
                    value = value * 10;
                    hex++;
                }

                hex = hex << 1;
                if (value < 0.0)
                    hex |= 1;
                hex = (hex << 16) + (int)(value < 0.0 ? -(value - 0.5) : value + 0.5);
                _Hash += hex.ToString("x", CultureInfo.InvariantCulture);
            }
        }

        private void SetHclpHuPhash(NativeChannelPerceptualHash instance)
        {
            for (int i = 0; i < 7; i++)
                _HclpHuPhash[i] = instance.GetHclpHuPhash(i);
        }

        private void SetSrgbHuPhash(NativeChannelPerceptualHash instance)
        {
            for (int i = 0; i < 7; i++)
                _SrgbHuPhash[i] = instance.GetSrgbHuPhash(i);
        }
    }
}
