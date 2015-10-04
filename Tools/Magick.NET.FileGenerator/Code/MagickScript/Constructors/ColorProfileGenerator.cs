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
using System.CodeDom.Compiler;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
  internal sealed class ColorProfileGenerator : ConstructorCodeGenerator
  {
    protected override string ClassName
    {
      get
      {
        return "ColorProfile";
      }
    }

    protected override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
    {
      throw new NotImplementedException();
    }

    protected override void WriteCode(IndentedTextWriter writer)
    {
      writer.Write("private static ");
      writer.Write(TypeName);
      writer.Write(" Create");
      writer.Write(ClassName);
      writer.WriteLine("(XmlElement element)");
      WriteStartColon(writer);
      foreach (string name in Types.GetColorProfileNames())
      {
        writer.Write("if (element.GetAttribute(\"name\") == \"");
        writer.Write(name);
        writer.WriteLine("\")");
        writer.Indent++;
        writer.Write("return ColorProfile.");
        writer.Write(name);
        writer.WriteLine(";");
        writer.Indent--;
      }
      writer.WriteLine("throw new NotImplementedException(element.Name);");
      WriteEndColon(writer);
    }

    protected override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
    {
      throw new NotImplementedException();
    }

    public ColorProfileGenerator()
    {
    }
  }
}
