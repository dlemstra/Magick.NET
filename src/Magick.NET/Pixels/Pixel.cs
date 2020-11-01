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
    /// <summary>
    /// Class that can be used to access an individual pixel of an image.
    /// </summary>
    public sealed class Pixel : IPixel<QuantumType>
    {
        private PixelCollection _collection;
        private QuantumType[] _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pixel"/> class.
        /// </summary>
        /// <param name="x">The X coordinate of the pixel.</param>
        /// <param name="y">The Y coordinate of the pixel.</param>
        /// <param name="value">The value of the pixel.</param>
        public Pixel(int x, int y, QuantumType[] value)
        {
            Throw.IfNull(nameof(value), value);

            CheckChannels(value.Length);
            Initialize(x, y, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pixel"/> class.
        /// </summary>
        /// <param name="x">The X coordinate of the pixel.</param>
        /// <param name="y">The Y coordinate of the pixel.</param>
        /// <param name="channels">The number of channels.</param>
        public Pixel(int x, int y, int channels)
        {
            CheckChannels(channels);
            Initialize(x, y, new QuantumType[channels]);
        }

        private Pixel(PixelCollection collection)
        {
            _collection = collection;
        }

        /// <summary>
        /// Gets the number of channels that the pixel contains.
        /// </summary>
        public int Channels
            => _value.Length;

        /// <summary>
        /// Gets the X coordinate of the pixel.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the Y coordinate of the pixel.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Returns the value of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the value for.</param>
        public QuantumType this[int channel]
        {
            get => GetChannel(channel);
            set => SetChannel(channel, value);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current pixel.
        /// </summary>
        /// <param name="obj">The object to compare pixel color with.</param>
        /// <returns>True when the specified object is equal to the current pixel.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as Pixel);
        }

        /// <summary>
        /// Determines whether the specified pixel is equal to the current pixel.
        /// </summary>
        /// <param name="other">The pixel to compare this color with.</param>
        /// <returns>True when the specified pixel is equal to the current pixel.</returns>
        public bool Equals(IPixel<QuantumType> other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Channels != other.Channels)
                return false;

            var otherValue = other.ToArray();

            for (int i = 0; i < _value.Length; i++)
            {
                if (_value[i] != otherValue[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns the value of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the value of.</param>
        /// <returns>The value of the specified channel.</returns>
        public QuantumType GetChannel(int channel)
        {
            if (channel < 0 || channel >= _value.Length)
                return 0;

            return _value[channel];
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
            => _value.GetHashCode();

        /// <summary>
        /// Set the value of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the value of.</param>
        /// <param name="value">The value.</param>
        public void SetChannel(int channel, QuantumType value)
        {
            if (channel < 0 || channel >= _value.Length)
                return;

            _value[channel] = value;
            UpdateCollection();
        }

        /// <summary>
        /// Sets the values of this pixel.
        /// </summary>
        /// <param name="values">The values.</param>
        public void SetValues(QuantumType[] values)
        {
            if (values == null || values.Length != _value.Length)
                return;

            Array.Copy(values, 0, _value, 0, _value.Length);
            UpdateCollection();
        }

        /// <summary>
        /// Returns the value of this pixel as an array.
        /// </summary>
        /// <returns>A <see cref="QuantumType"/> array.</returns>
        public QuantumType[] ToArray()
            => _value;

        /// <summary>
        /// Converts the pixel to a color. Assumes the pixel is RGBA.
        /// </summary>
        /// <returns>A <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
        public IMagickColor<QuantumType> ToColor()
        {
            var value = GetValueWithoutIndexChannel();

            if (value.Length == 0)
                return null;

            if (value.Length == 1)
                return new MagickColor(value[0], value[0], value[0]);

            if (value.Length == 2)
                return new MagickColor(value[0], value[0], value[0], value[1]);

            bool hasBlackChannel = _collection != null && _collection.GetIndex(PixelChannel.Black) != -1;
            bool hasAlphaChannel = _collection != null && _collection.GetIndex(PixelChannel.Alpha) != -1;

            if (hasBlackChannel)
            {
                if (value.Length == 4 || !hasAlphaChannel)
                    return new MagickColor(value[0], value[1], value[2], value[3], Quantum.Max);

                return new MagickColor(value[0], value[1], value[2], value[3], value[4]);
            }

            if (value.Length == 3 || !hasAlphaChannel)
                return new MagickColor(value[0], value[1], value[2]);

            return new MagickColor(value[0], value[1], value[2], value[3]);
        }

        internal static Pixel Create(PixelCollection collection, int x, int y, QuantumType[] value)
        {
            var pixel = new Pixel(collection);
            pixel.Initialize(x, y, value);
            return pixel;
        }

        private static void CheckChannels(int channels)
        {
            Throw.IfTrue(nameof(channels), channels < 1 || channels > 5, "Invalid number of channels (supported sizes are 1-5).");
        }

        private QuantumType[] GetValueWithoutIndexChannel()
        {
            if (_collection == null)
                return _value;

            int index = _collection.GetIndex(PixelChannel.Index);
            if (index == -1)
                return _value;

            List<QuantumType> newValue = new List<QuantumType>(_value);
            newValue.RemoveAt(index);

            return newValue.ToArray();
        }

        private void Initialize(int x, int y, QuantumType[] value)
        {
            X = x;
            Y = y;
            _value = value;
        }

        private void UpdateCollection()
        {
            if (_collection != null)
                _collection.SetPixelUnchecked(X, Y, _value);
        }
    }
}
