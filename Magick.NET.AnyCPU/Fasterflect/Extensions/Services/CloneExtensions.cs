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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace Fasterflect
{
	/// <summary>
	/// Extension methods for deep cloning of objects.
	/// </summary>
	public static class CloneExtensions
	{
		/// <summary>
		/// Produces a deep clone of the <paramref name="source"/> object. Reference integrity is maintained and
		/// every unique object in the graph is cloned only once.
		/// A current limitation of this method is that all objects in the graph must have a default constructor.
		/// </summary>
		/// <typeparam name="T">The type of the object to clone.</typeparam>
		/// <param name="source">The object to clone.</param>
		/// <returns>A deep clone of the source object.</returns>
		public static T DeepClone<T>( this T source ) where T : class, new()
		{
			return source.DeepClone( new Dictionary<object, object>() );
		}

		#region Private Helpers
		/// <summary>
		/// Returns a clone of the given <paramref name="source"/> object. The <paramref name="map"/> is used as
		/// a cache during recursive calls to ensure that a single object is only cloned once.
		/// </summary>
		private static T DeepClone<T>( this T source, Dictionary<object, object> map ) where T : class, new()
		{
			Type type = source.GetType();
			return type.IsArray ? source.CloneArray( map ) : source.CloneObject( map );
		}

		/// <summary>
		/// Returns a clone of the given <paramref name="source"/> object.
		/// </summary>
		private static T CloneObject<T>( this T source, Dictionary<object, object> map ) where T : class, new()
		{
			Type type = source.GetType();
			var clone = type.CreateInstance() as T;
			map[ source ] = clone;
			IList<FieldInfo> fields = type.Fields( Flags.StaticInstanceAnyVisibility ).Where( f => !f.IsLiteral && !f.IsCalculated( type ) ).ToList();
			object[] values = fields.Select( f => CloneField( f, source, map ) ).ToArray();
			for( int i = 0; i < fields.Count; i++ )
			{
				fields[ i ].Set( clone, values[ i ] );
			}
			return clone;
		}

		/// <summary>
		/// Returns a clone of the given <paramref name="source"/> array.
		/// </summary>
		private static T CloneArray<T>( this T source, Dictionary<object, object> map ) where T : class, new()
		{
			Type type = source.GetType();
			var clone = Activator.CreateInstance( type, ((ICollection) source).Count ) as T;
			map[ source ] = clone;
			var sourceList = (IList) source;
			var cloneList = (IList) clone;
			for( int i = 0; i < sourceList.Count; i++ )
			{
				object element = sourceList[ i ];
				cloneList[ i ] = element.ShouldClone() ? element.DeepClone( map ) : element;
			}
			return clone;
		}

		/// <summary>
		/// Determines whether the <paramref name="field"/> can be cloned or not. If the field cannot be
		/// cloned (because it is immutable, constant or literal) the value is returned, and otherwise
		/// a clone of the value will be returned (if not found in the map, a new clone is created).
		/// </summary>
		private static object CloneField( FieldInfo field, object source, Dictionary<object, object> map )
		{
			object result = field.IsLiteral ? field.GetRawConstantValue() : field.Get( source );
			if( result == null || ! result.ShouldClone() )
			{
				return result;
			}
			object clone;
			if( map.TryGetValue( result, out clone ) )
			{
				return clone;
			}
			return result.DeepClone( map );
		}

		/// <summary>
		/// Returns true if the <paramref name="obj"/> parameter needs cloning.
		/// </summary>
		private static bool ShouldClone( this object obj )
		{
			if( obj == null )
				return false;
			Type type = obj.GetType();
			if( type.IsValueType || type == typeof(string) )
				return false;
			if( type.IsGenericTypeDefinition || obj is Type || obj is Delegate )
				return false;
			return true;
		}

		/// <summary>
		/// This method returns true for fields that we can safely exclude while cloning an object. In theory 
		/// this relies on knowing implementation details for types, and may thus break if the BCL changes or
		/// has been implemented differently. Currently only used to make dictionaries clonable.
		/// </summary>
		private static bool IsCalculated( this FieldInfo field, Type ownerType )
		{
			var dictionaryFields = new[] { "keys", "values" };
			if( field.DeclaringType.IsFrameworkType() && ownerType.Implements<IDictionary>() )
				return dictionaryFields.Contains( field.Name );
			return false;
		}
		#endregion
	}
}