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
    /// Extension methods for locating, inspecting and invoking methods.
    /// </summary>
    public static class MethodExtensions
    {
        #region Method Invocation
        /// <summary>
        /// Invokes the method specified by <paramref name="name"/> on the given <paramref name="obj"/> 
        /// using <paramref name="parameters"/> as arguments. 
        /// Leave <paramref name="parameters"/> empty if the method has no arguments.
        /// </summary>
        /// <returns>The return value of the method.</returns>
        /// <remarks>If the method has no return type, <c>null</c> is returned.</remarks>
        /// <remarks>
        /// All elements of <paramref name="parameters"/> must not be <c>null</c>.  Otherwise, 
        /// <see cref="NullReferenceException"/> is thrown.  If you are not sure as to whether
        /// any element is <c>null</c> or not, use the overload that accepts <c>paramTypes</c> array.
        /// </remarks>
        /// <seealso cref="CallMethod(object,string,System.Type[],object[])"/>
        public static object CallMethod( this object obj, string name, params object[] parameters )
        {
            return DelegateForCallMethod( obj.GetTypeAdjusted(), name, parameters.ToTypeArray() )( obj, parameters );
        }

        /// <summary>
        /// Invoke generic method.  See the overload with same parameters except for <paramref name="genericTypes"/>.
        /// </summary>
        /// <seealso cref="CallMethod(object,string,object[])"/>
        public static object CallMethod(this object obj, Type[] genericTypes, string name, params object[] parameters)
        {
            return DelegateForCallMethod(obj.GetTypeAdjusted(), genericTypes, name, parameters.ToTypeArray())(obj, parameters);
        }


        /// <summary>
        /// Invokes the method specified by <paramref name="name"/> on the given <paramref name="obj"/> 
        /// using <paramref name="parameters"/> as arguments.
        /// Method parameter types are specified by <paramref name="parameterTypes"/>.
        /// </summary>
        /// <returns>The return value of the method.</returns>
        /// <remarks>If the method has no return type, <c>null</c> is returned.</remarks>
        public static object CallMethod( this object obj, string name, Type[] parameterTypes, params object[] parameters )
        {
            return DelegateForCallMethod( obj.GetTypeAdjusted(), name, parameterTypes )( obj, parameters );
        }

        /// <summary>
        /// Invoke generic method.  See the overload with same parameters except for <paramref name="genericTypes"/>.
        /// </summary>
        /// <seealso cref="CallMethod(object,string,Type[],object[])"/>
        public static object CallMethod(this object obj, Type[] genericTypes, string name, Type[] parameterTypes, params object[] parameters)
        {
            return DelegateForCallMethod(obj.GetTypeAdjusted(), genericTypes, name, parameterTypes)(obj, parameters);
        }

        /// <summary>
        /// Invokes the method specified by <paramref name="name"/> on the given <paramref name="obj"/>
        /// matching <paramref name="bindingFlags"/> using <paramref name="parameters"/> as arguments.
        /// Leave <paramref name="parameters"/> empty if the method has no argument.
        /// </summary>
        /// <returns>The return value of the method.</returns>
        /// <remarks>If the method has no return type, <c>null</c> is returned.</remarks>
        /// <remarks>
        /// All elements of <paramref name="parameters"/> must not be <c>null</c>.  Otherwise, 
        /// <see cref="NullReferenceException"/> is thrown.  If you are not sure as to whether
        /// any element is <c>null</c> or not, use the overload that accepts <c>paramTypes</c> array.
        /// </remarks>
        /// <seealso cref="CallMethod(object,string,System.Type[],Fasterflect.Flags,object[])"/>
        public static object CallMethod( this object obj, string name, Flags bindingFlags, params object[] parameters )
        {
            return DelegateForCallMethod( obj.GetTypeAdjusted(), name, bindingFlags, parameters.ToTypeArray() )( obj, parameters );
        }

        /// <summary>
        /// Invoke generic method.  See the overload with same parameters except for <paramref name="genericTypes"/>.
        /// </summary>
        /// <seealso cref="CallMethod(object,string,Flags,object[])"/>
        public static object CallMethod(this object obj, Type[] genericTypes, string name, Flags bindingFlags, params object[] parameters)
        {
            return DelegateForCallMethod(obj.GetTypeAdjusted(), genericTypes, name, bindingFlags)(obj, parameters);
        }

        /// <summary>
        /// Invokes a method specified by <paramref name="name"/> on the given <paramref name="obj"/> 
        /// matching <paramref name="bindingFlags"/> using <paramref name="parameters"/> as arguments.
        /// Method parameter types are specified by <paramref name="parameterTypes"/>.
        /// </summary>
        /// <returns>The return value of the method.</returns>
        /// <remarks>If the method has no return type, <c>null</c> is returned.</remarks>
        public static object CallMethod(this object obj, string name, Type[] parameterTypes, Flags bindingFlags, params object[] parameters)
        {
            return DelegateForCallMethod(obj.GetTypeAdjusted(), name, bindingFlags, parameterTypes)(obj, parameters);
        }

        /// <summary>
        /// Invoke generic method.  See the overload with same parameters except for <paramref name="genericTypes"/>.
        /// </summary>
        /// <seealso cref="CallMethod(object,string,Type[],Flags,object[])"/>
        public static object CallMethod(this object obj, Type[] genericTypes, string name, Type[] parameterTypes, Flags bindingFlags, params object[] parameters)
        {
            return DelegateForCallMethod(obj.GetTypeAdjusted(), genericTypes, name, bindingFlags, parameterTypes)(obj, parameters);
        }

        /// <summary>
        /// Creates a delegate which can invoke the method <paramref name="name"/> with arguments matching
        /// <paramref name="parameterTypes"/> on the given <paramref name="type"/>.
        /// Leave <paramref name="parameterTypes"/> empty if the method has no arguments.
        /// </summary>
        public static MethodInvoker DelegateForCallMethod( this Type type, string name, params Type[] parameterTypes )
        {
            return DelegateForCallMethod( type, name, Flags.StaticInstanceAnyVisibility, parameterTypes );
        }

        /// <summary>
        /// Create a delegate to invoke a generic method.  See the overload with same parameters except for <paramref name="genericTypes"/>.
        /// </summary>
        /// <seealso cref="DelegateForCallMethod(Type,string,Type[])"/>
        public static MethodInvoker DelegateForCallMethod(this Type type, Type[] genericTypes, string name, params Type[] parameterTypes)
        {
            return DelegateForCallMethod(type, genericTypes, name, Flags.StaticInstanceAnyVisibility, parameterTypes);
        }

        /// <summary>
        /// Creates a delegate which can invoke the method <paramref name="name"/> with arguments matching
        /// <paramref name="parameterTypes"/> and matching <paramref name="bindingFlags"/> on the given <paramref name="type"/>.
        /// Leave <paramref name="parameterTypes"/> empty if the method has no arguments.
        /// </summary>
        public static MethodInvoker DelegateForCallMethod( this Type type, string name, Flags bindingFlags, params Type[] parameterTypes )
        {
            return DelegateForCallMethod(type, null, name, bindingFlags, parameterTypes);
        }

        /// <summary>
        /// Create a delegate to invoke a generic method.  See the overload with same parameters except for <paramref name="genericTypes"/>.
        /// </summary>
        /// <seealso cref="DelegateForCallMethod(Type,string,Flags,Type[])"/>
        public static MethodInvoker DelegateForCallMethod(this Type type, Type[] genericTypes, string name, Flags bindingFlags, params Type[] parameterTypes)
        {
            var callInfo = new CallInfo(type, genericTypes, bindingFlags, MemberTypes.Method, name, parameterTypes, null, true);
            return (MethodInvoker)new MethodInvocationEmitter(callInfo).GetDelegate();
        }
		#endregion

        #region Method Lookup (Single)
        /// <summary>
        /// Gets the public or non-public instance method with the given <paramref name="name"/> on the
        /// given <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="name">The name of the method to search for. This argument must be supplied. The 
        /// default behavior is to check for an exact, case-sensitive match. Pass <see href="Flags.ExplicitNameMatch"/> 
        /// to include explicitly implemented interface members, <see href="Flags.PartialNameMatch"/> to locate
        /// by substring, and <see href="Flags.IgnoreCase"/> to ignore case.</param>
        /// <returns>The specified method or null if no method was found. If there are multiple matches
        /// due to method overloading the first found match will be returned.</returns>
        public static MethodInfo Method( this Type type, string name )
        {
            return type.Method( name, null, Flags.InstanceAnyVisibility );
        }

        /// <summary>
        /// Gets a generic method.  See the overload with same arguments exception for <paramref name="genericTypes"/>.
        /// </summary>
        /// <seealso cref="Method(Type,string)"/>
        public static MethodInfo Method(this Type type, Type[] genericTypes, string name)
        {
            return type.Method(genericTypes, name, Flags.InstanceAnyVisibility);
        }

        /// <summary>
        /// Gets the public or non-public instance method with the given <paramref name="name"/> on the 
        /// given <paramref name="type"/> where the parameter types correspond in order with the
        /// supplied <paramref name="parameterTypes"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="name">The name of the method to search for. This argument must be supplied. The 
        /// default behavior is to check for an exact, case-sensitive match.</param>
        /// <param name="parameterTypes">If this parameter is not null then only methods with the same 
        /// parameter signature will be included in the result.</param>
        /// <returns>The specified method or null if no method was found. If there are multiple matches
        /// due to method overloading the first found match will be returned.</returns>
        public static MethodInfo Method( this Type type, string name, Type[] parameterTypes )
        {
        	return type.Method( name, parameterTypes, Flags.InstanceAnyVisibility );
        }

        /// <summary>
        /// Gets a generic method.  See the overload with same arguments exception for <paramref name="genericTypes"/>.
        /// </summary>
        /// <seealso cref="Method(Type,string,Type[])"/>
        public static MethodInfo Method(this Type type, Type[] genericTypes, string name, Type[] parameterTypes)
        {
            return type.Method(genericTypes, name, parameterTypes, Flags.InstanceAnyVisibility);
        }

    	/// <summary>
        /// Gets the method with the given <paramref name="name"/> and matching <paramref name="bindingFlags"/>
        /// on the given <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="name">The name of the method to search for. This argument must be supplied. The 
        /// default behavior is to check for an exact, case-sensitive match. Pass <see href="Flags.ExplicitNameMatch"/> 
        /// to include explicitly implemented interface members, <see href="Flags.PartialNameMatch"/> to locate
        /// by substring, and <see href="Flags.IgnoreCase"/> to ignore case.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination used to define
        /// the search behavior and result filtering.</param>
        /// <returns>The specified method or null if no method was found. If there are multiple matches
        /// due to method overloading the first found match will be returned.</returns>
        public static MethodInfo Method( this Type type, string name, Flags bindingFlags )
        {
            return type.Method( name, null, bindingFlags );
        }

        /// <summary>
        /// Gets a generic method.  See the overload with same arguments exception for <paramref name="genericTypes"/>.
        /// </summary>
        /// <seealso cref="Method(Type,string,Flags)"/>
        public static MethodInfo Method(this Type type, Type[] genericTypes, string name, Flags bindingFlags)
        {
            return type.Method(genericTypes, name, null, bindingFlags);
        }

        /// <summary>
        /// Gets the method with the given <paramref name="name"/> and matching <paramref name="bindingFlags"/>
        /// on the given <paramref name="type"/> where the parameter types correspond in order with the
        /// supplied <paramref name="parameterTypes"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="name">The name of the method to search for. This argument must be supplied. The 
        ///   default behavior is to check for an exact, case-sensitive match. Pass <see href="Flags.ExplicitNameMatch"/> 
        ///   to include explicitly implemented interface members, <see href="Flags.PartialNameMatch"/> to locate
        ///   by substring, and <see href="Flags.IgnoreCase"/> to ignore case.</param>
        /// <param name="parameterTypes">If this parameter is supplied then only methods with the same parameter signature
        ///   will be included in the result. The default behavior is to check only for assignment compatibility,
        ///   but this can be changed to exact matching by passing <see href="Flags.ExactBinding"/>.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination used to define
        ///   the search behavior and result filtering.</param>
        /// <returns>The specified method or null if no method was found. If there are multiple matches
        /// due to method overloading the first found match will be returned.</returns>
        public static MethodInfo Method( this Type type, string name, Type[] parameterTypes, Flags bindingFlags )
        {
            return type.Method(null, name, parameterTypes, bindingFlags);
        }

        /// <summary>
        /// Gets the method with the given <paramref name="name"/> and matching <paramref name="bindingFlags"/>
        /// on the given <paramref name="type"/> where the parameter types correspond in order with the
        /// supplied <paramref name="parameterTypes"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="genericTypes">Type parameters if this is a generic method.</param>
        /// <param name="name">The name of the method to search for. This argument must be supplied. The 
        ///   default behavior is to check for an exact, case-sensitive match. Pass <see href="Flags.ExplicitNameMatch"/> 
        ///   to include explicitly implemented interface members, <see href="Flags.PartialNameMatch"/> to locate
        ///   by substring, and <see href="Flags.IgnoreCase"/> to ignore case.</param>
        /// <param name="parameterTypes">If this parameter is supplied then only methods with the same parameter signature
        ///   will be included in the result. The default behavior is to check only for assignment compatibility,
        ///   but this can be changed to exact matching by passing <see href="Flags.ExactBinding"/>.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination used to define
        ///   the search behavior and result filtering.</param>
        /// <returns>The specified method or null if no method was found. If there are multiple matches
        /// due to method overloading the first found match will be returned.</returns>
        public static MethodInfo Method( this Type type, Type[] genericTypes, string name, Type[] parameterTypes, Flags bindingFlags )
        {
			bool hasTypes = parameterTypes != null;
        	bool hasGenericTypes = genericTypes != null && genericTypes.Length > 0;
            // we need to check all methods to do partial name matches or complex parameter binding
        	bool processAll = bindingFlags.IsAnySet( Flags.PartialNameMatch | Flags.TrimExplicitlyImplemented );
        	processAll |= hasTypes && bindingFlags.IsSet( Flags.IgnoreParameterModifiers );
        	processAll |= hasGenericTypes;
            if( processAll )
            {
                return type.Methods( genericTypes, parameterTypes, bindingFlags, name ).FirstOrDefault().MakeGeneric( genericTypes );
            }

            var result = hasTypes ? type.GetMethod( name, bindingFlags, null, parameterTypes, null )
                             	  : type.GetMethod( name, bindingFlags );
            if( result == null && bindingFlags.IsNotSet( Flags.DeclaredOnly ) )
            {
                if( type.BaseType != typeof(object) && type.BaseType != null )
                {
                    return type.BaseType.Method( name, parameterTypes, bindingFlags ).MakeGeneric( genericTypes );
                }
            }
        	bool hasSpecialFlags = bindingFlags.IsAnySet( Flags.ExcludeBackingMembers | Flags.ExcludeExplicitlyImplemented | Flags.ExcludeHiddenMembers );
            if( hasSpecialFlags )
            {
                var methods = new List<MethodInfo> { result }.Filter( bindingFlags );
                return (methods.Count > 0 ? methods[ 0 ] : null).MakeGeneric( genericTypes );
            }
            return result.MakeGeneric(genericTypes);
        }

        internal static MethodInfo MakeGeneric(this MethodInfo methodInfo, Type[] genericTypes)
        {
            if (methodInfo == null)
                return null;
            if (genericTypes == null ||
                genericTypes.Length == 0 ||
                genericTypes == Type.EmptyTypes)
                return methodInfo;
            return methodInfo.MakeGenericMethod( genericTypes );
        }
        #endregion

        #region Method Lookup (Multiple)
        /// <summary>
        /// Gets all public and non-public instance methods on the given <paramref name="type"/> that match the 
        /// given <paramref name="names"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="names">The optional list of names against which to filter the result. If this parameter is
		/// <c>null</c> or empty no name filtering will be applied. The default behavior is to check for an exact, 
		/// case-sensitive match. Pass <see href="Flags.ExcludeExplicitlyImplemented"/> to exclude explicitly implemented 
		/// interface members, <see href="Flags.PartialNameMatch"/> to locate by substring, and 
		/// <see href="Flags.IgnoreCase"/> to ignore case.</param>
        /// <returns>A list of all matching methods. This value will never be null.</returns>
        public static IList<MethodInfo> Methods( this Type type, params string[] names )
        {
            return type.Methods( null, Flags.InstanceAnyVisibility, names );
        }

        /// <summary>
        /// Gets all public and non-public instance methods on the given <paramref name="type"/> that match the 
        /// given <paramref name="names"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination used to define
        /// the search behavior and result filtering.</param>
        /// <param name="names">The optional list of names against which to filter the result. If this parameter is
		/// <c>null</c> or empty no name filtering will be applied. The default behavior is to check for an exact, 
		/// case-sensitive match. Pass <see href="Flags.ExcludeExplicitlyImplemented"/> to exclude explicitly implemented 
		/// interface members, <see href="Flags.PartialNameMatch"/> to locate by substring, and 
		/// <see href="Flags.IgnoreCase"/> to ignore case.</param>
        /// <returns>A list of all matching methods. This value will never be null.</returns>
        public static IList<MethodInfo> Methods( this Type type, Flags bindingFlags, params string[] names )
        {
            return type.Methods( null, bindingFlags, names );
        }


        /// <summary>
        /// Gets all public and non-public instance methods on the given <paramref name="type"/> that match the given 
        ///  <paramref name="names"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="parameterTypes">If this parameter is supplied then only methods with the same parameter 
        /// signature will be included in the result.</param>
        /// <param name="names">The optional list of names against which to filter the result. If this parameter is
		/// <c>null</c> or empty no name filtering will be applied. The default behavior is to check for an exact, 
		/// case-sensitive match.</param>
        /// <returns>A list of all matching methods. This value will never be null.</returns>
        public static IList<MethodInfo> Methods( this Type type, Type[] parameterTypes, params string[] names )
        {
        	return type.Methods( parameterTypes, Flags.InstanceAnyVisibility, names );
        }

    	/// <summary>
        /// Gets all methods on the given <paramref name="type"/> that match the given lookup criteria.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="parameterTypes">If this parameter is supplied then only methods with the same parameter signature
        /// will be included in the result. The default behavior is to check only for assignment compatibility,
        /// but this can be changed to exact matching by passing <see href="Flags.ExactBinding"/>.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination used to define
        /// the search behavior and result filtering.</param>
        /// <param name="names">The optional list of names against which to filter the result. If this parameter is
		/// <c>null</c> or empty no name filtering will be applied. The default behavior is to check for an exact, 
		/// case-sensitive match. Pass <see href="Flags.ExcludeExplicitlyImplemented"/> to exclude explicitly implemented 
		/// interface members, <see href="Flags.PartialNameMatch"/> to locate by substring, and 
		/// <see href="Flags.IgnoreCase"/> to ignore case.</param>
        /// <returns>A list of all matching methods. This value will never be null.</returns>
        public static IList<MethodInfo> Methods( this Type type, Type[] parameterTypes, Flags bindingFlags, params string[] names )
    	{
    		return type.Methods( null, parameterTypes, bindingFlags, names );
    	}


    	/// <summary>
        /// Gets all methods on the given <paramref name="type"/> that match the given lookup criteria.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="genericTypes">If this parameter is supplied then only methods with the same generic parameter 
        /// signature will be included in the result. The default behavior is to check only for assignment compatibility,
        /// but this can be changed to exact matching by passing <see href="Flags.ExactBinding"/>.</param>
        /// <param name="parameterTypes">If this parameter is supplied then only methods with the same parameter signature
        /// will be included in the result. The default behavior is to check only for assignment compatibility,
        /// but this can be changed to exact matching by passing <see href="Flags.ExactBinding"/>.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination used to define
        /// the search behavior and result filtering.</param>
        /// <param name="names">The optional list of names against which to filter the result. If this parameter is
		/// <c>null</c> or empty no name filtering will be applied. The default behavior is to check for an exact, 
		/// case-sensitive match. Pass <see href="Flags.ExcludeExplicitlyImplemented"/> to exclude explicitly implemented 
		/// interface members, <see href="Flags.PartialNameMatch"/> to locate by substring, and 
		/// <see href="Flags.IgnoreCase"/> to ignore case.</param>
        /// <returns>A list of all matching methods. This value will never be null.</returns>
     	public static IList<MethodInfo> Methods( this Type type, Type[] genericTypes, Type[] parameterTypes, Flags bindingFlags, params string[] names )
        {
            if( type == null || type == typeof(object) )
            {
                return new MethodInfo[0];
            }
			bool recurse = bindingFlags.IsNotSet( Flags.DeclaredOnly );
            bool hasNames = names != null && names.Length > 0;
            bool hasTypes = parameterTypes != null;
            bool hasGenericTypes = genericTypes != null && genericTypes.Length > 0;
            bool hasSpecialFlags = bindingFlags.IsAnySet( Flags.ExcludeBackingMembers | Flags.ExcludeExplicitlyImplemented | Flags.ExcludeHiddenMembers );

            if( ! recurse && ! hasNames && ! hasTypes && ! hasSpecialFlags )
            {
                return type.GetMethods( bindingFlags ) ?? new MethodInfo[0];
            }

            var methods = GetMethods( type, bindingFlags );
            methods = hasNames ? methods.Filter(bindingFlags, names) : methods;
            methods = hasGenericTypes ? methods.Filter(genericTypes) : methods;
            methods = hasTypes ? methods.Filter( bindingFlags, parameterTypes ) : methods;
            methods = hasSpecialFlags ? methods.Filter( bindingFlags ) : methods;
            return methods;
        }

        private static IList<MethodInfo> GetMethods( Type type, Flags bindingFlags )
        {
            bool recurse = bindingFlags.IsNotSet( Flags.DeclaredOnly );

            if( ! recurse )
            {
                return type.GetMethods( bindingFlags ) ?? new MethodInfo[0];
            }

            bindingFlags |= Flags.DeclaredOnly;
            bindingFlags &= ~BindingFlags.FlattenHierarchy;

            var methods = new List<MethodInfo>();
            methods.AddRange( type.GetMethods( bindingFlags ) );
            Type baseType = type.BaseType;
            while( baseType != null && baseType != typeof(object) )
            {
                methods.AddRange( baseType.GetMethods( bindingFlags ) );
                baseType = baseType.BaseType;
            }
            return methods;
        }
        #endregion
    }
}