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
#include "Stdafx.h"
#include "ResourceLimits.h"

namespace ImageMagick
{
	//==============================================================================================
	Magick::MagickSizeType ResourceLimits::Disk::get()
	{
		return Magick::ResourceLimits::disk();
	}
	//==============================================================================================
	void ResourceLimits::Disk::set(Magick::MagickSizeType limit)
	{
		Magick::ResourceLimits::disk(limit);
	}
	//==============================================================================================
	Magick::MagickSizeType ResourceLimits::Height::get()
	{
		return Magick::ResourceLimits::height();
	}
	//==============================================================================================
	void ResourceLimits::Height::set(Magick::MagickSizeType limit)
	{
		Magick::ResourceLimits::height(limit);
	}
	//==============================================================================================
	Magick::MagickSizeType ResourceLimits::Memory::get()
	{
		return Magick::ResourceLimits::area();
	}
	//==============================================================================================
	void ResourceLimits::Memory::set(Magick::MagickSizeType limit)
	{
		Magick::ResourceLimits::area(limit);
		Magick::ResourceLimits::memory(limit);
	}
	//==============================================================================================
	Magick::MagickSizeType ResourceLimits::Thread::get()
	{
		return Magick::ResourceLimits::thread();
	}
	//==============================================================================================
	void ResourceLimits::Thread::set(Magick::MagickSizeType limit)
	{
		Magick::ResourceLimits::thread(limit);
	}
	//==============================================================================================
	Magick::MagickSizeType ResourceLimits::Throttle::get()
	{
		return Magick::ResourceLimits::throttle();
	}
	//==============================================================================================
	void ResourceLimits::Throttle::set(Magick::MagickSizeType limit)
	{
		Magick::ResourceLimits::throttle(limit);
	}
	//==============================================================================================
	Magick::MagickSizeType ResourceLimits::Width::get()
	{
		return Magick::ResourceLimits::width();
	}
	//==============================================================================================
	void ResourceLimits::Width::set(Magick::MagickSizeType limit)
	{
		Magick::ResourceLimits::width(limit);
	}
	//==============================================================================================
}