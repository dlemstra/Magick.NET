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
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Fasterflect.Probing
{
	/// <summary>
	/// Helper class for converting values between various types.
	/// </summary>
	public static class TypeConverter
	{
		#region GetValue methods
		/// <summary>
		/// Convert the supplied XmlNode into the specified target type. Only the InnerXml portion
		/// of the XmlNode is used in the conversion, unless the target type is itself an XmlNode.
		/// </summary>
		/// <param name="targetType">The type into which to convert</param>
		/// <param name="node">The source value used in the conversion operation</param>
		/// <returns>The converted value</returns>
		public static object Get( Type targetType, XmlNode node )
		{
			if( targetType == typeof(XmlNode) )
			{
				return node;
			}
			return Get( targetType, node.InnerXml );
		}
		/// <summary>
		/// Convert the supplied XAttribute into the specified target type. Only the Value portion
		/// of the XAttribute is used in the conversion, unless the target type is itself an XAttribute.
		/// </summary>
		/// <param name="targetType">The type into which to convert</param>
		/// <param name="attribute">The source value used in the conversion operation</param>
		/// <returns>The converted value</returns>
		public static object Get( Type targetType, XAttribute attribute )
		{
			if( targetType == typeof(XAttribute) )
			{
				return attribute;
			}
			return Get( targetType, attribute.Value );
		}
		/// <summary>
		/// Convert the supplied XElement into the specified target type. Only the Value portion
		/// of the XElement is used in the conversion, unless the target type is itself an XElement.
		/// </summary>
		/// <param name="targetType">The type into which to convert</param>
		/// <param name="element">The source value used in the conversion operation</param>
		/// <returns>The converted value</returns>
		public static object Get( Type targetType, XElement element )
		{
			if( targetType == typeof(XElement) )
			{
				return element;
			}
			return Get( targetType, element.Value );
		}

		/// <summary>
		/// Convert the supplied string into the specified target type. 
		/// </summary>
		/// <param name="targetType">The type into which to convert</param>
		/// <param name="value">The source value used in the conversion operation</param>
		/// <returns>The converted value</returns>
		public static object Get( Type targetType, string value )
		{
			Type sourceType = typeof(string);
			if( sourceType == targetType )
			{
				return value;
			}
			try
			{
				if( targetType.IsEnum )
				{
					return ConvertEnums( targetType, sourceType, value );
				}
				if( targetType == typeof(Guid) )
				{
					return ConvertGuids( targetType, sourceType, value );
				}
				if( targetType == typeof(Type) )
				{
					return ConvertTypes( targetType, sourceType, value );
				}
				return !string.IsNullOrEmpty( value ) ? Convert.ChangeType( value, targetType, CultureInfo.InvariantCulture ) : null;
			}
			catch( FormatException )
			{
				return null; // no conversion was possible
			}
		}

	    /// <summary>
		/// Convert the supplied object into the specified target type. 
		/// </summary>
		/// <param name="targetType">The type into which to convert</param>
		/// <param name="value">The source value used in the conversion operation</param>
		/// <returns>The converted value</returns>
		public static object Get( Type targetType, object value )
		{
			if( value == null )
			{
				return null;
			}
			Type sourceType = value.GetType();
			if( sourceType == targetType )
			{
				return value;
			}
			if( sourceType == typeof(string) )
			{
				return Get( targetType, value as string );
			}
			if( sourceType == typeof(XmlNode) )
			{
				return Get( targetType, value as XmlNode );
			}
			if( sourceType == typeof(XAttribute) )
			{
				return Get( targetType, value as XAttribute );
			}
			if( sourceType == typeof(XElement) )
			{
				return Get( targetType, value as XElement );
			}
			if( targetType.IsEnum || sourceType.IsEnum )
			{
				return ConvertEnums( targetType, sourceType, value );
			}
			if( targetType == typeof(Guid) || sourceType == typeof(Guid) )
			{
				return ConvertGuids( targetType, sourceType, value );
			}
			if( targetType == typeof(Type) || sourceType == typeof(Type) )
			{
				return ConvertTypes( targetType, sourceType, value );
			}
			return value is IConvertible ? Convert.ChangeType( value, targetType ) : null;
		}
		#endregion

		#region Type conversions
		/// <summary>
		/// A method that will convert between types and their textual names.
		/// </summary>
		/// <param name="targetType">The target type</param>
		/// <param name="sourceType">The type of the provided value.</param>
		/// <param name="value">The value representing the type.</param>
		/// <returns></returns>
		public static object ConvertTypes( Type targetType, Type sourceType, object value )
		{
			if( value == null )
			{
				return null;
			}
			if( targetType == typeof(Type) )
			{
				return Type.GetType( Convert.ToString( value ) );
			}
			if( sourceType == typeof(Type) && targetType == typeof(string) )
			{
				return sourceType.FullName;
			}
			return null;
		}
		#endregion

		#region Enum conversions
		/// <summary>
		/// Helper method for converting enums from/to different types.
		/// </summary>
		private static object ConvertEnums( Type targetType, Type sourceType, object value )
		{
			if( targetType.IsEnum )
			{
				if( sourceType == typeof(string) )
				{
					// assume the string represents the name of the enumeration element
					string source = (string) value;
					// first try to clean out unnecessary information (like assembly name and FQTN)
					source = source.Split( ',' )[ 0 ];
					int pos = source.LastIndexOf( '.' );
					if( pos > 0 )
						source = source.Substring( pos + 1 ).Trim();
					if( Enum.IsDefined( targetType, source ) )
					{
						return Enum.Parse( targetType, source );
					}
				}
				// convert the source object to the appropriate type of value
				value = Convert.ChangeType( value, Enum.GetUnderlyingType( targetType ) );
				return Enum.ToObject( targetType, value );
			}
			return Convert.ChangeType( value, targetType );
		}
		#endregion

		#region GUID conversions
		/// <summary>
		/// Convert the binary string (16 bytes) into a Guid.
		/// </summary>
		public static Guid StringToGuid( string guid )
		{
			char[] charBuffer = guid.ToCharArray();
			byte[] byteBuffer = new byte[16];
			int nCurrByte = 0;
			foreach( char currChar in charBuffer )
			{
				byteBuffer[ nCurrByte++ ] = (byte) currChar;
			}
			return new Guid( byteBuffer );
		}

		/// <summary>
		/// Convert the Guid into a binary string.
		/// </summary>
		public static string GuidToBinaryString( Guid guid )
		{
			StringBuilder sb = new StringBuilder( 16 );
			foreach( byte currByte in guid.ToByteArray() )
			{
				sb.Append( (char) currByte );
			}
			return sb.ToString();
		}

		/// <summary>
		/// A method that will convert guids from and to different types
		/// </summary>
		private static object ConvertGuids( Type targetType, Type sourceType, object sourceObj )
		{
			if( targetType == typeof(Guid) )
			{
				if( sourceType == typeof(string) )
				{
					string value = sourceObj as string;
					if( value != null && value.Length == 16 )
					{
						return StringToGuid( value );
					}
					return value != null ? new Guid( value ) : Guid.Empty;
				}
				if( sourceType == typeof(byte[]) )
				{
					return new Guid( (byte[]) sourceObj );
				}
			}
			else if( sourceType == typeof(Guid) )
			{
				Guid g = (Guid) sourceObj;
				if( targetType == typeof(string) )
				{
					return GuidToBinaryString( g );
				}
				if( targetType == typeof(byte[]) )
				{
					return g.ToByteArray();
				}
			}
			return null;
			// Check.FailPostcondition( typeof(TypeConverter), "Cannot convert type {0} to type {1}.", sourceType, targetType );
		}
		#endregion
	}
}