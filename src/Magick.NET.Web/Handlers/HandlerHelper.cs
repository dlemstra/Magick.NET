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

namespace ImageMagick.Web
{
    internal static class HandlerHelper
    {
        private static readonly ImageOptimizer _ImageOptimizer = new ImageOptimizer();

        public static bool CanCompress(MagickWebSettings settings, MagickFormatInfo formatInfo)
        {
            if (!settings.EnableGzip)
                return false;

            return formatInfo.Format == MagickFormat.Svg;
        }

        public static bool CanOptimize(MagickWebSettings settings, MagickFormatInfo formatInfo)
        {
            if (!settings.Optimization.IsEnabled)
                return false;

            return _ImageOptimizer.IsSupported(formatInfo);
        }
    }
}
