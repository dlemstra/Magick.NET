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
#include "MagickErrorExceptions.h"

namespace ImageMagick
{
	//==============================================================================================
	MagickErrorException^ MagickErrorException::Create(const Magick::Error& exception)
	{
		String^ message = Marshaller::Marshal(exception.what());

		if (typeid(exception) == typeid(Magick::ErrorBlob))
			return gcnew MagickBlobErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorCache))
			return gcnew MagickCacheErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorCoder))
			return gcnew MagickCoderErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorConfigure))
			return gcnew MagickConfigureErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorCorruptImage))
			return gcnew MagickCorruptImageErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorDelegate))
			return gcnew MagickDelegateErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorDraw))
			return gcnew MagickDrawErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorFileOpen))
			return gcnew MagickFileOpenErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorImage))
			return gcnew MagickImageErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorMissingDelegate))
			return gcnew MagickMissingDelegateErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorModule))
			return gcnew MagickModuleErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorOption))
			return gcnew MagickOptionErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorRegistry))
			return gcnew MagickRegistryErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorResourceLimit))
			return gcnew MagickResourceLimitErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorStream))
			return gcnew MagickStreamErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorType))
			return gcnew MagickTypeErrorException(message);

		if (typeid(exception) == typeid(Magick::ErrorUndefined))
			return gcnew MagickUndefinedErrorException(message);

		return gcnew MagickErrorException(message);
	}
	//==============================================================================================
}