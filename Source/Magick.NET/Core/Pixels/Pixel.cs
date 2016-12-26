//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
  public sealed class Pixel : IEquatable<Pixel>
  {
    private PixelCollection _Collection;

    private Pixel(PixelCollection collection)
    {
      _Collection = collection;
    }

    private QuantumType[] GetValueWithoutIndexChannel()
    {
      if (_Collection == null)
        return Value;

      int index = _Collection.GetIndex(PixelChannel.Index);
      if (index == -1)
        return Value;

      List<QuantumType> newValue = new List<QuantumType>(Value);
      newValue.RemoveAt(index);

      return newValue.ToArray();
    }

    private void Initialize(int x, int y, QuantumType[] value)
    {
      X = x;
      Y = y;
      Value = value;
    }

    private static void CheckChannels(int channels)
    {
      Throw.IfTrue(nameof(channels), channels < 1 || channels > 5, "Invalid number of channels (supported sizes are 1-5).");
    }

    internal QuantumType[] Value
    {
      get;
      private set;
    }

    internal static Pixel Create(PixelCollection collection, int x, int y, QuantumType[] value)
    {
      Pixel pixel = new Pixel(collection);
      pixel.Initialize(x, y, value);
      return pixel;
    }

    private void UpdateCollection()
    {
      if (_Collection != null)
        _Collection.SetPixelUnchecked(X, Y, Value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pixel"/> class.
    /// </summary>
    /// <param name="x">The X coordinate of the pixel.</param>
    /// <param name="y">The Y coordinate of the pixel.</param>
    /// <param name="value">The value of the pixel.</param>
#if Q16
    [CLSCompliant(false)]
#endif
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

    /// <summary>
    /// Determines whether the specified <see cref="Pixel"/> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="Pixel"/> to compare.</param>
    /// <param name="right"> The second <see cref="Pixel"/> to compare.</param>
    public static bool operator ==(Pixel left, Pixel right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Pixel"/> instances are not considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="Pixel"/> to compare.</param>
    /// <param name="right"> The second <see cref="Pixel"/> to compare.</param>
    public static bool operator !=(Pixel left, Pixel right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Returns the value of the specified channel.
    /// </summary>
    /// <param name="channel">The channel to get the value for.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType this[int channel]
    {
      get
      {
        return GetChannel(channel);
      }
      set
      {
        SetChannel(channel, value);
      }
    }

    /// <summary>
    /// Gets the number of channels that the pixel contains.
    /// </summary>
    public int Channels
    {
      get
      {
        return Value.Length;
      }
    }

    /// <summary>
    /// Gets the X coordinate of the pixel.
    /// </summary>
    public int X
    {
      get;
      private set;
    }

    /// <summary>
    /// Gets the Y coordinate of the pixel.
    /// </summary>
    public int Y
    {
      get;
      private set;
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
    public bool Equals(Pixel other)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      if (Channels != other.Channels)
        return false;

      for (int i = 0; i < Value.Length; i++)
      {
        if (Value[i] != other.Value[i])
          return false;
      }

      return true;
    }

    /// <summary>
    /// Returns the value of the specified channel.
    /// </summary>
    /// <param name="channel">The channel to get the value of.</param>
    /// <returns>The value of the specified channel.</returns>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType GetChannel(int channel)
    {
      if (channel < 0 || channel >= Value.Length)
        return 0;

      return Value[channel];
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
      return Value.GetHashCode();
    }

    /// <summary>
    /// Sets the values of this pixel.
    /// </summary>
    /// <param name="values">The values.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public void Set(QuantumType[] values)
    {
      if (values == null || values.Length != Value.Length)
        return;

      Array.Copy(values, 0, Value, 0, Value.Length);
      UpdateCollection();
    }

    /// <summary>
    /// Set the value of the specified channel.
    /// </summary>
    /// <param name="channel">The channel to set the value of.</param>
    /// <param name="value">The value.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public void SetChannel(int channel, QuantumType value)
    {
      if (channel < 0 || channel >= Value.Length)
        return;

      Value[channel] = value;
      UpdateCollection();
    }

    /// <summary>
    /// Converts the pixel to a color. Assumes the pixel is RGBA.
    /// </summary>
    /// <returns>A <see cref="MagickColor"/> instance.</returns>
    public MagickColor ToColor()
    {
      QuantumType[] value = GetValueWithoutIndexChannel();

      if (value.Length == 0)
        return null;

      if (value.Length == 1)
        return new MagickColor(value[0], value[0], value[0]);

      if (value.Length == 2)
        return new MagickColor(value[0], value[0], value[0], value[1]);

      bool hasBlackChannel = _Collection != null && _Collection.GetIndex(PixelChannel.Black) != -1;

      if (hasBlackChannel)
      {
        if (value.Length == 4)
          return new MagickColor(value[0], value[1], value[2], value[3], Quantum.Max);

        return new MagickColor(value[0], value[1], value[2], value[3], value[4]);
      }

      if (value.Length == 3)
        return new MagickColor(value[0], value[1], value[2]);

      return new MagickColor(value[0], value[1], value[2], value[3]);
    }
  }
}