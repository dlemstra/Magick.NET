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
#include "MagickImage.h"
#include "Drawables\Drawables.h"
#include "Helpers\ExceptionHelper.h"
#include "Helpers\EnumHelper.h"
#include "Helpers\MatrixHelper.h"
#include "IO\MagickReader.h"
#include "IO\MagickReaderSettings.h"
#include "IO\MagickWriter.h"
#include "MagickImageCollection.h"

using namespace System::Globalization;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		MagickImage::!MagickImage()
		{
			DisposeValue();
		}
		//===========================================================================================
		Magick::Image* MagickImage::Value::get()
		{
			if (_Value == NULL)
				throw gcnew ObjectDisposedException(GetType()->ToString());

			return _Value;
		}
		//===========================================================================================
		MagickReaderSettings^ MagickImage::CheckSettings(MagickReadSettings^ readSettings)
		{
			MagickReadSettings^ newReadSettings = readSettings;
			if (newReadSettings == nullptr)
				newReadSettings = gcnew MagickReadSettings();

			newReadSettings->FrameCount = 1;

			MagickReaderSettings^ settings = gcnew MagickReaderSettings(newReadSettings);
			settings->IgnoreWarnings = (_WarningEvent == nullptr);

			return settings;
		}
		//===========================================================================================
		MagickErrorInfo^ MagickImage::CreateErrorInfo()
		{
			return gcnew MagickErrorInfo(Value->meanErrorPerPixel(), Value->normalizedMeanError(), Value->normalizedMaxError());
		}
		//===========================================================================================
		Magick::Image* MagickImage::CreateImage()
		{
			Magick::Image* image = new Magick::Image();
			image->quiet(true);
			return image;
		}
		//===========================================================================================
		void MagickImage::DisposeValue()
		{
			if (_Value == NULL)
				return;

			delete _Value;
			_Value = NULL;
		}
		//===========================================================================================
		void MagickImage::HandleException(const Magick::Exception& exception)
		{
			HandleException(ExceptionHelper::Create(exception));
		}
		//===========================================================================================
		void MagickImage::HandleException(MagickException^ exception)
		{
			if (exception == nullptr)
				return;

			MagickWarningException^ warning = dynamic_cast<MagickWarningException^>(exception);
			if (warning == nullptr)
				throw exception;

			if (_WarningEvent != nullptr)
				_WarningEvent->Invoke(this, gcnew WarningEventArgs(warning));
		}
		//===========================================================================================
		void MagickImage::ReplaceValue(const Magick::Image& value)
		{
			DisposeValue();
			_Value = new Magick::Image(value);
		}
		//===========================================================================================
		void MagickImage::SetProfile(String^ name, Magick::Blob& blob)
		{
			Throw::IfNullOrEmpty("name", name);

			try
			{
				std::string profileName;
				Marshaller::Marshal(name, profileName);

				Value->profile(profileName, blob);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		MagickImage::MagickImage(const Magick::Image& image)
		{
			_Value = new Magick::Image(image);
		}
		//===========================================================================================
		void MagickImage::Apply(QuantizeSettings^ settings)
		{
			Value->quantizeColors(settings->Colors);
			Value->quantizeColorSpace((Magick::ColorspaceType)settings->ColorSpace);
			if (settings->DitherMethod.HasValue && settings->DitherMethod.Value != DitherMethod::No)
			{
				Value->quantizeDither(true);
				Value->quantizeDitherMethod((Magick::DitherMethod)settings->DitherMethod.Value);
			}
			else
				Value->quantizeDither(false);

			Value->quantizeTreeDepth(settings->TreeDepth);
		}
		//===========================================================================================
		const Magick::Image& MagickImage::ReuseValue()
		{
			return *Value;
		}
		//===========================================================================================
		MagickImage::~MagickImage()
		{
			this->!MagickImage();
		}
		//===========================================================================================
		MagickImage::MagickImage()
		{
			_Value = CreateImage();
		}
		//===========================================================================================
		MagickImage::MagickImage(MagickImage^ image)
		{
			Throw::IfNull("image", image);

			_Value = new Magick::Image(*image->Value);
		}
		//===========================================================================================
		void MagickImage::Warning::add(EventHandler<WarningEventArgs^>^ handler)
		{
			Value->quiet(false);
			_WarningEvent += handler;
		}
		//===========================================================================================
		void MagickImage::Warning::remove(EventHandler<WarningEventArgs^>^ handler)
		{
			_WarningEvent -= handler;
			if (_WarningEvent == nullptr)
				Value->quiet(true);
		}
		//===========================================================================================
		bool MagickImage::Adjoin::get()
		{
			return Value->adjoin();
		}
		//===========================================================================================
		void MagickImage::Adjoin::set(bool value)
		{
			Value->adjoin(value);
		}
		//===========================================================================================
		MagickColor^ MagickImage::AlphaColor::get()
		{
			return gcnew MagickColor(Value->alphaColor());
		}
		//===========================================================================================
		void MagickImage::AlphaColor::set(MagickColor^ value)
		{
			const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
			Value->alphaColor(*color);
			delete color;
		}
		//===========================================================================================
		int MagickImage::AnimationDelay::get()
		{
			return Convert::ToInt32(Value->animationDelay());
		}
		//===========================================================================================
		void MagickImage::AnimationDelay::set(int value)
		{
			Value->animationDelay(value);
		}
		//===========================================================================================
		int MagickImage::AnimationIterations::get()
		{
			return Convert::ToInt32(Value->animationIterations());
		}
		//===========================================================================================
		void MagickImage::AnimationIterations::set(int value)
		{
			Value->animationIterations(value);
		}
		//===========================================================================================
		bool MagickImage::AntiAlias::get()
		{
			return Value->antiAlias();
		}
		//===========================================================================================
		void MagickImage::AntiAlias::set(bool value)
		{
			Value->antiAlias(value);
		}
		//===========================================================================================
		MagickColor^ MagickImage::BackgroundColor::get()
		{
			return gcnew MagickColor(Value->backgroundColor());
		}
		//===========================================================================================
		void MagickImage::BackgroundColor::set(MagickColor^ value)
		{
			const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
			Value->backgroundColor(*color);
			delete color;
		}
		//===========================================================================================
		int MagickImage::BaseHeight::get()
		{
			return Convert::ToInt32(Value->baseRows());
		}
		//===========================================================================================
		int MagickImage::BaseWidth::get()
		{
			return Convert::ToInt32(Value->baseColumns());
		}
		//===========================================================================================
		bool MagickImage::BlackPointCompensation::get()
		{
			return Value->blackPointCompensation();
		}
		//===========================================================================================
		void MagickImage::BlackPointCompensation::set(bool value)
		{
			Value->blackPointCompensation(value);
		}
		//===========================================================================================
		MagickGeometry^ MagickImage::BoundingBox::get()
		{
			try
			{
				return gcnew MagickGeometry(Value->boundingBox());
			}
			catch(Magick::WarningOption)
			{
				return nullptr;
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		MagickColor^ MagickImage::BorderColor::get()
		{
			return gcnew MagickColor(Value->borderColor());
		}
		//===========================================================================================
		void MagickImage::BorderColor::set(MagickColor^ value)
		{
			const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
			Value->borderColor(*color);
			delete color;
		}
		//===========================================================================================
		MagickColor^ MagickImage::BoxColor::get()
		{
			return gcnew MagickColor(Value->boxColor());
		}
		//===========================================================================================
		void MagickImage::BoxColor::set(MagickColor^ value)
		{
			const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
			Value->boxColor(*color);
			delete color;
		}
		IEnumerable<PixelChannel>^ MagickImage::Channels::get()
		{
			List<PixelChannel>^ channels = gcnew List<PixelChannel>();

			if (Value->hasChannel((MagickCore::PixelChannel)PixelChannel::Red))
				channels->Add(PixelChannel::Red);
			if (Value->hasChannel((MagickCore::PixelChannel)PixelChannel::Green))
				channels->Add(PixelChannel::Green);
			if (Value->hasChannel((MagickCore::PixelChannel)PixelChannel::Blue))
				channels->Add(PixelChannel::Blue);
			if (Value->hasChannel((MagickCore::PixelChannel)PixelChannel::Black))
				channels->Add(PixelChannel::Black);
			if (Value->hasChannel((MagickCore::PixelChannel)PixelChannel::Alpha))
				channels->Add(PixelChannel::Alpha);

			return channels;
		}
		//===========================================================================================
		ClassType MagickImage::ClassType::get()
		{
			return (ImageMagick::ClassType)Value->classType();
		}
		//===========================================================================================
		void MagickImage::ClassType::set(ImageMagick::ClassType value)
		{
			return Value->classType((Magick::ClassType)value);
		}
		//===========================================================================================
		Magick::Quantum MagickImage::ColorFuzz::get()
		{
			return (Magick::Quantum)(Value->colorFuzz());
		}
		//===========================================================================================
		void MagickImage::ColorFuzz::set(Magick::Quantum value)
		{
			Value->colorFuzz(value);
		}
		//===========================================================================================
		int MagickImage::ColorMapSize::get()
		{
			int colorMapSize = -1;
			try
			{
				colorMapSize = Convert::ToInt32(Value->colorMapSize());
			}
			catch(Magick::ErrorOption)
			{
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}

			return colorMapSize;
		}
		//===========================================================================================
		void MagickImage::ColorMapSize::set(int value)
		{
			Value->colorMapSize(value);
		}
		//===========================================================================================
		ColorSpace MagickImage::ColorSpace::get()
		{
			return (ImageMagick::ColorSpace)Value->colorSpace();
		}
		//===========================================================================================
		void MagickImage::ColorSpace::set(ImageMagick::ColorSpace value)
		{
			try
			{
				Value->colorSpace((Magick::ColorspaceType)value);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		ColorType MagickImage::ColorType::get()
		{
			return (ImageMagick::ColorType)Value->type();
		}
		//===========================================================================================
		void MagickImage::ColorType::set(ImageMagick::ColorType value)
		{
			Value->type((Magick::ImageType)value);
		}
		//===========================================================================================
		String^ MagickImage::Comment::get()
		{
			return Marshaller::Marshal(Value->comment());
		}
		//===========================================================================================
		void MagickImage::Comment::set(String^ value)
		{
			std::string comment; 
			Value->comment(Marshaller::Marshal(value, comment));
		}
		//===========================================================================================
		CompositeOperator MagickImage::Compose::get()
		{
			return (CompositeOperator)Value->compose();
		}
		//===========================================================================================
		void MagickImage::Compose::set(CompositeOperator value)
		{
			Value->compose((Magick::CompositeOperator)value);
		}
		//===========================================================================================
		CompressionMethod MagickImage::CompressionMethod::get()
		{
			return (ImageMagick::CompressionMethod)Value->compressType();
		}
		//===========================================================================================
		void MagickImage::CompressionMethod::set(ImageMagick::CompressionMethod value)
		{
			Value->compressType((Magick::CompressionType)value);
		}
		//===========================================================================================
		bool MagickImage::Debug::get()
		{
			return Value->debug();
		}
		//===========================================================================================
		void MagickImage::Debug::set(bool value)
		{
			Value->debug(value);
		} 
		//===========================================================================================
		PointD MagickImage::Density::get()
		{
			Magick::Point density = Value->density();
			return PointD(density.x(), density.y());
		}
		//===========================================================================================
		void MagickImage::Density::set(PointD value)
		{
			Magick::Point density = Magick::Point(value.X, value.Y);
			Value->density(density);

			if (ResolutionUnits == Resolution::Undefined)
				ResolutionUnits = Resolution::PixelsPerInch;
		}
		//===========================================================================================
		int MagickImage::Depth::get()
		{
			return Convert::ToInt32(Value->depth());
		}
		//===========================================================================================
		void MagickImage::Depth::set(int value)
		{
			Value->depth(value);
		}
		//==============================================================================================
		MagickGeometry^ MagickImage::EncodingGeometry::get()
		{
			try
			{
				return gcnew MagickGeometry(Value->geometry());
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
		//===========================================================================================
		Endian MagickImage::Endian::get()
		{
			return (ImageMagick::Endian)Value->endian();
		}
		//===========================================================================================
		void MagickImage::Endian::set(ImageMagick::Endian value)
		{
			Value->endian((Magick::EndianType)value);
		}
		//===========================================================================================
		String^ MagickImage::FileName::get()
		{
			return Marshaller::Marshal(Value->baseFilename());
		}
		//===========================================================================================
		long MagickImage::FileSize::get()
		{
			return (long)Value->fileSize();
		}
		//===========================================================================================
		MagickColor^ MagickImage::FillColor::get()
		{
			return gcnew MagickColor(Value->fillColor());
		}
		//===========================================================================================
		void MagickImage::FillColor::set(MagickColor^ value)
		{
			const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
			Value->fillColor(*color);
			delete color;
		}
		//===========================================================================================
		MagickImage^ MagickImage::FillPattern::get()
		{
			Magick::Image fillPattern = Value->fillPattern();
			if (!fillPattern.isValid())
				return nullptr;

			return gcnew MagickImage(fillPattern);
		}
		//===========================================================================================
		void MagickImage::FillPattern::set(MagickImage^ value)
		{
			if (value == nullptr)
			{
				Magick::Image* image = CreateImage();
				Value->fillPattern(*image);
				delete image;
			}
			else
			{
				Value->fillPattern(*value->Value);
			}
		}
		//===========================================================================================
		FillRule MagickImage::FillRule::get()
		{
			return (ImageMagick::FillRule)Value->fillRule();
		}
		//===========================================================================================
		void MagickImage::FillRule::set(ImageMagick::FillRule value)
		{
			Value->fillRule((Magick::FillRule)value);
		}
		//===========================================================================================
		FilterType MagickImage::FilterType::get()
		{
			return (ImageMagick::FilterType)Value->filterType();
		}
		//===========================================================================================
		void MagickImage::FilterType::set(ImageMagick::FilterType value)
		{
			Value->filterType((Magick::FilterTypes)value);
		}
		//===========================================================================================
		String^ MagickImage::FlashPixView::get()
		{
			return Marshaller::Marshal(Value->view());
		}
		//===========================================================================================
		void MagickImage::FlashPixView::set(String^ value)
		{
			std::string view;
			Value->view(Marshaller::Marshal(value, view));
		}
		//===========================================================================================
		String^ MagickImage::Font::get()
		{
			return Marshaller::Marshal(Value->font());
		}
		//===========================================================================================
		void MagickImage::Font::set(String^ value)
		{
			std::string font;
			Value->font(Marshaller::Marshal(value, font));
		}
		//===========================================================================================
		double MagickImage::FontPointsize::get()
		{
			return Value->fontPointsize();
		}
		//===========================================================================================
		void MagickImage::FontPointsize::set(double value)
		{
			Value->fontPointsize(value);
		}
		//===========================================================================================
		MagickFormat MagickImage::Format::get()
		{
			return EnumHelper::Parse<MagickFormat>(Marshaller::Marshal(Value->magick()), MagickFormat::Unknown);
		}
		//===========================================================================================
		void MagickImage::Format::set(MagickFormat value)
		{
			if (value == MagickFormat::Unknown)
				return;

			std::string name;
			Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, value), name);

			Value->magick(name);
		}
		//===========================================================================================
		double MagickImage::Gamma::get()
		{
			return Value->gamma();
		}
		//===========================================================================================
		GifDisposeMethod MagickImage::GifDisposeMethod::get()
		{
			return (ImageMagick::GifDisposeMethod)Value->gifDisposeMethod();
		}
		//===========================================================================================
		void MagickImage::GifDisposeMethod::set(ImageMagick::GifDisposeMethod value)
		{
			Value->gifDisposeMethod((Magick::DisposeType)value);
		}
		//===========================================================================================
		bool MagickImage::HasAlpha::get()
		{
			return Value->alpha();
		}
		//===========================================================================================
		void MagickImage::HasAlpha::set(bool value)
		{
			Value->alpha(value);
		}
		//===========================================================================================
		int MagickImage::Height::get()
		{
			return Convert::ToInt32(Value->size().height());
		}
		//===========================================================================================
		Interlace MagickImage::Interlace::get()
		{
			return (ImageMagick::Interlace)Value->interlaceType();
		}
		//===========================================================================================
		void MagickImage::Interlace::set(ImageMagick::Interlace value)
		{
			Value->interlaceType((Magick::InterlaceType)value);
		}
		//===========================================================================================
		PixelInterpolateMethod MagickImage::Interpolate::get()
		{
			return (PixelInterpolateMethod)Value->interpolate();
		}
		//===========================================================================================
		void MagickImage::Interpolate::set(PixelInterpolateMethod value)
		{
			Value->interpolate((Magick::PixelInterpolateMethod)value);
		}
		//===========================================================================================
		bool MagickImage::IsOpaque::get()
		{
			return Value->isOpaque();
		}
		//===========================================================================================
		String^ MagickImage::Label::get()
		{
			std::string label = Value->label();
			if (label.length() == 0)
				return nullptr;

			return Marshaller::Marshal(label);
		}
		//===========================================================================================
		void MagickImage::Label::set(String^ value)
		{
			if (value == nullptr)
				value = "";

			std::string label;
			Marshaller::Marshal(value, label);
			Value->label(label);
		}
		//===========================================================================================
		MagickImage^ MagickImage::Mask::get()
		{
			Magick::Image mask = Value->mask();
			if (!mask.isValid())
				return nullptr;

			return gcnew MagickImage(mask);
		}
		//===========================================================================================
		void MagickImage::Mask::set(MagickImage^ value)
		{
			if (value == nullptr)
			{
				Magick::Image* image = CreateImage();
				Value->mask(*image);
				delete image;
			}
			else
			{
				Value->mask(*value->Value);
			}
		}
		//===========================================================================================
		OrientationType MagickImage::Orientation::get()
		{
			return (OrientationType)Value->orientation();
		}
		//===========================================================================================
		void MagickImage::Orientation::set(OrientationType value)
		{
			Value->orientation((Magick::OrientationType)value);
		}
		//===========================================================================================
		MagickGeometry^ MagickImage::Page::get()
		{
			return gcnew MagickGeometry(Value->page());
		}
		//===========================================================================================
		void MagickImage::Page::set(MagickGeometry^ value)
		{
			Throw::IfNull("value", value);

			const Magick::Geometry* geometry = value->CreateGeometry();
			Value->page(*geometry);
			delete geometry;
		}
		//===========================================================================================
		IEnumerable<String^>^ MagickImage::ProfileNames::get()
		{
			List<String^>^ names = gcnew List<String^>();

			std::vector<std::string> *profileNames = new std::vector<std::string>();

			try
			{
				Magick::profileNames(profileNames, Value);
				for (std::vector<std::string>::iterator iter = profileNames->begin(), end = profileNames->end(); iter != end; ++iter)
				{
					names->Add(Marshaller::Marshal(*iter));
				}

				return names;
			}
			finally
			{
				delete profileNames;
			}
		}
		//===========================================================================================
		int MagickImage::Quality::get()
		{
			return Convert::ToInt32(Value->quality());
		}
		//===========================================================================================
		void MagickImage::Quality::set(int value)
		{
			int quality = value < 1 ? 1 : value;
			quality = quality > 100 ? 100 : quality;

			Value->quality(quality);
		}
		//===========================================================================================
		RenderingIntent MagickImage::RenderingIntent::get()
		{
			return (ImageMagick::RenderingIntent)Value->renderingIntent();
		}
		//===========================================================================================
		void MagickImage::RenderingIntent::set(ImageMagick::RenderingIntent value)
		{
			return Value->renderingIntent((Magick::RenderingIntent)value);
		}
		//===========================================================================================
		Resolution MagickImage::ResolutionUnits::get()
		{
			return (Resolution)Value->resolutionUnits();
		}
		//===========================================================================================
		void MagickImage::ResolutionUnits::set(Resolution value)
		{
			return Value->resolutionUnits((Magick::ResolutionType)value);
		}
		//===========================================================================================
		double MagickImage::ResolutionX::get()
		{
			return Value->xResolution();
		}
		//===========================================================================================
		double MagickImage::ResolutionY::get()
		{
			return Value->yResolution();
		}
		//===========================================================================================
		String^ MagickImage::Signature::get()
		{
			try
			{
				return Marshaller::Marshal(Value->signature(true));
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return "";
			}
		}
		//===========================================================================================
		bool MagickImage::StrokeAntiAlias::get()
		{
			return Value->strokeAntiAlias();
		}
		//===========================================================================================
		void MagickImage::StrokeAntiAlias::set(bool value)
		{
			Value->strokeAntiAlias(value);
		}
		//===========================================================================================
		MagickColor^ MagickImage::StrokeColor::get()
		{
			return gcnew MagickColor(Value->strokeColor());
		}
		//===========================================================================================
		void MagickImage::StrokeColor::set(MagickColor^ value)
		{
			const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
			Value->strokeColor(*color);
			delete color;
		}
		//===========================================================================================
		array<double>^ MagickImage::StrokeDashArray::get()
		{
			const double* strokeDashArray = Value->strokeDashArray();
			if (strokeDashArray == NULL)
				return nullptr;

			return Marshaller::Marshal(strokeDashArray);
		}
		//===========================================================================================
		void MagickImage::StrokeDashArray::set(array<double>^ value)
		{
			double* strokeDashArray = Marshaller::MarshalAndTerminate(value);
			Value->strokeDashArray(strokeDashArray);
			delete[] strokeDashArray;
		}
		//===========================================================================================
		double MagickImage::StrokeDashOffset::get()
		{
			return Value->strokeDashOffset();
		}
		//===========================================================================================
		void MagickImage::StrokeDashOffset::set(double value)
		{
			Value->strokeDashOffset(value);
		}
		//===========================================================================================
		LineCap MagickImage::StrokeLineCap::get()
		{
			return (LineCap)Value->strokeLineCap();
		}
		//===========================================================================================
		void MagickImage::StrokeLineCap::set(LineCap value)
		{
			Value->strokeLineCap((Magick::LineCap)value);
		}
		//===========================================================================================
		LineJoin MagickImage::StrokeLineJoin::get()
		{
			return (LineJoin)Value->strokeLineJoin();
		}
		//===========================================================================================
		void MagickImage::StrokeLineJoin::set(LineJoin value)
		{
			Value->strokeLineJoin((Magick::LineJoin)value);
		}
		//===========================================================================================
		int MagickImage::StrokeMiterLimit::get()
		{
			return Convert::ToInt32(Value->strokeMiterLimit());
		}
		//===========================================================================================
		void MagickImage::StrokeMiterLimit::set(int value)
		{
			Value->strokeMiterLimit(value);
		}
		//===========================================================================================
		MagickImage^ MagickImage::StrokePattern::get()
		{
			Magick::Image strokePattern = Value->strokePattern();
			if (!strokePattern.isValid())
				return nullptr;

			return gcnew MagickImage(strokePattern);
		}
		//===========================================================================================
		void MagickImage::StrokePattern::set(MagickImage^ value)
		{
			if (value == nullptr)
				Value->strokePattern(Magick::Image());
			else
				Value->strokePattern(*value->Value);
		}
		//===========================================================================================
		double MagickImage::StrokeWidth::get()
		{
			return Value->strokeWidth();
		}
		//===========================================================================================
		void MagickImage::StrokeWidth::set(double value)
		{
			Value->strokeWidth(value);
		}
		//===========================================================================================
		TextDirection MagickImage::TextDirection::get()
		{
			return (ImageMagick::TextDirection)Value->textDirection();
		}
		//===========================================================================================
		void MagickImage::TextDirection::set(ImageMagick::TextDirection value)
		{
			Value->textDirection((Magick::DirectionType)value);
		}
		//===========================================================================================
		String^ MagickImage::TextEncoding::get()
		{
			return Marshaller::Marshal(Value->textEncoding());
		}
		//===========================================================================================
		void MagickImage::TextEncoding::set(String^ value)
		{
			std::string encoding;
			Value->textEncoding(Marshaller::Marshal(value, encoding));
		}
		//===========================================================================================
		Gravity MagickImage::TextGravity::get()
		{
			return (ImageMagick::Gravity)Value->textGravity();
		}
		//===========================================================================================
		void MagickImage::TextGravity::set(Gravity value)
		{
			Value->textGravity((Magick::GravityType)value);
		}
		//===========================================================================================
		double MagickImage::TextInterlineSpacing::get()
		{
			return Value->textInterlineSpacing();
		}
		//===========================================================================================
		void MagickImage::TextInterlineSpacing::set(double value)
		{
			Value->textInterlineSpacing(value);
		}
		//===========================================================================================
		double MagickImage::TextInterwordSpacing::get()
		{
			return Value->textInterwordSpacing();
		}
		//===========================================================================================
		void MagickImage::TextInterwordSpacing::set(double value)
		{
			Value->textInterwordSpacing(value);
		}
		//===========================================================================================
		double MagickImage::TextKerning::get()
		{
			return Value->textKerning();
		}
		//===========================================================================================
		void MagickImage::TextKerning::set(double value)
		{
			Value->textKerning(value);
		}
		//===========================================================================================
		int MagickImage::TotalColors::get()
		{
			return Convert::ToInt32(Value->totalColors());
		}
		//===========================================================================================
		bool MagickImage::Verbose::get() 
		{
			return Value->verbose();
		}
		//===========================================================================================
		void MagickImage::Verbose::set(bool verbose) 
		{
			return Value->verbose(verbose);
		}
		//===========================================================================================
		VirtualPixelMethod MagickImage::VirtualPixelMethod::get()
		{
			return (ImageMagick::VirtualPixelMethod)Value->virtualPixelMethod();
		}
		//===========================================================================================
		void MagickImage::VirtualPixelMethod::set(ImageMagick::VirtualPixelMethod value)
		{
			Value->virtualPixelMethod((Magick::VirtualPixelMethod)value);
		}
		//===========================================================================================
		int MagickImage::Width::get()
		{
			return Convert::ToInt32(Value->size().width());
		}
		//===========================================================================================
		void MagickImage::AdaptiveBlur(double radius, double sigma)
		{
			try
			{
				Value->adaptiveBlur(radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AdaptiveResize(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->adaptiveResize(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::AdaptiveSharpen(double radius, double sigma)
		{
			try
			{
				Value->adaptiveSharpen(radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AdaptiveSharpen(double radius, double sigma, ImageMagick::Channels channels)
		{
			try
			{
				Value->adaptiveSharpenChannel((Magick::ChannelType)channels, radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AdaptiveThreshold(int width, int height, Magick::Quantum bias)
		{
			try
			{
				Value->adaptiveThreshold(width, height, bias);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AddNoise(NoiseType noiseType)
		{
			try
			{
				Value->addNoise((Magick::NoiseType)noiseType);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AddNoise(NoiseType noiseType, ImageMagick::Channels channels)
		{
			try
			{
				Value->addNoiseChannel((Magick::ChannelType)channels, (Magick::NoiseType)noiseType);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AddProfile(String^ name, array<Byte>^ profile)
		{
			try
			{
				Magick::Blob blob;
				Marshaller::Marshal(profile, &blob);

				std::string profileName;
				Marshaller::Marshal(name, profileName);

				Value->profile(profileName, blob);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AffineTransform(IDrawableAffine^ drawableAffine)
		{
			Magick::DrawableAffine* affine = (Magick::DrawableAffine* ) NULL;

			try
			{
				affine = Drawables::CreateDrawableAffine(drawableAffine);
				Value->affineTransform(*affine);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				if (affine != (Magick::DrawableAffine* ) NULL)
					delete affine;
			}
		}//==============================================================================================
		//===========================================================================================
		void MagickImage::Alpha(AlphaOption option)
		{
			try
			{
				Value->alphaChannel((Magick::AlphaChannelOption)option);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Annotate(String^ text, Gravity gravity)
		{
			std::string annotateText;
			Marshaller::Marshal(text, annotateText);

			try
			{
				Value->annotate(annotateText, (Magick::GravityType)gravity);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Annotate(String^ text, MagickGeometry^ boundingArea, Gravity gravity, double degrees)
		{
			std::string annotateText;
			Marshaller::Marshal(text, annotateText);
			const Magick::Geometry* geometry = boundingArea->CreateGeometry();

			try
			{
				Value->annotate(annotateText, *geometry, (Magick::GravityType)gravity, degrees);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete geometry;
			}
		}
		//===========================================================================================
		void MagickImage::AutoGamma()
		{
			try
			{
				Value->autoGamma();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AutoGamma(ImageMagick::Channels channels)
		{
			try
			{
				Value->autoGammaChannel((Magick::ChannelType)channels);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AutoLevel()
		{
			try
			{
				Value->autoLevel();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AutoLevel(ImageMagick::Channels channels)
		{
			try
			{
				Value->autoLevelChannel((Magick::ChannelType)channels);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::AutoOrient()
		{
			try
			{
				Value->autoOrient();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		int MagickImage::BitDepth(ImageMagick::Channels channels)
		{
			try
			{
				return Convert::ToInt32(Value->channelDepth((Magick::ChannelType)channels));
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return -1;
			}
		}
		//===========================================================================================
		void MagickImage::BitDepth(ImageMagick::Channels channels, int value)
		{
			try
			{
				Value->channelDepth((Magick::ChannelType)channels, value);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::BlackThreshold(String^ threshold)
		{
			try
			{
				std::string magickThreshold;
				Value->blackThreshold(Marshaller::Marshal(threshold, magickThreshold));
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::BlackThreshold(String^ threshold, ImageMagick::Channels channels)
		{

			try
			{
				std::string magickThreshold;
				Value->blackThresholdChannel((Magick::ChannelType)channels,
					Marshaller::Marshal(threshold, magickThreshold));
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::BlueShift(double factor)
		{
			try
			{
				Value->blueShift(factor);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Blur(double radius, double sigma)
		{
			try
			{
				Value->blur(radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Blur(double radius, double sigma, ImageMagick::Channels channels)
		{
			try
			{
				Value->blurChannel((Magick::ChannelType)channels, radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Border(int width, int height)
		{
			const Magick::Geometry* geometry = new Magick::Geometry(width, height);

			try
			{
				Value->border(*geometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete geometry;
			}
		}
		//===========================================================================================
		void MagickImage::BrightnessContrast(double brightness, double contrast)
		{
			try
			{
				Value->brightnessContrast(brightness, contrast);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::BrightnessContrast(double brightness, double contrast, ImageMagick::Channels channels)
		{
			try
			{
				Value->brightnessContrastChannel((Magick::ChannelType)channels, brightness, contrast);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::CannyEdge(double radius, double sigma, double lower, double upper)
		{
			try
			{
				Value->cannyEdge(radius, sigma, lower, upper);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::CDL(String^ fileName)
		{
			Throw::IfInvalidFileName(fileName);

			String^ cdlData = File::ReadAllText(fileName);

			std::string cdl;
			Marshaller::Marshal(cdlData, cdl);

			try
			{
				Value->cdl(cdl);
			}
			catch (Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::ChangeColorSpace(ImageMagick::ColorSpace value)
		{
			try
			{
				Value->colorSpaceType((Magick::ColorspaceType)value);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Charcoal(double radius, double sigma)
		{
			try
			{
				Value->charcoal(radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Chop(MagickGeometry^ geometry)
		{

			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->chop(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::ChromaBluePrimary(double x, double y)
		{
			try
			{
				Value->chromaBluePrimary(x, y);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::ChromaGreenPrimary(double x, double y)
		{
			try
			{
				Value->chromaGreenPrimary(x, y);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::ChromaRedPrimary(double x, double y)
		{
			try
			{
				Value->chromaRedPrimary(x, y);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::ChromaWhitePoint(double x, double y)
		{
			try
			{
				Value->chromaWhitePoint(x, y);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Clamp()
		{
			try
			{
				Value->clamp();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Clamp(ImageMagick::Channels channels)
		{
			try
			{
				Value->clampChannel((Magick::ChannelType)channels);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Clip()
		{
			try
			{
				Value->clip();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Clip(String^ pathName, bool inside)
		{
			try
			{
				std::string name;
				Marshaller::Marshal(pathName, name);
				Value->clipPath(name, inside);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		MagickImage^ MagickImage::Clone()
		{
			return gcnew MagickImage(*Value);
		}//==============================================================================================
		//===========================================================================================
		void MagickImage::Clut(MagickImage^ image, PixelInterpolateMethod method)
		{
			try
			{
				Value->clut(*image->Value,(Magick::PixelInterpolateMethod)method);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Clut(MagickImage^ image, PixelInterpolateMethod method, ImageMagick::Channels channels)
		{
			try
			{
				Value->clutChannel((Magick::ChannelType)channels, *image->Value,(Magick::PixelInterpolateMethod)method);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::ColorAlpha(MagickColor^ color)
		{
			if (!HasAlpha)
				return;

			MagickImage^ canvas = gcnew MagickImage();
			canvas->Read(color, Width, Height);
			canvas->Composite(this, 0,0, CompositeOperator::SrcOver);
			ReplaceValue(canvas->ReuseValue());

			delete canvas;
		}
		//===========================================================================================
		void MagickImage::Colorize(MagickColor^ color, int alphaRed, int alphaGreen, int alphaBlue)
		{
			const Magick::Color* magickColor = color->CreateColor();

			try
			{
				Value->colorize(alphaRed, alphaGreen, alphaBlue, *magickColor);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickColor;
			}
		}
		//===========================================================================================
		MagickColor^ MagickImage::ColorMap(int index)
		{
			try
			{
				return gcnew MagickColor(Value->colorMap(index));
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
		//===========================================================================================
		void MagickImage::ColorMap(int index, MagickColor^ color)
		{
			try
			{
				const Magick::Color* colorMap = color->CreateColor();
				Value->colorMap(index, *colorMap);
				delete colorMap;
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::ColorMatrix(ImageMagick::ColorMatrix^ matrix)
		{
			double* colorMatrix = MatrixHelper::CreateArray(matrix);

			try
			{
				Value->colorMatrix(matrix->Order, colorMatrix);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete[] colorMatrix;
			}
		}
		//===========================================================================================
		MagickErrorInfo^ MagickImage::Compare(MagickImage^ image)
		{
			Throw::IfNull("image", image);

			try
			{
				if (Value->compare(*(image->Value)))
					return gcnew MagickErrorInfo();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}

			return CreateErrorInfo();
		}
		//===========================================================================================
		double MagickImage::Compare(MagickImage^ image, ErrorMetric metric, ImageMagick::Channels channels)
		{
			try
			{
				return Value->compareChannel((Magick::ChannelType)channels, *image->Value, (Magick::MetricType)metric);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return -1;
			}
		}
		//===========================================================================================
		double MagickImage::Compare(MagickImage^ image, ErrorMetric metric, MagickImage^ difference, ImageMagick::Channels channels)
		{
			try
			{
				double distortion = 0.0;
				Magick::Image result = Value->compareChannel((Magick::ChannelType)channels, *image->Value, (Magick::MetricType)metric, &distortion);
				if (result.isValid())
					difference->ReplaceValue(result);
				return distortion;
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return -1;
			}
		}
		//===========================================================================================
		void MagickImage::Composite(MagickImage^ image, int x, int y, CompositeOperator compose)
		{
			try
			{
				Value->composite(*(image->Value), x, y, (Magick::CompositeOperator)compose);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Composite(MagickImage^ image, MagickGeometry^ offset, CompositeOperator compose)
		{
			const Magick::Geometry* magickGeometry = offset->CreateGeometry();

			try
			{
				Value->composite(*(image->Value), *magickGeometry, (Magick::CompositeOperator)compose);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Composite(MagickImage^ image, Gravity gravity, CompositeOperator compose)
		{
			try
			{
				Value->composite(*(image->Value), (Magick::GravityType)gravity, (Magick::CompositeOperator)compose);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void  MagickImage::ConnectedComponents(int connectivity)
		{
			Throw::IfFalse("connectivity", connectivity == 4 || connectivity == 8, "Invalid connectivity, only 4 and 8 are supported.");

			try
			{
				Value->connectedComponents(connectivity);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Contrast(bool enhance)
		{
			try
			{
				Value->contrast(enhance);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::ContrastStretch(PointD contrast)
		{
			try
			{
				Value->contrastStretch(contrast.X, contrast.Y);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::ContrastStretch(PointD contrast, ImageMagick::Channels channels)
		{
			try
			{
				Value->contrastStretchChannel((Magick::ChannelType)channels, contrast.X, contrast.Y);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Convolve(ConvolveMatrix^ convolveMatrix)
		{
			double* kernel = MatrixHelper::CreateArray(convolveMatrix);

			try
			{
				Value->convolve(convolveMatrix->Order, kernel);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete[] kernel;
			}
		}
		//===========================================================================================
		void MagickImage::Crop(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->crop(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		IEnumerable<MagickImage^>^ MagickImage::CropToTiles(MagickGeometry^ geometry)
		{
			std::vector<Magick::Image> *images = new std::vector<Magick::Image>();
			const Magick::Geometry *cropGeometry = geometry->CreateGeometry();

			try
			{
				cropToTiles(images, *Value, *cropGeometry);

				return MagickImageCollection::Copy(images);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
			finally
			{
				delete images;
				delete cropGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::CycleColormap(int amount)
		{
			try
			{
				Value->cycleColormap(amount);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Decipher(String^ passphrase)
		{
			try
			{
				std::string passphrase_;
				Marshaller::Marshal(passphrase, passphrase_);
				Value->decipher(passphrase_);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Deskew(Magick::Quantum threshold)
		{
			try
			{
				Value->deskew(threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Despeckle()
		{
			try
			{
				Value->despeckle();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		ColorType MagickImage::DetermineColorType()
		{
			try
			{
				return (ImageMagick::ColorType)Value->determineType();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}

			return ImageMagick::ColorType::Undefined;
		}
		//===========================================================================================
		void MagickImage::Distort(DistortMethod method, bool bestfit, array<double>^ arguments)
		{
			double* distortArguments = Marshaller::Marshal(arguments);

			try
			{
				Value->distort((Magick::DistortImageMethod)method, arguments->Length, distortArguments, bestfit);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete[] distortArguments;
			}
		}
		//===========================================================================================
		void MagickImage::Draw(IEnumerable<IDrawable^>^ drawables)
		{
			try
			{
				Magick::DrawableList drawList;
				IEnumerator<IDrawable^>^ enumerator = drawables->GetEnumerator();
				while(enumerator->MoveNext())
				{
					for each (Drawable drawable in Drawables::CreateDrawables(enumerator->Current))
					{
						drawList.push_back(*drawable.Value);
					}
				}

				Value->draw(drawList);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Edge(double radius)
		{
			try
			{
				Value->edge(radius);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Emboss(double radius, double sigma)
		{
			try
			{
				Value->emboss(radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Encipher(String^ passphrase)
		{
			try
			{
				std::string passphrase_;
				Marshaller::Marshal(passphrase, passphrase_);
				Value->encipher(passphrase_);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Enhance()
		{
			try
			{
				Value->enhance();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Equalize()
		{
			try
			{
				Value->equalize();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		bool MagickImage::Equals(MagickImage^ other)
		{
			if (ReferenceEquals(other, nullptr))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			return (Magick::operator == (*Value, *other->Value)) ? true : false;
		}
		//===========================================================================================
		void MagickImage::Evaluate(ImageMagick::Channels channels, EvaluateOperator evaluateOperator, double value)
		{
			try
			{
				Value->quantumOperator((Magick::ChannelType)channels,
					(Magick::MagickEvaluateOperator)evaluateOperator, value);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Evaluate(ImageMagick::Channels channels, MagickGeometry^ geometry,
			EvaluateOperator evaluateOperator, double value)
		{
			try
			{
				Value->quantumOperator(geometry->X, geometry->Y, geometry->Width, geometry->Height,
					(Magick::ChannelType)channels, (Magick::MagickEvaluateOperator)evaluateOperator,
					value);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Extent(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->extent(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Extent(MagickGeometry^ geometry, MagickColor^ backgroundColor)
		{
			const Magick::Color* color = backgroundColor->CreateColor();
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->extent(*magickGeometry, *color);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete color;
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Extent(MagickGeometry^ geometry, Gravity gravity)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->extent(*magickGeometry, (Magick::GravityType)gravity);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Extent(MagickGeometry^ geometry, Gravity gravity, MagickColor^ backgroundColor)
		{
			const Magick::Color* color = backgroundColor->CreateColor();
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->extent(*magickGeometry, *color, (Magick::GravityType)gravity);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete color;
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Flip()
		{
			try
			{
				Value->flip();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::FloodFill(int alpha, int x, int y, bool invert)
		{
			try
			{
				Value->floodFillAlpha(x, y, alpha, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::FloodFill(MagickColor^ color, int x, int y, bool invert)
		{
			const Magick::Color* fillColor = color->CreateColor();

			try
			{
				Value->floodFillColor(x, y, *fillColor, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete fillColor;
			}
		}
		//===========================================================================================
		void MagickImage::FloodFill(MagickColor^ color, int x, int y, MagickColor^ borderColor, bool invert)
		{
			const Magick::Color* fillColor = color->CreateColor();
			const Magick::Color* fillBorderColor = borderColor->CreateColor();

			try
			{
				Value->floodFillColor(x, y, *fillColor, *fillBorderColor, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete fillColor;
				delete fillBorderColor;
			}
		}
		//===========================================================================================
		void MagickImage::FloodFill(MagickColor^ color, MagickGeometry^ geometry, bool invert)
		{
			const Magick::Color* fillColor = color->CreateColor();
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->floodFillColor(*magickGeometry, *fillColor, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete fillColor;
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::FloodFill(MagickColor^ color, MagickGeometry^ geometry, MagickColor^ borderColor, bool invert)
		{
			const Magick::Color* fillColor = color->CreateColor();
			const Magick::Color* fillBorderColor = borderColor->CreateColor();
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->floodFillColor(*magickGeometry, *fillColor, *fillBorderColor, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete fillColor;
				delete fillBorderColor;
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::FloodFill(MagickImage^ image, int x, int y, bool invert)
		{
			try
			{
				Value->floodFillTexture(x, y, *image->Value, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::FloodFill(MagickImage^ image, int x, int y, MagickColor^ borderColor, bool invert)
		{
			const Magick::Color* fillBorderColor = borderColor->CreateColor();

			try
			{
				Value->floodFillTexture(x, y, *image->Value, *fillBorderColor, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete fillBorderColor;
			}
		}
		//===========================================================================================
		void MagickImage::FloodFill(MagickImage^ image, MagickGeometry^ geometry, bool invert)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->floodFillTexture(*magickGeometry, *image->Value, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::FloodFill(MagickImage^ image, MagickGeometry^ geometry, MagickColor^ borderColor, bool invert)
		{
			const Magick::Color* fillBorderColor = borderColor->CreateColor();
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->floodFillTexture(*magickGeometry, *image->Value, *fillBorderColor, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete fillBorderColor;
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Flop()
		{
			try
			{
				Value->flop();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		TypeMetric^ MagickImage::FontTypeMetrics(String^ text, bool ignoreNewLines)
		{
			Magick::TypeMetric* metric = new Magick::TypeMetric();

			try
			{
				std::string fontText;
				Marshaller::Marshal(text, fontText);

				if (ignoreNewLines)
					Value->fontTypeMetrics(fontText, metric);
				else
					Value->fontTypeMetricsMultiline(fontText, metric);

				TypeMetric^ result = gcnew TypeMetric();
				result->Ascent = metric->ascent();
				result->Descent = metric->descent();
				result->MaxHorizontalAdvance = metric->maxHorizontalAdvance();
				result->TextHeight = metric->textHeight();
				result->TextWidth = metric->textWidth();
				result->UnderlinePosition = metric->underlinePosition();
				result->UnderlineThickness = metric->underlineThickness();

				return result;
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
			finally
			{
				delete metric;
			}
		}
		//===========================================================================================
		String^ MagickImage::FormatExpression(String^ expression)
		{
			try
			{
				std::string magickExpression;
				Marshaller::Marshal(expression, magickExpression);
				std::string result = Value->formatExpression(magickExpression);
				return Marshaller::Marshal(result);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
		//===========================================================================================
		void MagickImage::Frame(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->frame(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Fx(String^ expression)
		{
			try
			{
				std::string fxExpression;
				Marshaller::Marshal(expression, fxExpression);
				Value->fx(fxExpression);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Fx(String^ expression, ImageMagick::Channels channels)
		{
			Throw::IfNullOrEmpty("expression", expression);

			try
			{
				std::string fxExpression;
				Marshaller::Marshal(expression, fxExpression);
				Value->fx(fxExpression, (Magick::ChannelType)channels);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::GammaCorrect(double gamma)
		{
			try
			{
				Value->gamma(gamma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::GammaCorrect(double gammaRed, double gammaGreen, double gammaBlue)
		{
			try
			{
				Value->gamma(gammaRed, gammaGreen, gammaBlue);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::GaussianBlur(double width, double sigma)
		{
			try
			{
				Value->gaussianBlur(width, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::GaussianBlur(double width, double sigma, ImageMagick::Channels channels)
		{
			try
			{
				Value->gaussianBlurChannel((Magick::ChannelType)channels, width, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		String^ MagickImage::GetArtifact(String^ name)
		{
			std::string artifactName;
			Marshaller::Marshal(name, artifactName);

			try
			{
				return Marshaller::Marshal(Value->artifact(artifactName));
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
		//===========================================================================================
		String^ MagickImage::GetAttribute(String^ name)
		{
			Throw::IfNullOrEmpty("name", name);

			std::string attributeName;
			Marshaller::Marshal(name, attributeName);

			try
			{
				return Marshaller::Marshal(Value->attribute(attributeName));
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
		//===========================================================================================
		String^ MagickImage::GetDefine(String^ format, String^ name)
		{
			std::string magick;
			Marshaller::Marshal(format, magick);
			std::string optionName;
			Marshaller::Marshal(name, optionName);

			try
			{
				return Marshaller::Marshal(Value->defineValue(magick, optionName));
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
		//===========================================================================================
		array<Byte>^ MagickImage::GetProfile(String^ name)
		{
			Throw::IfNullOrEmpty("name", name);

			try
			{
				std::string profileName;
				Marshaller::Marshal(name, profileName);
				Magick::Blob blob = Value->profile(profileName);
				return Marshaller::Marshal(&blob);
			}
			catch(Magick::ErrorCoder)
			{
				return nullptr;
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		PixelCollection^ MagickImage::GetReadOnlyPixels(int x, int y, int width, int height)
		{
			return gcnew PixelCollection(Value, x, y, width, height);
		}
		//===========================================================================================
		WritablePixelCollection^ MagickImage::GetWritablePixels(int x, int y, int width, int height)
		{
			return gcnew WritablePixelCollection(Value, x, y, width, height);
		}
		//===========================================================================================
		void MagickImage::Grayscale(PixelIntensityMethod method)
		{
			try
			{
				Value->grayscale((MagickCore::PixelIntensityMethod)method);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::HaldClut(MagickImage^ image)
		{
			try
			{
				Value->haldClut(*image->Value);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		IEnumerable<Tuple<MagickColor^, int>^>^ MagickImage::Histogram()
		{
			std::list<std::pair<const Magick::Color,size_t>> *colors = new std::list<std::pair<const Magick::Color,size_t>>();

			try
			{
				colorHistogram(colors, *Value);

				Collection<Tuple<MagickColor^, int>^>^ result = gcnew Collection<Tuple<MagickColor^, int>^>();
				for (std::list<std::pair<const Magick::Color,size_t>>::iterator iter = colors->begin(), end = colors->end(); iter != end; ++iter)
				{
					result->Add(gcnew Tuple<MagickColor^, int>(gcnew MagickColor(iter->first), (int)iter->second));
				}

				return result;
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return gcnew Collection<Tuple<MagickColor^, int>^>();
			}
			finally
			{
				delete colors;
			}
		}
		//===========================================================================================
		void MagickImage::HoughLine(int width, int height, int threshold)
		{
			try
			{
				Value->houghLine(width, height, threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Implode(double factor)
		{
			try
			{
				Value->implode(factor);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::InverseFourierTransform(MagickImage^ image, bool magnitude)
		{
			try
			{
				Value->inverseFourierTransform(*image->Value, magnitude);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}//==============================================================================================
		void MagickImage::Kuwahara(double radius, double sigma)
		{
			try
			{
				Value->kuwahara(radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, double midpoint)
		{
			try
			{
				Value->level(blackPoint, whitePoint, midpoint);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, double midpoint, ImageMagick::Channels channels)
		{
			try
			{
				Value->levelChannel((Magick::ChannelType)channels, blackPoint, whitePoint, midpoint);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, bool invert)
		{
			const Magick::Color* black = blackColor->CreateColor();
			const Magick::Color* white = whiteColor->CreateColor();

			try
			{
				Value->levelColors(*black, *white, !invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete black;
				delete white;
			}
		}
		//===========================================================================================
		void MagickImage::LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, ImageMagick::Channels channels, bool invert)
		{
			const Magick::Color* black = blackColor->CreateColor();
			const Magick::Color* white = whiteColor->CreateColor();

			try
			{
				Value->levelColorsChannel((Magick::ChannelType)channels, *black, *white, !invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete black;
				delete white;
			}
		}
		//===========================================================================================
		void MagickImage::LinearStretch(Magick::Quantum blackPoint, Magick::Quantum whitePoint)
		{
			try
			{
				Value->linearStretch(blackPoint, whitePoint);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::LiquidRescale(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->liquidRescale(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Magnify()
		{
			try
			{
				Value->magnify();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		MagickErrorInfo^ MagickImage::Map(MagickImage^ image, QuantizeSettings^ settings)
		{
			try
			{
				Apply(settings);
				bool dither = settings->DitherMethod.HasValue && settings->DitherMethod.Value != DitherMethod::No;
				Value->map(*image->Value, dither);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}

			if (settings->MeasureErrors)
				return CreateErrorInfo();

			return nullptr;
		}
		//===========================================================================================
		void MagickImage::MedianFilter(double radius)
		{
			try
			{
				Value->medianFilter(radius);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Minify()
		{
			try
			{
				Value->minify();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Modulate(double brightness, double saturation, double hue)
		{
			try
			{
				Value->modulate(brightness, saturation, hue);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		Moments^ MagickImage::Moments()
		{
			try
			{
				Magick::ImageMoments moments = Value->moments();

				Dictionary<PixelChannel, ChannelMoments^>^ channels = gcnew Dictionary<PixelChannel, ChannelMoments^>();
				Array^ values = Enum::GetValues(PixelChannel::typeid);
				for (int i = 0; i < values->Length; i++)
				{
					PixelChannel channel = (PixelChannel)values->GetValue(i);
					if (channels->ContainsKey(channel))
						continue;

					Magick::ChannelMoments magickMoments = moments.channel((Magick::PixelChannel)channel);
					if (!magickMoments.isValid())
						continue;

					array<double>^ huInvariants = gcnew array<double>(8);
					for (int h=0; h < 8; h++)
					{
						huInvariants[h] = magickMoments.huInvariants(h);
					}

					ChannelMoments^ channelMoments = gcnew ChannelMoments((PixelChannel)magickMoments.channel(),
						PointD(magickMoments.centroidX(), magickMoments.centroidY()),
						PointD(magickMoments.ellipseAxisX(), magickMoments.ellipseAxisY()), magickMoments.ellipseAngle(),
						magickMoments.ellipseEccentricity(), magickMoments.ellipseIntensity(), huInvariants);

					channels->Add(channel,channelMoments);
				}

				return gcnew ImageMagick::Moments(channels);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
		//===========================================================================================
		void MagickImage::Morphology(MorphologyMethod method, Kernel kernel, String^ arguments)
		{
			try
			{
				std::string args;
				Marshaller::Marshal(arguments, args);
				Value->morphology((Magick::MorphologyMethod)method, (Magick::KernelInfoType)kernel, args);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Morphology(MorphologyMethod method, Kernel kernel, String^ arguments, ImageMagick::Channels channels)
		{
			try
			{
				std::string args;
				Marshaller::Marshal(arguments, args);
				Value->morphologyChannel((Magick::ChannelType)channels, (Magick::MorphologyMethod)method,
					(Magick::KernelInfoType)kernel, args);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Morphology(MorphologyMethod method, Kernel kernel, String^ arguments, ImageMagick::Channels channels, int iterations)
		{
			try
			{
				std::string args;
				Marshaller::Marshal(arguments, args);
				Value->morphologyChannel((Magick::ChannelType)channels, (Magick::MorphologyMethod)method,
					(Magick::KernelInfoType)kernel, args, iterations);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Morphology(MorphologyMethod method, Kernel kernel, String^ arguments, int iterations)
		{
			try
			{
				std::string args;
				Marshaller::Marshal(arguments, args);
				Value->morphology((Magick::MorphologyMethod)method, (Magick::KernelInfoType)kernel, args, iterations);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Morphology(MorphologyMethod method, String^ userKernel)
		{
			try
			{
				std::string magickKernel;
				Marshaller::Marshal(userKernel, magickKernel);
				Value->morphology((Magick::MorphologyMethod)method, magickKernel);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Morphology(MorphologyMethod method, String^ userKernel, ImageMagick::Channels channels)
		{
			try
			{
				std::string magickKernel;
				Marshaller::Marshal(userKernel, magickKernel);
				Value->morphologyChannel((Magick::ChannelType)channels, (Magick::MorphologyMethod)method, magickKernel);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Morphology(MorphologyMethod method, String^ userKernel, ImageMagick::Channels channels, int iterations)
		{
			try
			{
				std::string magickKernel;
				Marshaller::Marshal(userKernel, magickKernel);
				Value->morphologyChannel((Magick::ChannelType)channels, (Magick::MorphologyMethod)method, magickKernel,
					iterations);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Morphology(MorphologyMethod method, String^ userKernel, int iterations)
		{
			try
			{
				std::string magickKernel;
				Marshaller::Marshal(userKernel, magickKernel);
				Value->morphology((Magick::MorphologyMethod)method, magickKernel, iterations);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::MotionBlur(double radius, double sigma, double angle)
		{
			try
			{
				Value->motionBlur(radius, sigma, angle);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Negate(bool onlyGrayscale)
		{
			try
			{
				Value->negate(onlyGrayscale);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Negate(ImageMagick::Channels channels, bool onlyGrayscale)
		{
			try
			{
				Value->negateChannel((Magick::ChannelType)channels, onlyGrayscale);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Normalize()
		{
			try
			{
				Value->normalize();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::OilPaint(double radius)
		{
			try
			{
				Value->oilPaint(radius);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Opaque(MagickColor^ target, MagickColor^ fill, bool invert)
		{
			const Magick::Color* opaque = target->CreateColor();
			const Magick::Color* pen = fill->CreateColor();

			try
			{
				Value->opaque(*opaque, *pen, invert);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete opaque;
				delete pen;
			}
		}
		//===========================================================================================
		void MagickImage::OrderedDither(String^ thresholdMap)
		{
			try
			{
				std::string threshold;
				Marshaller::Marshal(thresholdMap, threshold);
				Value->orderedDither(threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::OrderedDither(String^ thresholdMap, ImageMagick::Channels channels)
		{
			try
			{
				std::string threshold;
				Marshaller::Marshal(thresholdMap, threshold);
				Value->orderedDitherChannel((Magick::ChannelType)channels, threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Perceptible(double epsilon)
		{
			try
			{
				Value->perceptible(epsilon);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Perceptible(double epsilon, ImageMagick::Channels channels)
		{
			try
			{
				Value->perceptibleChannel((Magick::ChannelType)channels, epsilon);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		IEnumerable<ChannelPerceptualHash^>^ MagickImage::PerceptualHash()
		{
			Collection<ChannelPerceptualHash^>^ channels = gcnew Collection<ChannelPerceptualHash^>();

			try
			{
				Magick::ImagePerceptualHash perceptualHash = Value->perceptualHash();
				if (perceptualHash.isValid())
				{
					channels->Add(gcnew ChannelPerceptualHash(perceptualHash.channel(Magick::RedPixelChannel)));
					channels->Add(gcnew ChannelPerceptualHash(perceptualHash.channel(Magick::GreenPixelChannel)));
					channels->Add(gcnew ChannelPerceptualHash(perceptualHash.channel(Magick::BluePixelChannel)));
				}
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}

			return channels;
		}
		//===========================================================================================
		void MagickImage::Ping(array<Byte>^ data)
		{
			MagickReaderSettings^ settings = gcnew MagickReaderSettings();
			settings->Ping = true;
			HandleException(MagickReader::Read(Value, data, settings));
		}
		//===========================================================================================
		void MagickImage::Ping(String^ fileName)
		{
			MagickReaderSettings^ settings = gcnew MagickReaderSettings();
			settings->Ping = true;
			HandleException(MagickReader::Read(Value, fileName, settings));
		}
		//===========================================================================================
		void MagickImage::Ping(Stream^ stream)
		{
			MagickReaderSettings^ settings = gcnew MagickReaderSettings();
			settings->Ping = true;
			HandleException(MagickReader::Read(Value, stream, settings));
		}
		//===========================================================================================
		void MagickImage::Polaroid(String^ caption, double angle, PixelInterpolateMethod method)
		{
			try
			{
				std::string caption_;
				Marshaller::Marshal(caption, caption_);

				Value->polaroid(caption_, angle, (Magick::PixelInterpolateMethod) method);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Posterize(int levels, DitherMethod method)
		{
			try
			{
				Value->posterize(levels, (Magick::DitherMethod)method);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Posterize(int levels, DitherMethod method, ImageMagick::Channels channels)
		{
			try
			{
				Value->posterizeChannel((Magick::ChannelType)channels, levels, (Magick::DitherMethod)method);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		MagickErrorInfo^ MagickImage::Quantize(QuantizeSettings^ settings)
		{
			try
			{
				Apply(settings);
				Value->quantize(settings->MeasureErrors);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}

			if (settings->MeasureErrors)
				return CreateErrorInfo();

			return nullptr;
		}
		//===========================================================================================
		void MagickImage::RaiseOrLower(int size, bool raiseFlag)
		{
			const Magick::Geometry* geometry = new Magick::Geometry(size, size);

			try
			{
				Value->raise(*geometry, raiseFlag);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete geometry;
			}
		}
		//===========================================================================================
		void MagickImage::RandomThreshold(Magick::Quantum low, Magick::Quantum high, bool isPercentage)
		{
			Magick::Geometry* geometry = new Magick::Geometry((size_t)low, (size_t)high);
			geometry->percent(isPercentage);

			try
			{
				Value->randomThreshold(*geometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete geometry;
			}
		}
		//===========================================================================================
		void MagickImage::RandomThreshold(Magick::Quantum low, Magick::Quantum high, ImageMagick::Channels channels, bool isPercentage)
		{
			Magick::Geometry* geometry = new Magick::Geometry((size_t)low, (size_t)high);
			geometry->percent(isPercentage);

			try
			{
				Value->randomThresholdChannel((Magick::ChannelType)channels, *geometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete geometry;
			}
		}
		//===========================================================================================
		void MagickImage::Read(array<Byte>^ data, MagickReadSettings^ readSettings)
		{
			HandleException(MagickReader::Read(Value, data, CheckSettings(readSettings)));
		}
		//===========================================================================================
		void MagickImage::Read(MagickColor^ color, int width, int height)
		{
			HandleException(MagickReader::Read(Value, color, width, height));
		}
		//===========================================================================================
		void MagickImage::Read(String^ fileName, int width, int height)
		{
			HandleException(MagickReader::Read(Value, fileName, width, height));
		}
		//===========================================================================================
		void MagickImage::Read(String^ fileName, MagickReadSettings^ readSettings)
		{
			HandleException(MagickReader::Read(Value, fileName, CheckSettings(readSettings)));
		}
		//===========================================================================================
		void MagickImage::Read(Stream^ stream, MagickReadSettings^ readSettings)
		{
			HandleException(MagickReader::Read(Value, stream, CheckSettings(readSettings)));
		}
		//==============================================================================================
		void MagickImage::ReduceNoise(int order)
		{
			try
			{
				Value->reduceNoise(order);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//==============================================================================================
		void MagickImage::RemoveDefine(String^ format, String^ name)
		{
			std::string magick;
			Marshaller::Marshal(format, magick);
			std::string optionName;
			Marshaller::Marshal(name, optionName);

			try
			{
				Value->defineSet(magick, optionName, false);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::RemoveProfile(String^ name)
		{
			Magick::Blob blob;
			SetProfile(name, blob);
		}
		//===========================================================================================
		void MagickImage::RePage()
		{
			try
			{
				Value->repage();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Resample(PointD density)
		{
			try
			{
				Magick::Point magickDensity = Magick::Point(density.X, density.Y);
				Value->resample(magickDensity);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Resize(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->resize(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Roll(int xOffset, int yOffset)
		{
			try
			{
				Value->roll(xOffset, yOffset);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Rotate(double degrees)
		{
			try
			{
				Value->rotate(degrees);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::RotationalBlur(double angle)
		{
			try
			{
				Value->rotationalBlur(angle);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::RotationalBlur(double angle, ImageMagick::Channels channels)
		{
			try
			{
				Value->rotationalBlurChannel((Magick::ChannelType)channels, angle);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Sample(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->sample(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Scale(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->scale(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Segment(ImageMagick::ColorSpace quantizeColorSpace, double clusterThreshold, double smoothingThreshold)
		{
			try
			{
				Value->quantizeColorSpace((Magick::ColorspaceType)quantizeColorSpace);
				Value->segment(clusterThreshold, smoothingThreshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::SelectiveBlur(double radius, double sigma, double threshold)
		{
			try
			{
				Value->selectiveBlur(radius, sigma, threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::SelectiveBlur(double radius, double sigma, double threshold, ImageMagick::Channels channels)
		{
			try
			{
				Value->selectiveBlurChannel((Magick::ChannelType)channels, radius, sigma, threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		IEnumerable<MagickImage^>^ MagickImage::Separate(ImageMagick::Channels channels)
		{
			std::vector<Magick::Image> *images = new std::vector<Magick::Image>();

			try
			{
				separateImages(images, *Value, (Magick::ChannelType)channels);

				return MagickImageCollection::Copy(images);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
			finally
			{
				delete images;
			}
		}
		//===========================================================================================
		void MagickImage::SepiaTone(Magick::Quantum threshold)
		{
			try
			{
				return Value->sepiaTone(threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::SetArtifact(String^ name, String^ value)
		{
			std::string artifactName;
			Marshaller::Marshal(name, artifactName);
			std::string artifactValue;
			Marshaller::Marshal(value, artifactValue);

			try
			{
				return Value->artifact(artifactName, artifactValue);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::SetAttenuate(double attenuate)
		{
			return Value->attenuate(attenuate);
		}
		//===========================================================================================
		void MagickImage::SetAttribute(String^ name, String^ value)
		{
			std::string attributeName;
			Marshaller::Marshal(name, attributeName);
			std::string attributeValue;
			Marshaller::Marshal(value, attributeValue);

			try
			{
				return Value->attribute(attributeName, attributeValue);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::SetDefine(String^ format, String^ name, String^ value)
		{
			std::string magick;
			Marshaller::Marshal(format, magick);
			std::string optionName;
			Marshaller::Marshal(name, optionName);
			std::string optionValue;
			Marshaller::Marshal(value, optionValue);

			try
			{
				Value->defineValue(magick, optionName, optionValue);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::SetOption(String^ name, String^ value)
		{
			try
			{
				std::string option;
				Marshaller::Marshal(name, option);

				std::string optionValue;
				Marshaller::Marshal(value, optionValue);

				MagickCore::SetImageOption(Value->imageInfo(), option.c_str(), optionValue.c_str());
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//==============================================================================================
		void MagickImage::SetHighlightColor(MagickColor^ color)
		{
			const Magick::Color* highlightColor = color->CreateColor();
			try
			{
				Value->highlightColor(*highlightColor);
			}
			finally
			{
				delete highlightColor;
			}
		}
		//==============================================================================================
		void MagickImage::SetLowlightColor(MagickColor^ color)
		{\
		const Magick::Color* lowlightColor = color->CreateColor();
		try
		{
			Value->lowlightColor(*lowlightColor);
		}
		finally
		{
			delete lowlightColor;
		}
		}
		//==============================================================================================
		void MagickImage::Shade(double azimuth, double elevation, bool colorShading)
		{
			try
			{
				Value->shade(azimuth, elevation, colorShading);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		///=============================================================================================
		void MagickImage::Shadow(int x, int y, double sigma, double alpha)
		{
			try
			{
				Value->shadow(alpha, sigma, x, y);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		///=============================================================================================
		void MagickImage::Shadow(int x, int y, double sigma, double alpha, MagickColor^ color)
		{
			std::vector<Magick::Image>* magickImages = new std::vector<Magick::Image>();
			const Magick::Color* backgroundColor = color->CreateColor();

			try
			{
				MagickImage^ shadow = Clone();
				shadow->Value->backgroundColor(*backgroundColor);
				shadow->Value->shadow(alpha, sigma, x, y);
				shadow->Value->backgroundColor(Value->backgroundColor());

				magickImages->push_back(shadow->ReuseValue());
				magickImages->push_back(this->ReuseValue());

				Magick::mergeImageLayers(Value, magickImages->begin(), magickImages->end(), Magick::MosaicLayer);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickImages;
				delete backgroundColor;
			}
		}
		//==============================================================================================
		void MagickImage::Sharpen(double radius, double sigma)
		{
			try
			{
				Value->sharpen(radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//==============================================================================================
		void MagickImage::Sharpen(double radius, double sigma, ImageMagick::Channels channels)
		{
			try
			{
				Value->sharpenChannel((Magick::ChannelType)channels, radius, sigma);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//==============================================================================================
		void MagickImage::Shave(int leftRight, int topBottom)
		{
			const Magick::Geometry* geometry = new Magick::Geometry(leftRight, topBottom);

			try
			{
				Value->shave(*geometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete geometry;
			}
		}
		//==============================================================================================
		void MagickImage::Shear(double xAngle, double yAngle)
		{
			try
			{
				Value->shear(xAngle, yAngle);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//==============================================================================================
		void MagickImage::SigmoidalContrast(bool sharpen, double contrast, double midpoint)
		{
			try
			{
				Value->sigmoidalContrast(sharpen ? 1 : 0, contrast, midpoint);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//==============================================================================================
		void MagickImage::Sketch(double radius, double sigma, double angle)
		{
			try
			{
				Value->sketch(radius, sigma, angle);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//==============================================================================================
		void MagickImage::Solarize(double factor)
		{
			try
			{
				Value->solarize(factor);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::SparseColor(ImageMagick::Channels channels, SparseColorMethod method, IEnumerable<Internal::ISparseColorArg^>^ args)
		{
			bool hasRed = ((int)channels & (int)ImageMagick::Channels::Red) != 0;
			bool hasGreen = ((int)channels & (int)ImageMagick::Channels::Green) != 0;
			bool hasBlue = ((int)channels & (int)ImageMagick::Channels::Blue) != 0;
			bool hasAlpha = ((int)channels & (int)ImageMagick::Channels::Alpha) != 0;

			Throw::IfTrue("channels", !hasRed && !hasGreen && !hasBlue && !hasAlpha, "Invalid channels specified.");

			List<double>^ argsList = gcnew List<double>();

			IEnumerator<Internal::ISparseColorArg^>^ enumerator = args->GetEnumerator();
			while(enumerator->MoveNext())
			{
				Internal::ISparseColorArg^ arg = enumerator->Current;
				MagickColor^ color = (MagickColor^)arg->Color;

				argsList->Add(arg->X);
				argsList->Add(arg->Y);
				if (hasRed)
					argsList->Add(Quantum::Scale(color->R));
				if (hasGreen)
					argsList->Add(Quantum::Scale(color->G));
				if (hasBlue)
					argsList->Add(Quantum::Scale(color->B));
				if (hasAlpha)
					argsList->Add(Quantum::Scale(color->A));
			}

			Throw::IfTrue("args", argsList->Count == 0, "Value cannot be empty");

			double* arguments = Marshaller::Marshal(argsList->ToArray());

			try
			{
				Magick::ChannelType magickChannels = (Magick::ChannelType)channels;
				magickChannels = (Magick::ChannelType) (magickChannels & ~Magick::IndexChannel);
				Value->sparseColor(magickChannels, (Magick::SparseColorMethod)method,
					argsList->Count, arguments);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete[] arguments;
			}
		}
		//==============================================================================================
		void MagickImage::Splice(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->splice(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Spread(int amount)
		{
			try
			{
				Value->spread(amount);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		Statistics^ MagickImage::Statistics()
		{
			try
			{
				Magick::ImageStatistics statistics = Value->statistics();

				Dictionary<PixelChannel, ChannelStatistics^>^ channels = gcnew Dictionary<PixelChannel, ChannelStatistics^>();
				for each (PixelChannel channel in Enum::GetValues(PixelChannel::typeid))
				{
					if (channels->ContainsKey(channel))
						continue;

					Magick::ChannelStatistics magickStatistics = statistics.channel((Magick::PixelChannel)channel);
					if (!magickStatistics.isValid())
						continue;

					ChannelStatistics^ channelStatistics = gcnew ChannelStatistics(channel, (int)magickStatistics.depth(),
						magickStatistics.entropy(), magickStatistics.kurtosis(), magickStatistics.maxima(),
						magickStatistics.mean(), magickStatistics.minima(), magickStatistics.skewness(),
						magickStatistics.standardDeviation(), magickStatistics.sum(), magickStatistics.sumCubed(),
						magickStatistics.sumFourthPower(), magickStatistics.sumSquared(), magickStatistics.variance());

					channels->Add(channel, channelStatistics);
				}

				return gcnew ImageMagick::Statistics(channels);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
		//===========================================================================================
		void MagickImage::Stegano(MagickImage^ watermark)
		{
			try
			{
				Value->stegano(*watermark->Value);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Stereo(MagickImage^ rightImage)
		{
			try
			{
				Value->stereo(*rightImage->Value);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Strip()
		{
			try
			{
				Value->strip();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		MagickSearchResult^ MagickImage::SubImageSearch(MagickImage^ image, ErrorMetric metric, double similarityThreshold)
		{
			try
			{
				Magick::Geometry offset;
				double similarityMetric = 0.0;

				Magick::Image result = Value->subImageSearch(*image->Value, (Magick::MetricType)metric, &offset, &similarityMetric, similarityThreshold);

				if (result.isValid())
					return gcnew MagickSearchResult(result,offset,similarityMetric);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}

			return nullptr;
		}
		//===========================================================================================
		void MagickImage::Swirl(double degrees)
		{
			try
			{
				Value->swirl(degrees);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Texture(MagickImage^ image)
		{
			try
			{
				Value->texture(*image->Value);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Threshold(Magick::Quantum threshold)
		{
			try
			{
				Value->threshold(threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Thumbnail(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->thumbnail(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
		void MagickImage::Tint(String^ opacity)
		{
			try
			{
				std::string magickOpacity;
				Marshaller::Marshal(opacity, magickOpacity);
				Value->tint(magickOpacity);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		Bitmap^ MagickImage::ToBitmap()
		{
			if (ColorSpace == ImageMagick::ColorSpace::CMYK)
				ColorSpace = ImageMagick::ColorSpace::sRGB;

			std::string map = "BGR";
			StorageType type = StorageType::Char;
			PixelFormat format = PixelFormat::Format24bppRgb;
			if (HasAlpha)
			{
				map = "BGRA";
				format = PixelFormat::Format32bppArgb;
			}

			try
			{
				Magick::PixelData pixelData(*Value, map, (MagickCore::StorageType)type);
				if (pixelData.length() == 0)
					return nullptr;

				Bitmap^ bitmap = gcnew Bitmap(Width, Height, format);
				BitmapData^ data = bitmap->LockBits(Rectangle(0, 0, Width, Height), ImageLockMode::ReadWrite, format);
				IntPtr destination = data->Scan0;
				size_t stride = pixelData.size() / Height;
				const char* source = (const char *)pixelData.data();
				for(int i=0; i < Height; i++)
				{
					MagickCore::CopyMagickMemory(destination.ToPointer(), source, stride);
					source += stride;
					destination = IntPtr(destination.ToInt64() + data->Stride);
				}
				bitmap->UnlockBits(data);
				return bitmap;
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
		//==============================================================================================
#if !(NET20)
		BitmapSource^ MagickImage::ToBitmapSource()
		{
			std::string map = "RGB";
			StorageType type = StorageType::Char;
			MediaPixelFormat format = MediaPixelFormats::Rgb24;
			if (HasAlpha)
			{
				map = "BGRA";
				format = MediaPixelFormats::Bgra32;
			}

			if (ColorSpace == ImageMagick::ColorSpace::CMYK)
			{
				map = "CMYK";
				format = MediaPixelFormats::Cmyk32;
			}

			int step = (format.BitsPerPixel / 8);
			int stride = Width * step;

			try
			{
				Magick::PixelData pixelData(*Value, map, (MagickCore::StorageType)type);
				if (pixelData.length() == 0)
					return nullptr;

				return BitmapSource::Create(Width, Height, 96, 96, format, nullptr, IntPtr((void *) pixelData.data()), stride * Height, stride);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
				return nullptr;
			}
		}
#endif
		//===========================================================================================
		array<Byte>^ MagickImage::ToByteArray()
		{
			Magick::Blob blob;
			HandleException(MagickWriter::Write(this->Value, &blob));
			return Marshaller::Marshal(&blob);
		}
		//===========================================================================================
		void MagickImage::Transform(MagickGeometry^ imageGeometry)
		{
			const Magick::Geometry* geometry = imageGeometry->CreateGeometry();

			try
			{
				Value->transform(*geometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete geometry;
			}
		}
		//===========================================================================================
		void MagickImage::Transform(MagickGeometry^ imageGeometry, MagickGeometry^ cropGeometry)
		{
			const Magick::Geometry* geometryImage = imageGeometry->CreateGeometry();
			const Magick::Geometry* geometryCrop = cropGeometry->CreateGeometry();

			try
			{ 
				Value->transform(*geometryImage, *geometryCrop);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete geometryImage;
				delete geometryCrop;
			}
		}
		//===========================================================================================
		void MagickImage::TransformOrigin(double x, double y)
		{
			Value->transformOrigin(x, y);
		}
		//===========================================================================================
		void MagickImage::TransformRotation(double angle)
		{
			Value->transformRotation(angle);
		}
		//===========================================================================================
		void MagickImage::TransformReset()
		{
			Value->transformReset();
		}
		//===========================================================================================
		void MagickImage::TransformScale(double scaleX, double scaleY)
		{
			Value->transformScale(scaleX, scaleY);
		}
		//===========================================================================================
		void MagickImage::TransformSkewX(double skewX)
		{
			Value->transformSkewX(skewX);
		}
		//===========================================================================================
		void MagickImage::TransformSkewY(double skewY)
		{
			Value->transformSkewY(skewY);
		}
		//===========================================================================================
		void MagickImage::Transparent(MagickColor^ color)
		{
			const Magick::Color* transparentColor = color->CreateColor();

			try
			{
				Value->transparent(*transparentColor);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete transparentColor;
			}
		}
		//===========================================================================================
		void MagickImage::TransparentChroma(MagickColor^ colorLow, MagickColor^ colorHigh)
		{
			const Magick::Color* transparentColorLow = colorLow->CreateColor();
			const Magick::Color* transparentColorHigh = colorHigh->CreateColor();

			try
			{
				Value->transparentChroma(*transparentColorLow, *transparentColorHigh);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete transparentColorLow;
				delete transparentColorHigh;
			}
		}
		//===========================================================================================
		void MagickImage::Transpose()
		{
			try
			{
				Value->transpose();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Transverse()
		{
			try
			{
				Value->transverse();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Trim()
		{
			try
			{
				Value->trim();
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		MagickImage^ MagickImage::UniqueColors()
		{
			Magick::Image uniqueColors = Value->uniqueColors();
			if (!uniqueColors.isValid())
				return nullptr;

			return gcnew MagickImage(uniqueColors);
		}
		//===========================================================================================
		void MagickImage::Unsharpmask(double radius, double sigma, double amount, double threshold)
		{
			try
			{
				Value->unsharpmask(radius, sigma, amount, threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Unsharpmask(double radius, double sigma, double amount, double threshold, ImageMagick::Channels channels)
		{
			try
			{
				Value->unsharpmaskChannel((Magick::ChannelType)channels, radius, sigma, amount, threshold);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Vignette(double radius, double sigma, int x, int y)
		{
			try
			{
				Value->vignette(radius, sigma, x, y);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Wave(double amplitude, double length)
		{
			try
			{
				Value->wave(amplitude, length);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::WhiteThreshold(String^ threshold)
		{
			try
			{
				std::string threshold_;
				Marshaller::Marshal(threshold, threshold_);
				Value->whiteThreshold(threshold_);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::WhiteThreshold(String^ threshold, ImageMagick::Channels channels)
		{
			try
			{
				std::string threshold_;
				Marshaller::Marshal(threshold, threshold_);
				Value->whiteThresholdChannel((Magick::ChannelType)channels, threshold_);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
		}
		//===========================================================================================
		void MagickImage::Write(Stream^ stream)
		{
			HandleException(MagickWriter::Write(Value, stream));
		}
		//===========================================================================================
		void MagickImage::Write(String^ fileName)
		{
			HandleException(MagickWriter::Write(Value, fileName));
		}
		//===========================================================================================
		void MagickImage::Zoom(MagickGeometry^ geometry)
		{
			const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

			try
			{
				Value->zoom(*magickGeometry);
			}
			catch(Magick::Exception& exception)
			{
				HandleException(exception);
			}
			finally
			{
				delete magickGeometry;
			}
		}
		//===========================================================================================
	}
}