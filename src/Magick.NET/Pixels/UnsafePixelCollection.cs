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
    internal sealed class UnsafePixelCollection : PixelCollection, IUnsafePixelCollection<QuantumType>
    {
        public UnsafePixelCollection(MagickImage image)
            : base(image)
        {
        }

        public override QuantumType[] GetArea(IMagickGeometry geometry)
        {
            if (geometry == null)
                return null;

            return base.GetArea(geometry);
        }

        public override IntPtr GetAreaPointer(IMagickGeometry geometry)
        {
            if (geometry == null)
                return IntPtr.Zero;

            return base.GetAreaPointer(geometry);
        }

        public override void SetArea(int x, int y, int width, int height, QuantumType[] values)
        {
            if (values != null)
                base.SetArea(x, y, width, height, values);
        }

        public override void SetArea(IMagickGeometry geometry, QuantumType[] values)
        {
            if (geometry != null)
                base.SetArea(geometry, values);
        }

        public override void SetByteArea(int x, int y, int width, int height, byte[] values)
        {
            if (values != null)
                base.SetByteArea(x, y, width, height, values);
        }

        public override void SetByteArea(IMagickGeometry geometry, byte[] values)
        {
            if (geometry != null)
                base.SetByteArea(geometry, values);
        }

        public override void SetBytePixels(byte[] values)
        {
            if (values != null)
                base.SetBytePixels(values);
        }

        public override void SetDoubleArea(int x, int y, int width, int height, double[] values)
        {
            if (values != null)
                base.SetDoubleArea(x, y, width, height, values);
        }

        public override void SetDoubleArea(IMagickGeometry geometry, double[] values)
        {
            if (geometry != null)
                base.SetDoubleArea(geometry, values);
        }

        public override void SetDoublePixels(double[] values)
        {
            if (values != null)
                base.SetDoublePixels(values);
        }

        public override void SetIntArea(int x, int y, int width, int height, int[] values)
        {
            if (values != null)
                base.SetIntArea(x, y, width, height, values);
        }

        public override void SetIntArea(IMagickGeometry geometry, int[] values)
        {
            if (geometry != null)
                base.SetIntArea(geometry, values);
        }

        public override void SetIntPixels(int[] values)
        {
            if (values != null)
                base.SetIntPixels(values);
        }

        public override void SetPixel(IEnumerable<IPixel<QuantumType>> pixels)
        {
            if (pixels != null)
                base.SetPixel(pixels);
        }

        public override void SetPixel(int x, int y, QuantumType[] value)
        {
            if (value != null)
                base.SetPixel(x, y, value);
        }

        public override void SetPixels(QuantumType[] values)
        {
            if (values != null)
                base.SetPixels(values);
        }

        public override byte[] ToByteArray(IMagickGeometry geometry, string mapping)
        {
            if (geometry == null)
                return null;

            return base.ToByteArray(geometry, mapping);
        }

        public override byte[] ToByteArray(int x, int y, int width, int height, string mapping)
        {
            if (mapping == null)
                return null;

            return base.ToByteArray(x, y, width, height, mapping);
        }

        public override ushort[] ToShortArray(IMagickGeometry geometry, string mapping)
        {
            if (geometry == null)
                return null;

            return base.ToShortArray(geometry, mapping);
        }

        public override ushort[] ToShortArray(int x, int y, int width, int height, string mapping)
        {
            if (mapping == null)
                return null;

            return base.ToShortArray(x, y, width, height, mapping);
        }
    }
}
