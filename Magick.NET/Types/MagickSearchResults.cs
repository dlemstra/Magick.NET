//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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

namespace ImageMagick
{
  ///<summary>
  /// Result for a sub image search operation.
  ///</summary>
  public sealed class MagickSearchResult : IDisposable
  {
    private Wrapper.MagickSearchResult _Instance;

    internal MagickSearchResult(Wrapper.MagickSearchResult instance)
    {
      _Instance = instance;
    }

    ///<summary>
    /// The offset for the best match.
    ///</summary>
    public MagickGeometry BestMatch
    {
      get
      {
        return MagickGeometry.Create(_Instance.BestMatch);
      }
    }

    ///<summary>
    /// A similarity image such that an exact match location is completely white and if none of
    /// the pixels match, black, otherwise some gray level in-between.
    ///</summary>
    public MagickImage SimilarityImage
    {
      get
      {
        return MagickImage.Create(_Instance.SimilarityImage);
      }
    }

    ///<summary>
    /// Similarity metric.
    ///</summary>
    public double SimilarityMetric
    {
      get
      {
        return _Instance.SimilarityMetric;
      }
    }

    /// <summary>
    /// Disposes the MagickSearchResult instance.
    /// </summary>
    public void Dispose()
    {
      if (_Instance != null)
        _Instance.Dispose();
      _Instance = null;
    }
  }
}