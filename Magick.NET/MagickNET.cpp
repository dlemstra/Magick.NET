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
#include "MagickNET.h"

using namespace System::IO;
using namespace System::Security;

namespace ImageMagick
{
	//==============================================================================================
	bool MagickNET::Initialize()
	{
		String^ path = Path::GetDirectoryName(MagickNET::typeid->Assembly->Location) + "\\ImageMagick";

		return Initialize(path);
	}
	//==============================================================================================
	bool MagickNET::Initialize(String^ path)
	{
		Throw::IfNullOrEmpty("path", path);

		path = Path::GetFullPath(path);

		Throw::IfFalse("path", Directory::Exists(path), "Unable to find path: " + path);

		try
		{
			String^ envPath = Environment::GetEnvironmentVariable("PATH");
			if (envPath->IndexOf(path, StringComparison::Ordinal) != -1)
				return true;

			if (!envPath->StartsWith(";", StringComparison::OrdinalIgnoreCase))
				envPath = ";" + envPath;

			envPath = path + envPath;

			Environment::SetEnvironmentVariable("PATH", envPath);
			return true;
		}
		catch(SecurityException^)
		{
			return false;
		}
	}
	//==============================================================================================
	void MagickNET::SetCacheThreshold(int threshold)
	{
		Magick::Image::cacheThreshold(threshold);
	}
	//==============================================================================================
}
