// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick
{
    /// <summary>
    /// Contains the possible exif values.
    /// </summary>
    public static partial class ExifValues
    {
        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.BitsPerSample"/> tag.
        /// </summary>
        public static ExifShortArray BitsPerSample => new ExifShortArray(ExifTag.BitsPerSample);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.MinSampleValue"/> tag.
        /// </summary>
        public static ExifShortArray MinSampleValue => new ExifShortArray(ExifTag.MinSampleValue);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.MaxSampleValue"/> tag.
        /// </summary>
        public static ExifShortArray MaxSampleValue => new ExifShortArray(ExifTag.MaxSampleValue);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.GrayResponseCurve"/> tag.
        /// </summary>
        public static ExifShortArray GrayResponseCurve => new ExifShortArray(ExifTag.GrayResponseCurve);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.ColorMap"/> tag.
        /// </summary>
        public static ExifShortArray ColorMap => new ExifShortArray(ExifTag.ColorMap);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.ExtraSamples"/> tag.
        /// </summary>
        public static ExifShortArray ExtraSamples => new ExifShortArray(ExifTag.ExtraSamples);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.PageNumber"/> tag.
        /// </summary>
        public static ExifShortArray PageNumber => new ExifShortArray(ExifTag.PageNumber);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.MinSampleValue"/> tag.
        /// </summary>
        public static ExifShortArray TransferFunction => new ExifShortArray(ExifTag.TransferFunction);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.Predictor"/> tag.
        /// </summary>
        public static ExifShortArray Predictor => new ExifShortArray(ExifTag.Predictor);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.HalftoneHints"/> tag.
        /// </summary>
        public static ExifShortArray HalftoneHints => new ExifShortArray(ExifTag.HalftoneHints);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.SampleFormat"/> tag.
        /// </summary>
        public static ExifShortArray SampleFormat => new ExifShortArray(ExifTag.SampleFormat);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.TransferRange"/> tag.
        /// </summary>
        public static ExifShortArray TransferRange => new ExifShortArray(ExifTag.TransferRange);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.DefaultImageColor"/> tag.
        /// </summary>
        public static ExifShortArray DefaultImageColor => new ExifShortArray(ExifTag.DefaultImageColor);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.JPEGLosslessPredictors"/> tag.
        /// </summary>
        public static ExifShortArray JPEGLosslessPredictors => new ExifShortArray(ExifTag.JPEGLosslessPredictors);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.JPEGPointTransforms"/> tag.
        /// </summary>
        public static ExifShortArray JPEGPointTransforms => new ExifShortArray(ExifTag.JPEGPointTransforms);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.YCbCrSubsampling"/> tag.
        /// </summary>
        public static ExifShortArray YCbCrSubsampling => new ExifShortArray(ExifTag.YCbCrSubsampling);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.CFARepeatPatternDim"/> tag.
        /// </summary>
        public static ExifShortArray CFARepeatPatternDim => new ExifShortArray(ExifTag.CFARepeatPatternDim);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.IntergraphPacketData"/> tag.
        /// </summary>
        public static ExifShortArray IntergraphPacketData => new ExifShortArray(ExifTag.IntergraphPacketData);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.ISOSpeedRatings"/> tag.
        /// </summary>
        public static ExifShortArray ISOSpeedRatings => new ExifShortArray(ExifTag.ISOSpeedRatings);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.SubjectArea"/> tag.
        /// </summary>
        public static ExifShortArray SubjectArea => new ExifShortArray(ExifTag.SubjectArea);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.SubjectLocation"/> tag.
        /// </summary>
        public static ExifShortArray SubjectLocation => new ExifShortArray(ExifTag.SubjectLocation);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.ImageWidth"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray ImageWidth(ushort[] value) => ExifShortArray.Create(ExifTag.ImageWidth, value);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.ImageLength"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray ImageLength(ushort[] value) => ExifShortArray.Create(ExifTag.ImageLength, value);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.TileWidth"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray TileWidth(ushort[] value) => ExifShortArray.Create(ExifTag.TileWidth, value);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.TileLength"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray TileLength(ushort[] value) => ExifShortArray.Create(ExifTag.TileLength, value);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.BadFaxLines"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray BadFaxLines(ushort[] value) => ExifShortArray.Create(ExifTag.BadFaxLines, value);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.ConsecutiveBadFaxLines"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray ConsecutiveBadFaxLines(ushort[] value) => ExifShortArray.Create(ExifTag.ConsecutiveBadFaxLines, value);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.PixelXDimension"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray PixelXDimension(ushort[] value) => ExifShortArray.Create(ExifTag.PixelXDimension, value);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTag.PixelYDimension"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray PixelYDimension(ushort[] value) => ExifShortArray.Create(ExifTag.PixelYDimension, value);
    }
}