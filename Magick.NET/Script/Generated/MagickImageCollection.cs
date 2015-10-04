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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml;

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
  public sealed partial class MagickScript
  {
    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
    private MagickImage ExecuteCollection(XmlElement element, MagickImageCollection collection)
    {
      switch(element.Name[0])
      {
        case 'c':
        {
          switch(element.Name[2])
          {
            case 'a':
            {
              return ExecuteCoalesce(collection);
            }
            case 'm':
            {
              return ExecuteCombine(element, collection);
            }
          }
          break;
        }
        case 'd':
        {
          return ExecuteDeconstruct(collection);
        }
        case 'm':
        {
          switch(element.Name[1])
          {
            case 'a':
            {
              return ExecuteMap(element, collection);
            }
            case 'e':
            {
              return ExecuteMerge(collection);
            }
            case 'o':
            {
              switch(element.Name[2])
              {
                case 'n':
                {
                  return ExecuteMontage(element, collection);
                }
                case 's':
                {
                  return ExecuteMosaic(collection);
                }
              }
              break;
            }
          }
          break;
        }
        case 'o':
        {
          if (element.Name.Length == 8)
          {
            return ExecuteOptimize(collection);
          }
          switch(element.Name[8])
          {
            case 'P':
            {
              return ExecuteOptimizePlus(collection);
            }
            case 'T':
            {
              return ExecuteOptimizeTransparency(collection);
            }
          }
          break;
        }
        case 'q':
        {
          return ExecuteQuantize(element, collection);
        }
        case 'r':
        {
          switch(element.Name[2])
          {
            case 'P':
            {
              return ExecuteRePage(collection);
            }
            case 'v':
            {
              return ExecuteReverse(collection);
            }
          }
          break;
        }
        case 'a':
        {
          switch(element.Name[6])
          {
            case 'H':
            {
              return ExecuteAppendHorizontally(collection);
            }
            case 'V':
            {
              return ExecuteAppendVertically(collection);
            }
          }
          break;
        }
        case 'e':
        {
          return ExecuteEvaluate(element, collection);
        }
        case 'f':
        {
          switch(element.Name[1])
          {
            case 'l':
            {
              return ExecuteFlatten(collection);
            }
            case 'x':
            {
              return ExecuteFx(element, collection);
            }
          }
          break;
        }
        case 's':
        {
          switch(element.Name[5])
          {
            case 'H':
            {
              return ExecuteSmushHorizontal(element, collection);
            }
            case 'V':
            {
              return ExecuteSmushVertical(element, collection);
            }
          }
          break;
        }
        case 't':
        {
          return ExecuteTrimBounds(collection);
        }
      }
      throw new NotImplementedException(element.Name);
    }

    private static MagickImage ExecuteCoalesce(MagickImageCollection collection)
    {
      collection.Coalesce();
      return null;
    }

    private static MagickImage ExecuteDeconstruct(MagickImageCollection collection)
    {
      collection.Deconstruct();
      return null;
    }

    private MagickImage ExecuteMap(XmlElement element, MagickImageCollection collection)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateQuantizeSettings(elem);
      }
      if (arguments.Count == 0)
        {
          collection.Map();
          return null;
        }
      else if (OnlyContains(arguments, "settings"))
        {
          collection.Map((QuantizeSettings)arguments["settings"]);
          return null;
        }
      else
        throw new ArgumentException("Invalid argument combination for 'map', allowed combinations are: [] [settings]");
    }

    private static MagickImage ExecuteOptimize(MagickImageCollection collection)
    {
      collection.Optimize();
      return null;
    }

    private static MagickImage ExecuteOptimizePlus(MagickImageCollection collection)
    {
      collection.OptimizePlus();
      return null;
    }

    private static MagickImage ExecuteOptimizeTransparency(MagickImageCollection collection)
    {
      collection.OptimizeTransparency();
      return null;
    }

    private MagickImage ExecuteQuantize(XmlElement element, MagickImageCollection collection)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateQuantizeSettings(elem);
      }
      if (arguments.Count == 0)
        {
          collection.Quantize();
          return null;
        }
      else if (OnlyContains(arguments, "settings"))
        {
          collection.Quantize((QuantizeSettings)arguments["settings"]);
          return null;
        }
      else
        throw new ArgumentException("Invalid argument combination for 'quantize', allowed combinations are: [] [settings]");
    }

    private static MagickImage ExecuteRePage(MagickImageCollection collection)
    {
      collection.RePage();
      return null;
    }

    private static MagickImage ExecuteReverse(MagickImageCollection collection)
    {
      collection.Reverse();
      return null;
    }

    private static MagickImage ExecuteAppendHorizontally(MagickImageCollection collection)
    {
      return collection.AppendHorizontally();
    }

    private static MagickImage ExecuteAppendVertically(MagickImageCollection collection)
    {
      return collection.AppendVertically();
    }

    private MagickImage ExecuteCombine(XmlElement element, MagickImageCollection collection)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Channels>(attribute);
      }
      if (arguments.Count == 0)
        return collection.Combine();
      else if (OnlyContains(arguments, "channels"))
        return collection.Combine((Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'combine', allowed combinations are: [] [channels]");
    }

    private MagickImage ExecuteEvaluate(XmlElement element, MagickImageCollection collection)
    {
      EvaluateOperator evaluateOperator_ = Variables.GetValue<EvaluateOperator>(element, "evaluateOperator");
      return collection.Evaluate(evaluateOperator_);
    }

    private static MagickImage ExecuteFlatten(MagickImageCollection collection)
    {
      return collection.Flatten();
    }

    private MagickImage ExecuteFx(XmlElement element, MagickImageCollection collection)
    {
      String expression_ = Variables.GetValue<String>(element, "expression");
      return collection.Fx(expression_);
    }

    private static MagickImage ExecuteMerge(MagickImageCollection collection)
    {
      return collection.Merge();
    }

    private MagickImage ExecuteMontage(XmlElement element, MagickImageCollection collection)
    {
      MontageSettings settings_ = CreateMontageSettings(element["settings"]);
      return collection.Montage(settings_);
    }

    private static MagickImage ExecuteMosaic(MagickImageCollection collection)
    {
      return collection.Mosaic();
    }

    private MagickImage ExecuteSmushHorizontal(XmlElement element, MagickImageCollection collection)
    {
      Int32 offset_ = Variables.GetValue<Int32>(element, "offset");
      return collection.SmushHorizontal(offset_);
    }

    private MagickImage ExecuteSmushVertical(XmlElement element, MagickImageCollection collection)
    {
      Int32 offset_ = Variables.GetValue<Int32>(element, "offset");
      return collection.SmushVertical(offset_);
    }

    private static MagickImage ExecuteTrimBounds(MagickImageCollection collection)
    {
      return collection.TrimBounds();
    }
  }
}
