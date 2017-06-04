//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

namespace ImageMagick
{
  internal sealed partial class MagickRectangle
  {
    private MagickRectangle(NativeMagickRectangle instance)
    {
      X = instance.X;
      Y = instance.Y;
      Width = instance.Width;
      Height = instance.Height;
    }

    private NativeMagickRectangle CreateNativeInstance()
    {
      NativeMagickRectangle instance = new NativeMagickRectangle();
      instance.X = X;
      instance.Y = Y;
      instance.Width = Width;
      instance.Height = Height;

      return instance;
    }

    internal static INativeInstance CreateInstance()
    {
      return new NativeMagickRectangle();
    }

    internal static MagickRectangle CreateInstance(INativeInstance nativeInstance)
    {
      NativeMagickRectangle instance = nativeInstance as NativeMagickRectangle;
      if (instance == null)
        throw new InvalidOperationException();

      return new MagickRectangle(instance);
    }

    public MagickRectangle(int x, int y, int width, int height)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }

    public int Height
    {
      get;
      set;
    }

    public int Width
    {
      get;
      set;
    }

    public int X
    {
      get;
      set;
    }

    public int Y
    {
      get;
      set;
    }

    public static MagickRectangle FromGeometry(MagickGeometry geometry, MagickImage image)
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
  }
}