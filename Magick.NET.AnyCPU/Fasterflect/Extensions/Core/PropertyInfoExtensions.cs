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

using System.Reflection;
using Fasterflect.Emitter;

namespace Fasterflect
{
	/// <summary>
    /// Extension methods for inspecting and working with properties.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Sets the static property identified by <paramref name="propInfo"/> to the specified <paramref name="value" />.
        /// </summary>
        public static void Set( this PropertyInfo propInfo, object value )
        {
            propInfo.DelegateForSetPropertyValue( Flags.StaticAnyVisibility )( null, value );
        }

        /// <summary>
        /// Sets the instance property identified by <paramref name="propInfo"/> on the given <paramref name="obj"/>
        /// to the specified <paramref name="value" />.
        /// </summary>
        public static void Set( this PropertyInfo propInfo, object obj, object value )
        {
            propInfo.DelegateForSetPropertyValue( Flags.InstanceAnyVisibility )( obj, value );
        }

        /// <summary>
        /// Gets the value of the static property identified by <paramref name="propInfo"/>.
        /// </summary>
        public static object Get( this PropertyInfo propInfo )
        {
            return propInfo.DelegateForGetPropertyValue( Flags.StaticAnyVisibility )( null );
        }

        /// <summary>
        /// Gets the value of the instance property identified by <paramref name="propInfo"/> on the given <paramref name="obj"/>.
        /// </summary>
        public static object Get( this PropertyInfo propInfo, object obj )
        {
            return propInfo.DelegateForGetPropertyValue( Flags.InstanceAnyVisibility )( obj );
        }

        /// <summary>
        /// Creates a delegate which can set the value of the property <paramref name="propInfo"/>.
        /// </summary>
		public static MemberSetter DelegateForSetPropertyValue( this PropertyInfo propInfo )
		{
		    var flags = propInfo.IsStatic() ? Flags.StaticAnyVisibility : Flags.InstanceAnyVisibility;
		    return (MemberSetter) new MemberSetEmitter( propInfo, flags ).GetDelegate();
		}

		/// <summary>
        /// Creates a delegate which can set the value of the property <param name="propInfo"/> matching the
        /// specified <param name="bindingFlags" />.
        /// </summary>
        public static MemberSetter DelegateForSetPropertyValue( this PropertyInfo propInfo, Flags bindingFlags )
        {
            return (MemberSetter) new MemberSetEmitter( propInfo, bindingFlags ).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can get the value of the property <param name="propInfo"/>.
        /// </summary>
		public static MemberGetter DelegateForGetPropertyValue( this PropertyInfo propInfo )
		{
		    var flags = propInfo.IsStatic() ? Flags.StaticAnyVisibility : Flags.InstanceAnyVisibility;
		    return (MemberGetter) new MemberGetEmitter( propInfo, flags ).GetDelegate();
		}

        /// <summary>
        /// Creates a delegate which can get the value of the property <param name="propInfo"/> matching the
        /// specified <param name="bindingFlags" />.
        /// </summary>
        public static MemberGetter DelegateForGetPropertyValue( this PropertyInfo propInfo, Flags bindingFlags )
        {
            return (MemberGetter) new MemberGetEmitter( propInfo, bindingFlags ).GetDelegate();
        }
    }
}