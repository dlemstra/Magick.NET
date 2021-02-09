// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;

namespace ImageMagick
{
    internal sealed partial class MagickRectangle
    {
        public MagickRectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        private MagickRectangle(NativeMagickRectangle instance)
        {
            X = instance.X;
            Y = instance.Y;
            Width = instance.Width;
            Height = instance.Height;
        }

        public int Height { get; set; }

        public int Width { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public static MagickRectangle FromGeometry(IMagickGeometry geometry, MagickImage image)
        {
            if (geometry == null)
                return null;

            int width = geometry.Width;
            int height = geometry.Height;

            if (geometry.IsPercentage)
            {
                width = image.Width * new Percentage(geometry.Width);
                height = image.Height * new Percentage(geometry.Height);
            }

            return new MagickRectangle(geometry.X, geometry.Y, width, height);
        }

        internal static INativeInstance CreateInstance() => new NativeMagickRectangle();

        internal static MagickRectangle CreateInstance(INativeInstance nativeInstance)
        {
            var instance = nativeInstance as NativeMagickRectangle;
            if (instance == null)
                throw new InvalidOperationException();

            return new MagickRectangle(instance);
        }

        private NativeMagickRectangle CreateNativeInstance()
        {
            return new NativeMagickRectangle
            {
                X = X,
                Y = Y,
                Width = Width,
                Height = Height,
            };
        }
    }
}