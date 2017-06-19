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

using System.Linq;
using System.Reflection;

namespace FileGenerator.Drawables
{
    internal sealed class DrawablesGenerator : DrawableCodeGenerator
    {
        private bool _ForCore;

        private DrawablesGenerator()
        {
        }

        private static void Generate(DrawablesGenerator generator)
        {
            generator.WriteStart("ImageMagick");
            generator.WriteDrawables();
            generator.WriteEnd();

            if (!generator._ForCore)
            {
                generator.WriteLine();
                generator.WriteLine("#endif");
            }

            generator.CloseWriter();
        }

        private bool IsValid(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();
            if (parameters.Length == 0)
                return _ForCore && constructor.DeclaringType.GetConstructors().Count() == 1;

            if (_ForCore)
                return parameters.All(parameter => IsSupportedByCore(parameter.ParameterType.Name));

            return parameters.All(parameter => !IsSupportedByCore(parameter.ParameterType.Name));
        }

        private bool IsSupportedByCore(string typeName)
        {
            string[] invalidTypes = new string[] { "Color", "Matrix", "Rectangle" };
            return !invalidTypes.Contains(typeName);
        }

        private void WriteDrawable(ConstructorInfo constructor)
        {
            if (!IsValid(constructor))
                return;

            var name = constructor.DeclaringType.Name.Substring(8);
            var parameters = constructor.GetParameters();

            foreach (string commentLine in Types.GetCommentLines(constructor, "Drawables"))
                WriteLine(commentLine);
            Write("public Drawables " + name + "(");
            WriteParameterDeclaration(parameters);
            WriteLine(")");
            WriteStartColon();
            Write("_Drawables.Add(new " + constructor.DeclaringType.Name + "(");
            WriteParameters(parameters);
            WriteLine("));");
            WriteLine("return this;");
            WriteEndColon();
            WriteLine();
        }

        private void WriteDrawable(ConstructorInfo[] constructors)
        {
            foreach (var constructor in constructors)
            {
                WriteDrawable(constructor);
            }
        }

        private void WriteDrawables()
        {
            if (_ForCore)
                WriteLine(@"[System.CodeDom.Compiler.GeneratedCode(""Magick.NET.FileGenerator"", """")]");

            WriteLine("public sealed partial class Drawables");
            WriteStartColon();

            foreach (var drawable in Types.GetDrawables())
            {
                WriteDrawable(drawable);
            }

            WriteEndColon();
        }

        protected override void WriteUsing()
        {
            if (!_ForCore)
            {
                WriteLine("#if !NETSTANDARD1_3");
                WriteLine();
                WriteLine("using System.Drawing;");
                WriteLine("using System.Drawing.Drawing2D;");
                WriteLine();
            }
            else
            {
                WriteLine("using System.Collections.Generic;");
                WriteLine("using System.Text;");
                WriteLine();
            }
        }

        public static void Generate()
        {
            DrawablesGenerator generator = new DrawablesGenerator();
            generator._ForCore = false;
            generator.CreateWriter(PathHelper.GetFullPath(@"Source\Magick.NET\Framework\Drawables\Generated\Drawables.cs"));
            Generate(generator);
        }

        public static void GenerateCore()
        {
            DrawablesGenerator generator = new DrawablesGenerator();
            generator._ForCore = true;
            generator.CreateWriter(PathHelper.GetFullPath(@"Source\Magick.NET\Shared\Drawables\Generated\Drawables.cs"));
            Generate(generator);
        }
    }
}
