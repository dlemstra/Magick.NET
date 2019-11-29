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
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.BitsPerSample"/> tag.
        /// </summary>
        public static ExifShortArray BitsPerSample => new ExifShortArray(ExifTagValue.BitsPerSample);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.MinSampleValue"/> tag.
        /// </summary>
        public static ExifShortArray MinSampleValue => new ExifShortArray(ExifTagValue.MinSampleValue);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.MaxSampleValue"/> tag.
        /// </summary>
        public static ExifShortArray MaxSampleValue => new ExifShortArray(ExifTagValue.MaxSampleValue);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.GrayResponseCurve"/> tag.
        /// </summary>
        public static ExifShortArray GrayResponseCurve => new ExifShortArray(ExifTagValue.GrayResponseCurve);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.ColorMap"/> tag.
        /// </summary>
        public static ExifShortArray ColorMap => new ExifShortArray(ExifTagValue.ColorMap);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.ExtraSamples"/> tag.
        /// </summary>
        public static ExifShortArray ExtraSamples => new ExifShortArray(ExifTagValue.ExtraSamples);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.PageNumber"/> tag.
        /// </summary>
        public static ExifShortArray PageNumber => new ExifShortArray(ExifTagValue.PageNumber);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.MinSampleValue"/> tag.
        /// </summary>
        public static ExifShortArray TransferFunction => new ExifShortArray(ExifTagValue.TransferFunction);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.Predictor"/> tag.
        /// </summary>
        public static ExifShortArray Predictor => new ExifShortArray(ExifTagValue.Predictor);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.HalftoneHints"/> tag.
        /// </summary>
        public static ExifShortArray HalftoneHints => new ExifShortArray(ExifTagValue.HalftoneHints);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.SampleFormat"/> tag.
        /// </summary>
        public static ExifShortArray SampleFormat => new ExifShortArray(ExifTagValue.SampleFormat);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.TransferRange"/> tag.
        /// </summary>
        public static ExifShortArray TransferRange => new ExifShortArray(ExifTagValue.TransferRange);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.DefaultImageColor"/> tag.
        /// </summary>
        public static ExifShortArray DefaultImageColor => new ExifShortArray(ExifTagValue.DefaultImageColor);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.JPEGLosslessPredictors"/> tag.
        /// </summary>
        public static ExifShortArray JPEGLosslessPredictors => new ExifShortArray(ExifTagValue.JPEGLosslessPredictors);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.JPEGPointTransforms"/> tag.
        /// </summary>
        public static ExifShortArray JPEGPointTransforms => new ExifShortArray(ExifTagValue.JPEGPointTransforms);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.YCbCrSubsampling"/> tag.
        /// </summary>
        public static ExifShortArray YCbCrSubsampling => new ExifShortArray(ExifTagValue.YCbCrSubsampling);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.CFARepeatPatternDim"/> tag.
        /// </summary>
        public static ExifShortArray CFARepeatPatternDim => new ExifShortArray(ExifTagValue.CFARepeatPatternDim);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.IntergraphPacketData"/> tag.
        /// </summary>
        public static ExifShortArray IntergraphPacketData => new ExifShortArray(ExifTagValue.IntergraphPacketData);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.ISOSpeedRatings"/> tag.
        /// </summary>
        public static ExifShortArray ISOSpeedRatings => new ExifShortArray(ExifTagValue.ISOSpeedRatings);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.SubjectArea"/> tag.
        /// </summary>
        public static ExifShortArray SubjectArea => new ExifShortArray(ExifTagValue.SubjectArea);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.SubjectLocation"/> tag.
        /// </summary>
        public static ExifShortArray SubjectLocation => new ExifShortArray(ExifTagValue.SubjectLocation);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.StripOffsets"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray StripOffsets(ushort[] value) => ExifShortArray.Create(ExifTagValue.StripOffsets, value);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.TileByteCounts"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray TileByteCounts(ushort[] value) => ExifShortArray.Create(ExifTagValue.TileByteCounts, value);

        /// <summary>
        /// Gets a new <see cref="ExifShortArray"/> instance for the <see cref="ExifTagValue.ImageLayer"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifShortArray"/> instance.</returns>
        public static ExifShortArray ImageLayer(ushort[] value) => ExifShortArray.Create(ExifTagValue.ImageLayer, value);
    }
}