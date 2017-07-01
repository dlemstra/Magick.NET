// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Linq;
using System.Reflection;

namespace FileGenerator.MagickScript
{
    internal abstract class ConstructorCodeGenerator : ScriptCodeGenerator
    {
        private ConstructorInfo[] _Constructors;

        private ConstructorInfo[] Constructors
        {
            get
            {
                if (_Constructors == null)
                    _Constructors = Types.GetConstructors(ClassName).ToArray();

                return _Constructors;
            }
        }

        protected string TypeName
        {
            get
            {
                return GetName(Constructors[0]);
            }
        }

        protected abstract string ClassName
        {
            get;
        }

        protected virtual bool WriteEnumerable
        {
            get
            {
                return true;
            }
        }

        protected override void WriteCode()
        {
            Write("private ");
            Write(TypeName);
            Write(" Create");
            Write(ClassName);
            WriteLine("(XmlElement element)");
            WriteStartColon();
            WriteMethod(Constructors);
            WriteEndColon();

            if (!WriteEnumerable)
                return;

            Write("private ");
            Write("Collection<");
            Write(TypeName);
            Write("> Create");
            Write(ClassName);
            WriteLine("s(XmlElement element)");
            WriteStartColon();
            Write("Collection<");
            Write(TypeName);
            Write("> collection = new Collection<");
            Write(TypeName);
            WriteLine(">();");
            WriteLine("foreach (XmlElement elem in element.SelectNodes(\"*\"))");
            WriteStartColon();
            Write("collection.Add(Create");
            Write(TypeName);
            WriteLine("(elem));");
            WriteEndColon();
            WriteLine("return collection;");
            WriteEndColon();
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
