// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Collections.Generic;
using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Class that contains an ICM/ICC color profile.
    /// </summary>
    public sealed class ColorProfile : ImageProfile, IColorProfile
    {
        private static readonly object _SyncRoot = new object();
        private static readonly Dictionary<string, ColorProfile> _profiles = new Dictionary<string, ColorProfile>();

        private ColorProfileData _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorProfile"/> class.
        /// </summary>
        /// <param name="data">A byte array containing the profile.</param>
        public ColorProfile(byte[] data)
          : base("icc", data)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorProfile"/> class.
        /// </summary>
        /// <param name="stream">A stream containing the profile.</param>
        public ColorProfile(Stream stream)
          : base("icc", stream)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorProfile"/> class.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
        public ColorProfile(string fileName)
          : base("icc", fileName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorProfile"/> class.
        /// </summary>
        /// <param name="name">The name of the color profile (e.g. icc or icm).</param>
        /// <param name="data">A byte array containing the profile.</param>
        public ColorProfile(string name, byte[] data)
          : base(name, data)
        {
            Initialize();
        }

        /// <summary>
        /// Gets the AdobeRGB1998 profile.
        /// </summary>
        public static ColorProfile AdobeRGB1998
            => Load("ImageMagick.Resources.ColorProfiles.RGB", "AdobeRGB1998.icc");

        /// <summary>
        /// Gets the AppleRGB profile.
        /// </summary>
        public static ColorProfile AppleRGB
            => Load("ImageMagick.Resources.ColorProfiles.RGB", "AppleRGB.icc");

        /// <summary>
        /// Gets the CoatedFOGRA39 profile.
        /// </summary>
        public static ColorProfile CoatedFOGRA39
            => Load("ImageMagick.Resources.ColorProfiles.CMYK", "CoatedFOGRA39.icc");

        /// <summary>
        /// Gets the ColorMatchRGB profile.
        /// </summary>
        public static ColorProfile ColorMatchRGB
            => Load("ImageMagick.Resources.ColorProfiles.RGB", "ColorMatchRGB.icc");

        /// <summary>
        /// Gets the sRGB profile.
        /// </summary>
        public static ColorProfile SRGB
            => Load("ImageMagick.Resources.ColorProfiles.RGB", "SRGB.icm");

        /// <summary>
        /// Gets the USWebCoatedSWOP profile.
        /// </summary>
        public static ColorProfile USWebCoatedSWOP
            => Load("ImageMagick.Resources.ColorProfiles.CMYK", "USWebCoatedSWOP.icc");

        /// <summary>
        /// Gets the color space of the profile.
        /// </summary>
        public ColorSpace ColorSpace
            => _data.ColorSpace;

        /// <summary>
        /// Gets the copyright of the profile.
        /// </summary>
        public string Copyright
            => _data.Copyright;

        /// <summary>
        /// Gets the description of the profile.
        /// </summary>
        public string Description
            => _data.Description;

        /// <summary>
        /// Gets the manufacturer of the profile.
        /// </summary>
        public string Manufacturer
            => _data.Manufacturer;

        /// <summary>
        /// Gets the model of the profile.
        /// </summary>
        public string Model
            => _data.Model;

        private static ColorProfile Load(string resourcePath, string resourceName)
        {
            lock (_SyncRoot)
            {
                if (!_profiles.ContainsKey(resourceName))
                {
                    using (Stream stream = TypeHelper.GetManifestResourceStream(typeof(ColorProfile), resourcePath, resourceName))
                    {
                        _profiles[resourceName] = new ColorProfile(stream);
                    }
                }
            }

            return _profiles[resourceName];
        }

        private void Initialize()
            => _data = ColorProfileReader.Read(GetData());
    }
}