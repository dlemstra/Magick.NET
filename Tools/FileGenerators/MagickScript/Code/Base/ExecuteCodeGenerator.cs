// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace FileGenerator.MagickScript
{
    internal abstract class ExecuteCodeGenerator : SwitchCodeGenerator
    {
        private static bool HasParameters(MethodBase[] methods)
        {
            if (methods.Length != 1)
                return false;

            return HasParameters(methods[0]);
        }

        private static bool HasParameters(MethodBase method)
        {
            if (method == null)
                return false;

            return method.GetParameters().Length == 0;
        }

        private static bool HasParameters(MemberInfo memberInfo)
        {
            return HasParameters(memberInfo as MethodBase);
        }

        private bool IsStatic(MethodBase[] methods)
        {
            if (methods == null)
                return false;

            foreach (var method in methods)
            {
                var parameters = method.GetParameters();

                foreach (ParameterInfo parameter in parameters)
                {
                    string xsdTypeName = MagickScriptTypes.GetXsdAttributeType(parameter);
                    if (xsdTypeName != null)
                        return false;

                    string typeName = GetName(parameter);
                    if (!HasStaticCreateMethod(typeName))
                        return false;
                }
            }

            return true;
        }

        private void WriteExecute()
        {
            WriteLine("[SuppressMessage(\"Microsoft.Maintainability\", \"CA1502:AvoidExcessiveComplexity\")]");
            WriteLine("[SuppressMessage(\"Microsoft.Maintainability\", \"CA1505:AvoidUnmaintainableCode\")]");
            Write("private ");
            Write(ReturnType);
            Write(" Execute");
            Write(ExecuteName);
            Write("(XmlElement element, ");
            Write(ExecuteArgument);
            WriteLine(")");
            WriteStartColon();

            IEnumerable<string> names = (from property in Properties
                                         select MagickScriptTypes.GetXsdName(property)).Concat(
                                           from method in Methods
                                           select MagickScriptTypes.GetXsdName(method[0])).Concat(
                                           CustomMethods);

            WriteSwitch(names);
            WriteEndColon();
        }

        private void WriteExecute(MethodBase[] methods)
        {
            Write("private ");
            if (IsStatic(methods))
                Write("static ");
            Write(ReturnType);
            Write(" Execute");
            Write(GetName(methods[0]));
            Write("(");
            if (!HasParameters(methods))
                Write("XmlElement element, ");
            Write(ExecuteArgument);
            WriteLine(")");
            WriteStartColon();

            WriteMethod(methods);

            WriteEndColon();
        }

        private void WriteExecute(PropertyInfo property)
        {
            Write("private ");
            Write("void Execute");
            Write(property.Name);
            Write("(XmlElement element, ");
            Write(ExecuteArgument);
            WriteLine(")");
            WriteStartColon();

            WriteSet(property);

            WriteEndColon();
        }

        protected virtual string[] CustomMethods
        {
            get
            {
                return new string[] { };
            }
        }

        protected abstract string ExecuteArgument
        {
            get;
        }

        protected abstract string ExecuteName
        {
            get;
        }

        protected virtual IEnumerable<MethodBase[]> Methods
        {
            get
            {
                return Enumerable.Empty<MethodBase[]>();
            }
        }

        protected virtual IEnumerable<PropertyInfo> Properties
        {
            get
            {
                return Enumerable.Empty<PropertyInfo>();
            }
        }

        protected virtual string ReturnType
        {
            get
            {
                return "void";
            }
        }

        protected sealed override void WriteCase(string name)
        {
            MemberInfo member = (from property in Properties
                                 where MagickScriptTypes.GetXsdName(property).Equals(name, StringComparison.OrdinalIgnoreCase)
                                 select property).FirstOrDefault();

            if (member == null)
                member = (from overloads in Methods
                          let method = overloads[overloads.Length - 1]
                          where MagickScriptTypes.GetXsdName(method).Equals(name, StringComparison.OrdinalIgnoreCase)
                          select method).FirstOrDefault();


            if (ReturnType != "void")
                Write("return ");
            Write("Execute");
            if (member == null)
            {
                Write(char.ToUpper(name[0], CultureInfo.InvariantCulture));
                Write(name.Substring(1));
            }
            else
            {
                Write(GetName(member));
            }
            Write("(");
            if (member == null || !HasParameters(member))
                Write("element, ");
            Write(ExecuteArgument.Split(' ').Last());
            WriteLine(");");
            if (ReturnType == "void")
                WriteLine("return;");
        }

        protected override void WriteCode()
        {
            WriteExecute();

            foreach (PropertyInfo property in Properties)
            {
                WriteExecute(property);
            }

            foreach (MethodBase[] methods in Methods)
            {
                WriteExecute(methods);
            }
        }

        protected void WriteGetValue(PropertyInfo property)
        {
            string typeName = GetName(property);
            string xsdTypeName = MagickScriptTypes.GetXsdAttributeType(property);

            if (xsdTypeName != null)
            {
                WriteGetElementValue(typeName, "value");
            }
            else
            {
                WriteCreateMethod(typeName);
                Write("(");
                WriteSelectElement(typeName, null);
                WriteLine(");");
            }
        }

        protected abstract void WriteSet(PropertyInfo property);

        public override string Name
        {
            get
            {
                return ExecuteName;
            }
        }
    }
}
