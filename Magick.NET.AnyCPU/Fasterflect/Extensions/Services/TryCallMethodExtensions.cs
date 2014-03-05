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
using Fasterflect.Probing;

namespace Fasterflect
{
    /// <summary>
    /// Extension methods for creating object instances when you do not know which constructor to call.
    /// </summary>
    public static class TryCallMethodExtensions
    {
        #region Method Invocation (TryCallMethod)
        /// <summary>
        /// Obtains a list of all methods with the given <paramref name="methodName"/> on the given 
        /// <paramref name="obj" />, and invokes the best match for the parameters obtained from the 
        /// public properties of the supplied <paramref name="sample"/> object.
        /// TryCallMethod is very liberal and attempts to convert values that are not otherwise
        /// considered compatible, such as between strings and enums or numbers, Guids and byte[16], etc.
        /// </summary>
        /// <returns>The result of the invocation.</returns>
        public static object TryCallMethod( this object obj, string methodName, bool mustUseAllParameters, object sample )
        {
            Type sourceType = sample.GetType();
            var sourceInfo = new SourceInfo( sourceType );
            var paramValues = sourceInfo.GetParameterValues( sample );
			return obj.TryCallMethod( methodName, mustUseAllParameters, sourceInfo.ParamNames, sourceInfo.ParamTypes, paramValues );
        }

        /// <summary>
        /// Obtains a list of all methods with the given <paramref name="methodName"/> on the given 
        /// <paramref name="obj" />, and invokes the best match for the parameters obtained from the 
        /// values in the supplied <paramref name="parameters"/> dictionary.
        /// TryCallMethod is very liberal and attempts to convert values that are not otherwise
        /// considered compatible, such as between strings and enums or numbers, Guids and byte[16], etc.
        /// </summary>
        /// <returns>The result of the invocation.</returns>
        public static object TryCallMethod( this object obj, string methodName, bool mustUseAllParameters, IDictionary<string, object> parameters )
        {
			bool hasParameters = parameters != null && parameters.Count > 0;
            string[] names = hasParameters ? parameters.Keys.ToArray() : new string[ 0 ];
            object[] values = hasParameters ? parameters.Values.ToArray() : new object[ 0 ];
            return obj.TryCallMethod( methodName, mustUseAllParameters, names, values.ToTypeArray(), values );
        }

        /// <summary>
        /// Obtains a list of all methods with the given <paramref name="methodName"/> on the given 
        /// <paramref name="obj" />, and invokes the best match for the supplied parameters.
        /// TryCallMethod is very liberal and attempts to convert values that are not otherwise
        /// considered compatible, such as between strings and enums or numbers, Guids and byte[16], etc.
        /// </summary>
        /// <param name="obj">The type of which an instance should be created.</param>
        /// <param name="methodName">The name of the overloaded methods.</param>
		/// <param name="mustUseAllParameters">Specifies whether all supplied parameters must be used in the
		/// invocation. Unless you know what you are doing you should pass true for this parameter.</param>
        /// <param name="parameterNames">The names of the supplied parameters.</param>
        /// <param name="parameterTypes">The types of the supplied parameters.</param>
        /// <param name="parameterValues">The values of the supplied parameters.</param>
        /// <returns>The result of the invocation.</returns>
        public static object TryCallMethod( this object obj, string methodName, bool mustUseAllParameters, 
											string[] parameterNames, Type[] parameterTypes, object[] parameterValues )
        {
        	bool isStatic = obj is Type;
			var type = isStatic ? obj as Type : obj.GetType();
        	var names = parameterNames ?? new string[ 0 ];
        	var types = parameterTypes ?? new Type[ 0 ];
			var values = parameterValues ?? new object[ 0 ];
			if( names.Length != values.Length || names.Length != types.Length )
			{
                throw new ArgumentException( "Mismatching name, type and value arrays (must be of identical length)." );
			}
			MethodMap map = MapFactory.DetermineBestMethodMatch( type.Methods( methodName ).Cast<MethodBase>(), mustUseAllParameters, names, types, values );
			return isStatic ? map.Invoke( values ) : map.Invoke( obj, values );
		}
        #endregion
    }
}