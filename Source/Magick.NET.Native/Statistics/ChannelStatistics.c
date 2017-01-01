//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
#include "ChannelStatistics.h"

MAGICK_NET_EXPORT const size_t ChannelStatistics_Depth_Get(const ChannelStatistics *instance)
{
  return instance->depth;
}

MAGICK_NET_EXPORT const double ChannelStatistics_Entropy_Get(const ChannelStatistics *instance)
{
  return instance->entropy;
}

MAGICK_NET_EXPORT const double ChannelStatistics_Kurtosis_Get(const ChannelStatistics *instance)
{
  return instance->kurtosis;
}

MAGICK_NET_EXPORT const double ChannelStatistics_Maximum_Get(const ChannelStatistics *instance)
{
  return instance->maxima;
}

MAGICK_NET_EXPORT const double ChannelStatistics_Mean_Get(const ChannelStatistics *instance)
{
  return instance->mean;
}

MAGICK_NET_EXPORT const double ChannelStatistics_Minimum_Get(const ChannelStatistics *instance)
{
  return instance->minima;
}

MAGICK_NET_EXPORT const double ChannelStatistics_Skewness_Get(const ChannelStatistics *instance)
{
  return instance->skewness;
}

MAGICK_NET_EXPORT const double ChannelStatistics_StandardDeviation_Get(const ChannelStatistics *instance)
{
  return instance->standard_deviation;
}

MAGICK_NET_EXPORT const double ChannelStatistics_Sum_Get(const ChannelStatistics *instance)
{
  return instance->sum;
}

MAGICK_NET_EXPORT const double ChannelStatistics_SumCubed_Get(const ChannelStatistics *instance)
{
  return instance->sum_cubed;
}

MAGICK_NET_EXPORT const double ChannelStatistics_SumFourthPower_Get(const ChannelStatistics *instance)
{
  return instance->sum_fourth_power;
}

MAGICK_NET_EXPORT const double ChannelStatistics_SumSquared_Get(const ChannelStatistics *instance)
{
  return instance->sum_squared;
}

MAGICK_NET_EXPORT const double ChannelStatistics_Variance_Get(const ChannelStatistics *instance)
{
  return instance->variance;
}