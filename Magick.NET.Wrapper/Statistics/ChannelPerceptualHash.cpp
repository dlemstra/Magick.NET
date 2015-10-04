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
#include "ChannelPerceptualHash.h"
#include "..\Helpers\ExceptionHelper.h"

namespace ImageMagick
{
  namespace Wrapper
  {
    void ChannelPerceptualHash::Initialize(const Magick::ChannelPerceptualHash channelPerceptualHash)
    {
      Channel = (PixelChannel)channelPerceptualHash.channel();
      SrgbHuPhash = gcnew array<double>(7);
      HclpHuPhash = gcnew array<double>(7);
      for (int i = 0; i < 7; i++)
      {
        SrgbHuPhash[i] = channelPerceptualHash.srgbHuPhash(i);
        HclpHuPhash[i] = channelPerceptualHash.hclpHuPhash(i);
      }
      std::string hash = channelPerceptualHash;
      Hash = Marshaller::Marshal(hash);
    }

    ChannelPerceptualHash::ChannelPerceptualHash(const Magick::ChannelPerceptualHash channelPerceptualHash)
    {
      Initialize(channelPerceptualHash);
    }

    ChannelPerceptualHash::ChannelPerceptualHash(String^ hash)
    {
      try
      {
        std::string magickHash;
        Marshaller::Marshal(hash, magickHash);
        Magick::ChannelPerceptualHash channelPerceptualHash = Magick::ChannelPerceptualHash(
          Magick::UndefinedPixelChannel, magickHash);
        if (!channelPerceptualHash.isValid())
          throw gcnew ArgumentException("Invalid hash specified", "hash");

        Initialize(channelPerceptualHash);
      }
      catch (Magick::Exception&)
      {
        throw gcnew ArgumentException("Invalid hash specified", "hash");
      }
    }
  }
}