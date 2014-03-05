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
using System.Reflection;
using Fasterflect.Emitter;

namespace Fasterflect
{
    /// <summary>
    /// Extension methods for inspecting, invoking and working with constructors.
    /// </summary>
    public static class ConstructorInfoExtensions
    {
        /// <summary>
        /// Invokes the constructor <paramref name="ctorInfo"/> with <paramref name="parameters"/> as arguments.
        /// Leave <paramref name="parameters"/> empty if the constructor has no argument.
        /// </summary>
        public static object CreateInstance( this ConstructorInfo ctorInfo, params object[] parameters )
        {
            return ctorInfo.DelegateForCreateInstance()( parameters );
        }

        /// <summary>
        /// Creates a delegate which can create instance based on the constructor <paramref name="ctorInfo"/>.
        /// </summary>
        public static ConstructorInvoker DelegateForCreateInstance( this ConstructorInfo ctorInfo )
        {
            return (ConstructorInvoker) new CtorInvocationEmitter( ctorInfo, Flags.InstanceAnyVisibility ).GetDelegate();
        }
    }
}