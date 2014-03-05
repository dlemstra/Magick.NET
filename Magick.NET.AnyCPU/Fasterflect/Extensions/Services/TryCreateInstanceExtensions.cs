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
using System.Linq;
using System.Reflection;
using Fasterflect.Caching;
using Fasterflect.Probing;

namespace Fasterflect
{
    /// <summary>
    /// Extension methods for creating object instances when you do not know which constructor to call.
    /// </summary>
    public static class TryCreateInstanceExtensions
    {
        /// <summary>
        /// This field is used to cache information on objects used as parameters for object construction, which
        /// improves performance for subsequent instantiations of the same type using a compatible source type.
        /// </summary>
        private static readonly Cache<Type, SourceInfo> sourceInfoCache = new Cache<Type, SourceInfo>();

        #region Constructor Invocation (TryCreateInstance)
        /// <summary>
        /// Creates an instance of the given <paramref name="type"/> using the public properties of the 
        /// supplied <paramref name="sample"/> object as input.
        /// This method will try to determine the least-cost route to constructing the instance, which
        /// implies mapping as many properties as possible to constructor parameters. Remaining properties
        /// on the source are mapped to properties on the created instance or ignored if none matches.
        /// TryCreateInstance is very liberal and attempts to convert values that are not otherwise
        /// considered compatible, such as between strings and enums or numbers, Guids and byte[], etc.
        /// </summary>
        /// <returns>An instance of <paramref name="type"/>.</returns>
        public static object TryCreateInstance( this Type type, object sample )
        {
            Type sourceType = sample.GetType();
            SourceInfo sourceInfo = sourceInfoCache.Get( sourceType );
            if( sourceInfo == null )
            {
                sourceInfo = new SourceInfo( sourceType );
                sourceInfoCache.Insert( sourceType, sourceInfo );
            }
            object[] paramValues = sourceInfo.GetParameterValues( sample );
            MethodMap map = MapFactory.PrepareInvoke( type, sourceInfo.ParamNames, sourceInfo.ParamTypes, paramValues );
            return map.Invoke( paramValues );
        }

        /// <summary>
        /// Creates an instance of the given <paramref name="type"/> using the values in the supplied
        /// <paramref name="parameters"/> dictionary as input.
        /// This method will try to determine the least-cost route to constructing the instance, which
        /// implies mapping as many values as possible to constructor parameters. Remaining values
        /// are mapped to properties on the created instance or ignored if none matches.
        /// TryCreateInstance is very liberal and attempts to convert values that are not otherwise
        /// considered compatible, such as between strings and enums or numbers, Guids and byte[], etc.
        /// </summary>
        /// <returns>An instance of <paramref name="type"/>.</returns>
        public static object TryCreateInstance( this Type type, IDictionary<string, object> parameters )
        {
			bool hasParameters = parameters != null && parameters.Count > 0;
            string[] names = hasParameters ? parameters.Keys.ToArray() : new string[ 0 ];
            object[] values = hasParameters ? parameters.Values.ToArray() : new object[ 0 ];
            return type.TryCreateInstance( names, values );
        }

        /// <summary>
        /// Creates an instance of the given <paramref name="type"/> using the supplied parameter information as input.
        /// Parameter types are inferred from the supplied <paramref name="parameterValues"/> and as such these
        /// should not be null.
        /// This method will try to determine the least-cost route to constructing the instance, which
        /// implies mapping as many properties as possible to constructor parameters. Remaining properties
        /// on the source are mapped to properties on the created instance or ignored if none matches.
        /// TryCreateInstance is very liberal and attempts to convert values that are not otherwise
        /// considered compatible, such as between strings and enums or numbers, Guids and byte[], etc.
        /// </summary>
        /// <param name="type">The type of which an instance should be created.</param>
        /// <param name="parameterNames">The names of the supplied parameters.</param>
        /// <param name="parameterValues">The values of the supplied parameters.</param>
        /// <returns>An instance of <paramref name="type"/>.</returns>
        public static object TryCreateInstance( this Type type, string[] parameterNames, object[] parameterValues )
        {
        	var names = parameterNames ?? new string[ 0 ];
			var values = parameterValues ?? new object[ 0 ];
			if( names.Length != values.Length )
			{
				throw new ArgumentException( "Mismatching name and value arrays (must be of identical length)." );
			}
            var parameterTypes = new Type[ names.Length ];
			for( int i = 0; i < names.Length; i++ )
            {
                object value = values[ i ];
                parameterTypes[ i ] = value != null ? value.GetType() : null;
            }
            return type.TryCreateInstance( names, parameterTypes, values );
        }

        /// <summary>
        /// Creates an instance of the given <paramref name="type"/> using the supplied parameter information as input.
        /// This method will try to determine the least-cost route to constructing the instance, which
        /// implies mapping as many properties as possible to constructor parameters. Remaining properties
        /// on the source are mapped to properties on the created instance or ignored if none matches.
        /// TryCreateInstance is very liberal and attempts to convert values that are not otherwise
        /// considered compatible, such as between strings and enums or numbers, Guids and byte[], etc.
        /// </summary>
        /// <param name="type">The type of which an instance should be created.</param>
        /// <param name="parameterNames">The names of the supplied parameters.</param>
        /// <param name="parameterTypes">The types of the supplied parameters.</param>
        /// <param name="parameterValues">The values of the supplied parameters.</param>
        /// <returns>An instance of <paramref name="type"/>.</returns>
        public static object TryCreateInstance( this Type type, string[] parameterNames, Type[] parameterTypes,
                                                object[] parameterValues )
        {
        	var names = parameterNames ?? new string[ 0 ];
        	var types = parameterTypes ?? new Type[ 0 ];
			var values = parameterValues ?? new object[ 0 ];
			if( names.Length != values.Length || names.Length != types.Length )
			{
                throw new ArgumentException( "Mismatching name, type and value arrays (must be of identical length)." );
			}
            MethodMap map = MapFactory.PrepareInvoke( type, names, types, values );
            return map.Invoke( values );
        }

        #endregion
    }
}