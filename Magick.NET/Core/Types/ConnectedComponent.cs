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
using System.Collections.ObjectModel;

namespace ImageMagick
{
  /// <summary>
  /// Encapsulation of the ImageMagick connected component object.
  /// </summary>
  public sealed partial class ConnectedComponent
  {
    private ConnectedComponent(IntPtr instance)
    {
      Height = NativeConnectedComponent.GetHeight(instance);
      Width = NativeConnectedComponent.GetWidth(instance);
      X = NativeConnectedComponent.GetX(instance);
      Y = NativeConnectedComponent.GetY(instance);
    }

    internal static IEnumerable<ConnectedComponent> Create(IntPtr list, int length)
    {
      Collection<ConnectedComponent> result = new Collection<ConnectedComponent>();

      if (list == IntPtr.Zero)
        return result;

      for (int i = 0; i < length; i++)
      {
        IntPtr instance = NativeConnectedComponent.GetInstance(list, i);
        if (instance == IntPtr.Zero)
          continue;

        if (NativeConnectedComponent.GetArea(instance) < double.Epsilon)
          continue;

        result.Add(new ConnectedComponent(instance));
      }

      return result;
    }

    internal static void DisposeList(IntPtr list)
    {
      if (list != IntPtr.Zero)
        NativeConnectedComponent.DisposeList(list);
    }

    /// <summary>
    /// The height of the area.
    /// </summary>
    public int Height
    {
      get;
      private set;
    }

    /// <summary>
    /// The width of the area.
    /// </summary>
    public int Width
    {
      get;
      private set;
    }

    /// <summary>
    /// X offset from origin
    /// </summary>
    public int X
    {
      get;
      private set;
    }

    /// <summary>
    /// Y offset from origin
    /// </summary>
    public int Y
    {
      get;
      private set;
    }

    /// <summary>
    /// Returns the geometry of the area of this connected component.
    /// </summary>
    public MagickGeometry ToGeometry()
    {
      return new MagickGeometry(X, Y, Width, Height);
    }

    /// <summary>
    /// Returns the geometry of the area of this connected component.
    /// </summary>
    /// <param name="extent">The number of pixels to extent the image width.</param>
    public MagickGeometry ToGeometry(int extent)
    {
      int extra;
      checked
      {
        extra = extent * 2;
      }

      return new MagickGeometry(X - extent, Y - extent, Width + extra, Height + extra);
    }
  }
}
