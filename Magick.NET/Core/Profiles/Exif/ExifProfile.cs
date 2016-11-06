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
using System.IO;

namespace ImageMagick
{
  /// <summary>
  /// Class that can be used to access an Exif profile.
  /// </summary>
  public sealed class ExifProfile : ImageProfile
  {
    private Collection<ExifValue> _Values;
    private List<ExifTag> _InvalidTags;
    private int _ThumbnailOffset;
    private int _ThumbnailLength;

    private void Initialize()
    {
      Parts = ExifParts.All;
      _InvalidTags = new List<ExifTag>();
    }

    private void InitializeValues()
    {
      if (_Values != null)
        return;

      if (Data == null)
      {
        _Values = new Collection<ExifValue>();
        return;
      }

      ExifReader reader = new ExifReader();
      _Values = reader.Read(Data);
      _InvalidTags = new List<ExifTag>(reader.InvalidTags);
      _ThumbnailOffset = (int)reader.ThumbnailOffset;
      _ThumbnailLength = (int)reader.ThumbnailLength;
    }

    /// <summary>
    /// Updates the data of the profile.
    /// </summary>
    protected override void UpdateData()
    {
      if (_Values == null || _Values.Count == 0)
      {
        Data = null;
        return;
      }

      ExifWriter writer = new ExifWriter(_Values, Parts);
      Data = writer.GetData();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExifProfile"/> class.
    /// </summary>
    public ExifProfile()
      : base("exif")
    {
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExifProfile"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the exif profile from.</param>
    public ExifProfile(byte[] data)
      : base("exif", data)
    {
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExifProfile"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the exif profile file, or the relative
    /// exif profile file name.</param>
    public ExifProfile(string fileName)
      : base("exif", fileName)
    {
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExifProfile"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the exif profile from.</param>
    public ExifProfile(Stream stream)
      : base("exif", stream)
    {
      Initialize();
    }

    /// <summary>
    /// Gets or sets which parts will be written when the profile is added to an image.
    /// </summary>
    public ExifParts Parts
    {
      get;
      set;
    }

    /// <summary>
    /// Gets the tags that where found but contained an invalid value.
    /// </summary>
    public IEnumerable<ExifTag> InvalidTags
    {
      get
      {
        return _InvalidTags;
      }
    }

    /// <summary>
    /// Gets the values of this exif profile.
    /// </summary>
    public IEnumerable<ExifValue> Values
    {
      get
      {
        InitializeValues();
        return _Values;
      }
    }

    /// <summary>
    /// Returns the thumbnail in the exif profile when available.
    /// </summary>
    /// <returns>The thumbnail in the exif profile when available.</returns>
    public MagickImage CreateThumbnail()
    {
      InitializeValues();

      if (_ThumbnailOffset == 0 || _ThumbnailLength == 0)
        return null;

      if (Data.Length < (_ThumbnailOffset + _ThumbnailLength))
        return null;

      byte[] data = new byte[_ThumbnailLength];
      Array.Copy(Data, _ThumbnailOffset, data, 0, _ThumbnailLength);
      return new MagickImage(data);
    }

    /// <summary>
    /// Returns the value with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the exif value.</param>
    /// <returns>The value with the specified tag.</returns>
    public ExifValue GetValue(ExifTag tag)
    {
      foreach (ExifValue exifValue in Values)
      {
        if (exifValue.Tag == tag)
          return exifValue;
      }

      return null;
    }

    /// <summary>
    /// Removes the value with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the exif value.</param>
    /// <returns>True when the value was fount and removed.</returns>
    public bool RemoveValue(ExifTag tag)
    {
      InitializeValues();

      for (int i = 0; i < _Values.Count; i++)
      {
        if (_Values[i].Tag == tag)
        {
          _Values.RemoveAt(i);
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Sets the value of the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the exif value.</param>
    /// <param name="value">The value.</param>
    public void SetValue(ExifTag tag, object value)
    {
      foreach (ExifValue exifValue in Values)
      {
        if (exifValue.Tag == tag)
        {
          exifValue.Value = value;
          return;
        }
      }

      ExifValue newExifValue = ExifValue.Create(tag, value);
      _Values.Add(newExifValue);
    }
  }
}