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
#include "stdafx.h"
#include "MagickImage.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	MagickImage::MagickImage()
	{
		Value = new Magick::Image();
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
	void MagickImage::ReplaceImage(Magick::Image* image)
	{
		if (!_IsValueOwner)
			throw gcnew NotSupportedException("This method can only be used after you make a Copy of this image.");

		delete Value;
		Value = image;
	}
	//==============================================================================================
	MagickImage::MagickImage(Magick::Image* image)
	{
		_IsValueOwner = false;
		Value = image;
	}
	//==============================================================================================
	MagickImage::MagickImage(int width, int height, MagickColor^ color)
	{
		Throw::IfNull("color", color);

		MagickGeometry^ geometry = gcnew	MagickGeometry(width, height);
		Magick::Color* background = color->CreateColor();
		Value = new Magick::Image(geometry, *background);
		delete geometry;
		delete background;
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::AddNoise(NoiseType noiseType)
	{
		try
		{
			Value->addNoise((Magick::NoiseType)noiseType);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::AddNoise(NoiseType noiseType, Channels channel)
	{
		try
		{
			Value->addNoiseChannel((Magick::ChannelType)channel, (Magick::NoiseType)noiseType);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Annotate(String^ text, MagickGeometry^ location)
	{
		Throw::IfNullOrEmpty("text", text);

		std::string annotateText;
		Marshaller::Marshal(text, annotateText);

		try
		{
			Value->annotate(annotateText, location);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Annotate(String^ text, MagickGeometry^ boundingArea, Gravity gravity)
	{
		Throw::IfNullOrEmpty("text", text);

		std::string annotateText;
		Marshaller::Marshal(text, annotateText);

		try
		{
			Value->annotate(annotateText, boundingArea, (Magick::GravityType)gravity);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Annotate(String^ text, MagickGeometry^ boundingArea, Gravity gravity, double degrees)
	{
		Throw::IfNullOrEmpty("text", text);

		std::string annotateText;
		Marshaller::Marshal(text, annotateText);

		try
		{
			Value->annotate(annotateText, boundingArea, (Magick::GravityType)gravity, degrees);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	String^ MagickImage::Attribute(String^ name)
	{
		Throw::IfNull("name", name);

		std::string attributeName;
		Marshaller::Marshal(name, attributeName);

		try
		{
			return Marshaller::Marshal(Value->attribute(attributeName));
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Attribute(String^ name, String^ value)
	{
		Throw::IfNull("name", name);
		Throw::IfNull("value", value);

		std::string attributeName;
		Marshaller::Marshal(name, attributeName);
		std::string attributeValue;
		Marshaller::Marshal(value, attributeValue);

		try
		{
			return Value->attribute(attributeName, attributeValue);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Blur()
	{
		Blur(0.0, 1.0);
	}
	//==============================================================================================
	void MagickImage::Blur(Channels channel)
	{
		Blur(0.0, 1.0, channel);
	}
	//==============================================================================================
	void MagickImage::Blur(double radius, double sigma)
	{
		try
		{
			Value->blur(radius, sigma);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Blur(double radius, double sigma, Channels channel)
	{
		try
		{
			Value->blurChannel((Magick::ChannelType)channel, radius, sigma);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Border(MagickColor^ color, int width)
	{
		Throw::IfNull("color", color);

		Magick::Color* magickColor = color->CreateColor();
		MagickGeometry^ geometry = gcnew MagickGeometry(width, width);

		try
		{
			Value->borderColor(*magickColor);
			Value->border(geometry);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete magickColor;
			delete geometry;
		}
	}
	//==============================================================================================
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
		catch (Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	int MagickImage::ChannelDepth(Channels channel)
	{
		try
		{
			return Value->channelDepth((MagickCore::ChannelType)channel);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::ChannelDepth(Channels channel, int depth)
	{
		try
		{
			Value->channelDepth((MagickCore::ChannelType)channel, depth);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Chop(int xOffset, int width, int yOffset, int height)
	{
		MagickGeometry^ geometry = gcnew MagickGeometry(xOffset, yOffset, width, height);

		try
		{
			Value->chop(geometry);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete geometry;
		}
	}
	//==============================================================================================
	void MagickImage::Chop(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

		try
		{
			Value->chop(geometry);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::ChromaGreenPrimary(double x, double y)
	{
		try
		{
			Value->chromaGreenPrimary(x, y);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::ChromaRedPrimary(double x, double y)
	{
		try
		{
			Value->chromaRedPrimary(x, y);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::ChromaWhitePoint(double x, double y)
	{
		try
		{
			Value->chromaWhitePoint(x, y);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::ColorAlpha(MagickColor^ color)
	{
		Throw::IfNull("color", color);

		MagickImage^ image = gcnew MagickImage(Width, Height, color);

		try
		{
			image->Composite(this, Gravity::Northwest, CompositeOperator::Atop);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}

		ReplaceImage(image->Value);
		image->Value = NULL;
	}
	//==============================================================================================
	MagickColor^ MagickImage::ColorMap(int index)
	{
		try
		{
			return gcnew MagickColor(Value->colorMap(index));
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::ColorMap(int index, MagickColor^ color)
	{
		Throw::IfNull("color", color);

		try
		{
			Magick::Color* colorMap = color->CreateColor();
			Value->colorMap(index, *colorMap);
			delete colorMap;
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Colorize(int opacityRedPercentage, int opacityGreenPercentage,
		int opacityBluePercentage, MagickColor^ color)
	{
		Throw::IfNotIsPercentage("opacityRedPercentage", opacityRedPercentage);
		Throw::IfNotIsPercentage("opacityGreenPercentage", opacityGreenPercentage);
		Throw::IfNotIsPercentage("opacityBluePercentage", opacityBluePercentage);
		Throw::IfNull("color", color);

		Magick::Color* magickColor = color->CreateColor();

		try
		{
			Value->colorize(opacityRedPercentage, opacityGreenPercentage, opacityBluePercentage, *magickColor);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete magickColor;
		}
	}
	//==============================================================================================
	void MagickImage::Colorize(int opacityPercentage, MagickColor^ color)
	{
		Throw::IfNotIsPercentage("opacityPercentage", opacityPercentage);
		Throw::IfNull("color", color);

		Magick::Color* magickColor = color->CreateColor();

		try
		{
			Value->colorize(opacityPercentage, *magickColor);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete magickColor;
		}
	}
	//==============================================================================================
	void MagickImage::ColorMatrix(MatrixColor^ matrixColor)
	{
		Throw::IfNull("matrixColor", matrixColor);

		double* matrix = (double*)matrixColor;

		try
		{
			Value->colorMatrix(matrixColor->Order, matrix);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete matrix;
		}
	}
	//==============================================================================================
	CompareResult^ MagickImage::Compare(MagickImage^ image)
	{
		Throw::IfNull("image", image);

		try
		{
			if (Value->compare(*(image->Value)))
				return nullptr;
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}

		return gcnew CompareResult(Value->meanErrorPerPixel(), Value->normalizedMaxError(), Value->normalizedMeanError());
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, int x, int y)
	{
		Composite(image, x, y, CompositeOperator::In);
	}
	//==============================================================================================
	void MagickImage::Composite(MagickImage^ image, int x, int y, CompositeOperator compose)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->composite(*(image->Value), x, y, (MagickCore::CompositeOperator)compose);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		Throw::IfNull("image", image);
		Throw::IfNull("geometry", geometry);

		try
		{
			Value->composite(*(image->Value), geometry, (MagickCore::CompositeOperator)compose);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		Throw::IfNull("image", image);

		try
		{
			Value->composite(*(image->Value), (MagickCore::GravityType)gravity, (MagickCore::CompositeOperator)compose);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
			Value->contrast(enhance ? 0 : 1);}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Convolve(MatrixConvolve^ convolveMatrix)
	{
		Throw::IfNull("convolveMatrix", convolveMatrix);

		double* kernel = (double*)convolveMatrix;

		try
		{
			Value->convolve(convolveMatrix->Order, kernel);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete kernel;
		}
	}
	//==============================================================================================
	MagickImage^ MagickImage::Copy()
	{
		MagickImage^ copy = gcnew MagickImage();
		copy->Value = new Magick::Image(*Value);
		return copy;
	}
	//==============================================================================================
	void MagickImage::Crop(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

		try
		{
			Value->crop(geometry);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Despeckle()
	{
		try
		{
			Value->despeckle();
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete distortArguments;
		}
	}
	//==============================================================================================
	void MagickImage::Draw(DrawableBase^ drawable)
	{
		Throw::IfNull("drawable", drawable);

		try
		{
			Value->draw(*(drawable->InternalValue));
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Draw(IEnumerable<DrawableBase^>^ drawables)
	{
		Throw::IfNull("drawables", drawables);

		try
		{
			std::list<Magick::Drawable> drawList;
			IEnumerator<DrawableBase^>^ enumerator = drawables->GetEnumerator();
			while(enumerator->MoveNext())
			{
				drawList.push_back(*(enumerator->Current->InternalValue));
			}

			Value->draw(drawList);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Edge(double radius)
	{
		try
		{
			Value->edge(radius);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	bool MagickImage::Equals(Object^ obj)
	{
		return Equals(dynamic_cast<MagickImage^>(obj));
	}
	//==============================================================================================
	bool MagickImage::Equals(MagickImage^ image)
	{
		if (image == nullptr)
			return false;

		return 
			Value->rows() == image->Value->rows() && 
			Value->columns() == image->Value->columns() &&
			Value->signature() == image->Value->signature();
	}
	//==============================================================================================
	void MagickImage::Extent(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

		try
		{
			Value->extent(geometry);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Extent(MagickGeometry^ geometry, MagickColor^ backgroundColor)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("backgroundColor", backgroundColor);

		Magick::Color* color = backgroundColor->CreateColor();

		try
		{
			Value->extent(geometry, *color);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete color;
		}
	}
	//==============================================================================================
	void MagickImage::Extent(MagickGeometry^ geometry, Gravity gravity)
	{
		Throw::IfNull("geometry", geometry);

		try
		{
			Value->extent(geometry, (MagickCore::GravityType)gravity);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Extent(MagickGeometry^ geometry, Gravity gravity, MagickColor^ backgroundColor)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("backgroundColor", backgroundColor);

		Magick::Color* color = backgroundColor->CreateColor();

		try
		{
			Value->extent(geometry, *color, (MagickCore::GravityType)gravity);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete color;
		}
	}
	//==============================================================================================
	void MagickImage::Flip()
	{
		try
		{
			Value->flip();
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::FloodFillColor(int x, int y, MagickColor^ color)
	{
		Throw::IfNull("color", color);

		Magick::Color* fillColor = color->CreateColor();

		try
		{
			Value->floodFillColor(x, y, *fillColor);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete fillColor;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFillColor(MagickGeometry^ geometry, MagickColor^ color)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("color", color);

		Magick::Color* fillColor = color->CreateColor();

		try
		{
			Value->floodFillColor(geometry, *fillColor);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete fillColor;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFillColor(int x, int y, MagickColor^ color, MagickColor^ borderColor)
	{
		Throw::IfNull("color", color);
		Throw::IfNull("borderColor", borderColor);

		Magick::Color* fillColor = color->CreateColor();
		Magick::Color* fillBorderColor = borderColor->CreateColor();

		try
		{
			Value->floodFillColor(x, y, *fillColor, *fillBorderColor);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete fillColor;
			delete fillBorderColor;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFillColor(MagickGeometry^ geometry, MagickColor^ color, MagickColor^ borderColor)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("color", color);
		Throw::IfNull("borderColor", borderColor);

		Magick::Color* fillColor = color->CreateColor();
		Magick::Color* fillBorderColor = borderColor->CreateColor();

		try
		{
			Value->floodFillColor(geometry, *fillColor, *fillBorderColor);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete fillColor;
			delete fillBorderColor;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFillOpacity(int x, int y, int opacity, PaintMethod paintMethod)
	{
		try
		{
			Value->floodFillOpacity(x, y, opacity, (MagickCore::PaintMethod)paintMethod);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::FloodFillTexture(int x, int y, MagickImage^ image)
	{
		Throw::IfNull("image", image);

		try
		{
			Value->floodFillTexture(x, y, *image->Value);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::FloodFillTexture(MagickGeometry^ geometry, MagickImage^ image)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("image", image);

		try
		{
			Value->floodFillTexture(geometry, *image->Value);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::FloodFillTexture(int x, int y, MagickImage^ image, MagickColor^ borderColor)
	{
		Throw::IfNull("image", image);
		Throw::IfNull("borderColor", borderColor);

		Magick::Color* fillBorderColor = borderColor->CreateColor();

		try
		{
			Value->floodFillTexture(x, y, *image->Value, *fillBorderColor);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete fillBorderColor;
		}
	}
	//==============================================================================================
	void MagickImage::FloodFillTexture(MagickGeometry^ geometry, MagickImage^ image, MagickColor^ borderColor)
	{
		Throw::IfNull("geometry", geometry);
		Throw::IfNull("image", image);
		Throw::IfNull("borderColor", borderColor);

		Magick::Color* fillBorderColor = borderColor->CreateColor();

		try
		{
			Value->floodFillTexture(geometry, *image->Value, *fillBorderColor);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete fillBorderColor;
		}
	}
	//==============================================================================================
	void MagickImage::Flop()
	{
		try
		{
			Value->flop();
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
		finally
		{
			delete metric;
		}
	}
	//==============================================================================================
	String^ MagickImage::Format()
	{
		try
		{
			return Marshaller::Marshal(Value->format());
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Frame()
	{
		Frame(gcnew MagickGeometry(25, 25, 6, 6));
	}
	//==============================================================================================
	void MagickImage::Frame(MagickGeometry^ geometry)
	{
		Throw::IfNull("geometry", geometry);

		try
		{
			Value->frame(geometry);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
	void MagickImage::Fx(String^ expression)
	{
		Throw::IfNullOrEmpty("expression", expression);

		try
		{
			std::string fxExpression;
			Marshaller::Marshal(expression, fxExpression);
			Value->fx(fxExpression);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Fx(String^ expression, Channels channel)
	{
		Throw::IfNullOrEmpty("expression", expression);

		try
		{
			std::string fxExpression;
			Marshaller::Marshal(expression, fxExpression);
			Value->fx(fxExpression, (MagickCore::ChannelType)channel);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	double MagickImage::Gamma()
	{
		try
		{
			return Value->gamma();
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
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
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::GaussianBlur(double width, double sigma)
	{
		try
		{
			Value->gaussianBlur(width, sigma);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::GaussianBlur(double width, double sigma, Channels channel)
	{
		try
		{
			Value->gaussianBlurChannel((MagickCore::ChannelType)channel, width, sigma);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	MagickGeometry^ MagickImage::Geometry()
	{
		try
		{
			return gcnew MagickGeometry(Value->geometry());
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	int MagickImage::GetHashCode()
	{
		return Object::GetHashCode();
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
	MagickBlob^ MagickImage::Profile(String^ name)
	{
		Throw::IfNullOrEmpty("name", name);

		try
		{
			std::string profileName;
			Marshaller::Marshal(name, profileName);
			Magick::Blob blob = Value->profile(profileName);
			return gcnew MagickBlob(blob);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	void MagickImage::Profile(String^ name, MagickBlob^ blob)
	{
		Throw::IfNullOrEmpty("name", name);

		try
		{
			Magick::Blob profileBlob = blob != nullptr ? (Magick::Blob&)blob : Magick::Blob();

			std::string profileName;
			Marshaller::Marshal(name, profileName);
			Value->profile(profileName, profileBlob);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	///=============================================================================================
	void MagickImage::Profile(String^ name, Stream^ stream)
	{
		if (stream == nullptr)
		{
			Profile(name, (MagickBlob^)nullptr);
			return;
		}

		MagickBlob^ blob = MagickBlob::Read(stream);
		try
		{
			Profile(name, blob);
		}
		finally
		{
			delete blob;
		}
	}
	///=============================================================================================
	void MagickImage::Profile(String^ name, String^ fileName)
	{
		if (String::IsNullOrEmpty(fileName))
		{
			Profile(name, (MagickBlob^)nullptr);
			return;
		}

		MagickBlob^ blob = MagickBlob::Read(fileName);
		try
		{
			Profile(name, blob);
		}
		finally
		{
			delete blob;
		}
	}
	//==============================================================================================
	MagickImage^ MagickImage::Read(MagickBlob^ blob)
	{
		MagickImage^ image = gcnew MagickImage();
		image->_ReadWarning = MagickReader::Read(image->Value, false, blob);
		return image;
	}
	//==============================================================================================
	MagickImage^ MagickImage::Read(MagickBlob^ blob, ImageMagick::ColorSpace colorSpace)
	{
		MagickImage^ image = gcnew MagickImage();
		image->_ReadWarning = MagickReader::Read(image->Value, false, blob, colorSpace);
		return image;
	}
	//==============================================================================================
	MagickImage^ MagickImage::Read(MagickBlob^ blob, int width, int height)
	{
		MagickImage^ image = gcnew MagickImage();
		image->_ReadWarning = MagickReader::Read(image->Value, blob, width, height);
		return image;
	}
	//==============================================================================================
	MagickImage^ MagickImage::Read(String^ fileName)
	{
		MagickImage^ image = gcnew MagickImage();
		image->_ReadWarning = MagickReader::Read(image->Value, false, fileName);
		return image;
	}
	//==============================================================================================
	MagickImage^ MagickImage::Read(String^ fileName, ImageMagick::ColorSpace colorSpace)
	{
		MagickImage^ image = gcnew MagickImage();
		image->_ReadWarning = MagickReader::Read(image->Value, false, fileName, colorSpace);
		return image;
	}
	//==============================================================================================
	MagickImage^ MagickImage::Read(String^ fileName, int width, int height)
	{
		MagickImage^ image = gcnew MagickImage();
		image->_ReadWarning = MagickReader::Read(image->Value, fileName, width, height);
		return image;
	}
	//==============================================================================================
	MagickImage^ MagickImage::Read(Stream^ stream)
	{
		MagickImage^ image = gcnew MagickImage();
		image->_ReadWarning = MagickReader::Read(image->Value, false, stream);
		return image;
	}
	//==============================================================================================
	MagickImage^ MagickImage::Read(Stream^ stream, ImageMagick::ColorSpace colorSpace)
	{
		MagickImage^ image = gcnew MagickImage();
		image->_ReadWarning = MagickReader::Read(image->Value, false, stream, colorSpace);
		return image;;
	}
	//==============================================================================================
	MagickImage^ MagickImage::Read(Stream^ stream, int width, int height)
	{
		MagickImage^ image = gcnew MagickImage();
		image->_ReadWarning = MagickReader::Read(image->Value, stream, width, height);
		return image;
	}
	//==============================================================================================
	void MagickImage::Separate(Channels channel)
	{
		try
		{
			Value->channel((MagickCore::ChannelType)channel);
		}
		catch(Magick::Exception exception)
		{
			throw gcnew MagickException(exception);
		}
	}
	//==============================================================================================
	MagickBlob^ MagickImage::ToBlob()
	{
		MagickBlob^ blob = MagickBlob::Create();
		MagickWriter::Write(this->Value, (Magick::Blob*)blob);
		return blob;
	}
	//==============================================================================================
	String^ MagickImage::ToString()
	{
		return String::Format(CultureInfo::InvariantCulture, "{0} {1}x{2} {3}-bit {4} {5}",
			ImageType, Width, Height, Depth, ColorSpace, FormatedFileSize());
	}
	//==============================================================================================
	void MagickImage::Write(String^ fileName)
	{
		MagickWriter::Write(Value, fileName);
	}
	//==============================================================================================
	void MagickImage::Write(Stream^ stream)
	{
		MagickBlob^ blob = ToBlob();
		MagickWriter::Write(Value, (Magick::Blob*)blob);
		blob->Write(stream);
		delete blob;
	}
	//==============================================================================================
}