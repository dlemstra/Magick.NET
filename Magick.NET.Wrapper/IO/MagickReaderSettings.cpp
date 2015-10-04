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
#include "MagickReaderSettings.h"

using namespace System::Globalization;

namespace ImageMagick
{
  namespace Wrapper
  {
    String^ MagickReaderSettings::Scenes::get()
    {
      if (!_Settings->FrameIndex.HasValue && !_Settings->FrameCount.HasValue)
        return nullptr;

      if (_Settings->FrameIndex.HasValue && (!_Settings->FrameCount.HasValue || _Settings->FrameCount.Value == 1))
        return _Settings->FrameIndex.Value.ToString(CultureInfo::InvariantCulture);

      int frame = _Settings->FrameIndex.HasValue ? _Settings->FrameIndex.Value : 0;
      return String::Format(CultureInfo::InvariantCulture, "{0}-{1}", frame, frame + _Settings->FrameCount.Value);
    }

    void MagickReaderSettings::ApplyColorSpace(MagickCore::ImageInfo *imageInfo)
    {
      if (_Settings->ColorSpace.HasValue)
        imageInfo->colorspace = (Magick::ColorspaceType)_Settings->ColorSpace.Value;
    }

    void MagickReaderSettings::ApplyDefines(MagickCore::ImageInfo *imageInfo)
    {
      for each (IDefine^ define in _Settings->AllDefines)
      {
        SetOption(imageInfo, GetDefineKey(define->Format, define->Name), define->Value);
      }
    }

    void MagickReaderSettings::ApplyDensity(MagickCore::ImageInfo *imageInfo)
    {
      if (!_Settings->Density.HasValue)
        return;

      if (imageInfo->density != (char*)NULL)
        imageInfo->density = MagickCore::DestroyString(imageInfo->density);

      const Magick::Point* point = new Magick::Point(_Settings->Density.Value.X, _Settings->Density.Value.Y);
      std::string pointStr = *point;
      MagickCore::CloneString(&imageInfo->density, pointStr.c_str());
      delete point;
    }

    void MagickReaderSettings::ApplyDimensions(MagickCore::ImageInfo *imageInfo)
    {
      if (!_Settings->Width.HasValue || !_Settings->Height.HasValue)
        return;

      if (imageInfo->size != (char*)NULL)
        imageInfo->size = MagickCore::DestroyString(imageInfo->size);

      Magick::Geometry geometry = Magick::Geometry(_Settings->Width.Value, _Settings->Height.Value);
      std::string geometryStr = geometry;
      MagickCore::CloneString(&imageInfo->size, geometryStr.c_str());
    }

    void MagickReaderSettings::ApplyFormat(MagickCore::ImageInfo *imageInfo)
    {
      if (!_Settings->Format.HasValue)
        return;

      std::string name;
      Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, _Settings->Format.Value) + ":", name);
      MagickCore::CopyMagickString(imageInfo->filename, name.c_str(), MagickPathExtent - 1);
    }

    void MagickReaderSettings::ApplyFrame(MagickCore::ImageInfo *imageInfo)
    {
      if (!_Settings->FrameIndex.HasValue && !_Settings->FrameCount.HasValue)
        return;

      if (imageInfo->scenes != (char*)NULL)
        imageInfo->scenes = MagickCore::DestroyString(imageInfo->scenes);

      std::string scenes;
      Marshaller::Marshal(Scenes, scenes);
      MagickCore::CloneString(&imageInfo->scenes, scenes.c_str());

      imageInfo->scene = _Settings->FrameIndex.HasValue ? _Settings->FrameIndex.Value : 0;
      imageInfo->number_scenes = _Settings->FrameCount.HasValue ? _Settings->FrameCount.Value : 1;
    }

    void MagickReaderSettings::ApplyUseMonochrome(MagickCore::ImageInfo *imageInfo)
    {
      if (_Settings->UseMonochrome.HasValue)
        imageInfo->monochrome = _Settings->UseMonochrome.Value ? Magick::MagickTrue : Magick::MagickFalse;
    }

    String^ MagickReaderSettings::GetDefineKey(MagickFormat format, String^ name)
    {
      if (format == MagickFormat::Unknown)
        return name;

      return Enum::GetName(MagickFormat::typeid, format) + ":" + name;
    }

    void MagickReaderSettings::SetOption(MagickCore::ImageInfo *imageInfo, String^ key, String^ value)
    {
      std::string option;
      Marshaller::Marshal(key, option);
      std::string val;
      Marshaller::Marshal(value, val);
      (void)MagickCore::SetImageOption(imageInfo, option.c_str(), val.c_str());
    }

    Nullable<int> MagickReaderSettings::Height::get()
    {
      return _Settings->Height;
    }

    PixelStorageSettings^ MagickReaderSettings::PixelStorage::get()
    {
      return _Settings->PixelStorage;
    }

    Nullable<int> MagickReaderSettings::Width::get()
    {
      return _Settings->Width;
    }

    void MagickReaderSettings::Apply(Magick::Image* image)
    {
      Apply(image->imageInfo());
    }

    void MagickReaderSettings::Apply(MagickCore::ImageInfo *imageInfo)
    {
      ApplyColorSpace(imageInfo);
      ApplyDefines(imageInfo);
      ApplyDensity(imageInfo);
      ApplyDimensions(imageInfo);
      ApplyFormat(imageInfo);
      ApplyFrame(imageInfo);
      ApplyUseMonochrome(imageInfo);
    }

    MagickReaderSettings::MagickReaderSettings()
    {
      _Settings = gcnew MagickReadSettings();
      IgnoreWarnings = true;
    }

    MagickReaderSettings::MagickReaderSettings(MagickReadSettings^ settings)
    {
      _Settings = settings;
      IgnoreWarnings = true;
    }
  }
}