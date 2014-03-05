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
    /// Extension methods for inspecting and working with fields.
    /// </summary>
    public static class FieldInfoExtensions
    {
        /// <summary>
        /// Sets the static field identified by <paramref name="fieldInfo"/> to the specified <paramref name="value" />.
        /// </summary>
        public static void Set( this FieldInfo fieldInfo, object value )
        {
            fieldInfo.DelegateForSetFieldValue()( null, value );
        }

        /// <summary>
        /// Sets the instance field identified by <paramref name="fieldInfo"/> on the given <paramref name="obj"/>
        /// to the specified <paramref name="value" />.
        /// </summary>
        public static void Set( this FieldInfo fieldInfo, object obj, object value )
        {
            fieldInfo.DelegateForSetFieldValue()( obj, value );
        }

        /// <summary>
        /// Gets the value of the static field identified by <paramref name="fieldInfo"/>.
        /// </summary>
        public static object Get( this FieldInfo fieldInfo )
        {
            return fieldInfo.DelegateForGetFieldValue()( null );
        }

        /// <summary>
        /// Gets the value of the instance field identified by <paramref name="fieldInfo"/> on the given <paramref name="obj"/>.
        /// </summary>
        public static object Get( this FieldInfo fieldInfo, object obj )
        {
            return fieldInfo.DelegateForGetFieldValue()( obj );
        }

        /// <summary>
        /// Creates a delegate which can set the value of the field identified by <paramref name="fieldInfo"/>.
        /// </summary>
        public static MemberSetter DelegateForSetFieldValue( this FieldInfo fieldInfo )
        {
        	var flags = fieldInfo.IsStatic ? Flags.StaticAnyVisibility : Flags.InstanceAnyVisibility;
            return (MemberSetter) new MemberSetEmitter( fieldInfo, flags ).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can get the value of the field identified by <paramref name="fieldInfo"/>.
        /// </summary>
        public static MemberGetter DelegateForGetFieldValue( this FieldInfo fieldInfo )
        {
        	var flags = fieldInfo.IsStatic ? Flags.StaticAnyVisibility : Flags.InstanceAnyVisibility;
            return (MemberGetter) new MemberGetEmitter( fieldInfo, flags ).GetDelegate();
        }
    }
}