// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Linq;
using System.Reflection;

namespace FileGenerator.MagickScript
{
    internal abstract class CreateObjectCodeGenerator : ScriptCodeGenerator
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

        private void WriteCallMethods()
        {
            foreach (MethodBase method in Methods)
            {
                string xsdMethodName = MagickScriptTypes.GetXsdName(method);
                ParameterInfo[] parameters = method.GetParameters();

                Write("XmlElement ");
                Write(xsdMethodName);
                Write(" = (XmlElement)element.SelectSingleNode(\"");
                Write(xsdMethodName);
                WriteLine("\");");

                Write("if (");
                Write(xsdMethodName);
                WriteLine(" != null)");
                WriteStartColon();

                foreach (ParameterInfo parameter in parameters)
                {
                    string typeName = GetName(parameter);

                    Write(typeName);
                    Write(" ");
                    Write(parameter.Name);
                    Write("_ = XmlHelper.GetAttribute<");
                    Write(typeName);
                    Write(">(");
                    Write(xsdMethodName);
                    Write(", \"");
                    Write(parameter.Name);
                    WriteLine("\");");
                }

                Write("result.");
                Write(method.Name);
                Write("(");

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i > 0)
                        Write(",");

                    Write(parameters[i].Name);
                    Write("_");
                }

                WriteLine(");");
                WriteEndColon();
            }
        }

        private void WriteGetValue(PropertyInfo property)
        {
            string typeName = GetName(property);
            string xsdTypeName = MagickScriptTypes.GetXsdAttributeType(property);

            if (xsdTypeName != null)
            {
                WriteGetElementValue(typeName, MagickScriptTypes.GetXsdName(property));
            }
            else
            {
                WriteCreateMethod(typeName);
                Write("(");
                WriteSelectElement(typeName, MagickScriptTypes.GetXsdName(property));
                WriteLine(");");
            }
        }

        private void WriteSetProperties()
        {
            foreach (PropertyInfo property in Properties)
            {
                Write("result.");
                Write(property.Name);
                Write(" = ");
                WriteGetValue(property);
            }
        }

        protected CreateObjectCodeGenerator()
          : base()
        {
        }

        protected CreateObjectCodeGenerator(CodeGenerator parent)
          : base(parent)
        {
        }

        protected virtual string ReturnType
        {
            get
            {
                return ClassName;
            }
        }

        protected override void WriteCode()
        {
            Write("private ");
            Write(ReturnType);
            Write(" Create");
            Write(ClassName);
            WriteLine("(XmlElement element)");
            WriteStartColon();
            WriteCheckNull("element");
            Write(ClassName);
            Write(" result = new ");
            Write(ClassName);
            WriteLine("();");
            WriteSetProperties();
            WriteCallMethods();
            WriteLine("return result;");
            WriteEndColon();
        }

        protected sealed override void WriteHashtableCall(MethodBase method, ParameterInfo[] parameters)
        {
            throw new NotImplementedException();
        }

        protected sealed override void WriteCall(MethodBase method, ParameterInfo[] parameters)
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
