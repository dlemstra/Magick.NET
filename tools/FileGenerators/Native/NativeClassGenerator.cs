// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace FileGenerator.Native
{
    internal sealed class NativeClassGenerator : NativeCodeGenerator
    {
        public NativeClassGenerator(MagickClass magickClass)
          : base(magickClass)
        {
        }

        public static void Create(IEnumerable<MagickClass> magickClasses)
        {
            RegisterClasses(magickClasses);
            foreach (var magickClass in magickClasses)
            {
                var codeGenerator = new NativeClassGenerator(magickClass);
                codeGenerator.Create();
            }
        }

        public void Create()
        {
            CreateWriter(Class.FileName);

            WriteStart(Class.Namespace);
            string baseClass = string.Empty;
            if (!Class.IsStatic && Class.HasInstance && !Class.IsConst && !Class.IsDynamic)
                baseClass = " : IDisposable";

            WriteLine(Class.Access + (Class.IsStatic ? " static" : string.Empty) + " partial class " + Class.ClassName + baseClass);
            WriteStartColon();

            WriteDelegates();

            WriteLine("[SuppressUnmanagedCodeSecurity]");
            WriteLine("private static class NativeMethods");
            WriteStartColon();
            WriteX64();
            WriteX86();
            WriteEndColon();

            var generator = new NativeInstanceGenerator(this);
            generator.Write();

            WriteEndColon();
            WriteEnd();

            CloseWriter();
        }

        private void WriteDelegates()
        {
            foreach (var func in Class.Delegates)
            {
                var arguments = GetNativeArgumentsDeclaration(func.Arguments);
                WriteLine("[UnmanagedFunctionPointer(CallingConvention.Cdecl)]");
                WriteLine("private delegate " + func.Type + " " + func.Name + "Delegate(" + arguments + ");");
            }
        }

        private void WriteDllImports(string platform)
        {
            WriteDllImportCreateAndDispose(platform);
            WriteDllImportProperties(platform);
            WriteDllImportMethods(platform);
        }

        private void WriteDllImportCreateAndDispose(string platform)
        {
            if (Class.IsStatic || Class.IsConst || !Class.HasInstance)
                return;

            if (!Class.HasNoConstructor)
            {
                WriteLine(GetDllImport(platform));
                var arguments = GetNativeArgumentsDeclaration(Class.Constructor.Arguments);
                WriteLine("public static extern IntPtr " + Class.Name + "_Create(" + arguments + ");");
            }

            WriteLine(GetDllImport(platform));
            WriteLine("public static extern void " + Class.Name + "_Dispose(IntPtr instance);");
        }

        private void WriteDllImportMethods(string platform)
        {
            foreach (var method in Class.Methods)
            {
                WriteLine(GetDllImport(platform));
                var arguments = GetNativeArgumentsDeclaration(method);
                WriteMarshal(method.ReturnType);
                WriteLine("public static extern " + method.ReturnType.Native + " " + Class.Name + "_" + method.Name + "(" + arguments + ");");
            }
        }

        private void WriteDllImportProperties(string platform)
        {
            foreach (var property in Class.Properties)
            {
                WriteLine(GetDllImport(platform));
                var arguments = Class.IsStatic ? null : "IntPtr instance";
                if (property.Throws)
                    arguments += ", out IntPtr exception";
                WriteMarshal(property.Type);
                WriteLine("public static extern " + property.Type.Native + " " + Class.Name + "_" + property.Name + "_Get(" + arguments + ");");

                if (property.IsReadOnly)
                    continue;

                WriteLine(GetDllImport(platform));
                arguments = Class.IsStatic ? null : "IntPtr instance, ";
                if (property.Type.IsBool)
                    arguments += "[MarshalAs(UnmanagedType.Bool)] ";

                arguments += property.Type.Native + " value";

                if (property.Throws)
                    arguments += ", out IntPtr exception";

                WriteLine("public static extern void " + Class.Name + "_" + property.Name + "_Set(" + arguments + ");");
            }
        }

        private void WriteNativeMethods(string platform)
        {
            if (platform == "X64")
                WriteLine("#if PLATFORM_x64 || PLATFORM_AnyCPU");
            else
                WriteLine("#if PLATFORM_x86 || PLATFORM_AnyCPU");
            WriteLine("public static class " + platform);
            WriteStartColon();
            WriteNativeMethodsStaticConstructor(platform);
            WriteDllImports(platform);
            WriteEndColon();
            WriteLine("#endif");
        }

        private void WriteNativeMethodsStaticConstructor(string platform)
        {
            WriteLine("#if PLATFORM_AnyCPU");
            WriteLine("static " + platform + "() { NativeLibraryLoader.Load(); }");
            WriteLine("#endif");
        }

        private void WriteMarshal(MagickType type)
        {
            if (type.IsBool)
                WriteLine("[return: MarshalAs(UnmanagedType.Bool)]");
        }

        private void WriteX64()
            => WriteNativeMethods("X64");

        private void WriteX86()
            => WriteNativeMethods("X86");

        private string GetDllImport(string platform)
           => "[DllImport(NativeLibrary." + platform + "Name, CallingConvention = CallingConvention.Cdecl)]";
    }
}
