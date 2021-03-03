// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Reflection;

namespace FileGenerator.Drawables
{
    internal sealed class IPathsGenerator : DrawableCodeGenerator
    {
        private IPathsGenerator()
            : base(true)
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

            foreach (string commentLine in Types.GetCommentLines(constructor, "IPaths{TQuantumType}"))
                WriteLine(commentLine);
            Write("IPaths<TQuantumType> " + name + "(");
            WriteParameterDeclaration(parameters);
            WriteLine(");");
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
            WriteLine("public partial interface IPaths<TQuantumType>");
            WriteStartColon();

            foreach (var path in Types.GetPaths())
            {
                WritePath(path);
            }

            WriteEndColon();
        }

        public static void Generate()
        {
            var generator = new IPathsGenerator();
            generator.CreateWriter(PathHelper.GetFullPath(@"src\Magick.NET.Core\Drawables\Paths\Generated\IPaths{TQuantumType}.cs"));
            generator.WriteStart("ImageMagick");
            generator.WritePaths();
            generator.WriteEnd();
            generator.CloseWriter();
        }
    }
}
