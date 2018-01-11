﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Collections;
using System.Collections.Generic;

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
    internal sealed class SafePixelCollection : PixelCollection
    {
        public SafePixelCollection(MagickImage image)
            : base(image)
        {
        }

        public override QuantumType[] GetArea(int x, int y, int width, int height)
        {
            CheckArea(x, y, width, height);

            return GetAreaUnchecked(x, y, width, height);
        }

        public override Pixel GetPixel(int x, int y)
        {
            CheckIndex(x, y);

            return base.GetPixel(x, y);
        }

        public override QuantumType[] GetValue(int x, int y)
        {
            CheckIndex(x, y);

            return GetAreaUnchecked(x, y, 1, 1);
        }

        public override void SetPixel(Pixel pixel)
        {
            Throw.IfNull(nameof(pixel), pixel);

            SetPixelPrivate(pixel.X, pixel.Y, pixel.Value);
        }

        public override void SetPixel(int x, int y, QuantumType[] value)
        {
            Throw.IfNullOrEmpty(nameof(value), value);

            SetPixelPrivate(x, y, value);
        }

#if !Q8
        public override void SetPixels(byte[] values)
        {
            CheckValues(values);
            base.SetPixels(values);
        }
#endif

        public override void SetPixels(double[] values)
        {
            CheckValues(values);
            base.SetPixels(values);
        }

        public override void SetPixels(int[] values)
        {
            CheckValues(values);
            base.SetPixels(values);
        }

        public override void SetPixels(QuantumType[] values)
        {
            CheckValues(values);
            base.SetPixels(values);
        }

#if !Q8
        public override void SetArea(int x, int y, int width, int height, byte[] values)
        {
            CheckValues(x, y, width, height, values);
            base.SetArea(x, y, width, height, values);
        }
#endif

        public override void SetArea(int x, int y, int width, int height, double[] values)
        {
            CheckValues(x, y, width, height, values);
            base.SetArea(x, y, width, height, values);
        }

        public override void SetArea(int x, int y, int width, int height, int[] values)
        {
            CheckValues(x, y, width, height, values);
            base.SetArea(x, y, width, height, values);
        }

        public override void SetArea(int x, int y, int width, int height, QuantumType[] values)
        {
            CheckValues(x, y, width, height, values);
            base.SetArea(x, y, width, height, values);
        }

        public override byte[] ToByteArray(int x, int y, int width, int height, string mapping)
        {
            CheckArea(x, y, width, height);
            return base.ToByteArray(x, y, width, height, mapping);
        }

        public override ushort[] ToShortArray(int x, int y, int width, int height, string mapping)
        {
            CheckArea(x, y, width, height);
            return base.ToShortArray(x, y, width, height, mapping);
        }

        private void CheckArea(int x, int y, int width, int height)
        {
            CheckIndex(x, y);
            Throw.IfOutOfRange(nameof(width), 1, Image.Width - x, width, "Invalid width: {0}.", width);
            Throw.IfOutOfRange(nameof(height), 1, Image.Height - y, height, "Invalid height: {0}.", height);
        }

        private void CheckIndex(int x, int y)
        {
            Throw.IfOutOfRange(nameof(x), 0, Image.Width - 1, x, "Invalid X coordinate: {0}.", x);
            Throw.IfOutOfRange(nameof(y), 0, Image.Height - 1, y, "Invalid Y coordinate: {0}.", y);
        }

        private void CheckValues<T>(T[] values)
        {
            CheckValues(0, 0, values);
        }

        private void CheckValues<T>(int x, int y, T[] values)
        {
            CheckValues(x, y, Image.Width, Image.Height, values);
        }

        private void CheckValues<T>(int x, int y, int width, int height, T[] values)
        {
            CheckIndex(x, y);
            Throw.IfNullOrEmpty(nameof(values), values);
            Throw.IfFalse(nameof(values), values.Length % Channels == 0, "Values should have {0} channels.", Channels);

            int length = values.Length;
            int max = width * height * Channels;
            Throw.IfTrue(nameof(values), length > max, "Too many values specified.");

            length = (x * y * Channels) + length;
            max = Image.Width * Image.Height * Channels;
            Throw.IfTrue(nameof(values), length > max, "Too many values specified.");
        }

        private void SetPixelPrivate(int x, int y, QuantumType[] value)
        {
            CheckIndex(x, y);

            base.SetPixel(x, y, value);
        }
    }
}
