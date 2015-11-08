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
#include "MagickImageInfo.h"
#include "Helpers\EnumHelper.h"
#include "Helpers\ExceptionHelper.h"
#include "IO\MagickReader.h"

using namespace System::Collections::ObjectModel;

namespace ImageMagick
{
  namespace Wrapper
  {
    MagickReaderSettings^ MagickImageInfo::CreateReadSettings()
    {
      MagickReaderSettings^ settings = gcnew MagickReaderSettings();
      settings->Ping = true;
      return settings;
    }

    IEnumerable<MagickImageInfo^>^ MagickImageInfo::Enumerate(std::vector<Magick::Image>* images)
    {
      Collection<MagickImageInfo^>^ result = gcnew Collection<MagickImageInfo^>();

      for (std::vector<Magick::Image>::iterator iter = images->begin(), end = images->end(); iter != end; ++iter)
      {
        MagickImageInfo^ info = gcnew MagickImageInfo();
        info->Initialize(&*iter);
        result->Add(info);
      }

      return result;
    }

    void MagickImageInfo::HandleException(MagickException^ exception)
    {
      if (exception == nullptr)
        return;

      MagickWarningException^ warning = dynamic_cast<MagickWarningException^>(exception);
      if (warning == nullptr)
        throw exception;
    }

    MagickException^ MagickImageInfo::Initialize(Magick::Image* image)
    {
      try
      {
        _ColorSpace = (ImageMagick::ColorSpace)image->colorSpace();
        _CompressionMethod = (ImageMagick::CompressionMethod)image->compressType();
        _Format = EnumHelper::Parse<MagickFormat>(Marshaller::Marshal(image->magick()), MagickFormat::Unknown);
        _FileName = Marshaller::Marshal(image->baseFilename());
        _Height = Convert::ToInt32(image->size().height());
        _ResolutionUnits = (ImageMagick::Resolution)image->resolutionUnits();
        _ResolutionX = image->xResolution();
        _ResolutionY = image->yResolution();
        _Width = Convert::ToInt32(image->size().width());
        return nullptr;
      }
      catch (Magick::Exception& exception)
      {
        return ExceptionHelper::Create(exception);
      }
    }

    ColorSpace MagickImageInfo::ColorSpace::get()
    {
      return _ColorSpace;
    }

    CompressionMethod MagickImageInfo::CompressionMethod::get()
    {
      return _CompressionMethod;
    }

    String^ MagickImageInfo::FileName::get()
    {
      return _FileName;
    }

    MagickFormat MagickImageInfo::Format::get()
    {
      return _Format;
    }

    int MagickImageInfo::Height::get()
    {
      return _Height;
    }

    Resolution MagickImageInfo::ResolutionUnits::get()
    {
      return _ResolutionUnits;
    }

    double MagickImageInfo::ResolutionX::get()
    {
      return _ResolutionX;
    }

    double MagickImageInfo::ResolutionY::get()
    {
      return _ResolutionY;
    }

    int MagickImageInfo::Width::get()
    {
      return _Width;
    }

    void MagickImageInfo::Read(array<Byte>^ data)
    {
      Magick::Image* image = new Magick::Image();
      try
      {
        HandleException(MagickReader::Read(image, data, CreateReadSettings()));
        HandleException(Initialize(image));
      }
      finally
      {
        delete image;
      }
    }

    void MagickImageInfo::Read(String^ fileName)
    {
      Magick::Image* image = new Magick::Image();
      try
      {
        HandleException(MagickReader::Read(image, fileName, CreateReadSettings()));
        HandleException(Initialize(image));
      }
      finally
      {
        delete image;
      }
    }

    void MagickImageInfo::Read(Stream^ stream)
    {
      Magick::Image* image = new Magick::Image();
      try
      {
        HandleException(MagickReader::Read(image, stream, CreateReadSettings()));
        HandleException(Initialize(image));
      }
      finally
      {
        delete image;
      }
    }

    IEnumerable<MagickImageInfo^>^ MagickImageInfo::ReadCollection(array<Byte>^ data)
    {
      std::vector<Magick::Image>* images = new std::vector<Magick::Image>();
      try
      {
        HandleException(MagickReader::Read(images, data, CreateReadSettings()));
        return Enumerate(images);
      }
      finally
      {
        delete images;
      }
    }

    IEnumerable<MagickImageInfo^>^ MagickImageInfo::ReadCollection(Stream^ stream)
    {
      std::vector<Magick::Image>* images = new std::vector<Magick::Image>();
      try
      {
        HandleException(MagickReader::Read(images, stream, CreateReadSettings()));
        return Enumerate(images);
      }
      finally
      {
        delete images;
      }
    }

    IEnumerable<MagickImageInfo^>^ MagickImageInfo::ReadCollection(String^ fileName)
    {
      std::vector<Magick::Image>* images = new std::vector<Magick::Image>();
      try
      {
        HandleException(MagickReader::Read(images, fileName, CreateReadSettings()));
        return Enumerate(images);
      }
      finally
      {
        delete images;
      }
    }
  }
}