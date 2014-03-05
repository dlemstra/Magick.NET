#region License
// Copyright 2010 Buu Nguyen, Morten Mertner
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://fasterflect.codeplex.com/
#endregion

using System;
using System.Linq;
using System.Collections.Generic;

namespace Fasterflect
{
	/// <summary>
	/// Extension methods for inspecting types.
	/// </summary>
	public static class TypeExtensions
	{
		#region Implements
		/// <summary>
		/// Returns true if the supplied <paramref name="type"/> implements the given interface <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type (interface) to check for.</typeparam>
		/// <param name="type">The type to check.</param>
		/// <returns>True if the given type implements the specified interface.</returns>
		/// <remarks>This method is for interfaces only. Use <seealso cref="Inherits"/> for class types and <seealso cref="InheritsOrImplements"/> 
		/// to check both interfaces and classes.</remarks>
		public static bool Implements<T>( this Type type )
		{
			return type.Implements( typeof(T) );
		}

		/// <summary>
		/// Returns true of the supplied <paramref name="type"/> implements the given interface <paramref name="interfaceType"/>. If the given
		/// interface type is a generic type definition this method will use the generic type definition of any implemented interfaces
		/// to determine the result.
		/// </summary>
		/// <param name="interfaceType">The interface type to check for.</param>
		/// <param name="type">The type to check.</param>
		/// <returns>True if the given type implements the specified interface.</returns>
		/// <remarks>This method is for interfaces only. Use <seealso cref="Inherits"/> for classes and <seealso cref="InheritsOrImplements"/> 
		/// to check both interfaces and classes.</remarks>
		public static bool Implements( this Type type, Type interfaceType )
		{
			if( type == null || interfaceType == null || type == interfaceType )
				return false;
			if( interfaceType.IsGenericTypeDefinition && type.GetInterfaces().Where( t => t.IsGenericType ).Select( t => t.GetGenericTypeDefinition() ).Any( gt => gt == interfaceType ) )
			{
				return true;
			}
			return interfaceType.IsAssignableFrom( type );
		}
		#endregion

		#region Inherits
		/// <summary>
		/// Returns true if the supplied <paramref name="type"/> inherits from the given class <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type (class) to check for.</typeparam>
		/// <param name="type">The type to check.</param>
		/// <returns>True if the given type inherits from the specified class.</returns>
		/// <remarks>This method is for classes only. Use <seealso cref="Implements"/> for interface types and <seealso cref="InheritsOrImplements"/> 
		/// to check both interfaces and classes.</remarks>
		public static bool Inherits<T>( this Type type )
		{
			return type.Inherits( typeof(T) );
		}

		/// <summary>
		/// Returns true if the supplied <paramref name="type"/> inherits from the given class <paramref name="baseType"/>.
		/// </summary>
		/// <param name="baseType">The type (class) to check for.</param>
		/// <param name="type">The type to check.</param>
		/// <returns>True if the given type inherits from the specified class.</returns>
		/// <remarks>This method is for classes only. Use <seealso cref="Implements"/> for interface types and <seealso cref="InheritsOrImplements"/> 
		/// to check both interfaces and classes.</remarks>
		public static bool Inherits( this Type type, Type baseType )
		{
			if( baseType == null || type == null || type == baseType )
				return false;
			var rootType = typeof(object);
			if( baseType == rootType )
				return true;
			while( type != null && type != rootType )
			{
				var current = type.IsGenericType && baseType.IsGenericTypeDefinition ? type.GetGenericTypeDefinition() : type;
				if( baseType == current )
					return true;
				type = type.BaseType;
			}
			return false;
		}
		#endregion

		#region InheritsOrImplements
		/// <summary>
		/// Returns true if the supplied <paramref name="type"/> inherits from or implements the type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The base type to check for.</typeparam>
		/// <param name="type">The type to check.</param>
		/// <returns>True if the given type inherits from or implements the specified base type.</returns>
		public static bool InheritsOrImplements<T>( this Type type )
		{
			return type.InheritsOrImplements( typeof(T) );
		}

		/// <summary>
		/// Returns true of the supplied <paramref name="type"/> inherits from or implements the type <paramref name="baseType"/>.
		/// </summary>
		/// <param name="baseType">The base type to check for.</param>
		/// <param name="type">The type to check.</param>
		/// <returns>True if the given type inherits from or implements the specified base type.</returns>
		public static bool InheritsOrImplements( this Type type, Type baseType )
		{
			if( type == null || baseType == null )
				return false;
			return baseType.IsInterface ? type.Implements( baseType ) : type.Inherits( baseType );
		}
		#endregion

		#region IsFrameworkType

		#region IsFrameworkType Helpers
		private static readonly List<byte[]> tokens = new List<byte[]>
		                                              {
		                                              	new byte[] { 0xb7, 0x7a, 0x5c, 0x56, 0x19, 0x34, 0xe0, 0x89 },
		                                              	new byte[] { 0x31, 0xbf, 0x38, 0x56, 0xad, 0x36, 0x4e, 0x35 },
		                                              	new byte[] { 0xb0, 0x3f, 0x5f, 0x7f, 0x11, 0xd5, 0x0a, 0x3a }
		                                              };

		internal class ByteArrayEqualityComparer : EqualityComparer<byte[]>
		{
			public override bool Equals( byte[] x, byte[] y )
			{
				return x != null && y != null && x.SequenceEqual( y );
			}

			public override int GetHashCode( byte[] obj )
			{
				return obj.GetHashCode();
			}
		}
		#endregion

		/// <summary>
		/// Returns true if the supplied type is defined in an assembly signed by Microsoft.
		/// </summary>
		public static bool IsFrameworkType( this Type type )
		{
			if( type == null )
			{
				throw new ArgumentNullException( "type" );
			}
			byte[] publicKeyToken = type.Assembly.GetName().GetPublicKeyToken();
			return publicKeyToken != null && tokens.Contains( publicKeyToken, new ByteArrayEqualityComparer() );
		}
		#endregion

		#region Name (with generic pretty-printing)
		/// <summary>
		/// Returns the C# name, including any generic parameters, of the supplied <paramref name="type"/>.
		/// </summary>
		/// <param name="type">The type to return the name for.</param>
		/// <returns>The type name formatted as you'd write it in C#.</returns>
		public static string Name( this Type type )
		{
			if( type.IsArray )
			{
				return string.Format( "{0}[]", type.GetElementType().Name() );
			}
			if( type.ContainsGenericParameters || type.IsGenericType )
			{
				if( type.BaseType == typeof(Nullable<>) || (type.BaseType == typeof(ValueType) && type.UnderlyingSystemType.Name.StartsWith( "Nullable" )) )
				{
					return GetCSharpTypeName( type.GetGenericArguments().Single().Name ) + "?";
				}
				int index = type.Name.IndexOf( "`" );
				string genericTypeName = index > 0 ? type.Name.Substring( 0, index ) : type.Name;
				string genericArgs = string.Join( ",", type.GetGenericArguments().Select( t => t.Name() ).ToArray() );
				return genericArgs.Length == 0 ? genericTypeName : genericTypeName + "<" + genericArgs + ">";
			}
			return GetCSharpTypeName( type.Name );
		}

		private static string GetCSharpTypeName( string typeName )
		{
			switch( typeName )
			{
				case "String":
				case "Object":
				case "Void":
				case "Byte":
				case "Double":
				case "Decimal":
					return typeName.ToLower();
				case "Int16":
					return "short";
				case "Int32":
					return "int";
				case "Int64":
					return "long";
				case "Single":
					return "float";
				case "Boolean":
					return "bool";
				default:
					return typeName;
			}
		}
		#endregion
	}
}