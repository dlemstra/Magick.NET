// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ImageMagick.SourceGenerator;

[Generator]
internal class NativeInteropGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<NativeInteropAttribute>());
        context.RegisterAttributeCodeGenerator<NativeInteropAttribute, NativeInteropInfo>(GetClass, GenerateCode);
    }

    private static NativeInteropInfo GetClass(GeneratorAttributeSyntaxContext context)
        => new(context.TargetNode);

    private static void GenerateCode(SourceProductionContext context, NativeInteropInfo info)
    {
        var codeBuilder = new CodeBuilder();
        codeBuilder.AppendLine("#nullable enable");

        codeBuilder.AppendLine();
        codeBuilder.AppendLine("using System.Runtime.InteropServices;");

        codeBuilder.AppendLine();
        codeBuilder.AppendLine("namespace ImageMagick;");
        codeBuilder.AppendLine();

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
            codeBuilder.Append(method.ReturnType.ToString());
            codeBuilder.Append(" ");
            codeBuilder.Append(info.ParentClassName);
            codeBuilder.Append("_");
            codeBuilder.Append(method.Identifier.Text);
            codeBuilder.Append("(");
            for (var i = 0; i < method.ParameterList.Parameters.Count; i++)
            {
                var parameter = method.ParameterList.Parameters[i];
                if (i > 0)
                    codeBuilder.Append(", ");

                codeBuilder.Append(parameter.Type.ToNativeString());
                codeBuilder.Append(" ");
                codeBuilder.Append(parameter.Identifier.Text);
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
            var isVoid = method.ReturnType.ToString() == "void";

            codeBuilder.Append("public ");
            if (method.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.StaticKeyword)))
                codeBuilder.Append("static ");
            codeBuilder.Append("partial ");
            codeBuilder.Append(method.ReturnType.ToString());
            codeBuilder.Append(" ");
            codeBuilder.Append(method.Identifier.Text);
            codeBuilder.Append("(");
            for (var i = 0; i < method.ParameterList.Parameters.Count; i++)
            {
                var parameter = method.ParameterList.Parameters[i];
                if (i > 0)
                    codeBuilder.Append(", ");

                codeBuilder.Append(parameter.Type!.ToString());
                codeBuilder.Append(" ");
                codeBuilder.Append(parameter.Identifier.Text);
            }

            codeBuilder.Append(")");
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("{");
            codeBuilder.Indent++;

            if (!isVoid)
            {
                codeBuilder.Append(method.ReturnType.ToNativeString());
                codeBuilder.AppendLine(" result;");
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

            if (!isVoid)
                codeBuilder.AppendLine("return result;");

            codeBuilder.Indent--;
            codeBuilder.AppendLine("}");
        }

        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");
    }

    private static void AppendMethodImplementation(CodeBuilder codeBuilder, NativeInteropInfo info, MethodDeclarationSyntax method, string platform)
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
        codeBuilder.Append(method.Identifier.Text);
        codeBuilder.Append("(");
        for (var i = 0; i < method.ParameterList.Parameters.Count; i++)
        {
            var parameter = method.ParameterList.Parameters[i];
            if (i > 0)
                codeBuilder.Append(", ");

            codeBuilder.Append(parameter.Identifier.Text);
        }

        codeBuilder.AppendLine(");");
        codeBuilder.AppendLine("#endif");
    }
}
