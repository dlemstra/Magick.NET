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

using System;

namespace ImageMagick
{
  internal static class IMagickImageExtensions
  {
    internal static IntPtr GetInstance(this IMagickImage self)
    {
      if (self == null)
        return IntPtr.Zero;

      INativeInstance nativeInstance = self as INativeInstance;
      if (nativeInstance == null)
        throw new NotSupportedException();

      return nativeInstance.Instance;
    }

    internal static MagickErrorInfo CreateErrorInfo(this IMagickImage self)
    {
      if (self == null)
        return null;

      MagickImage image = self as MagickImage;
      if (image == null)
        throw new NotSupportedException();

      return MagickImage.CreateErrorInfo(image);
    }

    internal static void SetNext(this IMagickImage self, IMagickImage next)
    {
      MagickImage image = self as MagickImage;
      if (image == null)
        throw new NotSupportedException();

      image.SetNext(next);
    }
  }
}
