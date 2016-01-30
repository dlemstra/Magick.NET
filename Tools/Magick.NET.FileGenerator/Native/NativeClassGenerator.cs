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

using System.Collections.Generic;

namespace Magick.NET.FileGenerator
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
        WriteLine("public delegate " + func.Type + " " + func.Name + "Delegate(" + arguments + ");");
      }
    }

    private void WriteDllImports()
    {
      WriteDllImportStaticConstructor();
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
        if (property.Type.Managed == "string")
          arguments += "[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] ";
        else if (property.Type.Managed == "bool")
          arguments += "[MarshalAs(UnmanagedType.Bool)] ";

        arguments += property.Type.Native + " value";

        if (property.Throws)
          arguments += ", out IntPtr exception";

        WriteLine("public static extern void " + Class.Name + "_" + property.Name + "_Set(" + arguments + ");");
      }
    }

    private void WriteDllImportStaticConstructor()
    {
      WriteLine("static "+_Platform + "() { NativeLibrary.DoInitialize(); }");
    }

    private void WriteMarshal(MagickType type)
    {
      if (type.Managed == "string")
      {
        if (type.IsNativeString)
          WriteLine("[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NativeMarshaler))]");
        else
          WriteLine("[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]");
      }
      else if (type.Managed == "bool")
        WriteLine("[return: MarshalAs(UnmanagedType.Bool)]");
    }

    private void WriteMethods()
    {
      if (!Class.IsStatic)
        return;

      foreach (var method in Class.Methods)
      {
        string arguments = GetArgumentsDeclaration(method.Arguments);
        WriteLine("public static " + method.ReturnType.Managed + " " + method.Name + "(" + arguments + ")");

        arguments = GetNativeArgumentsCall(method.Arguments);
        string action = method.ReturnType.ManagedTypeCast + "{0}." + Class.Name + "_" + method.Name + "(" + arguments + ");";

        if (!method.Throws)
        {
          if (!method.ReturnType.IsVoid)
            action = "return " + action;
          WriteStartColon();
          WriteNativeIf(action);
          WriteEndColon();
        }
        else
        {
          WriteStartColon();
          WriteThrowStart();
          if (!method.ReturnType.IsVoid)
          {
            WriteLine(method.ReturnType.Managed + " result;");
            action = "result = " + action;
          }
          WriteNativeIfContent(action);
          WriteCheckException(method.Throws);
          if (!method.ReturnType.IsVoid)
            WriteLine("return result;");
          WriteEndColon();
        }
      }
    }

    private void WriteProperties()
    {
      if (!Class.IsStatic)
        return;

      foreach (var property in Class.Properties)
      {
        WriteLine("public static " + property.Type.Managed + " " + property.Name);
        WriteStartColon();

        WriteLine("get");
        WriteStartColon();
        WriteNativeIf("return " + property.Type.ManagedTypeCast + "{0}." + Class.Name + "_" + property.Name + "_Get();");
        WriteEndColon();

        if (!property.IsReadOnly)
        {
          WriteLine("set");
          WriteStartColon();
          WriteNativeIf("{0}." + Class.Name + "_" + property.Name + "_Set(" + property.Type.NativeTypeCast + "value);");
          WriteEndColon();
        }

        WriteEndColon();
      }
    }

    private void WriteX64()
    {
      _Platform = "X64";

      WriteLine("public static class X64");
      WriteStartColon();
      WriteDllImports();
      WriteEndColon();
    }

    private void WriteX86()
    {
      _Platform = "X86";

      WriteLine("public static class X86");
      WriteStartColon();
      WriteDllImports();
      WriteEndColon();
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

      WriteLine("private static class NativeMethods");
      WriteStartColon();
      WriteDelegates();
      WriteX64();
      WriteX86();
      WriteProperties();
      WriteMethods();
      WriteEndColon();

      var _WrapperGenerator = new NativeInstanceGenerator(this);
      _WrapperGenerator.Write();

      WriteEndColon();
      WriteEnd();

      CloseWriter();
    }
  }
}
