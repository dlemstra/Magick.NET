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
using System.Text;

namespace ImageMagick
{
  internal sealed partial class DrawingSettings
  {
    private void DisposeInstance()
    {
      if (_NativeInstance != null)
        _NativeInstance.Dispose();
    }

    internal DrawingSettings()
    {
      _NativeInstance = new NativeDrawingSettings();
    }

    ~DrawingSettings()
    {
      DisposeInstance();
    }

    public MagickColor BorderColor
    {
      get
      {
        return _NativeInstance.BorderColor;
      }
      set
      {
        _NativeInstance.BorderColor = value;
      }
    }

    public MagickColor FillColor
    {
      get
      {
        return _NativeInstance.FillColor;
      }
      set
      {
        _NativeInstance.FillColor = value;
      }
    }

    public MagickImage FillPattern
    {
      get
      {
        return _NativeInstance.FillPattern;
      }
      set
      {
        _NativeInstance.FillPattern = value;
      }
    }

    public FillRule FillRule
    {
      get
      {
        return _NativeInstance.FillRule;
      }
      set
      {
        _NativeInstance.FillRule = value;
      }
    }

    public string Font
    {
      get
      {
        return _NativeInstance.Font;
      }
      set
      {
        _NativeInstance.Font = value;
      }
    }

    public string FontFamily
    {
      get
      {
        return _NativeInstance.FontFamily;
      }
      set
      {
        _NativeInstance.FontFamily = value;
      }
    }

    public double FontPointsize
    {
      get
      {
        return _NativeInstance.FontPointsize;
      }
      set
      {
        _NativeInstance.FontPointsize = value;
      }
    }

    public FontStyleType FontStyle
    {
      get
      {
        return _NativeInstance.FontStyle;
      }
      set
      {
        _NativeInstance.FontStyle = value;
      }
    }

    public FontWeight FontWeight
    {
      get
      {
        return _NativeInstance.FontWeight;
      }
      set
      {
        _NativeInstance.FontWeight = value;
      }
    }

    public bool StrokeAntiAlias
    {
      get
      {
        return _NativeInstance.StrokeAntiAlias;
      }
      set
      {
        _NativeInstance.StrokeAntiAlias = value;
      }
    }

    public MagickColor StrokeColor
    {
      get
      {
        return _NativeInstance.StrokeColor;
      }
      set
      {
        _NativeInstance.StrokeColor = value;
      }
    }

    public double[] StrokeDashArray
    {
      get
      {
        UIntPtr length;
        IntPtr data = _NativeInstance.GetStrokeDashArray(out length);

        try
        {
          return DoubleConverter.ToArray(data, (int)length);
        }
        finally
        {
          MagickMemory.Relinquish(data);
        }
      }
      set
      {
        if (value != null && value.Length > 0)
          _NativeInstance.SetStrokeDashArray(value, value.Length);
      }
    }

    public double StrokeDashOffset
    {
      get
      {
        return _NativeInstance.StrokeDashOffset;
      }
      set
      {
        _NativeInstance.StrokeDashOffset = value;
      }
    }

    public LineCap StrokeLineCap
    {
      get
      {
        return _NativeInstance.StrokeLineCap;
      }
      set
      {
        _NativeInstance.StrokeLineCap = value;
      }
    }

    public LineJoin StrokeLineJoin
    {
      get
      {
        return _NativeInstance.StrokeLineJoin;
      }
      set
      {
        _NativeInstance.StrokeLineJoin = value;
      }
    }

    public int StrokeMiterLimit
    {
      get
      {
        return _NativeInstance.StrokeMiterLimit;
      }
      set
      {
        _NativeInstance.StrokeMiterLimit = value;
      }
    }

    public MagickImage StrokePattern
    {
      get
      {
        return _NativeInstance.StrokePattern;
      }
      set
      {
        _NativeInstance.StrokePattern = value;
      }
    }

    public double StrokeWidth
    {
      get
      {
        return _NativeInstance.StrokeWidth;
      }
      set
      {
        _NativeInstance.StrokeWidth = value;
      }
    }

    public bool TextAntiAlias
    {
      get
      {
        return _NativeInstance.TextAntiAlias;
      }
      set
      {
        _NativeInstance.TextAntiAlias = value;
      }
    }

    public TextDirection TextDirection
    {
      get
      {
        return _NativeInstance.TextDirection;
      }
      set
      {
        _NativeInstance.TextDirection = value;
      }
    }

    public Encoding TextEncoding
    {
      get
      {
        string name = _NativeInstance.TextEncoding;
        if (string.IsNullOrEmpty(name))
          return null;

        try
        {
          return Encoding.GetEncoding(name);
        }
        catch (ArgumentException)
        {
          return null;
        }
      }
      set
      {
        if (value != null)
          _NativeInstance.TextEncoding = value.BodyName;
      }
    }

    public Gravity TextGravity
    {
      get
      {
        return _NativeInstance.TextGravity;
      }
      set
      {
        _NativeInstance.TextGravity = value;
      }
    }

    public double TextInterlineSpacing
    {
      get
      {
        return _NativeInstance.TextInterlineSpacing;
      }
      set
      {
        _NativeInstance.TextInterlineSpacing = value;
      }
    }

    public double TextInterwordSpacing
    {
      get
      {
        return _NativeInstance.TextInterwordSpacing;
      }
      set
      {
        _NativeInstance.TextInterwordSpacing = value;
      }
    }

    public double TextKerning
    {
      get
      {
        return _NativeInstance.TextKerning;
      }
      set
      {
        _NativeInstance.TextKerning = value;
      }
    }

    public MagickColor TextUnderColor
    {
      get
      {
        return _NativeInstance.TextUnderColor;
      }
      set
      {
        _NativeInstance.TextUnderColor = value;
      }
    }

    public void Dispose()
    {
      DisposeInstance();
      GC.SuppressFinalize(this);
    }

    internal void ResetTransform()
    {
      _NativeInstance.ResetTransform();
    }

    public void SetText(string value)
    {
      _NativeInstance.SetText(value);
    }

    public void SetTransformOrigin(double x, double y)
    {
      _NativeInstance.SetTransformOrigin(x, y);
    }

    public void SetTransformRotation(double angle)
    {
      _NativeInstance.SetTransformRotation(angle);
    }

    internal void SetTransformScale(double x, double y)
    {
      _NativeInstance.SetTransformScale(x, y);
    }

    internal void SetTransformSkewX(double value)
    {
      _NativeInstance.SetTransformSkewX(value);
    }

    internal void SetTransformSkewY(double value)
    {
      _NativeInstance.SetTransformSkewY(value);
    }
  }
}
