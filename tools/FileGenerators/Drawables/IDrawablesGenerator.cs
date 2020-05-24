// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Linq;
using System.Reflection;

namespace FileGenerator.Drawables
{
    internal sealed class IDrawablesGenerator : DrawableCodeGenerator
    {
        private IDrawablesGenerator()
            : base(true)
        {
        }

        private static void Generate(IDrawablesGenerator generator)
        {
            generator.WriteStart("ImageMagick");
            generator.WriteDrawables();
            generator.WriteEnd();

            generator.CloseWriter();
        }

        private bool IsValid(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();
            if (parameters.Length == 0)
                return constructor.DeclaringType.GetConstructors().Count() == 1;

            return true;
        }

        private void WriteDrawable(ConstructorInfo constructor)
        {
            if (!IsValid(constructor))
                return;

            var name = constructor.DeclaringType.Name.Substring(8);
            var parameters = constructor.GetParameters();

            foreach (string commentLine in Types.GetCommentLines(constructor, "IDrawables{TQuantumType}"))
                WriteLine(commentLine);
            Write("public IDrawables<TQuantumType> " + name + "(");
            WriteParameterDeclaration(parameters);
            WriteLine(");");
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
            WriteLine(@"[System.CodeDom.Compiler.GeneratedCode(""Magick.NET.FileGenerator"", """")]");

            WriteLine("public partial interface IDrawables<TQuantumType>");
            WriteStartColon();

            foreach (var drawable in Types.GetDrawables())
            {
                WriteDrawable(drawable);
            }

            WriteEndColon();
        }

        protected override void WriteUsing()
        {
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.Text;");
            WriteLine();
        }

        public static void Generate()
        {
            var generator = new IDrawablesGenerator();
            generator.CreateWriter(PathHelper.GetFullPath(@"src\Magick.NET\Shared\Drawables\Generated\IDrawables{TQuantumType}.cs"));
            Generate(generator);
        }
    }
}
