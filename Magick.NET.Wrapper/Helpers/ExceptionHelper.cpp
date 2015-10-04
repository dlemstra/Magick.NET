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
#include "ExceptionHelper.h"

namespace ImageMagick
{
  namespace Wrapper
  {
    MagickException^ ExceptionHelper::Create(const Magick::Exception& exception)
    {
      const Magick::Warning* warning = dynamic_cast<const Magick::Warning*>(&exception);

      if (warning != NULL)
        return CreateWarning(*warning);

      const Magick::Error* error = dynamic_cast<const Magick::Error*>(&exception);

      if (error != NULL)
        return CreateError(*error);

      String^ message = Marshaller::Marshal(exception.what());
      throw gcnew MagickException(message, nullptr);
    }

    MagickErrorException^ ExceptionHelper::CreateError(const Magick::Error& exception)
    {
      String^ message = Marshaller::Marshal(exception.what());
      MagickException^ innerException = nullptr;
      if (exception.nested() != nullptr)
        innerException = ExceptionHelper::Create(*exception.nested());

      if (typeid(exception) == typeid(Magick::ErrorBlob))
        return gcnew MagickBlobErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorCache))
        return gcnew MagickCacheErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorCoder))
        return gcnew MagickCoderErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorConfigure))
        return gcnew MagickConfigureErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorCorruptImage))
        return gcnew MagickCorruptImageErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorDelegate))
        return gcnew MagickDelegateErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorDraw))
        return gcnew MagickDrawErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorFileOpen))
        return gcnew MagickFileOpenErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorImage))
        return gcnew MagickImageErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorMissingDelegate))
        return gcnew MagickMissingDelegateErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorModule))
        return gcnew MagickModuleErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorOption))
        return gcnew MagickOptionErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorRegistry))
        return gcnew MagickRegistryErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorResourceLimit))
        return gcnew MagickResourceLimitErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorStream))
        return gcnew MagickStreamErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorType))
        return gcnew MagickTypeErrorException(message, innerException);

      if (typeid(exception) == typeid(Magick::ErrorUndefined))
        return gcnew MagickUndefinedErrorException(message, innerException);

      return gcnew MagickErrorException(message, innerException);
    }

    MagickWarningException^ ExceptionHelper::CreateWarning(const Magick::Warning& exception)
    {
      String^ message = Marshaller::Marshal(exception.what());
      MagickException^ innerException = nullptr;
      if (exception.nested() != nullptr)
        innerException = ExceptionHelper::Create(*exception.nested());

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

    void ExceptionHelper::Throw(const Magick::Exception& exception)
    {
      throw Create(exception);
    }
  }
}
