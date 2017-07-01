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

using System.Globalization;

namespace ImageMagick
{
    /// <summary>
    /// Class that contains setting for when an image is being read.
    /// </summary>
    public sealed class MagickReadSettings : MagickSettings
    {
        private string GetScenes()
        {
            if (!FrameIndex.HasValue && !FrameCount.HasValue)
                return null;

            if (FrameIndex.HasValue && (!FrameCount.HasValue || FrameCount.Value == 1))
                return FrameIndex.Value.ToString(CultureInfo.InvariantCulture);

            int frame = FrameIndex ?? 0;
            return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", frame, frame + FrameCount.Value);
        }

        private void ApplyDefines()
        {
            if (Defines == null)
                return;

            foreach (IDefine define in Defines.Defines)
            {
                SetOption(GetDefineKey(define), define.Value);
            }
        }

        private void ApplyDimensions()
        {
            if (Width.HasValue && Height.HasValue)
                Size = Width + "x" + Height;
            else if (Width.HasValue)
                Size = Width + "x";
            else if (Height.HasValue)
                Size = "x" + Height;
        }

        private void ApplyFrame()
        {
            if (!FrameIndex.HasValue && !FrameCount.HasValue)
                return;

            Scenes = GetScenes();
            Scene = FrameIndex ?? 0;
            NumberScenes = FrameCount ?? 1;
        }

        private static string GetDefineKey(IDefine define)
        {
            if (define.Format == MagickFormat.Unknown)
                return define.Name;

            return EnumHelper.GetName(define.Format) + ":" + define.Name;
        }

        private void Copy(MagickReadSettings settings)
        {
            base.Copy(settings);

            Defines = settings.Defines;
            FrameIndex = settings.FrameIndex;
            FrameCount = settings.FrameCount;
            Height = settings.Height;
            Width = settings.Width;
            PixelStorage = settings.PixelStorage?.Clone();
        }

        internal MagickReadSettings(MagickSettings settings)
        {
            Copy(settings);
        }

        internal MagickReadSettings(MagickReadSettings settings)
        {
            Copy(settings);

            ApplyDefines();
            ApplyDimensions();
            ApplyFrame();
        }

        internal void ForceSingleFrame()
        {
            FrameCount = 1;
            ApplyFrame();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickReadSettings"/> class.
        /// </summary>
        public MagickReadSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickReadSettings"/> class with the specified defines.
        /// </summary>
        /// <param name="readDefines">The read defines to set.</param>
        public MagickReadSettings(IReadDefines readDefines)
        {
            SetDefines(readDefines);
        }

        /// <summary>
        /// Gets or sets the defines that should be set before the image is read.
        /// </summary>
        public IReadDefines Defines
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the specified area to extract from the image.
        /// </summary>
        public MagickGeometry ExtractArea
        {
            get
            {
                return Extract;
            }
            set
            {
                Extract = value;
            }
        }

        /// <summary>
        /// Gets or sets the index of the image to read from a multi layer/frame image.
        /// </summary>
        public int? FrameIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of images to read from a multi layer/frame image.
        /// </summary>
        public int? FrameCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int? Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the settings for pixel storage.
        /// </summary>
        public PixelStorageSettings PixelStorage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the monochrome reader shoul be used. This is
        /// supported by: PCL, PDF, PS and XPS.
        /// </summary>
        public bool UseMonochrome
        {
            get
            {
                return Monochrome;
            }
            set
            {
                Monochrome = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int? Width
        {
            get;
            set;
        }
    }
}