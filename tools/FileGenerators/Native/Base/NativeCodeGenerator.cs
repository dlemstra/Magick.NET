// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Linq;
using System.Collections.Generic;

namespace FileGenerator.Native
{
    internal abstract class NativeCodeGenerator : CodeGenerator
    {
        private static MagickClass[] _Classes;

        protected NativeCodeGenerator(MagickClass magickClass)
        {
            Class = magickClass;
        }

        protected NativeCodeGenerator(NativeCodeGenerator parent)
          : base(parent)
        {
            Class = parent.Class;
        }

        protected string GetArgumentsDeclaration(IEnumerable<MagickArgument> arguments)
        {
            return GetArgumentsDeclaration(arguments, (argument) =>
            {
                if (argument.IsOut && !IsDynamic(argument.Type))
                    return "out " + argument.Type.Native;
                else if (argument.IsOut)
                    return "out " + argument.Type.Managed;
                else if (IsQuantumType(argument.Type))
                    return "I" + argument.Type.Managed + "<QuantumType>";
                else if (HasInterface(argument.Type))
                    return "I" + argument.Type.Managed;
                else
                    return argument.Type.Managed;
            }, (argument) =>
            {
                return argument.IsHidden;
            });
        }

        protected string GetArgumentsDeclaration(IEnumerable<MagickArgument> arguments, Func<MagickArgument, string> typeFunc, Func<MagickArgument, bool> skipFunc)
        {
            string result = null;

            foreach (var argument in arguments)
            {
                if (skipFunc(argument))
                    continue;

                if (result != null)
                    result += ", ";

                result += typeFunc(argument) + " " + argument.Name;
            }

            return result;
        }

        protected string GetNativeArgumentsCall(IEnumerable<MagickArgument> arguments)
        {
            string result = null;

            foreach (var argument in arguments)
            {
                if (result != null)
                    result += ", ";

                if (argument.IsOut)
                {
                    if (!IsDynamic(argument.Type))
                        result += "out ";
                }
                else
                    result += argument.Type.NativeTypeCast;

                if (NeedsCreate(argument.Type))
                {
                    result += argument.Name + "Native";
                    if (argument.IsOut)
                        result += "Out";
                    else
                        result += ".Instance";
                }
                else if (argument.Type.HasInstance)
                    result += argument.Name + ".GetInstance()";
                else
                    result += argument.Name;
            }

            return result;
        }

        protected string GetNativeArgumentsCall(MagickMethod method)
        {
            string arguments = GetNativeArgumentsCall(method.Arguments);

            if (Class.IsStatic || method.IsStatic)
                return arguments;
            else if (string.IsNullOrEmpty(arguments))
                return "Instance";
            else
                return "Instance, " + arguments;
        }

        protected string GetNativeArgumentsDeclaration(IEnumerable<MagickArgument> arguments)
        {
            return GetArgumentsDeclaration(arguments, (argument) =>
            {
                if (argument.Type.HasInstance)
                    return "IntPtr";

                if (argument.Type.IsString)
                    return "IntPtr";

                if (argument.Type.IsBool)
                    return "[MarshalAs(UnmanagedType.Bool)] bool";

                if (argument.IsOut && !IsDynamic(argument.Type))
                    return "out " + argument.Type.Native;
                else
                    return argument.Type.Native;
            }, (argument) =>
            {
                return false;
            });
        }

        protected string GetNativeArgumentsDeclaration(MagickMethod method)
        {
            string arguments = GetNativeArgumentsDeclaration(method.Arguments);

            if (Class.IsStatic || method.IsStatic)
                return arguments;
            else if (string.IsNullOrEmpty(arguments))
                return "IntPtr Instance";
            else
                return "IntPtr Instance, " + arguments;
        }

        protected bool HasInterface(MagickType type)
            => _Classes.Any(c => c.Name == type.Managed && c.HasInterface);

        protected bool IsDynamic(string typeName)
            => _Classes.Any(c => c.Name == typeName && c.IsDynamic);

        protected bool IsDynamic(MagickType type)
            => IsDynamic(type.Managed);

        protected bool IsQuantumType(MagickType type)
            => _Classes.Any(c => c.Name == type.Managed && c.IsQuantumType);

        protected bool NeedsCreate(MagickType type)
        {
            if (type.IsString)
                return true;

            return IsDynamic(type.Managed);
        }

        protected static void RegisterClasses(IEnumerable<MagickClass> magickClasses)
            =>  _Classes = magickClasses.ToArray();

        protected void WriteCheckException(bool throws)
        {
            if (!throws)
                return;

            if (Class.IsStatic)
                WriteLine("MagickExceptionHelper.Check(exception);");
            else
                WriteLine("CheckException(exception);");
        }

        protected void WriteNativeIf(string action)
            => WriteNativeIfContent(action);

        protected void WriteNativeIfContent(string action)
        {
            WriteLine("#if PLATFORM_AnyCPU");
            WriteLine("if (OperatingSystem.Is64Bit)");
            WriteLine("#endif");

            WriteLine("#if PLATFORM_x64 || PLATFORM_AnyCPU");
            WriteLine(string.Format(action, "X64"));
            WriteLine("#endif");

            WriteLine("#if PLATFORM_AnyCPU");
            WriteLine("else");
            WriteLine("#endif");

            WriteLine("#if PLATFORM_x86 || PLATFORM_AnyCPU");
            WriteLine(string.Format(action, "X86"));
            WriteLine("#endif");
        }

        protected void WriteThrowStart()
            => WriteThrowStart(true);

        protected void WriteThrowStart(bool throws)
        {
            if (throws)
                WriteLine("IntPtr exception = IntPtr.Zero;");
        }

        protected override void WriteUsing()
        {
            WriteLine("using System;");
            WriteLine("using System.Security;");
            WriteLine("using System.Runtime.InteropServices;");

            if (UsesQuantumType())
                WriteQuantumType();
            else
                WriteLine();
        }

        protected MagickClass Class { get; }

        private bool UsesQuantumType()
        {
            if (Class.Properties.Any(property => UsesQuantumType(property.Type)))
                return true;

            if (Class.Methods.Any(method => UsesQuantumType(method.ReturnType) || method.Arguments.Any(argument => UsesQuantumType(argument.Type))))
                return true;

            return false;
        }

        private bool UsesQuantumType(MagickType type)
            => type.IsQuantumType || IsQuantumType(type);
    }
}
