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
  internal abstract class InterfaceCodeGenerator : SwitchCodeGenerator
  {
    private InterfaceGenerator[] _InterfaceGenerators;

    private InterfaceGenerator[] InterfaceGenerators
    {
      get
      {
        if (_InterfaceGenerators == null)
          _InterfaceGenerators = (from type in Types.GetInterfaceTypes(ClassName)
                                  select new InterfaceGenerator(this, ClassName, type.Name)).ToArray();

        return _InterfaceGenerators;
      }
    }

    private void WriteCode(string className)
    {
      Write("private ");
      Write(className);
      Write(" Create");
      Write(className);
      WriteLine("(XmlElement parent)");
      WriteStartColon();
      WriteCheckNull("parent");
      WriteLine("XmlElement element = (XmlElement)parent.FirstChild;");
      WriteCheckNull("element");
      WriteSwitch(from type in Types.GetInterfaceTypes(className)
                  select MagickScriptTypes.GetXsdName(type));
      WriteEndColon();
    }

    protected abstract string ClassName
    {
      get;
    }

    protected override void WriteCase(string name)
    {
      Write("return Create");
      Write(name[0].ToString().ToUpperInvariant());
      Write(name.Substring(1));
      WriteLine("(element);");
    }

    protected override void WriteCall(MethodBase method, ParameterInfo[] parameters)
    {
      throw new NotImplementedException();
    }

    protected override void WriteCode()
    {
      WriteCode(ClassName);

      foreach (InterfaceGenerator generator in InterfaceGenerators)
      {
        generator.WriteCode(Types);
      }
    }

    protected override void WriteHashtableCall(MethodBase method, ParameterInfo[] parameters)
    {
      throw new NotImplementedException();
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
