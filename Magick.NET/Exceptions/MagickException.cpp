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
#include "MagickException.h"
#include "MagickErrorExceptions.h"
#include "MagickWarningExceptions.h"

namespace ImageMagick
{
	//==============================================================================================
	MagickException^ MagickException::CreateError(const Magick::Exception& exception, String^ message)
	{
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
	MagickException^ MagickException::CreateWarning(const Magick::Exception& exception, String^ message)
	{
		if (typeid(exception) == typeid(Magick::WarningBlob))
			return gcnew MagickBlobWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningCache))
			return gcnew MagickCacheWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningCoder))
			return gcnew MagickCoderWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningConfigure))
			return gcnew MagickConfigureWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningCorruptImage))
			return gcnew MagickCorruptImageWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningDelegate))
			return gcnew MagickDelegateWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningDraw))
			return gcnew MagickDrawWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningFileOpen))
			return gcnew MagickFileOpenWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningImage))
			return gcnew MagickImageWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningMissingDelegate))
			return gcnew MagickMissingDelegateWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningModule))
			return gcnew MagickModuleWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningOption))
			return gcnew MagickOptionWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningRegistry))
			return gcnew MagickRegistryWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningResourceLimit))
			return gcnew MagickResourceLimitWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningStream))
			return gcnew MagickStreamWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningType))
			return gcnew MagickTypeWarningException(message);

		if (typeid(exception) == typeid(Magick::WarningUndefined))
			return gcnew MagickUndefinedWarningException(message);

		return gcnew MagickWarningException(message);
	}
	//==============================================================================================
	MagickException::MagickException(String^ message)
		: Exception(message)
	{
	}
	//==============================================================================================
	MagickException^ MagickException::Create(const Magick::Exception& exception)
	{
		String^ message = Marshaller::Marshal(exception.what());

		if (dynamic_cast<const Magick::Error*>(&exception) != NULL)
			return CreateError(exception, message);

		if (dynamic_cast<const Magick::Warning*>(&exception) != NULL)
			return CreateWarning(exception, message);

		return gcnew MagickException(message);
	}
	//==============================================================================================
}
