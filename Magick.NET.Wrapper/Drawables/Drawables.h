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

#include "Stdafx.h"
#include "Drawable.h"

using namespace System::Collections::Generic;
using namespace ImageMagick::Drawables;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		private ref class Drawables abstract sealed
		{
			//========================================================================================
		private:
			//========================================================================================
			template<typename TDrawable>
			static TDrawable* CreateDrawable(IEnumerable<Coordinate>^ coordinates)
			{
				Magick::CoordinateList magickCoordinates;
				IEnumerator<Coordinate>^ enumerator = coordinates->GetEnumerator();

				int count = 0;
				while(enumerator->MoveNext())
				{
					magickCoordinates.push_back(Magick::Coordinate(enumerator->Current.X, enumerator->Current.Y));
					count++;
				}

				return new TDrawable(magickCoordinates);
			}
			//========================================================================================
			static Magick::DrawableBase* CreateDrawable(IDrawable^ drawable, Type^ interfaceType);
			//========================================================================================
		public:
			//========================================================================================
			static IEnumerable<Drawable>^ CreateDrawables(IDrawable^ drawable);
			//========================================================================================
			static Magick::DrawableAffine* CreateDrawableAffine(IDrawableAffine^ drawableAffine);
			//========================================================================================
			static Magick::DrawableArc* CreateDrawableArc(IDrawableArc^ drawableArc);
			//========================================================================================
			static Magick::DrawableBezier* CreateDrawableBezier(IDrawableBezier^ drawableBezier);
			//========================================================================================
			static Magick::DrawableCircle* CreateDrawableCircle(IDrawableCircle^ drawableCircle);
			//========================================================================================
			static Magick::DrawableClipPath* CreateDrawableClipPath(IDrawableClipPath^ drawableClipPath);
			//========================================================================================
			static Magick::DrawableColor* CreateDrawableColor(IDrawableColor^ drawableColor);
			//========================================================================================
			static Magick::DrawableCompositeImage* CreateDrawableCompositeImage(IDrawableCompositeImage^ drawableCompositeImage);
			//===========================================================================================
			static Magick::DrawableDashArray* CreateDrawableDashArray(IDrawableDashArray^ drawableCompositeImage);
			//===========================================================================================
			static Magick::DrawableDashOffset* CreateDrawableDashOffset(IDrawableDashOffset^ drawableDashOffset);
			//===========================================================================================
			static Magick::DrawableEllipse* CreateDrawableEllipse(IDrawableEllipse^ drawableEllipse);
			//========================================================================================
			static Magick::DrawableFillColor* CreateDrawableFillColor(IDrawableFillColor^ drawableFillColor);
			//===========================================================================================
			static Magick::DrawableFillAlpha* CreateDrawableFillOpacity(IDrawableFillOpacity^ drawableFillOpacity);
			//========================================================================================
			static Magick::DrawableFillRule* CreateDrawableFillRule(IDrawableFillRule^ drawableFillRule);
			//========================================================================================
			static Magick::DrawableFont* CreateDrawableFont(IDrawableFont^ drawableFont);
			//========================================================================================
			static Magick::DrawableGravity* CreateDrawableGravity(IDrawableGravity^ drawableGravity);
			//========================================================================================
			static Magick::DrawableLine* CreateDrawableLine(IDrawableLine^ drawableLine);
			//========================================================================================
			static Magick::DrawableMiterLimit* CreateDrawableMiterLimit(IDrawableMiterLimit^ drawableMiterLimit);
			//========================================================================================
			static Magick::DrawableMatte* CreateDrawableOpacity(IDrawableOpacity^ drawableOpacity);
			//========================================================================================
			static Magick::DrawablePath* CreateDrawablePath(IDrawablePath^ drawablePath);
			//========================================================================================
			static Magick::DrawablePoint* CreateDrawablePoint(IDrawablePoint^ drawablePoint);
			//========================================================================================
			static Magick::DrawablePointSize* CreateDrawablePointSize(IDrawablePointSize^ drawablePointSize);
			//========================================================================================
			static Magick::DrawablePolygon* CreateDrawablePolygon(IDrawablePolygon^ drawablePolygon);
			//========================================================================================
			static Magick::DrawablePolyline* CreateDrawablePolyline(IDrawablePolyline^ drawablePolyline);
			//========================================================================================
			static Magick::DrawablePopClipPath* CreateDrawablePopClipPath();
			//========================================================================================
			static Magick::DrawablePopGraphicContext* CreateDrawablePopGraphicContext();
			//========================================================================================
			static Magick::DrawablePopPattern* CreateDrawablePopPattern();
			//========================================================================================
			static Magick::DrawablePushClipPath* CreateDrawablePushClipPath(IDrawablePushClipPath^ drawablePushClipPath);
			//========================================================================================
			static Magick::DrawablePushGraphicContext* CreateDrawablePushGraphicContext();
			//========================================================================================
			static Magick::DrawablePushPattern* CreateDrawablePushPattern(IDrawablePushPattern^ drawablePushPattern);
			//========================================================================================
			static Magick::DrawableRectangle* CreateDrawableRectangle(IDrawableRectangle^ drawableRectangle);
			//========================================================================================
			static Magick::DrawableRotation* CreateDrawableRotation(IDrawableRotation^ drawableRotation);
			//========================================================================================
			static Magick::DrawableRoundRectangle* CreateDrawableRoundRectangle(IDrawableRoundRectangle^ drawableRoundRectangle);
			//========================================================================================
			static Magick::DrawableScaling* CreateDrawableScaling(IDrawableScaling^ drawableScaling);
			//========================================================================================
			static Magick::DrawableSkewX* CreateDrawableSkewX(IDrawableSkewX^ drawableSkewX);
			//========================================================================================
			static Magick::DrawableSkewY* CreateDrawableSkewY(IDrawableSkewY^ drawableSkewY);
			//========================================================================================
			static Magick::DrawableStrokeAntialias* CreateDrawableStrokeAntialias(IDrawableStrokeAntialias^ drawableStrokeAntialias);
			//========================================================================================
			static Magick::DrawableStrokeColor* CreateDrawableStrokeColor(IDrawableStrokeColor^ drawableStrokeColor);
			//========================================================================================
			static Magick::DrawableStrokeLineCap* CreateDrawableStrokeLineCap(IDrawableStrokeLineCap^ drawableStrokeLineCap);
			//========================================================================================
			static Magick::DrawableStrokeLineJoin* CreateDrawableStrokeLineJoin(IDrawableStrokeLineJoin^ drawableStrokeLineJoin);
			//========================================================================================
			static Magick::DrawableStrokeAlpha* CreateDrawableStrokeOpacity(IDrawableStrokeOpacity^ drawableStrokeOpacity);
			//========================================================================================
			static Magick::DrawableStrokeWidth* CreateDrawableStrokeWidth(IDrawableStrokeWidth^ drawableStrokeWidth);
			//========================================================================================
			static Magick::DrawableText* CreateDrawableText(IDrawableText^ drawableText);
			//========================================================================================
			static Magick::DrawableTextAntialias* CreateDrawableTextAntialias(IDrawableTextAntialias^ drawableTextAntialias);
			//========================================================================================
			static Magick::DrawableTextDecoration* CreateDrawableTextDecoration(IDrawableTextDecoration^ drawableTextDecoration);
			//========================================================================================
			static Magick::DrawableTextDirection* CreateDrawableTextDirection(IDrawableTextDirection^ drawableTextDirection);
			//========================================================================================
			static Magick::DrawableTextInterlineSpacing* CreateDrawableTextInterlineSpacing(IDrawableTextInterlineSpacing^ drawableTextInterlineSpacing);
			//========================================================================================
			static Magick::DrawableTextInterwordSpacing* CreateDrawableTextInterwordSpacing(IDrawableTextInterwordSpacing^ drawableTextInterwordSpacing);
			//========================================================================================
			static Magick::DrawableTextKerning* CreateDrawableTextKerning(IDrawableTextKerning^ drawableTextKerning);
			//========================================================================================
			static Magick::DrawableTextUnderColor* CreateDrawableTextUnderColor(IDrawableTextUnderColor^ drawableTextUnderColor);
			//========================================================================================
			static Magick::DrawableTranslation* CreateDrawableTranslation(IDrawableTranslation^ drawableTranslation);
			//========================================================================================
			static Magick::DrawableViewbox* CreateDrawableViewbox(IDrawableViewbox^ drawableViewbox);
			//========================================================================================
		};
		//===========================================================================================
	}
}