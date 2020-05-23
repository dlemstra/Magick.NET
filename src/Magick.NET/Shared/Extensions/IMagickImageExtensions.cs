// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    internal static class IMagickImageExtensions
    {
        internal static IntPtr GetInstance(this IMagickImage<QuantumType> self)
        {
            if (self == null)
                return IntPtr.Zero;

            if (self is INativeInstance nativeInstance)
                return nativeInstance.Instance;

            throw new NotSupportedException();
        }

        internal static MagickSettings GetSettings(this IMagickImage<QuantumType> self)
        {
            var settings = self?.Settings as MagickSettings;
            if (settings != null)
                return settings;

            throw new NotSupportedException();
        }

        internal static MagickErrorInfo CreateErrorInfo(this IMagickImage<QuantumType> self)
        {
            if (self == null)
                return null;

            if (self is MagickImage image)
                return MagickImage.CreateErrorInfo(image);

            throw new NotSupportedException();
        }

        internal static void SetNext(this IMagickImage<QuantumType> self, IMagickImage<QuantumType> next)
        {
            if (self is MagickImage image)
                image.SetNext(next);
            else
                throw new NotSupportedException();
        }
    }
}
