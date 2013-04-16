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
#pragma once

#include "Colors\MagickColor.h"
#include "Drawables\DrawableAffine.h"
#include "Drawables\DrawableBase.h"
#include "Enums\Channels.h"
#include "Enums\ClassType.h"
#include "Enums\ColorSpace.h"
#include "Enums\ColorType.h"
#include "Enums\CompositeOperator.h"
#include "Enums\DistortMethod.h"
#include "Enums\Endian.h"
#include "Enums\EvaluateOperator.h"
#include "Enums\FillRule.h"
#include "Enums\FilterType.h"
#include "Enums\GifDisposeMethod.h"
#include "Enums\Gravity.h"
#include "Enums\ImageType.h"
#include "Enums\NoiseType.h"
#include "Enums\OrientationType.h"
#include "Enums\PaintMethod.h"
#include "Enums\Resolution.h"
#include "Enums\RenderingIntent.h"
#include "Exceptions\MagickException.h"
#include "Helpers\MagickErrorInfo.h"
#include "Helpers\MagickReader.h"
#include "Helpers\MagickWrapper.h"
#include "Helpers\MagickWriter.h"
#include "Helpers\Percentage.h"
#include "Helpers\TypeMetric.h"
#include "MagickBlob.h"
#include "MagickGeometry.h"
#include "Matrices\MatrixColor.h"
#include "Matrices\MatrixConvolve.h"
#include "Pixels\PixelCollection.h"
#include "Pixels\WritablePixelCollection.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick image object.
	///</summary>
	public ref class MagickImage sealed : MagickWrapper<Magick::Image>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static initonly MagickGeometry^ _DefaultFrameGeometry = gcnew MagickGeometry(25, 25, 6, 6);
		//===========================================================================================
		String^ _ReadWarning;
		//===========================================================================================
		MagickImage();
		//===========================================================================================
		String^ FormatedFileSize();
		//===========================================================================================
		void RaiseOrLower(int size, bool raiseFlag);
		//===========================================================================================
		void RandomThreshold(Magick::Quantum low, Magick::Quantum high, bool isPercentage);
		//===========================================================================================
		void RandomThreshold(Channels channels, Magick::Quantum low, Magick::Quantum high, bool isPercentage);
		//===========================================================================================
		void ReplaceImage(Magick::Image* image);
		//===========================================================================================
		void Resize(int width, int height, bool isPercentage);
		//==========================================================================================
		void SetProfile(String^ name, MagickBlob^ blob);
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickImage(const Magick::Image& image);
		//===========================================================================================
		Magick::Image* ReuseImage();
		//===========================================================================================
		bool Equals(const Magick::Image& image);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified width, height
		/// and color.
		///</summary>
		///<param name="width">The width.</param>
		///<param name="height">The height.</param>
		///<param name="color">The color to fill the image with.</param>
		MagickImage(int width, int height, MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// Join images into a single multi-image file.
		///</summary>
		property bool Adjoin
		{
			bool get()
			{
				return Value->adjoin();
			}
			void set(bool value)
			{
				Value->adjoin(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Time in 1/100ths of a second which must expire before splaying the next image in an
		/// animated sequence.
		///</summary>
		property int AnimationDelay
		{
			int get()
			{
				return Value->animationDelay();
			}
			void set(int value)
			{
				Value->animationDelay(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Number of iterations to loop an animation (e.g. Netscape loop extension) for.
		///</summary>
		property int AnimationIterations
		{
			int get()
			{
				return Value->animationIterations();
			}
			void set(int value)
			{
				Value->animationIterations(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Anti-alias Postscript and TrueType fonts (default true).
		///</summary>
		property bool AntiAlias
		{
			bool get()
			{
				return Value->antiAlias();
			}
			void set(bool value)
			{
				Value->antiAlias(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Background color of the image.
		///</summary>
		property MagickColor^ BackgroundColor
		{
			MagickColor^ get()
			{
				return gcnew MagickColor(Value->backgroundColor());
			}
			void set(MagickColor^ value)
			{
				Magick::Color* color = value != nullptr ? value->CreateColor() : new Magick::Color();
				Value->backgroundColor(*color);
				delete color;
			}
		}
		///==========================================================================================
		///<summary>
		/// Height of the image before transformations.
		///</summary>
		property int BaseHeight
		{
			int get()
			{
				return Value->baseRows();
			}
		}
		///==========================================================================================
		///<summary>
		/// Width of the image before transformations.
		///</summary>
		property int BaseWidth
		{
			int get()
			{
				return Value->baseColumns();
			}
		}
		///==========================================================================================
		///<summary>
		/// Return smallest bounding box enclosing non-border pixels. The current fuzz value is used
		/// when discriminating between pixels.
		///</summary>
		property MagickGeometry^ BoundingBox
		{
			MagickGeometry^ get()
			{
				return gcnew MagickGeometry(Value->boundingBox());
			}
		}
		///==========================================================================================
		///<summary>
		/// Border color of the image.
		///</summary>
		property MagickColor^ BorderColor
		{
			MagickColor^ get()
			{
				return gcnew MagickColor(Value->borderColor());
			}
			void set(MagickColor^ value)
			{
				Magick::Color* color = value != nullptr ? value->CreateColor() : new Magick::Color();
				Value->borderColor(*color);
				delete color;
			}
		}
		///==========================================================================================
		///<summary>
		/// Image class (DirectClass or PseudoClass)
		/// NOTE: Setting a DirectClass image to PseudoClass will result in the loss of color information
		/// if the number of colors in the image is greater than the maximum palette size (either 256 (Q8)
		/// or 65536 (Q16).
		///</summary>
		property ClassType ClassType
		{
			ImageMagick::ClassType get()
			{
				return (ImageMagick::ClassType)Value->classType();
			}
			void set(ImageMagick::ClassType value)
			{
				return Value->classType((MagickCore::ClassType)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Associate a clip mask with the image. The clip mask must be the same dimensions as the
		/// image. Pass null to unset an existing clip mask.
		///</summary>
		property MagickImage^ ClipMask
		{
			MagickImage^ get()
			{
				return gcnew MagickImage(Magick::Image(Value->clipMask()));
			}
			void set(MagickImage^ value)
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
		}
		///==========================================================================================
		///<summary>
		/// Colors within this distance are considered equal.
		///</summary>
		property double ColorFuzz
		{
			double get()
			{
				return Value->colorFuzz();
			}
			Void set(double value)
			{
				Value->colorFuzz(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Colormap size (number of colormap entries).
		///</summary>
		property int ColorMapSize
		{
			int get()
			{
				return Value->colorMapSize();
			}
			void set(int value)
			{
				Value->colorMapSize(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Color space of the image.
		///</summary>
		property ColorSpace ColorSpace
		{
			ImageMagick::ColorSpace get()
			{
				return (ImageMagick::ColorSpace)Value->colorSpace();
			}
			void set(ImageMagick::ColorSpace value)
			{
				return Value->colorSpace((MagickCore::ColorspaceType)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Color type of the image.
		///</summary>
		property ColorType ColorType
		{
			ImageMagick::ColorType get()
			{
				return (ImageMagick::ColorType)Value->type();
			}
			void set(ImageMagick::ColorType value)
			{
				return Value->type((MagickCore::ImageType)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Comment text of the image.
		///</summary>
		property String^ Comment
		{
			String^ get()
			{
				return Marshaller::Marshal(Value->comment());
			}
			void set(String^ value)
			{
				std::string comment; 
				Value->comment(Marshaller::Marshal(value, comment));
			}
		}
		///==========================================================================================
		///<summary>
		/// Vertical and horizontal resolution in pixels of the image.
		///</summary>
		property MagickGeometry^ Density
		{
			MagickGeometry^ get()
			{
				return gcnew MagickGeometry(Value->density());
			}
			void set(MagickGeometry^ value)
			{
				if (value == nullptr)
					return;

				Value->density(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Endianness (little like Intel or big like SPARC) for image formats which support
		/// endian-specific options.
		///</summary>
		//===========================================================================================
		property Endian Endian
		{
			ImageMagick::Endian get()
			{
				return (ImageMagick::Endian)Value->endian();
			}
			void set(ImageMagick::Endian value)
			{
				Value->endian((MagickCore::EndianType)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Exif profile (BLOB).
		///</summary>
		property MagickBlob^ ExifProfile
		{
			MagickBlob^ get()
			{
				Magick::Blob blob = Value->exifProfile();
				return gcnew MagickBlob(blob);
			}
			void set(MagickBlob^ value)
			{
				Value->exifProfile(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Image file size.
		///</summary>
		property int FileSize
		{
			int get()
			{
				return Value->fileSize();
			}
		}
		///==========================================================================================
		///<summary>
		/// Color to use when drawing inside an object.
		///</summary>
		property MagickColor^ FillColor
		{
			MagickColor^ get()
			{
				return gcnew MagickColor(Value->fillColor());
			}
			void set(MagickColor^ value)
			{
				Magick::Color* color = value != nullptr ? value->CreateColor() : new Magick::Color();
				Value->fillColor(*color);
				delete color;
			}
		}
		///==========================================================================================
		///<summary>
		/// Pattern to use while filling drawn objects.
		///</summary>
		property MagickImage^ FillPattern
		{
			MagickImage^ get()
			{
				return gcnew MagickImage(Magick::Image(Value->fillPattern()));
			}
			void set(MagickImage^ value)
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
		}
		///==========================================================================================
		///<summary>
		/// Rule to use when filling drawn objects.
		///</summary>
		property FillRule FillRule
		{
			ImageMagick::FillRule get()
			{
				return (ImageMagick::FillRule)Value->fillRule();
			}
			void set(ImageMagick::FillRule value)
			{
				Value->fillRule((Magick::FillRule)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Filter to use when resizing image.
		///</summary>
		property FilterType FilterType
		{
			ImageMagick::FilterType get()
			{
				return (ImageMagick::FilterType)Value->filterType();
			}
			void set(ImageMagick::FilterType value)
			{
				Value->filterType((Magick::FilterTypes)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Text rendering font.
		///</summary>
		property String^ Font
		{
			String^ get()
			{
				return Marshaller::Marshal(Value->font());
			}
			void set(String^ value)
			{
				std::string font;
				Value->font(Marshaller::Marshal(value, font));
			}
		}
		///==========================================================================================
		///<summary>
		/// Font point size.
		///</summary>
		property double FontPointsize
		{
			double get()
			{
				return Value->fontPointsize();
			}
			void set(double value)
			{
				Value->fontPointsize(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Gif disposal method.
		///</summary>
		property GifDisposeMethod GifDisposeMethod
		{
			ImageMagick::GifDisposeMethod get()
			{
				return (ImageMagick::GifDisposeMethod)Value->gifDisposeMethod();
			}
			void set(ImageMagick::GifDisposeMethod value)
			{
				Value->gifDisposeMethod((int)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Image supports transparency (matte channel).
		///</summary>
		property bool HasMatte
		{
			bool get()
			{
				return Value->matte();
			}
			void set(bool value)
			{
				Value->matte(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Height of the image.
		///</summary>
		property int Height
		{
			int get()
			{
				return Value->size().height();
			}
		}
		///==========================================================================================
		///<summary>
		/// The type of the image.
		///</summary>
		property ImageType ImageType
		{
			ImageMagick::ImageType get()
			{
				ImageMagick::ImageType result;

				if (!Enum::TryParse<ImageMagick::ImageType>(Marshaller::Marshal(Value->magick()), true, result))
					return ImageMagick::ImageType::Unknown;

				return result;
			}
			void set(ImageMagick::ImageType value)
			{
				if (value == ImageMagick::ImageType::Unknown)
					return;

				std::string name;

				Marshaller::Marshal(Enum::GetName(value.GetType(), value), name);
				Value->magick(name);
			}
		}
		///==========================================================================================
		///<summary>
		/// Transform image to black and white.
		///</summary>
		property bool IsMonochrome
		{
			bool get()
			{
				return Value->monochrome();
			}
			void set(bool value)
			{
				return Value->monochrome(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The label of the image.
		///</summary>
		property String^ Label
		{
			String^ get()
			{
				std::string label = Value->label();
				if (label.length() == 0)
					return nullptr;

				return Marshaller::Marshal(label);
			}
			void set(String^ value)
			{
				if (value == nullptr)
					value = "";

				std::string label;
				Marshaller::Marshal(value, label);
				Value->label(label);
			}
		}
		///==========================================================================================
		///<summary>
		/// Transparent color.
		///</summary>
		property MagickColor^ MatteColor
		{
			MagickColor^ get()
			{
				return gcnew MagickColor(Value->matteColor());
			}
			void set(MagickColor^ value)
			{
				Magick::Color* color = value != nullptr ? value->CreateColor() : new Magick::Color();
				Value->matteColor(*color);
				delete color;
			}
		}
		///==========================================================================================
		///<summary>
		/// Image modulus depth (minimum number of bits required to support red/green/blue components
		/// without loss of accuracy).
		///</summary>
		property int ModulusDepth
		{
			int get()
			{
				return Value->modulusDepth();
			}
			void set(int value)
			{
				Value->modulusDepth(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Photo orientation of the image.
		///</summary>
		property OrientationType Orientation
		{
			OrientationType get()
			{
				return (OrientationType)Value->orientation();
			}
			void set(OrientationType value)
			{
				Value->orientation((Magick::OrientationType)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Preferred size and location of an image canvas.
		///</summary>
		property MagickGeometry^ Page
		{
			MagickGeometry^ get()
			{
				return gcnew MagickGeometry(Value->page());
			}
			void set(MagickGeometry^ value)
			{
				Value->page(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// JPEG/MIFF/PNG compression level (default 75).
		///</summary>
		property int Quality
		{
			int get()
			{
				return Value->quality();
			}
			void set(int value)
			{
				int quality = value < 1 ? 1 : value;
				quality = quality > 100 ? 100 : quality;

				Value->quality(quality);
			}
		}
		///==========================================================================================
		///<summary>
		/// Maximum number of colors to quantize to.
		///</summary>
		property int QuantizeColors
		{
			int get()
			{
				return Value->quantizeColors();
			}
			void set(int value)
			{
				Value->quantizeColors(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Colorspace to quantize in.
		///</summary>
		property ImageMagick::ColorSpace QuantizeColorSpace
		{
			ImageMagick::ColorSpace get()
			{
				return (ImageMagick::ColorSpace)Value->quantizeColorSpace();
			}
			void set(ImageMagick::ColorSpace value)
			{
				return Value->quantizeColorSpace((MagickCore::ColorspaceType)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Dither image during quantization (default true).
		///</summary>
		property bool QuantizeDither
		{
			bool get()
			{
				return Value->quantizeDither();
			}
			void set(bool value)
			{
				Value->quantizeDither(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Quantization tree-depth.
		///</summary>
		property int QuantizeTreeDepth
		{
			int get()
			{
				return Value->quantizeTreeDepth();
			}
			void set(int value)
			{
				Value->quantizeTreeDepth(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the warning that occurred during the read operation.
		///</summary>
		property String^ ReadWarning
		{
			String^ get()
			{
				return _ReadWarning;
			}
		}
		///==========================================================================================
		///<summary>
		/// The type of rendering intent.
		///</summary>
		property RenderingIntent RenderingIntent
		{
			ImageMagick::RenderingIntent get()
			{
				return (ImageMagick::RenderingIntent)Value->renderingIntent();
			}
			void set(ImageMagick::RenderingIntent value)
			{
				return Value->renderingIntent((MagickCore::RenderingIntent)value);
			}
		} 
		///==========================================================================================
		///<summary>
		/// Units of image resolution.
		///</summary>
		property Resolution ResolutionUnits
		{
			Resolution get()
			{
				return (Resolution)Value->resolutionUnits();
			}
			void set(Resolution value)
			{
				return Value->resolutionUnits((MagickCore::ResolutionType)value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Color to use when drawing object outlines.
		///</summary>
		property MagickColor^ StrokeColor
		{
			MagickColor^ get()
			{
				return gcnew MagickColor(Value->strokeColor());
			}
			void set(MagickColor^ value)
			{
				Magick::Color* color = value != nullptr ? value->CreateColor() : new Magick::Color();
				Value->strokeColor(*color);
				delete color;
			}
		}
		///==========================================================================================
		///<summary>
		/// Stroke width for drawing lines, circles, ellipses, etc.
		///</summary>
		property double StrokeWidth
		{
			double get()
			{
				return Value->strokeWidth();
			}
			void set(double value)
			{
				Value->strokeWidth(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Width of the image.
		///</summary>
		property int Width
		{
			int get()
			{
				return Value->size().width();
			}
		}
		//===========================================================================================
		static bool operator == (MagickImage^ left, MagickImage^ right)
		{
			return Object::Equals(left, right);
		}
		//===========================================================================================
		static bool operator != (MagickImage^ left, MagickImage^ right)
		{
			return !Object::Equals(left, right);
		}
		//===========================================================================================
		static bool operator > (MagickImage^ left, MagickImage^ right)
		{
			return !(left < right) && (left != right);
		}
		//===========================================================================================
		static bool operator < (MagickImage^ left, MagickImage^ right)
		{
			Throw::IfNull("left", left);
			Throw::IfNull("right", right);

			return 
				(left->Value->rows() * left->Value->columns()) < 
				(right->Value->rows() * right->Value->columns());
		}
		//===========================================================================================
		static bool operator >= (MagickImage^ left, MagickImage^ right)
		{
			return (left > right) || (left == right);
		}
		//===========================================================================================
		static bool operator <= (MagickImage^ left, MagickImage^ right)
		{
			return (left < right) || (left == right);
		}
		///==========================================================================================
		///<summary>
		/// Adaptive-blur image with the default blur factor (0x1).
		///</summary>
		///<exception cref="MagickException"/>
		void AdaptiveBlur();
		///==========================================================================================
		///<summary>
		/// Adaptive-blur image with specified blur factor.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		void AdaptiveBlur(double radius, double sigma);
		///==========================================================================================
		///<summary>
		/// Local adaptive threshold image.
		/// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm
		///</summary>
		///<param name="width">The width of the pixel neighborhood.</param>
		///<param name="height">The height of the pixel neighborhood.</param>
		///<exception cref="MagickException"/>
		void AdaptiveThreshold(int width, int height);
		///==========================================================================================
		///<summary>
		/// Local adaptive threshold image.
		/// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm
		///</summary>
		///<param name="width">The width of the pixel neighborhood.</param>
		///<param name="height">The height of the pixel neighborhood.</param>
		///<param name="offset">Constant to subtract from pixel neighborhood mean.</param>
		///<exception cref="MagickException"/>
		void AdaptiveThreshold(int width, int height, int offset);
		///==========================================================================================
		///<summary>
		/// Add noise to image with the specified noise type.
		///</summary>
		///<param name="noiseType">The type of noise that should be added to the image.</param>
		///<exception cref="MagickException"/>
		void AddNoise(NoiseType noiseType);
		///==========================================================================================
		///<summary>
		/// Add noise to the specified channel of the image with the specified noise type.
		///</summary>
		///<param name="noiseType">The type of noise that should be added to the image.</param>
		///<param name="channels">The channel(s) where the noise should be added.</param>
		///<exception cref="MagickException"/>
		void AddNoise(NoiseType noiseType, Channels channels);
		///==========================================================================================
		///<summary>
		/// Add an ICC iCM profile to an image.
		///</summary>
		///<param name="blob">A blob containing the profile.</param>
		///<exception cref="MagickException"/>
		void AddProfile(MagickBlob^ blob);
		///==========================================================================================
		///<summary>
		/// Add an ICC iCM profile to an image.
		///</summary>
		///<param name="stream">A stream containing the profile.</param>
		///<exception cref="MagickException"/>
		void AddProfile(Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Add an ICC iCM profile to an image.
		///</summary>
		///<param name="fileName">The file to read the profile from.</param>
		///<exception cref="MagickException"/>
		void AddProfile(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Add a named profile to an image.
		///</summary>
		///<param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
		///<param name="blob">A blob containing the profile.</param>
		///<exception cref="MagickException"/>
		void AddProfile(String^ name, MagickBlob^ blob);
		///==========================================================================================
		///<summary>
		/// Add a named profile to an image.
		///</summary>
		///<param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
		///<param name="stream">A stream containing the profile.</param>
		///<exception cref="MagickException"/>
		void AddProfile(String^ name, Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Add a named profile to an image.
		///</summary>
		///<param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
		///<param name="fileName">The file to read the profile from.</param>
		///<exception cref="MagickException"/>
		void AddProfile(String^ name, String^ fileName);
		///==========================================================================================
		///<summary>
		/// Affine Transform image.
		///</summary>
		///<param name="affineMatrix">The affine matrix to use.</param>
		///<exception cref="MagickException"/>
		void AffineTransform(DrawableAffine^ affineMatrix);
		///==========================================================================================
		///<summary>
		/// Annotate using specified text, and bounding area.
		///</summary>
		///<param name="text">The text to use.</param>
		///<param name="boundingArea">The bounding area.</param>
		///<exception cref="MagickException"/>
		void Annotate(String^ text, MagickGeometry^ boundingArea);
		///==========================================================================================
		///<summary>
		/// Annotate using specified text, bounding area, and placement gravity.
		///</summary>
		///<param name="text">The text to use.</param>
		///<param name="boundingArea">The bounding area.</param>
		///<param name="gravity">The placement gravity.</param>
		///<exception cref="MagickException"/>
		void Annotate(String^ text, MagickGeometry^ boundingArea, Gravity gravity);
		///==========================================================================================
		///<summary>
		/// Annotate using specified text, bounding area, and placement gravity.
		///</summary>
		///<param name="text">The text to use.</param>
		///<param name="boundingArea">The bounding area.</param>
		///<param name="gravity">The placement gravity.</param>
		///<param name="degrees">The rotation.</param>
		///<exception cref="MagickException"/>
		void Annotate(String^ text, MagickGeometry^ boundingArea, Gravity gravity, double degrees);
		///==========================================================================================
		///<summary>
		/// Annotate with text (bounding area is entire image) and placement gravity.
		///</summary>
		///<param name="text">The text to use.</param>
		///<param name="gravity">The placement gravity.</param>
		///<exception cref="MagickException"/>
		void Annotate(String^ text, Gravity gravity);
		///==========================================================================================
		///<summary>
		/// Returns the value of a named image attribute.
		///</summary>
		///<param name="name">The name of the attribute.</param>
		String^ Attribute(String^ name);
		///==========================================================================================
		///<summary>
		/// Sets a named image attribute.
		///</summary>
		///<param name="name">The name of the attribute.</param>
		///<param name="value">The value of the attribute.</param>
		void Attribute(String^ name, String^ value);
		///==========================================================================================
		///<summary>
		/// Blur image with the default blur factor (0x1).
		///</summary>
		///<exception cref="MagickException"/>
		void Blur();
		///==========================================================================================
		///<summary>
		/// Blur image the specified channel of the image with the default blur factor (0x1).
		///</summary>
		///<param name="channels">The channel(s) that should be blurred.</param>
		///<exception cref="MagickException"/>
		void Blur(Channels channels);
		///==========================================================================================
		///<summary>
		/// Blur image with specified blur factor.
		///</summary>
		///<param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		void Blur(double radius, double sigma);
		///==========================================================================================
		///<summary>
		/// Blur image with specified blur factor and channel.
		///</summary>
		///<param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="channels">The channel(s) that should be blurred.</param>
		///<exception cref="MagickException"/>
		void Blur(double radius, double sigma, Channels channels);
		///==========================================================================================
		///<summary>
		/// Border image (add border to image).
		///</summary>
		///<param name="size">The size of the border.</param>
		///<exception cref="MagickException"/>
		void Border(int size);
		///==========================================================================================
		///<summary>
		/// Border image (add border to image).
		///</summary>
		///<param name="width">The width of the border.</param>
		///<param name="height">The height of the border.</param>
		///<exception cref="MagickException"/>
		void Border(int width, int height);
		///==========================================================================================
		///<summary>
		/// Applies the color decision list from the specified ASC CDL file.
		///</summary>
		///<param name="fileName">The file to read the ASC CDL information from.</param>
		///<exception cref="MagickException"/>
		void CDL(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Charcoal effect image (looks like charcoal sketch).
		///</summary>
		///<exception cref="MagickException"/>
		void Charcoal();
		///==========================================================================================
		///<summary>
		/// Charcoal effect image (looks like charcoal sketch).
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		void Charcoal(double radius, double sigma);
		///==========================================================================================
		///<summary>
		/// Chop image (remove vertical and horizontal subregion of image).
		///</summary>
		///<param name="xOffset">The X offset from origin.</param>
		///<param name="width">The width of the part to chop horizontally.</param>
		///<param name="yOffset">The Y offset from origin.</param>
		///<param name="height">The height of the part to chop vertically.</param>
		///<exception cref="MagickException"/>
		void Chop(int xOffset, int width, int yOffset, int height);
		///==========================================================================================
		///<summary>
		/// Chop image (remove vertical or horizontal subregion of image) using the specified geometry.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<exception cref="MagickException"/>
		void Chop(MagickGeometry^ geometry);
		///==========================================================================================
		///<summary>
		/// Chop image (remove horizontal subregion of image).
		///</summary>
		///<param name="offset">The X offset from origin.</param>
		///<param name="width">The width of the part to chop horizontally.</param>
		///<exception cref="MagickException"/>
		void ChopHorizontal(int offset, int width);
		///==========================================================================================
		///<summary>
		/// Chop image (remove horizontal subregion of image).
		///</summary>
		///<param name="offset">The Y offset from origin.</param>
		///<param name="height">The height of the part to chop vertically.</param>
		///<exception cref="MagickException"/>
		void ChopVertical(int offset, int height);
		///==========================================================================================
		///<summary>
		/// Chromaticity blue primary point.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		void ChromaBluePrimary(double x, double y);
		///==========================================================================================
		///<summary>
		/// Chromaticity green primary point.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		void ChromaGreenPrimary(double x, double y);
		///==========================================================================================
		///<summary>
		/// Chromaticity red primary point.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		void ChromaRedPrimary(double x, double y);
		///==========================================================================================
		///<summary>
		/// Chromaticity red primary point.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		void ChromaWhitePoint(double x, double y);
		///==========================================================================================
		///<summary>
		/// Colorize image with the specified color, using specified percent opacity.
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="opacity">The opacity percentage.</param>
		///<exception cref="MagickException"/>
		void Colorize(MagickColor^ color, Percentage opacity);
		///==========================================================================================
		///<summary>
		/// Colorize image with the specified color, using specified percent opacity for red, green,
		/// and blue quantums
		///</summary>
		///<param name="color">The color to use.</param>
		///<param name="opacityRed">The opacity percentage for red.</param>
		///<param name="opacityGreen">The opacity percentage for green.</param>
		///<param name="opacityBlue">The opacity percentage for blue.</param>
		///<exception cref="MagickException"/>
		void Colorize(MagickColor^ color, Percentage opacityRed, Percentage opacityGreen,
			Percentage opacityBlue);
		///==========================================================================================
		///<summary>
		/// Sets the alpha channel to the specified color.
		///</summary>
		///<param name="color">The color to use.</param>
		///<exception cref="MagickException"/>
		void ColorAlpha(MagickColor^ color);
		///==========================================================================================
		///<summary>
		// Get color at colormap position index.
		///</summary>
		///<param name="index">The position index.</param>
		///<exception cref="MagickException"/>
		MagickColor^ ColorMap(int index);
		///==========================================================================================
		///<summary>
		// Set color at colormap position index.
		///</summary>
		///<param name="index">The position index.</param>
		///<exception cref="MagickException"/>
		void ColorMap(int index, MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// Apply a color matrix to the image channels.
		///</summary>
		///<param name="matrixColor">The color matrix to use.</param>
		///<exception cref="MagickException"/>
		void ColorMatrix(MatrixColor^ matrixColor);
		///==========================================================================================
		///<summary>
		/// Compare current image with another image. Returns error information if the images are not
		/// identical.
		///</summary>
		///<param name="image">The other image to compare with this image.</param>
		///<exception cref="MagickException"/>
		MagickErrorInfo^ Compare(MagickImage^ image);
		//==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the 'In' operator.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<exception cref="MagickException"/>
		void Composite(MagickImage^ image, int x, int y);
		//==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<param name="compose">The algorithm to use.</param>
		///<exception cref="MagickException"/>
		void Composite(MagickImage^ image, int x, int y, CompositeOperator compose);
		//==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the 'In' operator.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="offset">The offset from origin.</param>
		///<exception cref="MagickException"/>
		void Composite(MagickImage^ image, MagickGeometry^ offset);
		//==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="offset">The offset from origin.</param>
		///<param name="compose">The algorithm to use.</param>
		///<exception cref="MagickException"/>
		void Composite(MagickImage^ image, MagickGeometry^ offset, CompositeOperator compose);
		//==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the 'In' operator.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="gravity">The placement gravity.</param>
		///<exception cref="MagickException"/>
		void Composite(MagickImage^ image, Gravity gravity);
		//==========================================================================================
		///<summary>
		/// Compose an image onto another at specified offset using the specified algorithm.
		///</summary>
		///<param name="image">The image to composite with this image.</param>
		///<param name="gravity">The placement gravity.</param>
		///<param name="compose">The algorithm to use.</param>
		///<exception cref="MagickException"/>
		void Composite(MagickImage^ image, Gravity gravity, CompositeOperator compose);
		//==========================================================================================
		///<summary>
		/// Contrast image (enhance intensity differences in image)
		///</summary>
		///<exception cref="MagickException"/>
		void Contrast();
		//==========================================================================================
		///<summary>
		/// Contrast image (enhance intensity differences in image)
		///</summary>
		///<param name="enhance">Use true to enhance the contrast and false to reduce the contrast.</param>
		///<exception cref="MagickException"/>
		void Contrast(bool enhance);
		//==========================================================================================
		///<summary>
		/// Convolve image. Applies a user-specified convolution to the image.
		///</summary>
		///<exception cref="MagickException"/>
		void Convolve(MatrixConvolve^ convolveMatrix);
		///==========================================================================================
		///<summary>
		/// Creates a copy of the current image.
		///</summary>
		MagickImage^ Copy();
		///==========================================================================================
		///<summary>
		/// Crop image (subregion of original image).
		///</summary>
		///<param name="geometry">The subregion to crop.</param>
		///<exception cref="MagickException"/>
		void Crop(MagickGeometry^ geometry);
		///==========================================================================================
		///<summary>
		/// Crop image (subregion of original image) using CropPosition.Center.
		///</summary>
		///<param name="width">The width of the subregion.</param>
		///<param name="height">The height of the subregion.</param>
		///<exception cref="MagickException"/>
		void Crop(int width, int height);
		///==========================================================================================
		///<summary>
		/// Crop image (subregion of original image).
		///</summary>
		///<param name="width">The width of the subregion.</param>
		///<param name="height">The height of the subregion.</param>
		///<param name="gravity">The position where the cropping should start from.</param>
		///<exception cref="MagickException"/>
		void Crop(int width, int height,  Gravity gravity);
		///==========================================================================================
		///<summary>
		/// Displaces an image's colormap by a given number of positions.
		///</summary>
		///<param name="amount">Displace the colormap this amount.</param>
		///<exception cref="MagickException"/>
		void CycleColormap(int amount);
		///==========================================================================================
		///<summary>
		/// Returns the depth (bits allocated to red/green/blue components).
		///</summary>
		///<exception cref="MagickException"/>
		int Depth();
		///==========================================================================================
		///<summary>
		/// Returns the depth (bits allocated to red/green/blue components) of the specified channel.
		///</summary>
		///<param name="channels">The channel to get the depth for.</param>
		///<exception cref="MagickException"/>
		int Depth(Channels channels);
		///==========================================================================================
		///<summary>
		/// Returns the depth (bits allocated to red/green/blue components).
		///</summary>
		///<param name="value">The depth.</param>
		///<exception cref="MagickException"/>
		void Depth(int value);
		///==========================================================================================
		///<summary>
		/// Set the depth (bits allocated to red/green/blue components) of the specified channel.
		///</summary>
		///<param name="channels">The channel to set the depth for.</param>
		///<param name="value">The depth.</param>
		///<exception cref="MagickException"/>
		void Depth(Channels channels, int value);
		///==========================================================================================
		///<summary>
		/// Despeckle image (reduce speckle noise).
		///</summary>
		///<exception cref="MagickException"/>
		void Despeckle();
		///==========================================================================================
		///<summary>
		/// Distorts an image using various distortion methods, by mapping color lookups of the source
		/// image to a new destination image of the same size as the source image.
		///</summary>
		///<param name="method">The distortion method to use.</param>
		///<param name="arguments">An array containing the arguments for the distortion.</param>
		///<exception cref="MagickException"/>
		void Distort(DistortMethod method, array<double>^ arguments);
		///==========================================================================================
		///<summary>
		/// Distorts an image using various distortion methods, by mapping color lookups of the source
		/// image to a new destination image usually of the same size as the source image, unless
		/// 'bestfit' is set to true.
		///</summary>
		///<param name="method">The distortion method to use.</param>
		///<param name="arguments">An array containing the arguments for the distortion.</param>
		///<param name="bestfit">Attempt to 'bestfit' the size of the resulting image.</param>
		///<exception cref="MagickException"/>
		void Distort(DistortMethod method, array<double>^ arguments, bool bestfit);
		///==========================================================================================
		///<summary>
		/// Draw on image using a single drawable.
		///</summary>
		///<param name="drawable">The drawable to draw on the image.</param>
		///<exception cref="MagickException"/>
		void Draw(DrawableBase^ drawable);
		///==========================================================================================
		///<summary>
		/// Draw on image using a collection of drawables.
		///</summary>
		///<param name="drawables">The drawables to draw on the image.</param>
		///<exception cref="MagickException"/>
		void Draw(IEnumerable<DrawableBase^>^ drawables);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current image.
		///</summary>
		///<param name="obj">The object to compare this image with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified image is equal to the current image.
		///</summary>
		///<param name="image">The image to compare this image with.</param>
		bool Equals(MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// Edge image (hilight edges in image).
		///</summary>
		///<param name="radius">The radius of the pixel neighborhood.</param>
		///<exception cref="MagickException"/>
		void Edge(double radius);
		///==========================================================================================
		///<summary>
		/// Emboss image (hilight edges with 3D effect) with default value (0x1).
		///</summary>
		///<exception cref="MagickException"/>
		void Emboss();
		///==========================================================================================
		///<summary>
		/// Emboss image (hilight edges with 3D effect).
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<exception cref="MagickException"/>
		void Emboss(double radius, double sigma);
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the geometry.
		///</summary>
		///<param name="geometry">The geometry to extend the image to.</param>
		///<exception cref="MagickException"/>
		void Extent(MagickGeometry^ geometry);
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the geometry.
		///</summary>
		///<param name="geometry">The geometry to extend the image to.</param>
		///<param name="backgroundColor">The background color to use.</param>
		///<exception cref="MagickException"/>
		void Extent(MagickGeometry^ geometry, MagickColor^ backgroundColor);
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the geometry.
		///</summary>
		///<param name="geometry">The geometry to extend the image to.</param>
		///<param name="gravity">The placement gravity.</param>
		///<exception cref="MagickException"/>
		void Extent(MagickGeometry^ geometry, Gravity gravity);
		///==========================================================================================
		///<summary>
		/// Extend the image as defined by the geometry.
		///</summary>
		///<param name="geometry">The geometry to extend the image to.</param>
		///<param name="gravity">The placement gravity.</param>
		///<param name="backgroundColor">The background color to use.</param>
		///<exception cref="MagickException"/>
		void Extent(MagickGeometry^ geometry, Gravity gravity, MagickColor^ backgroundColor);
		///==========================================================================================
		///<summary>
		/// Flip image (reflect each scanline in the vertical direction).
		///</summary>
		///<exception cref="MagickException"/>
		void Flip();
		///==========================================================================================
		///<summary>
		/// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="color">The color to use.</param>
		///<exception cref="MagickException"/>
		void FloodFillColor(int x, int y, MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="geometry">The position of the pixel.</param>
		///<param name="color">The color to use.</param>
		///<exception cref="MagickException"/>
		void FloodFillColor(MagickGeometry^ geometry, MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// Flood-fill color across pixels that match the color of the  target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="color">The color to use.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		void FloodFillColor(int x, int y, MagickColor^ color, MagickColor^ borderColor);
		///==========================================================================================
		///<summary>
		/// Flood-fill color across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="geometry">The position of the pixel.</param>
		///<param name="color">The color to use.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		void FloodFillColor(MagickGeometry^ geometry, MagickColor^ color, MagickColor^ borderColor);
		///==========================================================================================
		///<summary>
		/// Floodfill pixels matching color (within fuzz factor) of target pixel(x,y) with replacement
		/// opacity value using method.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="opacity">The opacity to use.</param>
		///<param name="paintMethod">The paint method to use.</param>
		///<exception cref="MagickException"/>
		void FloodFillOpacity(int x, int y, int opacity, PaintMethod paintMethod);
		///==========================================================================================
		///<summary>
		/// Floodfill designated area with replacement opacity value.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="color">The color to use.</param>
		///<param name="paintMethod">The paint method to use.</param>
		///<exception cref="MagickException"/>
		void FloodFillMatte(int x, int y, MagickColor^ color, PaintMethod paintMethod);
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="image">The image to use.</param>
		///<exception cref="MagickException"/>
		void FloodFillTexture(int x, int y, MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="geometry">The position of the pixel.</param>
		///<param name="image">The image to use.</param>
		///<exception cref="MagickException"/>
		void FloodFillTexture(MagickGeometry^ geometry, MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="image">The image to use.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		void FloodFillTexture(int x, int y, MagickImage^ image, MagickColor^ borderColor);
		///==========================================================================================
		///<summary>
		/// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
		/// of the target pixel. Uses current fuzz setting when determining color match.
		///</summary>
		///<param name="geometry">The position of the pixel.</param>
		///<param name="image">The image to use.</param>
		///<param name="borderColor">The color of the border.</param>
		///<exception cref="MagickException"/>
		void FloodFillTexture(MagickGeometry^ geometry, MagickImage^ image, MagickColor^ borderColor);
		///==========================================================================================
		///<summary>
		/// Flop image (reflect each scanline in the horizontal direction).
		///</summary>
		///<exception cref="MagickException"/>
		void Flop();
		///==========================================================================================
		///<summary>
		/// Obtain font metrics for text string given current font, pointsize, and density settings.
		///</summary>
		///<exception cref="MagickException"/>
		TypeMetric^ FontTypeMetrics(String^ text);
		///==========================================================================================
		///<summary>
		/// Long image format description
		///</summary>
		///<exception cref="MagickException"/>
		String^ Format();
		///==========================================================================================
		///<summary>
		/// Frame image with the default geometry (25x25+6+6).
		///</summary>
		///<exception cref="MagickException"/>
		void Frame();
		///==========================================================================================
		///<summary>
		/// Frame image with the specified geometry.
		///</summary>
		///<param name="geometry">The geometry of the frame.</param>
		///<exception cref="MagickException"/>
		void Frame(MagickGeometry^ geometry);
		///==========================================================================================
		///<summary>
		/// Frame image with the specified with and height.
		///</summary>
		///<param name="width">The width of the frame.</param>
		///<param name="height">The height of the frame.</param>
		///<exception cref="MagickException"/>
		void Frame(int width, int height);
		///==========================================================================================
		///<summary>
		/// Frame image with the specified with, height, innerBevel and outerBevel.
		///</summary>
		///<param name="width">The width of the frame.</param>
		///<param name="height">The height of the frame.</param>
		///<param name="innerBevel">The inner bevel of the frame.</param>
		///<param name="outerBevel">The outer bevel of the frame.</param>
		///<exception cref="MagickException"/>
		void Frame(int width, int height, int innerBevel, int outerBevel);
		///==========================================================================================
		///<summary>
		/// Applies a mathematical expression to the image.
		///</summary>
		///<param name="expression">The expression to apply.</param>
		///<exception cref="MagickException"/>
		void Fx(String^ expression);
		///==========================================================================================
		///<summary>
		/// Applies a mathematical expression to the image.
		///</summary>
		///<param name="expression">The expression to apply.</param>
		///<param name="channel">The channel(s) to apply the expression to.</param>
		///<exception cref="MagickException"/>
		void Fx(String^ expression, Channels channel);
		///==========================================================================================
		///<summary>
		/// Gamma level of the image.
		///</summary>
		///<exception cref="MagickException"/>
		double Gamma();
		///==========================================================================================
		///<summary>
		/// Gamma correct image.
		///</summary>
		///<param name="value">The image gamma.</param>
		///<exception cref="MagickException"/>
		void Gamma(double value);
		///==========================================================================================
		///<summary>
		/// Gamma correct image.
		///</summary>
		///<param name="gammeRed">The image gamma for the red channel.</param>
		///<param name="gammeGreen">The image gamma for the green channel.</param>
		///<param name="gammeBlue">The image gamma for the blue channel.</param>
		///<exception cref="MagickException"/>
		void Gamma(double gammeRed, double gammeGreen, double gammeBlue);
		///==========================================================================================
		///<summary>
		/// Gaussian blur image.
		///</summary>
		///<param name="width">The number of neighbor pixels to be included in the convolution.</param>
		///<param name="sigma">The standard deviation of the gaussian bell curve.</param>
		///<exception cref="MagickException"/>
		void GaussianBlur(double width, double sigma);
		///==========================================================================================
		///<summary>
		/// Gaussian blur image.
		///</summary>
		///<param name="width">The number of neighbor pixels to be included in the convolution.</param>
		///<param name="sigma">The standard deviation of the gaussian bell curve.</param>
		///<param name="channels">The channel(s) to blur.</param>
		///<exception cref="MagickException"/>
		void GaussianBlur(double width, double sigma, Channels channels);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		///==========================================================================================
		///<summary>
		/// Preferred size of the image when encoding.
		///</summary>
		///<exception cref="MagickException"/>
		MagickGeometry^ Geometry();
		///==========================================================================================
		///<summary>
		/// Retrieve the ICC ICM profile from the image.
		///</summary>
		///<exception cref="MagickException"/>
		MagickBlob^ GetProfile();
		///==========================================================================================
		///<summary>
		/// Retrieve a named profile from the image.
		///</summary>
		///<param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
		///<exception cref="MagickException"/>
		MagickBlob^ GetProfile(String^ name);
		///==========================================================================================
		///<summary>
		/// Returns an read-only pixel collection that can be used to access the pixels of this image.
		///</summary>
		///<exception cref="MagickException"/>
		PixelCollection^ GetReadOnlyPixels();
		///==========================================================================================
		///<summary>
		/// Returns an read-only pixel collection that can be used to access the pixels of this image.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="width">The width of the pixel area.</param>
		///<param name="height">The height of the pixel area.</param>
		///<exception cref="MagickException"/>
		PixelCollection^ GetReadOnlyPixels(int x, int y, int width, int height);
		///==========================================================================================
		///<summary>
		/// Returns an writable pixel collection that can be used to access the pixels of this image.
		///</summary>
		///<exception cref="MagickException"/>
		WritablePixelCollection^ GetWritablePixels();
		///==========================================================================================
		///<summary>
		/// Returns an writable pixel collection that can be used to access the pixels of this image.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="width">The width of the pixel area.</param>
		///<param name="height">The height of the pixel area.</param>
		///<exception cref="MagickException"/>
		WritablePixelCollection^ GetWritablePixels(int x, int y, int width, int height);
		///==========================================================================================
		///<summary>
		/// Apply a color lookup table (Hald CLUT) to the image.
		///</summary>
		///<param name="image">The image to use.</param>
		///<exception cref="MagickException"/>
		void HaldClut(MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// Implode image (special effect).
		///</summary>
		///<param name="factor">The extent of the implosion.</param>
		///<exception cref="MagickException"/>
		void Implode(double factor);
		///==========================================================================================
		///<summary>
		/// Implements the inverse discrete Fourier transform (DFT) of the image as a magnitude phase.
		///</summary>
		///<param name="image">The image to use.</param>
		///<exception cref="MagickException"/>
		void InverseFourierTransform(MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// Implements the inverse discrete Fourier transform (DFT) of the image either as a magnitude
		/// phase or real / imaginary image pair.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="magnitude">Magnitude phase or real / imaginary image pair.</param>
		///<exception cref="MagickException"/>
		void InverseFourierTransform(MagickImage^ image, bool magnitude);
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range. Uses a midpoint of 1.0.
		///</summary>
		///<param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<exception cref="MagickException"/>
		void Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint);
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range. Uses a midpoint of 1.0.
		///</summary>
		///<param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<param name="channels">The channel(s) to level.</param>
		///<exception cref="MagickException"/>
		void Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, Channels channels);
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range.
		///</summary>
		///<param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
		///<exception cref="MagickException"/>
		void Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, double midpoint);
		///==========================================================================================
		///<summary>
		/// Adjust the levels of the image by scaling the colors falling between specified white and
		/// black points to the full available quantum range.
		///</summary>
		///<param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
		///<param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
		///<param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10)</param>
		///<param name="channels">The channel(s) to level.</param>
		///<exception cref="MagickException"/>
		void Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, double midpoint, Channels channels);
		///==========================================================================================
		///<summary>
		/// Lower image (lighten or darken the edges of an image to give a 3-D lowered effect).
		///</summary>
		///<param name="size">The size of the edges.</param>
		///<exception cref="MagickException"/>
		void Lower(int size);
		///==========================================================================================
		///<summary>
		/// Magnify image by integral size.
		///</summary>
		///<exception cref="MagickException"/>
		void Magnify();
		///==========================================================================================
		///<summary>
		/// Remap image colors with closest color from reference image.
		///</summary>
		///<param name="image">The image to use.</param>
		///<exception cref="MagickException"/>
		void Map(MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// Remap image colors with closest color from reference image.
		///</summary>
		///<param name="image">The image to use.</param>
		///<param name="dither">Dither the image.</param>
		///<exception cref="MagickException"/>
		void Map(MagickImage^ image, bool dither);
		///==========================================================================================
		///<summary>
		/// Filter image by replacing each pixel component with the median color in a circular neighborhood.
		///</summary>
		///<exception cref="MagickException"/>
		void MedianFilter();
		///==========================================================================================
		///<summary>
		/// Filter image by replacing each pixel component with the median color in a circular neighborhood.
		///</summary>
		///<param name="radius">The radius of the pixel neighborhood.</param>
		///<exception cref="MagickException"/>
		void MedianFilter(double radius);
		///==========================================================================================
		///<summary>
		/// Reduce image by integral size.
		///</summary>
		///<exception cref="MagickException"/>
		void Minify();
		///==========================================================================================
		///<summary>
		/// Modulate percent hue, saturation, and brightness of an image.
		///</summary>
		///<param name="brightness">The brightness percentage.</param>
		///<param name="saturation">The saturation percentage.</param>
		///<param name="hue">The hue percentage.</param>
		///<exception cref="MagickException"/>
		void Modulate(Percentage brightness, Percentage saturation, Percentage hue);
		///==========================================================================================
		///<summary>
		/// Motion blur image with specified blur factor.
		///</summary>
		///<param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
		///<param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
		///<param name="angle">The angle the object appears to be comming from (zero degrees is from the right).</param>
		///<exception cref="MagickException"/>
		void MotionBlur(double radius, double sigma, double angle);
		///==========================================================================================
		///<summary>
		/// Negate colors in image.
		///</summary>
		///<exception cref="MagickException"/>
		void Negate();
		///==========================================================================================
		///<summary>
		/// Negate colors in image.
		///</summary>
		///<param name="onlyGrayscale">Use true to negate only the grayscale colors.</param>
		///<exception cref="MagickException"/>
		void Negate(bool onlyGrayscale);
		///==========================================================================================
		///<summary>
		/// Normalize image (increase contrast by normalizing the pixel values to span the full range
		/// of color values)
		///</summary>
		///<exception cref="MagickException"/>
		void Normalize();
		///==========================================================================================
		///<summary>
		/// Oilpaint image (image looks like oil painting)
		///</summary>
		void OilPaint();
		///==========================================================================================
		///<summary>
		/// Oilpaint image (image looks like oil painting)
		///</summary>
		///<param name="radius">The radius of the circular neighborhood.</param>
		///<exception cref="MagickException"/>
		void OilPaint(double radius);
		///==========================================================================================
		///<summary>
		/// Quantize image (reduce number of colors).
		///</summary>
		///<exception cref="MagickException"/>
		void Quantize();
		///==========================================================================================
		///<summary>
		/// Quantize image (reduce number of colors).
		///</summary>
		///<param name="measureError">When false is specified this method will return null.</param>
		///<exception cref="MagickException"/>
		MagickErrorInfo^ Quantize(bool measureError);
		///==========================================================================================
		///<summary>
		/// Apply an arithmetic or bitwise operator to the image pixel quantums.
		///</summary>
		///<param name="channels">The channel(s) to apply the operator on.</param>
		///<param name="evaluateOperator">The operator.</param>
		///<param name="value">The value.</param>
		///<exception cref="MagickException"/>
		void QuantumOperator(Channels channels, EvaluateOperator evaluateOperator, double value);
		///==========================================================================================
		///<summary>
		/// Apply an arithmetic or bitwise operator to the image pixel quantums.
		///</summary>
		///<param name="geometry">The geometry to use.</param>
		///<param name="channels">The channel(s) to apply the operator on.</param>
		///<param name="evaluateOperator">The operator.</param>
		///<param name="value">The value.</param>
		///<exception cref="MagickException"/>
		void QuantumOperator(MagickGeometry^ geometry, Channels channels, EvaluateOperator evaluateOperator, double value);
		///==========================================================================================
		///<summary>
		/// Raise image (lighten or darken the edges of an image to give a 3-D raised effect).
		///</summary>
		///<param name="size">The size of the edges.</param>
		///<exception cref="MagickException"/>
		void Raise(int size);
		///==========================================================================================
		///<summary>
		/// Changes the value of individual pixels based on the intensity of each pixel compared to a
		/// random threshold. The result is a low-contrast, two color image.
		///</summary>
		///<param name="low">The low threshold.</param>
		///<param name="high">The low threshold.</param>
		///<exception cref="MagickException"/>
		void RandomThreshold(Magick::Quantum low, Magick::Quantum high);
		///==========================================================================================
		///<summary>
		/// Changes the value of individual pixels based on the intensity of each pixel compared to a
		/// random threshold. The result is a low-contrast, two color image.
		///</summary>
		///<param name="low">The low threshold.</param>
		///<param name="high">The low threshold.</param>
		///<exception cref="MagickException"/>
		void RandomThreshold(Percentage low, Percentage high);
		///==========================================================================================
		///<summary>
		/// Changes the value of individual pixels based on the intensity of each pixel compared to a
		/// random threshold. The result is a low-contrast, two color image.
		///</summary>
		///<param name="channels">The channel(s) to use.</param>
		///<param name="low">The low threshold.</param>
		///<param name="high">The low threshold.</param>
		///<exception cref="MagickException"/>
		void RandomThreshold(Channels channels, Magick::Quantum low, Magick::Quantum high);
		///==========================================================================================
		///<summary>
		/// Changes the value of individual pixels based on the intensity of each pixel compared to a
		/// random threshold. The result is a low-contrast, two color image.
		///</summary>
		///<param name="channels">The channel(s) to use.</param>
		///<param name="low">The low threshold.</param>
		///<param name="high">The low threshold.</param>
		///<exception cref="MagickException"/>
		void RandomThreshold(Channels channels, Percentage low, Percentage high);
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="blob">The blob to read the image data from.</param>
		///<exception cref="MagickException"/>
		static MagickImage^ Read(MagickBlob^ blob);
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="blob">The blob to read the image data from.</param>
		///<param name="colorSpace">The colorspace to convert the image to.</param>
		///<exception cref="MagickException"/>
		static MagickImage^ Read(MagickBlob^ blob, ImageMagick::ColorSpace colorSpace);
		///==========================================================================================
		///<summary>
		/// Read single vector image frame.
		///</summary>
		///<param name="blob">The blob to read the image data from.</param>
		///<param name="width">The width of the image.</param>
		///<param name="height">The height of the image.</param>
		///<exception cref="MagickException"/>
		static MagickImage^ Read(MagickBlob^ blob, int width, int height);
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		static MagickImage^ Read(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<param name="colorSpace">The colorspace to convert the image to.</param>
		///<exception cref="MagickException"/>
		static MagickImage^ Read(String^ fileName, ImageMagick::ColorSpace colorSpace);
		///==========================================================================================
		///<summary>
		/// Read single vector image frame.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<param name="width">The width of the image.</param>
		///<param name="height">The height of the image.</param>
		///<exception cref="MagickException"/>
		static MagickImage^ Read(String^ fileName, int width, int height);
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		static MagickImage^ Read(Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Read single image frame.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<param name="colorSpace">The colorspace to convert the image to.</param>
		///<exception cref="MagickException"/>
		static MagickImage^ Read(Stream^ stream, ImageMagick::ColorSpace colorSpace);
		///==========================================================================================
		///<summary>
		/// Read single vector image frame.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<param name="width">The width of the image.</param>
		///<param name="height">The height of the image.</param>
		///<exception cref="MagickException"/>
		static MagickImage^ Read(Stream^ stream, int width, int height);
		///==========================================================================================
		///<summary>
		/// Reduce noise in image using a noise peak elimination filter.
		///</summary>
		///<exception cref="MagickException"/>
		void ReduceNoise();
		///==========================================================================================
		///<summary>
		/// Reduce noise in image using a noise peak elimination filter.
		///</summary>
		///<param name="order">The order to use.</param>
		///<exception cref="MagickException"/>
		void ReduceNoise(int order);
		///==========================================================================================
		///<summary>
		/// Remove a named profile from the image.
		///</summary>
		///<param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
		///<exception cref="MagickException"/>
		void RemoveProfile(String^ name);
		///==========================================================================================
		///<summary>
		/// Resize image to specified size.
		///</summary>
		///<param name="width">The new width.</param>
		///<param name="height">The new height.</param>
		///<exception cref="MagickException"/>
		void Resize(int width, int height);
		///==========================================================================================
		///<summary>
		/// Resize image to specified percentage.
		///</summary>
		///<param name="percentage">The percentage.</param>
		///<exception cref="MagickException"/>
		void Resize(Percentage percentage);
		///==========================================================================================
		///<summary>
		/// Resize image to specified percentage.
		///</summary>
		///<param name="percentageWidth">The percentage of the width.</param>
		///<param name="percentageHeight">The percentage of the height.</param>
		///<exception cref="MagickException"/>
		void Resize(Percentage percentageWidth, Percentage percentageHeight);
		///==========================================================================================
		///<summary>
		/// Roll image (rolls image vertically and horizontally).
		///</summary>
		///<param name="xOffset">The X offset from origin.</param>
		///<param name="yOffset">The Y offset from origin.</param>
		///<exception cref="MagickException"/>
		void Roll(int xOffset, int yOffset);
		///==========================================================================================
		///<summary>
		/// Separates a channel from the image and makes it a grayscale image.
		///</summary>
		///<param name="channels">The channel(s) to separates.</param>
		///<exception cref="MagickException"/>
		void Separate(Channels channels);
		///==========================================================================================
		///<summary>
		/// Converts this instance to a MagickBlob.
		///</summary>
		MagickBlob^ ToBlob();
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current image.
		///</summary>
		virtual String^ ToString() override;
		///==========================================================================================
		///<summary>
		/// Add matte channel to image, setting pixels matching color to transparent.
		///</summary>
		///<param name="color">The color to make transparent.</param>
		void Transparent(MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// Writes the image to the specified file name.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		void Write(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Writes the image to the specified stream.
		///</summary>
		///<param name="stream">The stream to write the image data to.</param>
		///<exception cref="MagickException"/>
		void Write(Stream^ stream);
		//===========================================================================================
	};
	//==============================================================================================
}