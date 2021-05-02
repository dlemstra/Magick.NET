// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace FileGenerator.Native
{
    internal sealed class NativeClassGenerator : NativeCodeGenerator
    {
        private string _platform = string.Empty;

        public NativeClassGenerator(MagickClass magickClass)
          : base(magickClass)
        {
        }

        private string DllImport
           => "[DllImport(NativeLibrary." + _platform + "Name, CallingConvention = CallingConvention.Cdecl)]";

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

        private void WriteDllImports()
        {
            WriteDllImportCreateAndDispose();
            WriteDllImportProperties();
            WriteDllImportMethods();
        }

        private void WriteDllImportCreateAndDispose()
        {
            if (Class.IsStatic || Class.IsConst || !Class.HasInstance)
                return;

            if (!Class.HasNoConstructor)
            {
                WriteLine(DllImport);
                var arguments = GetNativeArgumentsDeclaration(Class.Constructor.Arguments);
                WriteLine("public static extern IntPtr " + Class.Name + "_Create(" + arguments + ");");
            }

            WriteLine(DllImport);
            WriteLine("public static extern void " + Class.Name + "_Dispose(IntPtr instance);");
        }

        private void WriteDllImportMethods()
        {
            foreach (var method in Class.Methods)
            {
                WriteLine(DllImport);
                var arguments = GetNativeArgumentsDeclaration(method);
                WriteMarshal(method.ReturnType);
                WriteLine("public static extern " + method.ReturnType.Native + " " + Class.Name + "_" + method.Name + "(" + arguments + ");");
            }
        }

        private void WriteDllImportProperties()
        {
            foreach (var property in Class.Properties)
            {
                WriteLine(DllImport);
                var arguments = Class.IsStatic ? null : "IntPtr instance";
                if (property.Throws)
                    arguments += ", out IntPtr exception";
                WriteMarshal(property.Type);
                WriteLine("public static extern " + property.Type.Native + " " + Class.Name + "_" + property.Name + "_Get(" + arguments + ");");

                if (property.IsReadOnly)
                    continue;

                WriteLine(DllImport);
                arguments = Class.IsStatic ? null : "IntPtr instance, ";
                if (property.Type.IsBool)
                    arguments += "[MarshalAs(UnmanagedType.Bool)] ";

                arguments += property.Type.Native + " value";

                if (property.Throws)
                    arguments += ", out IntPtr exception";

                WriteLine("public static extern void " + Class.Name + "_" + property.Name + "_Set(" + arguments + ");");
            }
        }

        private void WriteNativeMethods()
        {
            if (_platform == "X64")
                WriteLine("#if PLATFORM_x64 || PLATFORM_AnyCPU");
            else
                WriteLine("#if PLATFORM_x86 || PLATFORM_AnyCPU");
            WriteLine("public static class " + _platform);
            WriteStartColon();
            WriteNativeMethodsStaticConstructor();
            WriteDllImports();
            WriteEndColon();
            WriteLine("#endif");
        }

        private void WriteNativeMethodsStaticConstructor()
        {
            WriteLine("#if PLATFORM_AnyCPU");
            WriteLine("static " + _platform + "() { NativeLibraryLoader.Load(); }");
            WriteLine("#endif");
        }

        private void WriteMarshal(MagickType type)
        {
            if (type.IsBool)
                WriteLine("[return: MarshalAs(UnmanagedType.Bool)]");
        }

        private void WriteX64()
        {
            _platform = "X64";
            WriteNativeMethods();
        }

        private void WriteX86()
        {
            _platform = "X86";
            WriteNativeMethods();
        }
    }
}
