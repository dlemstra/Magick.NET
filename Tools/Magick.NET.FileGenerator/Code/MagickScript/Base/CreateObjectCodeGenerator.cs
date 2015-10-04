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
using System.Linq;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
  internal abstract class CreateObjectCodeGenerator : CodeGenerator
  {
    private MethodBase[] Methods
    {
      get
      {
        return Types.GetMethods(ClassName).ToArray();
      }
    }

    private PropertyInfo[] Properties
    {
      get
      {
        return Types.GetProperties(ClassName).ToArray();
      }
    }

    private void WriteCallMethods(IndentedTextWriter writer)
    {
      foreach (MethodBase method in Methods)
      {
        string xsdMethodName = MagickTypes.GetXsdName(method);
        ParameterInfo[] parameters = method.GetParameters();

        writer.Write("XmlElement ");
        writer.Write(xsdMethodName);
        writer.Write(" = (XmlElement)element.SelectSingleNode(\"");
        writer.Write(xsdMethodName);
        writer.WriteLine("\");");

        writer.Write("if (");
        writer.Write(xsdMethodName);
        writer.WriteLine(" != null)");
        WriteStartColon(writer);

        foreach (ParameterInfo parameter in parameters)
        {
          string typeName = GetName(parameter);

          writer.Write(typeName);
          writer.Write(" ");
          writer.Write(parameter.Name);
          writer.Write("_ = XmlHelper.GetAttribute<");
          writer.Write(typeName);
          writer.Write(">(");
          writer.Write(xsdMethodName);
          writer.Write(", \"");
          writer.Write(parameter.Name);
          writer.WriteLine("\");");
        }

        writer.Write("result.");
        writer.Write(method.Name);
        writer.Write("(");

        for (int i = 0; i < parameters.Length; i++)
        {
          if (i > 0)
            writer.Write(",");

          writer.Write(parameters[i].Name);
          writer.Write("_");
        }

        writer.WriteLine(");");
        WriteEndColon(writer);
      }
    }

    private void WriteGetValue(IndentedTextWriter writer, PropertyInfo property)
    {
      string typeName = GetName(property);
      string xsdTypeName = MagickTypes.GetXsdAttributeType(property);

      if (xsdTypeName != null)
      {
        WriteGetElementValue(writer, typeName, MagickTypes.GetXsdName(property));
      }
      else
      {
        WriteCreateMethod(writer, typeName);
        writer.Write("(");
        WriteSelectElement(writer, typeName, MagickTypes.GetXsdName(property));
        writer.WriteLine(");");
      }
    }

    private void WriteSetProperties(IndentedTextWriter writer)
    {
      foreach (PropertyInfo property in Properties)
      {
        writer.Write("result.");
        writer.Write(property.Name);
        writer.Write(" = ");
        WriteGetValue(writer, property);
      }
    }

    protected virtual string ReturnType
    {
      get
      {
        return ClassName;
      }
    }

    protected override void WriteCode(IndentedTextWriter writer)
    {
      writer.Write("private ");
      writer.Write(ReturnType);
      writer.Write(" Create");
      writer.Write(ClassName);
      writer.WriteLine("(XmlElement element)");
      WriteStartColon(writer);
      CheckNull(writer, "element");
      writer.Write(ClassName);
      writer.Write(" result = new ");
      writer.Write(ClassName);
      writer.WriteLine("();");
      WriteSetProperties(writer);
      WriteCallMethods(writer);
      writer.WriteLine("return result;");
      WriteEndColon(writer);
    }

    protected sealed override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
    {
      throw new NotImplementedException();
    }

    protected sealed override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
    {
      throw new NotImplementedException();
    }

    public abstract string ClassName
    {
      get;
    }

    public override string Name
    {
      get
      {
        return ClassName;
      }
    }
  }
}
