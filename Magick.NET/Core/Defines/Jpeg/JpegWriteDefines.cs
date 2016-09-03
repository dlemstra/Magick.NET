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

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick
{
  ///<summary>
  /// Class for defines that are used when a jpeg image is written.
  ///</summary>
  public sealed class JpegWriteDefines : DefineCreator, IWriteDefines
  {
    ///<summary>
    /// Initializes a new instance of the <see cref="JpegWriteDefines"/> class.
    ///</summary>
    public JpegWriteDefines()
      : base(MagickFormat.Jpeg)
    {
    }

    MagickFormat IWriteDefines.Format
    {
      get
      {
        return Format;
      }
    }

    ///<summary>
    /// Specifies the dtc method that will be used (jpeg:dct-method).
    ///</summary>
    public DctMethod? DctMethod
    {
      get;
      set;
    }

    ///<summary>
    /// Search for compression quality that does not exceed the specified extent in kilobytes. (jpeg:extent).
    ///</summary>
    public int? Extent
    {
      get;
      set;
    }

    ///<summary>
    /// Enables or disables optimize coding (jpeg:optimize-coding).
    ///</summary>
    public bool? OptimizeCoding
    {
      get;
      set;
    }

    ///<summary>
    /// Set quality scaling for luminance and chrominance separately (jpeg:quality).
    ///</summary>
    public MagickGeometry Quality
    {
      get;
      set;
    }

    ///<summary>
    /// File name that contains custom quantization tables (jpeg:q-table).
    ///</summary>
    public string QuantizationTables
    {
      get;
      set;
    }

    ///<summary>
    /// Set jpeg sampling factor (jpeg:sampling-factor).
    ///</summary>
    public IEnumerable<MagickGeometry> SamplingFactors
    {
      get;
      set;
    }

    ///<summary>
    /// The defines that should be set as an define on an image
    ///</summary>
    public override IEnumerable<IDefine> Defines
    {
      get
      {
        if (DctMethod.HasValue)
          yield return CreateDefine("dct-method", DctMethod.Value);

        if (Extent.HasValue)
          yield return CreateDefine("extent", Extent.Value + "KB");

        if (OptimizeCoding.HasValue)
          yield return CreateDefine("optimize-coding", OptimizeCoding.Value);

        if (Quality != null)
          yield return CreateDefine("quality", Quality);

        if (!string.IsNullOrEmpty(QuantizationTables))
          yield return CreateDefine("q-table", QuantizationTables);

        if (SamplingFactors != null)
        {
          string value = "";
          foreach (MagickGeometry samplingFactor in SamplingFactors)
          {
            if (value.Length != 0)
              value += ",";

            value += samplingFactor.ToString();
          }

          if (!string.IsNullOrEmpty(value))
            yield return CreateDefine("sampling-factor", value);
        }
      }
    }
  }
}