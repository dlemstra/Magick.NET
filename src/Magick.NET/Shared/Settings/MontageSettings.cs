// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <summary>
    /// Class that contains setting for the montage operation.
    /// </summary>
    public sealed partial class MontageSettings
    {
        /// <summary>
        /// Gets or sets the color of the background that thumbnails are composed on.
        /// </summary>
        public IMagickColor<QuantumType> BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the frame border color.
        /// </summary>
        public IMagickColor<QuantumType> BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the pixels between thumbnail and surrounding frame.
        /// </summary>
        public int BorderWidth { get; set; }

        /// <summary>
        /// Gets or sets the fill color.
        /// </summary>
        public IMagickColor<QuantumType> FillColor { get; set; }

        /// <summary>
        /// Gets or sets the label font.
        /// </summary>
        public string Font { get; set; }

        /// <summary>
        /// Gets or sets the font point size.
        /// </summary>
        public int FontPointsize { get; set; }

        /// <summary>
        /// Gets or sets the frame geometry (width &amp; height frame thickness).
        /// </summary>
        public IMagickGeometry FrameGeometry { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail width &amp; height plus border width &amp; height.
        /// </summary>
        public IMagickGeometry Geometry { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail position (e.g. SouthWestGravity).
        /// </summary>
        public Gravity Gravity { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail label (applied to image prior to montage).
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether drop-shadows on thumbnails are enabled or disabled.
        /// </summary>
        public bool Shadow { get; set; }

        /// <summary>
        /// Gets or sets the outline color.
        /// </summary>
        public IMagickColor<QuantumType> StrokeColor { get; set; }

        /// <summary>
        /// Gets or sets the background texture image.
        /// </summary>
        public string TextureFileName { get; set; }

        /// <summary>
        /// Gets or sets the frame geometry (width &amp; height frame thickness).
        /// </summary>
        public IMagickGeometry TileGeometry { get; set; }

        /// <summary>
        /// Gets or sets the montage title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the transparent color.
        /// </summary>
        public IMagickColor<QuantumType> TransparentColor { get; set; }

        private static string Convert(IMagickGeometry geometry)
        {
            if (geometry == null)
                return null;

            return geometry.ToString();
        }

        private INativeInstance CreateNativeInstance()
        {
            var instance = new NativeMontageSettings();
            instance.SetBackgroundColor(BackgroundColor);
            instance.SetBorderColor(BorderColor);
            instance.SetBorderWidth(BorderWidth);
            instance.SetFillColor(FillColor);
            instance.SetFont(Font);
            instance.SetFontPointsize(FontPointsize);
            instance.SetFrameGeometry(Convert(FrameGeometry));
            instance.SetGeometry(Convert(Geometry));
            instance.SetGravity(Gravity);
            instance.SetShadow(Shadow);
            instance.SetStrokeColor(StrokeColor);
            instance.SetTextureFileName(TextureFileName);
            instance.SetTileGeometry(Convert(TileGeometry));
            instance.SetTitle(Title);

            return instance;
        }
    }
}