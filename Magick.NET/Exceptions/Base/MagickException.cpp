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
#include "MagickException.h"
#include "..\MagickErrorExceptions.h"
#include "..\MagickWarningExceptions.h"
#include "..\..\MagickNET.h"

namespace ImageMagick
{
	//==============================================================================================
	MagickException::MagickException(String^ message, MagickException^ innerException)
		: Exception(message, innerException)
	{
	}
	//==============================================================================================
	MagickException^ MagickException::Create(const Magick::Exception& exception)
	{
		const Magick::Warning* warning = dynamic_cast<const Magick::Warning*>(&exception);

		if (warning != NULL)
			return MagickWarningException::Create(*warning);

		const Magick::Error* error = dynamic_cast<const Magick::Error*>(&exception);

		if (error != NULL)
			return MagickErrorException::Create(*error);

		String^ message = Marshaller::Marshal(exception.what());
		throw gcnew MagickException(message, nullptr);
	}
	//==============================================================================================
	void MagickException::Throw(const Magick::Exception& exception)
	{
		throw Create(exception);
	}
	//==============================================================================================
}
