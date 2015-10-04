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
#include "MagickImageCollection.h"
#include "Helpers\ExceptionHelper.h"
#include "IO\MagickReader.h"
#include "IO\MagickWriter.h"

using namespace System::Collections::ObjectModel;

namespace ImageMagick
{
  namespace Wrapper
  {
    MagickReaderSettings^ MagickImageCollection::CheckSettings(MagickReadSettings^ readSettings)
    {
      MagickReaderSettings^ settings = readSettings == nullptr ? gcnew MagickReaderSettings() : gcnew MagickReaderSettings(readSettings);
      settings->IgnoreWarnings = (_WarningEvent == nullptr);

      return settings;
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Copy(std::vector<Magick::Image>* images)
    {
      Collection<MagickImage^>^ result = gcnew Collection<MagickImage^>();

      for (std::vector<Magick::Image>::iterator iter = images->begin(), end = images->end(); iter != end; ++iter)
      {
        result->Add(gcnew MagickImage(*iter));
      }

      return result;
    }

    void MagickImageCollection::Copy(IEnumerable<MagickImage^>^ source, std::vector<Magick::Image>* destination)
    {
      for each(MagickImage^ image in source)
      {
        destination->push_back(image->ReuseValue());
      }
    }

    void MagickImageCollection::HandleException(const Magick::Exception& exception)
    {
      HandleException(ExceptionHelper::Create(exception));
    }

    void MagickImageCollection::HandleException(MagickException^ exception)
    {
      if (exception == nullptr)
        return;

      MagickWarningException^ warning = dynamic_cast<MagickWarningException^>(exception);
      if (warning == nullptr)
        throw exception;

      if (_WarningEvent != nullptr)
        _WarningEvent->Invoke(this, gcnew WarningEventArgs(warning));
    }

    bool MagickImageCollection::Merge(IEnumerable<MagickImage^>^ images, Magick::Image* image, LayerMethod method)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return false;

        Magick::mergeImageLayers(image, magickImages->begin(), magickImages->end(), (Magick::LayerMethod)method);
        return true;
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return false;
      }
      finally
      {
        delete magickImages;
      }
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Optimize(IEnumerable<MagickImage^>^ images, LayerMethod optizeMethod)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();
      std::vector<Magick::Image>* optimizedImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);

        if (optizeMethod == LayerMethod::OptimizeTrans)
        {
          Magick::optimizeTransparency(magickImages->begin(), magickImages->end());
          return Copy(magickImages);
        }
        else

        {
          if (optizeMethod == LayerMethod::OptimizeImage)
            Magick::optimizeImageLayers(optimizedImages, magickImages->begin(), magickImages->end());
          else
            Magick::optimizePlusImageLayers(optimizedImages, magickImages->begin(), magickImages->end());
          return Copy(magickImages);
        }
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return gcnew Collection<MagickImage^>();
      }
      finally
      {
        delete magickImages;
        delete optimizedImages;
      }
    }

    MagickImageCollection::MagickImageCollection()
    {
    }

    void MagickImageCollection::Warning::add(EventHandler<WarningEventArgs^>^ handler)
    {
      _WarningEvent += handler;
    }

    void MagickImageCollection::Warning::remove(EventHandler<WarningEventArgs^>^ handler)
    {
      _WarningEvent -= handler;
    }

    MagickImage^ MagickImageCollection::Append(IEnumerable<MagickImage^>^ images, bool vertically)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return nullptr;

        Magick::Image appendedImage;
        Magick::appendImages(&appendedImage, magickImages->begin(), magickImages->end(), vertically);

        return gcnew MagickImage(appendedImage);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return nullptr;
      }
      finally
      {
        delete magickImages;
      }
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Coalesce(IEnumerable<MagickImage^>^ images)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();
      std::vector<Magick::Image>* coalescedImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return gcnew Collection<MagickImage^>();

        Magick::coalesceImages(coalescedImages, magickImages->begin(), magickImages->end());

        return Copy(coalescedImages);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return gcnew Collection<MagickImage^>();
      }
      finally
      {
        delete magickImages;
        delete coalescedImages;
      }
    }

    MagickImage^ MagickImageCollection::Combine(IEnumerable<MagickImage^>^ images, Channels channels)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return nullptr;

        Magick::Image combinedImage;
        Magick::combineImages(&combinedImage, magickImages->begin(), magickImages->end(),
          (Magick::ChannelType)channels, magickImages->begin()->colorSpaceType());

        return gcnew MagickImage(combinedImage);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return nullptr;
      }
      finally
      {
        delete magickImages;
      }
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Deconstruct(IEnumerable<MagickImage^>^ images)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();
      std::vector<Magick::Image>* deconstructedImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return gcnew Collection<MagickImage^>();

        Magick::deconstructImages(deconstructedImages, magickImages->begin(), magickImages->end());

        return Copy(deconstructedImages);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return gcnew Collection<MagickImage^>();
      }
      finally
      {
        delete magickImages;
        delete deconstructedImages;
      }
    }

    MagickImage^ MagickImageCollection::Evaluate(IEnumerable<MagickImage^>^ images, EvaluateOperator evaluateOperator)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return nullptr;

        Magick::Image evaluatedImage;
        Magick::evaluateImages(&evaluatedImage, magickImages->begin(), magickImages->end(),
          (Magick::MagickEvaluateOperator)evaluateOperator);

        return gcnew MagickImage(evaluatedImage);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return nullptr;
      }
      finally
      {
        delete magickImages;
      }
    }

    MagickImage^ MagickImageCollection::Flatten(IEnumerable<MagickImage^>^ images)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return nullptr;

        Magick::Image flattendImage;
        Magick::flattenImages(&flattendImage, magickImages->begin(), magickImages->end());

        return gcnew MagickImage(flattendImage);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return nullptr;
      }
      finally
      {
        delete magickImages;
      }
    }

    MagickImage^ MagickImageCollection::Fx(IEnumerable<MagickImage^>^ images, String^ expression)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return nullptr;

        std::string fxExpression;
        Marshaller::Marshal(expression, fxExpression);

        Magick::Image fxImage;
        Magick::fxImages(&fxImage, magickImages->begin(), magickImages->end(), fxExpression);

        return gcnew MagickImage(fxImage);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return nullptr;
      }
      finally
      {
        delete magickImages;
      }
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Map(IEnumerable<MagickImage^>^ images, QuantizeSettings^ settings)
    {
      Throw::IfNull("settings", settings);

      for each(MagickImage^ image in images)
      {
        image->Apply(settings);
      }

      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return gcnew Collection<MagickImage^>();

        Magick::Image mapImage;
        Magick::mapImages(magickImages->begin(), magickImages->end(), mapImage, settings->DitherMethod.HasValue, settings->MeasureErrors);

        return Copy(magickImages);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return gcnew Collection<MagickImage^>();
      }
      finally
      {
        delete magickImages;
      }
    }

    MagickImage^ MagickImageCollection::Merge(IEnumerable<MagickImage^>^ images)
    {
      Magick::Image mergedImage;
      if (!Merge(images, &mergedImage, LayerMethod::Merge))
        return nullptr;

      return gcnew MagickImage(mergedImage);
    }

    MagickImage^ MagickImageCollection::Montage(IEnumerable<MagickImage^>^ images, MontageSettings^ settings)
    {
      Throw::IfNull("settings", settings);

      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return nullptr;

        Magick::MontageFramed options;
        settings->Apply(&options);
        Magick::montageImages(magickImages, magickImages->begin(), magickImages->end(), options);
        return Merge(Copy(magickImages));
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return nullptr;
      }
      finally
      {
        delete magickImages;
      }
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Morph(IEnumerable<MagickImage^>^ images, int frames)
    {
      Throw::IfTrue("frames", frames < 1, "Frames must be at least 1.");

      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();
      std::vector<Magick::Image>* morphedImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return gcnew Collection<MagickImage^>();

        Magick::morphImages(morphedImages, magickImages->begin(), magickImages->end(), frames);

        return Copy(morphedImages);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return gcnew Collection<MagickImage^>();
      }
      finally
      {
        delete magickImages;
      }
    }

    MagickImage^ MagickImageCollection::Mosaic(IEnumerable<MagickImage^>^ images)
    {
      Magick::Image mosaicImage;
      if (!Merge(images, &mosaicImage, LayerMethod::Mosaic))
        return nullptr;

      return gcnew MagickImage(mosaicImage);
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Optimize(IEnumerable<MagickImage^>^ images)
    {
      return Optimize(images, LayerMethod::OptimizeImage);
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::OptimizePlus(IEnumerable<MagickImage^>^ images)
    {
      return Optimize(images, LayerMethod::OptimizePlus);
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::OptimizeTransparency(IEnumerable<MagickImage^>^ images)
    {
      return Optimize(images, LayerMethod::OptimizeTrans);
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Read(array<Byte>^ data, MagickReadSettings^ readSettings)
    {
      std::vector<Magick::Image>* images = new std::vector<Magick::Image>();
      try
      {
        HandleException(MagickReader::Read(images, data, CheckSettings(readSettings)));
        return Copy(images);
      }
      finally
      {
        delete images;
      }
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Read(Stream^ stream, MagickReadSettings^ readSettings)
    {
      std::vector<Magick::Image>* images = new std::vector<Magick::Image>();
      try
      {
        HandleException(MagickReader::Read(images, stream, CheckSettings(readSettings)));
        return Copy(images);
      }
      finally
      {
        delete images;
      }
    }

    IEnumerable<MagickImage^>^ MagickImageCollection::Read(String^ fileName, MagickReadSettings^ readSettings)
    {
      std::vector<Magick::Image>* images = new std::vector<Magick::Image>();
      try
      {
        HandleException(MagickReader::Read(images, fileName, CheckSettings(readSettings)));
        return Copy(images);
      }
      finally
      {
        delete images;
      }
    }

    MagickImage^ MagickImageCollection::Smush(IEnumerable<MagickImage^>^ images, int offset, bool vertically)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();

      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return nullptr;

        Magick::Image smushedImage;
        Magick::smushImages(&smushedImage, magickImages->begin(), magickImages->end(), offset, vertically);

        return gcnew MagickImage(smushedImage);
      }
      catch (Magick::Exception& exception)
      {
        HandleException(exception);
        return nullptr;
      }
      finally
      {
        delete magickImages;
      }
    }

    array<Byte>^ MagickImageCollection::ToByteArray(IEnumerable<MagickImage^>^ images)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();
      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return nullptr;

        Magick::Blob blob;
        HandleException(MagickWriter::Write(magickImages, &blob));
        return Marshaller::Marshal(&blob);
      }
      finally
      {
        delete magickImages;
      }
    }

    MagickImage^ MagickImageCollection::TrimBounds(IEnumerable<MagickImage^>^ images)
    {
      Magick::Image trimBoundsImage;
      if (!Merge(images, &trimBoundsImage, LayerMethod::Trimbounds))
        return nullptr;

      return gcnew MagickImage(trimBoundsImage);
    }

    void MagickImageCollection::Write(IEnumerable<MagickImage^>^ images, Stream^ stream)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();
      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return;

        HandleException(MagickWriter::Write(magickImages, stream));
      }
      finally
      {
        delete magickImages;
      }
    }

    void MagickImageCollection::Write(IEnumerable<MagickImage^>^ images, String^ fileName)
    {
      std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();
      try
      {
        Copy(images, magickImages);
        if (magickImages->size() == 0)
          return;

        HandleException(MagickWriter::Write(magickImages, fileName));
      }
      finally
      {
        delete magickImages;
      }
    }
  }
}