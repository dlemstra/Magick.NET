// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ImageMagick.SourceGenerator;

[Generator]
internal class NativeInteropGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<CleanupAttribute>());
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<InstanceAttribute>());
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<NativeInteropAttribute>());
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<ThrowsAttribute>());
        context.RegisterAttributeCodeGenerator<NativeInteropAttribute, NativeInteropInfo>(GetClass, GenerateCode);
    }

    private static NativeInteropInfo GetClass(GeneratorAttributeSyntaxContext context)
        => new(context.SemanticModel, context.TargetNode);

    private static void GenerateCode(SourceProductionContext context, NativeInteropInfo info)
    {
        var codeBuilder = new CodeBuilder();
        codeBuilder.AppendLine("#nullable enable");

        codeBuilder.AppendLine();
        codeBuilder.AppendLine("using System;");
        codeBuilder.AppendLine("using System.Runtime.InteropServices;");

        codeBuilder.AppendLine();
        codeBuilder.Append("namespace ");
        codeBuilder.Append(info.Namespace);
        codeBuilder.AppendLine(";");
        codeBuilder.AppendLine();

        if (info.UsesQuantumType)
        {
            codeBuilder.AppendLine("#if Q8");
            codeBuilder.AppendLine("using QuantumType = System.Byte;");
            codeBuilder.AppendLine("#elif Q16");
            codeBuilder.AppendLine("using QuantumType = System.UInt16;");
            codeBuilder.AppendLine("#elif Q16HDRI");
            codeBuilder.AppendLine("using QuantumType = System.Single;");
            codeBuilder.AppendLine("#else");
            codeBuilder.AppendLine("#error Not implemented!");
            codeBuilder.AppendLine("#endif");
            codeBuilder.AppendLine();
        }

        if (info.IsInternal)
            codeBuilder.Append("internal");
        else
            codeBuilder.Append("public");

        codeBuilder.Append(" partial class ");
        codeBuilder.AppendLine(info.ParentClassName);
        codeBuilder.AppendOpenBrace();

        AppendInstance(info, codeBuilder);

        AppendNativeInterop(codeBuilder, info);

        AppendNativeClass(codeBuilder, info);

        AppendCreateMethods(context, codeBuilder, info);

        codeBuilder.AppendCloseBrace();

        context.AddSource($"{info.ParentClassName}.g.cs", SourceText.From(codeBuilder.ToString(), Encoding.UTF8));
    }

    private static void AppendInstance(NativeInteropInfo info, CodeBuilder codeBuilder)
    {
        if (info.HasInstanceField)
        {
            codeBuilder.Append("private readonly ");
            codeBuilder.Append(info.ClassName);
            codeBuilder.AppendLine(" _nativeInstance;");
            codeBuilder.AppendLine();
        }
    }

    private static void AppendNativeInterop(CodeBuilder codeBuilder, NativeInteropInfo info)
    {
        codeBuilder.AppendLine("private unsafe static class NativeMethods");
        codeBuilder.AppendOpenBrace();
        AppendNativeInterop(codeBuilder, info, "arm64");
        AppendNativeInterop(codeBuilder, info, "x64");
        AppendNativeInterop(codeBuilder, info, "x86");
        codeBuilder.AppendCloseBrace();
        codeBuilder.AppendLine();
    }

    private static void AppendNativeInterop(CodeBuilder codeBuilder, NativeInteropInfo info, string platform)
    {
        var name = platform.ToUpperInvariant();

        codeBuilder.Append("#if PLATFORM_");
        codeBuilder.Append(platform);
        codeBuilder.AppendLine(" || PLATFORM_AnyCPU");
        codeBuilder.Append("public static class ");
        codeBuilder.AppendLine(name);
        codeBuilder.AppendOpenBrace();

        var uniqueMethods = info.Methods
            .GroupBy(method => method.Name)
            .Select(group => group.First());

        if (info.HasDispose)
        {
            codeBuilder.Append("[DllImport(NativeLibrary.");
            codeBuilder.Append(name);
            codeBuilder.Append("Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = \"");
            codeBuilder.Append(info.EntryPointClassName);
            codeBuilder.AppendLine("_Dispose\")]");
            codeBuilder.Append("public static extern void ");
            codeBuilder.Append(info.ParentClassName);
            codeBuilder.AppendLine("_Dispose(IntPtr instance);");
        }

        foreach (var method in uniqueMethods)
        {
            if (method.OnlySupportedInNetstandard21)
                codeBuilder.AppendLine("#if NETSTANDARD2_1");

            codeBuilder.Append("[DllImport(NativeLibrary.");
            codeBuilder.Append(name);
            codeBuilder.Append("Name, CallingConvention = CallingConvention.Cdecl, EntryPoint = \"");
            codeBuilder.Append(info.EntryPointClassName);
            codeBuilder.Append("_");
            codeBuilder.Append(method.Name);
            codeBuilder.AppendLine("\")]");
            codeBuilder.Append("public static extern ");
            if (method.SetsInstance)
                codeBuilder.Append("IntPtr");
            else
                codeBuilder.Append(method.ReturnType.NativeName);
            codeBuilder.Append(" ");
            codeBuilder.Append(info.ParentClassName);
            codeBuilder.Append("_");
            codeBuilder.Append(method.Name);
            codeBuilder.Append("(");
            if (method.UsesInstance)
                codeBuilder.Append("IntPtr instance");

            for (var i = 0; i < method.Parameters.Count; i++)
            {
                var parameter = method.Parameters[i];
                if (method.UsesInstance ? i >= 0 : i > 0)
                    codeBuilder.Append(", ");

                if (parameter.IsOut)
                    codeBuilder.Append("out ");
                codeBuilder.Append(parameter.Type.NativeName);
                codeBuilder.Append(" ");
                codeBuilder.Append(parameter.Name);
            }

            if (method.Throws)
            {
                if (method.UsesInstance || method.Parameters.Count > 0)
                    codeBuilder.Append(", ");

                codeBuilder.Append("out IntPtr exception");
            }

            codeBuilder.Append(")");
            codeBuilder.AppendLine(";");

            if (method.OnlySupportedInNetstandard21)
                codeBuilder.AppendLine("#endif");
        }

        codeBuilder.AppendCloseBrace();
        codeBuilder.AppendLine("#endif");
    }

    private static void AppendNativeClass(CodeBuilder codeBuilder, NativeInteropInfo info)
    {
        codeBuilder.Append("private unsafe partial class ");
        codeBuilder.AppendLine(info.ClassName);
        codeBuilder.AppendOpenBrace();

        if (info.ParentClassName != "Environment")
        {
            codeBuilder.Append("static ");
            codeBuilder.Append(info.ClassName);
            codeBuilder.AppendLine("() { Environment.Initialize(); }");
        }

        AppendNativeConstructor(codeBuilder, info);

        AppendNativeClassInstanceMethods(codeBuilder, info);

        foreach (var method in info.Methods)
        {
            AppendNativeMethod(codeBuilder, info, method);
        }

        codeBuilder.AppendCloseBrace();
    }

    private static void AppendNativeConstructor(CodeBuilder codeBuilder, NativeInteropInfo info)
    {
        if (info.NativeToManaged)
        {
            codeBuilder.AppendLine();
            codeBuilder.Append("public ");
            codeBuilder.Append(info.ClassName);
            codeBuilder.AppendLine("(IntPtr nativeInstance)");
            codeBuilder.Indent++;
            codeBuilder.AppendLine("=> Instance = nativeInstance;");
            codeBuilder.Indent--;
        }
    }

    private static void AppendNativeClassInstanceMethods(CodeBuilder codeBuilder, NativeInteropInfo info)
    {
        if (info.HasTypeName)
        {
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("protected override string TypeName");
            codeBuilder.Indent++;
            codeBuilder.Append("=> nameof(");
            codeBuilder.Append(info.ParentClassName);
            codeBuilder.AppendLine(");");
            codeBuilder.Indent--;
        }

        if (info.HasDispose)
        {
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("protected override void Dispose(IntPtr instance)");
            codeBuilder.AppendOpenBrace();
            codeBuilder.AppendLine("#if PLATFORM_AnyCPU");
            codeBuilder.AppendLine("if (Runtime.IsArm64)");
            codeBuilder.AppendLine("#endif");
            AppendDisposeCall(codeBuilder, info, "arm64");
            codeBuilder.AppendLine("#if PLATFORM_AnyCPU");
            codeBuilder.AppendLine("else if (Runtime.Is64Bit)");
            codeBuilder.AppendLine("#endif");
            AppendDisposeCall(codeBuilder, info, "x64");
            codeBuilder.AppendLine("#if PLATFORM_AnyCPU");
            codeBuilder.AppendLine("else");
            codeBuilder.AppendLine("#endif");
            AppendDisposeCall(codeBuilder, info, "x86");
            codeBuilder.AppendCloseBrace();
        }
    }

    private static void AppendNativeMethod(CodeBuilder codeBuilder, NativeInteropInfo info, MethodInfo method)
    {
        codeBuilder.AppendLine();
        if (method.OnlySupportedInNetstandard21)
            codeBuilder.AppendLine("#if NETSTANDARD2_1");

        codeBuilder.Append("public ");
        if (method.IsStatic)
            codeBuilder.Append("static ");
        codeBuilder.Append("partial ");
        if (method.SetsInstance)
            codeBuilder.Append("void");
        else
            codeBuilder.Append(method.ReturnType.Name);
        codeBuilder.Append(" ");
        codeBuilder.Append(method.Name);
        codeBuilder.Append("(");
        for (var i = 0; i < method.Parameters.Count; i++)
        {
            var parameter = method.Parameters[i];
            if (i > 0)
                codeBuilder.Append(", ");

            if (parameter.IsOut)
                codeBuilder.Append("out ");
            codeBuilder.Append(parameter.Type.Name);
            codeBuilder.Append(" ");
            codeBuilder.Append(parameter.Name);
        }

        codeBuilder.Append(")");
        codeBuilder.AppendLine();
        codeBuilder.AppendOpenBrace();

        if (!method.IsVoid)
        {
            codeBuilder.Append(method.ReturnType.NativeName);
            codeBuilder.AppendLine(" result;");
        }
        else if (method.SetsInstance)
        {
            codeBuilder.AppendLine("IntPtr result;");
        }

        if (method.Throws)
        {
            codeBuilder.AppendLine("var exception = IntPtr.Zero;");
        }

        foreach (var parameter in method.Parameters)
        {
            if (parameter.Type.IsFixed)
            {
                codeBuilder.Append("fixed (");
                codeBuilder.Append(parameter.Type.NativeName);
                codeBuilder.Append(" ");
                codeBuilder.Append(parameter.Name);
                codeBuilder.Append("Fixed = ");
                codeBuilder.Append(parameter.Name);
                codeBuilder.AppendLine(")");
                codeBuilder.AppendOpenBrace();
            }
            else if (parameter.Type.HasCreateInstance)
            {
                codeBuilder.Append("using var ");
                codeBuilder.Append(parameter.Name);
                codeBuilder.Append("Native = ");

                if (parameter.Type.ClassName == "string")
                    codeBuilder.Append("UTF8Marshaler");
                else
                    codeBuilder.Append(parameter.Type.ClassName);

                codeBuilder.Append(".CreateInstance(");
                codeBuilder.Append(parameter.Name);
                codeBuilder.AppendLine(");");
            }
        }

        codeBuilder.AppendLine("#if PLATFORM_AnyCPU");
        codeBuilder.AppendLine("if (Runtime.IsArm64)");
        codeBuilder.AppendLine("#endif");
        AppendMethodImplementation(codeBuilder, info, method, "arm64");
        codeBuilder.AppendLine("#if PLATFORM_AnyCPU");
        codeBuilder.AppendLine("else if (Runtime.Is64Bit)");
        codeBuilder.AppendLine("#endif");
        AppendMethodImplementation(codeBuilder, info, method, "x64");
        codeBuilder.AppendLine("#if PLATFORM_AnyCPU");
        codeBuilder.AppendLine("else");
        codeBuilder.AppendLine("#endif");
        AppendMethodImplementation(codeBuilder, info, method, "x86");

        if (method.Cleanup is not null && method.Cleanup.Name is not null)
        {
            codeBuilder.AppendLine("var magickException = MagickExceptionHelper.Create(exception);");
            codeBuilder.AppendLine("if (magickException is MagickErrorException)");
            codeBuilder.AppendOpenBrace();
            codeBuilder.AppendLine("if (result != IntPtr.Zero)");
            codeBuilder.Indent++;
            codeBuilder.Append(method.Cleanup.Name);
            codeBuilder.Append("(result");
            if (method.Cleanup.Arguments is not null)
            {
                codeBuilder.Append(", ");
                codeBuilder.Append(method.Cleanup.Arguments);
            }

            codeBuilder.AppendLine(");");
            codeBuilder.Indent--;
            codeBuilder.AppendLine("throw magickException;");
            codeBuilder.AppendCloseBrace();
            if (!method.IsStatic)
                codeBuilder.AppendLine("RaiseWarning(magickException);");
        }
        else if (method.Throws)
        {
            codeBuilder.AppendLine("MagickExceptionHelper.Check(exception);");
        }

        if (method.SetsInstance)
        {
            codeBuilder.AppendLine("if (result != IntPtr.Zero)");
            codeBuilder.Indent++;
            codeBuilder.AppendLine("Instance = result;");
            codeBuilder.Indent--;
        }
        else if (!method.IsVoid)
        {
            codeBuilder.Append("return ");
            if (method.ReturnType.ClassName == "string")
            {
                if (method.Cleanup is not null)
                    codeBuilder.AppendLine("UTF8Marshaler.CreateInstanceAndRelinquish(result);");
                else if (method.ReturnType.Name == "string?")
                    codeBuilder.AppendLine("UTF8Marshaler.CreateNullableInstance(result);");
                else
                    codeBuilder.AppendLine("UTF8Marshaler.CreateInstance(result);");
            }
            else if (method.ReturnType.HasCreateInstance)
            {
                codeBuilder.Append(method.ReturnType.ClassName);
                codeBuilder.AppendLine(".CreateInstance(result);");
            }
            else
            {
                if (method.ReturnType.IsEnum)
                {
                    codeBuilder.Append("(");
                    codeBuilder.Append(method.ReturnType.Name);
                    codeBuilder.Append(")");
                }

                codeBuilder.AppendLine("result;");
            }
        }

        foreach (var parameter in method.Parameters)
        {
            if (parameter.Type.IsFixed)
            {
                codeBuilder.AppendCloseBrace();
            }
        }

        codeBuilder.AppendCloseBrace();

        if (method.OnlySupportedInNetstandard21)
            codeBuilder.AppendLine("#endif");
    }

    private static void AppendCreateMethods(SourceProductionContext context, CodeBuilder codeBuilder, NativeInteropInfo info)
    {
        if (info.ManagedToNative)
        {
            codeBuilder.AppendLine();
            codeBuilder.Append("internal static INativeInstance CreateInstance(");
            if (info.IsInternal)
                codeBuilder.Append(info.ParentClassName);
            else
                codeBuilder.Append(info.InterfaceName);
            codeBuilder.AppendLine("? instance)");
            codeBuilder.AppendOpenBrace();
            codeBuilder.AppendLine("if (instance is null)");
            codeBuilder.Indent++;
            codeBuilder.AppendLine("return NativeInstance.Zero;");
            codeBuilder.Indent--;
            if (info.IsInternal)
            {
                codeBuilder.AppendLine("return instance.CreateNativeInstance();");
            }
            else
            {
                codeBuilder.Append("return ");
                codeBuilder.Append(info.ParentClassName);
                codeBuilder.AppendLine(".CreateNativeInstance(instance);");
            }

            codeBuilder.AppendCloseBrace();
        }

        if (info.NativeToManaged)
        {
            codeBuilder.AppendLine();
            codeBuilder.Append("internal static ");
            codeBuilder.Append(info.InterfaceName);
            codeBuilder.AppendLine("? CreateInstance(IntPtr instance)");
            codeBuilder.AppendOpenBrace();
            codeBuilder.AppendLine("if (instance == IntPtr.Zero)");
            codeBuilder.Indent++;
            codeBuilder.AppendLine("return null;");
            codeBuilder.Indent--;
            codeBuilder.Append("using var nativeInstance = new ");
            codeBuilder.Append(info.ClassName);
            codeBuilder.AppendLine("(instance);");
            codeBuilder.Append("return new ");
            codeBuilder.Append(info.ParentClassName);
            codeBuilder.AppendLine("(nativeInstance);");
            codeBuilder.AppendCloseBrace();
        }
    }

    private static void AppendDisposeCall(CodeBuilder codeBuilder, NativeInteropInfo info, string platform)
    {
        codeBuilder.Append("#if PLATFORM_");
        codeBuilder.Append(platform);
        codeBuilder.AppendLine(" || PLATFORM_AnyCPU");
        codeBuilder.Append("NativeMethods.");
        codeBuilder.Append(platform.ToUpperInvariant());
        codeBuilder.Append(".");
        codeBuilder.Append(info.ParentClassName);
        codeBuilder.AppendLine("_Dispose(instance);");
        codeBuilder.AppendLine("#endif");
    }

    private static void AppendMethodImplementation(CodeBuilder codeBuilder, NativeInteropInfo info, MethodInfo method, string platform)
    {
        codeBuilder.Append("#if PLATFORM_");
        codeBuilder.Append(platform);
        codeBuilder.AppendLine(" || PLATFORM_AnyCPU");
        if (!method.IsVoid || method.SetsInstance)
            codeBuilder.Append("result = ");
        codeBuilder.Append("NativeMethods.");
        codeBuilder.Append(platform.ToUpperInvariant());
        codeBuilder.Append(".");
        codeBuilder.Append(info.ParentClassName);
        codeBuilder.Append("_");
        codeBuilder.Append(method.Name);
        codeBuilder.Append("(");
        if (method.UsesInstance)
            codeBuilder.Append("Instance");
        for (var i = 0; i < method.Parameters.Count; i++)
        {
            var parameter = method.Parameters[i];
            if (method.UsesInstance ? i >= 0 : i > 0)
                codeBuilder.Append(", ");

            if (parameter.Type.HasGetInstance)
            {
                codeBuilder.Append(parameter.Type.ClassName);
                codeBuilder.Append(".GetInstance(");
                codeBuilder.Append(parameter.Name);
                codeBuilder.Append(")");
                continue;
            }

            if (parameter.IsOut)
                codeBuilder.Append("out ");

            if (parameter.Type.IsEnum)
                codeBuilder.Append("(UIntPtr)");

            codeBuilder.Append(parameter.Name);
            if (parameter.Type.HasCreateInstance)
                codeBuilder.Append("Native.Instance");
            else if (parameter.Type.IsFixed)
                codeBuilder.Append("Fixed");
        }

        if (method.Throws)
        {
            if (method.UsesInstance || method.Parameters.Count > 0)
                codeBuilder.Append(", ");

            codeBuilder.Append("out exception");
        }

        codeBuilder.AppendLine(");");
        codeBuilder.AppendLine("#endif");
    }
}
