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
using System.Reflection.Emit;

namespace Fasterflect.Emitter
{
    internal class MemberSetEmitter : BaseEmitter
    {
        public MemberSetEmitter(MemberInfo memberInfo, Flags bindingFlags)
            : this(memberInfo.DeclaringType, bindingFlags, memberInfo.MemberType, memberInfo.Name, memberInfo)
        {
        }

		public MemberSetEmitter(Type targetType, Flags bindingFlags, MemberTypes memberType, string fieldOrProperty)
            : this(targetType, bindingFlags, memberType, fieldOrProperty, null)
		{
		}

        private MemberSetEmitter(Type targetType, Flags bindingFlags, MemberTypes memberType, string fieldOrProperty, MemberInfo memberInfo)
            : base(new CallInfo(targetType, null, bindingFlags, memberType, fieldOrProperty, Constants.ArrayOfObjectType, memberInfo, false))
        {
        }
        internal MemberSetEmitter(CallInfo callInfo) : base(callInfo)
        {
        }

        protected internal override DynamicMethod CreateDynamicMethod()
        {
            return CreateDynamicMethod("setter", CallInfo.TargetType, null, new[] { Constants.ObjectType, Constants.ObjectType });
        }

		protected internal override Delegate CreateDelegate()
		{
	    	MemberInfo member = CallInfo.MemberInfo;
			if( member == null )
			{
		    	member = LookupUtils.GetMember( CallInfo );
				CallInfo.IsStatic = member.IsStatic();
			}
			bool handleInnerStruct = CallInfo.ShouldHandleInnerStruct;

			if( CallInfo.IsStatic )
			{
				Generator.ldarg_1.end();							// load value-to-be-set
			}
			else 
			{
				Generator.ldarg_0.end();							// load arg-0 (this)
				if (handleInnerStruct)
				{
					Generator.DeclareLocal(CallInfo.TargetType);    // TargetType tmpStr
					LoadInnerStructToLocal(0);                      // tmpStr = ((ValueTypeHolder)this)).Value;
					Generator.ldarg_1.end();                        // load value-to-be-set;
				}
				else
				{
					Generator.castclass( CallInfo.TargetType )      // (TargetType)this
						.ldarg_1.end();								// load value-to-be-set;
				}
			}

            Generator.CastFromObject( member.Type() );				// unbox | cast value-to-be-set
			if (member.MemberType == MemberTypes.Field)
			{
				var field = member as FieldInfo;
                Generator.stfld(field.IsStatic, field);				// (this|tmpStr).field = value-to-be-set;
			}
			else
			{
				var prop = member as PropertyInfo;
				MethodInfo setMethod = LookupUtils.GetPropertySetMethod(prop, CallInfo);
                Generator.call(setMethod.IsStatic || CallInfo.IsTargetTypeStruct, setMethod); // (this|tmpStr).set_Prop(value-to-be-set);
			}

			if (handleInnerStruct)
			{
                StoreLocalToInnerStruct(0); // ((ValueTypeHolder)this)).Value = tmpStr
			}

		    Generator.ret();

			return Method.CreateDelegate(typeof (MemberSetter));
		}
	}
}