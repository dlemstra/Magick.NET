// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.
#pragma once

MAGICK_NET_EXPORT const PointInfo *ChannelMoments_Centroid_Get(const ChannelMoments *);

MAGICK_NET_EXPORT const double ChannelMoments_EllipseAngle_Get(const ChannelMoments *);

MAGICK_NET_EXPORT const double ChannelMoments_EllipseEccentricity_Get(const ChannelMoments *);

MAGICK_NET_EXPORT const double ChannelMoments_EllipseIntensity_Get(const ChannelMoments *);

MAGICK_NET_EXPORT const PointInfo *ChannelMoments_EllipseAxis_Get(const ChannelMoments *);

MAGICK_NET_EXPORT const double ChannelMoments_GetHuInvariants(const ChannelMoments *, const size_t);