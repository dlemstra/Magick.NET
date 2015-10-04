//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections;

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
  ///<summary>
  /// Class that can be used to access the individual pixels of an image.
  ///</summary>
  public sealed class PixelCollection : IDisposable, IEnumerable<Pixel>
  {
    private Wrapper.PixelCollection _Instance;

    private void CheckIndex(int y)
    {
      Throw.IfFalse("y", y >= 0 && y < Height, "Invalid Y coordinate: {0}.", y);
    }

    private void CheckIndex(int x, int y)
    {
      Throw.IfFalse("x", x >= 0 && x < Width, "Invalid X coordinate: {0}.", x);
      Throw.IfFalse("y", y >= 0 && y < Height, "Invalid Y coordinate: {0}.", y);
    }

    internal PixelCollection(Wrapper.PixelCollection instance)
    {
      _Instance = instance;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    ///<summary>
    /// Returns the pixel at the specified coordinate.
    ///</summary>
    public Pixel this[int x, int y]
    {
      get
      {
        return GetPixel(x, y);
      }
    }

    ///<summary>
    /// Returns the number of channels that the image contains.
    ///</summary>
    public int Channels
    {
      get
      {
        return _Instance.Channels;
      }
    }

    ///<summary>
    /// Returns the height.
    ///</summary>
    public int Height
    {
      get
      {
        return _Instance.Height;
      }
    }

    ///<summary>
    /// Returns the width.
    ///</summary>
    public int Width
    {
      get
      {
        return _Instance.Width;
      }
    }

    /// <summary>
    /// Disposes the PixelCollection instance.
    /// </summary>
    public void Dispose()
    {
      _Instance.Dispose();
    }

    /// <summary>
    ///  Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<Pixel> GetEnumerator()
    {
      return new PixelCollectionEnumerator(_Instance);
    }

    ///<summary>
    /// Returns the index of the specified channel. Returns -1 if not found.
    ///</summary>
    ///<param name="channel">The channel to get the index of.</param>
    public int GetIndex(PixelChannel channel)
    {
      return _Instance.GetIndex(channel);
    }

    ///<summary>
    /// Returns the pixel at the specified coordinate.
    ///</summary>
    ///<param name="x">The X coordinate of the pixel.</param>
    ///<param name="y">The Y coordinate of the pixel.</param>
    public Pixel GetPixel(int x, int y)
    {
      CheckIndex(x, y);

      return Pixel.Create(null, x, y, _Instance.GetValue(x, y));
    }

    ///<summary>
    /// Returns the value of the specified coordinate.
    ///</summary>
    ///<param name="x">The X coordinate of the pixel.</param>
    ///<param name="y">The Y coordinate of the pixel.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType[] GetValue(int x, int y)
    {
      CheckIndex(x, y);

      return _Instance.GetValue(x, y);
    }

    ///<summary>
    /// Returns the values of the pixels as an array.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType[] GetValues()
    {
      return _Instance.GetValues();
    }

    ///<summary>
    /// Returns the values of the pixels as an array.
    ///</summary>
    ///<param name="y">The Y coordinate.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType[] GetValues(int y)
    {
      CheckIndex(y);

      return _Instance.GetValues(y);
    }
  }
}
