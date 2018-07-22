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
using System.Reflection;

namespace FileGenerator.MagickScript
{
    internal sealed class CollectionGenerator : ScriptCodeGenerator
    {
        private Type[] SupportedTypes
        {
            get
            {
                return new Type[] { Types.GetType("MagickColor"), Types.GetType("MagickGeometry") };
            }
        }

        protected override void WriteCall(MethodBase method, ParameterInfo[] parameters)
        {
            throw new NotImplementedException();
        }

        protected override void WriteCode()
        {
            foreach (Type type in SupportedTypes)
            {
                string typeName = GetName(type);

                Write("private ");
                Write("Collection<");
                Write(typeName);
                Write(">");
                Write(" Create");
                Write(type.Name);
                Write("Collection");
                Write("(XmlElement element)");
                WriteLine();
                WriteStartColon();
                Write("Collection<");
                Write(typeName);
                Write("> collection = new ");
                Write("Collection<");
                Write(typeName);
                WriteLine(">();");
                WriteLine("foreach (XmlElement elem in element.SelectNodes(\"*\"))");
                WriteStartColon();
                Write("collection.Add(GetValue<");
                Write(typeName);
                WriteLine(">(elem, \"value\"));");
                WriteEndColon();
                WriteLine("return collection;");
                WriteEndColon();
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
                return "Collection";
            }
        }
    }
}
