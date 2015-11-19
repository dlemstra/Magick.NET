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
#include "MagickNET.h"
#include "Helpers\EnumHelper.h"
#include "Helpers\ExceptionHelper.h"

using namespace System::IO;
using namespace System::Security;

namespace ImageMagick
{
  namespace Wrapper
  {
    void MagickNET::OnLog(const Magick::LogEventType type, const char* text)
    {
      if (text == NULL)
        return;

      if (_ExternalLogDelegate == nullptr)
        return;

      std::string message = std::string(text);
      _ExternalLogDelegate((LogEvents)type, Marshaller::Marshal(message));
    }

    bool MagickNET::SetUseOpenCL(bool value)
    {
      try
      {
        if (value)
          return Magick::EnableOpenCL();
        else
          Magick::DisableOpenCL();
      }
      catch (Magick::Exception &exception)
      {
        ExceptionHelper::Throw(exception);
      }

      return false;
    }

    String^ MagickNET::Features::get()
    {
      std::string features = std::string(MagickCore::GetMagickFeatures());

      return Marshaller::Marshal(features);
    }

    IEnumerable<MagickFormatInfo^>^ MagickNET::SupportedFormats::get()
    {
      return MagickFormatInfo::All;
    }

    bool MagickNET::UseOpenCL::get()
    {
      if (!_UseOpenCL.HasValue)
        _UseOpenCL = SetUseOpenCL(true);

      return _UseOpenCL.Value;
    }

    void MagickNET::UseOpenCL::set(bool value)
    {
      _UseOpenCL = SetUseOpenCL(value);
    }

    MagickFormatInfo^ MagickNET::GetFormatInformation(MagickFormat format)
    {
      if (format == MagickFormat::Unknown)
        return nullptr;

      for each (MagickFormatInfo^ formatInfo in SupportedFormats)
      {
        if (formatInfo->Format == format)
          return formatInfo;
      }

      return nullptr;
    }

    void MagickNET::SetEnv(String^ name, String^ value)
    {
      std::string envName;
      std::string envValue;

      _putenv_s(Marshaller::Marshal(name, envName).c_str(), Marshaller::Marshal(value, envValue).c_str());
    }

    void MagickNET::SetLogDelegate(MagickLogDelegate^ logDelegate)
    {
      _ExternalLogDelegate = logDelegate;

      if (logDelegate == nullptr && _InternalLogDelegate != nullptr)
      {
        MagickCore::SetLogMethod((MagickCore::MagickLogMethod)NULL);
        _InternalLogDelegate = nullptr;
      }
      else if (logDelegate != nullptr && _InternalLogDelegate == nullptr)
      {
        _InternalLogDelegate = gcnew MagickLogFuncDelegate(&OnLog);
        MagickCore::SetLogMethod((MagickCore::MagickLogMethod)Marshal::GetFunctionPointerForDelegate(_InternalLogDelegate).ToPointer());
      }
    }

    void MagickNET::SetLogEvents(String^ events)
    {
      std::string logEvents;
      Marshaller::Marshal(events, logEvents);
      MagickCore::SetLogEventMask(logEvents.c_str());
    }

    void MagickNET::SetRandomSeed(int seed)
    {
      Magick::SetRandomSeed(seed);
    }
  }
}