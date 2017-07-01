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

using System.Collections.Generic;

namespace FileGenerator.Native
{
    internal sealed class NativeClassGenerator : NativeCodeGenerator
    {
        private string _Platform;

        private string _DllImport
        {
            get
            {
                return "[DllImport(NativeLibrary." + _Platform + "Name, CallingConvention = CallingConvention.Cdecl)]";
            }
        }

        private void WriteDelegates()
        {
            foreach (var func in Class.Delegates)
            {
                string arguments = GetNativeArgumentsDeclaration(func.Arguments);
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
                WriteLine(_DllImport);
                string arguments = GetNativeArgumentsDeclaration(Class.Constructor.Arguments);
                WriteLine("public static extern IntPtr " + Class.Name + "_Create(" + arguments + ");");
            }

            WriteLine(_DllImport);
            WriteLine("public static extern void " + Class.Name + "_Dispose(IntPtr instance);");
        }

        private void WriteDllImportMethods()
        {
            foreach (var method in Class.Methods)
            {
                WriteLine(_DllImport);
                string arguments = GetNativeArgumentsDeclaration(method);
                WriteMarshal(method.ReturnType);
                WriteLine("public static extern " + method.ReturnType.Native + " " + Class.Name + "_" + method.Name + "(" + arguments + ");");
            }
        }

        private void WriteDllImportProperties()
        {
            foreach (var property in Class.Properties)
            {
                WriteLine(_DllImport);
                string arguments = Class.IsStatic ? null : "IntPtr instance";
                if (property.Throws)
                    arguments += ", out IntPtr exception";
                WriteMarshal(property.Type);
                WriteLine("public static extern " + property.Type.Native + " " + Class.Name + "_" + property.Name + "_Get(" + arguments + ");");

                if (property.IsReadOnly)
                    continue;

                WriteLine(_DllImport);
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
            if (_Platform == "X64")
                WriteLine("#if PLATFORM_x64 || PLATFORM_AnyCPU");
            else
                WriteLine("#if PLATFORM_x86 || PLATFORM_AnyCPU");
            WriteLine("public static class " + _Platform);
            WriteStartColon();
            WriteNativeMethodsStaticConstructor();
            WriteDllImports();
            WriteEndColon();
            WriteLine("#endif");
        }

        private void WriteNativeMethodsStaticConstructor()
        {
            WriteLine("#if PLATFORM_AnyCPU");
            WriteLine(@"[SuppressMessage(""Microsoft.Performance"", ""CA1810: InitializeReferenceTypeStaticFieldsInline"", Scope = ""member"", Target = ""ImageMagick." + Class.Name + @"+NativeMethods." + _Platform + @"#.cctor()"")]");
            WriteLine("static " + _Platform + "() { NativeLibraryLoader.Load(); }");
            WriteLine("#endif");
        }

        private void WriteMarshal(MagickType type)
        {
            if (type.IsBool)
                WriteLine("[return: MarshalAs(UnmanagedType.Bool)]");
        }

        private void WriteX64()
        {
            _Platform = "X64";
            WriteNativeMethods();
        }

        private void WriteX86()
        {
            _Platform = "X86";
            WriteNativeMethods();
        }

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
            string baseClass = "";
            if (!Class.IsStatic && Class.HasInstance && !Class.IsConst && !Class.IsDynamic)
                baseClass = " : IDisposable";

            WriteLine(Class.Access + (Class.IsStatic ? " static" : "") + " partial class " + Class.Name + baseClass);
            WriteStartColon();

            WriteDelegates();

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
    }
}
