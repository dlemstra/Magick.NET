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
    /// Extension methods for inspecting, invoking and working with methods.
    /// </summary>
    public static class MethodInfoExtensions
    {
        #region Access
        /// <summary>
        /// Invokes the static method identified by <paramref name="methodInfo"/> with <paramref name="parameters"/>
        /// as arguments.  Leave <paramref name="parameters"/> empty if the method has no argument.
        /// </summary>
        /// <returns>The return value of the method.</returns>
        /// <remarks>If the method has no return type, <c>null</c> is returned.</remarks>
        public static object Call( this MethodInfo methodInfo, params object[] parameters )
        {
            return methodInfo.DelegateForCallMethod()( null, parameters );
        }

        /// <summary>
        /// Invokes the instance method identified by <paramref name="methodInfo"/> on the object
        /// <paramref name="obj"/> with <paramref name="parameters"/> as arguments.
        /// Leave <paramref name="parameters"/> empty if the method has no argument.
        /// </summary>
        /// <returns>The return value of the method.</returns>
        /// <remarks>If the method has no return type, <c>null</c> is returned.</remarks>
        public static object Call( this MethodInfo methodInfo, object obj, params object[] parameters )
        {
            return methodInfo.DelegateForCallMethod()( obj, parameters );
        }

        /// <summary>
        /// Creates a delegate which can invoke the instance method identified by <paramref name="methodInfo"/>.
        /// </summary>
        public static MethodInvoker DelegateForCallMethod( this MethodInfo methodInfo )
        {
		    var flags = methodInfo.IsStatic ? Flags.StaticAnyVisibility : Flags.InstanceAnyVisibility;
            return (MethodInvoker) new MethodInvocationEmitter( methodInfo, flags ).GetDelegate();
        }
        #endregion

        #region Method Parameter Lookup
        /// <summary>
        /// Gets all parameters for the given <paramref name="method"/>.
        /// </summary>
        /// <returns>The list of parameters for the method. This value will never be null.</returns>
        public static IList<ParameterInfo> Parameters( this MethodBase method )
        {
            return method.GetParameters();
        }
        #endregion

        #region Method Signature Comparer
        /// <summary>
        /// Compares the signature of the method with the given parameter types and returns true if
        /// all method parameters have the same order and type. Parameter names are not considered.
        /// </summary>
        /// <returns>True if the supplied parameter type array matches the method parameters array, false otherwise.</returns>
        public static bool HasParameterSignature( this MethodBase method, Type[] parameters )
        {
            return method.GetParameters().Select( p => p.ParameterType ).SequenceEqual( parameters );
        }

        /// <summary>
        /// Compares the signature of the method with the given parameter types and returns true if
        /// all method parameters have the same order and type. Parameter names are not considered.
        /// </summary>
        /// <returns>True if the supplied parameter type array matches the method parameters array, false otherwise.</returns>
        public static bool HasParameterSignature( this MethodBase method, ParameterInfo[] parameters )
        {
            return method.GetParameters().Select( p => p.ParameterType ).SequenceEqual( parameters.Select( p => p.ParameterType ) );
        }
		#endregion
    }
}