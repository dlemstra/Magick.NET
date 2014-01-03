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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Specifies log events.
	///</summary>
	[FlagsAttribute]
	public enum class LogEvents
	{
		None = Magick::NoEvents,
		Trace = Magick::TraceEvent,
		Annotate = Magick::AnnotateEvent,
		Blob = Magick::BlobEvent,
		Cache =  Magick::CacheEvent,
		Coder =  Magick::CoderEvent,
		Configure = Magick::ConfigureEvent,
		Deprecate = Magick::DeprecateEvent,
		Draw = Magick::DrawEvent,
		Exception = Magick::ExceptionEvent,
		Image = Magick::ImageEvent,
		Locale = Magick::LocaleEvent,
		Module = Magick::ModuleEvent,
		Policy = Magick::PolicyEvent,
		Resource = Magick::ResourceEvent,
		Transform = Magick::TransformEvent,
		User = Magick::UserEvent,
		Wand = Magick::WandEvent,
		Accelerate = Magick::AccelerateEvent,
		All  = Magick::AllEvents
	};
	//==============================================================================================
}