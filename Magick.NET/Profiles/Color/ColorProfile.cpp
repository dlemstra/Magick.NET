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
#include "ColorProfile.h"

using namespace System::Reflection;
using namespace System::Threading;

namespace ImageMagick
{
	//==============================================================================================
	ColorProfile^ ColorProfile::Load(String^ resourceName)
	{
		Monitor::Enter(_SyncRoot);

		if (!_Profiles->ContainsKey(resourceName))
		{
			Stream^ stream = Assembly::GetAssembly(ColorProfile::typeid)->GetManifestResourceStream(resourceName);
			_Profiles[resourceName] = gcnew ColorProfile(stream);
			delete stream;
		}

		Monitor::Exit(_SyncRoot);

		return _Profiles[resourceName];
	}
	//==============================================================================================
	ColorProfile^ ColorProfile::AdobeRGB1998::get()
	{
		return Load("AdobeRGB1998.icc");
	}
	//==============================================================================================
	ColorProfile^ ColorProfile::AppleRGB::get()
	{
		return Load("AppleRGB.icc");
	}
	//==============================================================================================
	ColorProfile^ ColorProfile::CoatedFOGRA39::get()
	{
		return Load("CoatedFOGRA39.icc");
	}
	//==============================================================================================
	ColorProfile^ ColorProfile::ColorMatchRGB::get()
	{
		return Load("ColorMatchRGB.icc");
	}
	//==============================================================================================
	ColorProfile^ ColorProfile::SRGB::get()
	{
		return Load("SRGB.icm");
	}
	//==============================================================================================
	ColorProfile^ ColorProfile::USWebCoatedSWOP::get()
	{
		return Load("USWebCoatedSWOP.icc");
	}
	//==============================================================================================
}