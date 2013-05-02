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
#include "stdafx.h"
#include "ColorProfile.h"

using namespace System::IO;
using namespace System::Reflection;
using namespace System::Threading;

namespace ImageMagick
{
	//==============================================================================================
	array<Byte>^ ColorProfile::LoadSRGbicm()
	{
		Monitor::Enter(_SyncRoot);

		if (_SRGBicm == nullptr)
		{
			Stream^ srgbIcm = Assembly::GetAssembly(ColorProfile::typeid)->GetManifestResourceStream("sRGB.icm");
			_SRGBicm = gcnew array<Byte>((int)srgbIcm->Length);
			srgbIcm->Read(_SRGBicm, 0, _SRGBicm->Length);
			delete srgbIcm;
		}

		Monitor::Exit(_SyncRoot);

		return _SRGBicm;
	}
	//==============================================================================================
}