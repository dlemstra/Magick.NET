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
namespace ImageMagick
{
	void MagickScript::InitializeExecuteMethods()
	{
		_ExecuteMethods = gcnew Hashtable();
		_ExecuteMethods["clipMask"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteClipMask);
		_ExecuteMethods["fillPattern"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteFillPattern);
		_ExecuteMethods["strokePattern"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteStrokePattern);
		_ExecuteMethods["composite"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteComposite);
		_ExecuteMethods["floodFill"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteFloodFill);
		_ExecuteMethods["haldClut"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteHaldClut);
		_ExecuteMethods["inverseFourierTransform"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteInverseFourierTransform);
		_ExecuteMethods["map"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteMap);
		_ExecuteMethods["stegano"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteStegano);
		_ExecuteMethods["stereo"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteStereo);
		_ExecuteMethods["texture"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteTexture);
		_ExecuteMethods["copy"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteCopy);
		_ExecuteMethods["write"] = gcnew ExecuteElementImage(this, &MagickScript::ExecuteWrite);
	}
	Hashtable^ MagickScript::InitializeStaticExecuteMethods()
	{
		Hashtable^ result = gcnew Hashtable();
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
		result["attribute"] = gcnew ExecuteElementImage(MagickScript::ExecuteAttribute);
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
		result["despeckle"] = gcnew ExecuteImage(MagickScript::ExecuteDespeckle);
		result["edge"] = gcnew ExecuteElementImage(MagickScript::ExecuteEdge);
		result["emboss"] = gcnew ExecuteElementImage(MagickScript::ExecuteEmboss);
		result["exifProfile"] = gcnew ExecuteElementImage(MagickScript::ExecuteExifProfile);
		result["extent"] = gcnew ExecuteElementImage(MagickScript::ExecuteExtent);
		result["flip"] = gcnew ExecuteImage(MagickScript::ExecuteFlip);
		result["flop"] = gcnew ExecuteImage(MagickScript::ExecuteFlop);
		result["frame"] = gcnew ExecuteElementImage(MagickScript::ExecuteFrame);
		result["fx"] = gcnew ExecuteElementImage(MagickScript::ExecuteFx);
		result["gamma"] = gcnew ExecuteElementImage(MagickScript::ExecuteGamma);
		result["gaussianBlur"] = gcnew ExecuteElementImage(MagickScript::ExecuteGaussianBlur);
		result["implode"] = gcnew ExecuteElementImage(MagickScript::ExecuteImplode);
		result["level"] = gcnew ExecuteElementImage(MagickScript::ExecuteLevel);
		result["lower"] = gcnew ExecuteElementImage(MagickScript::ExecuteLower);
		result["magnify"] = gcnew ExecuteImage(MagickScript::ExecuteMagnify);
		result["medianFilter"] = gcnew ExecuteElementImage(MagickScript::ExecuteMedianFilter);
		result["minify"] = gcnew ExecuteImage(MagickScript::ExecuteMinify);
		result["modulate"] = gcnew ExecuteElementImage(MagickScript::ExecuteModulate);
		result["motionBlur"] = gcnew ExecuteElementImage(MagickScript::ExecuteMotionBlur);
		result["negate"] = gcnew ExecuteElementImage(MagickScript::ExecuteNegate);
		result["normalize"] = gcnew ExecuteImage(MagickScript::ExecuteNormalize);
		result["oilPaint"] = gcnew ExecuteElementImage(MagickScript::ExecuteOilPaint);
		result["quantize"] = gcnew ExecuteImage(MagickScript::ExecuteQuantize);
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
		result["separate"] = gcnew ExecuteElementImage(MagickScript::ExecuteSeparate);
		result["shade"] = gcnew ExecuteElementImage(MagickScript::ExecuteShade);
		result["shadow"] = gcnew ExecuteElementImage(MagickScript::ExecuteShadow);
		result["sharpen"] = gcnew ExecuteElementImage(MagickScript::ExecuteSharpen);
		result["shave"] = gcnew ExecuteElementImage(MagickScript::ExecuteShave);
		result["shear"] = gcnew ExecuteElementImage(MagickScript::ExecuteShear);
		result["sigmoidalContrast"] = gcnew ExecuteElementImage(MagickScript::ExecuteSigmoidalContrast);
		result["solarize"] = gcnew ExecuteElementImage(MagickScript::ExecuteSolarize);
		result["strip"] = gcnew ExecuteImage(MagickScript::ExecuteStrip);
		result["swirl"] = gcnew ExecuteElementImage(MagickScript::ExecuteSwirl);
		result["threshold"] = gcnew ExecuteElementImage(MagickScript::ExecuteThreshold);
		result["transform"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransform);
		result["transformOrigin"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformOrigin);
		result["transformReset"] = gcnew ExecuteImage(MagickScript::ExecuteTransformReset);
		result["transformRotation"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformRotation);
		result["transformScale"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformScale);
		result["transformSkewX"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformSkewX);
		result["transformSkewY"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransformSkewY);
		result["transparent"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransparent);
		result["transparentChroma"] = gcnew ExecuteElementImage(MagickScript::ExecuteTransparentChroma);
		result["trim"] = gcnew ExecuteImage(MagickScript::ExecuteTrim);
		result["unsharpmask"] = gcnew ExecuteElementImage(MagickScript::ExecuteUnsharpmask);
		result["wave"] = gcnew ExecuteElementImage(MagickScript::ExecuteWave);
		result["zoom"] = gcnew ExecuteElementImage(MagickScript::ExecuteZoom);
		return result;
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
	void MagickScript::ExecuteDensity(XmlElement^ element, MagickImage^ image)
	{
		image->Density = CreateMagickGeometry((XmlElement^)element->SelectSingleNode("geometry"));
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
		image->Page = CreateMagickGeometry((XmlElement^)element->SelectSingleNode("geometry"));
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
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->AdaptiveBlur();
		else if (OnlyContains(arguments, "radius", "sigma"))
			image->AdaptiveBlur((double)arguments["radius"], (double)arguments["sigma"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'AdaptiveBlur', allowed combinations are: [] [radius, sigma]");
	}
	void MagickScript::ExecuteAdaptiveThreshold(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<int>(attribute);
		}
		if (OnlyContains(arguments, "width", "height"))
			image->AdaptiveThreshold((int)arguments["width"], (int)arguments["height"]);
		else if (OnlyContains(arguments, "width", "height", "offset"))
			image->AdaptiveThreshold((int)arguments["width"], (int)arguments["height"], (int)arguments["offset"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'AdaptiveThreshold', allowed combinations are: [width, height] [width, height, offset]");
	}
	void MagickScript::ExecuteAddNoise(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'AddNoise', allowed combinations are: [noiseType] [noiseType, channels]");
	}
	void MagickScript::ExecuteAddProfile(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<String^>(attribute);
		}
		if (OnlyContains(arguments, "fileName"))
			image->AddProfile((String^)arguments["fileName"]);
		else if (OnlyContains(arguments, "name", "fileName"))
			image->AddProfile((String^)arguments["name"], (String^)arguments["fileName"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'AddProfile', allowed combinations are: [fileName] [name, fileName]");
	}
	void MagickScript::ExecuteAnnotate(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "text")
				arguments["text"] = XmlHelper::GetAttribute<String^>(element, "text");
			else if (attribute->Name == "gravity")
				arguments["gravity"] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
			else if (attribute->Name == "degrees")
				arguments["degrees"] = XmlHelper::GetAttribute<double>(element, "degrees");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
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
			throw gcnew ArgumentException("Invalid argument combination for 'Annotate', allowed combinations are: [text, gravity] [text, boundingArea] [text, boundingArea, gravity] [text, boundingArea, gravity, degrees]");
	}
	void MagickScript::ExecuteAttribute(XmlElement^ element, MagickImage^ image)
	{
		String^ name_ = XmlHelper::GetAttribute<String^>(element, "name");
		String^ value_ = XmlHelper::GetAttribute<String^>(element, "value");
		image->Attribute(name_, value_);
	}
	void MagickScript::ExecuteBlur(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Blur', allowed combinations are: [] [channels] [radius, sigma] [radius, sigma, channels]");
	}
	void MagickScript::ExecuteBorder(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<int>(attribute);
		}
		if (OnlyContains(arguments, "size"))
			image->Border((int)arguments["size"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Border((int)arguments["width"], (int)arguments["height"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Border', allowed combinations are: [size] [width, height]");
	}
	void MagickScript::ExecuteCDL(XmlElement^ element, MagickImage^ image)
	{
		String^ fileName_ = XmlHelper::GetAttribute<String^>(element, "fileName");
		image->CDL(fileName_);
	}
	void MagickScript::ExecuteCharcoal(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Charcoal();
		else if (OnlyContains(arguments, "radius", "sigma"))
			image->Charcoal((double)arguments["radius"], (double)arguments["sigma"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Charcoal', allowed combinations are: [] [radius, sigma]");
	}
	void MagickScript::ExecuteChop(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<int>(attribute);
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
		}
		if (OnlyContains(arguments, "geometry"))
			image->Chop((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "xOffset", "width", "yOffset", "height"))
			image->Chop((int)arguments["xOffset"], (int)arguments["width"], (int)arguments["yOffset"], (int)arguments["height"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Chop', allowed combinations are: [geometry] [xOffset, width, yOffset, height]");
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
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Colorize', allowed combinations are: [color, alpha] [color, alphaRed, alphaGreen, alphaBlue]");
	}
	void MagickScript::ExecuteColorMap(XmlElement^ element, MagickImage^ image)
	{
		int index_ = XmlHelper::GetAttribute<int>(element, "index");
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		image->ColorMap(index_, color_);
	}
	void MagickScript::ExecuteComposite(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "gravity")
				arguments["gravity"] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
			else if (attribute->Name == "compose")
				arguments["compose"] = XmlHelper::GetAttribute<CompositeOperator>(element, "compose");
			else if (attribute->Name == "x")
				arguments["x"] = XmlHelper::GetAttribute<int>(element, "x");
			else if (attribute->Name == "y")
				arguments["y"] = XmlHelper::GetAttribute<int>(element, "y");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			if (elem->Name == "image")
				arguments["image"] = CreateMagickImage(elem);
			else if (elem->Name == "offset")
				arguments["offset"] = CreateMagickGeometry(elem);
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
			throw gcnew ArgumentException("Invalid argument combination for 'Composite', allowed combinations are: [image, gravity] [image, offset] [image, gravity, compose] [image, offset, compose] [image, x, y] [image, x, y, compose]");
	}
	void MagickScript::ExecuteContrast(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<bool>(attribute);
		}
		if (arguments->Count == 0)
			image->Contrast();
		else if (OnlyContains(arguments, "enhance"))
			image->Contrast((bool)arguments["enhance"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Contrast', allowed combinations are: [] [enhance]");
	}
	void MagickScript::ExecuteCrop(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
			else if (attribute->Name == "gravity")
				arguments["gravity"] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
		}
		if (OnlyContains(arguments, "geometry"))
			image->Crop((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "width", "height"))
			image->Crop((int)arguments["width"], (int)arguments["height"]);
		else if (OnlyContains(arguments, "width", "height", "gravity"))
			image->Crop((int)arguments["width"], (int)arguments["height"], (Gravity)arguments["gravity"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Crop', allowed combinations are: [geometry] [width, height] [width, height, gravity]");
	}
	void MagickScript::ExecuteCycleColormap(XmlElement^ element, MagickImage^ image)
	{
		int amount_ = XmlHelper::GetAttribute<int>(element, "amount");
		image->CycleColormap(amount_);
	}
	void MagickScript::ExecuteDepth(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Depth', allowed combinations are: [value] [value, channels]");
	}
	void MagickScript::ExecuteDespeckle(MagickImage^ image)
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
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Emboss();
		else if (OnlyContains(arguments, "radius", "sigma"))
			image->Emboss((double)arguments["radius"], (double)arguments["sigma"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Emboss', allowed combinations are: [] [radius, sigma]");
	}
	void MagickScript::ExecuteExifProfile(XmlElement^ element, MagickImage^ image)
	{
		String^ fileName_ = XmlHelper::GetAttribute<String^>(element, "fileName");
		image->ExifProfile(fileName_);
	}
	void MagickScript::ExecuteExtent(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "gravity")
				arguments["gravity"] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
			else if (attribute->Name == "backgroundColor")
				arguments["backgroundColor"] = XmlHelper::GetAttribute<MagickColor^>(element, "backgroundColor");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
		}
		if (OnlyContains(arguments, "geometry"))
			image->Extent((MagickGeometry^)arguments["geometry"]);
		else if (OnlyContains(arguments, "geometry", "gravity"))
			image->Extent((MagickGeometry^)arguments["geometry"], (Gravity)arguments["gravity"]);
		else if (OnlyContains(arguments, "geometry", "backgroundColor"))
			image->Extent((MagickGeometry^)arguments["geometry"], (MagickColor^)arguments["backgroundColor"]);
		else if (OnlyContains(arguments, "geometry", "gravity", "backgroundColor"))
			image->Extent((MagickGeometry^)arguments["geometry"], (Gravity)arguments["gravity"], (MagickColor^)arguments["backgroundColor"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Extent', allowed combinations are: [geometry] [geometry, gravity] [geometry, backgroundColor] [geometry, gravity, backgroundColor]");
	}
	void MagickScript::ExecuteFlip(MagickImage^ image)
	{
		image->Flip();
	}
	void MagickScript::ExecuteFloodFill(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "color")
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
			if (elem->Name == "image")
				arguments["image"] = CreateMagickImage(elem);
			else if (elem->Name == "geometry")
				arguments["geometry"] = CreateMagickGeometry(elem);
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
			throw gcnew ArgumentException("Invalid argument combination for 'FloodFill', allowed combinations are: [image, geometry] [color, geometry] [image, geometry, borderColor] [image, x, y] [color, geometry, borderColor] [color, x, y] [image, x, y, borderColor] [color, x, y, borderColor] [alpha, x, y, paintMethod]");
	}
	void MagickScript::ExecuteFlop(MagickImage^ image)
	{
		image->Flop();
	}
	void MagickScript::ExecuteFrame(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<int>(attribute);
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
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
			throw gcnew ArgumentException("Invalid argument combination for 'Frame', allowed combinations are: [] [geometry] [width, height] [width, height, innerBevel, outerBevel]");
	}
	void MagickScript::ExecuteFx(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Fx', allowed combinations are: [expression] [expression, channels]");
	}
	void MagickScript::ExecuteGamma(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (OnlyContains(arguments, "value"))
			image->Gamma((double)arguments["value"]);
		else if (OnlyContains(arguments, "gammeRed", "gammeGreen", "gammeBlue"))
			image->Gamma((double)arguments["gammeRed"], (double)arguments["gammeGreen"], (double)arguments["gammeBlue"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Gamma', allowed combinations are: [value] [gammeRed, gammeGreen, gammeBlue]");
	}
	void MagickScript::ExecuteGaussianBlur(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'GaussianBlur', allowed combinations are: [width, sigma] [width, sigma, channels]");
	}
	void MagickScript::ExecuteHaldClut(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ image_ = XmlHelper::GetAttribute<MagickImage^>(element, "image");
		image->HaldClut(image_);
	}
	void MagickScript::ExecuteImplode(XmlElement^ element, MagickImage^ image)
	{
		double factor_ = XmlHelper::GetAttribute<double>(element, "factor");
		image->Implode(factor_);
	}
	void MagickScript::ExecuteInverseFourierTransform(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'InverseFourierTransform', allowed combinations are: [image] [image, magnitude]");
	}
	void MagickScript::ExecuteLevel(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Level', allowed combinations are: [blackPoint, whitePoint] [blackPoint, whitePoint, midpoint] [blackPoint, whitePoint, channels] [blackPoint, whitePoint, midpoint, channels]");
	}
	void MagickScript::ExecuteLower(XmlElement^ element, MagickImage^ image)
	{
		int size_ = XmlHelper::GetAttribute<int>(element, "size");
		image->Lower(size_);
	}
	void MagickScript::ExecuteMagnify(MagickImage^ image)
	{
		image->Magnify();
	}
	void MagickScript::ExecuteMap(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Map', allowed combinations are: [image] [image, dither]");
	}
	void MagickScript::ExecuteMedianFilter(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->MedianFilter();
		else if (OnlyContains(arguments, "radius"))
			image->MedianFilter((double)arguments["radius"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'MedianFilter', allowed combinations are: [] [radius]");
	}
	void MagickScript::ExecuteMinify(MagickImage^ image)
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
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<bool>(attribute);
		}
		if (arguments->Count == 0)
			image->Negate();
		else if (OnlyContains(arguments, "onlyGrayscale"))
			image->Negate((bool)arguments["onlyGrayscale"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Negate', allowed combinations are: [] [onlyGrayscale]");
	}
	void MagickScript::ExecuteNormalize(MagickImage^ image)
	{
		image->Normalize();
	}
	void MagickScript::ExecuteOilPaint(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->OilPaint();
		else if (OnlyContains(arguments, "radius"))
			image->OilPaint((double)arguments["radius"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'OilPaint', allowed combinations are: [] [radius]");
	}
	void MagickScript::ExecuteQuantize(MagickImage^ image)
	{
		image->Quantize();
	}
	void MagickScript::ExecuteQuantumOperator(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "channels")
				arguments["channels"] = XmlHelper::GetAttribute<Channels>(element, "channels");
			else if (attribute->Name == "evaluateOperator")
				arguments["evaluateOperator"] = XmlHelper::GetAttribute<EvaluateOperator>(element, "evaluateOperator");
			else if (attribute->Name == "value")
				arguments["value"] = XmlHelper::GetAttribute<double>(element, "value");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
		}
		if (OnlyContains(arguments, "channels", "evaluateOperator", "value"))
			image->QuantumOperator((Channels)arguments["channels"], (EvaluateOperator)arguments["evaluateOperator"], (double)arguments["value"]);
		else if (OnlyContains(arguments, "channels", "geometry", "evaluateOperator", "value"))
			image->QuantumOperator((Channels)arguments["channels"], (MagickGeometry^)arguments["geometry"], (EvaluateOperator)arguments["evaluateOperator"], (double)arguments["value"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'QuantumOperator', allowed combinations are: [channels, evaluateOperator, value] [channels, geometry, evaluateOperator, value]");
	}
	void MagickScript::ExecuteRaise(XmlElement^ element, MagickImage^ image)
	{
		int size_ = XmlHelper::GetAttribute<int>(element, "size");
		image->Raise(size_);
	}
	void MagickScript::ExecuteRandomThreshold(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'RandomThreshold', allowed combinations are: [low, high] [percentageLow, percentageHigh] [low, high, channels] [percentageLow, percentageHigh, channels]");
	}
	void MagickScript::ExecuteReduceNoise(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<int>(attribute);
		}
		if (arguments->Count == 0)
			image->ReduceNoise();
		else if (OnlyContains(arguments, "order"))
			image->ReduceNoise((int)arguments["order"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'ReduceNoise', allowed combinations are: [] [order]");
	}
	void MagickScript::ExecuteRemoveProfile(XmlElement^ element, MagickImage^ image)
	{
		String^ name_ = XmlHelper::GetAttribute<String^>(element, "name");
		image->RemoveProfile(name_);
	}
	void MagickScript::ExecuteResize(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "percentage")
				arguments["percentage"] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
			else if (attribute->Name == "percentageWidth")
				arguments["percentageWidth"] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
			else if (attribute->Name == "percentageHeight")
				arguments["percentageHeight"] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
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
			throw gcnew ArgumentException("Invalid argument combination for 'Resize', allowed combinations are: [percentage] [geometry] [percentageWidth, percentageHeight] [width, height]");
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
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "percentage")
				arguments["percentage"] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
			else if (attribute->Name == "percentageWidth")
				arguments["percentageWidth"] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
			else if (attribute->Name == "percentageHeight")
				arguments["percentageHeight"] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
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
			throw gcnew ArgumentException("Invalid argument combination for 'Sample', allowed combinations are: [percentage] [geometry] [percentageWidth, percentageHeight] [width, height]");
	}
	void MagickScript::ExecuteScale(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "percentage")
				arguments["percentage"] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
			else if (attribute->Name == "percentageWidth")
				arguments["percentageWidth"] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
			else if (attribute->Name == "percentageHeight")
				arguments["percentageHeight"] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
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
			throw gcnew ArgumentException("Invalid argument combination for 'Scale', allowed combinations are: [percentage] [geometry] [percentageWidth, percentageHeight] [width, height]");
	}
	void MagickScript::ExecuteSegment(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Segment();
		else if (OnlyContains(arguments, "clusterThreshold", "smoothingThreshold"))
			image->Segment((double)arguments["clusterThreshold"], (double)arguments["smoothingThreshold"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Segment', allowed combinations are: [] [clusterThreshold, smoothingThreshold]");
	}
	void MagickScript::ExecuteSeparate(XmlElement^ element, MagickImage^ image)
	{
		Channels channels_ = XmlHelper::GetAttribute<Channels>(element, "channels");
		image->Separate(channels_);
	}
	void MagickScript::ExecuteShade(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Shade', allowed combinations are: [] [azimuth, elevation, colorShading]");
	}
	void MagickScript::ExecuteShadow(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Shadow', allowed combinations are: [] [color] [x, y, sigma, alpha] [x, y, sigma, alpha, color]");
	}
	void MagickScript::ExecuteSharpen(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Sharpen', allowed combinations are: [] [channels] [radius, sigma] [radius, sigma, channels]");
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
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'SigmoidalContrast', allowed combinations are: [sharpen, contrast] [sharpen, contrast, midpoint]");
	}
	void MagickScript::ExecuteSolarize(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Solarize();
		else if (OnlyContains(arguments, "factor"))
			image->Solarize((double)arguments["factor"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Solarize', allowed combinations are: [] [factor]");
	}
	void MagickScript::ExecuteStegano(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ watermark_ = XmlHelper::GetAttribute<MagickImage^>(element, "watermark");
		image->Stegano(watermark_);
	}
	void MagickScript::ExecuteStereo(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ rightImage_ = XmlHelper::GetAttribute<MagickImage^>(element, "rightImage");
		image->Stereo(rightImage_);
	}
	void MagickScript::ExecuteStrip(MagickImage^ image)
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
		MagickImage^ image_ = XmlHelper::GetAttribute<MagickImage^>(element, "image");
		image->Texture(image_);
	}
	void MagickScript::ExecuteThreshold(XmlElement^ element, MagickImage^ image)
	{
		double value_ = XmlHelper::GetAttribute<double>(element, "value");
		image->Threshold(value_);
	}
	void MagickScript::ExecuteTransform(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
		}
		if (OnlyContains(arguments, "imageGeometry"))
			image->Transform((MagickGeometry^)arguments["imageGeometry"]);
		else if (OnlyContains(arguments, "imageGeometry", "cropGeometry"))
			image->Transform((MagickGeometry^)arguments["imageGeometry"], (MagickGeometry^)arguments["cropGeometry"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Transform', allowed combinations are: [imageGeometry] [imageGeometry, cropGeometry]");
	}
	void MagickScript::ExecuteTransformOrigin(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->TransformOrigin(x_, y_);
	}
	void MagickScript::ExecuteTransformReset(MagickImage^ image)
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
	void MagickScript::ExecuteTrim(MagickImage^ image)
	{
		image->Trim();
	}
	void MagickScript::ExecuteUnsharpmask(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
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
			throw gcnew ArgumentException("Invalid argument combination for 'Unsharpmask', allowed combinations are: [radius, sigma, amount, threshold] [radius, sigma, amount, threshold, channels]");
	}
	void MagickScript::ExecuteWave(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			arguments[attribute->Name] = XmlHelper::GetValue<double>(attribute);
		}
		if (arguments->Count == 0)
			image->Wave();
		else if (OnlyContains(arguments, "amplitude", "length"))
			image->Wave((double)arguments["amplitude"], (double)arguments["length"]);
		else
			throw gcnew ArgumentException("Invalid argument combination for 'Wave', allowed combinations are: [] [amplitude, length]");
	}
	void MagickScript::ExecuteZoom(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "percentage")
				arguments["percentage"] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
			else if (attribute->Name == "percentageWidth")
				arguments["percentageWidth"] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
			else if (attribute->Name == "percentageHeight")
				arguments["percentageHeight"] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetAttribute<int>(element, "width");
			else if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetAttribute<int>(element, "height");
		}
		for each(XmlElement^ elem in element->SelectNodes("*"))
		{
			arguments[elem->Name] = CreateMagickGeometry(elem);
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
			throw gcnew ArgumentException("Invalid argument combination for 'Zoom', allowed combinations are: [percentage] [geometry] [percentageWidth, percentageHeight] [width, height]");
	}
}
