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

#include "IDefines.h"
#include "MagickDefine.h"
#include "..\..\Arguments\MagickGeometry.h"

using namespace System::Collections::Generic;
using namespace System::Collections::ObjectModel;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Base class that can create defines.
	///</summary>
	public ref class DefineCreator abstract : public IDefines
	{
		//===========================================================================================
	private:
		//===========================================================================================
		MagickFormat _Format;
		//===========================================================================================
	protected private:
		//===========================================================================================
		DefineCreator(MagickFormat format);
		//===========================================================================================
		MagickDefine^ CreateDefine(String^ name, bool value);
		//===========================================================================================
		MagickDefine^ CreateDefine(String^ name, int value);
		//===========================================================================================
		MagickDefine^ CreateDefine(String^ name, MagickGeometry^ value);
		//===========================================================================================
		MagickDefine^ CreateDefine(String^ name, String^ value);
		//===========================================================================================
		generic<typename TEnum>
		where TEnum : value class, ValueType
		MagickDefine^ CreateDefine(String^ name, TEnum value);
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void AddDefines(Collection<IDefine^>^ defines) abstract;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// The defines that should be set as an define on an image
		///</summary>
		property IEnumerable<IDefine^>^ Defines
		{
			virtual IEnumerable<IDefine^>^ get();
		}
		//===========================================================================================
	};
	//==============================================================================================
}