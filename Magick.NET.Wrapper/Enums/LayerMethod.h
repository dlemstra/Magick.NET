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
#pragma once

namespace ImageMagick
{
  namespace Wrapper
  {
    ///<summary>
    /// The method of selecting the size of the initial canvas.
    ///</summary>
    private enum class LayerMethod
    {
      Undefined = Magick::UndefinedLayer,
      Coalesce = Magick::CoalesceLayer,
      CompareAny = Magick::CompareAnyLayer,
      CompareClear = Magick::CompareClearLayer,
      CompareOverlay = Magick::CompareOverlayLayer,
      Dispose = Magick::DisposeLayer,
      Optimize = Magick::OptimizeLayer,
      OptimizeImage = Magick::OptimizeImageLayer,
      OptimizePlus = Magick::OptimizePlusLayer,
      OptimizeTrans = Magick::OptimizeTransLayer,
      RemoveDups = Magick::RemoveDupsLayer,
      RemoveZero = Magick::RemoveZeroLayer,
      Composite = Magick::CompositeLayer,
      Merge = Magick::MergeLayer,
      Flatten = Magick::FlattenLayer,
      Mosaic = Magick::MosaicLayer,
      Trimbounds = Magick::TrimBoundsLayer
    };
  }
}