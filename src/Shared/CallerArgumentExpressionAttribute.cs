// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NET8_0
namespace System.Runtime.CompilerServices;

/// <summary>Allows capturing of the expressions passed to a method.</summary>
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
internal sealed class CallerArgumentExpressionAttribute : Attribute
{
    public CallerArgumentExpressionAttribute(string parameterName)
    {
        ParameterName = parameterName;
    }

    public string ParameterName { get; }
}
#endif
