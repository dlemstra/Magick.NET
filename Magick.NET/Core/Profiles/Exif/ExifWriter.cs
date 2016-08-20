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
using System.Collections.ObjectModel;
using System.Text;

namespace ImageMagick
{
  internal sealed class ExifWriter
  {
    private static readonly ExifTag[] _IfdTags = new ExifTag[93]
    {
      ExifTag.ImageWidth, ExifTag.ImageLength, ExifTag.BitsPerSample, ExifTag.Compression,
      ExifTag.PhotometricInterpretation, ExifTag.Thresholding, ExifTag.CellWidth,
      ExifTag.CellLength, ExifTag.FillOrder,ExifTag.ImageDescription, ExifTag.Make,
      ExifTag.Model, ExifTag.StripOffsets, ExifTag.Orientation, ExifTag.SamplesPerPixel,
      ExifTag.RowsPerStrip, ExifTag.StripByteCounts, ExifTag.MinSampleValue,
      ExifTag.MaxSampleValue, ExifTag.XResolution, ExifTag.YResolution,
      ExifTag.PlanarConfiguration, ExifTag.FreeOffsets, ExifTag.FreeByteCounts,
      ExifTag.GrayResponseUnit, ExifTag.GrayResponseCurve, ExifTag.ResolutionUnit,
      ExifTag.Software, ExifTag.DateTime, ExifTag.Artist, ExifTag.HostComputer,
      ExifTag.ColorMap, ExifTag.ExtraSamples, ExifTag.Copyright, ExifTag.DocumentName,
      ExifTag.PageName, ExifTag.XPosition, ExifTag.YPosition, ExifTag.T4Options,
      ExifTag.T6Options, ExifTag.PageNumber, ExifTag.TransferFunction, ExifTag.Predictor,
      ExifTag.WhitePoint, ExifTag.PrimaryChromaticities, ExifTag.HalftoneHints,
      ExifTag.TileWidth, ExifTag.TileLength, ExifTag.TileOffsets, ExifTag.TileByteCounts,
      ExifTag.BadFaxLines, ExifTag.CleanFaxData, ExifTag.ConsecutiveBadFaxLines,
      ExifTag.InkSet, ExifTag.InkNames, ExifTag.NumberOfInks, ExifTag.DotRange,
      ExifTag.TargetPrinter, ExifTag.SampleFormat, ExifTag.SMinSampleValue,
      ExifTag.SMaxSampleValue, ExifTag.TransferRange, ExifTag.ClipPath,
      ExifTag.XClipPathUnits, ExifTag.YClipPathUnits, ExifTag.Indexed, ExifTag.JPEGTables,
      ExifTag.OPIProxy, ExifTag.ProfileType, ExifTag.FaxProfile, ExifTag.CodingMethods,
      ExifTag.VersionYear, ExifTag.ModeNumber, ExifTag.Decode, ExifTag.DefaultImageColor,
      ExifTag.JPEGProc, ExifTag.JPEGInterchangeFormat, ExifTag.JPEGInterchangeFormatLength,
      ExifTag.JPEGRestartInterval, ExifTag.JPEGLosslessPredictors,
      ExifTag.JPEGPointTransforms, ExifTag.JPEGQTables, ExifTag.JPEGDCTables,
      ExifTag.JPEGACTables, ExifTag.YCbCrCoefficients, ExifTag.YCbCrSubsampling,
      ExifTag.YCbCrSubsampling, ExifTag.YCbCrPositioning, ExifTag.ReferenceBlackWhite,
      ExifTag.StripRowCounts, ExifTag.XMP, ExifTag.ImageID, ExifTag.ImageLayer
    };

    private static readonly ExifTag[] _ExifTags = new ExifTag[56]
    {
      ExifTag.ExposureTime, ExifTag.FNumber, ExifTag.ExposureProgram,
      ExifTag.SpectralSensitivity, ExifTag.ISOSpeedRatings, ExifTag.OECF,
      ExifTag.ExifVersion, ExifTag.DateTimeOriginal, ExifTag.DateTimeDigitized,
      ExifTag.ComponentsConfiguration, ExifTag.CompressedBitsPerPixel,
      ExifTag.ShutterSpeedValue, ExifTag.ApertureValue, ExifTag.BrightnessValue,
      ExifTag.ExposureBiasValue, ExifTag.MaxApertureValue, ExifTag.SubjectDistance,
      ExifTag.MeteringMode, ExifTag.LightSource, ExifTag.Flash, ExifTag.FocalLength,
      ExifTag.SubjectArea, ExifTag.MakerNote, ExifTag.UserComment, ExifTag.SubsecTime,
      ExifTag.SubsecTimeOriginal, ExifTag.SubsecTimeDigitized, ExifTag.FlashpixVersion,
      ExifTag.ColorSpace, ExifTag.PixelXDimension, ExifTag.PixelYDimension,
      ExifTag.RelatedSoundFile, ExifTag.FlashEnergy, ExifTag.SpatialFrequencyResponse,
      ExifTag.FocalPlaneXResolution, ExifTag.FocalPlaneYResolution,
      ExifTag.FocalPlaneResolutionUnit, ExifTag.SubjectLocation, ExifTag.ExposureIndex,
      ExifTag.SensingMethod, ExifTag.FileSource, ExifTag.SceneType, ExifTag.CFAPattern,
      ExifTag.CustomRendered, ExifTag.ExposureMode, ExifTag.WhiteBalance,
      ExifTag.DigitalZoomRatio, ExifTag.FocalLengthIn35mmFilm, ExifTag.SceneCaptureType,
      ExifTag.GainControl, ExifTag.Contrast, ExifTag.Saturation, ExifTag.Sharpness,
      ExifTag.DeviceSettingDescription, ExifTag.SubjectDistanceRange, ExifTag.ImageUniqueID
    };

    private static readonly ExifTag[] _GPSTags = new ExifTag[31]
    {
      ExifTag.GPSVersionID, ExifTag.GPSLatitudeRef, ExifTag.GPSLatitude,
      ExifTag.GPSLongitudeRef, ExifTag.GPSLongitude, ExifTag.GPSAltitudeRef,
      ExifTag.GPSAltitude, ExifTag.GPSTimestamp, ExifTag.GPSSatellites, ExifTag.GPSStatus,
      ExifTag.GPSMeasureMode, ExifTag.GPSDOP, ExifTag.GPSSpeedRef, ExifTag.GPSSpeed,
      ExifTag.GPSTrackRef, ExifTag.GPSTrack, ExifTag.GPSImgDirectionRef,
      ExifTag.GPSImgDirection, ExifTag.GPSMapDatum, ExifTag.GPSDestLatitudeRef,
      ExifTag.GPSDestLatitude, ExifTag.GPSDestLongitudeRef, ExifTag.GPSDestLongitude,
      ExifTag.GPSDestBearingRef, ExifTag.GPSDestBearing, ExifTag.GPSDestDistanceRef,
      ExifTag.GPSDestDistance, ExifTag.GPSProcessingMethod, ExifTag.GPSAreaInformation,
      ExifTag.GPSDateStamp, ExifTag.GPSDifferential
    };

    private const int _StartIndex = 6;

    private ExifParts _AllowedParts;
    private bool _BestPrecision;
    private Collection<ExifValue> _Values;
    private Collection<int> _DataOffsets;
    private Collection<int> _IfdIndexes;
    private Collection<int> _ExifIndexes;
    private Collection<int> _GPSIndexes;

    private int GetIndex(Collection<int> indexes, ExifTag tag)
    {
      foreach (int index in indexes)
      {
        if (_Values[index].Tag == tag)
          return index;
      }

      int newIndex = _Values.Count;
      indexes.Add(newIndex);
      _Values.Add(ExifValue.Create(tag, null));
      return newIndex;
    }

    private Collection<int> GetIndexes(ExifParts part, ExifTag[] tags)
    {
      if (((int)_AllowedParts & (int)part) == 0)
        return new Collection<int>();

      Collection<int> result = new Collection<int>();
      for (int i = 0; i < _Values.Count; i++)
      {
        ExifValue value = _Values[i];

        if (!value.HasValue)
          continue;

        int index = Array.IndexOf(tags, value.Tag);
        if (index > -1)
          result.Add(i);
      }

      return result;
    }

    private uint GetLength(IEnumerable<int> indexes)
    {
      uint length = 0;

      foreach (int index in indexes)
      {
        uint valueLength = (uint)_Values[index].Length;

        if (valueLength > 4)
          length += 12 + valueLength;
        else
          length += 12;
      }

      return length;
    }

    private static int Write(byte[] source, byte[] destination, int offset)
    {
      Buffer.BlockCopy(source, 0, destination, offset, source.Length);

      return offset + source.Length;
    }

    private int WriteArray(ExifValue value, byte[] destination, int offset)
    {
      if (value.DataType == ExifDataType.Ascii)
        return WriteValue(ExifDataType.Ascii, value.Value, destination, offset);

      int newOffset = offset;
      foreach (object obj in (Array)value.Value)
        newOffset = WriteValue(value.DataType, obj, destination, newOffset);

      return newOffset;
    }

    private int WriteData(Collection<int> indexes, byte[] destination, int offset)
    {
      if (_DataOffsets.Count == 0)
        return offset;

      int newOffset = offset;

      int i = 0;
      foreach (int index in indexes)
      {
        ExifValue value = _Values[index];
        if (value.Length > 4)
        {
          Write(BitConverter.GetBytes(newOffset - _StartIndex), destination, _DataOffsets[i++]);
          newOffset = WriteValue(value, destination, newOffset);
        }
      }

      return newOffset;
    }

    private int WriteHeaders(Collection<int> indexes, byte[] destination, int offset)
    {
      _DataOffsets = new Collection<int>();

      int newOffset = Write(BitConverter.GetBytes((ushort)indexes.Count), destination, offset);

      if (indexes.Count == 0)
        return newOffset;

      foreach (int index in indexes)
      {
        ExifValue value = _Values[index];
        newOffset = Write(BitConverter.GetBytes((ushort)value.Tag), destination, newOffset);
        newOffset = Write(BitConverter.GetBytes((ushort)value.DataType), destination, newOffset);
        newOffset = Write(BitConverter.GetBytes((uint)value.NumberOfComponents), destination, newOffset);

        if (value.Length > 4)
          _DataOffsets.Add(newOffset);
        else
          WriteValue(value, destination, newOffset);

        newOffset += 4;
      }

      return newOffset;
    }

    private int WriteRational(double value, byte[] destination, int offset)
    {
      uint numerator = 1;
      uint denominator = 1;

      if (double.IsPositiveInfinity(value))
        denominator = 0;
      else if (double.IsNegativeInfinity(value))
        denominator = 0;
      else
      {
        double val = Math.Abs(value);
        double df = numerator / denominator;
        double epsilon = _BestPrecision ? double.Epsilon : .000001;

        while (Math.Abs(df - val) > epsilon)
        {
          if (df < val)
            numerator++;
          else
          {
            denominator++;
            numerator = (uint)(val * denominator);
          }

          df = numerator / (double)denominator;
        }
      }

      Write(BitConverter.GetBytes(numerator), destination, offset);
      Write(BitConverter.GetBytes(denominator), destination, offset + 4);

      return offset + 8;
    }

    private int WriteSignedRational(double value, byte[] destination, int offset)
    {
      int numerator = 1;
      int denominator = 1;

      if (double.IsPositiveInfinity(value))
        denominator = 0;
      else if (double.IsNegativeInfinity(value))
        denominator = 0;
      else
      {
        double val = Math.Abs(value);
        double df = numerator / denominator;
        double epsilon = _BestPrecision ? double.Epsilon : .000001;

        while (Math.Abs(df - val) > epsilon)
        {
          if (df < val)
            numerator++;
          else
          {
            denominator++;
            numerator = (int)(val * denominator);
          }

          df = numerator / (double)denominator;
        }
      }

      Write(BitConverter.GetBytes(numerator * (value < 0.0 ? -1 : 1)), destination, offset);
      Write(BitConverter.GetBytes(denominator), destination, offset + 4);

      return offset + 8;
    }

    private int WriteValue(ExifDataType dataType, object value, byte[] destination, int offset)
    {
      switch (dataType)
      {
        case ExifDataType.Ascii:
          return Write(Encoding.UTF8.GetBytes((string)value), destination, offset);
        case ExifDataType.Byte:
        case ExifDataType.Undefined:
          destination[offset] = (byte)value;
          return offset + 1;
        case ExifDataType.DoubleFloat:
          return Write(BitConverter.GetBytes((double)value), destination, offset);
        case ExifDataType.Short:
          return Write(BitConverter.GetBytes((ushort)value), destination, offset);
        case ExifDataType.Long:
          return Write(BitConverter.GetBytes((uint)value), destination, offset);
        case ExifDataType.Rational:
          return WriteRational((double)value, destination, offset);
        case ExifDataType.SignedByte:
          destination[offset] = unchecked((byte)((sbyte)value));
          return offset + 1;
        case ExifDataType.SignedLong:
          return Write(BitConverter.GetBytes((int)value), destination, offset);
        case ExifDataType.SignedShort:
          return Write(BitConverter.GetBytes((short)value), destination, offset);
        case ExifDataType.SignedRational:
          return WriteSignedRational((double)value, destination, offset);
        case ExifDataType.SingleFloat:
          return Write(BitConverter.GetBytes((float)value), destination, offset);
        default:
          throw new NotSupportedException();
      }
    }

    private int WriteValue(ExifValue value, byte[] destination, int offset)
    {
      if (value.IsArray && value.DataType != ExifDataType.Ascii)
        return WriteArray(value, destination, offset);
      else
        return WriteValue(value.DataType, value.Value, destination, offset);
    }

    public ExifWriter(Collection<ExifValue> values, ExifParts allowedParts, bool bestPrecision)
    {
      _Values = values;
      _AllowedParts = allowedParts;
      _BestPrecision = bestPrecision;

      _IfdIndexes = GetIndexes(ExifParts.IfdTags, _IfdTags);
      _ExifIndexes = GetIndexes(ExifParts.ExifTags, _ExifTags);
      _GPSIndexes = GetIndexes(ExifParts.GPSTags, _GPSTags);
    }

    public byte[] GetData()
    {
      uint length = 0;
      int exifIndex = -1;
      int gpsIndex = -1;

      if (_ExifIndexes.Count > 0)
        exifIndex = (int)GetIndex(_IfdIndexes, ExifTag.SubIFDOffset);

      if (_GPSIndexes.Count > 0)
        gpsIndex = (int)GetIndex(_IfdIndexes, ExifTag.GPSIFDOffset);

      uint ifdLength = 2 + GetLength(_IfdIndexes) + 4;
      uint exifLength = GetLength(_ExifIndexes);
      uint gpsLength = GetLength(_GPSIndexes);

      if (exifLength > 0)
        exifLength += 2;

      if (gpsLength > 0)
        gpsLength += 2;

      length = ifdLength + exifLength + gpsLength;

      if (length == 6)
        return null;

      length += 10 + 4 + 2;

      byte[] result = new byte[length];
      result[0] = (byte)'E';
      result[1] = (byte)'x';
      result[2] = (byte)'i';
      result[3] = (byte)'f';
      result[4] = 0x00;
      result[5] = 0x00;
      result[6] = (byte)'I';
      result[7] = (byte)'I';
      result[8] = 0x2A;
      result[9] = 0x00;

      int i = 10;
      uint ifdOffset = ((uint)i - _StartIndex) + 4;
      uint thumbnailOffset = ifdOffset + ifdLength + exifLength + gpsLength;

      if (exifLength > 0)
        _Values[exifIndex].Value = (ifdOffset + ifdLength);

      if (gpsLength > 0)
        _Values[gpsIndex].Value = (ifdOffset + ifdLength + exifLength);

      i = Write(BitConverter.GetBytes(ifdOffset), result, i);
      i = WriteHeaders(_IfdIndexes, result, i);
      i = Write(BitConverter.GetBytes(thumbnailOffset), result, i);
      i = WriteData(_IfdIndexes, result, i);

      if (exifLength > 0)
      {
        i = WriteHeaders(_ExifIndexes, result, i);
        i = WriteData(_ExifIndexes, result, i);
      }

      if (gpsLength > 0)
      {
        i = WriteHeaders(_GPSIndexes, result, i);
        i = WriteData(_GPSIndexes, result, i);
      }

      Write(BitConverter.GetBytes((ushort)0), result, i);

      return result;
    }
  }
}