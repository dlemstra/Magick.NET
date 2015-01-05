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
#include "Stdafx.h"
#include "..\MagickImage.h"
#include "MagickSearchResult.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	MagickSearchResult::!MagickSearchResult()
	{
		if (_SimilarityImage == nullptr)
			return;

		delete _SimilarityImage;
		_SimilarityImage = nullptr;
	}
	//==============================================================================================
	MagickSearchResult::MagickSearchResult(const Magick::Image& image, Magick::Geometry bestMatch, double similarityMetric)
	{
		_SimilarityImage = gcnew MagickImage(image);
		_BestMatch = gcnew MagickGeometry(bestMatch);
		_SimilarityMetric = similarityMetric;
	}
	//==============================================================================================
	MagickGeometry^ MagickSearchResult::BestMatch::get()
	{
		return _BestMatch;
	}
	//==============================================================================================
	MagickImage^ MagickSearchResult::SimilarityImage::get()
	{
		return _SimilarityImage;
	}
	//==============================================================================================
	double MagickSearchResult::SimilarityMetric::get()
	{
		return _SimilarityMetric;
	}
	//==============================================================================================
}