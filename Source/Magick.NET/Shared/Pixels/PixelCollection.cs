// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    internal abstract partial class PixelCollection : IPixelCollection
    {
        protected PixelCollection(MagickImage image)
        {
            Image = image;
            _nativeInstance = new NativePixelCollection(image);
        }

#if NET20
        private delegate TResult Func<T, TResult>(T arg);
#endif

        public int Channels
        {
            get
            {
                return Image.ChannelCount;
            }
        }

        protected MagickImage Image { get; }

        public Pixel this[int x, int y]
        {
            get
            {
                return GetPixel(x, y);
            }
        }

        public void Dispose()
        {
            _nativeInstance.Dispose();
        }

        public virtual QuantumType[] GetArea(int x, int y, int width, int height)
        {
            return GetAreaUnchecked(x, y, width, height);
        }

        public QuantumType[] GetArea(MagickGeometry geometry)
        {
            Throw.IfNull(nameof(geometry), geometry);

            return GetArea(geometry.X, geometry.Y, geometry.Width, geometry.Height);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Pixel> GetEnumerator()
        {
            return new PixelCollectionEnumerator(this, Image.Width, Image.Height);
        }

        public int GetIndex(PixelChannel channel)
        {
            return Image.ChannelOffset(channel);
        }

        public virtual Pixel GetPixel(int x, int y)
        {
            return Pixel.Create(this, x, y, GetAreaUnchecked(x, y, 1, 1));
        }

        public virtual QuantumType[] GetValue(int x, int y)
        {
            return GetAreaUnchecked(x, y, 1, 1);
        }

        public QuantumType[] GetValues()
        {
            return GetAreaUnchecked(0, 0, Image.Width, Image.Height);
        }

        public virtual void SetPixel(Pixel pixel)
        {
            if (pixel != null)
                SetPixelUnchecked(pixel.X, pixel.Y, pixel.Value);
        }

        public void SetPixel(IEnumerable<Pixel> pixels)
        {
            Throw.IfNull(nameof(pixels), pixels);

            IEnumerator<Pixel> enumerator = pixels.GetEnumerator();

            while (enumerator.MoveNext())
            {
                SetPixel(enumerator.Current);
            }
        }

        public virtual void SetPixel(int x, int y, QuantumType[] value)
        {
            SetPixelUnchecked(x, y, value);
        }

#if !Q8
        public virtual void SetPixels(byte[] values)
        {
            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(0, 0, Image.Width, Image.Height, castedValues);
        }
#endif

        public virtual void SetPixels(double[] values)
        {
            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(0, 0, Image.Width, Image.Height, castedValues);
        }

        public virtual void SetPixels(int[] values)
        {
            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(0, 0, Image.Width, Image.Height, castedValues);
        }

        public virtual void SetPixels(QuantumType[] values)
        {
            SetAreaUnchecked(0, 0, Image.Width, Image.Height, values);
        }

#if !Q8
        public virtual void SetArea(int x, int y, int width, int height, byte[] values)
        {
            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(x, y, width, height, castedValues);
        }
#endif

        public virtual void SetArea(int x, int y, int width, int height, double[] values)
        {
            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(x, y, width, height, castedValues);
        }

        public virtual void SetArea(int x, int y, int width, int height, int[] values)
        {
            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(x, y, width, height, castedValues);
        }

        public virtual void SetArea(int x, int y, int width, int height, QuantumType[] values)
        {
            SetAreaUnchecked(x, y, width, height, values);
        }

        public QuantumType[] ToArray()
        {
            return GetValues();
        }

        public virtual byte[] ToByteArray(int x, int y, int width, int height, string mapping)
        {
            Throw.IfNullOrEmpty(nameof(mapping), mapping);

            IntPtr nativeResult = IntPtr.Zero;
            byte[] result = null;

            try
            {
                nativeResult = _nativeInstance.ToByteArray(x, y, width, height, mapping);
                result = ByteConverter.ToArray(nativeResult, width * height * mapping.Length);
            }
            finally
            {
                MagickMemory.Relinquish(nativeResult);
            }

            return result;
        }

        public byte[] ToByteArray(MagickGeometry geometry, string mapping)
        {
            Throw.IfNull(nameof(geometry), geometry);

            return ToByteArray(geometry.X, geometry.Y, geometry.Width, geometry.Height, mapping);
        }

        public byte[] ToByteArray(string mapping)
        {
            return ToByteArray(0, 0, Image.Width, Image.Height, mapping);
        }

        public virtual ushort[] ToShortArray(int x, int y, int width, int height, string mapping)
        {
            Throw.IfNullOrEmpty(nameof(mapping), mapping);

            IntPtr nativeResult = IntPtr.Zero;
            ushort[] result = null;

            try
            {
                nativeResult = _nativeInstance.ToShortArray(x, y, width, height, mapping);
                result = ShortConverter.ToArray(nativeResult, width * height * mapping.Length);
            }
            finally
            {
                MagickMemory.Relinquish(nativeResult);
            }

            return result;
        }

        public ushort[] ToShortArray(MagickGeometry geometry, string mapping)
        {
            Throw.IfNull(nameof(geometry), geometry);

            return ToShortArray(geometry.X, geometry.Y, geometry.Width, geometry.Height, mapping);
        }

        public ushort[] ToShortArray(string mapping)
        {
            return ToShortArray(0, 0, Image.Width, Image.Height, mapping);
        }

        internal QuantumType[] GetAreaUnchecked(int x, int y, int width, int height)
        {
            IntPtr pixels = _nativeInstance.GetArea(x, y, width, height);
            if (pixels == IntPtr.Zero)
                throw new InvalidOperationException("Image contains no pixel data.");

            int length = width * height * Image.ChannelCount;
            return QuantumConverter.ToArray(pixels, length);
        }

        internal void SetPixelUnchecked(int x, int y, QuantumType[] value)
        {
            SetAreaUnchecked(x, y, 1, 1, value);
        }

        private static QuantumType[] CastArray<T>(T[] values, Func<T, QuantumType> convertMethod)
        {
            QuantumType[] result = new QuantumType[values.Length];
            for (int i = 0; i < values.Length; i++)
                result[i] = convertMethod(values[i]);

            return result;
        }

        private void SetAreaUnchecked(int x, int y, int width, int height, QuantumType[] values)
        {
            _nativeInstance.SetArea(x, y, width, height, values, values.Length);
        }
    }
}
