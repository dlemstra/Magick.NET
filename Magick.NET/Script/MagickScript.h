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

#include "ScriptReadEventArgs.h"
#include "ScriptWriteEventArgs.h"
#include "..\MagickImage.h"
#include "..\Settings\MagickReadSettings.h"

using namespace System::Xml;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to execute a Magick Script Language file.
	///</summary>
	public ref class MagickScript sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static initonly XmlReaderSettings^ _ReaderSettings = CreateXmlReaderSettings();
		//===========================================================================================
		EventHandler<ScriptReadEventArgs^>^ _ReadHandler;
		XmlDocument^ _Script;
		EventHandler<ScriptWriteEventArgs^>^ _WriteHandler;
		//===========================================================================================
		static MagickGeometry^ CreateMagickGeometry(XmlElement^ element);
		//===========================================================================================
		MagickImage^ CreateMagickImage(XmlElement^ element);
		//===========================================================================================
		static MagickReadSettings^ CreateMagickReadSettings(XmlElement^ element);
		//===========================================================================================
		static XmlReaderSettings^ CreateXmlReaderSettings();
		//===========================================================================================
		void Execute(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		MagickImage^ Execute(XmlElement^ element, MagickImageCollection^ collection);
		//===========================================================================================
		MagickImage^ ExecuteCollection(XmlElement^ collectionElement);
		//===========================================================================================
		MagickImage^ ExecuteRead(XmlElement^ readElement, MagickImage^ image);
		//===========================================================================================
		void ExecuteWrite(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void Initialize(Stream^ stream);
		//===========================================================================================
		static bool OnlyContains(System::Collections::Hashtable^ arguments, ... array<Object^>^ keys);
		//===========================================================================================
#pragma region Generated Code.
		//===========================================================================================
		static void ExecuteAdaptiveBlur(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteAdaptiveThreshold(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteAddNoise(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteAddProfile(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteAdjoin(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteAnimationDelay(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteAnimationIterations(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteAnnotate(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteAntiAlias(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteAttribute(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteBackgroundColor(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteBlur(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteBorder(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteBorderColor(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteCDL(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteCharcoal(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteChop(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteChopHorizontal(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteChopVertical(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteChromaBluePrimary(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteChromaGreenPrimary(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteChromaRedPrimary(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteChromaWhitePoint(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteClassType(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void ExecuteClipMask(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteColorAlpha(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteColorFuzz(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteColorize(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteColorMap(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteColorMapSize(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteColorSpace(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteColorType(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteComment(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteCompose(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void ExecuteComposite(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteContrast(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteCrop(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteCycleColormap(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteDensity(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteDepth(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteDespeckle(MagickImage^ image);
		//===========================================================================================
		static void ExecuteEdge(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteEmboss(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteEndian(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteExifProfile(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteExtent(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFillColor(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void ExecuteFillPattern(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFillRule(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFilterType(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFlashPixView(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFlip(MagickImage^ image);
		//===========================================================================================
		void ExecuteFloodFill(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFlop(MagickImage^ image);
		//===========================================================================================
		static void ExecuteFont(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFontPointsize(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFormat(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFrame(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteFx(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteGamma(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteGaussianBlur(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteGifDisposeMethod(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void ExecuteHaldClut(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteHasMatte(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteImplode(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void ExecuteInverseFourierTransform(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteIsMonochrome(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteLabel(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteLevel(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteLower(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteMagnify(MagickImage^ image);
		//===========================================================================================
		void ExecuteMap(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteMatteColor(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteMedianFilter(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteMinify(MagickImage^ image);
		//===========================================================================================
		static void ExecuteModulate(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteModulusDepth(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteMotionBlur(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteNegate(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteNormalize(MagickImage^ image);
		//===========================================================================================
		static void ExecuteOilPaint(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteOrientation(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecutePage(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteQuality(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteQuantize(MagickImage^ image);
		//===========================================================================================
		static void ExecuteQuantizeColors(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteQuantizeColorSpace(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteQuantizeDither(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteQuantizeTreeDepth(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteQuantumOperator(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteRaise(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteRandomThreshold(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteReduceNoise(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteRemoveProfile(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteRenderingIntent(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteResize(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteResolutionUnits(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteRoll(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteRotate(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteSample(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteScale(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteSegment(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteSeparate(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteShade(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteShadow(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteSharpen(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteShave(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteShear(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteSigmoidalContrast(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteSolarize(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void ExecuteStegano(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void ExecuteStereo(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteStrip(MagickImage^ image);
		//===========================================================================================
		static void ExecuteStrokeAntiAlias(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteStrokeColor(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteStrokeDashOffset(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteStrokeLineCap(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteStrokeLineJoin(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteStrokeMiterLimit(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void ExecuteStrokePattern(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteStrokeWidth(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteSwirl(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTextEncoding(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		void ExecuteTexture(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteThreshold(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTileName(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTransform(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTransformOrigin(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTransformReset(MagickImage^ image);
		//===========================================================================================
		static void ExecuteTransformRotation(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTransformScale(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTransformSkewX(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTransformSkewY(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTransparent(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTransparentChroma(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteTrim(MagickImage^ image);
		//===========================================================================================
		static void ExecuteUnsharpmask(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteVerbose(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteVirtualPixelMethod(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteWave(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
		static void ExecuteZoom(XmlElement^ element, MagickImage^ image);
		//===========================================================================================
#pragma endregion
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickScript class using the specified filename.
		///</summary>
		///<param name="fileName">The fully qualified name of the script file, or the relative script file name.</param>
		MagickScript(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickScript class using the specified stream.
		///</summary>
		///<param name="stream">The stream to read the script data from.</param>
		MagickScript(Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Event that will be raised when the script needs an image to be read.
		///</summary>
		event EventHandler<ScriptReadEventArgs^>^ Read
		{
			void add(EventHandler<ScriptReadEventArgs^>^ handler);
			void raise(Object^ sender, ScriptReadEventArgs^ arguments);
			void remove(EventHandler<ScriptReadEventArgs^>^ handler);
		}
		///==========================================================================================
		///<summary>
		/// Event that will be raised when the script needs an image to be written.
		///</summary>
		event EventHandler<ScriptWriteEventArgs^>^ Write
		{
			void add(EventHandler<ScriptWriteEventArgs^>^ handler);
			void raise(Object^ sender, ScriptWriteEventArgs^ arguments);
			void remove(EventHandler<ScriptWriteEventArgs^>^ handler);
		}
		///==========================================================================================
		///<summary>
		/// Executes the script and returns the resulting image.
		///</summary>
		MagickImage^ Execute();
		///==========================================================================================
		///<summary>
		/// Executes the script using the specified image.
		///</summary>
		///<param name="image">The image to execute the script on.</param>
		void Execute(MagickImage^ image);
		//===========================================================================================
	};
	//==============================================================================================
}