#region License
// Copyright 2009 Buu Nguyen (http://www.buunguyen.net/blog)
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

namespace Fasterflect
{
	internal static class MemberFilter
    {
        public static bool IsReservedName( this string name )
        {
            name = name.ToLowerInvariant();
            return name == ".ctor" || name == ".cctor";
        }

        public static string TrimExplicitlyImplementedName( this string name )
        {
            int index = name.IsReservedName() ? -1 : name.LastIndexOf( '.' ) + 1;
            return index > 0 ? name.Substring( index ) : name;
        }

        /// <summary>
        /// This method applies name filtering to a set of members.
        /// </summary>
        public static IList<T> Filter<T>( this IList<T> members, Flags bindingFlags, string[] names )
            where T : MemberInfo
        {
            var result = new List<T>( members.Count );
            bool ignoreCase = bindingFlags.IsSet( Flags.IgnoreCase );
            bool isPartial = bindingFlags.IsSet( Flags.PartialNameMatch );
            bool trimExplicit = bindingFlags.IsSet( Flags.TrimExplicitlyImplemented );

            for( int i = 0; i < members.Count; i++ )
            {
                var member = members[ i ];
                var memberName = trimExplicit ? member.Name.TrimExplicitlyImplementedName() : member.Name;
                for( int j = 0; j < names.Length; j++ )
                {
                    var name = names[ j ];
                	var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                    bool match = isPartial ? memberName.Contains( name ) : memberName.Equals( name, comparison );
                    if( match )
                    {
						result.Add( member );
                    	break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// This method applies type parameter type filtering to a set of methods.
        /// </summary>
        public static IList<T> Filter<T>(this IList<T> methods, Type[] genericTypes)
            where T : MethodBase
        {
            var result = new List<T>(methods.Count);
            for (int i = 0; i < methods.Count; i++)
            {
                var method = methods[i];
                if (method.ContainsGenericParameters)
                {
                    var genericArgs = method.GetGenericArguments();
                    if (genericArgs.Length != genericTypes.Length)
                        continue; 
                    result.Add(method);
                }
            }
            return result;
        }

        /// <summary>
        /// This method applies method parameter type filtering to a set of methods.
        /// </summary>
        public static IList<T> Filter<T>( this IList<T> methods, Flags bindingFlags, Type[] paramTypes )
            where T : MethodBase
        {
            var result = new List<T>( methods.Count );

            bool exact = bindingFlags.IsSet( Flags.ExactBinding );
            for( int i = 0; i < methods.Count; i++ )
            {
                var method = methods[ i ];
				// verify parameters
            	var parameters = method.GetParameters();
                if( parameters.Length != paramTypes.Length )
                {
                    continue;
                }
				// verify parameter type compatibility
                bool match = true;
                for( int j = 0; j < paramTypes.Length; j++ )
                {
                    var type = paramTypes[ j ];
                    var parameter = parameters[ j ];
                	Type parameterType = parameter.ParameterType;
                	bool ignoreParameterModifiers = ! exact;
					if( ignoreParameterModifiers && parameterType.IsByRef )
					{
						string name = parameterType.FullName;
						parameterType = Type.GetType( name.Substring( 0, name.Length - 1 ) ) ?? parameterType;
					}
                    match &= parameterType.IsGenericParameter || parameterType.ContainsGenericParameters || (exact ? type == parameterType : parameterType.IsAssignableFrom( type ));
                    if( ! match )
                    {
                        break;
                    }
                }
				if( match )
				{
	                result.Add( method );
				}
            }
            return result;
        }

        /// <summary>
        /// This method applies member type filtering to a set of members.
        /// </summary>
        public static IList<T> Filter<T>( this IList<T> members, Flags bindingFlags, MemberTypes memberTypes )
            where T : MemberInfo
        {
            var result = new List<T>( members.Count );

            for( int i = 0; i < members.Count; i++ )
            {
                var member = members[ i ];
                bool match = (member.MemberType & memberTypes) == member.MemberType;
                if( ! match )
                {
					continue;
                }
                result.Add( member );
            }
            return result;
        }

        /// <summary>
        /// This method applies flags-based filtering to a set of members.
        /// </summary>
        public static IList<T> Filter<T>( this IList<T> members, Flags bindingFlags ) where T : MemberInfo
        {
            var result = new List<T>( members.Count );
        	var properties = new List<string>( members.Count );

            bool excludeHidden = bindingFlags.IsSet( Flags.ExcludeHiddenMembers );
            bool excludeBacking = bindingFlags.IsSet( Flags.ExcludeBackingMembers );
            bool excludeExplicit = bindingFlags.IsSet( Flags.ExcludeExplicitlyImplemented );

            for( int i = 0; i < members.Count; i++ )
            {
                var member = members[ i ];
            	bool exclude = false;
				if( excludeHidden )
				{
					var method = member as MethodBase;
					// filter out anything but methods/constructors based on their name only
					exclude |= method == null && result.Any( m => m.Name == member.Name );
					// filter out methods that do not have a unique signature (this prevents overloads from being excluded by the ExcludeHiddenMembers flag)
					exclude |= method != null && result.Where( m => m is MethodBase ).Cast<MethodBase>().Any( m => m.Name == member.Name && m.HasParameterSignature( method.GetParameters() ) );
				}
				if( !exclude && excludeBacking )
				{
					exclude |= member is FieldInfo && member.Name[ 0 ] == '<';
					var method = member as MethodInfo;
 					if( method != null )
 					{
 						// filter out property backing methods
						exclude |= member.Name.Length > 4 && member.Name.Substring( 1, 3 ) == "et_";
						// filter out base implementations when an overrride exists
						exclude |= result.ContainsOverride( method );
 					}
					var property = member as PropertyInfo;
					if( property != null )
					{
						MethodInfo propertyGetter = property.GetGetMethod( true );
						exclude |= propertyGetter.IsVirtual && properties.Contains( property.Name );
						if( ! exclude )
						{
							properties.Add( property.Name );
						}
					}
				}
                exclude |= excludeExplicit && member.Name.Contains( "." ) && ! member.Name.IsReservedName();
                if( exclude )
                {
					continue;
                }
                result.Add( member );
            }
            return result;
        }

		private static bool ContainsOverride<T>( this IList<T> candidates, MethodInfo method ) where T : MemberInfo
    	{
			if( ! method.IsVirtual )
				return false;
   			var parameters = method.Parameters();
			for( int i = 0; i < candidates.Count; i++ )
			{
				MethodInfo candidate = candidates[ i ] as MethodInfo;
				if( candidate == null || ! candidate.IsVirtual || method.Name != candidate.Name )
				{
					continue;
				}
				if( parameters.Select( p => p.ParameterType ).SequenceEqual( candidate.Parameters().Select( p => p.ParameterType ) ) )
				{
					return true;
				}
			}
			return false;
    	}
    }
}