// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using System.Reflection;

namespace FileGenerator.Drawables;

internal abstract class DrawableCodeGenerator : CodeGenerator
{
    private readonly bool _isInterface;

    protected DrawableCodeGenerator(bool isInterface)
    {
        Types = new DrawableTypes();
        _isInterface = isInterface;
    }

    protected DrawableTypes Types { get; private set; }

    protected string GetTypeName(Type type)
    {
        var name = string.Empty;
        if (type.IsArray)
            name += "params ";

        if (type.IsGenericType)
            return name + type.Name.Replace("`1", string.Empty) + "<" + GetArgumentTypeName(type) + ">";

        return type.Name switch
        {
            "Boolean" => name + "bool",
            "Int32" => name + "int",
            "Double" or "Double[]" or "String" => name + type.Name.ToLowerInvariant(),
            _ => name + type.Name,
        };
    }

    protected void WriteParameterDeclaration(ParameterInfo[] parameters)
    {
        for (var i = 0; i < parameters.Length; i++)
        {
            Write(GetTypeName(parameters[i].ParameterType));
            Write(" ");
            Write(parameters[i].Name);

            if (i != parameters.Length - 1)
                Write(", ");
        }
    }

    protected void WriteParameters(ParameterInfo[] parameters)
    {
        for (var i = 0; i < parameters.Length; i++)
        {
            Write(parameters[i].Name);

            if (i != parameters.Length - 1)
                Write(", ");
        }
    }

    private string GetArgumentTypeName(Type type)
    {
        var name = type.GetGenericArguments().First().Name;
        if (name == "UInt16")
            name = _isInterface ? "TQuantumType" : "QuantumType";

        return name;
    }
}
