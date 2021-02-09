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
using System.Collections.Generic;
using System.Linq;

namespace FileGenerator.Native
{
    internal sealed class NativeInstanceGenerator : NativeCodeGenerator
    {
        private bool IsNativeStatic
        {
            get
            {
                if (Class.HasInstance)
                    return false;

                if (Class.Methods.Any(method => method.CreatesInstance))
                    return false;

                if (Class.Methods.Any(method => method.Throws))
                    return false;

                if (Class.Methods.Any(method => !method.IsStatic))
                    return false;

                return true;
            }
        }

        private string CreateCleanupString(MagickMethod method)
        {
            if (method.Cleanup == null)
            {
                if (!Class.HasInstance && method.ReturnType.IsInstance)
                    return "Dispose(result);";

                return null;
            }

            var cleanup = method.Cleanup;

            string result = cleanup.Name + "(result";
            if (cleanup.Arguments.Count() > 0)
                result += ", " + string.Join(", ", cleanup.Arguments);
            return result + ");";
        }

        private string GetAction(string action, MagickType type)
        {
            if (type.IsString)
                return "UTF8Marshaler.NativeToManaged(" + action + ");";
            else
                return type.ManagedTypeCast + action + ";";
        }

        private void WriteCleanup(string cleanupString)
        {
            WriteLine("var magickException = MagickExceptionHelper.Create(exception);");
            WriteIf("magickException == null", "return result;");
            WriteLine("if (magickException is MagickErrorException)");
            WriteStartColon();
            WriteIf("result != IntPtr.Zero", cleanupString);
            WriteLine("throw magickException;");
            WriteEndColon();
            if (!Class.IsStatic)
                WriteLine("RaiseWarning(magickException);");
        }

        private void WriteConstructors()
        {
            if (!Class.HasInstance)
                return;

            if (!Class.IsConst && !Class.HasNoConstructor)
            {
                string arguments = GetArgumentsDeclaration(Class.Constructor.Arguments);
                WriteLine("public Native" + Class.Name + "(" + arguments + ")");
                WriteStartColon();

                WriteCreateStart(Class.Constructor.Arguments);

                WriteThrowStart(Class.Constructor.Throws);

                arguments = GetNativeArgumentsCall(Class.Constructor.Arguments);
                WriteNativeIfContent("Instance = NativeMethods.{0}." + Class.Name + "_Create(" + arguments + ");");

                if (Class.Constructor.Throws)
                    WriteLine("CheckException(exception, Instance);");

                WriteIf("Instance == IntPtr.Zero", "throw new InvalidOperationException();");

                WriteCreateEnd(Class.Constructor.Arguments);

                WriteEndColon();
            }

            if (!Class.HasNativeConstructor)
                return;

            WriteLine("public Native" + Class.Name + "(IntPtr instance)");
            WriteStartColon();
            WriteLine("Instance = instance;");
            WriteEndColon();
        }

        private void WriteCreateEnd(IEnumerable<MagickArgument> arguments)
        {
            foreach (MagickArgument argument in arguments)
            {
                if (!NeedsCreate(argument.Type))
                    continue;

                WriteEndColon();
            }
        }

        private void WriteCreateEnd(MagickProperty property)
        {
            if (NeedsCreate(property.Type))
                WriteEndColon();
        }

        private void WriteCreateInstance()
        {
            var name = Class.Name;
            if (Class.IsQuantumType)
                name = "I" + name + "<QuantumType>";
            else if (Class.HasInterface)
                name = "I" + name;

            if (Class.DynamicMode.HasFlag(DynamicMode.ManagedToNative))
            {
                WriteLine("internal static INativeInstance CreateInstance(" + name + " instance)");
                WriteStartColon();
                WriteIf("instance == null", "return NativeInstance.Zero;");
                if (Class.IsQuantumType || Class.HasInterface)
                    WriteLine("return " + Class.Name + ".CreateNativeInstance(instance);");
                else
                    WriteLine("return instance.CreateNativeInstance();");
                WriteEndColon();
            }

            if (Class.DynamicMode.HasFlag(DynamicMode.NativeToManaged))
            {
                WriteLine("internal static " + name + " CreateInstance(IntPtr instance)");
                WriteStartColon();
                WriteIf("instance == IntPtr.Zero", "return null;");
                WriteLine("using (Native" + Class.Name + " nativeInstance = new Native" + Class.Name + "(instance))");
                WriteStartColon();
                WriteLine("return new " + Class.Name + "(nativeInstance);");
                WriteEndColon();
                WriteEndColon();
            }
        }

        private void WriteCreateOut(IEnumerable<MagickArgument> arguments)
        {
            foreach (MagickArgument argument in arguments)
            {
                if (!argument.IsOut || !NeedsCreate(argument.Type))
                    continue;

                WriteLine(argument.Name + " = " + argument.Type.Managed + ".CreateInstance(" + argument.Name + "Native);");
            }
        }

        private void WriteCreateStart(IEnumerable<MagickArgument> arguments)
        {
            foreach (MagickArgument argument in arguments)
            {
                if (!NeedsCreate(argument.Type))
                    continue;

                if (argument.IsOut)
                    WriteCreateStartOut(argument.Name, argument.Type);
                else
                    WriteCreateStart(argument.Name, argument.Type);
            }
        }

        private void WriteCreateStart(MagickProperty property)
        {
            if (!NeedsCreate(property.Type))
                return;

            WriteCreateStart("value", property.Type);
        }

        private void WriteCreateStart(string name, MagickType type)
        {
            Write("using (INativeInstance " + name + "Native = ");

            if (type.IsString)
                Write("UTF8Marshaler");
            else
                Write(type.Managed);

            WriteLine(".CreateInstance(" + name + "))");
            WriteStartColon();
        }

        private void WriteCreateStartOut(string name, MagickType type)
        {
            WriteLine("using (INativeInstance " + name + "Native = " + type.Managed + ".CreateInstance())");
            WriteStartColon();
            WriteLine("IntPtr " + name + "NativeOut = " + name + "Native.Instance;");
        }

        private void WriteDispose()
        {
            if (Class.IsConst || !Class.HasInstance)
                return;


            WriteLine("protected override void Dispose(IntPtr instance)");
            WriteStartColon();

            if (Class.HasNoConstructor)
            {
                WriteLine("DisposeInstance(instance);");
                WriteEndColon();

                WriteLine("public static void DisposeInstance(IntPtr instance)");
                WriteStartColon();
            }

            WriteNativeIfContent("NativeMethods.{0}." + Class.Name + "_Dispose(instance);");
            WriteEndColon();
        }

        private void WriteTypeName()
        {
            if (!Class.HasInstance)
                return;

            WriteLine("protected override string TypeName");
            WriteStartColon();
            WriteLine("get");
            WriteStartColon();
            WriteLine("return nameof(" + Class.Name + ");");
            WriteEndColon();
            WriteEndColon();
        }

        private void WriteMethods()
        {
            foreach (var method in Class.Methods)
            {
                string arguments = GetArgumentsDeclaration(method.Arguments);
                bool isStatic = Class.IsStatic || ((method.IsStatic && !method.Throws) && !method.CreatesInstance);
                string typeName = GetTypeName(method.ReturnType);
                WriteLine("public " + (isStatic ? "static " : "") + typeName + " " + method.Name + "(" + arguments + ")");

                WriteStartColon();

                WriteCreateStart(method.Arguments);

                if (method.Throws)
                    WriteThrow(method);
                else
                {
                    if (method.CreatesInstance)
                        throw new NotImplementedException();

                    bool isDynamic = IsDynamic(method.ReturnType);

                    if (isDynamic)
                        WriteLine("IntPtr result;");
                    arguments = GetNativeArgumentsCall(method);
                    string action = GetAction("NativeMethods.{0}." + Class.Name + "_" + method.Name + "(" + arguments + ")", method.ReturnType);
                    if (isDynamic)
                        action = "result = " + action;
                    else if (!method.ReturnType.IsVoid && !method.CreatesInstance)
                        action = "return " + action;

                    WriteNativeIf(action);

                    if (isDynamic)
                        WriteLine("return " + method.ReturnType.Managed + ".CreateInstance(result);");
                }

                WriteCreateEnd(method.Arguments);

                WriteEndColon();
            }
        }

        private void WriteProperties()
        {
            foreach (var property in Class.Properties)
            {
                Write("public ");
                if (Class.IsStatic)
                    Write("static ");

                var typeName = GetTypeName(property.Type);

                WriteLine(typeName + " " + property.Name);
                WriteStartColon();

                WriteLine("get");
                WriteStartColon();

                WriteThrowStart(property.Throws);

                WriteLine(property.Type.Native + " result;");
                string arguments = !Class.IsStatic ? "Instance" : "";
                if (property.Throws)
                    arguments += ", out exception";
                WriteNativeIfContent("result = NativeMethods.{0}." + Class.Name + "_" + property.Name + "_Get(" + arguments + ");");
                WriteCheckException(property.Throws);
                WriteReturn(property.Type);

                WriteEndColon();

                if (!property.IsReadOnly)
                {
                    WriteLine("set");
                    WriteStartColon();

                    WriteCreateStart(property);

                    string value = property.Type.NativeTypeCast + "value";
                    if (NeedsCreate(property.Type))
                        value = "valueNative.Instance";
                    else if (property.Type.HasInstance)
                        value = "value.GetInstance()";

                    arguments = !Class.IsStatic ? "Instance, " : "";

                    if (property.Throws)
                        WriteThrowSet(property, value);
                    else
                        WriteNativeIfContent("NativeMethods.{0}." + Class.Name + "_" + property.Name + "_Set(" + arguments + value + ");");

                    WriteCreateEnd(property);

                    WriteEndColon();
                }

                WriteEndColon();
            }
        }

        private void WriteReturn(MagickType type)
        {
            if (type.IsVoid)
                return;

            if (IsDynamic(type))
                WriteLine("return " + type.Managed + ".CreateInstance(result);");
            else if (type.IsNativeString)
                WriteLine("return UTF8Marshaler.NativeToManagedAndRelinquish(result);");
            else if (type.IsString)
                WriteLine("return UTF8Marshaler.NativeToManaged(result);");
            else if (type.HasInstance)
                WriteLine("return result.Create" + type.Managed + "();");
            else
                WriteLine("return " + type.ManagedTypeCast + "result;");
        }

        private void WriteStaticConstructor()
        {
            if (Class.Name == "Environment")
                return;

            WriteLine("static Native" + Class.ClassName + "() { Environment.Initialize(); }");
        }

        private void WriteThrow(MagickMethod method)
        {
            WriteThrowStart();

            if (method.CreatesInstance)
                WriteLine("IntPtr result;");
            else if (!method.ReturnType.IsVoid)
                WriteLine(method.ReturnType.Native + " result;");

            string arguments = GetNativeArgumentsCall(method);
            string action = "NativeMethods.{0}." + Class.Name + "_" + method.Name + "(" + arguments + ");";
            if (!method.ReturnType.IsVoid || method.CreatesInstance)
                action = "result = " + action;
            WriteNativeIfContent(action);

            WriteCreateOut(method.Arguments);

            string cleanupString = CreateCleanupString(method);
            if (!string.IsNullOrEmpty(cleanupString))
                WriteCleanup(cleanupString);
            else if ((method.CreatesInstance) && !Class.IsConst)
                WriteLine("CheckException(exception, result);");
            else
                WriteCheckException(true);

            if (method.CreatesInstance && method.ReturnType.IsVoid)
            {
                WriteLine("if (result != IntPtr.Zero)");
                WriteLine("  Instance = result;");
            }
            else
                WriteReturn(method.ReturnType);
        }

        private void WriteThrowSet(MagickProperty property, string value)
        {
            WriteThrowStart();

            WriteNativeIfContent("NativeMethods.{0}." + Class.Name + "_" + property.Name + "_Set(Instance, " + value + ", out exception);");
            WriteCheckException(true);
        }

        public NativeInstanceGenerator(NativeClassGenerator parent)
            : base(parent)
        {
        }

        public void Write()
        {
            if (Class.IsStatic)
            {
                WriteLine("private static class Native" + Class.ClassName);
                WriteStartColon();

                WriteStaticConstructor();
            }
            else
            {
                if (!IsDynamic(Class.Name))
                    WriteLine("private Native" + Class.ClassName + " _nativeInstance;");

                string baseClass = "";
                if (IsNativeStatic)
                    baseClass = "";
                else if (!Class.HasInstance)
                    baseClass = " : NativeHelper";
                else if (Class.IsConst)
                    baseClass = " : ConstNativeInstance";
                else
                    baseClass = " : NativeInstance";

                WriteLine("private " + (IsNativeStatic ? "static" : "sealed") + " class Native" + Class.ClassName + baseClass);
                WriteStartColon();

                WriteStaticConstructor();

                WriteDispose();

                WriteConstructors();

                WriteTypeName();
            }

            WriteProperties();

            WriteMethods();

            WriteEndColon();

            if (!Class.HasInstance || Class.IsConst || Class.IsStatic)
                return;

            if (IsDynamic(Class.Name))
                WriteCreateInstance();
        }

        private string GetTypeName(MagickType type)
        {
            var typeName = type.Managed;

            if (IsQuantumType(type))
                return "I" + typeName + "<QuantumType>";

            if (HasInterface(type))
                typeName = "I" + typeName;

            return typeName;
        }
    }
}
