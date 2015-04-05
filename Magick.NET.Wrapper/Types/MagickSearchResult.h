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

#include "Stdafx.h"
#include "MagickGeometry.h"

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		ref class MagickImage;
		///==========================================================================================
		///<summary>
		/// Result for a sub image search operation.
		///</summary>
		private ref class MagickSearchResult sealed
		{
			//========================================================================================
		private:
			//========================================================================================
			MagickGeometry^ _BestMatch;
			MagickImage^ _SimilarityImage;
			double _SimilarityMetric;
			//========================================================================================
			!MagickSearchResult();
			//========================================================================================
		internal:
			//========================================================================================
			MagickSearchResult(const Magick::Image& image, Magick::Geometry bestMatch, double similarityMetric);
			//========================================================================================
		public:
			//========================================================================================
			~MagickSearchResult()
			{
				this->!MagickSearchResult();
			}
			//========================================================================================
			property MagickGeometry^ BestMatch
			{
				MagickGeometry^ get();
			}
			//========================================================================================
			property MagickImage^ SimilarityImage
			{
				MagickImage^ get();
			}
			//========================================================================================
			property double SimilarityMetric
			{
				double get();
			}
			//========================================================================================
		};
		//===========================================================================================
	}
}