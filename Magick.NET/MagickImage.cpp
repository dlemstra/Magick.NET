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
#include "Helpers\FileHelper.h"
#include "MagickImage.h"
#include "MagickImageCollection.h"
#include "Quantum.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	template<class TImageProfile>
	TImageProfile^ MagickImage::CreateProfile(String^ name)
	{
		Throw::IfNullOrEmpty("name", name);

		try
		{
			std::string profileName;
			Marshaller::Marshal(name, profileName);
			Magick::Blob blob = Value->profile(profileName);
			array<Byte>^ data = Marshaller::Marshal(&blob);
			if (data == nullptr)
				return nullptr;

			TImageProfile^ result = gcnew TImageProfile();
			result->Initialize(name, data);
			result->Initialize(this);
			return result;
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, int x, int y, bool invert)
	{
		Throw::IfNull("color", color);

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
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, int x, int y, MagickColor^ borderColor, bool invert)
	{
		Throw::IfNull("color", color);
		Throw::IfNull("borderColor", borderColor);

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
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, MagickGeometry^ geometry, bool invert)
	{
		Throw::IfNull("color", color);
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, MagickGeometry^ geometry, MagickColor^ borderColor, bool invert)
	{
		Throw::IfNull("color", color);
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("borderColor", borderColor);

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
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, int x, int y, bool invert)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->floodFillTexture(x, y, *image->Value, invert);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, int x, int y, MagickColor^ borderColor, bool invert)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("borderColor", borderColor);

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
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, MagickGeometry^ geometry, bool invert)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, MagickGeometry^ geometry, MagickColor^ borderColor, bool invert)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("borderColor", borderColor);

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
	//==============================================================================================
	String^ MagickImage::FormatedFileSize()
	{
		Decimal fileSize = FileSize;

		String^ suffix = "";
		if (fileSize > 1073741824)
		{
			fileSize /= 1073741824;
			suffix = "GB";
		}
		else if (fileSize > 1048576)
		{
			fileSize /= 1048576;
			suffix = "MB";
		}
		else if (fileSize > 1024)
		{
			fileSize /= 1024;
			suffix = "kB";
		}

		return String::Format(CultureInfo::InvariantCulture, "{0:N2}{1}", fileSize, suffix);
	}
	//==============================================================================================
	MagickFormat MagickImage::GetCoderFormat(MagickFormat format)
	{
		switch(format)
		{
		case MagickFormat::Jpg:
		case MagickFormat::Pjpeg:
			return MagickFormat::Jpeg;
		case MagickFormat::Tif:
		case MagickFormat::Tiff64:
			return MagickFormat::Tiff;
		default:
			return format;
		}
	}
	//==============================================================================================
	void MagickImage::HandleException(const Magick::Exception& exception)
	{
		HandleException(MagickException::Create(exception));
	}
	//==============================================================================================
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
	//==============================================================================================
	MagickWarningException^ MagickImage::HandleReadException(MagickException^ exception)
	{
		if (exception == nullptr)
			return nullptr;

		MagickWarningException^ warning = dynamic_cast<MagickWarningException^>(exception);
		if (warning == nullptr)
			throw exception;

		_ReadWarning = warning;
		if (_WarningEvent != nullptr)
			_WarningEvent->Invoke(this, gcnew WarningEventArgs(warning));

		return warning;
	}
	//==============================================================================================
	bool MagickImage::IsSupportedImageFormat(ImageFormat^ format)
	{
		return
			format->Guid.Equals(ImageFormat::Bmp->Guid) ||
			format->Guid.Equals(ImageFormat::Gif->Guid) ||
			format->Guid.Equals(ImageFormat::Icon->Guid) ||
			format->Guid.Equals(ImageFormat::Jpeg->Guid) ||
			format->Guid.Equals(ImageFormat::Png->Guid) ||
			format->Guid.Equals(ImageFormat::Tiff->Guid);
	}
	//==============================================================================================
	void MagickImage::LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, bool invert)
	{
		Throw::IfNull("blackColor", blackColor);
		Throw::IfNull("whiteColor", whiteColor);

		const Magick::Color* black = blackColor->CreateColor();
		const Magick::Color* white = whiteColor->CreateColor();

		try
		{
			Value->levelColors(*black, *white, invert);
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
	///=============================================================================================
	void MagickImage::LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, Channels channels, bool invert)
	{
		Throw::IfNull("blackColor", blackColor);
		Throw::IfNull("whiteColor", whiteColor);

		const Magick::Color* black = blackColor->CreateColor();
		const Magick::Color* white = whiteColor->CreateColor();

		try
		{
			Value->levelColorsChannel((Magick::ChannelType)channels, *black, *white, invert);
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
	//==============================================================================================
	void MagickImage::Opaque(MagickColor^ target, MagickColor^ fill, bool invert)
	{
		Throw::IfNull("target", target);
		Throw::IfNull("fill", fill);

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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::RandomThreshold(Magick::Quantum low, Magick::Quantum high, Channels channels, bool isPercentage)
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
	//==============================================================================================
	void MagickImage::SetFormat(ImageFormat^ format)
	{
		if (format == ImageFormat::Bmp)
			Format = MagickFormat::Bmp;
		else if (format == ImageFormat::Gif)
			Format = MagickFormat::Gif;
		else if (format == ImageFormat::Icon)
			Format = MagickFormat::Icon;
		else if (format == ImageFormat::Jpeg)
			Format = MagickFormat::Jpeg;
		else if (format == ImageFormat::Png)
			Format = MagickFormat::Png;
		else if (format == ImageFormat::Tiff)
			Format = MagickFormat::Tiff;
		else
			throw gcnew NotSupportedException("Unsupported image format: " + format->ToString());
	}
	//==============================================================================================
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
	template<typename TMagickOption>
	void MagickImage::SetOption(String^ name, TMagickOption commandOption, ssize_t value)
	{
		try
		{
			std::string option;
			Marshaller::Marshal(name, option);

			MagickCore::SetImageOption(Value->imageInfo(), option.c_str(), MagickCore::CommandOptionToMnemonic(commandOption, value));
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
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
	//==============================================================================================
	MagickImage::MagickImage(const Magick::Image& image)
	{
		Value = new Magick::Image(image);
	}
	//==============================================================================================
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
		{
			Value->quantizeDither(false);
		}
		Value->quantizeTreeDepth(settings->TreeDepth);
	}
	//==============================================================================================
	const Magick::Image& MagickImage::ReuseValue()
	{
		return *Value;
	}
	//==============================================================================================
	MagickImage::MagickImage()
	{
		Value = new Magick::Image();
	}
	//==============================================================================================
	MagickImage::MagickImage(array<Byte>^ data)
	{
		Value = new Magick::Image();
		this->Read(data);
	}
	//==============================================================================================
	MagickImage::MagickImage(array<Byte>^ data, MagickReadSettings^ readSettings)
	{
		Value = new Magick::Image();
		this->Read(data, readSettings);
	}
	//==============================================================================================
	MagickImage::MagickImage(Bitmap^ bitmap)
	{
		Value = new Magick::Image();
		this->Read(bitmap);
	}
	//==============================================================================================
	MagickImage::MagickImage(FileInfo^ file)
	{
		Value = new Magick::Image();
		this->Read(file);
	}
	//==============================================================================================
	MagickImage::MagickImage(FileInfo^ file, MagickReadSettings^ readSettings)
	{
		Value = new Magick::Image();
		this->Read(file, readSettings);
	}
	//==============================================================================================
	MagickImage::MagickImage(MagickColor^ color, int width, int height)
	{
		Throw::IfNull("color", color);

		const Magick::Geometry* geometry = new Magick::Geometry(width, height);
		const Magick::Color* background = color->CreateColor();
		try
		{
			Value = new Magick::Image(*geometry, *background);
			Value->backgroundColor(*background);
		}
		finally
		{
			delete geometry;
			delete background;
		}
	}
	//==============================================================================================
	MagickImage::MagickImage(MagickImage^ image)
	{
		Throw::IfNull("image", image);

		Value = new Magick::Image(*image->Value);
	}
	//==============================================================================================
	MagickImage::MagickImage(String^ fileName)
	{
		Value = new Magick::Image();
		this->Read(fileName);
	}
	//==============================================================================================
	MagickImage::MagickImage(String^ fileName, MagickReadSettings^ readSettings)
	{
		Value = new Magick::Image();
		this->Read(fileName, readSettings);
	}
	//==============================================================================================
	MagickImage::MagickImage(Stream^ stream)
	{
		Value = new Magick::Image();
		this->Read(stream);
	}
	//==============================================================================================
	MagickImage::MagickImage(Stream^ stream, MagickReadSettings^ readSettings)
	{
		Value = new Magick::Image();
		this->Read(stream, readSettings);
	}
	//==============================================================================================
	bool MagickImage::Adjoin::get()
	{
		return Value->adjoin();
	}
	//==============================================================================================
	void MagickImage::Adjoin::set(bool value)
	{
		Value->adjoin(value);
	}
	//==============================================================================================
	MagickColor^ MagickImage::AlphaColor::get()
	{
		return gcnew MagickColor(Value->alphaColor());
	}
	//==============================================================================================
	void MagickImage::AlphaColor::set(MagickColor^ value)
	{
		const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
		Value->alphaColor(*color);
		delete color;
	}
	//==============================================================================================
	int MagickImage::AnimationDelay::get()
	{
		return Convert::ToInt32(Value->animationDelay());
	}
	//==============================================================================================
	void MagickImage::AnimationDelay::set(int value)
	{
		Value->animationDelay(value);
	}
	//==============================================================================================
	int MagickImage::AnimationIterations::get()
	{
		return Convert::ToInt32(Value->animationIterations());
	}
	//==============================================================================================
	void MagickImage::AnimationIterations::set(int value)
	{
		Value->animationIterations(value);
	}
	//==============================================================================================
	bool MagickImage::AntiAlias::get()
	{
		return Value->antiAlias();
	}
	//==============================================================================================
	void MagickImage::AntiAlias::set(bool value)
	{
		Value->antiAlias(value);
	}
	//==============================================================================================
	MagickColor^ MagickImage::BackgroundColor::get()
	{
		return gcnew MagickColor(Value->backgroundColor());
	}
	//==============================================================================================
	void MagickImage::BackgroundColor::set(MagickColor^ value)
	{
		const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
		Value->backgroundColor(*color);
		delete color;
	}
	//==============================================================================================
	int MagickImage::BaseHeight::get()
	{
		return Convert::ToInt32(Value->baseRows());
	}
	//==============================================================================================
	int MagickImage::BaseWidth::get()
	{
		return Convert::ToInt32(Value->baseColumns());
	}
	//==============================================================================================
	bool MagickImage::BlackPointCompensation::get()
	{
		return Value->blackPointCompensation();
	}
	//==============================================================================================
	void MagickImage::BlackPointCompensation::set(bool value)
	{
		Value->blackPointCompensation(value);
	}
	//==============================================================================================
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
	//==============================================================================================
	MagickColor^ MagickImage::BorderColor::get()
	{
		return gcnew MagickColor(Value->borderColor());
	}
	//==============================================================================================
	void MagickImage::BorderColor::set(MagickColor^ value)
	{
		const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
		Value->borderColor(*color);
		delete color;
	}
	//==============================================================================================
	MagickColor^ MagickImage::BoxColor::get()
	{
		return gcnew MagickColor(Value->boxColor());
	}
	//==============================================================================================
	void MagickImage::BoxColor::set(MagickColor^ value)
	{
		const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
		Value->boxColor(*color);
		delete color;
	}
	//==============================================================================================
	ClassType MagickImage::ClassType::get()
	{
		return (ImageMagick::ClassType)Value->classType();
	}
	//==============================================================================================
	void MagickImage::ClassType::set(ImageMagick::ClassType value)
	{
		return Value->classType((Magick::ClassType)value);
	}
	//==============================================================================================
	MagickImage^ MagickImage::ClipMask::get()
	{
		Magick::Image clipMask = Value->mask();
		if (!clipMask.isValid())
			return nullptr;

		return gcnew MagickImage(clipMask);
	}
	//==============================================================================================
	void MagickImage::ClipMask::set(MagickImage^ value)
	{
		if (value == nullptr)
		{
			Magick::Image* image = new Magick::Image();
			Value->mask(*image);
			delete image;
		}
		else
		{
			Value->mask(*value->Value);
		}
	}
	//==============================================================================================
	Percentage MagickImage::ColorFuzz::get()
	{
		return Percentage::FromQuantum(Value->colorFuzz());
	}
	//==============================================================================================
	void MagickImage::ColorFuzz::set(Percentage value)
	{
		Value->colorFuzz(value.ToQuantum());
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::ColorMapSize::set(int value)
	{
		Value->colorMapSize(value);
	}
	//==============================================================================================
	ColorSpace MagickImage::ColorSpace::get()
	{
		return (ImageMagick::ColorSpace)Value->colorSpace();
	}
	//==============================================================================================
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
	//==============================================================================================
	ColorType MagickImage::ColorType::get()
	{
		return (ImageMagick::ColorType)Value->type();
	}
	//==============================================================================================
	void MagickImage::ColorType::set(ImageMagick::ColorType value)
	{
		Value->type((Magick::ImageType)value);
	}
	//==============================================================================================
	String^ MagickImage::Comment::get()
	{
		return Marshaller::Marshal(Value->comment());
	}
	//==============================================================================================
	void MagickImage::Comment::set(String^ value)
	{
		std::string comment; 
		Value->comment(Marshaller::Marshal(value, comment));
	}
	//==============================================================================================
	CompositeOperator MagickImage::Compose::get()
	{
		return (CompositeOperator)Value->compose();
	}
	//==============================================================================================
	void MagickImage::Compose::set(CompositeOperator value)
	{
		Value->compose((Magick::CompositeOperator)value);
	}
	//==============================================================================================
	CompressionMethod MagickImage::CompressionMethod::get()
	{
		return (ImageMagick::CompressionMethod)Value->compressType();
	}
	//==============================================================================================
	void MagickImage::CompressionMethod::set(ImageMagick::CompressionMethod value)
	{
		Value->compressType((Magick::CompressionType)value);
	}
	//==============================================================================================
	bool MagickImage::Debug::get()
	{
		return Value->debug();
	}
	//==============================================================================================
	void MagickImage::Debug::set(bool value)
	{
		Value->debug(value);
	} 
	//==============================================================================================
	MagickGeometry^ MagickImage::Density::get()
	{
		return gcnew MagickGeometry(Value->density());
	}
	//==============================================================================================
	void MagickImage::Density::set(MagickGeometry^ value)
	{
		if (value == nullptr)
			return;

		const Magick::Geometry* geometry = value->CreateGeometry();
		Value->density(*geometry);
		delete geometry;

		if (ResolutionUnits == Resolution::Undefined)
			ResolutionUnits = Resolution::PixelsPerInch;
	}
	//==============================================================================================
	int MagickImage::Depth::get()
	{
		return Convert::ToInt32(Value->depth());
	}
	//==============================================================================================
	void MagickImage::Depth::set(int value)
	{
		Value->depth(value);
	}
	//==============================================================================================
	Endian MagickImage::Endian::get()
	{
		return (ImageMagick::Endian)Value->endian();
	}
	//==============================================================================================
	void MagickImage::Endian::set(ImageMagick::Endian value)
	{
		Value->endian((Magick::EndianType)value);
	}
	//==============================================================================================
	long MagickImage::FileSize::get()
	{
		return (long)Value->fileSize();
	}
	//==============================================================================================
	String^ MagickImage::FileName::get()
	{
		return Marshaller::Marshal(Value->baseFilename());
	}
	//==============================================================================================
	MagickColor^ MagickImage::FillColor::get()
	{
		return gcnew MagickColor(Value->fillColor());
	}
	//==============================================================================================
	void MagickImage::FillColor::set(MagickColor^ value)
	{
		const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
		Value->fillColor(*color);
		delete color;
		String^ colorName = ReferenceEquals(value, nullptr) ? nullptr : value->ToString();
		SetOption("fill", colorName);
	}
	//==============================================================================================
	MagickImage^ MagickImage::FillPattern::get()
	{
		Magick::Image fillPattern = Value->fillPattern();
		if (!fillPattern.isValid())
			return nullptr;

		return gcnew MagickImage(fillPattern);
	}
	//==============================================================================================
	void MagickImage::FillPattern::set(MagickImage^ value)
	{
		if (value == nullptr)
		{
			Magick::Image* image = new Magick::Image();
			Value->fillPattern(*image);
			delete image;
		}
		else
		{
			Value->fillPattern(*value->Value);
		}
	}
	//==============================================================================================
	FillRule MagickImage::FillRule::get()
	{
		return (ImageMagick::FillRule)Value->fillRule();
	}
	//==============================================================================================
	void MagickImage::FillRule::set(ImageMagick::FillRule value)
	{
		Value->fillRule((Magick::FillRule)value);
	}
	//==============================================================================================
	FilterType MagickImage::FilterType::get()
	{
		return (ImageMagick::FilterType)Value->filterType();
	}
	//==============================================================================================
	void MagickImage::FilterType::set(ImageMagick::FilterType value)
	{
		Value->filterType((Magick::FilterTypes)value);
	}
	//==============================================================================================
	String^ MagickImage::FlashPixView::get()
	{
		return Marshaller::Marshal(Value->view());
	}
	//==============================================================================================
	void MagickImage::FlashPixView::set(String^ value)
	{
		std::string view;
		Value->view(Marshaller::Marshal(value, view));
	}
	//==============================================================================================
	String^ MagickImage::Font::get()
	{
		return Marshaller::Marshal(Value->font());
	}
	//==============================================================================================
	void MagickImage::Font::set(String^ value)
	{
		std::string font;
		Value->font(Marshaller::Marshal(value, font));
	}
	//==============================================================================================
	double MagickImage::FontPointsize::get()
	{
		return Value->fontPointsize();
	}
	//==============================================================================================
	void MagickImage::FontPointsize::set(double value)
	{
		Value->fontPointsize(value);
	}
	//==============================================================================================
	MagickFormat MagickImage::Format::get()
	{
		return EnumHelper::Parse<MagickFormat>(Marshaller::Marshal(Value->magick()), MagickFormat::Unknown);
	}
	//==============================================================================================
	void MagickImage::Format::set(MagickFormat value)
	{
		if (value == MagickFormat::Unknown)
			return;

		std::string name;
		Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, value), name);

		Value->magick(name);
	}
	//==============================================================================================
	MagickFormatInfo^ MagickImage::FormatInfo::get()
	{
		if (Format == MagickFormat::Unknown)
			return nullptr;

		for each(MagickFormatInfo^ info in MagickFormatInfo::All)
		{
			if (info->Format == Format)
				return info;
		}

		return nullptr;
	}
	//==============================================================================================
	GifDisposeMethod MagickImage::GifDisposeMethod::get()
	{
		return (ImageMagick::GifDisposeMethod)Value->gifDisposeMethod();
	}
	//==============================================================================================
	void MagickImage::GifDisposeMethod::set(ImageMagick::GifDisposeMethod value)
	{
		Value->gifDisposeMethod((Magick::DisposeType)value);
	}
	//==============================================================================================
	bool MagickImage::HasAlpha::get()
	{
		return Value->alpha();
	}
	//==============================================================================================
	void MagickImage::HasAlpha::set(bool value)
	{
		Value->alpha(value);
	}
	//==============================================================================================
	int MagickImage::Height::get()
	{
		return Convert::ToInt32(Value->size().height());
	}
	//==============================================================================================
	Interlace MagickImage::Interlace::get()
	{
		return (ImageMagick::Interlace)Value->interlaceType();
	}
	//==============================================================================================
	void MagickImage::Interlace::set(ImageMagick::Interlace value)
	{
		Value->interlaceType((Magick::InterlaceType)value);
	}
	//==============================================================================================
	PixelInterpolateMethod MagickImage::Interpolate::get()
	{
		return (PixelInterpolateMethod)Value->interpolate();
	}
	//==============================================================================================
	void MagickImage::Interpolate::set(PixelInterpolateMethod value)
	{
		Value->interpolate((Magick::PixelInterpolateMethod)value);
	}
	//==============================================================================================
	bool MagickImage::IsMonochrome::get()
	{
		return Value->monochrome();
	}
	//==============================================================================================
	void MagickImage::IsMonochrome::set(bool value)
	{
		Value->monochrome(value);
	}
	//==============================================================================================
	String^ MagickImage::Label::get()
	{
		std::string label = Value->label();
		if (label.length() == 0)
			return nullptr;

		return Marshaller::Marshal(label);
	}
	//==============================================================================================
	void MagickImage::Label::set(String^ value)
	{
		if (value == nullptr)
			value = "";

		std::string label;
		Marshaller::Marshal(value, label);
		Value->label(label);
	}
	//==============================================================================================
	MagickImage^ MagickImage::Mask::get()
	{
		Magick::Image mask = Value->mask();
		if (!mask.isValid())
			return nullptr;

		return gcnew MagickImage(mask);
	}
	//==============================================================================================
	void MagickImage::Mask::set(MagickImage^ value)
	{
		if (value == nullptr)
		{
			Magick::Image* image = new Magick::Image();
			Value->mask(*image);
			delete image;
		}
		else
		{
			Value->mask(*value->Value);
		}
	}
	//==============================================================================================
	OrientationType MagickImage::Orientation::get()
	{
		return (OrientationType)Value->orientation();
	}
	//==============================================================================================
	void MagickImage::Orientation::set(OrientationType value)
	{
		Value->orientation((Magick::OrientationType)value);
	}
	//==============================================================================================
	MagickGeometry^ MagickImage::Page::get()
	{
		return gcnew MagickGeometry(Value->page());
	}
	//==============================================================================================
	void MagickImage::Page::set(MagickGeometry^ value)
	{
		Throw::IfNull("value", value);

		const Magick::Geometry* geometry = value->CreateGeometry();
		Value->page(*geometry);
		delete geometry;
	}
	//==============================================================================================
	IEnumerable<String^>^ MagickImage::ProfileNames::get()
	{
		List<String^>^ names = gcnew List<String^>();

		std::list<std::string> *profileNames = new std::list<std::string>();

		try
		{
			Magick::profileNames(profileNames, Value);
			for (std::list<std::string>::iterator iter = profileNames->begin(), end = profileNames->end(); iter != end; ++iter)
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
	//==============================================================================================
	int MagickImage::Quality::get()
	{
		return Convert::ToInt32(Value->quality());
	}
	//==============================================================================================
	void MagickImage::Quality::set(int value)
	{
		int quality = value < 1 ? 1 : value;
		quality = quality > 100 ? 100 : quality;

		Value->quality(quality);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::ReadWarning::get()
	{
		return _ReadWarning;
	}
	//==============================================================================================
	RenderingIntent MagickImage::RenderingIntent::get()
	{
		return (ImageMagick::RenderingIntent)Value->renderingIntent();
	}
	//==============================================================================================
	void MagickImage::RenderingIntent::set(ImageMagick::RenderingIntent value)
	{
		return Value->renderingIntent((Magick::RenderingIntent)value);
	}
	//==============================================================================================
	Resolution MagickImage::ResolutionUnits::get()
	{
		return (Resolution)Value->resolutionUnits();
	}
	//==============================================================================================
	void MagickImage::ResolutionUnits::set(Resolution value)
	{
		return Value->resolutionUnits((Magick::ResolutionType)value);
	}
	//==============================================================================================
	double MagickImage::ResolutionX::get()
	{
		return Value->xResolution();
	}
	//==============================================================================================
	double MagickImage::ResolutionY::get()
	{
		return Value->yResolution();
	}
	//==============================================================================================
	bool MagickImage::StrokeAntiAlias::get()
	{
		return Value->strokeAntiAlias();
	}
	//==============================================================================================
	void MagickImage::StrokeAntiAlias::set(bool value)
	{
		Value->strokeAntiAlias(value);
	}
	//==============================================================================================
	MagickColor^ MagickImage::StrokeColor::get()
	{
		return gcnew MagickColor(Value->strokeColor());
	}
	//==============================================================================================
	void MagickImage::StrokeColor::set(MagickColor^ value)
	{
		const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
		Value->strokeColor(*color);
		delete color;
		String^ colorName = ReferenceEquals(value, nullptr) ? nullptr : value->ToString();
		SetOption("stroke", colorName);
	}
	//==============================================================================================
	array<double>^ MagickImage::StrokeDashArray::get()
	{
		const double* strokeDashArray = Value->strokeDashArray();
		if (strokeDashArray == NULL)
			return nullptr;

		return Marshaller::Marshal(strokeDashArray);
	}
	//==============================================================================================
	void MagickImage::StrokeDashArray::set(array<double>^ value)
	{
		double* strokeDashArray = Marshaller::MarshalAndTerminate(value);
		Value->strokeDashArray(strokeDashArray);
		delete[] strokeDashArray;
	}
	//==============================================================================================
	double MagickImage::StrokeDashOffset::get()
	{
		return Value->strokeDashOffset();
	}
	//==============================================================================================
	void MagickImage::StrokeDashOffset::set(double value)
	{
		Value->strokeDashOffset(value);
	}
	//==============================================================================================
	LineCap MagickImage::StrokeLineCap::get()
	{
		return (LineCap)Value->strokeLineCap();
	}
	//==============================================================================================
	void MagickImage::StrokeLineCap::set(LineCap value)
	{
		Value->strokeLineCap((Magick::LineCap)value);
	}
	//==============================================================================================
	LineJoin MagickImage::StrokeLineJoin::get()
	{
		return (LineJoin)Value->strokeLineJoin();
	}
	//==============================================================================================
	void MagickImage::StrokeLineJoin::set(LineJoin value)
	{
		Value->strokeLineJoin((Magick::LineJoin)value);
	}
	//==============================================================================================
	int MagickImage::StrokeMiterLimit::get()
	{
		return Convert::ToInt32(Value->strokeMiterLimit());
	}
	//==============================================================================================
	void MagickImage::StrokeMiterLimit::set(int value)
	{
		Value->strokeMiterLimit(value);
	}
	//==============================================================================================
	MagickImage^ MagickImage::StrokePattern::get()
	{
		Magick::Image strokePattern = Value->strokePattern();
		if (!strokePattern.isValid())
			return nullptr;

		return gcnew MagickImage(strokePattern);
	}
	//==============================================================================================
	void MagickImage::StrokePattern::set(MagickImage^ value)
	{
		if (value == nullptr)
			Value->strokePattern(Magick::Image());
		else
			Value->strokePattern(*value->Value);
	}
	//==============================================================================================
	double MagickImage::StrokeWidth::get()
	{
		return Value->strokeWidth();
	}
	//==============================================================================================
	void MagickImage::StrokeWidth::set(double value)
	{
		Value->strokeWidth(value);
		SetOption("strokeWidth", value.ToString(CultureInfo::InvariantCulture));
	}
	//==============================================================================================
	TextDirection MagickImage::TextDirection::get()
	{
		return (ImageMagick::TextDirection)Value->textDirection();
	}
	//==============================================================================================
	void MagickImage::TextDirection::set(ImageMagick::TextDirection value)
	{
		Value->textDirection((Magick::DirectionType)value);
		SetOption("direction", MagickCore::MagickDirectionOptions, (ssize_t)value);;
	}
	//==============================================================================================
	Encoding^ MagickImage::TextEncoding::get()
	{
		String^ encoding = Marshaller::Marshal(Value->textEncoding());

		if (String::IsNullOrEmpty(encoding))
			return nullptr;

		try
		{
			return Encoding::GetEncoding(encoding);
		}
		catch (ArgumentException^)
		{
			return nullptr;
		}
	}
	//==============================================================================================
	void MagickImage::TextEncoding::set(Encoding^ value)
	{
		String^ name = value != nullptr ? value->WebName : nullptr;

		std::string encoding;
		Value->textEncoding(Marshaller::Marshal(name, encoding));
		SetOption("encoding", name);
	}
	//==============================================================================================
	Gravity MagickImage::TextGravity::get()
	{
		return (ImageMagick::Gravity)Value->textGravity();
	}
	//==============================================================================================
	void MagickImage::TextGravity::set(Gravity value)
	{
		Value->textGravity((Magick::GravityType)value);
		SetOption("gravity", MagickCore::MagickGravityOptions, (ssize_t)value);
	}
	//==============================================================================================
	double MagickImage::TextInterlineSpacing::get()
	{
		return Value->textInterlineSpacing();
	}
	//==============================================================================================
	void MagickImage::TextInterlineSpacing::set(double value)
	{
		Value->textInterlineSpacing(value);
		SetOption("interline-spacing", value.ToString(CultureInfo::InvariantCulture));
	}
	//==============================================================================================
	double MagickImage::TextInterwordSpacing::get()
	{
		return Value->textInterwordSpacing();
	}
	//==============================================================================================
	void MagickImage::TextInterwordSpacing::set(double value)
	{
		Value->textInterwordSpacing(value);
		SetOption("interword-spacing", value.ToString(CultureInfo::InvariantCulture));
	}
	//==============================================================================================
	double MagickImage::TextKerning::get()
	{
		return Value->textKerning();
	}
	//==============================================================================================
	void MagickImage::TextKerning::set(double value)
	{
		Value->textKerning(value);
		SetOption("kerning", value.ToString(CultureInfo::InvariantCulture));
	}
	//==============================================================================================
	int MagickImage::TotalColors::get()
	{
		return Convert::ToInt32(Value->totalColors());
	}
	//==============================================================================================
	bool MagickImage::Verbose::get() 
	{
		return Value->verbose();
	}
	//==============================================================================================
	void MagickImage::Verbose::set(bool verbose) 
	{
		return Value->verbose(verbose);
	}
	//==============================================================================================
	VirtualPixelMethod MagickImage::VirtualPixelMethod::get()
	{
		return (ImageMagick::VirtualPixelMethod)Value->virtualPixelMethod();
	}
	//==============================================================================================
	void MagickImage::VirtualPixelMethod::set(ImageMagick::VirtualPixelMethod value)
	{
		Value->virtualPixelMethod((Magick::VirtualPixelMethod)value);
	}
	//==============================================================================================
	int MagickImage::Width::get()
	{
		return Convert::ToInt32(Value->size().width());
	}
	//==============================================================================================
	bool MagickImage::operator == (MagickImage^ left, MagickImage^ right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool MagickImage::operator != (MagickImage^ left, MagickImage^ right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool MagickImage::operator > (MagickImage^ left, MagickImage^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return ReferenceEquals(right, nullptr);

		return left->CompareTo(right) == 1;
	}
	//==============================================================================================
	bool MagickImage::operator < (MagickImage^ left, MagickImage^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return !ReferenceEquals(right, nullptr);

		return left->CompareTo(right) == -1;
	}
	//==============================================================================================
	bool MagickImage::operator >= (MagickImage^ left, MagickImage^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return ReferenceEquals(right, nullptr);

		return left->CompareTo(right) >= 0;
	}
	//==============================================================================================
	bool MagickImage::operator <= (MagickImage^ left, MagickImage^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return !ReferenceEquals(right, nullptr);

		return left->CompareTo(right) <= 0;
	}
	//==============================================================================================
	MagickImage::operator array<Byte>^ (MagickImage^ image)
	{
		Throw::IfNull("image", image);

		return image->ToByteArray();
	}
	//==============================================================================================
	void MagickImage::Warning::add(EventHandler<WarningEventArgs^>^ handler)
	{
		_WarningEvent += handler;
	}
	//==============================================================================================
	void MagickImage::Warning::remove(EventHandler<WarningEventArgs^>^ handler)
	{
		_WarningEvent -= handler;
	}
	//==============================================================================================
	void MagickImage::AdaptiveBlur()
	{
		AdaptiveBlur(0.0, 1.0);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::AdaptiveResize(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		AdaptiveResize(geometry);
	}
	//==============================================================================================
	void MagickImage::AdaptiveResize(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::AdaptiveSharpen()
	{
		AdaptiveSharpen(0.0, 1.0);
	}
	//==============================================================================================
	void MagickImage::AdaptiveSharpen(Channels channels)
	{
		AdaptiveSharpen(0.0, 1.0, channels);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::AdaptiveSharpen(double radius, double sigma, Channels channels)
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
	//==============================================================================================
	void MagickImage::AdaptiveThreshold(int width, int height)
	{
		AdaptiveThreshold(width, height, 0);
	}
	//==============================================================================================
	void MagickImage::AdaptiveThreshold(int width, int height, int offset)
	{
		try
		{
			Value->adaptiveThreshold(width, height, offset);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::AddNoise(NoiseType noiseType, Channels channels)
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
	//==============================================================================================
	void MagickImage::AddProfile(ImageProfile^ profile)
	{
		Throw::IfNull("profile", profile);

		try
		{
			Magick::Blob blob;
			Marshaller::Marshal(profile->GetData(), &blob);

			std::string profileName;
			Marshaller::Marshal(profile->Name, profileName);

			Value->profile(profileName, blob);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::AffineTransform(DrawableAffine^ drawableAffine)
	{
		Throw::IfNull("drawableAffine", drawableAffine);

		try
		{
			Value->affineTransform(*((Magick::DrawableAffine*)drawableAffine->InternalValue));
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Annotate(String^ text, MagickGeometry^ location)
	{
		Throw::IfNullOrEmpty("text", text);
		Throw::IfNull("location", location);

		std::string annotateText;
		Marshaller::Marshal(text, annotateText);
		const Magick::Geometry* geometry = location->CreateGeometry();

		try
		{
			Value->annotate(annotateText, *geometry);
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
	void MagickImage::Annotate(String^ text, MagickGeometry^ boundingArea, Gravity gravity)
	{
		Throw::IfNullOrEmpty("text", text);
		Throw::IfNull("boundingArea", boundingArea);

		std::string annotateText;
		Marshaller::Marshal(text, annotateText);
		const Magick::Geometry* geometry = boundingArea->CreateGeometry();

		try
		{
			Value->annotate(annotateText, *geometry, (Magick::GravityType)gravity);
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
	void MagickImage::Annotate(String^ text, MagickGeometry^ boundingArea, Gravity gravity, double degrees)
	{
		Throw::IfNullOrEmpty("text", text);
		Throw::IfNull("boundingArea", boundingArea);

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
	//==============================================================================================
	void MagickImage::Annotate(String^ text, Gravity gravity)
	{
		Throw::IfNullOrEmpty("text", text);

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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::AutoGamma(Channels channels)
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::AutoLevel(Channels channels)
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
	//==============================================================================================
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
	//==============================================================================================
	int MagickImage::BitDepth()
	{
		return BitDepth(Channels::Composite);
	}
	//==============================================================================================
	int MagickImage::BitDepth(Channels channels)
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
	//==============================================================================================
	void MagickImage::BitDepth(Channels channels, int value)
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
	//==============================================================================================
	void MagickImage::BitDepth(int value)
	{
		BitDepth(Channels::Composite, value);
	}
	//==============================================================================================
	void MagickImage::BlackThreshold(Percentage threshold)
	{
		Throw::IfNegative("threshold", threshold);

		try
		{
			std::string threshold_;
			Marshaller::Marshal(threshold.ToString(), threshold_);
			Value->blackThreshold(threshold_);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::BlackThreshold(Percentage threshold, Channels channels)
	{
		Throw::IfNegative("threshold", threshold);

		try
		{
			std::string threshold_;
			Marshaller::Marshal(threshold.ToString(), threshold_);
			Value->blackThresholdChannel((Magick::ChannelType)channels, threshold_);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::BlueShift()
	{
		BlueShift(1.5);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Blur()
	{
		Blur(0.0, 1.0);
	}
	//==============================================================================================
	void MagickImage::Blur(Channels channels)
	{
		Blur(0.0, 1.0, channels);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Blur(double radius, double sigma, Channels channels)
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
	//==============================================================================================
	void MagickImage::Border(int size)
	{
		Border(size, size);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::BrightnessContrast(Percentage brightness, Percentage contrast)
	{
		try
		{
			Value->brightnessContrast((double)brightness, (double)contrast);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::BrightnessContrast(Percentage brightness, Percentage contrast, Channels channels)
	{
		try
		{
			Value->brightnessContrastChannel((Magick::ChannelType)channels, (double)brightness, (double)contrast);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::CannyEdge()
	{
		CannyEdge(0.0, 1.0, 10, 30);
	}
	//==============================================================================================
	void MagickImage::CannyEdge(double radius, double sigma, Percentage lower, Percentage upper)
	{
		try
		{
			Value->cannyEdge(radius, sigma, (double)lower / 100, (double)upper / 100);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::CDL(String^ fileName)
	{
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);
		Throw::IfInvalidFileName(filePath);

		String^ cdlData = File::ReadAllText(filePath);

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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Charcoal()
	{
		Charcoal(0.0, 1.0);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Chop(int xOffset, int width, int yOffset, int height)
	{
		try
		{
			Magick::Geometry geometry = Magick::Geometry(xOffset, yOffset, width, height);
			Value->chop(geometry);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Chop(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::ChopHorizontal(int offset, int width)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(offset, 0, width, 0);
		Chop(geometry);
	}
	//==============================================================================================
	void MagickImage::ChopVertical(int offset, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(0, offset, 0, height);
		Chop(geometry);
	}
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Clamp(Channels channels)
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Clip(String^ pathName, bool inside)
	{
		Throw::IfNullOrEmpty("pathName", pathName);

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
	//==============================================================================================
	MagickImage^ MagickImage::Clone()
	{
		return gcnew MagickImage(*Value);
	}
	//==============================================================================================
	void MagickImage::Clut(MagickImage^ image, PixelInterpolateMethod method)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->clut(*image->Value,(Magick::PixelInterpolateMethod)method);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Clut(MagickImage^ image, PixelInterpolateMethod method, Channels channels)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->clutChannel((Magick::ChannelType)channels, *image->Value,(Magick::PixelInterpolateMethod)method);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::ColorAlpha(MagickColor^ color)
	{
		Throw::IfNull("color", color);

		MagickImageCollection^ images = gcnew MagickImageCollection();
		images->Add(gcnew MagickImage(color, Width, Height));
		images->Add(Clone());
		images->Merge(Value, LayerMethod::Merge);
		delete images;
	}
	//==============================================================================================
	void MagickImage::Colorize(MagickColor^ color, Percentage alpha)
	{
		Throw::IfNegative("alpha", alpha);

		Colorize(color, alpha, alpha, alpha);
	}
	//==============================================================================================
	void MagickImage::Colorize(MagickColor^ color, Percentage alphaRed, Percentage alphaGreen,
		Percentage alphaBlue)
	{
		Throw::IfNull("color", color);
		Throw::IfNegative("alphaRed", alphaRed);
		Throw::IfNegative("alphaGreen", alphaGreen);
		Throw::IfNegative("alphaBlue", alphaBlue);

		const Magick::Color* magickColor = color->CreateColor();

		try
		{
			Value->colorize((int)alphaRed, (int)alphaGreen, (int)alphaBlue, *magickColor);
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::ColorMap(int index, MagickColor^ color)
	{
		Throw::IfNull("color", color);

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
	//==============================================================================================
	void MagickImage::ColorMatrix(ImageMagick::ColorMatrix^ matrix)
	{
		Throw::IfNull("matrix", matrix);

		double* colorMatrix = matrix->CreateArray();

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
	//==============================================================================================
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

		return gcnew MagickErrorInfo(Value);
	}
	//==============================================================================================
	double MagickImage::Compare(MagickImage^ image, ErrorMetric metric)
	{
		return Compare(image, metric, Channels::Composite);
	}
	//==============================================================================================
	double MagickImage::Compare(MagickImage^ image, ErrorMetric metric, Channels channels)
	{
		Throw::IfNull("image", image);

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
	//==============================================================================================
	double MagickImage::Compare(MagickImage^ image, ErrorMetric metric, MagickImage^ difference)
	{
		return Compare(image, metric, difference, Channels::Composite);
	}
	//==============================================================================================
	double MagickImage::Compare(MagickImage^ image, ErrorMetric metric, MagickImage^ difference, Channels channels)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("difference", difference);

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
	//==============================================================================================
	int MagickImage::CompareTo(MagickImage^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return 1;

		int left = (this->Width * this->Height);
		int right = (other->Width * other->Height);

		if (left == right)
			return 0;

		return left < right ? -1 : 1;
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, int x, int y)
	{
		Composite(image, x, y, CompositeOperator::In);
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, int x, int y, CompositeOperator compose)
	{
		Composite(image, x, y, compose, nullptr);
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, int x, int y, CompositeOperator compose, String^ args)
	{
		Throw::IfNull("image", image);

		if (!String::IsNullOrEmpty(args))
			SetArtifact("compose:args", args);

		try
		{
			Value->composite(*(image->Value), x, y, (Magick::CompositeOperator)compose);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, MagickGeometry^ offset)
	{
		Composite(image, offset, CompositeOperator::In);
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, MagickGeometry^ geometry, CompositeOperator compose)
	{
		Composite(image, geometry, compose, nullptr);
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, MagickGeometry^ geometry, CompositeOperator compose, String^ args)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("geometry", geometry);

		const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

		if (!String::IsNullOrEmpty(args))
			SetArtifact("compose:args", args);

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
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, Gravity gravity)
	{
		Composite(image, gravity, CompositeOperator::In);
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, Gravity gravity, CompositeOperator compose)
	{
		Composite(image, gravity, compose, nullptr);
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, Gravity gravity, CompositeOperator compose, String^ args)
	{
		Throw::IfNull("image", image);

		if (!String::IsNullOrEmpty(args))
			SetArtifact("compose:args", args);

		try
		{
			Value->composite(*(image->Value), (Magick::GravityType)gravity, (Magick::CompositeOperator)compose);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Contrast()
	{
		Contrast(true);
	}
	//===========================================================================================
	void MagickImage::Contrast(bool enhance)
	{
		try
		{
			Value->contrast(enhance ? 0 : 1);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::ContrastStretch(Percentage blackPoint, Percentage whitePoint)
	{
		Throw::IfNegative("blackPoint", blackPoint);
		Throw::IfNegative("whitePoint", whitePoint);

		try
		{
			Value->contrastStretch(blackPoint.ToQuantum(), whitePoint.ToQuantum());
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::ContrastStretch(Percentage blackPoint, Percentage whitePoint, Channels channels)
	{
		Throw::IfNegative("blackPoint", blackPoint);
		Throw::IfNegative("whitePoint", whitePoint);

		try
		{
			Value->contrastStretchChannel((Magick::ChannelType)channels, blackPoint.ToQuantum(), whitePoint.ToQuantum());
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Convolve(ConvolveMatrix^ convolveMatrix)
	{
		Throw::IfNull("convolveMatrix", convolveMatrix);

		double* kernel = convolveMatrix->CreateArray();

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
	//==============================================================================================
	void MagickImage::Crop(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Crop(int width, int height)
	{
		Crop(width, height, Gravity::Center);
	}
	//==============================================================================================
	void MagickImage::Crop(int width, int height, Gravity gravity)
	{
		int imageWidth = (int)Value->size().width();
		int imageHeight = (int)Value->size().height();

		int newWidth = width > imageWidth ? imageWidth : width;
		int newHeight = height > imageHeight ? imageHeight : height;

		if (newWidth == imageWidth && newHeight == imageHeight)
			return;

		MagickGeometry^ geometry = gcnew MagickGeometry(newWidth, newHeight);
		switch(gravity)
		{
		case Gravity::North:
			geometry->X = (imageWidth - newWidth) / 2;
			break;
		case Gravity::Northeast:
			geometry->X = imageWidth - newWidth;
			break;
		case Gravity::East:
			geometry->X = imageWidth - newWidth;
			geometry->Y = (imageHeight - newHeight) / 2;
			break;
		case Gravity::Southeast:
			geometry->X = imageWidth - newWidth;
			geometry->Y = imageHeight - newHeight;
			break;
		case Gravity::South:
			geometry->X = (imageWidth - newWidth) / 2;
			geometry->Y = imageHeight - newHeight;
			break;
		case Gravity::Southwest:
			geometry->Y = imageHeight - newHeight;
			break;
		case Gravity::West:
			geometry->Y = (imageHeight - newHeight) / 2;
			break;
		case Gravity::Northwest:
			break;
		case Gravity::Center:
			geometry->X = (imageWidth - newWidth) / 2;
			geometry->Y = (imageHeight - newHeight) / 2;
			break;
		}

		Crop(geometry);
	}
	//==============================================================================================
	IEnumerable<MagickImage^>^  MagickImage::CropToTiles(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		return CropToTiles(geometry);
	}
	//==============================================================================================
	IEnumerable<MagickImage^>^ MagickImage::CropToTiles(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

		std::list<Magick::Image> *images = new std::list<Magick::Image>();
		const Magick::Geometry *cropGeometry = geometry->CreateGeometry();

		try
		{
			cropToTiles(images, *Value, *cropGeometry);

			return MagickImageCollection::CreateList(images);
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Decipher(String^ passphrase)
	{
		Throw::IfNullOrEmpty("passphrase", passphrase);

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
	//==============================================================================================
	void MagickImage::Deskew(Percentage threshold)
	{
		Throw::IfNegative("threshold", threshold);

		try
		{
			Value->deskew(threshold.ToQuantum());
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::DetermineColorType()
	{
		try
		{
			Value->determineType();
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Distort(DistortMethod method, array<double>^ arguments)
	{
		Distort(method, arguments, false);
	}
	//==============================================================================================
	void MagickImage::Distort(DistortMethod method, array<double>^ arguments, bool bestfit)
	{
		Throw::IfNull("arguments", arguments);

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
	//==============================================================================================
	void MagickImage::Draw(... array<Drawable^>^ drawables)
	{
		Draw((IEnumerable<Drawable^>^)drawables);
	}
	//==============================================================================================
	void MagickImage::Draw(IEnumerable<Drawable^>^ drawables)
	{
		Throw::IfNull("drawables", drawables);

		try
		{
			std::list<Magick::Drawable> drawList;
			IEnumerator<Drawable^>^ enumerator = drawables->GetEnumerator();
			while(enumerator->MoveNext())
			{
				drawList.push_back(*(enumerator->Current->InternalValue));
			}

			Value->draw(drawList);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Emboss()
	{
		Emboss(0.0, 1.0);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Encipher(String^ passphrase)
	{
		Throw::IfNullOrEmpty("passphrase", passphrase);

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
	//==============================================================================================
	MagickGeometry^ MagickImage::EncodingGeometry()
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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	bool MagickImage::Equals(Object^ obj)
	{
		if (ReferenceEquals(this, obj))
			return true;

		return Equals(dynamic_cast<MagickImage^>(obj));
	}
	//==============================================================================================
	bool MagickImage::Equals(MagickImage^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		return (Magick::operator == (*Value, *other->Value)) ? true : false;
	}
	//==============================================================================================
	void MagickImage::Evaluate(Channels channels, EvaluateOperator evaluateOperator, double value)
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
	//==============================================================================================
	void MagickImage::Evaluate(Channels channels, MagickGeometry^ geometry,
		EvaluateOperator evaluateOperator, double value)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfTrue("geometry", geometry->IsPercentage, "Percentage is not supported.");

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
	//==============================================================================================
	void MagickImage::Extent(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Extent(geometry);
	}
	//==============================================================================================
	void MagickImage::Extent(int width, int height, MagickColor^ backgroundColor)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Extent(geometry, backgroundColor);
	}
	//==============================================================================================
	void MagickImage::Extent(int width, int height, Gravity gravity)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Extent(geometry, gravity);
	}
	//==============================================================================================
	void MagickImage::Extent(int width, int height, Gravity gravity, MagickColor^ backgroundColor)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Extent(geometry, gravity, backgroundColor);
	}
	//==============================================================================================
	void MagickImage::Extent(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Extent(MagickGeometry^ geometry, MagickColor^ backgroundColor)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("backgroundColor", backgroundColor);

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
	//==============================================================================================
	void MagickImage::Extent(MagickGeometry^ geometry, Gravity gravity)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Extent(MagickGeometry^ geometry, Gravity gravity, MagickColor^ backgroundColor)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("backgroundColor", backgroundColor);

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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::FloodFill(int alpha, int x, int y)
	{
		FloodFill(alpha, x, y, false);
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, int x, int y)
	{
		FloodFill(color, x, y, false);
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, int x, int y, MagickColor^ borderColor)
	{
		FloodFill(color, x, y, borderColor, false);
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, MagickGeometry^ geometry)
	{
		FloodFill(color, geometry, false);
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, MagickGeometry^ geometry, MagickColor^ borderColor)
	{
		FloodFill(color, geometry, borderColor, false);
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, int x, int y)
	{
		FloodFill(image, x, y, false);
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, int x, int y, MagickColor^ borderColor)
	{
		FloodFill(image, x, y, borderColor, false);
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, MagickGeometry^ geometry)
	{
		FloodFill(image, geometry, false);
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, MagickGeometry^ geometry, MagickColor^ borderColor)
	{
		FloodFill(image, geometry, borderColor, false);
	}
	//==============================================================================================
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
	//==============================================================================================
	TypeMetric^ MagickImage::FontTypeMetrics(String^ text)
	{
		return FontTypeMetrics(text, false);
	}
	//==============================================================================================
	TypeMetric^ MagickImage::FontTypeMetrics(String^ text, bool ignoreNewLines)
	{
		Throw::IfNullOrEmpty("text", text);

		Magick::TypeMetric* metric = new Magick::TypeMetric();

		try
		{
			std::string fontText;
			Marshaller::Marshal(text, fontText);

			if (ignoreNewLines)
				Value->fontTypeMetrics(fontText, metric);
			else
				Value->fontTypeMetricsMultiline(fontText, metric);

			return gcnew TypeMetric(metric);
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
	//==============================================================================================
	String^ MagickImage::FormatExpression(String^ expression)
	{
		Throw::IfNullOrEmpty("expression", expression);

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
	//==============================================================================================
	void MagickImage::Frame()
	{
		Frame(_DefaultFrameGeometry);
	}
	//==============================================================================================
	void MagickImage::Frame(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Frame(int width, int height)
	{
		Frame(gcnew MagickGeometry(width, height, 6, 6));
	}
	//==============================================================================================
	void MagickImage::Frame(int width, int height, int innerBevel, int outerBevel)
	{
		Frame(gcnew MagickGeometry(width, height, innerBevel, outerBevel));
	}
	//==============================================================================================
	MagickImage^ MagickImage::FromBase64(String^ value)
	{
		array<Byte>^ data = Convert::FromBase64String(value);
		return gcnew MagickImage(data);
	}
	//==============================================================================================
	void MagickImage::Fx(String^ expression)
	{
		Throw::IfNullOrEmpty("expression", expression);

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
	//==============================================================================================
	void MagickImage::Fx(String^ expression, Channels channels)
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
	//==============================================================================================
	double MagickImage::Gamma()
	{
		try
		{
			return Value->gamma();
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return -1;
		}
	}
	//==============================================================================================
	void MagickImage::Gamma(double value)
	{
		Gamma(value, value, value);
	}
	//==============================================================================================
	void MagickImage::Gamma(double gammaRed, double gammaGreen, double gammaBlue)
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::GaussianBlur(double width, double sigma, Channels channels)
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
	//==============================================================================================
	String^ MagickImage::GetArtifact(String^ name)
	{
		Throw::IfNullOrEmpty("name", name);

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
	//==============================================================================================
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
	//==============================================================================================
	String^ MagickImage::GetOption(MagickFormat format, String^ name)
	{
		Throw::IfNullOrEmpty("name", name);

		std::string magick;
		Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, format), magick);
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
	//==============================================================================================
	ColorProfile^ MagickImage::GetColorProfile()
	{
		ColorProfile^ result = CreateProfile<ColorProfile>("icc");

		if (result == nullptr)
			result = CreateProfile<ColorProfile>("icm");

		return result;
	}
	//==============================================================================================
	EightBimProfile^ MagickImage::Get8BimProfile()
	{
		return CreateProfile<EightBimProfile>("8bim");
	}
	//==============================================================================================
	ExifProfile^ MagickImage::GetExifProfile()
	{
		return CreateProfile<ExifProfile>("exif");
	}
	//==============================================================================================
	int MagickImage::GetHashCode()
	{
		String^ signature = Marshaller::Marshal(Value->signature());

		return
			Value->rows().GetHashCode() ^
			Value->columns().GetHashCode() ^
			signature->GetHashCode();
	}
	//==============================================================================================
	IptcProfile^ MagickImage::GetIptcProfile()
	{
		return CreateProfile<IptcProfile>("iptc");
	}
	//==============================================================================================
	ImageProfile^ MagickImage::GetProfile(String^ name)
	{
		return CreateProfile<ImageProfile>(name);
	}
	//==============================================================================================
	PixelCollection^ MagickImage::GetReadOnlyPixels()
	{
		return gcnew PixelCollection(Value, 0, 0, Width, Height);
	}
	//==============================================================================================
	PixelCollection^ MagickImage::GetReadOnlyPixels(int x, int y, int width, int height)
	{
		return gcnew PixelCollection(Value, x, y, width, height);
	}
	//==============================================================================================
	WritablePixelCollection^ MagickImage::GetWritablePixels()
	{
		return gcnew WritablePixelCollection(Value, 0, 0, Width, Height);
	}
	//==============================================================================================
	WritablePixelCollection^ MagickImage::GetWritablePixels(int x, int y, int width, int height)
	{
		return gcnew WritablePixelCollection(Value, x, y, width, height);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::HaldClut(MagickImage^ image)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->haldClut(*image->Value);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	Dictionary<MagickColor^, int>^ MagickImage::Histogram()
	{
		std::list<std::pair<const Magick::Color,size_t>> *colors = new std::list<std::pair<const Magick::Color,size_t>>();

		try
		{
			colorHistogram(colors, *Value);

			Dictionary<MagickColor^, int>^ result = gcnew Dictionary<MagickColor^, int>();
			for (std::list<std::pair<const Magick::Color,size_t>>::iterator iter = colors->begin(), end = colors->end(); iter != end; ++iter)
			{
				result->Add(gcnew MagickColor(iter->first), (int)iter->second);
			}

			return result;
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return nullptr;
		}
		finally
		{
			delete colors;
		}
	}
	//==============================================================================================
	void MagickImage::HoughLine()
	{
		HoughLine(0, 0, 40);
	}
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::InverseFloodFill(int alpha, int x, int y)
	{
		FloodFill(alpha, x, y, true);
	}
	//==============================================================================================
	void MagickImage::InverseFloodFill(MagickColor^ color, int x, int y)
	{
		FloodFill(color, x, y, true);
	}
	//==============================================================================================
	void MagickImage::InverseFloodFill(MagickColor^ color, int x, int y, MagickColor^ borderColor)
	{
		FloodFill(color, x, y, borderColor, true);
	}
	//==============================================================================================
	void MagickImage::InverseFloodFill(MagickColor^ color, MagickGeometry^ geometry)
	{
		FloodFill(color, geometry, true);
	}
	//==============================================================================================
	void MagickImage::InverseFloodFill(MagickColor^ color, MagickGeometry^ geometry, MagickColor^ borderColor)
	{
		FloodFill(color, geometry, borderColor, true);
	}
	//==============================================================================================
	void MagickImage::InverseFloodFill(MagickImage^ image, int x, int y)
	{
		FloodFill(image, x, y, true);
	}
	//==============================================================================================
	void MagickImage::InverseFloodFill(MagickImage^ image, int x, int y, MagickColor^ borderColor)
	{
		FloodFill(image, x, y, borderColor, true);
	}
	//==============================================================================================
	void MagickImage::InverseFloodFill(MagickImage^ image, MagickGeometry^ geometry)
	{
		FloodFill(image, geometry, true);
	}
	//==============================================================================================
	void MagickImage::InverseFloodFill(MagickImage^ image, MagickGeometry^ geometry, MagickColor^ borderColor)
	{
		FloodFill(image, geometry, borderColor, true);
	}
	//==============================================================================================
	void MagickImage::InverseFourierTransform(MagickImage^ image)
	{
		InverseFourierTransform(image, true);
	}
	//==============================================================================================
	void MagickImage::InverseFourierTransform(MagickImage^ image, bool magnitude)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->inverseFourierTransform(*image->Value, magnitude);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::InverseLevelColors(MagickColor^ blackColor, MagickColor^ whiteColor)
	{
		LevelColors(blackColor, whiteColor, false);
	}
	//==============================================================================================
	void MagickImage::InverseLevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, Channels channels)
	{
		LevelColors(blackColor, whiteColor, channels, false);
	}
	//==============================================================================================
	void MagickImage::InverseOpaque(MagickColor^ target, MagickColor^ fill)
	{
		Opaque(target, fill, true);
	}
	//==============================================================================================
	void MagickImage::Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint)
	{
		Level(blackPoint, whitePoint, 1.0);
	}
	//==============================================================================================
	void MagickImage::Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, Channels channels)
	{
		Level(blackPoint, whitePoint, 1.0, channels);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, double midpoint, Channels channels)
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
	//==============================================================================================
	void MagickImage::LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor)
	{
		LevelColors(blackColor, whiteColor, true);
	}
	///=============================================================================================
	void MagickImage::LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, Channels channels)
	{
		LevelColors(blackColor, whiteColor, channels, true);
	}
	//==============================================================================================
	void MagickImage::LinearStretch(Percentage blackPoint, Percentage whitePoint)
	{
		Throw::IfNegative("blackPoint", blackPoint);
		Throw::IfNegative("whitePoint", whitePoint);

		try
		{
			Value->linearStretch(blackPoint.ToQuantum(), whitePoint.ToQuantum());
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::LiquidRescale(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Lower(int size)
	{
		RaiseOrLower(size, false);
	}
	//==============================================================================================
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
	//==============================================================================================
	MagickErrorInfo^ MagickImage::Map(MagickImage^ image)
	{
		return Map(image, gcnew QuantizeSettings());
	}
	//==============================================================================================
	MagickErrorInfo^ MagickImage::Map(MagickImage^ image, QuantizeSettings^ settings)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("settings", settings);

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

		return settings->MeasureErrors ? gcnew MagickErrorInfo(Value) : nullptr;
	}
	//==============================================================================================
	void MagickImage::MedianFilter()
	{
		MedianFilter(0.0);
	}
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Modulate(Percentage brightness, Percentage saturation, Percentage hue)
	{
		Throw::IfNegative("brightness", brightness);
		Throw::IfNegative("saturation", saturation);
		Throw::IfNegative("hue", hue);

		try
		{
			Value->modulate((double)brightness, (double)saturation, (double)hue);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	Moments^ MagickImage::Moments()
	{
		try
		{
			Magick::ImageMoments moments = Value->moments();
			return gcnew ImageMagick::Moments(&moments);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return nullptr;
		}
	}
	//==============================================================================================
	void MagickImage::Morphology(MorphologyMethod method, Kernel kernel)
	{
		try
		{
			Value->morphology((Magick::MorphologyMethod)method, (Magick::KernelInfoType)kernel, "");
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Morphology(MorphologyMethod method, Kernel kernel, Channels channels)
	{
		try
		{
			Value->morphologyChannel((Magick::ChannelType)channels, (Magick::MorphologyMethod)method,
				(Magick::KernelInfoType)kernel, "");
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Morphology(MorphologyMethod method, Kernel kernel, Channels channels, int iterations)
	{
		try
		{
			Value->morphologyChannel((Magick::ChannelType)channels, (Magick::MorphologyMethod)method,
				(Magick::KernelInfoType)kernel, "", iterations);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Morphology(MorphologyMethod method, Kernel kernel, int iterations)
	{
		try
		{
			Value->morphology((Magick::MorphologyMethod)method, (Magick::KernelInfoType)kernel, "", iterations);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Morphology(MorphologyMethod method, Kernel kernel, String^ arguments, Channels channels)
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
	//==============================================================================================
	void MagickImage::Morphology(MorphologyMethod method, Kernel kernel, String^ arguments, Channels channels, int iterations)
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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Morphology(MorphologyMethod method, String^ userKernel, Channels channels)
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
	//==============================================================================================
	void MagickImage::Morphology(MorphologyMethod method, String^ userKernel, Channels channels, int iterations)
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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Negate()
	{
		Negate(false);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Negate(Channels channels)
	{
		Negate(channels, false);
	}
	//==============================================================================================
	void MagickImage::Negate(Channels channels, bool onlyGrayscale)
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::OilPaint()
	{
		OilPaint(3.0);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Opaque(MagickColor^ target, MagickColor^ fill)
	{
		Opaque(target, fill, false);
	}
	//==============================================================================================
	void MagickImage::OrderedDither(String^ thresholdMap)
	{
		Throw::IfNullOrEmpty("thresholdMap", thresholdMap);
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
	//==============================================================================================
	void MagickImage::OrderedDither(String^ thresholdMap, Channels channels)
	{
		Throw::IfNullOrEmpty("thresholdMap", thresholdMap);
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
	//==============================================================================================
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
	///=============================================================================================
	void MagickImage::Perceptible(double epsilon, Channels channels)
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
	//==============================================================================================
	PerceptualHash^ MagickImage::PerceptualHash()
	{
		try
		{
			Magick::ImagePerceptualHash perceptualHash = Value->perceptualHash();
			if (perceptualHash.isValid())
				return gcnew ImageMagick::PerceptualHash(&perceptualHash);
			else
				return nullptr;
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return nullptr;
		}
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Ping(array<Byte>^ data)
	{
		MagickReadSettings^ readSettings = gcnew MagickReadSettings();
		readSettings->Ping = true;
		return Read(data, readSettings);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Ping(String^ fileName)
	{
		MagickReadSettings^ readSettings = gcnew MagickReadSettings();
		readSettings->Ping = true;
		return Read(fileName, readSettings);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Ping(Stream^ stream)
	{
		MagickReadSettings^ readSettings = gcnew MagickReadSettings();
		readSettings->Ping = true;
		return Read(stream, readSettings);
	}
	//==============================================================================================
	void MagickImage::Polaroid(String^ caption, double angle, PixelInterpolateMethod method)
	{
		Throw::IfNull("caption", caption);

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
	//==============================================================================================
	void MagickImage::Posterize(int levels)
	{
		Posterize(levels, DitherMethod::No);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Posterize(int levels, DitherMethod method, Channels channels)
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
	//==============================================================================================
	void MagickImage::Posterize(int levels, Channels channels)
	{
		Posterize(levels, DitherMethod::No, channels);
	}
	//==============================================================================================
	void MagickImage::PreserveColorType()
	{
		ColorType = ColorType;
	}
	//==============================================================================================
	MagickErrorInfo^ MagickImage::Quantize(QuantizeSettings^ settings)
	{
		Throw::IfNull("settings", settings);

		try
		{
			Apply(settings);
			Value->quantize(settings->MeasureErrors);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}

		return settings->MeasureErrors ? gcnew MagickErrorInfo(Value) : nullptr;
	}
	//==============================================================================================
	void MagickImage::Raise(int size)
	{
		RaiseOrLower(size, true);
	}
	//==============================================================================================
	void MagickImage::RandomThreshold(Magick::Quantum low, Magick::Quantum high)
	{
		RandomThreshold(low, high, false);
	}
	//==============================================================================================
	void MagickImage::RandomThreshold(Magick::Quantum low, Magick::Quantum high, Channels channels)
	{
		RandomThreshold(low, high, channels, false);
	}
	//==============================================================================================
	void MagickImage::RandomThreshold(Percentage percentageLow, Percentage percentageHigh)
	{
		Throw::IfNegative("percentageLow", percentageLow);
		Throw::IfNegative("percentageHigh", percentageHigh);

		RandomThreshold((Magick::Quantum)percentageLow, (Magick::Quantum)percentageHigh, true);
	}
	//==============================================================================================
	void MagickImage::RandomThreshold(Percentage percentageLow, Percentage percentageHigh, Channels channels)
	{
		Throw::IfNegative("percentageLow", percentageLow);
		Throw::IfNegative("percentageHigh", percentageHigh);

		RandomThreshold((Magick::Quantum)percentageLow, (Magick::Quantum)percentageHigh, channels, true);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(array<Byte>^ data)
	{
		return Read(data, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(array<Byte>^ data, MagickReadSettings^ readSettings)
	{
		return HandleReadException(MagickReader::Read(Value, data, readSettings));
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(Bitmap^ bitmap)
	{
		Throw::IfNull("bitmap", bitmap);

		MemoryStream^ memStream = gcnew MemoryStream();
		try
		{
			if (IsSupportedImageFormat(bitmap->RawFormat))
				bitmap->Save(memStream, bitmap->RawFormat);
			else
				bitmap->Save(memStream, ImageFormat::Bmp);

			memStream->Position = 0;
			return Read(memStream);
		}
		finally
		{
			delete memStream;
		}
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(FileInfo^ file)
	{
		Throw::IfNull("file", file);
		return Read(file->FullName);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(FileInfo^ file, MagickReadSettings^ readSettings)
	{
		Throw::IfNull("file", file);
		return Read(file->FullName, readSettings);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(String^ fileName)
	{
		return Read(fileName, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(String^ fileName, MagickReadSettings^ readSettings)
	{
		return HandleReadException(MagickReader::Read(Value, fileName, readSettings));
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(Stream^ stream)
	{
		return Read(stream, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(Stream^ stream, MagickReadSettings^ readSettings)
	{
		return HandleReadException(MagickReader::Read(Value, stream, readSettings));
	}
	//==============================================================================================
	void MagickImage::ReduceNoise()
	{
		try
		{
			Value->reduceNoise();
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
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
	void MagickImage::RemoveProfile(String^ name)
	{
		Magick::Blob blob;
		SetProfile(name, blob);
	}
	//==============================================================================================
	void MagickImage::RePage()
	{
		Page = gcnew MagickGeometry(0, 0);
	}
	//==============================================================================================
	void MagickImage::Resample(int resolutionX, int resolutionY)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(resolutionX, resolutionY);
		Resize(geometry);
	}
	//==============================================================================================
	void MagickImage::Resample(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

		const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

		try
		{
			Value->resample(*magickGeometry);
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
	//==============================================================================================
	void MagickImage::Resize(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Resize(geometry);
	}
	//==============================================================================================
	void MagickImage::Resize(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Resize(Percentage percentage)
	{
		Resize(percentage, percentage);
	}
	//==============================================================================================
	void MagickImage::Resize(Percentage percentageWidth, Percentage percentageHeight)
	{
		Throw::IfNegative("percentageWidth", percentageWidth);
		Throw::IfNegative("percentageHeight", percentageHeight);

		MagickGeometry^ geometry = gcnew MagickGeometry(percentageWidth, percentageHeight);
		Resize(geometry);
	}
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::RotationalBlur(double angle, Channels channels)
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
	//==============================================================================================
	void MagickImage::Sample(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Sample(geometry);
	}
	//==============================================================================================
	void MagickImage::Sample(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Sample(Percentage percentage)
	{
		Sample(percentage, percentage);
	}
	//==============================================================================================
	void MagickImage::Sample(Percentage percentageWidth, Percentage percentageHeight)
	{
		Throw::IfNegative("percentageWidth", percentageWidth);
		Throw::IfNegative("percentageHeight", percentageHeight);

		MagickGeometry^ geometry = gcnew MagickGeometry(percentageWidth, percentageHeight);
		Sample(geometry);
	}
	//==============================================================================================
	void MagickImage::Scale(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Scale(geometry);
	}
	//==============================================================================================
	void MagickImage::Scale(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Scale(Percentage percentage)
	{
		Scale(percentage, percentage);
	}
	//==============================================================================================
	void MagickImage::Scale(Percentage percentageWidth, Percentage percentageHeight)
	{
		Throw::IfNegative("percentageWidth", percentageWidth);
		Throw::IfNegative("percentageHeight", percentageHeight);

		MagickGeometry^ geometry = gcnew MagickGeometry(percentageWidth, percentageHeight);
		Scale(geometry);
	}
	//==============================================================================================
	void MagickImage::Segment()
	{
		Segment(ImageMagick::ColorSpace::Undefined, 1.0, 1.5);
	}
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::SelectiveBlur(double radius, double sigma, double threshold, Channels channels)
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
	//==============================================================================================
	IEnumerable<MagickImage^>^ MagickImage::Separate()
	{
		return Separate(Channels::All);
	}
	//==============================================================================================
	IEnumerable<MagickImage^>^ MagickImage::Separate(Channels channels)
	{
		std::list<Magick::Image> *images = new std::list<Magick::Image>();

		try
		{
			separateImages(images, *Value, (Magick::ChannelType)channels);

			return MagickImageCollection::CreateList(images);
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
	//==============================================================================================
	void MagickImage::SepiaTone()
	{
		SepiaTone(Percentage(80));
	}
	//==============================================================================================
	void MagickImage::SepiaTone(Percentage threshold)
	{
		try
		{
			return Value->sepiaTone(threshold.ToQuantum());
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::SetArtifact(String^ name, String^ value)
	{
		Throw::IfNullOrEmpty("name", name);
		Throw::IfNull("value", value);

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
	//==============================================================================================
	void MagickImage::SetAttenuate(double attenuate)
	{
		return Value->attenuate(attenuate);
	}
	//==============================================================================================
	void MagickImage::SetAttribute(String^ name, String^ value)
	{
		Throw::IfNullOrEmpty("name", name);
		Throw::IfNull("value", value);

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
	//==============================================================================================
	void MagickImage::SetDefine(MagickFormat format, String^ name, bool flag)
	{
		Throw::IfNullOrEmpty("name", name);

		std::string magick;
		Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, GetCoderFormat(format)), magick);
		std::string optionName;
		Marshaller::Marshal(name, optionName);

		try
		{
			Value->defineSet(magick, optionName, flag);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::SetDefine(MagickFormat format, String^ name, String^ value)
	{
		Throw::IfNullOrEmpty("name", name);
		Throw::IfNull("value", value);

		std::string magick;
		Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, GetCoderFormat(format)), magick);
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
	//==============================================================================================
	void MagickImage::SetHighlightColor(MagickColor^ color)
	{
		Throw::IfNull("color", color);

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
	{
		Throw::IfNull("color", color);

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
	void MagickImage::Shade()
	{
		Shade(30, 30, false);
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
	//==============================================================================================
	void MagickImage::Shadow()
	{
		return Shadow(5, 5, 0.5, 0.8);
	}
	//==============================================================================================
	void MagickImage::Shadow(MagickColor^ color)
	{
		return Shadow(5, 5, 0.5, 0.8, color);
	}
	///=============================================================================================
	void MagickImage::Shadow(int x, int y, double sigma, Percentage alpha)
	{
		try
		{
			Value->shadow((double)alpha, sigma, x, y);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	///=============================================================================================
	void MagickImage::Shadow(int x, int y, double sigma, Percentage alpha, MagickColor^ color)
	{
		Throw::IfNull("color", color);

		MagickImageCollection^ images = gcnew MagickImageCollection();
		const Magick::Color* backgroundColor = color->CreateColor();

		try
		{
			MagickImage^ clone = Clone();
			clone->Value->backgroundColor(*backgroundColor);
			clone->Value->shadow((double)alpha, sigma, x, y);
			clone->Value->backgroundColor(Magick::Color());

			images->Add(clone);
			images->Add(Clone());
			images->Merge(Value, LayerMethod::Mosaic);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
		finally
		{
			delete images;
			delete backgroundColor;
		}
	}
	//==============================================================================================
	void MagickImage::Sharpen()
	{
		Sharpen(0.0, 1.0);
	}
	//==============================================================================================
	void MagickImage::Sharpen(Channels channels)
	{
		Sharpen(0.0, 1.0, channels);
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
	void MagickImage::Sharpen(double radius, double sigma, Channels channels)
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
	void MagickImage::SigmoidalContrast(bool sharpen, double contrast)
	{
		SigmoidalContrast(sharpen, contrast, Quantum::Max / 2.0);
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
	void MagickImage::Sketch()
	{
		try
		{
			Value->sketch();
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
	void MagickImage::Solarize()
	{
		Solarize(50.0);
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
	//==============================================================================================
	void MagickImage::SparseColor(SparseColorMethod method, IEnumerable<SparseColorArg^>^ args)
	{
		SparseColor(Channels::Default, method, args);
	}
	//==============================================================================================
	void MagickImage::SparseColor(Channels channels, SparseColorMethod method, IEnumerable<SparseColorArg^>^ args)
	{
		Throw::IfNull("args", args);

		bool hasRed = ((int)channels & (int)Channels::Red) != 0;
		bool hasGreen = ((int)channels & (int)Channels::Green) != 0;
		bool hasBlue = ((int)channels & (int)Channels::Blue) != 0;
		bool hasAlpha = ((int)channels & (int)Channels::Alpha) != 0;

		Throw::IfTrue("channels", !hasRed && !hasGreen && !hasBlue && !hasAlpha, "Invalid channels specified.");

		List<double>^ argsList = gcnew List<double>();

		IEnumerator<ImageMagick::SparseColorArg^>^ enumerator = args->GetEnumerator();
		while(enumerator->MoveNext())
		{
			ImageMagick::SparseColorArg^ arg = enumerator->Current;

			argsList->Add(arg->X);
			argsList->Add(arg->Y);
			if (hasRed)
				argsList->Add(Quantum::Scale(arg->Color->R));
			if (hasGreen)
				argsList->Add(Quantum::Scale(arg->Color->G));
			if (hasBlue)
				argsList->Add(Quantum::Scale(arg->Color->B));
			if (hasAlpha)
				argsList->Add(Quantum::Scale(arg->Color->A));
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
	void MagickImage::Spread()
	{
		Spread(3);
	}
	//==============================================================================================
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
	//==============================================================================================
	Statistics^ MagickImage::Statistics()
	{
		try
		{
			Magick::ImageStatistics statistics = Value->statistics();
			return gcnew ImageMagick::Statistics(&statistics);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
			return nullptr;
		}
	}
	//==============================================================================================
	void MagickImage::Stegano(MagickImage^ watermark)
	{
		Throw::IfNull("watermark", watermark);

		try
		{
			Value->stegano(*watermark->Value);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Stereo(MagickImage^ rightImage)
	{
		Throw::IfNull("rightImage", rightImage);

		try
		{
			Value->stereo(*rightImage->Value);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	MagickSearchResult^ MagickImage::SubImageSearch(MagickImage^ image)
	{
		return SubImageSearch(image, ErrorMetric::RootMeanSquared, -1);
	}
	//==============================================================================================
	MagickSearchResult^ MagickImage::SubImageSearch(MagickImage^ image, ErrorMetric metric)
	{
		return SubImageSearch(image, metric, -1);
	}
	//==============================================================================================
	MagickSearchResult^ MagickImage::SubImageSearch(MagickImage^ image, ErrorMetric metric, double similarityThreshold)
	{
		Throw::IfNull("image", image);

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
	//==============================================================================================
	void MagickImage::Texture(MagickImage^ image)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->texture(*image->Value);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Threshold(Percentage percentage)
	{
		try
		{
			Value->threshold(percentage.ToQuantum());
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Thumbnail(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Thumbnail(geometry);
	}
	//==============================================================================================
	void MagickImage::Thumbnail(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Thumbnail(Percentage percentage)
	{
		Thumbnail(percentage, percentage);
	}
	//==============================================================================================
	void MagickImage::Thumbnail(Percentage percentageWidth, Percentage percentageHeight)
	{
		Throw::IfNegative("percentageWidth", percentageWidth);
		Throw::IfNegative("percentageHeight", percentageHeight);

		MagickGeometry^ geometry = gcnew MagickGeometry(percentageWidth, percentageHeight);
		Thumbnail(geometry);
	}
	//==============================================================================================
	void MagickImage::Tile(MagickImage^ image, CompositeOperator compose)
	{
		Tile(image, compose, nullptr);
	}
	//==============================================================================================
	void MagickImage::Tile(MagickImage^ image, CompositeOperator compose, String^ args)
	{
		Throw::IfNull("image", image);

		for (int y=0; y < Height; y+= image->Height)
		{
			for (int x=0; x < Width; x += image->Width)
			{
				Composite(image, x, y, compose, args);
			}
		}
	}
	//==============================================================================================
	void MagickImage::Tint(String^ opacity)
	{
		Throw::IfNullOrEmpty("opacity", opacity);

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
	//==============================================================================================
	String^ MagickImage::ToBase64()
	{
		return Convert::ToBase64String(ToByteArray());
	}
	//==============================================================================================
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
	Bitmap^ MagickImage::ToBitmap(ImageFormat^ imageFormat)
	{
		SetFormat(imageFormat);

		MemoryStream^ memStream = gcnew MemoryStream();
		Write(memStream);
		memStream->Position = 0;
		// Do not dispose the memStream, the bitmap owns it.
		return gcnew Bitmap(memStream);
	}
	//==============================================================================================
	array<Byte>^ MagickImage::ToByteArray()
	{
		Magick::Blob blob;
		HandleException(MagickWriter::Write(this->Value, &blob));
		return Marshaller::Marshal(&blob);
	}
	//==============================================================================================
	array<Byte>^ MagickImage::ToByteArray(MagickFormat format)
	{
		Format = format;
		return ToByteArray();
	}
	//==============================================================================================
	String^ MagickImage::ToString()
	{
		return String::Format(CultureInfo::InvariantCulture, "{0} {1}x{2} {3}-bit {4} {5}",
			Format, Width, Height, Depth, ColorSpace, FormatedFileSize());
	}
	//==============================================================================================
	void MagickImage::Transform(MagickGeometry^ imageGeometry)
	{
		Throw::IfNull("imageGeometry", imageGeometry);

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
	//==============================================================================================
	void MagickImage::Transform(MagickGeometry^ imageGeometry, MagickGeometry^ cropGeometry)
	{
		Throw::IfNull("imageGeometry", imageGeometry);
		Throw::IfNull("cropGeometry", cropGeometry);

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
	//==============================================================================================
	void MagickImage::TransformOrigin(double x, double y)
	{
		Value->transformOrigin(x, y);
	}
	//==============================================================================================
	void MagickImage::TransformRotation(double angle)
	{
		Value->transformRotation(angle);
	}
	//==============================================================================================
	void MagickImage::TransformReset()
	{
		Value->transformReset();
	}
	//==============================================================================================
	void MagickImage::TransformScale(double scaleX, double scaleY)
	{
		Value->transformScale(scaleX, scaleY);
	}
	//==============================================================================================
	void MagickImage::TransformSkewX(double skewX)
	{
		Value->transformSkewX(skewX);
	}
	//==============================================================================================
	void MagickImage::TransformSkewY(double skewY)
	{
		Value->transformSkewY(skewY);
	}
	//==============================================================================================
	void MagickImage::Transparent(MagickColor^ color)
	{
		Throw::IfNull("color", color);

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
	//==============================================================================================
	void MagickImage::TransparentChroma(MagickColor^ colorLow, MagickColor^ colorHigh)
	{
		Throw::IfNull("colorLow", colorLow);
		Throw::IfNull("colorHigh", colorHigh);

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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
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
	//==============================================================================================
	MagickImage^ MagickImage::UniqueColors()
	{
		Magick::Image uniqueColors = Value->uniqueColors();
		if (!uniqueColors.isValid())
			return nullptr;

		return gcnew MagickImage(uniqueColors);
	}
	//==============================================================================================
	void MagickImage::Unsharpmask(double radius, double sigma)
	{
		Unsharpmask(radius, sigma, 1.0, 0.05);
	}
	//==============================================================================================
	void MagickImage::Unsharpmask(double radius, double sigma, Channels channels)
	{
		Unsharpmask(radius, sigma, 1.0, 0.05, channels);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Unsharpmask(double radius, double sigma, double amount, double threshold, Channels channels)
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
	//==============================================================================================
	void MagickImage::Vignette()
	{
		try
		{
			Value->vignette();
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::Wave()
	{
		Wave(25.0, 150.0);
	}
	//==============================================================================================
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
	//==============================================================================================
	void MagickImage::WhiteThreshold(Percentage threshold)
	{
		Throw::IfNegative("threshold", threshold);

		try
		{
			std::string threshold_;
			Marshaller::Marshal(threshold.ToString(), threshold_);
			Value->whiteThreshold(threshold_);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::WhiteThreshold(Percentage threshold, Channels channels)
	{
		Throw::IfNegative("threshold", threshold);

		try
		{
			std::string threshold_;
			Marshaller::Marshal(threshold.ToString(), threshold_);
			Value->whiteThresholdChannel((Magick::ChannelType)channels, threshold_);
		}
		catch(Magick::Exception& exception)
		{
			HandleException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Write(FileInfo^ file)
	{
		Throw::IfNull("file", file);
		Write(file->FullName);
		file->Refresh();
	}
	//==============================================================================================
	void MagickImage::Write(Stream^ stream)
	{
		HandleException(MagickWriter::Write(Value, stream));
	}
	//==============================================================================================
	void MagickImage::Write(Stream^ stream, MagickFormat format)
	{
		Format=format;
		Write(stream);
	}
	//==============================================================================================
	void MagickImage::Write(String^ fileName)
	{
		HandleException(MagickWriter::Write(Value, fileName));
	}
	//==============================================================================================
	void MagickImage::Zoom(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Zoom(geometry);
	}
	//==============================================================================================
	void MagickImage::Zoom(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

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
	//==============================================================================================
	void MagickImage::Zoom(Percentage percentage)
	{
		Zoom(percentage, percentage);
	}
	//==============================================================================================
	void MagickImage::Zoom(Percentage percentageWidth, Percentage percentageHeight)
	{
		Throw::IfNegative("percentageWidth", percentageWidth);
		Throw::IfNegative("percentageHeight", percentageHeight);

		MagickGeometry^ geometry = gcnew MagickGeometry(percentageWidth, percentageHeight);
		Zoom(geometry);
	}
	//==============================================================================================
#if !(NET20)
	//==============================================================================================
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
	//==============================================================================================
#endif
	//==============================================================================================
}