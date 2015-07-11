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
#include "Drawables.h"
#include "Paths.h"
#include "..\MagickImage.h"

using namespace System::Collections::ObjectModel;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		Magick::DrawableBase* Drawables::CreateDrawable(IDrawable^ drawable, Type^ interfaceType)
		{
			if (IDrawableAffine::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableAffine((IDrawableAffine^)drawable);

			if (IDrawableArc::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableArc((IDrawableArc^)drawable);

			if (IDrawableBezier::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableBezier((IDrawableBezier^)drawable);

			if (IDrawableCircle::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableCircle((IDrawableCircle^)drawable);

			if (IDrawableClipPath::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableClipPath((IDrawableClipPath^)drawable);

			if (IDrawableClipPath::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableClipPath((IDrawableClipPath^)drawable);

			if (IDrawableColor::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableColor((IDrawableColor^)drawable);

			if (IDrawableCompositeImage::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableCompositeImage((IDrawableCompositeImage^)drawable);

			if (IDrawableDashArray::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableDashArray((IDrawableDashArray^)drawable);

			if (IDrawableDashOffset::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableDashOffset((IDrawableDashOffset^)drawable);

			if (IDrawableDensity::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableDensity((IDrawableDensity^)drawable);

			if (IDrawableEllipse::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableEllipse((IDrawableEllipse^)drawable);

			if (IDrawableFillColor::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableFillColor((IDrawableFillColor^)drawable);

			if (IDrawableFillOpacity::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableFillOpacity((IDrawableFillOpacity^)drawable);

			if (IDrawableFillRule::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableFillRule((IDrawableFillRule^)drawable);

			if (IDrawableFont::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableFont((IDrawableFont^)drawable);

			if (IDrawableGravity::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableGravity((IDrawableGravity^)drawable);

			if (IDrawableLine::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableLine((IDrawableLine^)drawable);

			if (IDrawableMiterLimit::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableMiterLimit((IDrawableMiterLimit^)drawable);

			if (IDrawableOpacity::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableOpacity((IDrawableOpacity^)drawable);

			if (IDrawablePath::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePath((IDrawablePath^)drawable);

			if (IDrawablePoint::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePoint((IDrawablePoint^)drawable);

			if (IDrawablePointSize::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePointSize((IDrawablePointSize^)drawable);

			if (IDrawablePolygon::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePolygon((IDrawablePolygon^)drawable);

			if (IDrawablePolyline::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePolyline((IDrawablePolyline^)drawable);

			if (IDrawablePopClipPath::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePopClipPath();

			if (IDrawablePopGraphicContext::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePopGraphicContext();

			if (IDrawablePopPattern::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePopPattern();

			if (IDrawablePushClipPath::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePushClipPath((IDrawablePushClipPath^)drawable);

			if (IDrawablePushGraphicContext::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePushGraphicContext();

			if (IDrawablePushPattern::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawablePushPattern((IDrawablePushPattern^)drawable);

			if (IDrawableRectangle::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableRectangle((IDrawableRectangle^)drawable);

			if (IDrawableRotation::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableRotation((IDrawableRotation^)drawable);

			if (IDrawableRoundRectangle::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableRoundRectangle((IDrawableRoundRectangle^)drawable);

			if (IDrawableScaling::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableScaling((IDrawableScaling^)drawable);

			if (IDrawableSkewX::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableSkewX((IDrawableSkewX^)drawable);

			if (IDrawableSkewY::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableSkewY((IDrawableSkewY^)drawable);

			if (IDrawableStrokeAntialias::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableStrokeAntialias((IDrawableStrokeAntialias^)drawable);

			if (IDrawableStrokeColor::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableStrokeColor((IDrawableStrokeColor^)drawable);

			if (IDrawableStrokeLineCap::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableStrokeLineCap((IDrawableStrokeLineCap^)drawable);

			if (IDrawableStrokeLineJoin::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableStrokeLineJoin((IDrawableStrokeLineJoin^)drawable);

			if (IDrawableStrokeOpacity::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableStrokeOpacity((IDrawableStrokeOpacity^)drawable);

			if (IDrawableStrokeWidth::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableStrokeWidth((IDrawableStrokeWidth^)drawable);

			if (IDrawableText::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableText((IDrawableText^)drawable);

			if (IDrawableTextAntialias::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableTextAntialias((IDrawableTextAntialias^)drawable);

			if (IDrawableTextDecoration::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableTextDecoration((IDrawableTextDecoration^)drawable);

			if (IDrawableTextDirection::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableTextDirection((IDrawableTextDirection^)drawable);

			if (IDrawableTextInterlineSpacing::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableTextInterlineSpacing((IDrawableTextInterlineSpacing^)drawable);

			if (IDrawableTextInterwordSpacing::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableTextInterwordSpacing((IDrawableTextInterwordSpacing^)drawable);

			if (IDrawableTextKerning::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableTextKerning((IDrawableTextKerning^)drawable);

			if (IDrawableTextUnderColor::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableTextUnderColor((IDrawableTextUnderColor^)drawable);

			if (IDrawableTranslation::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableTranslation((IDrawableTranslation^)drawable);

			if (IDrawableViewbox::typeid->IsAssignableFrom(interfaceType))
				return CreateDrawableViewbox((IDrawableViewbox^)drawable);

			throw gcnew NotImplementedException(interfaceType->Name);
		}
		//===========================================================================================
		IEnumerable<Drawable>^ Drawables::CreateDrawables(IDrawable^ drawable)
		{
			Collection<Drawable>^ drawables = gcnew Collection<Drawable>();

			Type^ type = drawable->GetType();
			for each(Type^ interfaceType in type->GetInterfaces())
			{
				if (interfaceType == IDrawable::typeid)
					continue;

				if (!IDrawable::typeid->IsAssignableFrom(interfaceType))
					continue;

				Magick::DrawableBase* magickDrawable = CreateDrawable(drawable, interfaceType);
				drawables->Add(Drawable(magickDrawable));
			}

			if (drawables->Count == 0)
				throw gcnew NotImplementedException();

			return drawables;
		}
		//===========================================================================================
		Magick::DrawableAffine* Drawables::CreateDrawableAffine(IDrawableAffine^ drawableAffine)
		{
			return new Magick::DrawableAffine(drawableAffine->ScaleX, drawableAffine->ScaleY,
				drawableAffine->ShearX, drawableAffine->ShearY, drawableAffine->TranslateX,
				drawableAffine->TranslateY);
		}
		//===========================================================================================
		Magick::DrawableArc* Drawables::CreateDrawableArc(IDrawableArc^ drawableAlpha)
		{
			return new Magick::DrawableArc(drawableAlpha->StartX, drawableAlpha->StartY,
				drawableAlpha->EndX, drawableAlpha->EndY, drawableAlpha->StartDegrees,
				drawableAlpha->EndDegrees);
		}
		//===========================================================================================
		Magick::DrawableBezier* Drawables::CreateDrawableBezier(IDrawableBezier^ drawableBezier)
		{
			return CreateDrawable<Magick::DrawableBezier>(drawableBezier->Coordinates);
		}
		//===========================================================================================
		Magick::DrawableCircle* Drawables::CreateDrawableCircle(IDrawableCircle^ drawableCircle)
		{
			return new Magick::DrawableCircle(drawableCircle->OriginX, drawableCircle->OriginY,
				drawableCircle->PerimeterX, drawableCircle->PerimeterY);
		}
		//===========================================================================================
		Magick::DrawableClipPath* Drawables::CreateDrawableClipPath(IDrawableClipPath^ drawableClipPath)
		{
			std::string clipPath;
			return new Magick::DrawableClipPath(Marshaller::Marshal(drawableClipPath->ClipPath, clipPath));
		}
		//===========================================================================================
		Magick::DrawableColor* Drawables::CreateDrawableColor(IDrawableColor^ drawableColor)
		{
			return new Magick::DrawableColor(drawableColor->X, drawableColor->Y,
				(Magick::PaintMethod)drawableColor->PaintMethod);
		}
		//===========================================================================================
		Magick::DrawableCompositeImage* Drawables::CreateDrawableCompositeImage(IDrawableCompositeImage^ drawableCompositeImage)
		{
			MagickImage^ image = dynamic_cast<MagickImage^>(drawableCompositeImage->Image);
			Throw::IfNull("image", image);

			return new Magick::DrawableCompositeImage(drawableCompositeImage->X, drawableCompositeImage->Y,
				drawableCompositeImage->Width, drawableCompositeImage->Height, image->ReuseValue(),
				(Magick::CompositeOperator)drawableCompositeImage->Compose);
		}
		//===========================================================================================
		Magick::DrawableDashArray* Drawables::CreateDrawableDashArray(IDrawableDashArray^ drawableCompositeImage)
		{
			double* dashArray = Marshaller::MarshalAndTerminate(drawableCompositeImage->Dash);
			Magick::DrawableDashArray* result = new Magick::DrawableDashArray(dashArray);
			delete[] dashArray;
			return result;
		}
		//===========================================================================================
		Magick::DrawableDashOffset* Drawables::CreateDrawableDashOffset(IDrawableDashOffset^ drawableDashOffset)
		{
			return new Magick::DrawableDashOffset(drawableDashOffset->Offset);
		}
		//===========================================================================================
		Magick::DrawableDensity* Drawables::CreateDrawableDensity(IDrawableDensity^ drawableDensity)
		{
			Magick::Point density(drawableDensity->Density.X,drawableDensity->Density.Y);
			return new Magick::DrawableDensity(density);
		}
		//===========================================================================================
		Magick::DrawableEllipse* Drawables::CreateDrawableEllipse(IDrawableEllipse^ drawableEllipse)
		{
			return new Magick::DrawableEllipse(drawableEllipse->OriginX, drawableEllipse->OriginY,
				drawableEllipse->RadiusX, drawableEllipse->RadiusY, drawableEllipse->StartDegrees,
				drawableEllipse->EndDegrees);
		}
		//===========================================================================================
		Magick::DrawableFillColor* Drawables::CreateDrawableFillColor(IDrawableFillColor^ drawableFillAlpha)
		{
			MagickColor^ color = dynamic_cast<MagickColor^>(drawableFillAlpha->Color);
			Throw::IfNull("color", color);

			const Magick::Color* magickColor = color->CreateColor();
			Magick::DrawableFillColor* result = new Magick::DrawableFillColor(*magickColor);
			delete magickColor;
			return result;
		}
		//===========================================================================================
		Magick::DrawableFillOpacity* Drawables::CreateDrawableFillOpacity(IDrawableFillOpacity^ drawableFillOpacity)
		{
			return new Magick::DrawableFillOpacity(drawableFillOpacity->Opacity);
		}
		//===========================================================================================
		Magick::DrawableFillRule* Drawables::CreateDrawableFillRule(IDrawableFillRule^ drawableFillRule)
		{
			return new Magick::DrawableFillRule((Magick::FillRule)drawableFillRule->FillRule);
		}
		//===========================================================================================
		Magick::DrawableFont* Drawables::CreateDrawableFont(IDrawableFont^ drawableFont)
		{
			std::string family;
			return new Magick::DrawableFont(Marshaller::Marshal(drawableFont->Family, family),
				(Magick::StyleType)drawableFont->Style, (int)drawableFont->Weight,
				(Magick::StretchType)drawableFont->Stretch);
		}
		//===========================================================================================
		Magick::DrawableGravity* Drawables::CreateDrawableGravity(IDrawableGravity^ drawableGravity)
		{
			return new Magick::DrawableGravity((Magick::GravityType)drawableGravity->Gravity);
		}
		//===========================================================================================
		Magick::DrawableLine* Drawables::CreateDrawableLine(IDrawableLine^ drawableLine)
		{
			return new Magick::DrawableLine(drawableLine->StartX, drawableLine->StartY,
				drawableLine->EndX, drawableLine->EndY);
		}
		//===========================================================================================
		Magick::DrawableMiterLimit* Drawables::CreateDrawableMiterLimit(IDrawableMiterLimit^ drawableMiterLimit)
		{
			return new Magick::DrawableMiterLimit(drawableMiterLimit->Miterlimit);
		}
		//===========================================================================================
		Magick::DrawableAlpha* Drawables::CreateDrawableOpacity(IDrawableOpacity^ drawableOpacity)
		{
			return new Magick::DrawableAlpha(drawableOpacity->X, drawableOpacity->Y,
				(Magick::PaintMethod)drawableOpacity->PaintMethod);
		}
		//===========================================================================================
		Magick::DrawablePath* Drawables::CreateDrawablePath(IDrawablePath^ drawablePath)
		{
			Magick::VPathList paths;
			for each(IPath^ path in drawablePath->Paths)
			{
				Magick::VPathBase* pathBase = Paths::CreatePath(path);
				paths.push_back(Magick::VPath(*pathBase));
				delete pathBase;
			}

			return new Magick::DrawablePath(paths);
		}
		//===========================================================================================
		Magick::DrawablePoint* Drawables::CreateDrawablePoint(IDrawablePoint^ drawablePoint)
		{
			return new Magick::DrawablePoint(drawablePoint->X, drawablePoint->Y);
		}
		//===========================================================================================
		Magick::DrawablePointSize* Drawables::CreateDrawablePointSize(IDrawablePointSize^ drawablePointSize)
		{
			return new Magick::DrawablePointSize(drawablePointSize->PointSize);
		}
		//===========================================================================================
		Magick::DrawablePolygon* Drawables::CreateDrawablePolygon(IDrawablePolygon^ drawablePolygon)
		{
			return CreateDrawable<Magick::DrawablePolygon>(drawablePolygon->Coordinates);
		}
		//===========================================================================================
		Magick::DrawablePolyline* Drawables::CreateDrawablePolyline(IDrawablePolyline^ drawablePolyline)
		{
			return CreateDrawable<Magick::DrawablePolyline>(drawablePolyline->Coordinates);
		}
		//===========================================================================================
		Magick::DrawablePopClipPath* Drawables::CreateDrawablePopClipPath()
		{
			return new Magick::DrawablePopClipPath();
		}
		//===========================================================================================
		Magick::DrawablePopGraphicContext* Drawables::CreateDrawablePopGraphicContext()
		{
			return new Magick::DrawablePopGraphicContext();
		}
		//===========================================================================================
		Magick::DrawablePopPattern* Drawables::CreateDrawablePopPattern()
		{
			return new Magick::DrawablePopPattern();
		}
		//========================================================================================
		Magick::DrawablePushClipPath* Drawables::CreateDrawablePushClipPath(IDrawablePushClipPath^ drawablePushClipPath)
		{
			std::string clipPath;
			return new Magick::DrawablePushClipPath(Marshaller::Marshal(drawablePushClipPath->ClipPath, clipPath));
		}
		//===========================================================================================
		Magick::DrawablePushGraphicContext* Drawables::CreateDrawablePushGraphicContext()
		{
			return new Magick::DrawablePushGraphicContext();
		}
		//===========================================================================================
		Magick::DrawablePushPattern* Drawables::CreateDrawablePushPattern(IDrawablePushPattern^ drawablePushPattern)
		{
			std::string id;
			return new Magick::DrawablePushPattern(Marshaller::Marshal(drawablePushPattern->ID, id),
				drawablePushPattern->X, drawablePushPattern->Y, drawablePushPattern->Width,
				drawablePushPattern->Height);
		}
		//===========================================================================================
		Magick::DrawableRectangle* Drawables::CreateDrawableRectangle(IDrawableRectangle^ drawableRectangle)
		{
			return new Magick::DrawableRectangle(drawableRectangle->UpperLeftX, drawableRectangle->UpperLeftY,
				drawableRectangle->LowerRightX, drawableRectangle->LowerRightY);
		}
		//===========================================================================================
		Magick::DrawableRotation* Drawables::CreateDrawableRotation(IDrawableRotation^ drawableRotation)
		{
			return new Magick::DrawableRotation(drawableRotation->Angle);
		}
		//===========================================================================================
		Magick::DrawableRoundRectangle* Drawables::CreateDrawableRoundRectangle(IDrawableRoundRectangle^ drawableRoundRectangle)
		{
			return new Magick::DrawableRoundRectangle(drawableRoundRectangle->CenterX,
				drawableRoundRectangle->CenterY, drawableRoundRectangle->Width, drawableRoundRectangle->Height,
				drawableRoundRectangle->CornerWidth, drawableRoundRectangle->CornerHeight);
		}
		//===========================================================================================
		Magick::DrawableScaling* Drawables::CreateDrawableScaling(IDrawableScaling^ drawableScaling)
		{
			return new Magick::DrawableScaling(drawableScaling->X, drawableScaling->Y);
		}
		//===========================================================================================
		Magick::DrawableSkewX* Drawables::CreateDrawableSkewX(IDrawableSkewX^ drawableSkewX)
		{
			return new Magick::DrawableSkewX(drawableSkewX->Angle);
		}
		//===========================================================================================
		Magick::DrawableSkewY* Drawables::CreateDrawableSkewY(IDrawableSkewY^ drawableSkewY)
		{
			return new Magick::DrawableSkewY(drawableSkewY->Angle);
		}
		//===========================================================================================
		Magick::DrawableStrokeAntialias* Drawables::CreateDrawableStrokeAntialias(IDrawableStrokeAntialias^ drawableStrokeAntialias)
		{
			return new Magick::DrawableStrokeAntialias(drawableStrokeAntialias->IsEnabled);
		}
		//===========================================================================================
		Magick::DrawableStrokeColor* Drawables::CreateDrawableStrokeColor(IDrawableStrokeColor^ drawableStrokeColor)
		{
			MagickColor^ color = dynamic_cast<MagickColor^>(drawableStrokeColor->Color);
			Throw::IfNull("color", color);

			const Magick::Color* magickColor = color->CreateColor();
			Magick::DrawableStrokeColor* result = new Magick::DrawableStrokeColor(*magickColor);
			delete magickColor;
			return result;
		}
		//===========================================================================================
		Magick::DrawableStrokeLineCap* Drawables::CreateDrawableStrokeLineCap(IDrawableStrokeLineCap^ drawableStrokeLineCap)
		{
			return new Magick::DrawableStrokeLineCap((Magick::LineCap) drawableStrokeLineCap->LineCap);
		}
		//===========================================================================================
		Magick::DrawableStrokeLineJoin* Drawables::CreateDrawableStrokeLineJoin(IDrawableStrokeLineJoin^ drawableStrokeLineJoin)
		{
			return new Magick::DrawableStrokeLineJoin((Magick::LineJoin) drawableStrokeLineJoin->LineJoin);
		}
		//===========================================================================================
		Magick::DrawableStrokeOpacity* Drawables::CreateDrawableStrokeOpacity(IDrawableStrokeOpacity^ drawableStrokeOpacity)
		{
			return new Magick::DrawableStrokeOpacity(drawableStrokeOpacity->Opacity);
		}
		//===========================================================================================
		Magick::DrawableStrokeWidth* Drawables::CreateDrawableStrokeWidth(IDrawableStrokeWidth^ drawableStrokeWidth)
		{
			return new Magick::DrawableStrokeWidth(drawableStrokeWidth->Width);
		}
		//===========================================================================================
		Magick::DrawableText* Drawables::CreateDrawableText(IDrawableText^ drawableText)
		{
			std::string drawText;
			Marshaller::Marshal(drawableText->Text, drawText);

			if (drawableText->Encoding == nullptr)
				return new Magick::DrawableText(drawableText->X, drawableText->Y, drawText);

			std::string drawEncoding;
			Marshaller::Marshal(drawableText->Encoding->BodyName, drawEncoding);
			return new Magick::DrawableText(drawableText->X, drawableText->Y, drawText, drawEncoding);
		}
		//===========================================================================================
		Magick::DrawableTextAntialias* Drawables::CreateDrawableTextAntialias(IDrawableTextAntialias^ drawableTextAntialias)
		{
			return new Magick::DrawableTextAntialias(drawableTextAntialias->IsEnabled);
		}
		//===========================================================================================
		Magick::DrawableTextDecoration* Drawables::CreateDrawableTextDecoration(IDrawableTextDecoration^ drawableTextDecoration)
		{
			return new Magick::DrawableTextDecoration((Magick::DecorationType)drawableTextDecoration->Decoration);
		}
		//===========================================================================================
		Magick::DrawableTextDirection* Drawables::CreateDrawableTextDirection(IDrawableTextDirection^ drawableTextDirection)
		{
			return new Magick::DrawableTextDirection((Magick::DirectionType) drawableTextDirection->Direction);
		}
		//===========================================================================================
		Magick::DrawableTextInterlineSpacing* Drawables::CreateDrawableTextInterlineSpacing(IDrawableTextInterlineSpacing^ drawableTextInterlineSpacing)
		{
			return new Magick::DrawableTextInterlineSpacing(drawableTextInterlineSpacing->Spacing);
		}
		//===========================================================================================
		Magick::DrawableTextInterwordSpacing* Drawables::CreateDrawableTextInterwordSpacing(IDrawableTextInterwordSpacing^ drawableTextInterwordSpacing)
		{
			return new Magick::DrawableTextInterwordSpacing(drawableTextInterwordSpacing->Spacing);
		}
		//===========================================================================================
		Magick::DrawableTextKerning* Drawables::CreateDrawableTextKerning(IDrawableTextKerning^ drawableTextKerning)
		{
			return new Magick::DrawableTextKerning(drawableTextKerning->Kerning);
		}
		//===========================================================================================
		Magick::DrawableTextUnderColor* Drawables::CreateDrawableTextUnderColor(IDrawableTextUnderColor^ drawableTextUnderColor)
		{
			MagickColor^ color = dynamic_cast<MagickColor^>(drawableTextUnderColor->Color);
			Throw::IfNull("color", color);

			const Magick::Color* magickColor = color->CreateColor();
			Magick::DrawableTextUnderColor* result = new Magick::DrawableTextUnderColor(*magickColor);
			delete magickColor;
			return result;
		}
		//===========================================================================================
		Magick::DrawableTranslation* Drawables::CreateDrawableTranslation(IDrawableTranslation^ drawableTranslation)
		{
			return new Magick::DrawableTranslation(drawableTranslation->X, drawableTranslation->Y);
		}
		//===========================================================================================
		Magick::DrawableViewbox* Drawables::CreateDrawableViewbox(IDrawableViewbox^ drawableViewbox)
		{
			return new Magick::DrawableViewbox(drawableViewbox->UpperLeftX, drawableViewbox->UpperLeftY,
				drawableViewbox->LowerRightX, drawableViewbox->LowerRightY);
		}
		//===========================================================================================
	}
}