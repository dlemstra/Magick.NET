//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Linq;
using System.Reflection;

namespace FileGenerator.Drawables
{
  internal abstract class DrawableCodeGenerator : CodeGenerator
  {
    protected DrawableCodeGenerator()
    {
      Types = new DrawableTypes(QuantumDepth.Q16);
    }

    protected DrawableTypes Types
    {
      get;
      private set;
    }

    protected string GetTypeName(Type type)
    {
      string name = "";
      if (type.IsArray)
        name += "params ";

      if (type.IsGenericType)
        return name + type.Name.Replace("`1", "") + "<" + type.GetGenericArguments().First().Name + ">";

      switch (type.Name)
      {
        case "Boolean":
          return name + "bool";
        case "Int32":
          return name + "int";
        case "Double":
        case "Double[]":
        case "String":
          return name + type.Name.ToLowerInvariant();
        default:
          return name + type.Name;
      }
    }

    protected void WriteParameterDeclaration(ParameterInfo[] parameters)
    {
      for (int i = 0; i < parameters.Length; i++)
      {
        Write(GetTypeName(parameters[i].ParameterType));
        Write(" ");
        Write(parameters[i].Name);

        if (i != parameters.Length - 1)
          Write(", ");
      }
    }

    protected void WriteParameters(ParameterInfo[] parameters)
    {
      for (int i = 0; i < parameters.Length; i++)
      {
        Write(parameters[i].Name);

        if (i != parameters.Length - 1)
          Write(", ");
      }
    }
  }
}
