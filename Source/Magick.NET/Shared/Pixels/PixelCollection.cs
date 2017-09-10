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
    internal sealed partial class PixelCollection : IPixelCollection
    {
        private readonly MagickImage _image;

        internal PixelCollection(MagickImage image)
        {
            _image = image;
            _nativeInstance = new NativePixelCollection(image);
        }

#if NET20
        private delegate TResult Func<T, TResult>(T arg);
#endif

        public int Channels
        {
            get
            {
                return _image.ChannelCount;
            }
        }

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

        public QuantumType[] GetArea(int x, int y, int width, int height)
        {
            CheckArea(x, y, width, height);

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
            return new PixelCollectionEnumerator(this, _image.Width, _image.Height);
        }

        public int GetIndex(PixelChannel channel)
        {
            return _image.ChannelOffset(channel);
        }

        public Pixel GetPixel(int x, int y)
        {
            CheckIndex(x, y);

            return Pixel.Create(this, x, y, GetAreaUnchecked(x, y, 1, 1));
        }

        public QuantumType[] GetValue(int x, int y)
        {
            CheckIndex(x, y);

            return GetAreaUnchecked(x, y, 1, 1);
        }

        public QuantumType[] GetValues()
        {
            return GetAreaUnchecked(0, 0, _image.Width, _image.Height);
        }

        public void Set(Pixel pixel)
        {
            Throw.IfNull(nameof(pixel), pixel);

            SetPixel(pixel.X, pixel.Y, pixel.Value);
        }

        public void Set(IEnumerable<Pixel> pixels)
        {
            Throw.IfNull(nameof(pixels), pixels);

            IEnumerator<Pixel> enumerator = pixels.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Set(enumerator.Current);
            }
        }

        public void Set(int x, int y, QuantumType[] value)
        {
            Throw.IfNullOrEmpty(nameof(value), value);

            SetPixel(x, y, value);
        }

#if !Q8
        public void Set(byte[] values)
        {
            CheckValues(values);

            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(0, 0, _image.Width, _image.Height, castedValues);
        }
#endif

        public void Set(double[] values)
        {
            CheckValues(values);

            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(0, 0, _image.Width, _image.Height, castedValues);
        }

        public void Set(int[] values)
        {
            CheckValues(values);

            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(0, 0, _image.Width, _image.Height, castedValues);
        }

        public void Set(QuantumType[] values)
        {
            CheckValues(values);

            SetAreaUnchecked(0, 0, _image.Width, _image.Height, values);
        }

#if !Q8
        public void SetArea(int x, int y, int width, int height, byte[] values)
        {
          CheckValues(x, y, width, height, values);

          QuantumType[] castedValues = CastArray(values, Quantum.Convert);
          SetAreaUnchecked(x, y, width, height, castedValues);
        }
#endif

        public void SetArea(int x, int y, int width, int height, double[] values)
        {
            CheckValues(x, y, width, height, values);

            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(x, y, width, height, castedValues);
        }

        public void SetArea(int x, int y, int width, int height, int[] values)
        {
            CheckValues(x, y, width, height, values);

            QuantumType[] castedValues = CastArray(values, Quantum.Convert);
            SetAreaUnchecked(x, y, width, height, castedValues);
        }

        public void SetArea(int x, int y, int width, int height, QuantumType[] values)
        {
            CheckValues(x, y, width, height, values);

            SetAreaUnchecked(x, y, width, height, values);
        }

        public QuantumType[] ToArray()
        {
            return GetValues();
        }

        public byte[] ToByteArray(int x, int y, int width, int height, string mapping)
        {
            Throw.IfNullOrEmpty(nameof(mapping), mapping);

            CheckArea(x, y, width, height);
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
            return ToByteArray(0, 0, _image.Width, _image.Height, mapping);
        }

        public ushort[] ToShortArray(int x, int y, int width, int height, string mapping)
        {
            Throw.IfNullOrEmpty(nameof(mapping), mapping);

            CheckArea(x, y, width, height);
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
            return ToShortArray(0, 0, _image.Width, _image.Height, mapping);
        }

        internal QuantumType[] GetAreaUnchecked(int x, int y, int width, int height)
        {
            IntPtr pixels = _nativeInstance.GetArea(x, y, width, height);
            if (pixels == IntPtr.Zero)
                throw new InvalidOperationException("Image contains no pixel data.");

            int length = width * height * _image.ChannelCount;
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

        private void CheckArea(int x, int y, int width, int height)
        {
            CheckIndex(x, y);
            Throw.IfOutOfRange(nameof(width), 0, _image.Width - x, width, "Invalid width: {0}.", width);
            Throw.IfOutOfRange(nameof(height), 0, _image.Height - y, height, "Invalid height: {0}.", height);
        }

        private void CheckIndex(int x, int y)
        {
            Throw.IfOutOfRange(nameof(x), 0, _image.Width - 1, x, "Invalid X coordinate: {0}.", x);
            Throw.IfOutOfRange(nameof(y), 0, _image.Height - 1, y, "Invalid Y coordinate: {0}.", y);
        }

        private void CheckValues<T>(T[] values)
        {
            CheckValues(0, 0, values);
        }

        private void CheckValues<T>(int x, int y, T[] values)
        {
            CheckValues(x, y, _image.Width, _image.Height, values);
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
            max = _image.Width * _image.Height * Channels;
            Throw.IfTrue(nameof(values), length > max, "Too many values specified.");
        }

        private void SetAreaUnchecked(int x, int y, int width, int height, QuantumType[] values)
        {
            _nativeInstance.SetArea(x, y, width, height, values, values.Length);
        }

        private void SetPixel(int x, int y, QuantumType[] value)
        {
            CheckIndex(x, y);

            SetAreaUnchecked(x, y, 1, 1, value);
        }
    }
}
