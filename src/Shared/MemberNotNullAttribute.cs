// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_0

namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]

internal sealed class MemberNotNullAttribute : Attribute
{
    public MemberNotNullAttribute(string member)
        => Members = [member];

    public MemberNotNullAttribute(params string[] members)
        => Members = members;

    public string[] Members { get; }
}

#endif
