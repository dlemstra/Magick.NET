//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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

namespace Magick
{
	//==============================================================================================
	template <class InputIterator>
	void combineImages(Image *combinedImage_, InputIterator first_, InputIterator last_,
		const ChannelType channel_)
	{
		MagickCore::ExceptionInfo exceptionInfo;
		MagickCore::GetExceptionInfo(&exceptionInfo);

		linkImages(first_, last_);

		MagickCore::Image* image = MagickCore::CombineImages(first_->image(), channel_, &exceptionInfo);

		unlinkImages(first_, last_);
		combinedImage_->replaceImage(image);

		throwException(exceptionInfo);
		(void) MagickCore::DestroyExceptionInfo(&exceptionInfo);
	}
	//==============================================================================================
	template <class InputIterator>
	void evaluateImages(Image *evaluatedImage_, InputIterator first_, InputIterator last_,
		const MagickEvaluateOperator operator_)
	{
		MagickCore::ExceptionInfo exceptionInfo;
		MagickCore::GetExceptionInfo(&exceptionInfo);

		Magick::linkImages(first_, last_);

		MagickCore::Image* image = MagickCore::EvaluateImages(first_->image(), operator_, &exceptionInfo);

		Magick::unlinkImages(first_, last_);
		evaluatedImage_->replaceImage(image);

		Magick::throwException(exceptionInfo);
		(void) MagickCore::DestroyExceptionInfo(&exceptionInfo);
	}
	//==============================================================================================
	template <class InputIterator>
	void mergeImages(Image *mergedImage_, InputIterator first_, InputIterator last_,
		const ImageLayerMethod method_)
	{
		MagickCore::ExceptionInfo exceptionInfo;
		MagickCore::GetExceptionInfo(&exceptionInfo);

		linkImages(first_, last_);

		MagickCore::Image* image = MagickCore::MergeImageLayers(first_->image(), method_, &exceptionInfo);

		unlinkImages(first_, last_);
		mergedImage_->replaceImage(image);

		throwException(exceptionInfo);
		(void) MagickCore::DestroyExceptionInfo(&exceptionInfo);
	}
	//==============================================================================================
	template <class InputIterator, class Container>
	void optimizeImageLayers(Container *optimizedImages_, InputIterator first_, InputIterator last_)
	{
		MagickCore::ExceptionInfo exceptionInfo;
		MagickCore::GetExceptionInfo(&exceptionInfo);

		linkImages(first_, last_);

		MagickCore::Image* images = MagickCore::OptimizeImageLayers(first_->image(), &exceptionInfo);

		unlinkImages(first_, last_);
		optimizedImages_->clear();
		insertImages(optimizedImages_, images);

		throwException(exceptionInfo);
		(void) MagickCore::DestroyExceptionInfo(&exceptionInfo);
	}
	//==============================================================================================
	template <class InputIterator, class Container>
	void optimizePlusImageLayers(Container *optimizedImages_, InputIterator first_, InputIterator last_)
	{
		MagickCore::ExceptionInfo exceptionInfo;
		MagickCore::GetExceptionInfo(&exceptionInfo);

		linkImages(first_, last_);

		MagickCore::Image* images = MagickCore::OptimizePlusImageLayers(first_->image(), &exceptionInfo);

		unlinkImages(first_, last_);
		optimizedImages_->clear();
		insertImages(optimizedImages_, images);

		throwException(exceptionInfo);
		(void) MagickCore::DestroyExceptionInfo(&exceptionInfo);
	}
	//==============================================================================================
	template <class Container>
	void separateImages(Container *separateImages_, const Image &image_, const ChannelType channel_)
	{
		MagickCore::ExceptionInfo exceptionInfo;
		MagickCore::GetExceptionInfo(&exceptionInfo);

		MagickCore::Image* images = MagickCore::SeparateImages(image_.constImage(), channel_, &exceptionInfo);

		separateImages_->clear();
		insertImages(separateImages_, images);

		throwException(exceptionInfo);
		(void) MagickCore::DestroyExceptionInfo(&exceptionInfo);
	}
	//==============================================================================================
}