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
#include "..\..\Helpers\XmlHelper.h"
#include "..\MagickScript.h"
#include "..\..\Drawables\DrawableAffine.h"
#include "..\..\Drawables\DrawableArc.h"
#include "..\..\Drawables\DrawableBezier.h"
#include "..\..\Drawables\DrawableCircle.h"
#include "..\..\Drawables\DrawableClipPath.h"
#include "..\..\Drawables\DrawableColor.h"
#include "..\..\Drawables\DrawableCompositeImage.h"
#include "..\..\Drawables\DrawableDashOffset.h"
#include "..\..\Drawables\DrawableEllipse.h"
#include "..\..\Drawables\DrawableFillColor.h"
#include "..\..\Drawables\DrawableFillOpacity.h"
#include "..\..\Drawables\DrawableFillRule.h"
#include "..\..\Drawables\DrawableFont.h"
#include "..\..\Drawables\DrawableGravity.h"
#include "..\..\Drawables\DrawableLine.h"
#include "..\..\Drawables\DrawableMatte.h"
#include "..\..\Drawables\DrawableMiterLimit.h"
#include "..\..\Drawables\DrawablePath.h"
#include "..\..\Drawables\DrawablePoint.h"
#include "..\..\Drawables\DrawablePointSize.h"
#include "..\..\Drawables\DrawablePolygon.h"
#include "..\..\Drawables\DrawablePolyline.h"
#include "..\..\Drawables\DrawablePushClipPath.h"
#include "..\..\Drawables\DrawablePushPattern.h"
#include "..\..\Drawables\DrawableRectangle.h"
#include "..\..\Drawables\DrawableRotation.h"
#include "..\..\Drawables\DrawableRoundRectangle.h"
#include "..\..\Drawables\DrawableScaling.h"
#include "..\..\Drawables\DrawableSkewX.h"
#include "..\..\Drawables\DrawableSkewY.h"
#include "..\..\Drawables\DrawableStrokeAntialias.h"
#include "..\..\Drawables\DrawableStrokeColor.h"
#include "..\..\Drawables\DrawableStrokeLineCap.h"
#include "..\..\Drawables\DrawableStrokeLineJoin.h"
#include "..\..\Drawables\DrawableStrokeOpacity.h"
#include "..\..\Drawables\DrawableStrokeWidth.h"
#include "..\..\Drawables\DrawableText.h"
#include "..\..\Drawables\DrawableTextAntialias.h"
#include "..\..\Drawables\DrawableTextDecoration.h"
#include "..\..\Drawables\DrawableTextUnderColor.h"
#include "..\..\Drawables\DrawableTranslation.h"
#include "..\..\Drawables\DrawableViewbox.h"
#include "..\..\Drawables\Paths\PathArcAbs.h"
#include "..\..\Drawables\Paths\PathArcRel.h"
#include "..\..\Drawables\Paths\PathCurvetoAbs.h"
#include "..\..\Drawables\Paths\PathCurvetoRel.h"
#include "..\..\Drawables\Paths\PathLinetoAbs.h"
#include "..\..\Drawables\Paths\PathLinetoHorizontalAbs.h"
#include "..\..\Drawables\Paths\PathLinetoHorizontalRel.h"
#include "..\..\Drawables\Paths\PathLinetoRel.h"
#include "..\..\Drawables\Paths\PathLinetoVerticalAbs.h"
#include "..\..\Drawables\Paths\PathLinetoVerticalRel.h"
#include "..\..\Drawables\Paths\PathMovetoAbs.h"
#include "..\..\Drawables\Paths\PathMovetoRel.h"
#include "..\..\Drawables\Paths\PathQuadraticCurvetoAbs.h"
#include "..\..\Drawables\Paths\PathQuadraticCurvetoRel.h"
#include "..\..\Drawables\Paths\PathSmoothCurvetoAbs.h"
#include "..\..\Drawables\Paths\PathSmoothCurvetoRel.h"
#include "..\..\Drawables\Paths\PathSmoothQuadraticCurvetoAbs.h"
#include "..\..\Drawables\Paths\PathSmoothQuadraticCurvetoRel.h"
#include "..\..\Drawables\Coordinate.h"
#pragma warning (disable: 4100)
namespace ImageMagick
{
	void MagickScript::InitializeExecute()
	{
		InitializeExecuteImage();
		InitializeExecuteDrawable();
	}
	System::Collections::Hashtable^ MagickScript::InitializeStaticExecuteImage()
	{
		System::Collections::Hashtable^ result = gcnew System::Collections::Hashtable();
		result["adjoin"] = gcnew ExecuteElementImage(MagickScript::ExecuteAdjoin);
		result["animationDelay"] = gcnew ExecuteElementImage(MagickScript::ExecuteAnimationDelay);
		result["animationIterations"] = gcnew ExecuteElementImage(MagickScript::ExecuteAnimationIterations);
		result["antiAlias"] = gcnew ExecuteElementImage(MagickScript::ExecuteAntiAlias);
		result["backgroundColor"] = gcnew ExecuteElementImage(MagickScript::ExecuteBackgroundColor);
		result["borderColor"] = gcnew ExecuteElementImage(MagickScript::ExecuteBorderColor);
		result["boxColor"] = gcnew ExecuteElementImage(MagickScript::ExecuteBoxColor);
		result["classType"] = gcnew ExecuteElementImage(MagickScript::ExecuteClassType);
		result["colorFuzz"] = gcnew ExecuteElementImage(MagickScript::ExecuteColorFuzz);
		result["colorMapSize"] = gcnew ExecuteElementImage(MagickScript::ExecuteColorMapSize);
		result["colorSpace"] = gcnew ExecuteElementImage(MagickScript::ExecuteColorSpace);
		result["colorType"] = gcnew ExecuteElementImage(MagickScript::ExecuteColorType);
		result["comment"] = gcnew ExecuteElementImage(MagickScript::ExecuteComment);
		result["compose"] = gcnew ExecuteElementImage(MagickScript::ExecuteCompose);
		result["compressionMethod"] = gcnew ExecuteElementImage(MagickScript::ExecuteCompressionMethod);
		result["density"] = gcnew ExecuteElementImage(MagickScript::ExecuteDensity);
		result["endian"] = gcnew ExecuteElementImage(MagickScript::ExecuteEndian);
		result["fillColor"] = gcnew ExecuteElementImage(MagickScript::ExecuteFillColor);
		result["fillRule"] = gcnew ExecuteElementImage(MagickScript::ExecuteFillRule);
		result["filterType"] = gcnew ExecuteElementImage(MagickScript::ExecuteFilterType);
		result["flashPixView"] = gcnew ExecuteElementImage(MagickScript::ExecuteFlashPixView);
		result["font"] = gcnew ExecuteElementImage(MagickScript::ExecuteFont);
		result["fontPointsize"] = gcnew ExecuteElementImage(MagickScript::ExecuteFontPointsize);
		result["format"] = gcnew ExecuteElementImage(MagickScript::ExecuteFormat);
		result["gifDisposeMethod"] = gcnew ExecuteElementImage(MagickScript::ExecuteGifDisposeMethod);
		result["hasMatte"] = gcnew ExecuteElementImage(MagickScript::ExecuteHasMatte);
		result["isMonochrome"] = gcnew ExecuteElementImage(MagickScript::ExecuteIsMonochrome);
		result["label"] = gcnew ExecuteElementImage(MagickScript::ExecuteLabel);
		result["matteColor"] = gcnew ExecuteElementImage(MagickScript::ExecuteMatteColor);
		result["modulusDepth"] = gcnew ExecuteElementImage(MagickScript::ExecuteModulusDepth);
		result["orientation"] = gcnew ExecuteElementImage(MagickScript::ExecuteOrientation);
		result["page"] = gcnew ExecuteElementImage(MagickScript::ExecutePage);
		result["quality"] = gcnew ExecuteElementImage(MagickScript::ExecuteQuality);
		result["quantizeColors"] = gcnew ExecuteElementImage(MagickScript::ExecuteQuantizeColors);
		result["quantizeColorSpace"] = gcnew ExecuteElementImage(MagickScript::ExecuteQuantizeColorSpace);
		result["quantizeDither"] = gcnew ExecuteElementImage(MagickScript::ExecuteQuantizeDither);
		result["quantizeTreeDepth"] = gcnew ExecuteElementImage(MagickScript::ExecuteQuantizeTreeDepth);
		result["renderingIntent"] = gcnew ExecuteElementImage(MagickScript::ExecuteRenderingIntent);
		result["resolutionUnits"] = gcnew ExecuteElementImage(MagickScript::ExecuteResolutionUnits);
		result["strokeAntiAlias"] = gcnew ExecuteElementImage(MagickScript::ExecuteStrokeAntiAlias);
		result["strokeColor"] = gcnew ExecuteElementImage(MagickScript::ExecuteStrokeColor);
		result["strokeDashOffset"] = gcnew ExecuteElementImage(MagickScript::ExecuteStrokeDashOffset);
		result["strokeLineCap"] = gcnew ExecuteElementImage(MagickScript::ExecuteStrokeLineCap);
		result["strokeLineJoin"] = gcnew ExecuteElementImage(MagickScript::ExecuteStrokeLineJoin);
		result["strokeMiterLimit"] = gcnew ExecuteElementImage(MagickScript::ExecuteStrokeMiterLimit);
		result["strokeWidth"] = gcnew ExecuteElementImage(MagickScript::ExecuteStrokeWidth);
		result["textEncoding"] = gcnew ExecuteElementImage(MagickScript::ExecuteTextEncoding);
		result["tileName"] = gcnew ExecuteElementImage(MagickScript::ExecuteTileName);
		result["verbose"] = gcnew ExecuteElementImage(MagickScript::ExecuteVerbose);
		result["virtualPixelMethod"] = gcnew ExecuteElementImage(MagickScript::ExecuteVirtualPixelMethod);
		result["adaptiveBlur"] = gcnew ExecuteElementImage(MagickScript::ExecuteAdaptiveBlur);
		result["adaptiveThreshold"] = gcnew ExecuteElementImage(MagickScript::ExecuteAdaptiveThreshold);
		result["addNoise"] = gcnew ExecuteElementImage(MagickScript::ExecuteAddNoise);
		result["addProfile"] = gcnew ExecuteElementImage(MagickScript::ExecuteAddProfile);
		result["annotate"] = gcnew ExecuteElementImage(MagickScript::ExecuteAnnotate);
		result["blur"] = gcnew ExecuteElementImage(MagickScript::ExecuteBlur);
		result["border"] = gcnew ExecuteElementImage(MagickScript::ExecuteBorder);
		result["cdl"] = gcnew ExecuteElementImage(MagickScript::ExecuteCDL);
		result["charcoal"] = gcnew ExecuteElementImage(MagickScript::ExecuteCharcoal);
		result["chop"] = gcnew ExecuteElementImage(MagickScript::ExecuteChop);
		result["chopHorizontal"] = gcnew ExecuteElementImage(MagickScript::ExecuteChopHorizontal);
		result["chopVertical"] = gcnew ExecuteElementImage(MagickScript::ExecuteChopVertical);
		result["chromaBluePrimary"] = gcnew ExecuteElementImage(MagickScript::ExecuteChromaBluePrimary);
		result["chromaGreenPrimary"] = gcnew ExecuteElementImage(MagickScript::ExecuteChromaGreenPrimary);
		result["chromaRedPrimary"] = gcnew ExecuteElementImage(MagickScript::ExecuteChromaRedPrimary);
		result["chromaWhitePoint"] = gcnew ExecuteElementImage(MagickScript::ExecuteChromaWhitePoint);
		result["colorAlpha"] = gcnew ExecuteElementImage(MagickScript::ExecuteColorAlpha);
		result["colorize"] = gcnew ExecuteElementImage(MagickScript::ExecuteColorize);
		result["colorMap"] = gcnew ExecuteElementImage(MagickScript::ExecuteColorMap);
		result["contrast"] = gcnew ExecuteElementImage(MagickScript::ExecuteContrast);
		result["crop"] = gcnew ExecuteElementImage(MagickScript::ExecuteCrop);
		result["cycleColormap"] = gcnew ExecuteElementImage(MagickScript::ExecuteCycleColormap);
		result["depth"] = gcnew ExecuteElementImage(MagickScript::ExecuteDepth);
		result["despeckle"] = gcnew ExecuteElementImage(MagickScript::ExecuteDespeckle);
		result["edge"] = gcnew ExecuteElementImage(MagickScript::ExecuteEdge);
		result["emboss"] = gcnew ExecuteElementImage(MagickScript::ExecuteEmboss);
		result["extent"] = gcnew ExecuteElementImage(MagickScript::ExecuteExtent);
		result["flip"] = gcnew ExecuteElementImage(MagickScript::ExecuteFlip);
		result["flop"] = gcnew ExecuteElementImage(MagickScript::ExecuteFlop);
		result["frame"] = gcnew ExecuteElementImage(MagickScript::ExecuteFrame);
		result["fx"] = gcnew ExecuteElementImage(MagickScript::ExecuteFx);
		result["gamma"] = gcnew ExecuteElementImage(MagickScript::ExecuteGamma);
		result["gaussianBlur"] = gcnew ExecuteElementImage(MagickScript::ExecuteGaussianBlur);
		result["implode"] = gcnew ExecuteElementImage(MagickScript::ExecuteImplode);
		result["level"] = gcnew ExecuteElementImage(MagickScript::ExecuteLevel);
		result["lower"] = gcnew ExecuteElementImage(MagickScript::ExecuteLower);
		result["magnify"] = gcnew ExecuteElementImage(MagickScript::ExecuteMagnify);
		result["medianFilter"] = gcnew ExecuteElementImage(MagickScript::ExecuteMedianFilter);
		result["minify"] = gcnew ExecuteElementImage(MagickScript::ExecuteMinify);
		result["modulate"] = gcnew ExecuteElementImage(MagickScript::ExecuteModulate);
		result["motionBlur"] = gcnew ExecuteElementImage(MagickScript::ExecuteMotionBlur);
		result["negate"] = gcnew ExecuteElementImage(MagickScript::ExecuteNegate);
		result["normalize"] = gcnew ExecuteElementImage(MagickScript::ExecuteNormalize);
		result["oilPaint"] = gcnew ExecuteElementImage(MagickScript::ExecuteOilPaint);
		result["quantize"] = gcnew ExecuteElementImage(MagickScript::ExecuteQuantize);
		result["quantumOperator"] = gcnew ExecuteElementImage(MagickScript::ExecuteQuantumOperator);
		result["raise"] = gcnew ExecuteElementImage(MagickScript::ExecuteRaise);
		result["randomThreshold"] = gcnew ExecuteElementImage(MagickScript::ExecuteRandomThreshold);
		result["reduceNoise"] = gcnew ExecuteElementImage(MagickScript::ExecuteReduceNoise);
		result["removeProfile"] = gcnew ExecuteElementImage(MagickScript::ExecuteRemoveProfile);
		result["resize"] = gcnew ExecuteElementImage(MagickScript::ExecuteResize);
		result["roll"] = gcnew ExecuteElementImage(MagickScript::ExecuteRoll);
		result["rotate"] = gcnew ExecuteElementImage(MagickScript::ExecuteRotate);
		result["sample"] = gcnew ExecuteElementImage(MagickScript::ExecuteSample);
		result["scale"] = gcnew ExecuteElementImage(MagickScript::ExecuteScale);
		result["segment"] = gcnew ExecuteElementImage(MagickScript::ExecuteSegment);
		result["setAttribute"] = gcnew ExecuteElementImage(MagickScript::ExecuteSetAttribute);
		result["setOption"] = gcnew ExecuteElementImage(MagickScript::ExecuteSetOption);
		result["shade"] = gcnew ExecuteElementImage(MagickScript::ExecuteShade);
		result["shadow"] = gcnew ExecuteElementImage(MagickScript::ExecuteShadow);
		result["sharpen"] = gcnew ExecuteElementImage(MagickScript::ExecuteSharpen);
		result["shave"] = gcnew ExecuteElementImage(MagickScript::ExecuteShave);
		result["shear"] = gcnew ExecuteElementImage(MagickScript::ExecuteShear);
		result["sigmoidalContrast"] = gcnew ExecuteElementImage(MagickScript::ExecuteSigmoidalContrast);
		result["solarize"] = gcnew ExecuteElementImage(MagickScript::ExecuteSolarize);
		result["strip"] = gcnew ExecuteElementImage(MagickScript::ExecuteStrip);
		result["swirl"] = gcnew ExecuteElementImage(MagickScript::ExecuteSwirl);
		result["threshold"] = gcnew ExecuteElementImage(MagickScript::ExecuteThreshold);
		result["transform"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransform);
		result["transformOrigin"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformOrigin);
		result["transformReset"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformReset);
		result["transformRotation"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformRotation);
		result["transformScale"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformScale);
		result["transformSkewX"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformSkewX);
		result["transformSkewY"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformSkewY);
		result["transparent"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransparent);
		result["transparentChroma"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransparentChroma);
		result["trim"] = gcnew ExecuteElementImage(MagickScript::ExecuteTrim);
		result["unsharpmask"] = gcnew ExecuteElementImage(MagickScript::ExecuteUnsharpmask);
		result["wave"] = gcnew ExecuteElementImage(MagickScript::ExecuteWave);
		result["zoom"] = gcnew ExecuteElementImage(MagickScript::ExecuteZoom);
		return result;
	}
	void MagickScript::InitializeExecuteImage()
	{
		_ExecuteImage = gcnew System::Collections::Hashtable();
		_ExecuteImage["clipMask"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteClipMask);
		_ExecuteImage["fillPattern"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteFillPattern);
		_ExecuteImage["strokePattern"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteStrokePattern);
		_ExecuteImage["composite"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteComposite);
		_ExecuteImage["floodFill"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteFloodFill);
		_ExecuteImage["haldClut"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteHaldClut);
		_ExecuteImage["inverseFourierTransform"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteInverseFourierTransform);
		_ExecuteImage["map"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteMap);
		_ExecuteImage["stegano"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteStegano);
		_ExecuteImage["stereo"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteStereo);
		_ExecuteImage["texture"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteTexture);
		_ExecuteImage["clone"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteClone);
		_ExecuteImage["draw"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteDraw);
		_ExecuteImage["write"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteWrite);
	}
	System::Collections::Hashtable^ MagickScript::InitializeStaticExecuteCollection()
	{
		System::Collections::Hashtable^ result = gcnew System::Collections::Hashtable();
		result["coalesce"] = gcnew ExecuteElementCollection(MagickScript::ExecuteCoalesce);
		result["deconstruct"] = gcnew ExecuteElementCollection(MagickScript::ExecuteDeconstruct);
		result["optimize"] = gcnew ExecuteElementCollection(MagickScript::ExecuteOptimize);
		result["optimizePlus"] = gcnew ExecuteElementCollection(MagickScript::ExecuteOptimizePlus);
		result["rePage"] = gcnew ExecuteElementCollection(MagickScript::ExecuteRePage);
		result["appendHorizontally"] = gcnew ExecuteElementCollection(MagickScript::ExecuteAppendHorizontally);
		result["appendVertically"] = gcnew ExecuteElementCollection(MagickScript::ExecuteAppendVertically);
		result["combine"] = gcnew ExecuteElementCollection(MagickScript::ExecuteCombine);
		result["evaluate"] = gcnew ExecuteElementCollection(MagickScript::ExecuteEvaluate);
		result["flatten"] = gcnew ExecuteElementCollection(MagickScript::ExecuteFlatten);
		result["merge"] = gcnew ExecuteElementCollection(MagickScript::ExecuteMerge);
		result["mosaic"] = gcnew ExecuteElementCollection(MagickScript::ExecuteMosaic);
		result["trimBounds"] = gcnew ExecuteElementCollection(MagickScript::ExecuteTrimBounds);
		return result;
	}
	System::Collections::Hashtable^ MagickScript::InitializeStaticExecuteDrawable()
	{
		System::Collections::Hashtable^ result = gcnew System::Collections::Hashtable();
		result["affine"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteAffine);
		result["arc"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteArc);
		result["bezier"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteBezier);
		result["circle"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteCircle);
		result["clipPath"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteClipPath);
		result["color"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteColor);
		result["dashOffset"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteDashOffset);
		result["ellipse"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteEllipse);
		result["fillColor"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteFillColor);
		result["fillOpacity"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteFillOpacity);
		result["fillRule"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteFillRule);
		result["font"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteFont);
		result["gravity"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteGravity);
		result["line"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteLine);
		result["matte"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteMatte);
		result["miterLimit"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteMiterLimit);
		result["path"] = gcnew ExecuteElementDrawable(MagickScript::ExecutePath);
		result["point"] = gcnew ExecuteElementDrawable(MagickScript::ExecutePoint);
		result["pointSize"] = gcnew ExecuteElementDrawable(MagickScript::ExecutePointSize);
		result["polygon"] = gcnew ExecuteElementDrawable(MagickScript::ExecutePolygon);
		result["polyline"] = gcnew ExecuteElementDrawable(MagickScript::ExecutePolyline);
		result["pushClipPath"] = gcnew ExecuteElementDrawable(MagickScript::ExecutePushClipPath);
		result["pushPattern"] = gcnew ExecuteElementDrawable(MagickScript::ExecutePushPattern);
		result["rectangle"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteRectangle);
		result["rotation"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteRotation);
		result["roundRectangle"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteRoundRectangle);
		result["scaling"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteScaling);
		result["skewX"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteSkewX);
		result["skewY"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteSkewY);
		result["strokeAntialias"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteStrokeAntialias);
		result["strokeColor"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteStrokeColor);
		result["strokeLineCap"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteStrokeLineCap);
		result["strokeLineJoin"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteStrokeLineJoin);
		result["strokeOpacity"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteStrokeOpacity);
		result["strokeWidth"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteStrokeWidth);
		result["text"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteText);
		result["textAntialias"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteTextAntialias);
		result["textDecoration"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteTextDecoration);
		result["textUnderColor"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteTextUnderColor);
		result["translation"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteTranslation);
		result["viewbox"] = gcnew ExecuteElementDrawable(MagickScript::ExecuteViewbox);
		return result;
	}
	void MagickScript::InitializeExecuteDrawable()
	{
		_ExecuteDrawable = gcnew System::Collections::Hashtable();
		_ExecuteDrawable["compositeImage"] = gcnew ExecuteElementDrawable(this, &MagickScript::ExecuteCompositeImage);
	}
	System::Collections::Hashtable^ MagickScript::InitializeStaticExecutePath()
	{
		System::Collections::Hashtable^ result = gcnew System::Collections::Hashtable();
		result["arcAbs"] = gcnew ExecuteElementPath(MagickScript::ExecuteArcAbs);
		result["arcRel"] = gcnew ExecuteElementPath(MagickScript::ExecuteArcRel);
		result["curvetoAbs"] = gcnew ExecuteElementPath(MagickScript::ExecuteCurvetoAbs);
		result["curvetoRel"] = gcnew ExecuteElementPath(MagickScript::ExecuteCurvetoRel);
		result["linetoAbs"] = gcnew ExecuteElementPath(MagickScript::ExecuteLinetoAbs);
		result["linetoHorizontalAbs"] = gcnew ExecuteElementPath(MagickScript::ExecuteLinetoHorizontalAbs);
		result["linetoHorizontalRel"] = gcnew ExecuteElementPath(MagickScript::ExecuteLinetoHorizontalRel);
		result["linetoRel"] = gcnew ExecuteElementPath(MagickScript::ExecuteLinetoRel);
		result["linetoVerticalAbs"] = gcnew ExecuteElementPath(MagickScript::ExecuteLinetoVerticalAbs);
		result["linetoVerticalRel"] = gcnew ExecuteElementPath(MagickScript::ExecuteLinetoVerticalRel);
		result["movetoAbs"] = gcnew ExecuteElementPath(MagickScript::ExecuteMovetoAbs);
		result["movetoRel"] = gcnew ExecuteElementPath(MagickScript::ExecuteMovetoRel);
		result["quadraticCurvetoAbs"] = gcnew ExecuteElementPath(MagickScript::ExecuteQuadraticCurvetoAbs);
		result["quadraticCurvetoRel"] = gcnew ExecuteElementPath(MagickScript::ExecuteQuadraticCurvetoRel);
		result["smoothCurvetoAbs"] = gcnew ExecuteElementPath(MagickScript::ExecuteSmoothCurvetoAbs);
		result["smoothCurvetoRel"] = gcnew ExecuteElementPath(MagickScript::ExecuteSmoothCurvetoRel);
		result["smoothQuadraticCurvetoAbs"] = gcnew ExecuteElementPath(MagickScript::ExecuteSmoothQuadraticCurvetoAbs);
		result["smoothQuadraticCurvetoRel"] = gcnew ExecuteElementPath(MagickScript::ExecuteSmoothQuadraticCurvetoRel);
		return result;
	}
	void MagickScript::ExecuteImage(XmlElement^ element, MagickImage^ image)
	{
		ExecuteElementImage^ method = dynamic_cast<ExecuteElementImage^>(_StaticExecuteImage[element->Name]);
		if (method == nullptr)
			method = dynamic_cast<ExecuteElementImage^>(_ExecuteImage[element->Name]);
		if (method == nullptr)
			throw gcnew NotImplementedException(element->Name);
		method(element,image);
	}
	void MagickScript::ExecuteAdjoin(XmlElement^ element, MagickImage^ image)
	{
		image->Adjoin = XmlHelper::GetAttribute<bool>(element, "value");
	}
	void MagickScript::ExecuteAnimationDelay(XmlElement^ element, MagickImage^ image)
	{
		image->AnimationDelay = XmlHelper::GetAttribute<int>(element, "value");
	}
	void MagickScript::ExecuteAnimationIterations(XmlElement^ element, MagickImage^ image)
	{
		image->AnimationIterations = XmlHelper::GetAttribute<int>(element, "value");
	}
	void MagickScript::ExecuteAntiAlias(XmlElement^ element, MagickImage^ image)
	{
		image->AntiAlias = XmlHelper::GetAttribute<bool>(element, "value");
	}
	void MagickScript::ExecuteBackgroundColor(XmlElement^ element, MagickImage^ image)
	{
		image->BackgroundColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	void MagickScript::ExecuteBorderColor(XmlElement^ element, MagickImage^ image)
	{
		image->BorderColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	void MagickScript::ExecuteBoxColor(XmlElement^ element, MagickImage^ image)
	{
		image->BoxColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	void MagickScript::ExecuteClassType(XmlElement^ element, MagickImage^ image)
	{
		image->ClassType = XmlHelper::GetAttribute<ClassType>(element, "value");
	}
	void MagickScript::ExecuteClipMask(XmlElement^ element, MagickImage^ image)
	{
		image->ClipMask = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
	}
	void MagickScript::ExecuteColorFuzz(XmlElement^ element, MagickImage^ image)
	{
		image->ColorFuzz = XmlHelper::GetAttribute<double>(element, "value");
	}
	void MagickScript::ExecuteColorMapSize(XmlElement^ element, MagickImage^ image)
	{
		image->ColorMapSize = XmlHelper::GetAttribute<int>(element, "value");
	}
	void MagickScript::ExecuteColorSpace(XmlElement^ element, MagickImage^ image)
	{
		image->ColorSpace = XmlHelper::GetAttribute<ColorSpace>(element, "value");
	}
	void MagickScript::ExecuteColorType(XmlElement^ element, MagickImage^ image)
	{
		image->ColorType = XmlHelper::GetAttribute<ColorType>(element, "value");
	}
	void MagickScript::ExecuteComment(XmlElement^ element, MagickImage^ image)
	{
		image->Comment = XmlHelper::GetAttribute<String^>(element, "value");
	}
	void MagickScript::ExecuteCompose(XmlElement^ element, MagickImage^ image)
	{
		image->Compose = XmlHelper::GetAttribute<CompositeOperator>(element, "value");
	}
	void MagickScript::ExecuteCompressionMethod(XmlElement^ element, MagickImage^ image)
	{
		image->CompressionMethod = XmlHelper::GetAttribute<CompressionMethod>(element, "value");
	}
	void MagickScript::ExecuteDensity(XmlElement^ element, MagickImage^ image)
	{
		image->Density = XmlHelper::GetAttribute<MagickGeometry^>(element, "value");
	}
	void MagickScript::ExecuteEndian(XmlElement^ element, MagickImage^ image)
	{
		image->Endian = XmlHelper::GetAttribute<Endian>(element, "value");
	}
	void MagickScript::ExecuteFillColor(XmlElement^ element, MagickImage^ image)
	{
		image->FillColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	void MagickScript::ExecuteFillPattern(XmlElement^ element, MagickImage^ image)
	{
		image->FillPattern = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
	}
	void MagickScript::ExecuteFillRule(XmlElement^ element, MagickImage^ image)
	{
		image->FillRule = XmlHelper::GetAttribute<FillRule>(element, "value");
	}
	void MagickScript::ExecuteFilterType(XmlElement^ element, MagickImage^ image)
	{
		image->FilterType = XmlHelper::GetAttribute<FilterType>(element, "value");
	}
	void MagickScript::ExecuteFlashPixView(XmlElement^ element, MagickImage^ image)
	{
		image->FlashPixView = XmlHelper::GetAttribute<String^>(element, "value");
	}
	void MagickScript::ExecuteFont(XmlElement^ element, MagickImage^ image)
	{
		image->Font = XmlHelper::GetAttribute<String^>(element, "value");
	}
	void MagickScript::ExecuteFontPointsize(XmlElement^ element, MagickImage^ image)
	{
		image->FontPointsize = XmlHelper::GetAttribute<double>(element, "value");
	}
	void MagickScript::ExecuteFormat(XmlElement^ element, MagickImage^ image)
	{
		image->Format = XmlHelper::GetAttribute<MagickFormat>(element, "value");
	}
	void MagickScript::ExecuteGifDisposeMethod(XmlElement^ element, MagickImage^ image)
	{
		image->GifDisposeMethod = XmlHelper::GetAttribute<GifDisposeMethod>(element, "value");
	}
	void MagickScript::ExecuteHasMatte(XmlElement^ element, MagickImage^ image)
	{
		image->HasMatte = XmlHelper::GetAttribute<bool>(element, "value");
	}
	void MagickScript::ExecuteIsMonochrome(XmlElement^ element, MagickImage^ image)
	{
		image->IsMonochrome = XmlHelper::GetAttribute<bool>(element, "value");
	}
	void MagickScript::ExecuteLabel(XmlElement^ element, MagickImage^ image)
	{
		image->Label = XmlHelper::GetAttribute<String^>(element, "value");
	}
	void MagickScript::ExecuteMatteColor(XmlElement^ element, MagickImage^ image)
	{
		image->MatteColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	void MagickScript::ExecuteModulusDepth(XmlElement^ element, MagickImage^ image)
	{
		image->ModulusDepth = XmlHelper::GetAttribute<int>(element, "value");
	}
	void MagickScript::ExecuteOrientation(XmlElement^ element, MagickImage^ image)
	{
		image->Orientation = XmlHelper::GetAttribute<OrientationType>(element, "value");
	}
	void MagickScript::ExecutePage(XmlElement^ element, MagickImage^ image)
	{
		image->Page = XmlHelper::GetAttribute<MagickGeometry^>(element, "value");
	}
	void MagickScript::ExecuteQuality(XmlElement^ element, MagickImage^ image)
	{
		image->Quality = XmlHelper::GetAttribute<int>(element, "value");
	}
	void MagickScript::ExecuteQuantizeColors(XmlElement^ element, MagickImage^ image)
	{
		image->QuantizeColors = XmlHelper::GetAttribute<int>(element, "value");
	}
	void MagickScript::ExecuteQuantizeColorSpace(XmlElement^ element, MagickImage^ image)
	{
		image->QuantizeColorSpace = XmlHelper::GetAttribute<ColorSpace>(element, "value");
	}
	void MagickScript::ExecuteQuantizeDither(XmlElement^ element, MagickImage^ image)
	{
		image->QuantizeDither = XmlHelper::GetAttribute<bool>(element, "value");
	}
	void MagickScript::ExecuteQuantizeTreeDepth(XmlElement^ element, MagickImage^ image)
	{
		image->QuantizeTreeDepth = XmlHelper::GetAttribute<int>(element, "value");
	}
	void MagickScript::ExecuteRenderingIntent(XmlElement^ element, MagickImage^ image)
	{
		image->RenderingIntent = XmlHelper::GetAttribute<RenderingIntent>(element, "value");
	}
	void MagickScript::ExecuteResolutionUnits(XmlElement^ element, MagickImage^ image)
	{
		image->ResolutionUnits = XmlHelper::GetAttribute<Resolution>(element, "value");
	}
	void MagickScript::ExecuteStrokeAntiAlias(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeAntiAlias = XmlHelper::GetAttribute<bool>(element, "value");
	}
	void MagickScript::ExecuteStrokeColor(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	void MagickScript::ExecuteStrokeDashOffset(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeDashOffset = XmlHelper::GetAttribute<double>(element, "value");
	}
	void MagickScript::ExecuteStrokeLineCap(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeLineCap = XmlHelper::GetAttribute<LineCap>(element, "value");
	}
	void MagickScript::ExecuteStrokeLineJoin(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeLineJoin = XmlHelper::GetAttribute<LineJoin>(element, "value");
	}
	void MagickScript::ExecuteStrokeMiterLimit(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeMiterLimit = XmlHelper::GetAttribute<int>(element, "value");
	}
	void MagickScript::ExecuteStrokePattern(XmlElement^ element, MagickImage^ image)
	{
		image->StrokePattern = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
	}
	void MagickScript::ExecuteStrokeWidth(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeWidth = XmlHelper::GetAttribute<double>(element, "value");
	}
	void MagickScript::ExecuteTextEncoding(XmlElement^ element, MagickImage^ image)
	{
		image->TextEncoding = XmlHelper::GetAttribute<Encoding^>(element, "value");
	}
	void MagickScript::ExecuteTileName(XmlElement^ element, MagickImage^ image)
	{
		image->TileName = XmlHelper::GetAttribute<String^>(element, "value");
	}
	void MagickScript::ExecuteVerbose(XmlElement^ element, MagickImage^ image)
	{
		image->Verbose = XmlHelper::GetAttribute<bool>(element, "value");
	}
	void MagickScript::ExecuteVirtualPixelMethod(XmlElement^ element, MagickImage^ image)
	{
		image->VirtualPixelMethod = XmlHelper::GetAttribute<VirtualPixelMethod>(element, "value");
	}
	void MagickScript::ExecuteAdaptiveBlur(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->AdaptiveBlur();
		else if (OnlyContains(arguments, "radius", "sigma"))
			image->AdaptiveBlur((double)arguments["radius"], (double)arguments["sigma"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'adaptiveBlur', allowed combinations are: [] [radius, sigma]");
	}
	void MagickScript::ExecuteAdaptiveThreshold(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<int>(attribute);
		}
		if (OnlyContains(arguments, "width", "height"))
			image->AdaptiveThreshold((int)arguments["width"], (int)arguments["height"]);
		else if (OnlyContains(arguments, "width", "height", "offset"))
			image->AdaptiveThreshold((int)arguments["width"], (int)arguments["height"], (int)arguments["offset"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'adaptiveThreshold', allowed combinations are: [width, height] [width, height, offset]");
	}
	void MagickScript::ExecuteAddNoise(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "noiseType")
				arguments["noiseType"] = XmlHelper::GetAttribute<NoiseType>(element, "noiseType");
			else if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
		}
		if (OnlyContains(arguments, "noiseType"))
			image->AddNoise((NoiseType)arguments["noiseType"]);
		else if (OnlyContains(arguments, "noiseType", "channels"))
			image->AddNoise((NoiseType)arguments["noiseType"], (Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'addNoise', allowed combinations are: [noiseType] [noiseType, channels]");
	}
	void MagickScript::ExecuteAddProfile(XmlElement^ element, MagickImage^ image)
	{
		ImageProfile^ profile_ = CreateProfile(element);
		image->AddProfile(profile_);
	}
	void MagickScript::ExecuteAnnotate(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "text")
				arguments["text"] = XmlHelper::GetAttribute<String^>(element, "text");
			else if (attribute->Name == "gravity")
				arguments["gravity"] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
			else if (attribute->Name == "boundingArea")
				arguments["boundingArea"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "boundingArea");
			else if (attribute->Name == "degrees")
				arguments["degrees"] = XmlHelper::GetAttribute<double>(element, "degrees");
		}
		if (OnlyContains(arguments, "text", "gravity"))
			image->Annotate((String^)arguments["text"], (Gravity)arguments["gravity"]);
		else if (OnlyContains(arguments, "text", "boundingArea"))
			image->Annotate((String^)arguments["text"], (MagickGeometry^)arguments["boundingArea"]);
		else if (OnlyContains(arguments, "text", "boundingArea", "gravity"))
			image->Annotate((String^)arguments["text"], (MagickGeometry^)arguments["boundingArea"], (Gravity)arguments["gravity"]);
		else if (OnlyContains(arguments, "text", "boundingArea", "gravity", "degrees"))
			image->Annotate((String^)arguments["text"], (MagickGeometry^)arguments["boundingArea"], (Gravity)arguments["gravity"], (double)arguments["degrees"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'annotate', allowed combinations are: [text, gravity] [text, boundingArea] [text, boundingArea, gravity] [text, boundingArea, gravity, degrees]");
	}
	void MagickScript::ExecuteBlur(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
			else if (attribute->Name == "radius")
				arguments["radius"] = XmlHelper::GetAttribute<double>(element, "radius");
			else if (attribute->Name == "sigma")
				arguments["sigma"] = XmlHelper::GetAttribute<double>(element, "sigma");
		}
		if (arguments->Count == 0)
			image->Blur();
		else if (OnlyContains(arguments, "channels"))
			image->Blur((Channels)arguments["channels"]);
		else if (OnlyContains(arguments, "radius", "sigma"))
			image->Blur((double)arguments["radius"], (double)arguments["sigma"]);
		else if (OnlyContains(arguments, "radius", "sigma", "channels"))
			image->Blur((double)arguments["radius"], (double)arguments["sigma"], (Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'blur', allowed combinations are: [] [channels] [radius, sigma] [radius, sigma, channels]");
	}
	void MagickScript::ExecuteBorder(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<int>(attribute);
		}
		if (OnlyContains(arguments, "size"))
			image->Border((int)arguments["size"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Border((int)arguments["width"], (int)arguments["height"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'border', allowed combinations are: [size] [width, height]");
	}
	void MagickScript::ExecuteCDL(XmlElement^ element, MagickImage^ image)
	{
		String^ fileName_ = XmlHelper::GetAttribute<String^>(element, "fileName");
		image->CDL(fileName_);
	}
	void MagickScript::ExecuteCharcoal(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Charcoal();
		else if (OnlyContains(arguments, "radius", "sigma"))
			image->Charcoal((double)arguments["radius"], (double)arguments["sigma"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'charcoal', allowed combinations are: [] [radius, sigma]");
	}
	void MagickScript::ExecuteChop(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
			else if (attribute->Name == "xOffset")
				arguments["xOffset"] = XmlHelper::GetAttribute<int>(element, "xOffset");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "yOffset")
				arguments["yOffset"] = XmlHelper::GetAttribute<int>(element, "yOffset");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		if (OnlyContains(arguments, "geometry"))
			image->Chop((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "xOffset", "width", "yOffset", "height"))
			image->Chop((int)arguments["xOffset"], (int)arguments["width"], (int)arguments["yOffset"], (int)arguments["height"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'chop', allowed combinations are: [geometry] [xOffset, width, yOffset, height]");
	}
	void MagickScript::ExecuteChopHorizontal(XmlElement^ element, MagickImage^ image)
	{
		int offset_ = XmlHelper::GetAttribute<int>(element, "offset");
		int width_ = XmlHelper::GetAttribute<int>(element, "width");
		image->ChopHorizontal(offset_, width_);
	}
	void MagickScript::ExecuteChopVertical(XmlElement^ element, MagickImage^ image)
	{
		int offset_ = XmlHelper::GetAttribute<int>(element, "offset");
		int height_ = XmlHelper::GetAttribute<int>(element, "height");
		image->ChopVertical(offset_, height_);
	}
	void MagickScript::ExecuteChromaBluePrimary(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->ChromaBluePrimary(x_, y_);
	}
	void MagickScript::ExecuteChromaGreenPrimary(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->ChromaGreenPrimary(x_, y_);
	}
	void MagickScript::ExecuteChromaRedPrimary(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->ChromaRedPrimary(x_, y_);
	}
	void MagickScript::ExecuteChromaWhitePoint(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->ChromaWhitePoint(x_, y_);
	}
	void MagickScript::ExecuteColorAlpha(XmlElement^ element, MagickImage^ image)
	{
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		image->ColorAlpha(color_);
	}
	void MagickScript::ExecuteColorize(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "color")
				arguments["color"] = XmlHelper::GetAttribute<MagickColor^>(element, "color");
			else if (attribute->Name == "alpha")
				arguments["alpha"] = XmlHelper::GetAttribute<Percentage>(element, "alpha");
			else if (attribute->Name == "alphaRed")
				arguments["alphaRed"] = XmlHelper::GetAttribute<Percentage>(element, "alphaRed");
			else if (attribute->Name == "alphaGreen")
				arguments["alphaGreen"] = XmlHelper::GetAttribute<Percentage>(element, "alphaGreen");
			else if (attribute->Name == "alphaBlue")
				arguments["alphaBlue"] = XmlHelper::GetAttribute<Percentage>(element, "alphaBlue");
		}
		if (OnlyContains(arguments, "color", "alpha"))
			image->Colorize((MagickColor^)arguments["color"], (Percentage)arguments["alpha"]);
		else if (OnlyContains(arguments, "color", "alphaRed", "alphaGreen", "alphaBlue"))
			image->Colorize((MagickColor^)arguments["color"], (Percentage)arguments["alphaRed"], (Percentage)arguments["alphaGreen"], (Percentage)arguments["alphaBlue"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'colorize', allowed combinations are: [color, alpha] [color, alphaRed, alphaGreen, alphaBlue]");
	}
	void MagickScript::ExecuteColorMap(XmlElement^ element, MagickImage^ image)
	{
		int index_ = XmlHelper::GetAttribute<int>(element, "index");
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		image->ColorMap(index_, color_);
	}
	void MagickScript::ExecuteComposite(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "gravity")
				arguments["gravity"] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
			else if (attribute->Name == "offset")
				arguments["offset"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "offset");
			else if (attribute->Name == "compose")
				arguments["compose"] = XmlHelper::GetAttribute<CompositeOperator>(element, "compose");
			else if (attribute->Name == "x")
				arguments["x"] = XmlHelper::GetAttribute<int>(element, "x");
			else if (attribute->Name == "y")
				arguments["y"] = XmlHelper::GetAttribute<int>(element, "y");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickImage(elem);
		}
		if (OnlyContains(arguments, "image", "gravity"))
			image->Composite((MagickImage^)arguments["image"], (Gravity)arguments["gravity"]);
		else if (OnlyContains(arguments, "image", "offset"))
			image->Composite((MagickImage^)arguments["image"], (MagickGeometry^)arguments["offset"]);
		else if (OnlyContains(arguments, "image", "gravity", "compose"))
			image->Composite((MagickImage^)arguments["image"], (Gravity)arguments["gravity"], (CompositeOperator)arguments["compose"]);
		else if (OnlyContains(arguments, "image", "offset", "compose"))
			image->Composite((MagickImage^)arguments["image"], (MagickGeometry^)arguments["offset"], (CompositeOperator)arguments["compose"]);
		else if (OnlyContains(arguments, "image", "x", "y"))
			image->Composite((MagickImage^)arguments["image"], (int)arguments["x"], (int)arguments["y"]);
		else if (OnlyContains(arguments, "image", "x", "y", "compose"))
			image->Composite((MagickImage^)arguments["image"], (int)arguments["x"], (int)arguments["y"], (CompositeOperator)arguments["compose"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'composite', allowed combinations are: [image, gravity] [image, offset] [image, gravity, compose] [image, offset, compose] [image, x, y] [image, x, y, compose]");
	}
	void MagickScript::ExecuteContrast(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<bool>(attribute);
		}
		if (arguments->Count == 0)
			image->Contrast();
		else if (OnlyContains(arguments, "enhance"))
			image->Contrast((bool)arguments["enhance"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'contrast', allowed combinations are: [] [enhance]");
	}
	void MagickScript::ExecuteCrop(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
			else if (attribute->Name == "gravity")
				arguments["gravity"] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
		}
		if (OnlyContains(arguments, "geometry"))
			image->Crop((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Crop((int)arguments["width"], (int)arguments["height"]);
		else if (OnlyContains(arguments, "width", "height", "gravity"))
			image->Crop((int)arguments["width"], (int)arguments["height"], (Gravity)arguments["gravity"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'crop', allowed combinations are: [geometry] [width, height] [width, height, gravity]");
	}
	void MagickScript::ExecuteCycleColormap(XmlElement^ element, MagickImage^ image)
	{
		int amount_ = XmlHelper::GetAttribute<int>(element, "amount");
		image->CycleColormap(amount_);
	}
	void MagickScript::ExecuteDepth(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "value")
				arguments["value"] = XmlHelper::GetAttribute<int>(element, "value");
			else if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
		}
		if (OnlyContains(arguments, "value"))
			image->Depth((int)arguments["value"]);
		else if (OnlyContains(arguments, "value", "channels"))
			image->Depth((int)arguments["value"], (Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'depth', allowed combinations are: [value] [value, channels]");
	}
	void MagickScript::ExecuteDespeckle(XmlElement^ element, MagickImage^ image)
	{
		image->Despeckle();
	}
	void MagickScript::ExecuteEdge(XmlElement^ element, MagickImage^ image)
	{
		double radius_ = XmlHelper::GetAttribute<double>(element, "radius");
		image->Edge(radius_);
	}
	void MagickScript::ExecuteEmboss(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Emboss();
		else if (OnlyContains(arguments, "radius", "sigma"))
			image->Emboss((double)arguments["radius"], (double)arguments["sigma"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'emboss', allowed combinations are: [] [radius, sigma]");
	}
	void MagickScript::ExecuteExtent(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
			else if (attribute->Name == "gravity")
				arguments["gravity"] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
			else if (attribute->Name == "backgroundColor")
				arguments["backgroundColor"] = XmlHelper::GetAttribute<MagickColor^>(element, "backgroundColor");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		if (OnlyContains(arguments, "geometry"))
			image->Extent((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "geometry", "gravity"))
			image->Extent((MagickGeometry^)arguments["geometry"], (Gravity)arguments["gravity"]);
		else if (OnlyContains(arguments, "geometry", "backgroundColor"))
			image->Extent((MagickGeometry^)arguments["geometry"], (MagickColor^)arguments["backgroundColor"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Extent((int)arguments["width"], (int)arguments["height"]);
		else if (OnlyContains(arguments, "geometry", "gravity", "backgroundColor"))
			image->Extent((MagickGeometry^)arguments["geometry"], (Gravity)arguments["gravity"], (MagickColor^)arguments["backgroundColor"]);
		else if (OnlyContains(arguments, "width", "height", "gravity"))
			image->Extent((int)arguments["width"], (int)arguments["height"], (Gravity)arguments["gravity"]);
		else if (OnlyContains(arguments, "width", "height", "backgroundColor"))
			image->Extent((int)arguments["width"], (int)arguments["height"], (MagickColor^)arguments["backgroundColor"]);
		else if (OnlyContains(arguments, "width", "height", "gravity", "backgroundColor"))
			image->Extent((int)arguments["width"], (int)arguments["height"], (Gravity)arguments["gravity"], (MagickColor^)arguments["backgroundColor"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'extent', allowed combinations are: [geometry] [geometry, gravity] [geometry, backgroundColor] [width, height] [geometry, gravity, backgroundColor] [width, height, gravity] [width, height, backgroundColor] [width, height, gravity, backgroundColor]");
	}
	void MagickScript::ExecuteFlip(XmlElement^ element, MagickImage^ image)
	{
		image->Flip();
	}
	void MagickScript::ExecuteFloodFill(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
			else if (attribute->Name == "color")
				arguments["color"] = XmlHelper::GetAttribute<MagickColor^>(element, "color");
			else if (attribute->Name == "borderColor")
				arguments["borderColor"] = XmlHelper::GetAttribute<MagickColor^>(element, "borderColor");
			else if (attribute->Name == "x")
				arguments["x"] = XmlHelper::GetAttribute<int>(element, "x");
			else if (attribute->Name == "y")
				arguments["y"] = XmlHelper::GetAttribute<int>(element, "y");
			else if (attribute->Name == "alpha")
				arguments["alpha"] = XmlHelper::GetAttribute<int>(element, "alpha");
			else if (attribute->Name == "paintMethod")
				arguments["paintMethod"] = XmlHelper::GetAttribute<PaintMethod>(element, "paintMethod");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickImage(elem);
		}
		if (OnlyContains(arguments, "image", "geometry"))
			image->FloodFill((MagickImage^)arguments["image"], (MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "color", "geometry"))
			image->FloodFill((MagickColor^)arguments["color"], (MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "image", "geometry", "borderColor"))
			image->FloodFill((MagickImage^)arguments["image"], (MagickGeometry^)arguments["geometry"], (MagickColor^)arguments["borderColor"]);
		else if (OnlyContains(arguments, "image", "x", "y"))
			image->FloodFill((MagickImage^)arguments["image"], (int)arguments["x"], (int)arguments["y"]);
		else if (OnlyContains(arguments, "color", "geometry", "borderColor"))
			image->FloodFill((MagickColor^)arguments["color"], (MagickGeometry^)arguments["geometry"], (MagickColor^)arguments["borderColor"]);
		else if (OnlyContains(arguments, "color", "x", "y"))
			image->FloodFill((MagickColor^)arguments["color"], (int)arguments["x"], (int)arguments["y"]);
		else if (OnlyContains(arguments, "image", "x", "y", "borderColor"))
			image->FloodFill((MagickImage^)arguments["image"], (int)arguments["x"], (int)arguments["y"], (MagickColor^)arguments["borderColor"]);
		else if (OnlyContains(arguments, "color", "x", "y", "borderColor"))
			image->FloodFill((MagickColor^)arguments["color"], (int)arguments["x"], (int)arguments["y"], (MagickColor^)arguments["borderColor"]);
		else if (OnlyContains(arguments, "alpha", "x", "y", "paintMethod"))
			image->FloodFill((int)arguments["alpha"], (int)arguments["x"], (int)arguments["y"], (PaintMethod)arguments["paintMethod"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'floodFill', allowed combinations are: [image, geometry] [color, geometry] [image, geometry, borderColor] [image, x, y] [color, geometry, borderColor] [color, x, y] [image, x, y, borderColor] [color, x, y, borderColor] [alpha, x, y, paintMethod]");
	}
	void MagickScript::ExecuteFlop(XmlElement^ element, MagickImage^ image)
	{
		image->Flop();
	}
	void MagickScript::ExecuteFrame(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
			else if (attribute->Name == "innerBevel")
				arguments["innerBevel"] = XmlHelper::GetAttribute<int>(element, "innerBevel");
			else if (attribute->Name == "outerBevel")
				arguments["outerBevel"] = XmlHelper::GetAttribute<int>(element, "outerBevel");
		}
		if (arguments->Count == 0)
			image->Frame();
		else if (OnlyContains(arguments, "geometry"))
			image->Frame((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Frame((int)arguments["width"], (int)arguments["height"]);
		else if (OnlyContains(arguments, "width", "height", "innerBevel", "outerBevel"))
			image->Frame((int)arguments["width"], (int)arguments["height"], (int)arguments["innerBevel"], (int)arguments["outerBevel"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'frame', allowed combinations are: [] [geometry] [width, height] [width, height, innerBevel, outerBevel]");
	}
	void MagickScript::ExecuteFx(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "expression")
				arguments["expression"] = XmlHelper::GetAttribute<String^>(element, "expression");
			else if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
		}
		if (OnlyContains(arguments, "expression"))
			image->Fx((String^)arguments["expression"]);
		else if (OnlyContains(arguments, "expression", "channels"))
			image->Fx((String^)arguments["expression"], (Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'fx', allowed combinations are: [expression] [expression, channels]");
	}
	void MagickScript::ExecuteGamma(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (OnlyContains(arguments, "value"))
			image->Gamma((double)arguments["value"]);
		else if (OnlyContains(arguments, "gammeRed", "gammeGreen", "gammeBlue"))
			image->Gamma((double)arguments["gammeRed"], (double)arguments["gammeGreen"], (double)arguments["gammeBlue"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'gamma', allowed combinations are: [value] [gammeRed, gammeGreen, gammeBlue]");
	}
	void MagickScript::ExecuteGaussianBlur(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<double>(element, "width");
			else if (attribute->Name == "sigma")
				arguments["sigma"] = XmlHelper::GetAttribute<double>(element, "sigma");
			else if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
		}
		if (OnlyContains(arguments, "width", "sigma"))
			image->GaussianBlur((double)arguments["width"], (double)arguments["sigma"]);
		else if (OnlyContains(arguments, "width", "sigma", "channels"))
			image->GaussianBlur((double)arguments["width"], (double)arguments["sigma"], (Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'gaussianBlur', allowed combinations are: [width, sigma] [width, sigma, channels]");
	}
	void MagickScript::ExecuteHaldClut(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ image_ = CreateMagickImage((XmlElement^)element->SelectSingleNode("image"));
		image->HaldClut(image_);
	}
	void MagickScript::ExecuteImplode(XmlElement^ element, MagickImage^ image)
	{
		double factor_ = XmlHelper::GetAttribute<double>(element, "factor");
		image->Implode(factor_);
	}
	void MagickScript::ExecuteInverseFourierTransform(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<bool>(attribute);
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickImage(elem);
		}
		if (OnlyContains(arguments, "image"))
			image->InverseFourierTransform((MagickImage^)arguments["image"]);
		else if (OnlyContains(arguments, "image", "magnitude"))
			image->InverseFourierTransform((MagickImage^)arguments["image"], (bool)arguments["magnitude"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'inverseFourierTransform', allowed combinations are: [image] [image, magnitude]");
	}
	void MagickScript::ExecuteLevel(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "blackPoint")
				arguments["blackPoint"] = XmlHelper::GetAttribute<Magick::Quantum>(element, "blackPoint");
			else if (attribute->Name == "whitePoint")
				arguments["whitePoint"] = XmlHelper::GetAttribute<Magick::Quantum>(element, "whitePoint");
			else if (attribute->Name == "midpoint")
				arguments["midpoint"] = XmlHelper::GetAttribute<double>(element, "midpoint");
			else if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
		}
		if (OnlyContains(arguments, "blackPoint", "whitePoint"))
			image->Level((Magick::Quantum)arguments["blackPoint"], (Magick::Quantum)arguments["whitePoint"]);
		else if (OnlyContains(arguments, "blackPoint", "whitePoint", "midpoint"))
			image->Level((Magick::Quantum)arguments["blackPoint"], (Magick::Quantum)arguments["whitePoint"], (double)arguments["midpoint"]);
		else if (OnlyContains(arguments, "blackPoint", "whitePoint", "channels"))
			image->Level((Magick::Quantum)arguments["blackPoint"], (Magick::Quantum)arguments["whitePoint"], (Channels)arguments["channels"]);
		else if (OnlyContains(arguments, "blackPoint", "whitePoint", "midpoint", "channels"))
			image->Level((Magick::Quantum)arguments["blackPoint"], (Magick::Quantum)arguments["whitePoint"], (double)arguments["midpoint"], (Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'level', allowed combinations are: [blackPoint, whitePoint] [blackPoint, whitePoint, midpoint] [blackPoint, whitePoint, channels] [blackPoint, whitePoint, midpoint, channels]");
	}
	void MagickScript::ExecuteLower(XmlElement^ element, MagickImage^ image)
	{
		int size_ = XmlHelper::GetAttribute<int>(element, "size");
		image->Lower(size_);
	}
	void MagickScript::ExecuteMagnify(XmlElement^ element, MagickImage^ image)
	{
		image->Magnify();
	}
	void MagickScript::ExecuteMap(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<bool>(attribute);
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickImage(elem);
		}
		if (OnlyContains(arguments, "image"))
			image->Map((MagickImage^)arguments["image"]);
		else if (OnlyContains(arguments, "image", "dither"))
			image->Map((MagickImage^)arguments["image"], (bool)arguments["dither"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'map', allowed combinations are: [image] [image, dither]");
	}
	void MagickScript::ExecuteMedianFilter(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->MedianFilter();
		else if (OnlyContains(arguments, "radius"))
			image->MedianFilter((double)arguments["radius"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'medianFilter', allowed combinations are: [] [radius]");
	}
	void MagickScript::ExecuteMinify(XmlElement^ element, MagickImage^ image)
	{
		image->Minify();
	}
	void MagickScript::ExecuteModulate(XmlElement^ element, MagickImage^ image)
	{
		Percentage brightness_ = XmlHelper::GetAttribute<Percentage>(element, "brightness");
		Percentage saturation_ = XmlHelper::GetAttribute<Percentage>(element, "saturation");
		Percentage hue_ = XmlHelper::GetAttribute<Percentage>(element, "hue");
		image->Modulate(brightness_, saturation_, hue_);
	}
	void MagickScript::ExecuteMotionBlur(XmlElement^ element, MagickImage^ image)
	{
		double radius_ = XmlHelper::GetAttribute<double>(element, "radius");
		double sigma_ = XmlHelper::GetAttribute<double>(element, "sigma");
		double angle_ = XmlHelper::GetAttribute<double>(element, "angle");
		image->MotionBlur(radius_, sigma_, angle_);
	}
	void MagickScript::ExecuteNegate(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<bool>(attribute);
		}
		if (arguments->Count == 0)
			image->Negate();
		else if (OnlyContains(arguments, "onlyGrayscale"))
			image->Negate((bool)arguments["onlyGrayscale"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'negate', allowed combinations are: [] [onlyGrayscale]");
	}
	void MagickScript::ExecuteNormalize(XmlElement^ element, MagickImage^ image)
	{
		image->Normalize();
	}
	void MagickScript::ExecuteOilPaint(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->OilPaint();
		else if (OnlyContains(arguments, "radius"))
			image->OilPaint((double)arguments["radius"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'oilPaint', allowed combinations are: [] [radius]");
	}
	void MagickScript::ExecuteQuantize(XmlElement^ element, MagickImage^ image)
	{
		image->Quantize();
	}
	void MagickScript::ExecuteQuantumOperator(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
			else if (attribute->Name == "evaluateOperator")
				arguments["evaluateOperator"] = XmlHelper::GetAttribute<EvaluateOperator>(element, "evaluateOperator");
			else if (attribute->Name == "value")
				arguments["value"] = XmlHelper::GetAttribute<double>(element, "value");
			else if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
		}
		if (OnlyContains(arguments, "channels", "evaluateOperator", "value"))
			image->QuantumOperator((Channels)arguments["channels"], (EvaluateOperator)arguments["evaluateOperator"], (double)arguments["value"]);
		else if (OnlyContains(arguments, "channels", "geometry", "evaluateOperator", "value"))
			image->QuantumOperator((Channels)arguments["channels"], (MagickGeometry^)arguments["geometry"], (EvaluateOperator)arguments["evaluateOperator"], (double)arguments["value"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'quantumOperator', allowed combinations are: [channels, evaluateOperator, value] [channels, geometry, evaluateOperator, value]");
	}
	void MagickScript::ExecuteRaise(XmlElement^ element, MagickImage^ image)
	{
		int size_ = XmlHelper::GetAttribute<int>(element, "size");
		image->Raise(size_);
	}
	void MagickScript::ExecuteRandomThreshold(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "low")
				arguments["low"] = XmlHelper::GetAttribute<Magick::Quantum>(element, "low");
			else if (attribute->Name == "high")
				arguments["high"] = XmlHelper::GetAttribute<Magick::Quantum>(element, "high");
			else if (attribute->Name == "percentageLow")
				arguments["percentageLow"] = XmlHelper::GetAttribute<Percentage>(element, "percentageLow");
			else if (attribute->Name == "percentageHigh")
				arguments["percentageHigh"] = XmlHelper::GetAttribute<Percentage>(element, "percentageHigh");
			else if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
		}
		if (OnlyContains(arguments, "low", "high"))
			image->RandomThreshold((Magick::Quantum)arguments["low"], (Magick::Quantum)arguments["high"]);
		else if (OnlyContains(arguments, "percentageLow", "percentageHigh"))
			image->RandomThreshold((Percentage)arguments["percentageLow"], (Percentage)arguments["percentageHigh"]);
		else if (OnlyContains(arguments, "low", "high", "channels"))
			image->RandomThreshold((Magick::Quantum)arguments["low"], (Magick::Quantum)arguments["high"], (Channels)arguments["channels"]);
		else if (OnlyContains(arguments, "percentageLow", "percentageHigh", "channels"))
			image->RandomThreshold((Percentage)arguments["percentageLow"], (Percentage)arguments["percentageHigh"], (Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'randomThreshold', allowed combinations are: [low, high] [percentageLow, percentageHigh] [low, high, channels] [percentageLow, percentageHigh, channels]");
	}
	void MagickScript::ExecuteReduceNoise(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<int>(attribute);
		}
		if (arguments->Count == 0)
			image->ReduceNoise();
		else if (OnlyContains(arguments, "order"))
			image->ReduceNoise((int)arguments["order"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'reduceNoise', allowed combinations are: [] [order]");
	}
	void MagickScript::ExecuteRemoveProfile(XmlElement^ element, MagickImage^ image)
	{
		String^ name_ = XmlHelper::GetAttribute<String^>(element, "name");
		image->RemoveProfile(name_);
	}
	void MagickScript::ExecuteResize(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "percentage")
				arguments["percentage"] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
			else if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
			else if (attribute->Name == "percentageWidth")
				arguments["percentageWidth"] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
			else if (attribute->Name == "percentageHeight")
				arguments["percentageHeight"] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		if (OnlyContains(arguments, "percentage"))
			image->Resize((Percentage)arguments["percentage"]);
		else if (OnlyContains(arguments, "geometry"))
			image->Resize((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
			image->Resize((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Resize((int)arguments["width"], (int)arguments["height"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'resize', allowed combinations are: [percentage] [geometry] [percentageWidth, percentageHeight] [width, height]");
	}
	void MagickScript::ExecuteRoll(XmlElement^ element, MagickImage^ image)
	{
		int xOffset_ = XmlHelper::GetAttribute<int>(element, "xOffset");
		int yOffset_ = XmlHelper::GetAttribute<int>(element, "yOffset");
		image->Roll(xOffset_, yOffset_);
	}
	void MagickScript::ExecuteRotate(XmlElement^ element, MagickImage^ image)
	{
		double degrees_ = XmlHelper::GetAttribute<double>(element, "degrees");
		image->Rotate(degrees_);
	}
	void MagickScript::ExecuteSample(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "percentage")
				arguments["percentage"] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
			else if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
			else if (attribute->Name == "percentageWidth")
				arguments["percentageWidth"] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
			else if (attribute->Name == "percentageHeight")
				arguments["percentageHeight"] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		if (OnlyContains(arguments, "percentage"))
			image->Sample((Percentage)arguments["percentage"]);
		else if (OnlyContains(arguments, "geometry"))
			image->Sample((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
			image->Sample((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Sample((int)arguments["width"], (int)arguments["height"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'sample', allowed combinations are: [percentage] [geometry] [percentageWidth, percentageHeight] [width, height]");
	}
	void MagickScript::ExecuteScale(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "percentage")
				arguments["percentage"] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
			else if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
			else if (attribute->Name == "percentageWidth")
				arguments["percentageWidth"] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
			else if (attribute->Name == "percentageHeight")
				arguments["percentageHeight"] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		if (OnlyContains(arguments, "percentage"))
			image->Scale((Percentage)arguments["percentage"]);
		else if (OnlyContains(arguments, "geometry"))
			image->Scale((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
			image->Scale((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Scale((int)arguments["width"], (int)arguments["height"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'scale', allowed combinations are: [percentage] [geometry] [percentageWidth, percentageHeight] [width, height]");
	}
	void MagickScript::ExecuteSegment(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Segment();
		else if (OnlyContains(arguments, "clusterThreshold", "smoothingThreshold"))
			image->Segment((double)arguments["clusterThreshold"], (double)arguments["smoothingThreshold"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'segment', allowed combinations are: [] [clusterThreshold, smoothingThreshold]");
	}
	void MagickScript::ExecuteSetAttribute(XmlElement^ element, MagickImage^ image)
	{
		String^ name_ = XmlHelper::GetAttribute<String^>(element, "name");
		String^ value_ = XmlHelper::GetAttribute<String^>(element, "value");
		image->SetAttribute(name_, value_);
	}
	void MagickScript::ExecuteSetOption(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "format")
				arguments["format"] = XmlHelper::GetAttribute<MagickFormat>(element, "format");
			else if (attribute->Name == "name")
				arguments["name"] = XmlHelper::GetAttribute<String^>(element, "name");
			else if (attribute->Name == "value")
				arguments["value"] = XmlHelper::GetAttribute<String^>(element, "value");
			else if (attribute->Name == "flag")
				arguments["flag"] = XmlHelper::GetAttribute<bool>(element, "flag");
		}
		if (OnlyContains(arguments, "format", "name", "value"))
			image->SetOption((MagickFormat)arguments["format"], (String^)arguments["name"], (String^)arguments["value"]);
		else if (OnlyContains(arguments, "format", "name", "flag"))
			image->SetOption((MagickFormat)arguments["format"], (String^)arguments["name"], (bool)arguments["flag"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'setOption', allowed combinations are: [format, name, value] [format, name, flag]");
	}
	void MagickScript::ExecuteShade(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "azimuth")
				arguments["azimuth"] = XmlHelper::GetAttribute<double>(element, "azimuth");
			else if (attribute->Name == "elevation")
				arguments["elevation"] = XmlHelper::GetAttribute<double>(element, "elevation");
			else if (attribute->Name == "colorShading")
				arguments["colorShading"] = XmlHelper::GetAttribute<bool>(element, "colorShading");
		}
		if (arguments->Count == 0)
			image->Shade();
		else if (OnlyContains(arguments, "azimuth", "elevation", "colorShading"))
			image->Shade((double)arguments["azimuth"], (double)arguments["elevation"], (bool)arguments["colorShading"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'shade', allowed combinations are: [] [azimuth, elevation, colorShading]");
	}
	void MagickScript::ExecuteShadow(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "color")
				arguments["color"] = XmlHelper::GetAttribute<MagickColor^>(element, "color");
			else if (attribute->Name == "x")
				arguments["x"] = XmlHelper::GetAttribute<int>(element, "x");
			else if (attribute->Name == "y")
				arguments["y"] = XmlHelper::GetAttribute<int>(element, "y");
			else if (attribute->Name == "sigma")
				arguments["sigma"] = XmlHelper::GetAttribute<double>(element, "sigma");
			else if (attribute->Name == "alpha")
				arguments["alpha"] = XmlHelper::GetAttribute<Percentage>(element, "alpha");
		}
		if (arguments->Count == 0)
			image->Shadow();
		else if (OnlyContains(arguments, "color"))
			image->Shadow((MagickColor^)arguments["color"]);
		else if (OnlyContains(arguments, "x", "y", "sigma", "alpha"))
			image->Shadow((int)arguments["x"], (int)arguments["y"], (double)arguments["sigma"], (Percentage)arguments["alpha"]);
		else if (OnlyContains(arguments, "x", "y", "sigma", "alpha", "color"))
			image->Shadow((int)arguments["x"], (int)arguments["y"], (double)arguments["sigma"], (Percentage)arguments["alpha"], (MagickColor^)arguments["color"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'shadow', allowed combinations are: [] [color] [x, y, sigma, alpha] [x, y, sigma, alpha, color]");
	}
	void MagickScript::ExecuteSharpen(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
			else if (attribute->Name == "radius")
				arguments["radius"] = XmlHelper::GetAttribute<double>(element, "radius");
			else if (attribute->Name == "sigma")
				arguments["sigma"] = XmlHelper::GetAttribute<double>(element, "sigma");
		}
		if (arguments->Count == 0)
			image->Sharpen();
		else if (OnlyContains(arguments, "channels"))
			image->Sharpen((Channels)arguments["channels"]);
		else if (OnlyContains(arguments, "radius", "sigma"))
			image->Sharpen((double)arguments["radius"], (double)arguments["sigma"]);
		else if (OnlyContains(arguments, "radius", "sigma", "channels"))
			image->Sharpen((double)arguments["radius"], (double)arguments["sigma"], (Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'sharpen', allowed combinations are: [] [channels] [radius, sigma] [radius, sigma, channels]");
	}
	void MagickScript::ExecuteShave(XmlElement^ element, MagickImage^ image)
	{
		int leftRight_ = XmlHelper::GetAttribute<int>(element, "leftRight");
		int topBottom_ = XmlHelper::GetAttribute<int>(element, "topBottom");
		image->Shave(leftRight_, topBottom_);
	}
	void MagickScript::ExecuteShear(XmlElement^ element, MagickImage^ image)
	{
		double xAngle_ = XmlHelper::GetAttribute<double>(element, "xAngle");
		double yAngle_ = XmlHelper::GetAttribute<double>(element, "yAngle");
		image->Shear(xAngle_, yAngle_);
	}
	void MagickScript::ExecuteSigmoidalContrast(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "sharpen")
				arguments["sharpen"] = XmlHelper::GetAttribute<bool>(element, "sharpen");
			else if (attribute->Name == "contrast")
				arguments["contrast"] = XmlHelper::GetAttribute<double>(element, "contrast");
			else if (attribute->Name == "midpoint")
				arguments["midpoint"] = XmlHelper::GetAttribute<double>(element, "midpoint");
		}
		if (OnlyContains(arguments, "sharpen", "contrast"))
			image->SigmoidalContrast((bool)arguments["sharpen"], (double)arguments["contrast"]);
		else if (OnlyContains(arguments, "sharpen", "contrast", "midpoint"))
			image->SigmoidalContrast((bool)arguments["sharpen"], (double)arguments["contrast"], (double)arguments["midpoint"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'sigmoidalContrast', allowed combinations are: [sharpen, contrast] [sharpen, contrast, midpoint]");
	}
	void MagickScript::ExecuteSolarize(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Solarize();
		else if (OnlyContains(arguments, "factor"))
			image->Solarize((double)arguments["factor"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'solarize', allowed combinations are: [] [factor]");
	}
	void MagickScript::ExecuteStegano(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ watermark_ = CreateMagickImage((XmlElement^)element->SelectSingleNode("watermark"));
		image->Stegano(watermark_);
	}
	void MagickScript::ExecuteStereo(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ rightImage_ = CreateMagickImage((XmlElement^)element->SelectSingleNode("rightImage"));
		image->Stereo(rightImage_);
	}
	void MagickScript::ExecuteStrip(XmlElement^ element, MagickImage^ image)
	{
		image->Strip();
	}
	void MagickScript::ExecuteSwirl(XmlElement^ element, MagickImage^ image)
	{
		double degrees_ = XmlHelper::GetAttribute<double>(element, "degrees");
		image->Swirl(degrees_);
	}
	void MagickScript::ExecuteTexture(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ image_ = CreateMagickImage((XmlElement^)element->SelectSingleNode("image"));
		image->Texture(image_);
	}
	void MagickScript::ExecuteThreshold(XmlElement^ element, MagickImage^ image)
	{
		double value_ = XmlHelper::GetAttribute<double>(element, "value");
		image->Threshold(value_);
	}
	void MagickScript::ExecuteTransform(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<MagickGeometry^>(attribute);
		}
		if (OnlyContains(arguments, "imageGeometry"))
			image->Transform((MagickGeometry^)arguments["imageGeometry"]);
		else if (OnlyContains(arguments, "imageGeometry", "cropGeometry"))
			image->Transform((MagickGeometry^)arguments["imageGeometry"], (MagickGeometry^)arguments["cropGeometry"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'transform', allowed combinations are: [imageGeometry] [imageGeometry, cropGeometry]");
	}
	void MagickScript::ExecuteTransformOrigin(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->TransformOrigin(x_, y_);
	}
	void MagickScript::ExecuteTransformReset(XmlElement^ element, MagickImage^ image)
	{
		image->TransformReset();
	}
	void MagickScript::ExecuteTransformRotation(XmlElement^ element, MagickImage^ image)
	{
		double angle_ = XmlHelper::GetAttribute<double>(element, "angle");
		image->TransformRotation(angle_);
	}
	void MagickScript::ExecuteTransformScale(XmlElement^ element, MagickImage^ image)
	{
		double scaleX_ = XmlHelper::GetAttribute<double>(element, "scaleX");
		double scaleY_ = XmlHelper::GetAttribute<double>(element, "scaleY");
		image->TransformScale(scaleX_, scaleY_);
	}
	void MagickScript::ExecuteTransformSkewX(XmlElement^ element, MagickImage^ image)
	{
		double skewX_ = XmlHelper::GetAttribute<double>(element, "skewX");
		image->TransformSkewX(skewX_);
	}
	void MagickScript::ExecuteTransformSkewY(XmlElement^ element, MagickImage^ image)
	{
		double skewY_ = XmlHelper::GetAttribute<double>(element, "skewY");
		image->TransformSkewY(skewY_);
	}
	void MagickScript::ExecuteTransparent(XmlElement^ element, MagickImage^ image)
	{
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		image->Transparent(color_);
	}
	void MagickScript::ExecuteTransparentChroma(XmlElement^ element, MagickImage^ image)
	{
		MagickColor^ colorLow_ = XmlHelper::GetAttribute<MagickColor^>(element, "colorLow");
		MagickColor^ colorHigh_ = XmlHelper::GetAttribute<MagickColor^>(element, "colorHigh");
		image->TransparentChroma(colorLow_, colorHigh_);
	}
	void MagickScript::ExecuteTrim(XmlElement^ element, MagickImage^ image)
	{
		image->Trim();
	}
	void MagickScript::ExecuteUnsharpmask(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "radius")
				arguments["radius"] = XmlHelper::GetAttribute<double>(element, "radius");
			else if (attribute->Name == "sigma")
				arguments["sigma"] = XmlHelper::GetAttribute<double>(element, "sigma");
			else if (attribute->Name == "amount")
				arguments["amount"] = XmlHelper::GetAttribute<double>(element, "amount");
			else if (attribute->Name == "threshold")
				arguments["threshold"] = XmlHelper::GetAttribute<double>(element, "threshold");
			else if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
		}
		if (OnlyContains(arguments, "radius", "sigma", "amount", "threshold"))
			image->Unsharpmask((double)arguments["radius"], (double)arguments["sigma"], (double)arguments["amount"], (double)arguments["threshold"]);
		else if (OnlyContains(arguments, "radius", "sigma", "amount", "threshold", "channels"))
			image->Unsharpmask((double)arguments["radius"], (double)arguments["sigma"], (double)arguments["amount"], (double)arguments["threshold"], (Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'unsharpmask', allowed combinations are: [radius, sigma, amount, threshold] [radius, sigma, amount, threshold, channels]");
	}
	void MagickScript::ExecuteWave(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Wave();
		else if (OnlyContains(arguments, "amplitude", "length"))
			image->Wave((double)arguments["amplitude"], (double)arguments["length"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'wave', allowed combinations are: [] [amplitude, length]");
	}
	void MagickScript::ExecuteZoom(XmlElement^ element, MagickImage^ image)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "percentage")
				arguments["percentage"] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
			else if (attribute->Name == "geometry")
				arguments["geometry"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "geometry");
			else if (attribute->Name == "percentageWidth")
				arguments["percentageWidth"] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
			else if (attribute->Name == "percentageHeight")
				arguments["percentageHeight"] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		if (OnlyContains(arguments, "percentage"))
			image->Zoom((Percentage)arguments["percentage"]);
		else if (OnlyContains(arguments, "geometry"))
			image->Zoom((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
			image->Zoom((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Zoom((int)arguments["width"], (int)arguments["height"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'zoom', allowed combinations are: [percentage] [geometry] [percentageWidth, percentageHeight] [width, height]");
	}
	MagickImage^ MagickScript::ExecuteCollection(XmlElement^ element, MagickImageCollection^ collection)
	{
		ExecuteElementCollection^ method = dynamic_cast<ExecuteElementCollection^>(_StaticExecuteCollection[element->Name]);
		if (method == nullptr)
			throw gcnew NotImplementedException(element->Name);
		return method(element,collection);
	}
	MagickImage^ MagickScript::ExecuteCoalesce(XmlElement^ element, MagickImageCollection^ collection)
	{
		collection->Coalesce();
		return nullptr;
	}
	MagickImage^ MagickScript::ExecuteDeconstruct(XmlElement^ element, MagickImageCollection^ collection)
	{
		collection->Deconstruct();
		return nullptr;
	}
	MagickImage^ MagickScript::ExecuteOptimize(XmlElement^ element, MagickImageCollection^ collection)
	{
		collection->Optimize();
		return nullptr;
	}
	MagickImage^ MagickScript::ExecuteOptimizePlus(XmlElement^ element, MagickImageCollection^ collection)
	{
		collection->OptimizePlus();
		return nullptr;
	}
	MagickImage^ MagickScript::ExecuteRePage(XmlElement^ element, MagickImageCollection^ collection)
	{
		collection->RePage();
		return nullptr;
	}
	MagickImage^ MagickScript::ExecuteAppendHorizontally(XmlElement^ element, MagickImageCollection^ collection)
	{
		return collection->AppendHorizontally();
	}
	MagickImage^ MagickScript::ExecuteAppendVertically(XmlElement^ element, MagickImageCollection^ collection)
	{
		return collection->AppendVertically();
	}
	MagickImage^ MagickScript::ExecuteCombine(XmlElement^ element, MagickImageCollection^ collection)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<Channels>(attribute);
		}
		if (arguments->Count == 0)
			return collection->Combine();
		else if (OnlyContains(arguments, "channels"))
			return collection->Combine((Channels)arguments["channels"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'combine', allowed combinations are: [] [channels]");
	}
	MagickImage^ MagickScript::ExecuteEvaluate(XmlElement^ element, MagickImageCollection^ collection)
	{
		EvaluateOperator evaluateOperator_ = XmlHelper::GetAttribute<EvaluateOperator>(element, "evaluateOperator");
		return collection->Evaluate(evaluateOperator_);
	}
	MagickImage^ MagickScript::ExecuteFlatten(XmlElement^ element, MagickImageCollection^ collection)
	{
		return collection->Flatten();
	}
	MagickImage^ MagickScript::ExecuteMerge(XmlElement^ element, MagickImageCollection^ collection)
	{
		return collection->Merge();
	}
	MagickImage^ MagickScript::ExecuteMosaic(XmlElement^ element, MagickImageCollection^ collection)
	{
		return collection->Mosaic();
	}
	MagickImage^ MagickScript::ExecuteTrimBounds(XmlElement^ element, MagickImageCollection^ collection)
	{
		return collection->TrimBounds();
	}
	void MagickScript::ExecuteDrawable(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		ExecuteElementDrawable^ method = dynamic_cast<ExecuteElementDrawable^>(_StaticExecuteDrawable[element->Name]);
		if (method == nullptr)
			method = dynamic_cast<ExecuteElementDrawable^>(_ExecuteDrawable[element->Name]);
		if (method == nullptr)
			throw gcnew NotImplementedException(element->Name);
		method(element,drawables);
	}
	void MagickScript::ExecuteAffine(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double scaleX_ = XmlHelper::GetAttribute<double>(element, "scaleX");
		double scaleY_ = XmlHelper::GetAttribute<double>(element, "scaleY");
		double shearX_ = XmlHelper::GetAttribute<double>(element, "shearX");
		double shearY_ = XmlHelper::GetAttribute<double>(element, "shearY");
		double translateX_ = XmlHelper::GetAttribute<double>(element, "translateX");
		double translateY_ = XmlHelper::GetAttribute<double>(element, "translateY");
		drawables->Add(gcnew DrawableAffine(scaleX_, scaleY_, shearX_, shearY_, translateX_, translateY_));
	}
	void MagickScript::ExecuteArc(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double startX_ = XmlHelper::GetAttribute<double>(element, "startX");
		double startY_ = XmlHelper::GetAttribute<double>(element, "startY");
		double endX_ = XmlHelper::GetAttribute<double>(element, "endX");
		double endY_ = XmlHelper::GetAttribute<double>(element, "endY");
		double startDegrees_ = XmlHelper::GetAttribute<double>(element, "startDegrees");
		double endDegrees_ = XmlHelper::GetAttribute<double>(element, "endDegrees");
		drawables->Add(gcnew DrawableArc(startX_, startY_, endX_, endY_, startDegrees_, endDegrees_));
	}
	void MagickScript::ExecuteBezier(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		drawables->Add(gcnew DrawableBezier(coordinates_));
	}
	void MagickScript::ExecuteCircle(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double originX_ = XmlHelper::GetAttribute<double>(element, "originX");
		double originY_ = XmlHelper::GetAttribute<double>(element, "originY");
		double perimeterX_ = XmlHelper::GetAttribute<double>(element, "perimeterX");
		double perimeterY_ = XmlHelper::GetAttribute<double>(element, "perimeterY");
		drawables->Add(gcnew DrawableCircle(originX_, originY_, perimeterX_, perimeterY_));
	}
	void MagickScript::ExecuteClipPath(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		String^ clipPath_ = XmlHelper::GetAttribute<String^>(element, "clipPath");
		drawables->Add(gcnew DrawableClipPath(clipPath_));
	}
	void MagickScript::ExecuteColor(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		PaintMethod paintMethod_ = XmlHelper::GetAttribute<PaintMethod>(element, "paintMethod");
		drawables->Add(gcnew DrawableColor(x_, y_, paintMethod_));
	}
	void MagickScript::ExecuteCompositeImage(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "offset")
				arguments["offset"] = XmlHelper::GetAttribute<MagickGeometry^>(element, "offset");
			else if (attribute->Name == "compose")
				arguments["compose"] = XmlHelper::GetAttribute<CompositeOperator>(element, "compose");
			else if (attribute->Name == "x")
				arguments["x"] = XmlHelper::GetAttribute<double>(element, "x");
			else if (attribute->Name == "y")
				arguments["y"] = XmlHelper::GetAttribute<double>(element, "y");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickImage(elem);
		}
		if (OnlyContains(arguments, "offset", "image"))
			drawables->Add(gcnew DrawableCompositeImage((MagickGeometry^)arguments["offset"], (MagickImage^)arguments["image"]));
		else if (OnlyContains(arguments, "offset", "compose", "image"))
			drawables->Add(gcnew DrawableCompositeImage((MagickGeometry^)arguments["offset"], (CompositeOperator)arguments["compose"], (MagickImage^)arguments["image"]));
		else if (OnlyContains(arguments, "x", "y", "image"))
			drawables->Add(gcnew DrawableCompositeImage((double)arguments["x"], (double)arguments["y"], (MagickImage^)arguments["image"]));
		else if (OnlyContains(arguments, "x", "y", "compose", "image"))
			drawables->Add(gcnew DrawableCompositeImage((double)arguments["x"], (double)arguments["y"], (CompositeOperator)arguments["compose"], (MagickImage^)arguments["image"]));
		else
			throw gcnew ArgumentException("Invalid argument combination for 'compositeImage', allowed combinations are: [offset, image] [offset, compose, image] [x, y, image] [x, y, compose, image]");
	}
	void MagickScript::ExecuteDashOffset(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double offset_ = XmlHelper::GetAttribute<double>(element, "offset");
		drawables->Add(gcnew DrawableDashOffset(offset_));
	}
	void MagickScript::ExecuteEllipse(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double originX_ = XmlHelper::GetAttribute<double>(element, "originX");
		double originY_ = XmlHelper::GetAttribute<double>(element, "originY");
		double radiusX_ = XmlHelper::GetAttribute<double>(element, "radiusX");
		double radiusY_ = XmlHelper::GetAttribute<double>(element, "radiusY");
		double startDegrees_ = XmlHelper::GetAttribute<double>(element, "startDegrees");
		double endDegrees_ = XmlHelper::GetAttribute<double>(element, "endDegrees");
		drawables->Add(gcnew DrawableEllipse(originX_, originY_, radiusX_, radiusY_, startDegrees_, endDegrees_));
	}
	void MagickScript::ExecuteFillColor(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		drawables->Add(gcnew DrawableFillColor(color_));
	}
	void MagickScript::ExecuteFillOpacity(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double opacity_ = XmlHelper::GetAttribute<double>(element, "opacity");
		drawables->Add(gcnew DrawableFillOpacity(opacity_));
	}
	void MagickScript::ExecuteFillRule(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		FillRule fillRule_ = XmlHelper::GetAttribute<FillRule>(element, "fillRule");
		drawables->Add(gcnew DrawableFillRule(fillRule_));
	}
	void MagickScript::ExecuteFont(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "family")
				arguments["family"] = XmlHelper::GetAttribute<String^>(element, "family");
			else if (attribute->Name == "style")
				arguments["style"] = XmlHelper::GetAttribute<FontStyleType>(element, "style");
			else if (attribute->Name == "weight")
				arguments["weight"] = XmlHelper::GetAttribute<FontWeight>(element, "weight");
			else if (attribute->Name == "stretch")
				arguments["stretch"] = XmlHelper::GetAttribute<FontStretch>(element, "stretch");
		}
		if (OnlyContains(arguments, "family"))
			drawables->Add(gcnew DrawableFont((String^)arguments["family"]));
		else if (OnlyContains(arguments, "family", "style", "weight", "stretch"))
			drawables->Add(gcnew DrawableFont((String^)arguments["family"], (FontStyleType)arguments["style"], (FontWeight)arguments["weight"], (FontStretch)arguments["stretch"]));
		else
			throw gcnew ArgumentException("Invalid argument combination for 'font', allowed combinations are: [family] [family, style, weight, stretch]");
	}
	void MagickScript::ExecuteGravity(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		Gravity gravity_ = XmlHelper::GetAttribute<Gravity>(element, "gravity");
		drawables->Add(gcnew DrawableGravity(gravity_));
	}
	void MagickScript::ExecuteLine(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double startX_ = XmlHelper::GetAttribute<double>(element, "startX");
		double startY_ = XmlHelper::GetAttribute<double>(element, "startY");
		double endX_ = XmlHelper::GetAttribute<double>(element, "endX");
		double endY_ = XmlHelper::GetAttribute<double>(element, "endY");
		drawables->Add(gcnew DrawableLine(startX_, startY_, endX_, endY_));
	}
	void MagickScript::ExecuteMatte(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		PaintMethod paintMethod_ = XmlHelper::GetAttribute<PaintMethod>(element, "paintMethod");
		drawables->Add(gcnew DrawableMatte(x_, y_, paintMethod_));
	}
	void MagickScript::ExecuteMiterLimit(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		int miterlimit_ = XmlHelper::GetAttribute<int>(element, "miterlimit");
		drawables->Add(gcnew DrawableMiterLimit(miterlimit_));
	}
	void MagickScript::ExecutePath(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		IEnumerable<PathBase^>^ paths_ = CreatePaths(element);
		drawables->Add(gcnew DrawablePath(paths_));
	}
	void MagickScript::ExecutePoint(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		drawables->Add(gcnew DrawablePoint(x_, y_));
	}
	void MagickScript::ExecutePointSize(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double pointSize_ = XmlHelper::GetAttribute<double>(element, "pointSize");
		drawables->Add(gcnew DrawablePointSize(pointSize_));
	}
	void MagickScript::ExecutePolygon(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		drawables->Add(gcnew DrawablePolygon(coordinates_));
	}
	void MagickScript::ExecutePolyline(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		drawables->Add(gcnew DrawablePolyline(coordinates_));
	}
	void MagickScript::ExecutePushClipPath(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		String^ clipPath_ = XmlHelper::GetAttribute<String^>(element, "clipPath");
		drawables->Add(gcnew DrawablePushClipPath(clipPath_));
	}
	void MagickScript::ExecutePushPattern(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		String^ id_ = XmlHelper::GetAttribute<String^>(element, "id");
		int x_ = XmlHelper::GetAttribute<int>(element, "x");
		int y_ = XmlHelper::GetAttribute<int>(element, "y");
		int width_ = XmlHelper::GetAttribute<int>(element, "width");
		int height_ = XmlHelper::GetAttribute<int>(element, "height");
		drawables->Add(gcnew DrawablePushPattern(id_, x_, y_, width_, height_));
	}
	void MagickScript::ExecuteRectangle(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double upperLeftX_ = XmlHelper::GetAttribute<double>(element, "upperLeftX");
		double upperLeftY_ = XmlHelper::GetAttribute<double>(element, "upperLeftY");
		double lowerRightX_ = XmlHelper::GetAttribute<double>(element, "lowerRightX");
		double lowerRightY_ = XmlHelper::GetAttribute<double>(element, "lowerRightY");
		drawables->Add(gcnew DrawableRectangle(upperLeftX_, upperLeftY_, lowerRightX_, lowerRightY_));
	}
	void MagickScript::ExecuteRotation(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double angle_ = XmlHelper::GetAttribute<double>(element, "angle");
		drawables->Add(gcnew DrawableRotation(angle_));
	}
	void MagickScript::ExecuteRoundRectangle(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double centerX_ = XmlHelper::GetAttribute<double>(element, "centerX");
		double centerY_ = XmlHelper::GetAttribute<double>(element, "centerY");
		double width_ = XmlHelper::GetAttribute<double>(element, "width");
		double height_ = XmlHelper::GetAttribute<double>(element, "height");
		double cornerWidth_ = XmlHelper::GetAttribute<double>(element, "cornerWidth");
		double cornerHeight_ = XmlHelper::GetAttribute<double>(element, "cornerHeight");
		drawables->Add(gcnew DrawableRoundRectangle(centerX_, centerY_, width_, height_, cornerWidth_, cornerHeight_));
	}
	void MagickScript::ExecuteScaling(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		drawables->Add(gcnew DrawableScaling(x_, y_));
	}
	void MagickScript::ExecuteSkewX(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double angle_ = XmlHelper::GetAttribute<double>(element, "angle");
		drawables->Add(gcnew DrawableSkewX(angle_));
	}
	void MagickScript::ExecuteSkewY(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double angle_ = XmlHelper::GetAttribute<double>(element, "angle");
		drawables->Add(gcnew DrawableSkewY(angle_));
	}
	void MagickScript::ExecuteStrokeAntialias(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		bool isEnabled_ = XmlHelper::GetAttribute<bool>(element, "isEnabled");
		drawables->Add(gcnew DrawableStrokeAntialias(isEnabled_));
	}
	void MagickScript::ExecuteStrokeColor(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		drawables->Add(gcnew DrawableStrokeColor(color_));
	}
	void MagickScript::ExecuteStrokeLineCap(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		LineCap lineCap_ = XmlHelper::GetAttribute<LineCap>(element, "lineCap");
		drawables->Add(gcnew DrawableStrokeLineCap(lineCap_));
	}
	void MagickScript::ExecuteStrokeLineJoin(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		LineJoin lineJoin_ = XmlHelper::GetAttribute<LineJoin>(element, "lineJoin");
		drawables->Add(gcnew DrawableStrokeLineJoin(lineJoin_));
	}
	void MagickScript::ExecuteStrokeOpacity(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double opacity_ = XmlHelper::GetAttribute<double>(element, "opacity");
		drawables->Add(gcnew DrawableStrokeOpacity(opacity_));
	}
	void MagickScript::ExecuteStrokeWidth(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double width_ = XmlHelper::GetAttribute<double>(element, "width");
		drawables->Add(gcnew DrawableStrokeWidth(width_));
	}
	void MagickScript::ExecuteText(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		System::Collections::Hashtable^ arguments = gcnew System::Collections::Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "x")
				arguments["x"] = XmlHelper::GetAttribute<double>(element, "x");
			else if (attribute->Name == "y")
				arguments["y"] = XmlHelper::GetAttribute<double>(element, "y");
			else if (attribute->Name == "value")
				arguments["value"] = XmlHelper::GetAttribute<String^>(element, "value");
			else if (attribute->Name == "encoding")
				arguments["encoding"] = XmlHelper::GetAttribute<Encoding^>(element, "encoding");
		}
		if (OnlyContains(arguments, "x", "y", "value"))
			drawables->Add(gcnew DrawableText((double)arguments["x"], (double)arguments["y"], (String^)arguments["value"]));
		else if (OnlyContains(arguments, "x", "y", "value", "encoding"))
			drawables->Add(gcnew DrawableText((double)arguments["x"], (double)arguments["y"], (String^)arguments["value"], (Encoding^)arguments["encoding"]));
		else
			throw gcnew ArgumentException("Invalid argument combination for 'text', allowed combinations are: [x, y, value] [x, y, value, encoding]");
	}
	void MagickScript::ExecuteTextAntialias(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		bool isEnabled_ = XmlHelper::GetAttribute<bool>(element, "isEnabled");
		drawables->Add(gcnew DrawableTextAntialias(isEnabled_));
	}
	void MagickScript::ExecuteTextDecoration(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		TextDecoration decoration_ = XmlHelper::GetAttribute<TextDecoration>(element, "decoration");
		drawables->Add(gcnew DrawableTextDecoration(decoration_));
	}
	void MagickScript::ExecuteTextUnderColor(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		drawables->Add(gcnew DrawableTextUnderColor(color_));
	}
	void MagickScript::ExecuteTranslation(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		drawables->Add(gcnew DrawableTranslation(x_, y_));
	}
	void MagickScript::ExecuteViewbox(XmlElement^ element, System::Collections::ObjectModel::Collection<Drawable^>^ drawables)
	{
		int upperLeftX_ = XmlHelper::GetAttribute<int>(element, "upperLeftX");
		int upperLeftY_ = XmlHelper::GetAttribute<int>(element, "upperLeftY");
		int lowerRightX_ = XmlHelper::GetAttribute<int>(element, "lowerRightX");
		int lowerRightY_ = XmlHelper::GetAttribute<int>(element, "lowerRightY");
		drawables->Add(gcnew DrawableViewbox(upperLeftX_, upperLeftY_, lowerRightX_, lowerRightY_));
	}
	void MagickScript::ExecutePath(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		ExecuteElementPath^ method = dynamic_cast<ExecuteElementPath^>(_StaticExecutePath[element->Name]);
		if (method == nullptr)
			throw gcnew NotImplementedException(element->Name);
		method(element,paths);
	}
	void MagickScript::ExecuteArcAbs(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<PathArc^>^ pathArcs_ = CreatePathArcs(element);
		paths->Add(gcnew PathArcAbs(pathArcs_));
	}
	void MagickScript::ExecuteArcRel(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<PathArc^>^ pathArcs_ = CreatePathArcs(element);
		paths->Add(gcnew PathArcRel(pathArcs_));
	}
	void MagickScript::ExecuteCurvetoAbs(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<PathCurveto^>^ pathCurvetos_ = CreatePathCurvetos(element);
		paths->Add(gcnew PathCurvetoAbs(pathCurvetos_));
	}
	void MagickScript::ExecuteCurvetoRel(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<PathCurveto^>^ pathCurvetos_ = CreatePathCurvetos(element);
		paths->Add(gcnew PathCurvetoRel(pathCurvetos_));
	}
	void MagickScript::ExecuteLinetoAbs(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		paths->Add(gcnew PathLinetoAbs(coordinates_));
	}
	void MagickScript::ExecuteLinetoHorizontalAbs(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		paths->Add(gcnew PathLinetoHorizontalAbs(x_));
	}
	void MagickScript::ExecuteLinetoHorizontalRel(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		paths->Add(gcnew PathLinetoHorizontalRel(x_));
	}
	void MagickScript::ExecuteLinetoRel(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		paths->Add(gcnew PathLinetoRel(coordinates_));
	}
	void MagickScript::ExecuteLinetoVerticalAbs(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		paths->Add(gcnew PathLinetoVerticalAbs(x_));
	}
	void MagickScript::ExecuteLinetoVerticalRel(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		paths->Add(gcnew PathLinetoVerticalRel(x_));
	}
	void MagickScript::ExecuteMovetoAbs(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		paths->Add(gcnew PathMovetoAbs(coordinates_));
	}
	void MagickScript::ExecuteMovetoRel(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		paths->Add(gcnew PathMovetoRel(coordinates_));
	}
	void MagickScript::ExecuteQuadraticCurvetoAbs(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<PathQuadraticCurveto^>^ pathQuadraticCurvetos_ = CreatePathQuadraticCurvetos(element);
		paths->Add(gcnew PathQuadraticCurvetoAbs(pathQuadraticCurvetos_));
	}
	void MagickScript::ExecuteQuadraticCurvetoRel(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<PathQuadraticCurveto^>^ pathQuadraticCurvetos_ = CreatePathQuadraticCurvetos(element);
		paths->Add(gcnew PathQuadraticCurvetoRel(pathQuadraticCurvetos_));
	}
	void MagickScript::ExecuteSmoothCurvetoAbs(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		paths->Add(gcnew PathSmoothCurvetoAbs(coordinates_));
	}
	void MagickScript::ExecuteSmoothCurvetoRel(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		paths->Add(gcnew PathSmoothCurvetoRel(coordinates_));
	}
	void MagickScript::ExecuteSmoothQuadraticCurvetoAbs(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		paths->Add(gcnew PathSmoothQuadraticCurvetoAbs(coordinates_));
	}
	void MagickScript::ExecuteSmoothQuadraticCurvetoRel(XmlElement^ element, System::Collections::ObjectModel::Collection<PathBase^>^ paths)
	{
		IEnumerable<Coordinate>^ coordinates_ = CreateCoordinates(element);
		paths->Add(gcnew PathSmoothQuadraticCurvetoRel(coordinates_));
	}
	Coordinate MagickScript::CreateCoordinate(XmlElement^ element)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		return Coordinate(x_, y_);
	}
	Collection<Coordinate>^  MagickScript::CreateCoordinates(XmlElement^ element)
	{
		Collection<Coordinate>^ collection = gcnew Collection<Coordinate>();
		for each (XmlElement^ elem in element->SelectNodes("*"))
		{
			collection->Add(CreateCoordinate(elem));
		}
		return collection;
	}
	ImageProfile^ MagickScript::CreateImageProfile(XmlElement^ element)
	{
		String^ name_ = XmlHelper::GetAttribute<String^>(element, "name");
		String^ fileName_ = XmlHelper::GetAttribute<String^>(element, "fileName");
		return gcnew ImageProfile(name_, fileName_);
	}
	PathArc^ MagickScript::CreatePathArc(XmlElement^ element)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		double radiusX_ = XmlHelper::GetAttribute<double>(element, "radiusX");
		double radiusY_ = XmlHelper::GetAttribute<double>(element, "radiusY");
		double rotationX_ = XmlHelper::GetAttribute<double>(element, "rotationX");
		bool useLargeArc_ = XmlHelper::GetAttribute<bool>(element, "useLargeArc");
		bool useSweep_ = XmlHelper::GetAttribute<bool>(element, "useSweep");
		return gcnew PathArc(x_, y_, radiusX_, radiusY_, rotationX_, useLargeArc_, useSweep_);
	}
	Collection<PathArc^>^  MagickScript::CreatePathArcs(XmlElement^ element)
	{
		Collection<PathArc^>^ collection = gcnew Collection<PathArc^>();
		for each (XmlElement^ elem in element->SelectNodes("*"))
		{
			collection->Add(CreatePathArc(elem));
		}
		return collection;
	}
	PathCurveto^ MagickScript::CreatePathCurveto(XmlElement^ element)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		double x1_ = XmlHelper::GetAttribute<double>(element, "x1");
		double y1_ = XmlHelper::GetAttribute<double>(element, "y1");
		double x2_ = XmlHelper::GetAttribute<double>(element, "x2");
		double y2_ = XmlHelper::GetAttribute<double>(element, "y2");
		return gcnew PathCurveto(x_, y_, x1_, y1_, x2_, y2_);
	}
	Collection<PathCurveto^>^  MagickScript::CreatePathCurvetos(XmlElement^ element)
	{
		Collection<PathCurveto^>^ collection = gcnew Collection<PathCurveto^>();
		for each (XmlElement^ elem in element->SelectNodes("*"))
		{
			collection->Add(CreatePathCurveto(elem));
		}
		return collection;
	}
	PathQuadraticCurveto^ MagickScript::CreatePathQuadraticCurveto(XmlElement^ element)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		double x1_ = XmlHelper::GetAttribute<double>(element, "x1");
		double y1_ = XmlHelper::GetAttribute<double>(element, "y1");
		return gcnew PathQuadraticCurveto(x_, y_, x1_, y1_);
	}
	Collection<PathQuadraticCurveto^>^  MagickScript::CreatePathQuadraticCurvetos(XmlElement^ element)
	{
		Collection<PathQuadraticCurveto^>^ collection = gcnew Collection<PathQuadraticCurveto^>();
		for each (XmlElement^ elem in element->SelectNodes("*"))
		{
			collection->Add(CreatePathQuadraticCurveto(elem));
		}
		return collection;
	}
	MagickReadSettings^ MagickScript::CreateMagickReadSettings(XmlElement^ element)
	{
		MagickReadSettings^ result = gcnew MagickReadSettings();
		result->ColorSpace = XmlHelper::GetAttribute<Nullable<ColorSpace>>(element, "colorSpace");
		result->Density = XmlHelper::GetAttribute<MagickGeometry^>(element, "density");
		result->Format = XmlHelper::GetAttribute<Nullable<MagickFormat>>(element, "format");
		result->FrameCount = XmlHelper::GetAttribute<Nullable<Int32>>(element, "frameCount");
		result->FrameIndex = XmlHelper::GetAttribute<Nullable<Int32>>(element, "frameIndex");
		result->Height = XmlHelper::GetAttribute<Nullable<Int32>>(element, "height");
		result->Width = XmlHelper::GetAttribute<Nullable<Int32>>(element, "width");
		return result;
	}
}
#pragma warning (default: 4100)
