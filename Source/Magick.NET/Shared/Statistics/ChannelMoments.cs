//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
    /// The normalized moments of one image channels.
    /// </summary>
    public sealed partial class ChannelMoments
    {
        private double[] _HuInvariants;

        private ChannelMoments(PixelChannel channel, IntPtr instance)
        {
            Channel = channel;

            NativeChannelMoments nativeInstance = new NativeChannelMoments(instance);
            Centroid = PointD.FromPointInfo(nativeInstance.Centroid);
            EllipseAngle = nativeInstance.EllipseAngle;
            EllipseAxis = PointD.FromPointInfo(nativeInstance.EllipseAxis);
            EllipseEccentricity = nativeInstance.EllipseEccentricity;
            EllipseIntensity = nativeInstance.EllipseIntensity;
            SetHuInvariants(nativeInstance);
        }

        private void SetHuInvariants(NativeChannelMoments nativeInstance)
        {
            _HuInvariants = new double[8];

            for (int i = 0; i < 8; i++)
                _HuInvariants[i] = nativeInstance.GetHuInvariants(i);
        }

        internal static ChannelMoments Create(PixelChannel channel, IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return null;

            return new ChannelMoments(channel, instance);
        }

        /// <summary>
        /// Gets the centroid.
        /// </summary>
        public PointD Centroid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the channel of this moment.
        /// </summary>
        public PixelChannel Channel
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ellipse axis.
        /// </summary>
        public PointD EllipseAxis
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ellipse angle.
        /// </summary>
        public double EllipseAngle
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ellipse eccentricity.
        /// </summary>
        public double EllipseEccentricity
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ellipse intensity.
        /// </summary>
        public double EllipseIntensity
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the Hu invariants.
        /// </summary>
        /// <param name="index">The index to use.</param>
        /// <returns>The Hu invariants.</returns>
        public double HuInvariants(int index)
        {
            Throw.IfOutOfRange(nameof(index), index, 8);

            return _HuInvariants[index];
        }
    }
}