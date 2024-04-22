// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.CodeAnalysis;

namespace ImageMagick.SourceGenerator;

internal static class IncrementalGeneratorInitializationContextExtensions
{
    public static void RegisterAttributeCodeGenerator<TAttribute, TInfoType>(this IncrementalGeneratorInitializationContext context, Func<GeneratorAttributeSyntaxContext, TInfoType> transform, Action<SourceProductionContext, TInfoType> action)
    {
        var fullName = typeof(TAttribute).FullName ?? throw new InvalidOperationException();
        var valuesProvider = context.SyntaxProvider.ForAttributeWithMetadataName(fullName, (_, _) => true, (context, _) => transform(context));
        context.RegisterSourceOutput(valuesProvider, action);
    }
}
