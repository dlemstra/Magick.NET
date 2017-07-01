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

using System.Reflection;

namespace FileGenerator.Drawables
{
    internal sealed class PathsGenerator : DrawableCodeGenerator
    {
        private PathsGenerator()
        {
        }

        protected override void WriteUsing()
        {
            WriteLine("using System.Collections.Generic;");
            WriteLine();
        }

        private void WritePath(ConstructorInfo constructor)
        {
            var name = constructor.DeclaringType.Name.Substring(4);
            var parameters = constructor.GetParameters();

            foreach (string commentLine in Types.GetCommentLines(constructor, "Paths"))
                WriteLine(commentLine);
            Write("public Paths " + name + "(");
            WriteParameterDeclaration(parameters);
            WriteLine(")");
            WriteStartColon();
            Write("_Paths.Add(new " + constructor.DeclaringType.Name + "(");
            WriteParameters(parameters);
            WriteLine("));");
            WriteLine("return this;");
            WriteEndColon();
            WriteLine();
        }

        private void WritePath(ConstructorInfo[] constructors)
        {
            foreach (var constructor in constructors)
            {
                WritePath(constructor);
            }
        }

        private void WritePaths()
        {
            WriteLine(@"[System.CodeDom.Compiler.GeneratedCode(""Magick.NET.FileGenerator"", """")]");
            WriteLine("public sealed partial class Paths");
            WriteStartColon();

            foreach (var path in Types.GetPaths())
            {
                WritePath(path);
            }

            WriteEndColon();
        }

        public static void Generate()
        {
            PathsGenerator generator = new PathsGenerator();
            generator.CreateWriter(PathHelper.GetFullPath(@"Source\Magick.NET\Shared\Drawables\Paths\Generated\Paths.cs"));
            generator.WriteStart("ImageMagick");
            generator.WritePaths();
            generator.WriteEnd();
            generator.CloseWriter();
        }
    }
}
