//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections.Generic;
using System;

namespace Magick.NET.FileGenerator
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
        if (!Class.HasInstance && !method.ReturnType.IsVoid)
          return "Dispose(result);";

        return null;
      }

      var cleanup = method.Cleanup;

      string result = cleanup.Name + "(result";
      if (cleanup.Arguments.Count() > 0)
        result += ", " + string.Join(", ", cleanup.Arguments);
      return result + ");";
    }

    private void WriteCleanup(string cleanupString)
    {
      WriteLine("MagickException magickException = MagickExceptionHelper.Create(exception);");
      WriteLine("if (MagickExceptionHelper.IsError(magickException))");
      WriteStartColon();
      if (!string.IsNullOrEmpty(cleanupString))
        WriteIf("result != IntPtr.Zero", cleanupString);
      WriteLine("throw magickException;");
      WriteEndColon();
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

        WriteThrowStart(Class.Constructor.Throws);

        arguments = GetNativeArgumentsCall(Class.Constructor.Arguments);
        WriteNativeIfContent("_Instance = NativeMethods.{0}." + Class.Name + "_Create(" + arguments + ");");

        if (Class.Constructor.Throws)
          WriteLine("CheckException(exception, _Instance);");

        WriteIf("_Instance == IntPtr.Zero", "throw new InvalidOperationException();");

        WriteEndColon();
      }

      if (!Class.HasNativeConstructor)
        return;

      WriteLine("public Native" + Class.Name + "(IntPtr instance)");
      WriteStartColon();
      WriteLine("_Instance = instance;");
      WriteEndColon();
    }

    private void WriteCreateInstance()
    {
      if (Class.DynamicMode.HasFlag(DynamicMode.ManagedToNative))
      {
        WriteLine("internal static INativeInstance CreateInstance(" + Class.Name + " instance)");
        WriteStartColon();
        WriteIf("instance == null", "return NativeInstance.Zero;");
        WriteLine("return instance.CreateNativeInstance();");
        WriteEndColon();
      }

      if (Class.DynamicMode.HasFlag(DynamicMode.NativeToManaged))
      {
        WriteLine("internal static " + Class.Name + " CreateInstance(IntPtr instance)");
        WriteStartColon();
        WriteIf("instance == IntPtr.Zero", "return null;");
        WriteLine("using (Native" + Class.Name + " nativeInstance = new Native" + Class.Name + "(instance))");
        WriteStartColon();
        WriteLine("return new " + Class.Name + "(nativeInstance);");
        WriteEndColon();
        WriteEndColon();
      }
    }

    private void WriteDispose()
    {
      if (Class.IsConst || !Class.HasInstance)
        return;

      WriteLine("protected override void Dispose(IntPtr instance)");
      WriteStartColon();
      WriteLine("DisposeInstance(instance);");
      WriteEndColon();

      WriteLine("public static void DisposeInstance(IntPtr instance)");
      WriteStartColon();
      WriteNativeIfContent("NativeMethods.{0}." + Class.Name + "_Dispose(instance);");
      WriteEndColon();
    }

    private void WriteDynamicEnd(IEnumerable<MagickArgument> arguments)
    {
      foreach (MagickArgument argument in arguments)
      {
        if (!IsDynamic(argument.Type))
          continue;

        WriteEndColon();
      }
    }

    private void WriteDynamicEnd(MagickProperty property)
    {
      if (IsDynamic(property.Type))
        WriteEndColon();
    }

    private void WriteDynamicOut(IEnumerable<MagickArgument> arguments)
    {
      foreach (MagickArgument argument in arguments)
      {
        if (!argument.IsOut || !IsDynamic(argument.Type))
          continue;

        WriteLine(argument.Name + " = " + argument.Type.Managed + ".CreateInstance(" + argument.Name + "DynamicOut);");
      }
    }

    private void WriteDynamicStart(IEnumerable<MagickArgument> arguments)
    {
      foreach (MagickArgument argument in arguments)
      {
        if (!IsDynamic(argument.Type))
          continue;

        if (argument.IsOut)
          WriteDynamicStartOut(argument.Name, argument.Type);
        else
          WriteDynamicStart(argument.Name, argument.Type);
      }
    }

    private void WriteDynamicStart(MagickProperty property)
    {
      if (!IsDynamic(property.Type))
        return;

      WriteDynamicStart("value", property.Type);
    }

    private void WriteDynamicStart(string name, MagickType type)
    {
      WriteLine("using (INativeInstance " + name + "Dynamic = " + type.Managed + ".CreateInstance(" + name + "))");
      WriteStartColon();
    }

    private void WriteDynamicStartOut(string name, MagickType type)
    {
      WriteLine("using (INativeInstance " + name + "Dynamic = " + type.Managed + ".CreateInstance())");
      WriteStartColon();
      WriteLine("IntPtr " + name + "DynamicOut = " + name + "Dynamic.Instance;");
    }

    private void WriteGetInstance()
    {
      WriteLine("internal static IntPtr GetInstance(" + Class.Name + " instance)");
      WriteStartColon();
      WriteIf("instance == null", "return IntPtr.Zero;");
      WriteLine("return instance._NativeInstance.Instance;");
      WriteEndColon();
    }

    private void WriteInstance()
    {
      if (!Class.HasInstance)
        return;

      WriteLine("public override IntPtr Instance");
      WriteStartColon();
      WriteLine("get");
      WriteStartColon();
      WriteIf("_Instance == IntPtr.Zero", "throw new ObjectDisposedException(typeof(" + Class.Name + ").ToString());");
      WriteLine("return _Instance;");
      WriteEndColon();
      WriteLine("set");
      WriteStartColon();
      if (!Class.IsConst)
        WriteIf("_Instance != IntPtr.Zero", "Dispose(_Instance);");
      WriteLine("_Instance = value;");
      WriteEndColon();
      WriteEndColon();
    }

    private void WriteMethods()
    {
      foreach (var method in Class.Methods)
      {
        string arguments = GetArgumentsDeclaration(method.Arguments);
        bool isStatic = (method.IsStatic && !method.Throws) && !method.CreatesInstance;
        WriteLine("public " + (isStatic ? "static " : "") + method.ReturnType.Managed + " " + method.Name + "(" + arguments + ")");

        WriteStartColon();

        WriteDynamicStart(method.Arguments);

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
          string action = method.ReturnType.ManagedTypeCast + "NativeMethods.{0}." + Class.Name + "_" + method.Name + "(" + arguments + ");";
          if (isDynamic)
            action = "result = " + action;
          else if (!method.ReturnType.IsVoid && !method.CreatesInstance)
            action = "return " + action;

          WriteNativeIf(action);

          if (isDynamic)
            WriteLine("return " + method.ReturnType.Managed + ".CreateInstance(result);");
        }

        WriteDynamicEnd(method.Arguments);

        WriteEndColon();
      }
    }

    private void WriteProperties()
    {
      foreach (var property in Class.Properties)
      {
        WriteLine("public " + property.Type.Managed + " " + property.Name);
        WriteStartColon();

        WriteLine("get");
        WriteStartColon();

        WriteThrowStart(property.Throws);

        WriteLine(property.Type.Native + " result;");
        string arguments = "Instance";
        if (property.Throws)
          arguments += ", out exception";
        WriteNativeIfContent("result = NativeMethods.{0}." + Class.Name + "_" + property.Name + "_Get(" + arguments + ");");
        WriteCheckException(property.Throws);
        if (IsDynamic(property.Type))
          WriteLine("return " + property.Type.Managed + ".CreateInstance(result);");
        else if (property.Type.HasInstance)
          WriteLine("return " + property.Type.Managed + ".Create(result);");
        else
          WriteLine("return " + property.Type.ManagedTypeCast + "result;");

        WriteEndColon();

        if (!property.IsReadOnly)
        {
          WriteLine("set");
          WriteStartColon();

          WriteDynamicStart(property);

          string value = property.Type.NativeTypeCast + "value";
          if (IsDynamic(property.Type))
            value = "valueDynamic.Instance";
          else if (property.Type.HasInstance)
            value = property.Type.Managed + ".GetInstance(value)";

          if (property.Throws)
            WriteThrowSet(property, value);
          else
            WriteNativeIfContent("NativeMethods.{0}." + Class.Name + "_" + property.Name + "_Set(Instance, " + value + ");");

          WriteDynamicEnd(property);

          WriteEndColon();
        }

        WriteEndColon();
      }
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

      WriteDynamicOut(method.Arguments);

      string cleanupString = CreateCleanupString(method);
      if (!string.IsNullOrEmpty(cleanupString))
        WriteCleanup(cleanupString);
      else if ((method.CreatesInstance) && !Class.IsConst)
        WriteLine("CheckException(exception, result);");
      else
        WriteCheckException(true);

      if (IsDynamic(method.ReturnType))
        WriteLine("return " + method.ReturnType.Managed + ".CreateInstance(result);");
      else if (method.CreatesInstance && method.ReturnType.IsVoid)
        WriteLine("Instance = result;");
      else if (!method.ReturnType.IsVoid)
        WriteLine("return " + method.ReturnType.ManagedTypeCast + "result;");
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
        return;

      if (!IsDynamic(Class.Name))
        WriteLine("private Native" + Class.Name + " _NativeInstance;");

      string baseClass = "";
      if (IsNativeStatic)
        baseClass = "";
      else if (!Class.HasInstance)
        baseClass = " : NativeHelper";
      else if (Class.IsConst)
        baseClass = " : ConstNativeInstance";
      else
        baseClass = " : NativeInstance";

      WriteLine("private " + (IsNativeStatic ? "static" : "sealed") + " class Native" + Class.Name + baseClass);
      WriteStartColon();

      if (Class.HasInstance)
        WriteLine("private IntPtr _Instance = IntPtr.Zero;");

      WriteDispose();

      WriteConstructors();

      WriteInstance();

      WriteProperties();

      WriteMethods();

      WriteEndColon();

      if (!Class.HasInstance || Class.IsConst)
        return;

      if (IsDynamic(Class.Name))
        WriteCreateInstance();
      else
        WriteGetInstance();
    }
  }
}
