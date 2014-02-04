//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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
	//==============================================================================================
	ref class MagickImage;
	///=============================================================================================
	///<summary>
	/// Result for a sub image search operation.
	///</summary>
	public ref class MagickSearchResult sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		MagickGeometry^ _BestMatch;
		MagickImage^ _SimilarityImage;
		double _SimilarityMetric;
		//===========================================================================================
		!MagickSearchResult();
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickSearchResult(const Magick::Image& image, Magick::Geometry bestMatch, double similarityMetric);
		//===========================================================================================
	public:
		//===========================================================================================
		~MagickSearchResult()
		{
			this->!MagickSearchResult();
		}
		///==========================================================================================
		///<summary>
		/// The offset for the best match.
		///</summary>
		property MagickGeometry^ BestMatch
		{
			MagickGeometry^ get();
		}
		///==========================================================================================
		///<summary>
		/// A similarity image such that an exact match location is completely white and if none of
		/// the pixels match, black, otherwise some gray level in-between.
		///</summary>
		property MagickImage^ SimilarityImage
		{
			MagickImage^ get();
		}
		///==========================================================================================
		///<summary>
		/// Similarity metric.
		///</summary>
		property double SimilarityMetric
		{
			double get();
		}
		//===========================================================================================
	};
	//==============================================================================================
}