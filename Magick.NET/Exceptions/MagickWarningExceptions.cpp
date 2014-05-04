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
#include "MagickWarningExceptions.h"

namespace ImageMagick
{
	//==============================================================================================
	MagickWarningException^ MagickWarningException::Create(const Magick::Warning& exception)
	{
		String^ message = Marshaller::Marshal(exception.what());
		MagickException^ innerException = nullptr;
		if (exception.nested() != nullptr)
			innerException = MagickException::Create(*exception.nested());

		if (typeid(exception) == typeid(Magick::WarningBlob))
			return gcnew MagickBlobWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningCache))
			return gcnew MagickCacheWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningCoder))
			return gcnew MagickCoderWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningConfigure))
			return gcnew MagickConfigureWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningCorruptImage))
			return gcnew MagickCorruptImageWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningDelegate))
			return gcnew MagickDelegateWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningDraw))
			return gcnew MagickDrawWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningFileOpen))
			return gcnew MagickFileOpenWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningImage))
			return gcnew MagickImageWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningMissingDelegate))
			return gcnew MagickMissingDelegateWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningModule))
			return gcnew MagickModuleWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningOption))
			return gcnew MagickOptionWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningRegistry))
			return gcnew MagickRegistryWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningResourceLimit))
			return gcnew MagickResourceLimitWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningStream))
			return gcnew MagickStreamWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningType))
			return gcnew MagickTypeWarningException(message, innerException);

		if (typeid(exception) == typeid(Magick::WarningUndefined))
			return gcnew MagickUndefinedWarningException(message, innerException);

		return gcnew MagickWarningException(message, innerException);
	}
	//==============================================================================================
}