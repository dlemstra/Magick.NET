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
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Fasterflect.Emitter
{
    /// <summary>
    /// Stores all necessary information to construct a dynamic method.
    /// </summary>
    [DebuggerStepThrough]
    internal class CallInfo
    {
        public Type TargetType { get; private set; }
        public Flags BindingFlags { get; internal set; }
        public MemberTypes MemberTypes { get; set; }
        public Type[] ParamTypes { get; internal set; }
        public Type[] GenericTypes { get; private set; }
        public string Name { get; private set; }
        public bool IsReadOperation { get; set; }
        public bool IsStatic { get; internal set; }
		
        // This field doesn't constitute CallInfo identity:
        public MemberInfo MemberInfo { get; internal set; }

        public CallInfo(Type targetType, Type[] genericTypes, Flags bindingFlags, MemberTypes memberTypes, string name,
                        Type[] parameterTypes, MemberInfo memberInfo, bool isReadOperation )
        {
            TargetType = targetType;
            GenericTypes = genericTypes == null || genericTypes.Length == 0
                             ? Type.EmptyTypes
                             : genericTypes;
            BindingFlags = bindingFlags;
            MemberTypes = memberTypes;
            Name = name;
            ParamTypes = parameterTypes == null || parameterTypes.Length == 0
                             ? Type.EmptyTypes
                             : parameterTypes;
            MemberInfo = memberInfo;
        	IsReadOperation = isReadOperation;
        	IsStatic = BindingFlags.IsSet( Flags.Static );
        }

        /// <summary>
        /// The CIL should handle inner struct only when the target type is 
        /// a value type or the wrapper ValueTypeHolder type.  In addition, the call 
        /// must also be executed in the non-static context since static 
        /// context doesn't need to handle inner struct case.
        /// </summary>
        public bool ShouldHandleInnerStruct
        {
            get { return IsTargetTypeStruct && !IsStatic; }
        }

        public bool IsTargetTypeStruct
        {
            get { return TargetType.IsValueType; }
        }

        public bool HasNoParam
        {
            get { return ParamTypes == Type.EmptyTypes; }
        }

        public bool IsGeneric
        {
            get { return GenericTypes != Type.EmptyTypes; }
        }

        public bool HasRefParam
        {
            get { return ParamTypes.Any( t => t.IsByRef ); }
        }

        /// <summary>
        /// Two <c>CallInfo</c> instances are considered equaled if the following properties
        /// are equaled: <c>TargetType</c>, <c>Flags</c>, <c>IsStatic</c>, <c>MemberTypes</c>, <c>Name</c>,
        /// <c>ParamTypes</c> and <c>GenericTypes</c>.
        /// </summary>
        public override bool Equals( object obj )
        {
            var other = obj as CallInfo;
            if( other == null )
            {
                return false;
            }
            if( other == this )
            {
                return true;
            }

            if( other.TargetType != TargetType ||
                other.Name != Name ||
                other.MemberTypes != MemberTypes ||
                other.BindingFlags != BindingFlags ||
				other.IsReadOperation != IsReadOperation ||
                other.ParamTypes.Length != ParamTypes.Length ||
                other.GenericTypes.Length != GenericTypes.Length)
            {
                return false;
            }

            for( int i = 0; i < ParamTypes.Length; i++ )
            {
                if( ParamTypes[ i ] != other.ParamTypes[ i ] )
                {
                    return false;
                }
            }

            for (int i = 0; i < GenericTypes.Length; i++)
            {
                if (GenericTypes[i] != other.GenericTypes[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
        	int hash = TargetType.GetHashCode() + (int) MemberTypes * Name.GetHashCode() + BindingFlags.GetHashCode() + IsReadOperation.GetHashCode();
        	for( int i = 0; i < ParamTypes.Length; i++ )
        	{
        	    hash += ParamTypes[ i ].GetHashCode() * (i+1);
        	}
        	for (int i = 0; i < GenericTypes.Length; i++)
        	{
        	    hash += GenericTypes[i].GetHashCode() * (i+1);
        	}
        	return hash;
        }
    }
}