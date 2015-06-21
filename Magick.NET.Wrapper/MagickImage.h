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

#include "Colors\MagickColor.h"
#include "IO\MagickReaderSettings.h"
#include "Pixels\PixelCollection.h"
#include "Pixels\WritablePixelCollection.h"
#include "Statistics\ChannelPerceptualHash.h"
#include "Types\MagickGeometry.h"
#include "Types\MagickSearchResult.h"
#include "MagickFormatInfo.h"

using namespace System::Collections::Generic;
using namespace System::Drawing::Imaging;
using namespace System::IO;
using namespace System::Text;
using namespace ImageMagick::Drawables;

#if !(NET20)
using namespace System::Windows::Media::Imaging;
typedef System::Windows::Media::PixelFormat MediaPixelFormat;
typedef System::Windows::Media::PixelFormats MediaPixelFormats;
#endif

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		private ref class MagickImage sealed : Internal::IMagickImage
		{
			//========================================================================================
		private:
			//========================================================================================
			Magick::Image* _Value;
			EventHandler<WarningEventArgs^>^ _WarningEvent;
			//========================================================================================
			!MagickImage();
			//========================================================================================
			property Magick::Image* Value
			{
				Magick::Image* get();
			}
			//========================================================================================
			MagickReaderSettings^ CheckSettings(MagickReadSettings^ readSettings);
			//========================================================================================
			MagickErrorInfo^ CreateErrorInfo();
			//========================================================================================
			static Magick::Image* CreateImage();
			//========================================================================================
			void DisposeValue();
			//========================================================================================
			void HandleException(const Magick::Exception& exception);
			//========================================================================================
			void HandleException(MagickException^ exception);
			//========================================================================================
			void ReplaceValue(const Magick::Image& value);
			//========================================================================================
			void SetProfile(String^ name, Magick::Blob& blob);
			//========================================================================================
		internal:
			//========================================================================================
			MagickImage(const Magick::Image& image);
			//========================================================================================
			void Apply(QuantizeSettings^ settings);
			//========================================================================================
			const Magick::Image& ReuseValue();
			//========================================================================================
		public:
			//========================================================================================
			~MagickImage();
			//========================================================================================
			MagickImage();
			//========================================================================================
			MagickImage(MagickImage^ image);
			//========================================================================================
			event EventHandler<WarningEventArgs^>^ Warning
			{
				void add(EventHandler<WarningEventArgs^>^ handler);
				void remove(EventHandler<WarningEventArgs^>^ handler);
			}
			//========================================================================================
			property bool Adjoin
			{
				bool get();
				void set(bool value);
			}
			//========================================================================================
			property MagickColor^ AlphaColor
			{
				MagickColor^ get();
				void set(MagickColor^ value);
			}
			//========================================================================================
			property int AnimationDelay
			{
				int get();
				void set(int value);
			}
			//========================================================================================
			property int AnimationIterations
			{
				int get();
				void set(int value);
			}
			//========================================================================================
			property bool AntiAlias
			{
				bool get();
				void set(bool value);
			}
			//========================================================================================
			property MagickColor^ BackgroundColor
			{
				MagickColor^ get();
				void set(MagickColor^ value);
			}
			//========================================================================================
			property int BaseHeight
			{
				int get();
			}
			//========================================================================================
			property int BaseWidth
			{
				int get();
			}
			//========================================================================================
			property bool BlackPointCompensation
			{
				bool get();
				void set(bool value);
			}
			//========================================================================================
			property MagickGeometry^ BoundingBox
			{
				MagickGeometry^ get();
			}
			//========================================================================================
			property MagickColor^ BorderColor
			{
				MagickColor^ get();
				void set(MagickColor^ value);
			}
			//========================================================================================
			property MagickColor^ BoxColor
			{
				MagickColor^ get();
				void set(MagickColor^ value);
			}
			//========================================================================================
			property IEnumerable<PixelChannel>^ Channels
			{
				IEnumerable<PixelChannel>^ get();
			}
			//========================================================================================
			property ClassType ClassType
			{
				ImageMagick::ClassType get();
				void set(ImageMagick::ClassType value);
			}
			//========================================================================================
			property Magick::Quantum ColorFuzz
			{
				Magick::Quantum get();
				void set(Magick::Quantum value);
			}
			//========================================================================================
			property int ColorMapSize
			{
				int get();
				void set(int value);
			}
			//========================================================================================
			property ColorSpace ColorSpace
			{
				ImageMagick::ColorSpace get();
				void set(ImageMagick::ColorSpace value);
			}
			//========================================================================================
			property ColorType ColorType
			{
				ImageMagick::ColorType get();
				void set(ImageMagick::ColorType value);
			}
			//========================================================================================
			property String^ Comment
			{
				String^ get();
				void set(String^ value);
			}
			//========================================================================================
			property CompositeOperator Compose
			{
				CompositeOperator get();
				void set(CompositeOperator value);
			}
			//========================================================================================
			property CompressionMethod CompressionMethod
			{
				ImageMagick::CompressionMethod get();
				void set(ImageMagick::CompressionMethod value);
			}
			//========================================================================================
			property bool Debug
			{
				bool get();
				void set(bool value);
			} 
			//========================================================================================
			property PointD Density
			{
				PointD get();
				void set(PointD value);
			}
			//========================================================================================
			property int Depth
			{
				int get();
				void set(int value);
			}
			//========================================================================================
			property MagickGeometry^ EncodingGeometry
			{
				MagickGeometry^ get();
			}
			//========================================================================================
			property Endian Endian
			{
				ImageMagick::Endian get();
				void set(ImageMagick::Endian value);
			}
			//========================================================================================
			property String^ FileName
			{
				String^ get();
			}
			//========================================================================================
			property long FileSize
			{
				long get();
			}
			//========================================================================================
			property MagickColor^ FillColor
			{
				MagickColor^ get();
				void set(MagickColor^ value);
			}
			//========================================================================================
			property MagickImage^ FillPattern
			{
				MagickImage^ get();
				void set(MagickImage^ value);
			}
			//========================================================================================
			property FillRule FillRule
			{
				ImageMagick::FillRule get();
				void set(ImageMagick::FillRule value);
			}
			//========================================================================================
			property FilterType FilterType
			{
				ImageMagick::FilterType get();
				void set(ImageMagick::FilterType value);
			}
			//========================================================================================
			property String^ FlashPixView
			{
				String^ get();
				void set(String^ value);
			}
			//========================================================================================
			property String^ Font
			{
				String^ get();
				void set(String^ value);
			}
			//========================================================================================
			property double FontPointsize
			{
				double get();
				void set(double value);
			}
			//========================================================================================
			property MagickFormat Format
			{
				MagickFormat get();
				void set(MagickFormat value);
			}
			//========================================================================================
			property double Gamma
			{
				double get();
			}
			//========================================================================================
			property GifDisposeMethod GifDisposeMethod
			{
				ImageMagick::GifDisposeMethod get();
				void set(ImageMagick::GifDisposeMethod value);
			}
			//========================================================================================
			property bool HasAlpha
			{
				bool get();
				void set(bool value);
			}
			//========================================================================================
			property int Height
			{
				int get();
			}
			//========================================================================================
			property Interlace Interlace
			{
				ImageMagick::Interlace get();
				void set(ImageMagick::Interlace value);
			}
			//========================================================================================
			property PixelInterpolateMethod Interpolate
			{
				PixelInterpolateMethod get();
				void set(PixelInterpolateMethod);
			}
			//========================================================================================
			property bool IsOpaque
			{
				bool get();
			}
			//========================================================================================
			property String^ Label
			{
				String^ get();
				void set(String^ value);
			}
			//========================================================================================
			property MagickImage^ Mask
			{
				MagickImage^ get();
				void set(MagickImage^ value);
			}
			//========================================================================================
			property OrientationType Orientation
			{
				OrientationType get();
				void set(OrientationType value);
			}
			//========================================================================================
			property MagickGeometry^ Page
			{
				MagickGeometry^ get();
				void set(MagickGeometry^ value);
			}
			//========================================================================================
			property IEnumerable<String^>^ ProfileNames
			{
				IEnumerable<String^>^ get();
			}
			//========================================================================================
			property int Quality
			{
				int get();
				void set(int value);
			}
			//========================================================================================
			property RenderingIntent RenderingIntent
			{
				ImageMagick::RenderingIntent get();
				void set(ImageMagick::RenderingIntent value);
			}
			//========================================================================================
			property Resolution ResolutionUnits
			{
				Resolution get();
				void set(Resolution value);
			}
			//========================================================================================
			property double ResolutionX
			{
				double get();
			}
			//========================================================================================
			property double ResolutionY
			{
				double get();
			}
			//========================================================================================
			property String^ Signature
			{
				String^ get();
			}
			//========================================================================================
			property bool StrokeAntiAlias
			{
				bool get();
				void set(bool value);
			}
			//========================================================================================
			property MagickColor^ StrokeColor
			{
				MagickColor^ get();
				void set(MagickColor^ value);
			}
			//========================================================================================
			property array<double>^ StrokeDashArray
			{
				array<double>^ get();
				void set(array<double>^ value);
			}
			//========================================================================================
			property double StrokeDashOffset
			{
				double get();
				void set(double value);
			}
			//========================================================================================
			property LineCap StrokeLineCap
			{
				LineCap get();
				void set(ImageMagick::LineCap value);
			}
			//========================================================================================
			property LineJoin StrokeLineJoin
			{
				LineJoin get();
				void set(LineJoin value);
			}
			//========================================================================================
			property int StrokeMiterLimit
			{
				int get();
				void set(int value);
			}
			//========================================================================================
			property MagickImage^ StrokePattern
			{
				MagickImage^ get();
				void set(MagickImage^ value);
			}
			//========================================================================================
			property double StrokeWidth
			{
				double get();
				void set(double value);
			}
			//========================================================================================
			property TextDirection TextDirection
			{
				ImageMagick::TextDirection get();
				void set(ImageMagick::TextDirection value);
			}
			//========================================================================================
			property String^ TextEncoding
			{
				String^ get();
				void set(String^ value);
			}
			//========================================================================================
			property Gravity TextGravity
			{
				Gravity get();
				void set(Gravity value);
			}
			//========================================================================================
			property double TextInterlineSpacing
			{
				double get();
				void set(double value);
			}
			//========================================================================================
			property double TextInterwordSpacing
			{
				double get();
				void set(double value);
			}
			//========================================================================================
			property double TextKerning
			{
				double get();
				void set(double value);
			}
			//========================================================================================
			property int TotalColors
			{
				int get();
			}
			//========================================================================================
			property bool Verbose
			{
				bool get();
				void set(bool verbose);
			}
			//========================================================================================
			property VirtualPixelMethod VirtualPixelMethod
			{
				ImageMagick::VirtualPixelMethod get();
				void set(ImageMagick::VirtualPixelMethod value);
			}
			//========================================================================================
			property int Width
			{
				int get();
			}
			//========================================================================================
			void AdaptiveBlur(double radius, double sigma);
			//========================================================================================
			void AdaptiveResize(MagickGeometry^ geometry);
			//========================================================================================
			void AdaptiveSharpen(double radius, double sigma);
			//========================================================================================
			void AdaptiveSharpen(double radius, double sigma, ImageMagick::Channels channels);
			//========================================================================================
			void AdaptiveThreshold(int width, int height, double bias);
			//========================================================================================
			void AddNoise(NoiseType noiseType);
			//========================================================================================
			void AddNoise(NoiseType noiseType, ImageMagick::Channels channels);
			//========================================================================================
			void AddProfile(String^ name, array<Byte>^ profile);
			//========================================================================================
			void AffineTransform(IDrawableAffine^ affineMatrix);
			//========================================================================================
			void Alpha(AlphaOption option);
			//========================================================================================
			void Annotate(String^ text, Gravity gravity);
			//========================================================================================
			void Annotate(String^ text, MagickGeometry^ boundingArea, Gravity gravity, double degrees);
			//========================================================================================
			void AutoGamma();
			//========================================================================================
			void AutoGamma(ImageMagick::Channels channels);
			//========================================================================================
			void AutoLevel();
			//========================================================================================
			void AutoLevel(ImageMagick::Channels channels);
			//========================================================================================
			void AutoOrient();
			//========================================================================================
			int BitDepth(ImageMagick::Channels channels);
			//========================================================================================
			void BitDepth(ImageMagick::Channels channels, int value);
			//========================================================================================
			void BlackThreshold(String^ threshold);
			//========================================================================================
			void BlackThreshold(String^ threshold, ImageMagick::Channels channels);
			//========================================================================================
			void BlueShift(double factor);
			//========================================================================================
			void Blur(double radius, double sigma);
			//========================================================================================
			void Blur(double radius, double sigma, ImageMagick::Channels channels);
			//========================================================================================
			void Border(int width, int height);
			//========================================================================================
			void BrightnessContrast(double brightness, double contrast);
			//========================================================================================
			void BrightnessContrast(double brightness, double contrast, ImageMagick::Channels channels);
			//========================================================================================
			void CannyEdge(double radius, double sigma, double lower, double upper);
			//========================================================================================
			void CDL(String^ fileName);
			//========================================================================================
			void ChangeColorSpace(ImageMagick::ColorSpace value);
			//========================================================================================
			void Charcoal(double radius, double sigma);
			//========================================================================================
			void Chop(MagickGeometry^ geometry);
			//========================================================================================
			void ChromaBluePrimary(double x, double y);
			//========================================================================================
			void ChromaGreenPrimary(double x, double y);
			//========================================================================================
			void ChromaRedPrimary(double x, double y);
			//========================================================================================
			void ChromaWhitePoint(double x, double y);
			//========================================================================================
			void Clamp();
			//========================================================================================
			void Clamp(ImageMagick::Channels channels);
			//========================================================================================
			void Clip();
			//========================================================================================
			void Clip(String^ pathName, bool inside);
			//========================================================================================
			MagickImage^ Clone();
			//========================================================================================
			void Clut(MagickImage^ image, PixelInterpolateMethod method);
			//========================================================================================
			void Clut(MagickImage^ image, PixelInterpolateMethod method, ImageMagick::Channels channels);
			//========================================================================================
			void ColorAlpha(MagickColor^ color);
			//========================================================================================
			void Colorize(MagickColor^ color, int alphaRed, int alphaGreen, int alphaBlue);
			//========================================================================================
			MagickColor^ ColorMap(int index);
			//========================================================================================
			void ColorMap(int index, MagickColor^ color);
			//========================================================================================
			void ColorMatrix(ColorMatrix^ matrix);
			//========================================================================================
			MagickErrorInfo^ Compare(MagickImage^ image);
			//========================================================================================
			double Compare(MagickImage^ image, ErrorMetric metric, ImageMagick::Channels channels);
			//========================================================================================
			double Compare(MagickImage^ image, ErrorMetric metric, MagickImage^ difference, ImageMagick::Channels channels);
			//========================================================================================
			void Composite(MagickImage^ image, int x, int y, CompositeOperator compose);
			//========================================================================================
			void Composite(MagickImage^ image, MagickGeometry^ offset, CompositeOperator compose);
			//========================================================================================
			void Composite(MagickImage^ image, Gravity gravity, CompositeOperator compose);
			//========================================================================================
			void ConnectedComponents(int connectivity);
			//========================================================================================
			void Contrast(bool enhance);
			//========================================================================================
			void ContrastStretch(PointD contrast);
			//========================================================================================
			void ContrastStretch(PointD contrast, ImageMagick::Channels channels);
			//========================================================================================
			void Convolve(ConvolveMatrix^ convolveMatrix);
			//========================================================================================
			void Crop(MagickGeometry^ geometry);
			//========================================================================================
			IEnumerable<MagickImage^>^ CropToTiles(MagickGeometry^ geometry);
			//========================================================================================
			void CycleColormap(int amount);
			//========================================================================================
			void Decipher(String^ passphrase);
			//========================================================================================
			void Deskew(Magick::Quantum threshold);
			//========================================================================================
			void Despeckle();
			//========================================================================================
			ImageMagick::ColorType DetermineColorType();
			//========================================================================================
			void Distort(DistortMethod method, bool bestfit, array<double>^ arguments);
			//========================================================================================
			void Draw(IEnumerable<IDrawable^>^ drawables);
			//========================================================================================
			void Edge(double radius);
			//========================================================================================
			void Emboss(double radius, double sigma);
			//========================================================================================
			void Encipher(String^ passphrase);
			//========================================================================================
			void Enhance();
			//========================================================================================
			void Equalize();
			//========================================================================================
			virtual bool Equals(MagickImage^ other);
			//========================================================================================
			void Evaluate(ImageMagick::Channels channels, EvaluateOperator evaluateOperator, double value);
			//========================================================================================
			void Evaluate(ImageMagick::Channels channels, MagickGeometry^ geometry, EvaluateOperator evaluateOperator, double value);
			//========================================================================================
			void Extent(MagickGeometry^ geometry);
			//========================================================================================
			void Extent(MagickGeometry^ geometry, MagickColor^ backgroundColor);
			//========================================================================================
			void Extent(MagickGeometry^ geometry, Gravity gravity);
			//========================================================================================
			void Extent(MagickGeometry^ geometry, Gravity gravity, MagickColor^ backgroundColor);
			//========================================================================================
			void Flip();
			//========================================================================================
			void FloodFill(int alpha, int x, int y, bool invert);
			//========================================================================================
			void FloodFill(MagickColor^ color, int x, int y, bool invert);
			//========================================================================================
			void FloodFill(MagickColor^ color, int x, int y, MagickColor^ borderColor, bool invert);
			//========================================================================================
			void FloodFill(MagickColor^ color, MagickGeometry^ geometry, bool invert);
			//========================================================================================
			void FloodFill(MagickColor^ color, MagickGeometry^ geometry, MagickColor^ borderColor, bool invert);
			//========================================================================================
			void FloodFill(MagickImage^ image, int x, int y, bool invert);
			//========================================================================================
			void FloodFill(MagickImage^ image, int x, int y, MagickColor^ borderColor, bool invert);
			//========================================================================================
			void FloodFill(MagickImage^ image, MagickGeometry^ geometry, bool invert);
			//========================================================================================
			void FloodFill(MagickImage^ image, MagickGeometry^ geometry, MagickColor^ borderColor, bool invert);
			//========================================================================================
			void Flop();
			//========================================================================================
			TypeMetric^ FontTypeMetrics(String^ text, bool ignoreNewLines);
			//========================================================================================
			String^ FormatExpression(String^ expression);
			//========================================================================================
			void Frame(MagickGeometry^ geometry);
			//========================================================================================
			void Fx(String^ expression);
			//========================================================================================
			void Fx(String^ expression, ImageMagick::Channels channels);
			//========================================================================================
			void GammaCorrect(double gamma);
			//========================================================================================
			void GammaCorrect(double gammaRed, double gammaGreen, double gammaBlue);
			//========================================================================================
			void GaussianBlur(double width, double sigma);
			//========================================================================================
			void GaussianBlur(double width, double sigma, ImageMagick::Channels channels);
			//========================================================================================
			String^ GetArtifact(String^ name);
			//========================================================================================
			String^ GetAttribute(String^ name);
			//========================================================================================
			String^ GetDefine(String^ format, String^ name);
			//========================================================================================
			array<Byte>^ GetProfile(String^ name);
			//========================================================================================
			PixelCollection^ GetReadOnlyPixels(int x, int y, int width, int height);
			//========================================================================================
			WritablePixelCollection^ GetWritablePixels(int x, int y, int width, int height);
			//========================================================================================
			void Grayscale(PixelIntensityMethod method);
			//========================================================================================
			void HaldClut(MagickImage^ image);
			//========================================================================================
			IEnumerable<Tuple<MagickColor^, int>^>^ Histogram();
			//========================================================================================
			void HoughLine(int width, int height, int threshold);
			//========================================================================================
			void Implode(double factor);
			//========================================================================================
			void InverseFourierTransform(MagickImage^ image, bool magnitude);
			//========================================================================================
			void Kuwahara(double radius, double sigma);
			//========================================================================================
			QUANTUM_CLS_COMPLIANT void Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, double midpoint);
			//========================================================================================
			QUANTUM_CLS_COMPLIANT void Level(Magick::Quantum blackPoint, Magick::Quantum whitePoint, double midpoint, ImageMagick::Channels channels);
			//========================================================================================
			void LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, bool invert);
			//========================================================================================
			void LevelColors(MagickColor^ blackColor, MagickColor^ whiteColor, ImageMagick::Channels channels, bool invert);
			//========================================================================================
			void LinearStretch(Magick::Quantum blackPoint, Magick::Quantum whitePoint);
			//========================================================================================
			void LiquidRescale(MagickGeometry^ geometry);
			//========================================================================================
			void Magnify();
			//========================================================================================
			MagickErrorInfo^ Map(MagickImage^ image, QuantizeSettings^ settings);
			//========================================================================================
			void MedianFilter(double radius);
			//========================================================================================
			void Minify();
			//========================================================================================
			void Modulate(double brightness, double saturation, double hue);
			//========================================================================================
			Moments^ Moments();
			//========================================================================================
			void Morphology(MorphologyMethod method, Kernel kernel, String^ arguments);
			//========================================================================================
			void Morphology(MorphologyMethod method, Kernel kernel, String^ arguments, ImageMagick::Channels channels);
			//========================================================================================
			void Morphology(MorphologyMethod method, Kernel kernel, String^ arguments, ImageMagick::Channels channels, int iterations);
			//========================================================================================
			void Morphology(MorphologyMethod method, Kernel kernel, String^ arguments, int iterations);
			//========================================================================================
			void Morphology(MorphologyMethod method, String^ userKernel);
			//========================================================================================
			void Morphology(MorphologyMethod method, String^ userKernel, ImageMagick::Channels channels);
			//========================================================================================
			void Morphology(MorphologyMethod method, String^ userKernel, ImageMagick::Channels channels, int iterations);
			//========================================================================================
			void Morphology(MorphologyMethod method, String^ userKernel, int iterations);
			//========================================================================================
			void MotionBlur(double radius, double sigma, double angle);
			//========================================================================================
			void Negate(bool onlyGrayscale);
			//========================================================================================
			void Negate(ImageMagick::Channels channels, bool onlyGrayscale);
			//========================================================================================
			void Normalize();
			//========================================================================================
			void OilPaint(double radius);
			//========================================================================================
			void Opaque(MagickColor^ target, MagickColor^ fill, bool invert);
			//========================================================================================
			void OrderedDither(String^ thresholdMap);
			//========================================================================================
			void OrderedDither(String^ thresholdMap, ImageMagick::Channels channels);
			//========================================================================================
			void Perceptible(double epsilon);
			//========================================================================================
			void Perceptible(double epsilon, ImageMagick::Channels channels);
			//========================================================================================
			IEnumerable<ChannelPerceptualHash^>^ PerceptualHash();
			//========================================================================================
			void Ping(array<Byte>^ data);
			//========================================================================================
			void Ping(String^ fileName);
			//========================================================================================
			void Ping(Stream^ stream);
			//========================================================================================
			void Polaroid(String^ caption, double angle, PixelInterpolateMethod method);
			//========================================================================================
			void Posterize(int levels, DitherMethod method);
			//========================================================================================
			void Posterize(int levels, DitherMethod method, ImageMagick::Channels channels);
			//========================================================================================
			MagickErrorInfo^ Quantize(QuantizeSettings^ settings);
			//========================================================================================
			void RaiseOrLower(int size, bool raiseFlag);
			//========================================================================================
			void RandomThreshold(Magick::Quantum low, Magick::Quantum high, bool isPercentage);
			//========================================================================================
			void RandomThreshold(Magick::Quantum low, Magick::Quantum high, ImageMagick::Channels channels, bool isPercentage);
			//========================================================================================
			void Read(array<Byte>^ data, MagickReadSettings^ readSettings);
			//========================================================================================
			void Read(MagickColor^ color, int width, int height);
			//========================================================================================
			void Read(String^ fileName, int width, int height);
			//========================================================================================
			void Read(String^ fileName, MagickReadSettings^ readSettings);
			//========================================================================================
			void Read(Stream^ stream, MagickReadSettings^ readSettings);
			//========================================================================================
			void ReduceNoise(int order);
			//========================================================================================
			void RemoveDefine(String^ format, String^ name);
			//========================================================================================
			void RemoveProfile(String^ name);
			//========================================================================================
			void RePage();
			//========================================================================================
			void Resample(PointD density);
			//========================================================================================
			void Resize(MagickGeometry^ geometry);
			//========================================================================================
			void Roll(int xOffset, int yOffset);
			//========================================================================================
			void Rotate(double degrees);
			//========================================================================================
			void RotationalBlur(double angle);
			//========================================================================================
			void RotationalBlur(double angle, ImageMagick::Channels channels);
			//========================================================================================
			void Sample(MagickGeometry^ geometry);
			//========================================================================================
			void Scale(MagickGeometry^ geometry);
			//========================================================================================
			void Segment(ImageMagick::ColorSpace quantizeColorSpace, double clusterThreshold, double smoothingThreshold);
			//========================================================================================
			void SelectiveBlur(double radius, double sigma, double threshold);
			//========================================================================================
			void SelectiveBlur(double radius, double sigma, double threshold, ImageMagick::Channels channels);
			//========================================================================================
			IEnumerable<MagickImage^>^ Separate(ImageMagick::Channels channels);
			//========================================================================================
			void SepiaTone(Magick::Quantum threshold);
			//========================================================================================
			void SetArtifact(String^ name, String^ value);
			//========================================================================================
			void SetAttenuate(double attenuate);
			//========================================================================================
			void SetAttribute(String^ name, String^ value);
			//========================================================================================
			void SetDefine(String^ format, String^ name, String^ value);
			//========================================================================================
			void SetHighlightColor(MagickColor^ color);
			//========================================================================================
			void SetOption(String^ name, String^ value);
			//========================================================================================
			void SetLowlightColor(MagickColor^ color);
			//========================================================================================
			void Shade(double azimuth, double elevation, bool colorShading);
			//========================================================================================
			void Shadow(int x, int y, double sigma, double alpha);
			//========================================================================================
			void Shadow(int x, int y, double sigma, double alpha, MagickColor^ color);
			//========================================================================================
			void Sharpen(double radius, double sigma);
			//========================================================================================
			void Sharpen(double radius, double sigma, ImageMagick::Channels channels);
			//========================================================================================
			void Shave(int leftRight, int topBottom);
			//========================================================================================
			void Shear(double xAngle, double yAngle);
			//========================================================================================
			void SigmoidalContrast(bool sharpen, double contrast, double midpoint);
			//========================================================================================
			void Sketch(double radius, double sigma, double angle);
			//========================================================================================
			void Solarize(double factor);
			//========================================================================================
			void SparseColor(ImageMagick::Channels channels, SparseColorMethod method, IEnumerable<Internal::ISparseColorArg^>^ args);
			//========================================================================================
			void Splice(MagickGeometry^ geometry);
			//========================================================================================
			void Spread(int amount);
			//========================================================================================
			Statistics^ Statistics();
			//========================================================================================
			void Stegano(MagickImage^ watermark);
			//========================================================================================
			void Stereo(MagickImage^ rightImage);
			//========================================================================================
			void Strip();
			//========================================================================================
			MagickSearchResult^ SubImageSearch(MagickImage^ image, ErrorMetric metric, double similarityThreshold);
			//========================================================================================
			void Swirl(double degrees);
			//========================================================================================
			void Texture(MagickImage^ image);
			//========================================================================================
			void Threshold(Magick::Quantum threshold);
			//========================================================================================
			void Thumbnail(MagickGeometry^ geometry);
			//========================================================================================
			void Tint(String^ opacity);
			//========================================================================================
			Bitmap^ ToBitmap();
			//========================================================================================
#if !(NET20)
			BitmapSource^ ToBitmapSource();
#endif
			//========================================================================================
			array<Byte>^ ToByteArray();
			//========================================================================================
			void Transform(MagickGeometry^ imageGeometry);
			//========================================================================================
			void Transform(MagickGeometry^ imageGeometry, MagickGeometry^ cropGeometry);
			//========================================================================================
			void TransformOrigin(double x, double y);
			//========================================================================================
			void TransformRotation(double angle);
			//========================================================================================
			void TransformReset();
			//========================================================================================
			void TransformScale(double scaleX, double scaleY);
			//========================================================================================
			void TransformSkewX(double skewX);
			//========================================================================================
			void TransformSkewY(double skewY);
			//========================================================================================
			void Transparent(MagickColor^ color);
			//========================================================================================
			void TransparentChroma(MagickColor^ colorLow, MagickColor^ colorHigh);
			//========================================================================================
			void Transpose();
			//========================================================================================
			void Transverse();
			//========================================================================================
			void Trim();
			//========================================================================================
			MagickImage^ UniqueColors();
			//========================================================================================
			void Unsharpmask(double radius, double sigma, double amount, double threshold);
			//========================================================================================
			void Unsharpmask(double radius, double sigma, double amount, double threshold, ImageMagick::Channels channels);
			//========================================================================================
			void Vignette(double radius, double sigma, int x, int y);
			//========================================================================================
			void Wave(double amplitude, double length);
			//========================================================================================
			void WhiteThreshold(String^ threshold);
			//========================================================================================
			void WhiteThreshold(String^ threshold, ImageMagick::Channels channels);
			//========================================================================================
			void Write(Stream^ stream);
			//========================================================================================
			void Write(String^ fileName);
			//========================================================================================
			void Zoom(MagickGeometry^ geometry);
			//========================================================================================
		};
	}
}