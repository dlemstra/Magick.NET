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

namespace ImageMagick
{
    /// <summary>
    /// Class that contains setting for the montage operation.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IMontageSettings<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets or sets the color of the background that thumbnails are composed on.
        /// </summary>
        IMagickColor<TQuantumType>? BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the frame border color.
        /// </summary>
        IMagickColor<TQuantumType>? BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the pixels between thumbnail and surrounding frame.
        /// </summary>
        int BorderWidth { get; set; }

        /// <summary>
        /// Gets or sets the fill color.
        /// </summary>
        IMagickColor<TQuantumType>? FillColor { get; set; }

        /// <summary>
        /// Gets or sets the label font.
        /// </summary>
        string? Font { get; set; }

        /// <summary>
        /// Gets or sets the font point size.
        /// </summary>
        int FontPointsize { get; set; }

        /// <summary>
        /// Gets or sets the frame geometry (width &amp; height frame thickness).
        /// </summary>
        IMagickGeometry? FrameGeometry { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail width &amp; height plus border width &amp; height.
        /// </summary>
        IMagickGeometry? Geometry { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail position (e.g. SouthWestGravity).
        /// </summary>
        Gravity Gravity { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail label (applied to image prior to montage).
        /// </summary>
        string? Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether drop-shadows on thumbnails are enabled or disabled.
        /// </summary>
        bool Shadow { get; set; }

        /// <summary>
        /// Gets or sets the outline color.
        /// </summary>
        IMagickColor<TQuantumType>? StrokeColor { get; set; }

        /// <summary>
        /// Gets or sets the background texture image.
        /// </summary>
        string? TextureFileName { get; set; }

        /// <summary>
        /// Gets or sets the frame geometry (width &amp; height frame thickness).
        /// </summary>
        IMagickGeometry? TileGeometry { get; set; }

        /// <summary>
        /// Gets or sets the montage title.
        /// </summary>
        string? Title { get; set; }

        /// <summary>
        /// Gets or sets the transparent color.
        /// </summary>
        IMagickColor<TQuantumType>? TransparentColor { get; set; }
    }
}