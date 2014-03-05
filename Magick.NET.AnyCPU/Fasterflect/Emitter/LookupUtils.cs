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

namespace Fasterflect.Emitter
{
    internal class LookupUtils
    {
        public static ConstructorInfo GetConstructor(CallInfo callInfo)
        {
            var constructor = callInfo.MemberInfo as ConstructorInfo;
            if (constructor != null)
                return constructor;

            constructor = callInfo.TargetType.Constructor( callInfo.BindingFlags, callInfo.ParamTypes );
            if (constructor == null)
                throw new MissingMemberException("Constructor does not exist");
			callInfo.MemberInfo = constructor;
        	callInfo.ParamTypes = constructor.GetParameters().ToTypeArray();
            return constructor;
        }

        public static MethodInfo GetMethod(CallInfo callInfo)
        {
            var method = callInfo.MemberInfo as MethodInfo;
            if (method != null)
                return method;
            method = callInfo.TargetType.Method(callInfo.GenericTypes, callInfo.Name, callInfo.ParamTypes, callInfo.BindingFlags);
            if (method == null)
			{
				const string fmt = "No match for method with name {0} and flags {1} on type {2}.";
				throw new MissingMethodException( string.Format( fmt, callInfo.Name, callInfo.BindingFlags, callInfo.TargetType ) );
			}
			callInfo.MemberInfo = method;
        	callInfo.ParamTypes = method.GetParameters().ToTypeArray();
            return method;
        }

        public static MemberInfo GetMember(CallInfo callInfo)
        {
            var member = callInfo.MemberInfo;
            if (member != null)
                return member;

            if (callInfo.MemberTypes == MemberTypes.Property)
            {
                member = callInfo.TargetType.Property(callInfo.Name, callInfo.BindingFlags);
                if (member == null)
				{
					const string fmt = "No match for property with name {0} and flags {1} on type {2}.";
					throw new MissingMemberException( string.Format( fmt, callInfo.Name, callInfo.BindingFlags, callInfo.TargetType ) );
				}
				callInfo.MemberInfo = member;
				return member;
            }
            if (callInfo.MemberTypes == MemberTypes.Field)
            {
                member = callInfo.TargetType.Field(callInfo.Name, callInfo.BindingFlags);
                if (member == null)
				{
					const string fmt = "No match for field with name {0} and flags {1} on type {2}.";
					throw new MissingFieldException( string.Format( fmt, callInfo.Name, callInfo.BindingFlags, callInfo.TargetType ) );
				}
				callInfo.MemberInfo = member;
                return member;
            }
            throw new ArgumentException(callInfo.MemberTypes + " is not supported");
        }

		public static FieldInfo GetField( CallInfo callInfo )
		{
			var field = callInfo.TargetType.Field( callInfo.Name, callInfo.BindingFlags );
			if( field == null )
			{
				const string fmt = "No match for field with name {0} and flags {1} on type {2}.";
				throw new MissingFieldException( string.Format( fmt, callInfo.Name, callInfo.BindingFlags, callInfo.TargetType ) );
			}
			callInfo.MemberInfo = field;
			return field;
		}

		public static PropertyInfo GetProperty( CallInfo callInfo )
		{
			var property = callInfo.TargetType.Property( callInfo.Name, callInfo.BindingFlags );
			if( property == null )
			{
				const string fmt = "No match for property with name {0} and flags {1} on type {2}.";
				throw new MissingMemberException( string.Format( fmt, callInfo.Name, callInfo.BindingFlags, callInfo.TargetType ) );
			}
			callInfo.MemberInfo = property;
			return property;
		}

    	public static MethodInfo GetPropertyGetMethod(PropertyInfo propInfo, CallInfo callInfo)
        {
            var method = propInfo.GetGetMethod();
			if( method != null )
			    callInfo.MemberInfo = method;
			return method ?? GetPropertyMethod("get_", "getter", callInfo);
        }

        public static MethodInfo GetPropertySetMethod(PropertyInfo propInfo, CallInfo callInfo)
        {
            var method = propInfo.GetSetMethod();
			if( method != null )
			    callInfo.MemberInfo = method;
            return method ?? GetPropertyMethod("set_", "setter", callInfo);
        }

        private static MethodInfo GetPropertyMethod(string infoPrefix, string propertyMethod, CallInfo callInfo)
        {
            var method = callInfo.TargetType.Method(infoPrefix + callInfo.Name, callInfo.BindingFlags);
            if (method == null)
			{
				const string fmt = "No {0} for property {1} with flags {2} on type {3}.";
				throw new MissingFieldException( string.Format( fmt, propertyMethod, callInfo.Name, callInfo.BindingFlags, callInfo.TargetType ) );
			}
			callInfo.MemberInfo = method;
            return method;
        }
    }
}
