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
#pragma once

using namespace System::Collections::Generic;
using namespace System::Collections::ObjectModel;

namespace ImageMagick
{
  namespace Wrapper
  {
    private ref class MagickFormatInfo sealed
    {
    private:
      String^ _Description;
      MagickFormat _Format;
      String^ _MimeType;
      MagickFormat _Module;
      Magick::CoderInfo* _CoderInfo;

      MagickFormatInfo() {};

      static void AddStealthCoder(std::vector<Magick::CoderInfo>* coderList, std::string name);

      static MagickFormat GetFormat(std::string name);

      static Collection<MagickFormatInfo^>^ LoadFormats();

    internal:
      static initonly Collection<MagickFormatInfo^>^ All = LoadFormats();

    public:
      property bool CanReadMultithreaded
      {
        bool get();
      }

      property bool CanWriteMultithreaded
      {
        bool get();
      }

      property String^ Description
      {
        String^ get();
      }

      property MagickFormat Format
      {
        MagickFormat get();
      }

      property bool IsMultiFrame
      {
        bool get();
      }

      property bool IsReadable
      {
        bool get();
      }

      property bool IsWritable
      {
        bool get();
      }

      property String^ MimeType
      {
        String^ get();
      }

      property MagickFormat Module
      {
        MagickFormat get();
      }

      bool Unregister(void);
    };
  }
}