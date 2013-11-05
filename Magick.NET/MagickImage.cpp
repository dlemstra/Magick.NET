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
#include "Stdafx.h"
#include "MagickImage.h"
#include "Helpers\FileHelper.h"
#include "MagickImageCollection.h"

using namespace System::Globalization;

namespace ImageMagick
{
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
			return result;
		}
		catch(Magick::ErrorCoder)
		{
			return nullptr;
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
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
	void MagickImage::RaiseOrLower(int size, bool raiseFlag)
	{
		Magick::Geometry* geometry = new Magick::Geometry(size, size);

		try
		{
			Value->raise(*geometry, raiseFlag);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			Value->randomThresholdChannel(*geometry, (MagickCore::ChannelType)channels);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	MagickImage::MagickImage(const Magick::Image& image)
	{
		Value = new Magick::Image(image);
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
	MagickImage::MagickImage(MagickColor^ color, int width, int height)
	{
		Throw::IfNull("color", color);

		Magick::Geometry* geometry = new Magick::Geometry(width, height);
		const Magick::Color* background = color->CreateColor();
		Value = new Magick::Image(*geometry, *background);
		Value->backgroundColor(*background);
		delete geometry;
		delete background;
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
			throw MagickException::Create(exception);
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
		return Value->classType((MagickCore::ClassType)value);
	}
	//==============================================================================================
	MagickImage^ MagickImage::ClipMask::get()
	{
		Magick::Image clipMask = Value->clipMask();
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
			Value->clipMask(*image);
			delete image;
		}
		else
		{
			Value->clipMask(*value->Value);
		}
	}
	//==============================================================================================
	double MagickImage::ColorFuzz::get()
	{
		return Value->colorFuzz();
	}
	//==============================================================================================
	void MagickImage::ColorFuzz::set(double value)
	{
		Value->colorFuzz(value);
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
			throw MagickException::Create(exception);
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
		return Value->colorSpace((MagickCore::ColorspaceType)value);
	}
	//==============================================================================================
	ColorType MagickImage::ColorType::get()
	{
		return (ImageMagick::ColorType)Value->type();
	}
	//==============================================================================================
	void MagickImage::ColorType::set(ImageMagick::ColorType value)
	{
		Value->type((MagickCore::ImageType)value);
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
		Value->compressType((MagickCore::CompressionType)value);
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
		Value->endian((MagickCore::EndianType)value);
	}
	//==============================================================================================
	int MagickImage::FileSize::get()
	{
		return Value->fileSize();
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
		Value->gifDisposeMethod((int)value);
	}
	//==============================================================================================
	bool MagickImage::HasAlpha::get()
	{
		return Value->matte();
	}
	//==============================================================================================
	void MagickImage::HasAlpha::set(bool value)
	{
		Value->matte(value);
	}
	//==============================================================================================
	int MagickImage::Height::get()
	{
		return Convert::ToInt32(Value->size().height());
	}
	//==============================================================================================
	bool MagickImage::IsMonochrome::get()
	{
		return Value->monochrome();
	}
	//==============================================================================================
	void MagickImage::IsMonochrome::set(bool value)
	{
		return Value->monochrome(value);
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
	MagickColor^ MagickImage::MatteColor::get()
	{
		return gcnew MagickColor(Value->matteColor());
	}
	//==============================================================================================
	void MagickImage::MatteColor::set(MagickColor^ value)
	{
		const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
		Value->matteColor(*color);
		delete color;
	}
	//==============================================================================================
	int MagickImage::ModulusDepth::get()
	{
		return Convert::ToInt32(Value->modulusDepth());
	}
	//==============================================================================================
	void MagickImage::ModulusDepth::set(int value)
	{
		Value->modulusDepth(value);
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
			delete[] profileNames;
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
	int MagickImage::QuantizeColors::get()
	{
		return Convert::ToInt32(Value->quantizeColors());
	}
	//==============================================================================================
	void MagickImage::QuantizeColors::set(int value)
	{
		Value->quantizeColors(value);
	}
	//==============================================================================================
	ImageMagick::ColorSpace MagickImage::QuantizeColorSpace::get()
	{
		return (ImageMagick::ColorSpace)Value->quantizeColorSpace();
	}
	//==============================================================================================
	void MagickImage::QuantizeColorSpace::set(ImageMagick::ColorSpace value)
	{
		return Value->quantizeColorSpace((MagickCore::ColorspaceType)value);
	}
	//==============================================================================================
	bool MagickImage::QuantizeDither::get()
	{
		return Value->quantizeDither();
	}
	//==============================================================================================
	void MagickImage::QuantizeDither::set(bool value)
	{
		Value->quantizeDither(value);
	}
	//==============================================================================================
	int MagickImage::QuantizeTreeDepth::get()
	{
		return Convert::ToInt32(Value->quantizeTreeDepth());
	}
	//==============================================================================================
	void MagickImage::QuantizeTreeDepth::set(int value)
	{
		Value->quantizeTreeDepth(value);
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
		return Value->renderingIntent((MagickCore::RenderingIntent)value);
	}
	//==============================================================================================
	Resolution MagickImage::ResolutionUnits::get()
	{
		return (Resolution)Value->resolutionUnits();
	}
	//==============================================================================================
	void MagickImage::ResolutionUnits::set(Resolution value)
	{
		return Value->resolutionUnits((MagickCore::ResolutionType)value);
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
		Value->strokeLineCap((MagickCore::LineCap)value);
	}
	//==============================================================================================
	LineJoin MagickImage::StrokeLineJoin::get()
	{
		return (LineJoin)Value->strokeLineJoin();
	}
	//==============================================================================================
	void MagickImage::StrokeLineJoin::set(LineJoin value)
	{
		Value->strokeLineJoin((MagickCore::LineJoin)value);
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
	}
	//==============================================================================================
	String^ MagickImage::TileName::get()
	{
		return Marshaller::Marshal(Value->tileName());
	}
	//==============================================================================================
	void MagickImage::TileName::set(String^ value)
	{
		std::string tileName;
		Value->tileName(Marshaller::Marshal(value, tileName));
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
		Value->virtualPixelMethod((MagickCore::VirtualPixelMethod)value);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::AdaptiveSharpen(double radius, double sigma, Channels channels)
	{
		try
		{
			Value->adaptiveSharpenChannel((MagickCore::ChannelType)channels, radius, sigma);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::AddProfile(ImageProfile^ profile)
	{
		Throw::IfNull("profile", profile);

		try
		{
			Magick::Blob blob;
			Marshaller::Marshal(profile->Data, &blob);

			std::string profileName;
			Marshaller::Marshal(profile->Name, profileName);

			Value->profile(profileName, blob);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Alpha(AlphaOption option)
	{
		try
		{
			Value->alphaChannel((MagickCore::AlphaChannelType)option);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::AutoGamma(Channels channels)
	{
		try
		{
			Value->autoGammaChannel((MagickCore::ChannelType)channels);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::AutoLevel(Channels channels)
	{
		try
		{
			Value->autoLevelChannel((MagickCore::ChannelType)channels);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			return Convert::ToInt32(Value->channelDepth((MagickCore::ChannelType)channels));
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::BitDepth(Channels channels, int value)
	{
		try
		{
			Value->channelDepth((MagickCore::ChannelType)channels, value);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
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
			throw MagickException::Create(exception);
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
			Value->blackThresholdChannel((MagickCore::ChannelType)channels, threshold_);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
		Magick::Geometry* geometry = new Magick::Geometry(width, height);

		try
		{
			Value->border(*geometry);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			Value->brightnessContrast(brightness.ToInt32(), contrast.ToInt32());
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::BrightnessContrast(Percentage brightness, Percentage contrast, Channels channels)
	{
		try
		{
			Value->brightnessContrastChannel((MagickCore::ChannelType)channels, brightness.ToInt32(), contrast.ToInt32());
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
		delete geometry;
	}
	//==============================================================================================
	void MagickImage::ChopVertical(int offset, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(0, offset, 0, height);
		Chop(geometry);
		delete geometry;
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Clamp(Channels channels)
	{
		try
		{
			Value->clampChannel((MagickCore::ChannelType)channels);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	MagickImage^ MagickImage::Clone()
	{
		return gcnew MagickImage(*Value);
	}
	//==============================================================================================
	void MagickImage::Clut(MagickImage^ image)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->clut(*image->Value);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Clut(MagickImage^ image, Channels channels)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->clutChannel((MagickCore::ChannelType)channels, *image->Value);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}

		return gcnew MagickErrorInfo(Value);
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
			Value->composite(*(image->Value), x, y, (MagickCore::CompositeOperator)compose);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			Value->composite(*(image->Value), *magickGeometry, (MagickCore::CompositeOperator)compose);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			Value->composite(*(image->Value), (MagickCore::GravityType)gravity, (MagickCore::CompositeOperator)compose);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
		delete geometry;
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			Value->distort((MagickCore::DistortImageMethod)method, arguments->Length, distortArguments, bestfit);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
	void MagickImage::Extent(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Resize(geometry);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			Value->extent(*magickGeometry, (MagickCore::GravityType)gravity);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			Value->extent(*magickGeometry, *color, (MagickCore::GravityType)gravity);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(int alpha, int x, int y, PaintMethod paintMethod)
	{
		try
		{
			Value->floodFillOpacity(x, y, alpha, (MagickCore::PaintMethod)paintMethod);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, int x, int y)
	{
		Throw::IfNull("color", color);

		const Magick::Color* fillColor = color->CreateColor();

		try
		{
			Value->floodFillColor(x, y, *fillColor);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete fillColor;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, int x, int y, MagickColor^ borderColor)
	{
		Throw::IfNull("color", color);
		Throw::IfNull("borderColor", borderColor);

		const Magick::Color* fillColor = color->CreateColor();
		const Magick::Color* fillBorderColor = borderColor->CreateColor();

		try
		{
			Value->floodFillColor(x, y, *fillColor, *fillBorderColor);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete fillColor;
			delete fillBorderColor;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, MagickGeometry^ geometry)
	{
		Throw::IfNull("color", color);
		Throw::IfNull("geometry", geometry);

		const Magick::Color* fillColor = color->CreateColor();
		const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

		try
		{
			Value->floodFillColor(*magickGeometry, *fillColor);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete fillColor;
			delete magickGeometry;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickColor^ color, MagickGeometry^ geometry, MagickColor^ borderColor)
	{
		Throw::IfNull("color", color);
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("borderColor", borderColor);

		const Magick::Color* fillColor = color->CreateColor();
		const Magick::Color* fillBorderColor = borderColor->CreateColor();
		const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

		try
		{
			Value->floodFillColor(*magickGeometry, *fillColor, *fillBorderColor);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete fillColor;
			delete fillBorderColor;
			delete magickGeometry;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, int x, int y)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->floodFillTexture(x, y, *image->Value);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, int x, int y, MagickColor^ borderColor)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("borderColor", borderColor);

		const Magick::Color* fillBorderColor = borderColor->CreateColor();

		try
		{
			Value->floodFillTexture(x, y, *image->Value, *fillBorderColor);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete fillBorderColor;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, MagickGeometry^ geometry)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("geometry", geometry);

		const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

		try
		{
			Value->floodFillTexture(*magickGeometry, *image->Value);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete magickGeometry;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFill(MagickImage^ image, MagickGeometry^ geometry, MagickColor^ borderColor)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("borderColor", borderColor);

		const Magick::Color* fillBorderColor = borderColor->CreateColor();
		const Magick::Geometry* magickGeometry = geometry->CreateGeometry();

		try
		{
			Value->floodFillTexture(*magickGeometry, *image->Value, *fillBorderColor);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete fillBorderColor;
			delete magickGeometry;
		}
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	TypeMetric^ MagickImage::FontTypeMetrics(String^ text)
	{
		Throw::IfNullOrEmpty("text", text);

		Magick::TypeMetric* metric = new Magick::TypeMetric();

		try
		{
			std::string fontText;
			Marshaller::Marshal(text, fontText);
			Value->fontTypeMetrics(fontText, metric);
			return gcnew TypeMetric(metric);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete metric;
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			Value->fx(fxExpression, (MagickCore::ChannelType)channels);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::GaussianBlur(double width, double sigma, Channels channels)
	{
		try
		{
			Value->gaussianBlurChannel((MagickCore::ChannelType)channels, width, sigma);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	ColorProfile^ MagickImage::GetColorProfile()
	{
		ColorProfile^ result = CreateProfile<ColorProfile>("icm");

		if (result == nullptr)
			result = CreateProfile<ColorProfile>("icc");

		return result;
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
	void MagickImage::HaldClut(MagickImage^ image)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->haldClut(*image->Value);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
		finally
		{
			delete[] colors;
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
			throw MagickException::Create(exception);
		}
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
			throw MagickException::Create(exception);
		}
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, double midpoint, Channels channels)
	{
		try
		{
			Value->levelChannel((MagickCore::ChannelType)channels, blackPoint, whitePoint, midpoint);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor)
	{
		Throw::IfNull("blackColor", blackColor);
		Throw::IfNull("whiteColor", whiteColor);

		const Magick::Color* black = blackColor->CreateColor();
		const Magick::Color* white = whiteColor->CreateColor();

		try
		{
			Value->levelColors(*black, *white);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete black;
			delete white;
		}
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
			throw MagickException::Create(exception);
		}
		finally
		{
			delete black;
			delete white;
		}
	}
	///=============================================================================================
	void MagickImage::LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, bool invert, Channels channels)
	{
		Throw::IfNull("blackColor", blackColor);
		Throw::IfNull("whiteColor", whiteColor);

		const Magick::Color* black = blackColor->CreateColor();
		const Magick::Color* white = whiteColor->CreateColor();

		try
		{
			Value->levelColorsChannel((MagickCore::ChannelType)channels, *black, *white, invert);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete black;
			delete white;
		}
	}
	///=============================================================================================
	void MagickImage::LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, Channels channels)
	{
		Throw::IfNull("blackColor", blackColor);
		Throw::IfNull("whiteColor", whiteColor);

		const Magick::Color* black = blackColor->CreateColor();
		const Magick::Color* white = whiteColor->CreateColor();

		try
		{
			Value->levelColorsChannel((MagickCore::ChannelType)channels, *black, *white);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete black;
			delete white;
		}
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Map(MagickImage^ image)
	{
		Map(image, false);
	}
	//==============================================================================================
	void MagickImage::Map(MagickImage^ image, bool dither)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->map(*image->Value, dither);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			Value->negateChannel((MagickCore::ChannelType)channels, onlyGrayscale);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Opaque(MagickColor^ opaqueColor, MagickColor^ penColor)
	{
		Throw::IfNull("opaqueColor", opaqueColor);
		Throw::IfNull("penColor", penColor);

		const Magick::Color* opaque = opaqueColor->CreateColor();
		const Magick::Color* pen = penColor->CreateColor();

		try
		{
			Value->opaque(*opaque, *pen);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete opaque;
			delete pen;
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
			throw MagickException::Create(exception);
		}
	}
	///=============================================================================================
	void MagickImage::Perceptible(double epsilon, Channels channels)
	{
		try
		{
			Value->perceptibleChannel((MagickCore::ChannelType)channels, epsilon);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Polaroid(String^ caption, double angle)
	{
		Throw::IfNull("caption", caption);

		try
		{
			std::string caption_;
			Marshaller::Marshal(caption, caption_);

			Value->polaroid(caption_, angle);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Posterize(int levels)
	{
		Posterize(levels, false);
	}
	//==============================================================================================
	void MagickImage::Posterize(int levels, bool dither)
	{
		try
		{
			Value->posterize(levels, dither);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Posterize(int levels, bool dither, Channels channels)
	{
		try
		{
			Value->posterizeChannel((MagickCore::ChannelType)channels, levels, dither);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Posterize(int levels, Channels channels)
	{
		Posterize(levels, false, channels);
	}
	//==============================================================================================
	void MagickImage::Quantize()
	{
		Quantize(false);
	}
	//==============================================================================================
	MagickErrorInfo^ MagickImage::Quantize(bool measureError)
	{
		try
		{
			Value->quantize(measureError);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}

		return measureError ? gcnew MagickErrorInfo(Value) : nullptr;
	}
	//==============================================================================================
	void MagickImage::QuantumOperator(Channels channels, EvaluateOperator evaluateOperator, double value)
	{
		try
		{
			Value->quantumOperator((MagickCore::ChannelType)channels,
				(MagickCore::MagickEvaluateOperator)evaluateOperator, value);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::QuantumOperator(Channels channels, MagickGeometry^ geometry,  
		EvaluateOperator evaluateOperator, double value)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfTrue("geometry", geometry->IsPercentage, "Percentage is not supported.");

		try
		{
			Value->quantumOperator(geometry->X, geometry->Y, geometry->Width, geometry->Height,
				(MagickCore::ChannelType)channels, (MagickCore::MagickEvaluateOperator)evaluateOperator,
				value);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
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
		_ReadWarning = MagickReader::Read(Value, data, readSettings);
		return _ReadWarning;
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
	MagickWarningException^ MagickImage::Read(String^ fileName)
	{
		return Read(fileName, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(String^ fileName, MagickReadSettings^ readSettings)
	{
		_ReadWarning = MagickReader::Read(Value, fileName, readSettings);
		return _ReadWarning;
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(Stream^ stream)
	{
		return Read(stream, nullptr);
	}
	//==============================================================================================
	MagickWarningException^ MagickImage::Read(Stream^ stream, MagickReadSettings^ readSettings)
	{
		_ReadWarning = MagickReader::Read(Value, stream, readSettings);
		return _ReadWarning;
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::RemoveProfile(String^ name)
	{
		Magick::Blob blob;
		SetProfile(name, blob);
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
			throw MagickException::Create(exception);
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
		delete geometry;
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Sample(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Sample(geometry);
		delete geometry;
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
			throw MagickException::Create(exception);
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
		delete geometry;
	}
	//==============================================================================================
	void MagickImage::Scale(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Scale(geometry);
		delete geometry;
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
			throw MagickException::Create(exception);
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
		delete geometry;
	}
	//==============================================================================================
	void MagickImage::Segment()
	{
		Segment(1.0, 1.5);
	}
	//==============================================================================================
	void MagickImage::Segment(double clusterThreshold, double smoothingThreshold)
	{
		try
		{
			Value->segment(clusterThreshold, smoothingThreshold);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			separateImages(images, *Value, (MagickCore::ChannelType)channels);

			return MagickImageCollection::CreateList(images);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete images;
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
			throw MagickException::Create(exception);
		}
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::SetOption(MagickFormat format, String^ name, bool flag)
	{
		Throw::IfNullOrEmpty("name", name);

		std::string magick;
		Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, format), magick);
		std::string optionName;
		Marshaller::Marshal(name, optionName);

		try
		{
			Value->defineSet(magick, optionName, flag);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::SetOption(MagickFormat format, String^ name, String^ value)
	{
		Throw::IfNullOrEmpty("name", name);
		Throw::IfNull("value", value);

		std::string magick;
		Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, format), magick);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			Value->shadow((double)alpha * 100, sigma, x, y);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			clone->Value->shadow((double)alpha * 100, sigma, x, y);
			clone->Value->backgroundColor(Magick::Color());

			images->Add(clone);
			images->Add(Clone());
			images->Merge(Value, LayerMethod::Mosaic);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Sharpen(double radius, double sigma, Channels channels)
	{
		try
		{
			Value->sharpenChannel((MagickCore::ChannelType)channels, radius, sigma);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Shave(int leftRight, int topBottom)
	{
		Magick::Geometry* geometry = new Magick::Geometry(leftRight, topBottom);

		try
		{
			Value->shave(*geometry);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::SigmoidalContrast(bool sharpen, double contrast)
	{
		SigmoidalContrast(sharpen, contrast, MaxMap / 2.0);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::SparseColor(Channels channels, SparseColorMethod method, array<double>^ coordinates)
	{
		Throw::IfNull("coordinates", coordinates);

		double* arguments = Marshaller::Marshal(coordinates);

		try
		{

			Value->sparseColor((MagickCore::ChannelType)channels, (MagickCore::SparseColorMethod)method,
				coordinates->Length, arguments);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
		finally
		{
			delete[] arguments;
		}
	}
	//==============================================================================================
	MagickImageStatistics^ MagickImage::Statistics()
	{
		Magick::Image::ImageStatistics statistics;

		try
		{
			Value->statistics(&statistics);

			return gcnew MagickImageStatistics(&statistics);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
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
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Threshold(double value)
	{
		try
		{
			Value->threshold(value);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
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
		return ToBitmap(ImageFormat::Png);
	}
	//==============================================================================================
	Bitmap^ MagickImage::ToBitmap(ImageFormat^ format)
	{
		SetFormat(format);

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
		MagickWriter::Write(this->Value, &blob);
		return Marshaller::Marshal(&blob);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
		}
		finally
		{
			delete transparentColorLow;
			delete transparentColorHigh;
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
			throw MagickException::Create(exception);
		}
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			throw MagickException::Create(exception);
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
			Value->whiteThresholdChannel((MagickCore::ChannelType)channels, threshold_);
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Write(String^ fileName)
	{
		MagickWriter::Write(Value, fileName);
	}
	//==============================================================================================
	void MagickImage::Write(Stream^ stream)
	{
		MagickWriter::Write(Value, stream);
	}
	//==============================================================================================
	void MagickImage::Zoom(int width, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(width, height);
		Zoom(geometry);
		delete geometry;
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
			throw MagickException::Create(exception);
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
		delete geometry;
	}
	//==============================================================================================
}