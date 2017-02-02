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
using System.Linq;
using System.Reflection;

namespace FileGenerator.MagickScript
{
  internal abstract class ScriptCodeGenerator : CodeGenerator
  {
    private static void CheckDuplicateParameterNames(MethodBase[] methods)
    {
      int count = (from method in methods
                   let name = string.Join(",", from parameter in method.GetParameters()
                                               orderby parameter.Name
                                               select parameter.Name)
                   select name).Distinct().Count();

      if (count != methods.Length)
        throw new InvalidOperationException("Duplicate names detected for: " + methods[0].Name);
    }

    private void WriteAttributeForEach(ParameterInfo[] allParameters)
    {
      ParameterInfo[] parameters = allParameters.Where(p => MagickScriptTypes.GetXsdAttributeType(p) != null).ToArray();
      if (parameters.Length == 0)
        return;

      parameters = parameters.OrderBy(p => p.Name).ToArray();

      WriteLine("foreach (XmlAttribute attribute in element.Attributes)");
      WriteStartColon();

      if (parameters.DistinctBy(p => GetName(p)).Count() == 1)
      {
        Write("arguments[attribute.Name] = Variables.GetValue<");
        Write(GetName(parameters[0]));
        WriteLine(">(attribute);");
      }
      else
      {
        for (int i = 0; i < parameters.Length; i++)
        {
          string xsdName = MagickScriptTypes.GetXsdName(parameters[i]);

          if (i > 0)
            Write("else ");

          Write("if (attribute.Name == \"");
          Write(xsdName);
          WriteLine("\")");
          Indent++;
          Write("arguments[\"");
          Write(xsdName);
          Write("\"] = ");
          WriteGetAttributeValue(GetName(parameters[i]));
          Indent--;
        }
      }

      WriteEndColon();
    }

    private void WriteCallIfElse(MethodBase[] methods)
    {
      for (int i = 0; i < methods.Length; i++)
      {
        ParameterInfo[] parameters = methods[i].GetParameters();

        if (i > 0)
          Write("else ");

        Write("if (");
        if (parameters.Length > 0)
        {
          Write("OnlyContains(arguments");
          foreach (ParameterInfo parameter in parameters)
          {
            Write(", \"");
            Write(parameter.Name);
            Write("\"");
          }
          Write(")");
        }
        else
        {
          Write("arguments.Count == 0");
        }

        WriteLine(")");
        Indent++;
        WriteHashtableCall(methods[i], parameters);
        Indent--;
      }
    }

    private void WriteElementForEach(ParameterInfo[] allParameters)
    {
      ParameterInfo[] parameters = allParameters.Where(p => MagickScriptTypes.GetXsdAttributeType(p) == null).ToArray();
      if (parameters.Length == 0)
        return;

      WriteLine("foreach (XmlElement elem in element.SelectNodes(\"*\"))");
      WriteStartColon();

      if (parameters.DistinctBy(p => GetName(p)).Count() == 1)
      {
        Write("arguments[elem.Name] = ");
        WriteCreateMethod(GetName(parameters[0]));
        WriteLine("(elem);");
      }
      else
      {
        for (int i = 0; i < parameters.Length; i++)
        {
          string xsdName = MagickScriptTypes.GetXsdName(parameters[i]);

          if (i > 0)
            Write("else ");

          Write("if (elem.Name == \"");
          Write(xsdName);
          WriteLine("\")");
          Indent++;
          Write("arguments[\"");
          Write(xsdName);
          Write("\"] = ");
          WriteCreateMethod(GetName(parameters[i]));
          WriteLine("(elem);");
          Indent--;
        }
      }

      WriteEndColon();
    }

    protected void WriteGetElementValue(string typeName, string attributeName)
    {
      Write("Variables.GetValue<");
      Write(typeName);
      Write(">(element, \"");
      Write(attributeName);
      WriteLine("\");");
    }

    private void WriteGetValue(ParameterInfo parameter)
    {
      string typeName = GetName(parameter);
      string xsdTypeName = MagickScriptTypes.GetXsdAttributeType(parameter);

      if (xsdTypeName != null)
      {
        WriteGetElementValue(typeName, parameter.Name);
      }
      else
      {
        WriteCreateMethod(typeName);
        Write("(");
        WriteSelectElement(typeName, parameter.Name);
        WriteLine(");");
      }
    }

    private void WriteInvalidCombinations(MethodBase[] methods)
    {
      WriteLine("else");
      Indent++;
      Write("throw new ArgumentException(\"Invalid argument combination for '" + MagickScriptTypes.GetXsdName(methods[0]) + "', allowed combinations are:");
      foreach (MethodBase method in methods)
      {
        Write(" [");
        ParameterInfo[] parameters = method.GetParameters();
        for (int i = 0; i < parameters.Length; i++)
        {
          Write(parameters[i].Name);
          if (i != parameters.Length - 1)
            Write(", ");
        }
        Write("]");
      }
      WriteLine("\");");
      Indent--;
    }

    private void WriteMethod(MethodBase method, ParameterInfo[] parameters)
    {
      foreach (ParameterInfo parameter in parameters)
      {
        string typeName = GetName(parameter);

        Write(typeName);
        Write(" ");
        Write(parameter.Name);
        Write("_ = ");
        WriteGetValue(parameter);
      }

      WriteCall(method, parameters);
    }

    protected ScriptCodeGenerator()
    {
    }

    protected ScriptCodeGenerator(CodeGenerator parent)
      : base(parent)
    {
    }

    protected MagickScriptTypes Types
    {
      get;
      private set;
    }

    protected static string GetName(MemberInfo member)
    {
      return MagickScriptTypes.GetName(member);
    }

    protected string GetName(ParameterInfo parameterInfo)
    {
      return Types.GetName(parameterInfo.ParameterType);
    }

    protected string GetName(PropertyInfo propertyInfo)
    {
      return Types.GetName(propertyInfo.PropertyType);
    }

    protected abstract void WriteCall(MethodBase method, ParameterInfo[] parameters);

    protected abstract void WriteCode();

    protected void WriteCheckNull(string name)
    {
      Write("if (");
      Write(name);
      WriteLine(" == null)");
      Indent++;
      WriteLine("return null;");
      Indent--;
    }

    protected void WriteCreateMethod(string typeName)
    {
      switch (typeName)
      {
        case "ColorProfile":
          Write("CreateColorProfile");
          break;
        case "Double[]":
        case "IEnumerable<Double>":
          Write("Variables.GetDoubleArray");
          break;
        case "IDefines":
          Write("CreateIDefines");
          break;
        case "IEnumerable<MagickGeometry>":
          Write("CreateMagickGeometryCollection");
          break;
        case "IEnumerable<IPath>":
          Write("CreatePaths");
          break;
        case "IEnumerable<PathArc>":
          Write("CreatePathArcs");
          break;
        case "IEnumerable<PointD>":
          Write("CreatePointDs");
          break;
        case "IEnumerable<Single>":
          Write("Variables.GetSingleArray");
          break;
        case "IEnumerable<SparseColorArg>":
          Write("CreateSparseColorArgs");
          break;
        case "IEnumerable<String>":
          Write("Variables.GetStringArray");
          break;
        case "ImageProfile":
          Write("CreateProfile");
          break;
        case "IReadDefines":
          Write("CreateIReadDefines");
          break;
        case "MagickImage":
          Write("CreateMagickImage");
          break;
        case "MagickGeometry":
          Write("CreateMagickGeometry");
          break;
        case "MagickSettings":
          Write("CreateMagickSettings");
          break;
        case "MontageSettings":
          Write("CreateMontageSettings");
          break;
        case "MorphologySettings":
          Write("CreateMorphologySettings");
          break;
        case "PathArc":
          Write("CreateArc");
          break;
        case "PixelStorageSettings":
          Write("CreatePixelStorageSettings");
          break;
        case "PrimaryInfo":
          Write("CreatePrimaryInfo");
          break;
        case "QuantizeSettings":
          Write("CreateQuantizeSettings");
          break;
        default:
          throw new NotImplementedException("WriteCreateMethod: " + typeName);
      }
    }

    protected void WriteGetAttributeValue(string typeName)
    {
      Write("Variables.GetValue<");
      Write(typeName);
      WriteLine(">(attribute);");
    }

    protected abstract void WriteHashtableCall(MethodBase method, ParameterInfo[] parameters);

    protected void WriteHashtableParameters(ParameterInfo[] parameters)
    {
      for (int k = 0; k < parameters.Length; k++)
      {
        Write("(");
        Write(GetName(parameters[k]));
        Write(")arguments[\"");
        Write(parameters[k].Name);
        Write("\"]");

        if (k != parameters.Length - 1)
          Write(", ");
      }
    }

    protected void WriteMethod(MethodBase[] methods)
    {
      ParameterInfo[] parameters = (from method in methods
                                    from param in method.GetParameters()
                                    select param).DistinctBy(p => p.Name).ToArray();

      if (methods.Length == 1)
      {
        WriteMethod(methods[0], parameters);
      }
      else
      {
        MethodBase[] sortedMethods = (from method in methods
                                      orderby string.Join(" ", from parameter in method.GetParameters()
                                                               select parameter.Name)
                                      select method).ToArray();
        WriteMethod(sortedMethods, parameters);
      }
    }

    private void WriteMethod(MethodBase[] methods, ParameterInfo[] parameters)
    {
      CheckDuplicateParameterNames(methods);

      WriteLine("Hashtable arguments = new Hashtable();");

      WriteAttributeForEach(parameters);
      WriteElementForEach(parameters);
      WriteCallIfElse(methods);
      WriteInvalidCombinations(methods);
    }

    protected void WriteParameters(ParameterInfo[] parameters)
    {
      for (int i = 0; i < parameters.Length; i++)
      {
        Write(parameters[i].Name);
        Write("_");

        if (i != parameters.Length - 1)
          Write(", ");
      }
    }

    protected void WriteSelectElement(string typeName, string elementName)
    {
      switch (typeName)
      {
        case "Double[]":
        case "IEnumerable<Double>":
        case "IEnumerable<Single>":
        case "IEnumerable<String>":
        case "MagickImage":
          Write("element");
          if (!string.IsNullOrEmpty(elementName))
          {
            Write("[\"");
            Write(elementName);
            Write("\"]");
          }
          break;
        case "IEnumerable<Drawable>":
        case "IEnumerable<MagickGeometry>":
        case "IEnumerable<IPath>":
        case "IEnumerable<PathArc>":
        case "IEnumerable<PointD>":
        case "ImageProfile":
        case "IReadDefines":
        case "PrimaryInfo":
          Write("element");
          break;
        case "ColorProfile":
        case "IDefines":
        case "MagickSettings":
        case "MontageSettings":
        case "PixelStorageSettings":
        case "QuantizeSettings":
          Write("element[\"");
          Write(elementName);
          Write("\"]");
          break;
        default:
          throw new NotImplementedException("WriteSelectElement: " + typeName);
      }
    }

    protected override void WriteUsing()
    {
      WriteLine("using System;");
      WriteLine("using System.Collections;");
      WriteLine("using System.Collections.Generic;");
      WriteLine("using System.Collections.ObjectModel;");
      WriteLine("using System.Diagnostics.CodeAnalysis;");
      WriteLine("using System.Text;");
      WriteLine("using System.Xml;");
      WriteQuantumType();
    }

    public abstract string Name
    {
      get;
    }

    public void Write(MagickScriptTypes types)
    {
      Types = types;

      WriteStart("ImageMagick");
      WriteLine("public sealed partial class MagickScript");
      WriteStartColon();
      WriteCode();
      WriteEndColon();
      WriteEnd();
    }

    public void WriteCode(MagickScriptTypes types)
    {
      Types = types;
      WriteCode();
    }
  }
}
