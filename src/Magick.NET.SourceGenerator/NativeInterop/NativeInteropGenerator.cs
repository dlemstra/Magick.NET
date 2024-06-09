// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ImageMagick.SourceGenerator;

[Generator]
internal class NativeInteropGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<NativeInteropAttribute>());
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<ThrowsAttribute>());
        context.RegisterAttributeCodeGenerator<NativeInteropAttribute, NativeInteropInfo>(GetClass, GenerateCode);
    }

    private static NativeInteropInfo GetClass(GeneratorAttributeSyntaxContext context)
        => new(context.TargetNode);

    private static void GenerateCode(SourceProductionContext context, NativeInteropInfo info)
    {
        var codeBuilder = new CodeBuilder();
        codeBuilder.AppendLine("#nullable enable");

        codeBuilder.AppendLine();
        codeBuilder.AppendLine("using System;");
        codeBuilder.AppendLine("using System.Runtime.InteropServices;");

        codeBuilder.AppendLine();
        codeBuilder.AppendLine("namespace ImageMagick;");
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

        codeBuilder.Append("public partial class ");
        codeBuilder.AppendLine(info.ParentClassName);
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;

        AppendNativeInterop(codeBuilder, info);

        codeBuilder.AppendLine();

        AppendNativeClass(codeBuilder, info);

        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");

        context.AddSource($"{info.ParentClassName}.g.cs", SourceText.From(codeBuilder.ToString(), Encoding.UTF8));
    }

    private static void AppendNativeInterop(CodeBuilder codeBuilder, NativeInteropInfo info)
    {
        codeBuilder.AppendLine("private unsafe static class NativeMethods");
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;
        AppendNativeInterop(codeBuilder, info, "x64");
        AppendNativeInterop(codeBuilder, info, "arm64");
        AppendNativeInterop(codeBuilder, info, "x86");
        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");
    }

    private static void AppendNativeInterop(CodeBuilder codeBuilder, NativeInteropInfo info, string platform)
    {
        var name = platform.ToUpperInvariant();

        codeBuilder.Append("#if PLATFORM_");
        codeBuilder.Append(platform);
        codeBuilder.AppendLine(" || PLATFORM_AnyCPU");
        codeBuilder.Append("public static class ");
        codeBuilder.AppendLine(name);
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;

        foreach (var method in info.Methods)
        {
            codeBuilder.Append("[DllImport(NativeLibrary.");
            codeBuilder.Append(name);
            codeBuilder.AppendLine("Name, CallingConvention = CallingConvention.Cdecl)]");
            codeBuilder.Append("public static extern ");
            codeBuilder.Append(method.ReturnType);
            codeBuilder.Append(" ");
            codeBuilder.Append(info.ParentClassName);
            codeBuilder.Append("_");
            codeBuilder.Append(method.Name);
            codeBuilder.Append("(");
            for (var i = 0; i < method.Parameters.Count; i++)
            {
                var parameter = method.Parameters[i];
                if (i > 0)
                    codeBuilder.Append(", ");

                codeBuilder.Append(parameter.NativeType);
                codeBuilder.Append(" ");
                codeBuilder.Append(parameter.Name);
            }

            if (method.Throws)
            {
                if (method.Parameters.Count > 0)
                    codeBuilder.Append(", ");

                codeBuilder.Append("out IntPtr exception");
            }

            codeBuilder.Append(")");
            codeBuilder.AppendLine(";");
        }

        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");
        codeBuilder.AppendLine("#endif");
    }

    private static void AppendNativeClass(CodeBuilder codeBuilder, NativeInteropInfo info)
    {
        codeBuilder.Append("private unsafe partial class ");
        codeBuilder.AppendLine(info.ClassName);
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;

        codeBuilder.Append("static ");
        codeBuilder.Append(info.ClassName);
        codeBuilder.AppendLine("() { Environment.Initialize(); }");

        foreach (var method in info.Methods)
        {
            codeBuilder.Append("public ");
            if (method.IsStatic)
                codeBuilder.Append("static ");
            codeBuilder.Append("partial ");
            codeBuilder.Append(method.ReturnType);
            codeBuilder.Append(" ");
            codeBuilder.Append(method.Name);
            codeBuilder.Append("(");
            for (var i = 0; i < method.Parameters.Count; i++)
            {
                var parameter = method.Parameters[i];
                if (i > 0)
                    codeBuilder.Append(", ");

                codeBuilder.Append(parameter.Type);
                codeBuilder.Append(" ");
                codeBuilder.Append(parameter.Name);
            }

            codeBuilder.Append(")");
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("{");
            codeBuilder.Indent++;

            if (!method.IsVoid)
            {
                codeBuilder.Append(method.NativeReturnType);
                codeBuilder.AppendLine(" result;");
            }

            if (method.Throws)
            {
                codeBuilder.AppendLine("IntPtr exception = IntPtr.Zero;");
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

            if (method.Throws)
            {
                codeBuilder.AppendLine("MagickExceptionHelper.Check(exception);");
            }

            if (!method.IsVoid)
                codeBuilder.AppendLine("return result;");

            codeBuilder.Indent--;
            codeBuilder.AppendLine("}");
        }

        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");
    }

    private static void AppendMethodImplementation(CodeBuilder codeBuilder, NativeInteropInfo info, MethodInfo method, string platform)
    {
        codeBuilder.Append("#if PLATFORM_");
        codeBuilder.Append(platform);
        codeBuilder.AppendLine(" || PLATFORM_AnyCPU");
        if (method.ReturnType.ToString() != "void")
            codeBuilder.Append("result = ");
        codeBuilder.Append("NativeMethods.");
        codeBuilder.Append(platform.ToUpperInvariant());
        codeBuilder.Append(".");
        codeBuilder.Append(info.ParentClassName);
        codeBuilder.Append("_");
        codeBuilder.Append(method.Name);
        codeBuilder.Append("(");
        for (var i = 0; i < method.Parameters.Count; i++)
        {
            var parameter = method.Parameters[i];
            if (i > 0)
                codeBuilder.Append(", ");

            codeBuilder.Append(parameter.Name);
        }

        if (method.Throws)
        {
            if (method.Parameters.Count > 0)
                codeBuilder.Append(", ");

            codeBuilder.Append("out exception");
        }

        codeBuilder.AppendLine(");");
        codeBuilder.AppendLine("#endif");
    }
}
