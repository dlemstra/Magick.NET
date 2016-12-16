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

using System.IO;
using System.Runtime.Serialization;

namespace FileGenerator.Native
{
  internal sealed class MagickType
  {
    private string _Type;
    private bool _IsEnum;

    private bool _NeedsTypeCast
    {
      get
      {
        if (_Type == "Instance" || HasInstance || IsString)
          return false;

        return _Type != Native || _Type != Managed;
      }
    }

    public string GetNativeType(string type)
    {
      if (_Type == "void")
        return "void";

      if (_Type == "nativeString" || _Type == "string")
        return "IntPtr";

      if (_IsEnum || _Type == "size_t")
        return "UIntPtr";

      if (_Type == "ssize_t" || _Type == "Instance" || _Type == "voidInstance" || HasInstance)
        return "IntPtr";

      return type;
    }

    public string GetManagedType(string type)
    {
      if (type == "size_t" || type == "ssize_t")
        return "int";

      if (_Type == "voidInstance")
        return "void";

      if (_Type == "Instance")
        return "IntPtr";

      if (_Type == "nativeString")
        return "string";

      return type;
    }

    public MagickType(string type)
    {
      _Type = !string.IsNullOrEmpty(type) ? type : "void";
      _IsEnum = File.Exists(PathHelper.GetFullPath(@"\Source\Magick.NET\Core\Enums\" + type + ".cs"));
    }

    public bool HasInstance
    {
      get
      {
        if (_Type.EndsWith("Delegate"))
          return false;

        switch (_Type)
        {
          case "byte":
          case "byte[]":
          case "bool":
          case "double":
          case "double[]":
          case "Instance":
          case "long":
          case "QuantumType":
          case "QuantumType[]":
          case "size_t":
          case "ssize_t":
          case "string":
          case "ulong":
            return false;
          default:
            return !_IsEnum;
        }
      }
    }

    public bool IsBool
    {
      get
      {
        return Managed == "bool";
      }
    }

    public bool IsNativeString
    {
      get
      {
        return _Type == "nativeString";
      }
    }

    public bool IsString
    {
      get
      {
        return _Type == "string";
      }
    }

    public bool IsVoid
    {
      get
      {
        return _Type == "void" || _Type == "voidInstance";
      }
    }

    public string Managed
    {
      get
      {
        return GetManagedType(_Type);
      }
    }

    public string ManagedTypeCast
    {
      get
      {
        if (_NeedsTypeCast)
          return "(" + Managed + ")";

        return "";
      }
    }

    [DataMember(Name = "name")]
    public string Name
    {
      get;
      set;
    }

    public string Native
    {
      get
      {
        return GetNativeType(_Type);
      }
    }

    public string NativeTypeCast
    {
      get
      {
        if (_NeedsTypeCast)
          return "(" + Native + ")";

        return "";
      }
    }
  }
}