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
using Fasterflect.Emitter;

namespace Fasterflect
{
    /// <summary>
    /// Extension methods for locating and accessing fields.
    /// </summary>
    public static class FieldExtensions
    {
        #region Field Access
        /// <summary>
        /// Sets the field specified by <paramref name="name"/> on the given <paramref name="obj"/>
        /// to the specified <paramref name="value" />.
        /// </summary>
        /// <returns><paramref name="obj"/>.</returns>
        public static object SetFieldValue( this object obj, string name, object value )
        {
            DelegateForSetFieldValue( obj.GetTypeAdjusted(), name )( obj, value );
            return obj;
        }

        /// <summary>
        /// Gets the value of the field specified by <paramref name="name"/> on the given <paramref name="obj"/>.
        /// </summary>
        public static object GetFieldValue( this object obj, string name )
        {
            return DelegateForGetFieldValue( obj.GetTypeAdjusted(), name )( obj );
        }

        /// <summary>
        /// Sets the field specified by <paramref name="name"/> and matching <paramref name="bindingFlags"/>
        /// on the given <paramref name="obj"/> to the specified <paramref name="value" />.
        /// </summary>
        /// <returns><paramref name="obj"/>.</returns>
        public static object SetFieldValue( this object obj, string name, object value, Flags bindingFlags )
        {
            DelegateForSetFieldValue( obj.GetTypeAdjusted(), name, bindingFlags )( obj, value );
            return obj;
        }

        /// <summary>
        /// Gets the value of the field specified by <paramref name="name"/> and matching <paramref name="bindingFlags"/>
        /// on the given <paramref name="obj"/>.
        /// </summary>
        public static object GetFieldValue( this object obj, string name, Flags bindingFlags )
        {
            return DelegateForGetFieldValue( obj.GetTypeAdjusted(), name, bindingFlags )( obj );
        }

        /// <summary>
        /// Creates a delegate which can set the value of the field specified by <paramref name="name"/> on 
        /// the given <paramref name="type"/>.
        /// </summary>
        public static MemberSetter DelegateForSetFieldValue( this Type type, string name )
        {
            return DelegateForSetFieldValue( type, name, Flags.StaticInstanceAnyVisibility );
        }

        /// <summary>
        /// Creates a delegate which can get the value of the field specified by <paramref name="name"/> on 
        /// the given <paramref name="type"/>.
        /// </summary>
        public static MemberGetter DelegateForGetFieldValue( this Type type, string name )
        {
            return DelegateForGetFieldValue( type, name, Flags.StaticInstanceAnyVisibility );
        }

        /// <summary>
        /// Creates a delegate which can set the value of the field specified by <paramref name="name"/> and
        /// matching <paramref name="bindingFlags"/> on the given <paramref name="type"/>.
        /// </summary>
        public static MemberSetter DelegateForSetFieldValue( this Type type, string name, Flags bindingFlags )
        {
            var callInfo = new CallInfo(type, null, bindingFlags, MemberTypes.Field, name, null, null, false);
			return (MemberSetter) new MemberSetEmitter( callInfo ).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can get the value of the field specified by <paramref name="name"/> and
        /// matching <paramref name="bindingFlags"/> on the given <paramref name="type"/>.
        /// </summary>
        public static MemberGetter DelegateForGetFieldValue( this Type type, string name, Flags bindingFlags )
        {
            var callInfo = new CallInfo(type, null, bindingFlags, MemberTypes.Field, name, null, null, true);
			return (MemberGetter) new MemberGetEmitter( callInfo ).GetDelegate();
        }
        #endregion

        #region Field Lookup (Single)
        /// <summary>
        /// Gets the field identified by <paramref name="name"/> on the given <paramref name="type"/>. This method 
        /// searches for public and non-public instance fields on both the type itself and all parent classes.
        /// </summary>
        /// <returns>A single FieldInfo instance of the first found match or null if no match was found.</returns>
        public static FieldInfo Field( this Type type, string name )
        {
            return type.Field( name, Flags.InstanceAnyVisibility );
        }

        /// <summary>
        /// Gets the field identified by <paramref name="name"/> on the given <paramref name="type"/>. 
        /// Use the <paramref name="bindingFlags"/> parameter to define the scope of the search.
        /// </summary>
        /// <returns>A single FieldInfo instance of the first found match or null if no match was found.</returns>
        public static FieldInfo Field( this Type type, string name, Flags bindingFlags )
        {
            // we need to check all fields to do partial name matches
            if( bindingFlags.IsAnySet( Flags.PartialNameMatch | Flags.TrimExplicitlyImplemented ) )
            {
                return type.Fields( bindingFlags, name ).FirstOrDefault();
            }

            var result = type.GetField( name, bindingFlags );
            if( result == null && bindingFlags.IsNotSet( Flags.DeclaredOnly ) )
            {
                if( type.BaseType != typeof(object) && type.BaseType != null )
                {
                    return type.BaseType.Field( name, bindingFlags );
                }
            }
            bool hasSpecialFlags = bindingFlags.IsAnySet( Flags.ExcludeBackingMembers | Flags.ExcludeExplicitlyImplemented | Flags.ExcludeHiddenMembers );
            if( hasSpecialFlags )
            {
                IList<FieldInfo> fields = new List<FieldInfo> { result };
                fields = fields.Filter( bindingFlags );
                return fields.Count > 0 ? fields[ 0 ] : null;
            }
            return result;
        }
        #endregion

        #region Field Lookup (Multiple)
        /// <summary>
        /// Gets all public and non-public instance fields on the given <paramref name="type"/>,
        /// including fields defined on base types.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="names">The optional list of names against which to filter the result. If this parameter is
		/// <c>null</c> or empty no name filtering will be applied. This method will check for an exact, 
		/// case-sensitive match.</param>
        /// <returns>A list of all instance fields on the type. This value will never be null.</returns>
        public static IList<FieldInfo> Fields( this Type type, params string[] names )
        {
            return type.Fields( Flags.InstanceAnyVisibility, names );
        }

        /// <summary>
        /// Gets all fields on the given <paramref name="type"/> that match the specified <paramref name="bindingFlags"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination used to define
        /// the search behavior and result filtering.</param>
        /// <param name="names">The optional list of names against which to filter the result. If this parameter is
		/// <c>null</c> or empty no name filtering will be applied. The default behavior is to check for an exact, 
		/// case-sensitive match. Pass <see href="Flags.ExcludeExplicitlyImplemented"/> to exclude explicitly implemented 
		/// interface members, <see href="Flags.PartialNameMatch"/> to locate by substring, and 
		/// <see href="Flags.IgnoreCase"/> to ignore case.</param>
        /// <returns>A list of all matching fields on the type. This value will never be null.</returns>
        public static IList<FieldInfo> Fields( this Type type, Flags bindingFlags, params string[] names )
        {
            if( type == null || type == typeof(object) )
            {
                return new FieldInfo[0];
            }

            bool recurse = bindingFlags.IsNotSet( Flags.DeclaredOnly );
            bool hasNames = names != null && names.Length > 0;
            bool hasSpecialFlags = bindingFlags.IsAnySet( Flags.ExcludeBackingMembers | Flags.ExcludeExplicitlyImplemented | Flags.ExcludeHiddenMembers );

            if( ! recurse && ! hasNames && ! hasSpecialFlags )
            {
                return type.GetFields( bindingFlags ) ?? new FieldInfo[0];
            }

            var fields = GetFields( type, bindingFlags );
            fields = hasSpecialFlags ? fields.Filter( bindingFlags ) : fields;
            fields = hasNames ? fields.Filter( bindingFlags, names ) : fields;
            return fields;
        }

        private static IList<FieldInfo> GetFields( Type type, Flags bindingFlags )
        {
            bool recurse = bindingFlags.IsNotSet( Flags.DeclaredOnly );

            if( ! recurse )
            {
                return type.GetFields( bindingFlags ) ?? new FieldInfo[0];
            }

            bindingFlags |= Flags.DeclaredOnly;
            bindingFlags &= ~BindingFlags.FlattenHierarchy;

            var fields = new List<FieldInfo>();
            fields.AddRange( type.GetFields( bindingFlags ) );
            Type baseType = type.BaseType;
            while( baseType != null && baseType != typeof(object) )
            {
                fields.AddRange( baseType.GetFields( bindingFlags ) );
                baseType = baseType.BaseType;
            }
            return fields;
        }
        #endregion

        #region Field Combined

        #region TryGetValue
		/// <summary>
        /// Gets the first (public or non-public) instance field with the given <paramref name="name"/> on the given
        /// <paramref name="obj"/> object. Returns the value of the field if a match was found and null otherwise.
		/// </summary>
		/// <remarks>
        /// When using this method it is not possible to distinguish between a missing field and a field whose value is null.
		/// </remarks>
		/// <param name="obj">The source object on which to find the field</param>
		/// <param name="name">The name of the field whose value should be retrieved</param>
		/// <returns>The value of the field or null if no field was found</returns>
		public static object TryGetFieldValue( this object obj, string name )
        {
            return TryGetFieldValue( obj, name, Flags.InstanceAnyVisibility );
        }

		/// <summary>
        /// Gets the first field with the given <paramref name="name"/> on the given <paramref name="obj"/> object.
        /// Returns the value of the field if a match was found and null otherwise.
        /// Use the <paramref name="bindingFlags"/> parameter to limit the scope of the search.
		/// </summary>
		/// <remarks>
        /// When using this method it is not possible to distinguish between a missing field and a field whose value is null.
		/// </remarks>
		/// <param name="obj">The source object on which to find the field</param>
		/// <param name="name">The name of the field whose value should be retrieved</param>
		/// <param name="bindingFlags">A combination of Flags that define the scope of the search</param>
		/// <returns>The value of the field or null if no field was found</returns>
        public static object TryGetFieldValue( this object obj, string name, Flags bindingFlags )
        {
            try
            {
                return obj.GetFieldValue( name, bindingFlags );
            }
            catch( MissingFieldException )
            {
                return null;
            }
        }
        #endregion

        #region TrySetValue
		/// <summary>
        /// Sets the first (public or non-public) instance field with the given <paramref name="name"/> on the 
        /// given <paramref name="obj"/> object to supplied <paramref name="value"/>. Returns true if a value
        /// was assigned to a field and false otherwise.
		/// </summary>
		/// <param name="obj">The source object on which to find the field</param>
		/// <param name="name">The name of the field whose value should be retrieved</param>
		/// <param name="value">The value that should be assigned to the field</param>
		/// <returns>True if the value was assigned to a field and false otherwise</returns>
        public static bool TrySetFieldValue( this object obj, string name, object value )
        {
            return TrySetFieldValue( obj, name, value, Flags.InstanceAnyVisibility );
        }

		/// <summary>
        /// Sets the first field with the given <paramref name="name"/> on the given <paramref name="obj"/> object
        /// to the supplied <paramref name="value"/>. Returns true if a value was assigned to a field and false otherwise.
        /// Use the <paramref name="bindingFlags"/> parameter to limit the scope of the search.
		/// </summary>
		/// <param name="obj">The source object on which to find the field</param>
		/// <param name="name">The name of the field whose value should be retrieved</param>
		/// <param name="value">The value that should be assigned to the field</param>
		/// <param name="bindingFlags">A combination of Flags that define the scope of the search</param>
		/// <returns>True if the value was assigned to a field and false otherwise</returns>
        public static bool TrySetFieldValue( this object obj, string name, object value, Flags bindingFlags )
        {
            try
            {
                obj.SetFieldValue(name, value, bindingFlags );
                return true;
            }
            catch( MissingFieldException )
            {
                return false;
            }
        }
        #endregion

        #endregion
    }
}