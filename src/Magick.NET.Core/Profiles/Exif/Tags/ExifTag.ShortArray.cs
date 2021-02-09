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
    /// <content/>
    public abstract partial class ExifTag
    {
        /// <summary>
        /// Gets the BitsPerSample exif tag.
        /// </summary>
        public static ExifTag<ushort[]> BitsPerSample { get; } = new ExifTag<ushort[]>(ExifTagValue.BitsPerSample);

        /// <summary>
        /// Gets the MinSampleValue exif tag.
        /// </summary>
        public static ExifTag<ushort[]> MinSampleValue { get; } = new ExifTag<ushort[]>(ExifTagValue.MinSampleValue);

        /// <summary>
        /// Gets the MaxSampleValue exif tag.
        /// </summary>
        public static ExifTag<ushort[]> MaxSampleValue { get; } = new ExifTag<ushort[]>(ExifTagValue.MaxSampleValue);

        /// <summary>
        /// Gets the GrayResponseCurve exif tag.
        /// </summary>
        public static ExifTag<ushort[]> GrayResponseCurve { get; } = new ExifTag<ushort[]>(ExifTagValue.GrayResponseCurve);

        /// <summary>
        /// Gets the ColorMap exif tag.
        /// </summary>
        public static ExifTag<ushort[]> ColorMap { get; } = new ExifTag<ushort[]>(ExifTagValue.ColorMap);

        /// <summary>
        /// Gets the ExtraSamples exif tag.
        /// </summary>
        public static ExifTag<ushort[]> ExtraSamples { get; } = new ExifTag<ushort[]>(ExifTagValue.ExtraSamples);

        /// <summary>
        /// Gets the PageNumber exif tag.
        /// </summary>
        public static ExifTag<ushort[]> PageNumber { get; } = new ExifTag<ushort[]>(ExifTagValue.PageNumber);

        /// <summary>
        /// Gets the TransferFunction exif tag.
        /// </summary>
        public static ExifTag<ushort[]> TransferFunction { get; } = new ExifTag<ushort[]>(ExifTagValue.TransferFunction);

        /// <summary>
        /// Gets the Predictor exif tag.
        /// </summary>
        public static ExifTag<ushort[]> Predictor { get; } = new ExifTag<ushort[]>(ExifTagValue.Predictor);

        /// <summary>
        /// Gets the HalftoneHints exif tag.
        /// </summary>
        public static ExifTag<ushort[]> HalftoneHints { get; } = new ExifTag<ushort[]>(ExifTagValue.HalftoneHints);

        /// <summary>
        /// Gets the SampleFormat exif tag.
        /// </summary>
        public static ExifTag<ushort[]> SampleFormat { get; } = new ExifTag<ushort[]>(ExifTagValue.SampleFormat);

        /// <summary>
        /// Gets the TransferRange exif tag.
        /// </summary>
        public static ExifTag<ushort[]> TransferRange { get; } = new ExifTag<ushort[]>(ExifTagValue.TransferRange);

        /// <summary>
        /// Gets the DefaultImageColor exif tag.
        /// </summary>
        public static ExifTag<ushort[]> DefaultImageColor { get; } = new ExifTag<ushort[]>(ExifTagValue.DefaultImageColor);

        /// <summary>
        /// Gets the JPEGLosslessPredictors exif tag.
        /// </summary>
        public static ExifTag<ushort[]> JPEGLosslessPredictors { get; } = new ExifTag<ushort[]>(ExifTagValue.JPEGLosslessPredictors);

        /// <summary>
        /// Gets the JPEGPointTransforms exif tag.
        /// </summary>
        public static ExifTag<ushort[]> JPEGPointTransforms { get; } = new ExifTag<ushort[]>(ExifTagValue.JPEGPointTransforms);

        /// <summary>
        /// Gets the YCbCrSubsampling exif tag.
        /// </summary>
        public static ExifTag<ushort[]> YCbCrSubsampling { get; } = new ExifTag<ushort[]>(ExifTagValue.YCbCrSubsampling);

        /// <summary>
        /// Gets the CFARepeatPatternDim exif tag.
        /// </summary>
        public static ExifTag<ushort[]> CFARepeatPatternDim { get; } = new ExifTag<ushort[]>(ExifTagValue.CFARepeatPatternDim);

        /// <summary>
        /// Gets the IntergraphPacketData exif tag.
        /// </summary>
        public static ExifTag<ushort[]> IntergraphPacketData { get; } = new ExifTag<ushort[]>(ExifTagValue.IntergraphPacketData);

        /// <summary>
        /// Gets the ISOSpeedRatings exif tag.
        /// </summary>
        public static ExifTag<ushort[]> ISOSpeedRatings { get; } = new ExifTag<ushort[]>(ExifTagValue.ISOSpeedRatings);

        /// <summary>
        /// Gets the SubjectArea exif tag.
        /// </summary>
        public static ExifTag<ushort[]> SubjectArea { get; } = new ExifTag<ushort[]>(ExifTagValue.SubjectArea);

        /// <summary>
        /// Gets the SubjectLocation exif tag.
        /// </summary>
        public static ExifTag<ushort[]> SubjectLocation { get; } = new ExifTag<ushort[]>(ExifTagValue.SubjectLocation);
    }
}