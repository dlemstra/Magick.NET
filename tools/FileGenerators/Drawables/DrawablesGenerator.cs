// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using System.Reflection;

namespace FileGenerator.Drawables
{
    internal sealed class DrawablesGenerator : DrawableCodeGenerator
    {
        private DrawablesGenerator()
            : base(false)
        {
        }

        public static void Generate()
        {
            var generator = new DrawablesGenerator();
            generator.CreateWriter(PathHelper.GetFullPath(@"src\Magick.NET\Drawables\Generated\Drawables.cs"));
            Generate(generator);
        }

        protected override void WriteUsing()
        {
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.Text;");
            WriteLine();
            WriteQuantumType();
        }

        private static void Generate(DrawablesGenerator generator)
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
                return constructor.DeclaringType!.GetConstructors().Count() == 1;

            return true;
        }

        private void WriteDrawable(ConstructorInfo constructor)
        {
            if (!IsValid(constructor))
                return;

            var name = constructor.DeclaringType!.Name.Substring(8);
            var parameters = constructor.GetParameters();

            foreach (var commentLine in Types.GetCommentLines(constructor, "Drawables"))
                WriteLine(commentLine);
            Write("public IDrawables<QuantumType> " + name + "(");
            WriteParameterDeclaration(parameters);
            WriteLine(")");
            WriteStartColon();
            Write("_drawables.Add(new " + constructor.DeclaringType.Name + "(");
            WriteParameters(parameters);
            WriteLine("));");
            WriteLine("return this;");
            WriteEndColon();
            WriteLine();
        }

        private void WriteDrawable(PropertyInfo property)
        {
            var name = property.Name.Replace("led", "le") + property.PropertyType.Name.Substring(8);

            foreach (var commentLine in Types.GetCommentLines(property, "Drawables"))
                WriteLine(commentLine);
            WriteLine("public IDrawables<QuantumType> " + name + "()");
            WriteStartColon();
            WriteLine("_drawables.Add(" + property.PropertyType.Name + "." + property.Name + ");");
            WriteLine("return this;");
            WriteEndColon();
            WriteLine();
        }

        private void WriteDrawables()
        {
            WriteLine(@"[System.CodeDom.Compiler.GeneratedCode(""Magick.NET.FileGenerator"", """")]");

            WriteLine("public sealed partial class Drawables");
            WriteStartColon();

            foreach (var drawable in Types.GetDrawableConstructors())
            {
                WriteDrawable(drawable);
            }

            foreach (var drawable in Types.GetStaticDrawableConstructors())
            {
                WriteDrawable(drawable);
            }

            WriteEndColon();
        }
    }
}
