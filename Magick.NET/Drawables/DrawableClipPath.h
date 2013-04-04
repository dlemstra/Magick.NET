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

#include "DrawableWrapper.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableClipPath object.
	///</summary>
	public ref class DrawableClipPath sealed : DrawableWrapper<Magick::DrawableClipPath>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableClipPath instance.
		///</summary>
		///<param name="clipPath">The ID of the clip path.</param>
		DrawableClipPath(String^ clipPath);
		///==========================================================================================
		///<summary>
		/// The ID of the clip path.
		///</summary>
		property String^ ClipPath
		{
			String^ get()
			{
				return Marshaller::Marshal(Value->clip_path());
			}
			void set(String^ value)
			{
				std::string id;
				Marshaller::Marshal(value, id);
				Value->clip_path(id);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}