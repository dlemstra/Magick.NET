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
using System.Linq.Expressions;
using System.Reflection;
using Fasterflect.Emitter;

namespace Fasterflect
{
    /// <summary>
    /// Extension methods for locating and accessing properties.
    /// </summary>
    public static class PropertyExtensions
    {
        #region Property Access
        /// <summary>
        /// Sets the property specified by <param name="name"/> on the given <param name="obj"/> to the 
        /// specified <param name="value" />.
        /// </summary>
        /// <returns><paramref name="obj"/>.</returns>
        public static object SetPropertyValue( this object obj, string name, object value )
        {
            DelegateForSetPropertyValue( obj.GetTypeAdjusted(), name )( obj, value );
            return obj;
        }

        /// <summary>
        /// Gets the value of the property specified by <param name="name"/> on the given <param name="obj"/>.
        /// </summary>
        public static object GetPropertyValue( this object obj, string name )
        {
            return DelegateForGetPropertyValue( obj.GetTypeAdjusted(), name )( obj );
        }

        /// <summary>
        /// Sets the property specified by <param name="name"/> matching <param name="bindingFlags"/>
        /// on the given <param name="obj"/> to the specified <param name="value" />.
        /// </summary>
        /// <returns><paramref name="obj"/>.</returns>
        public static object SetPropertyValue( this object obj, string name, object value, Flags bindingFlags )
        {
            DelegateForSetPropertyValue( obj.GetTypeAdjusted(), name, bindingFlags )( obj, value );
            return obj;
        }

        /// <summary>
        /// Gets the value of the property specified by <param name="name"/> matching <param name="bindingFlags"/>
        /// on the given <param name="obj"/>.
        /// </summary>
        public static object GetPropertyValue( this object obj, string name, Flags bindingFlags )
        {
            return DelegateForGetPropertyValue( obj.GetTypeAdjusted(), name, bindingFlags )( obj );
        }

        /// <summary>
        /// Sets the property specified by <param name="memberExpression"/> on the given <param name="obj"/> to the 
        /// specified <param name="value" />.
        /// </summary>
        /// <returns><paramref name="obj"/>.</returns>
        public static object SetPropertyValue( this object obj, Expression<Func<object>> memberExpression, object value )
        {
        	var body = memberExpression != null ? memberExpression.Body as MemberExpression : null;
			if( body == null || body.Member == null )
			{
				throw new ArgumentNullException( "memberExpression" );
			}
        	return obj.SetPropertyValue( body.Member.Name, value );
        }

        /// <summary>
        /// Gets the value of the property specified by <param name="memberExpression"/> on the given <param name="obj"/>.
        /// </summary>
        public static object GetPropertyValue( this object obj, Expression<Func<object>> memberExpression )
        {
        	var body = memberExpression != null ? memberExpression.Body as MemberExpression : null;
			if( body == null || body.Member == null )
			{
				throw new ArgumentNullException( "memberExpression" );
			}
        	return obj.GetPropertyValue( body.Member.Name );
        }

        /// <summary>
        /// Creates a delegate which can set the value of the property specified by <param name="name"/>
        /// on the given <param name="type"/>.
        /// </summary>
        public static MemberSetter DelegateForSetPropertyValue( this Type type, string name )
        {
            return DelegateForSetPropertyValue( type, name, Flags.StaticInstanceAnyVisibility );
        }

        /// <summary>
        /// Creates a delegate which can get the value of the property specified by <param name="name"/>
        /// on the given <param name="type"/>.
        /// </summary>
        public static MemberGetter DelegateForGetPropertyValue( this Type type, string name )
        {
            return DelegateForGetPropertyValue( type, name, Flags.StaticInstanceAnyVisibility );
        }

        /// <summary>
        /// Creates a delegate which can set the value of the property specified by <param name="name"/>
        /// matching <param name="bindingFlags"/> on the given <param name="type"/>.
        /// </summary>
        public static MemberSetter DelegateForSetPropertyValue( this Type type, string name, Flags bindingFlags )
        {
            var callInfo = new CallInfo(type, null, bindingFlags, MemberTypes.Property, name, null, null, false);
			return (MemberSetter) new MemberSetEmitter( callInfo ).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can get the value of the property specified by <param name="name"/>
        /// matching <param name="bindingFlags"/> on the given <param name="type"/>.
        /// </summary>
        public static MemberGetter DelegateForGetPropertyValue( this Type type, string name, Flags bindingFlags )
        {
            var callInfo = new CallInfo(type, null, bindingFlags, MemberTypes.Property, name, null, null, true);
			return (MemberGetter) new MemberGetEmitter( callInfo ).GetDelegate();
        }
    	#endregion

        #region Indexer Access
        /// <summary>
        /// Sets the value of the indexer of the given <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object whose indexer is to be set.</param>
        /// <param name="parameters">The list of the indexer parameters plus the value to be set to the indexer.
        /// The parameter types are determined from these parameters, therefore no parameter can be <c>null</c>.
        /// If any parameter is <c>null</c> (or you can't be sure of that, i.e. receive from a variable), 
        /// use a different overload of this method.</param>
        /// <returns>The object whose indexer is to be set.</returns>
        /// <example>
        /// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
        /// method should be invoked as follow:
        /// <code>
        /// obj.SetIndexer(new Type[]{typeof(int), typeof(string)}, new object[]{1, "a"});
        /// </code>
        /// </example>
        public static object SetIndexer( this object obj, params object[] parameters )
        {
            DelegateForSetIndexer( obj.GetTypeAdjusted(), parameters.ToTypeArray() )( obj, parameters );
            return obj;
        }

        /// <summary>
        /// Sets the value of the indexer of the given <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object whose indexer is to be set.</param>
        /// <param name="parameterTypes">The types of the indexer parameters (must be in the right order), plus
        /// the type of the indexer.</param>
        /// <param name="parameters">The list of the indexer parameters plus the value to be set to the indexer.
        /// This list must match with the <paramref name="parameterTypes"/> list.</param>
        /// <returns>The object whose indexer is to be set.</returns>
        /// <example>
        /// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
        /// method should be invoked as follow:
        /// <code>
        /// obj.SetIndexer(new Type[]{typeof(int), typeof(string)}, new object[]{1, "a"});
        /// </code>
        /// </example>
        public static object SetIndexer( this object obj, Type[] parameterTypes, params object[] parameters )
        {
            DelegateForSetIndexer( obj.GetTypeAdjusted(), parameterTypes )( obj, parameters );
            return obj;
        }

        /// <summary>
        /// Gets the value of the indexer of the given <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object whose indexer is to be retrieved.</param>
        /// <param name="parameters">The list of the indexer parameters.
        /// The parameter types are determined from these parameters, therefore no parameter can be <code>null</code>.
        /// If any parameter is <code>null</code> (or you can't be sure of that, i.e. receive from a variable), 
        /// use a different overload of this method.</param>
        /// <returns>The value returned by the indexer.</returns>
        public static object GetIndexer( this object obj, params object[] parameters )
        {
            return DelegateForGetIndexer( obj.GetTypeAdjusted(), parameters.ToTypeArray() )( obj, parameters );
        }

        /// <summary>
        /// Gets the value of the indexer of the given <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object whose indexer is to be retrieved.</param>
        /// <param name="parameterTypes">The types of the indexer parameters (must be in the right order).</param>
        /// <param name="parameters">The list of the indexer parameters.</param>
        /// <returns>The value returned by the indexer.</returns>
        public static object GetIndexer( this object obj, Type[] parameterTypes, params object[] parameters )
        {
            return DelegateForGetIndexer( obj.GetTypeAdjusted(), parameterTypes )( obj, parameters );
        }

        /// <summary>
        /// Sets the value of the indexer matching <paramref name="bindingFlags"/> of the given <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object whose indexer is to be set.</param>
        /// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
        /// <param name="parameters">The list of the indexer parameters plus the value to be set to the indexer.
        /// The parameter types are determined from these parameters, therefore no parameter can be <c>null</c>.
        /// If any parameter is <c>null</c> (or you can't be sure of that, i.e. receive from a variable), 
        /// use a different overload of this method.</param>
        /// <returns>The object whose indexer is to be set.</returns>
        /// <example>
        /// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
        /// method should be invoked as follow:
        /// <code>
        /// obj.SetIndexer(new Type[]{typeof(int), typeof(string)}, new object[]{1, "a"});
        /// </code>
        /// </example>
        public static object SetIndexer( this object obj, Flags bindingFlags, params object[] parameters )
        {
            DelegateForSetIndexer( obj.GetTypeAdjusted(), bindingFlags, parameters.ToTypeArray() )( obj,
                                                                                                        parameters );
            return obj;
        }

        /// <summary>
        /// Sets the value of the indexer matching <paramref name="bindingFlags"/> of the given <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object whose indexer is to be set.</param>
        /// <param name="parameterTypes">The types of the indexer parameters (must be in the right order), plus
        ///   the type of the indexer.</param>
        /// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
        /// <param name="parameters">The list of the indexer parameters plus the value to be set to the indexer.
        ///   This list must match with the <paramref name="parameterTypes"/> list.</param>
        /// <returns>The object whose indexer is to be set.</returns>
        /// <example>
        /// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
        /// method should be invoked as follow:
        /// <code>
        /// obj.SetIndexer(new Type[]{typeof(int), typeof(string)}, new object[]{1, "a"});
        /// </code>
        /// </example>
        public static object SetIndexer( this object obj, Type[] parameterTypes, Flags bindingFlags, params object[] parameters )
        {
            DelegateForSetIndexer( obj.GetTypeAdjusted(), bindingFlags, parameterTypes )( obj, parameters );
            return obj;
        }

        /// <summary>
        /// Gets the value of the indexer matching <paramref name="bindingFlags"/> of the given <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object whose indexer is to be retrieved.</param>
        /// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
        /// <param name="parameters">The list of the indexer parameters.
        /// The parameter types are determined from these parameters, therefore no parameter can be <code>null</code>.
        /// If any parameter is <code>null</code> (or you can't be sure of that, i.e. receive from a variable), 
        /// use a different overload of this method.</param>
        /// <returns>The value returned by the indexer.</returns>
        public static object GetIndexer( this object obj, Flags bindingFlags, params object[] parameters )
        {
            return DelegateForGetIndexer( obj.GetTypeAdjusted(), bindingFlags, parameters.ToTypeArray() )( obj,
                                                                                                               parameters );
        }

        /// <summary>
        /// Gets the value of the indexer matching <paramref name="bindingFlags"/> of the given <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object whose indexer is to be retrieved.</param>
        /// <param name="parameterTypes">The types of the indexer parameters (must be in the right order).</param>
        /// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
        /// <param name="parameters">The list of the indexer parameters.</param>
        /// <returns>The value returned by the indexer.</returns>
        public static object GetIndexer( this object obj, Type[] parameterTypes, Flags bindingFlags, params object[] parameters )
        {
            return DelegateForGetIndexer( obj.GetTypeAdjusted(), bindingFlags, parameterTypes )( obj, parameters );
        }

        /// <summary>
        /// Creates a delegate which can set an indexer
        /// </summary>
        /// <param name="type">The type which the indexer belongs to.</param>
        /// <param name="parameterTypes">The types of the indexer parameters (must be in the right order), plus
        /// the type of the indexer.</param>
        /// <returns>A delegate which can set an indexer.</returns>
        /// <example>
        /// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
        /// method should be invoked as follow:
        /// <code>
        /// MethodInvoker invoker = type.DelegateForSetIndexer(new Type[]{typeof(int), typeof(string)});
        /// </code>
        /// </example>
        public static MethodInvoker DelegateForSetIndexer( this Type type, params Type[] parameterTypes )
        {
            return DelegateForSetIndexer( type, Flags.InstanceAnyVisibility, parameterTypes );
        }

        /// <summary>
        /// Creates a delegate which can get the value of an indexer.
        /// </summary>
        /// <param name="type">The type which the indexer belongs to.</param>
        /// <param name="parameterTypes">The types of the indexer parameters (must be in the right order).</param>
        /// <returns>The delegate which can get the value of an indexer.</returns>
        public static MethodInvoker DelegateForGetIndexer( this Type type, params Type[] parameterTypes )
        {
            return DelegateForGetIndexer( type, Flags.InstanceAnyVisibility, parameterTypes );
        }

        /// <summary>
        /// Creates a delegate which can set an indexer matching <paramref name="bindingFlags"/>.
        /// </summary>
        /// <param name="type">The type which the indexer belongs to.</param>
        /// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
        /// <param name="parameterTypes">The types of the indexer parameters (must be in the right order), plus
        /// the type of the indexer.</param>
        /// <returns>A delegate which can set an indexer.</returns>
        /// <example>
        /// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
        /// method should be invoked as follow:
        /// <code>
        /// MethodInvoker invoker = type.DelegateForSetIndexer(new Type[]{typeof(int), typeof(string)});
        /// </code>
        /// </example>
        public static MethodInvoker DelegateForSetIndexer( this Type type, Flags bindingFlags,
                                                           params Type[] parameterTypes )
        {
            return (MethodInvoker)
                new MethodInvocationEmitter( type, bindingFlags, Constants.IndexerSetterName, parameterTypes ).
                    GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can get the value of an indexer matching <paramref name="bindingFlags"/>.
        /// </summary>
        /// <param name="type">The type which the indexer belongs to.</param>
        /// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
        /// <param name="parameterTypes">The types of the indexer parameters (must be in the right order).</param>
        /// <returns>The delegate which can get the value of an indexer.</returns>
        public static MethodInvoker DelegateForGetIndexer( this Type type, Flags bindingFlags,
                                                           params Type[] parameterTypes )
        {
            return (MethodInvoker)
                new MethodInvocationEmitter( type, bindingFlags, Constants.IndexerGetterName, parameterTypes ).
                    GetDelegate();
        }
        #endregion

        #region Property Lookup (Single)
        /// <summary>
        /// Gets the property identified by <paramref name="name"/> on the given <paramref name="type"/>. This method 
        /// searches for public and non-public instance properties on both the type itself and all parent classes.
        /// </summary>
        /// <returns>A single PropertyInfo instance of the first found match or null if no match was found.</returns>
        public static PropertyInfo Property( this Type type, string name )
        {
            return type.Property( name, Flags.InstanceAnyVisibility );
        }

        /// <summary>
        /// Gets the property identified by <paramref name="name"/> on the given <paramref name="type"/>. 
        /// Use the <paramref name="bindingFlags"/> parameter to define the scope of the search.
        /// </summary>
        /// <returns>A single PropertyInfo instance of the first found match or null if no match was found.</returns>
        public static PropertyInfo Property( this Type type, string name, Flags bindingFlags )
        {
            // we need to check all properties to do partial name matches
            if( bindingFlags.IsAnySet( Flags.PartialNameMatch | Flags.TrimExplicitlyImplemented ) )
            {
                return type.Properties( bindingFlags, name ).FirstOrDefault();
            }

            var result = type.GetProperty( name, bindingFlags );
            if( result == null && bindingFlags.IsNotSet( Flags.DeclaredOnly ) )
            {
                if( type.BaseType != typeof(object) && type.BaseType != null )
                {
                    return type.BaseType.Property( name, bindingFlags );
                }
            }
            bool hasSpecialFlags = bindingFlags.IsSet( Flags.ExcludeExplicitlyImplemented );
            if( hasSpecialFlags )
            {
                IList<PropertyInfo> properties = new List<PropertyInfo> { result };
                properties = properties.Filter( bindingFlags );
                return properties.Count > 0 ? properties[ 0 ] : null;
            }
            return result;
        }
        #endregion

        #region Property Lookup (Multiple)
        /// <summary>
        /// Gets all public and non-public instance properties on the given <paramref name="type"/>,
        /// including properties defined on base types. The result can optionally be filtered by specifying
        /// a list of property names to include using the <paramref name="names"/> parameter.
        /// </summary>
        /// <returns>A list of matching instance properties on the type.</returns>
        /// <param name="type">The type whose public properties are to be retrieved.</param>
        /// <param name="names">A list of names of properties to be retrieved. If this is <c>null</c>, 
        /// all properties are returned.</param>
        /// <returns>A list of all public properties on the type filted by <paramref name="names"/>.
        /// This value will never be null.</returns>
        public static IList<PropertyInfo> Properties( this Type type, params string[] names )
        {
            return type.Properties( Flags.InstanceAnyVisibility, names );
        }

        /// <summary>
        /// Gets all properties on the given <paramref name="type"/> that match the specified <paramref name="bindingFlags"/>,
        /// including properties defined on base types.
        /// </summary>
        /// <returns>A list of all matching properties on the type. This value will never be null.</returns>
        public static IList<PropertyInfo> Properties( this Type type, Flags bindingFlags, params string[] names )
        {
            if (type == null || type == Constants.ObjectType)
            {
                return Constants.EmptyPropertyInfoArray;
            }

            bool recurse = bindingFlags.IsNotSet( Flags.DeclaredOnly );
            bool hasNames = names != null && names.Length > 0;
            bool hasSpecialFlags = bindingFlags.IsAnySet( Flags.ExcludeBackingMembers | Flags.ExcludeExplicitlyImplemented | Flags.ExcludeHiddenMembers );

            if( ! recurse && ! hasNames && ! hasSpecialFlags )
            {
                return type.GetProperties( bindingFlags ) ?? Constants.EmptyPropertyInfoArray;
            }

            var properties = GetProperties( type, bindingFlags );
            properties = hasSpecialFlags ? properties.Filter( bindingFlags ) : properties;
            properties = hasNames ? properties.Filter( bindingFlags, names ) : properties;
            return properties;
        }

        private static IList<PropertyInfo> GetProperties( Type type, Flags bindingFlags )
        {
            bool recurse = bindingFlags.IsNotSet( Flags.DeclaredOnly );

            if( ! recurse )
            {
                return type.GetProperties( bindingFlags ) ?? Constants.EmptyPropertyInfoArray;
            }

            bindingFlags |= Flags.DeclaredOnly;
            bindingFlags &= ~BindingFlags.FlattenHierarchy;

            var properties = new List<PropertyInfo>();
            properties.AddRange( type.GetProperties( bindingFlags ) );
            Type baseType = type.BaseType;
            while( baseType != null && baseType != typeof(object) )
            {
                properties.AddRange( baseType.GetProperties( bindingFlags ) );
                baseType = baseType.BaseType;
            }
            return properties;
        }
        #endregion

        #region Property Combined

        #region TryGetValue
		/// <summary>
        /// Gets the first (public or non-public) instance property with the given <paramref name="name"/> on the given
        /// <paramref name="obj"/> object. Returns the value of the property if a match was found and null otherwise.
		/// </summary>
		/// <remarks>
        /// When using this method it is not possible to distinguish between a missing property and a property whose value is null.
		/// </remarks>
		/// <param name="obj">The source object on which to find the property</param>
		/// <param name="name">The name of the property whose value should be retrieved</param>
		/// <returns>The value of the property or null if no property was found</returns>
        public static object TryGetPropertyValue( this object obj, string name )
        {
            return TryGetPropertyValue( obj, name, Flags.InstanceAnyVisibility );
        }

		/// <summary>
        /// Gets the first property with the given <paramref name="name"/> on the given <paramref name="obj"/> object.
        /// Returns the value of the property if a match was found and null otherwise.
        /// Use the <paramref name="bindingFlags"/> parameter to limit the scope of the search.
		/// </summary>
		/// <remarks>
        /// When using this method it is not possible to distinguish between a missing property and a property whose value is null.
		/// </remarks>
		/// <param name="obj">The source object on which to find the property</param>
		/// <param name="name">The name of the property whose value should be retrieved</param>
		/// <param name="bindingFlags">A combination of Flags that define the scope of the search</param>
		/// <returns>The value of the property or null if no property was found</returns>
        public static object TryGetPropertyValue( this object obj, string name, Flags bindingFlags )
        {
            try
            {
                return obj.GetPropertyValue( name, bindingFlags );
            }
            catch( MissingMemberException )
            {
                return null;
            }
        }
        #endregion

        #region TrySetValue
		/// <summary>
        /// Sets the first (public or non-public) instance property with the given <paramref name="name"/> on the 
        /// given <paramref name="obj"/> object to the supplied <paramref name="value"/>. Returns true 
        /// if a value was assigned to a property and false otherwise.
		/// </summary>
		/// <param name="obj">The source object on which to find the property</param>
		/// <param name="name">The name of the property whose value should be retrieved</param>
		/// <param name="value">The value that should be assigned to the property</param>
		/// <returns>True if the value was assigned to a property and false otherwise</returns>
        public static bool TrySetPropertyValue( this object obj, string name, object value )
        {
            return TrySetPropertyValue( obj, name, value, Flags.InstanceAnyVisibility );
        }

		/// <summary>
        /// Sets the first property with the given <paramref name="name"/> on the given <paramref name="obj"/> object
        /// to the supplied <paramref name="value"/>. Returns true if a value was assigned to a property and false otherwise.
        /// Use the <paramref name="bindingFlags"/> parameter to limit the scope of the search.
		/// </summary>
		/// <param name="obj">The source object on which to find the property</param>
		/// <param name="name">The name of the property whose value should be retrieved</param>
		/// <param name="value">The value that should be assigned to the property</param>
		/// <param name="bindingFlags">A combination of Flags that define the scope of the search</param>
		/// <returns>True if the value was assigned to a property and false otherwise</returns>
        public static bool TrySetPropertyValue( this object obj, string name, object value, Flags bindingFlags )
        {
            try
            {
                obj.SetPropertyValue( name, value, bindingFlags );
                return true;
            }
            catch (MissingMemberException)
            {
                return false;
            }
        }
        #endregion

        #endregion
    }
}