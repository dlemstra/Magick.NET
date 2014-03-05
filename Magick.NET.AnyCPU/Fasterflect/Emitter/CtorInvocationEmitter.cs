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
	internal class CtorInvocationEmitter : InvocationEmitter
    {
        public CtorInvocationEmitter(ConstructorInfo ctorInfo, Flags bindingFlags)
            : this(ctorInfo.DeclaringType, bindingFlags, ctorInfo.GetParameters().ToTypeArray(), ctorInfo) { }

        public CtorInvocationEmitter(Type targetType, Flags bindingFlags, Type[] paramTypes)
            : this(targetType, bindingFlags, paramTypes, null) { }

		private CtorInvocationEmitter(Type targetType, Flags flags, Type[] parameterTypes, ConstructorInfo ctorInfo)
            : base(new CallInfo(targetType, null, flags, MemberTypes.Constructor, targetType.Name, parameterTypes, ctorInfo, true))
		{
		}
        
		protected internal override DynamicMethod CreateDynamicMethod()
		{
            return CreateDynamicMethod("ctor", CallInfo.TargetType, Constants.ObjectType, new[] { Constants.ObjectType });
		}

		protected internal override Delegate CreateDelegate()
		{
			if (CallInfo.IsTargetTypeStruct && CallInfo.HasNoParam) // no-arg struct needs special initialization
			{
			    Generator.DeclareLocal( CallInfo.TargetType );      // TargetType tmp
                Generator.ldloca_s(0)                               // &tmp
			             .initobj( CallInfo.TargetType )            // init_obj(&tmp)
			             .ldloc_0.end();                            // load tmp
			}
			else if (CallInfo.TargetType.IsArray)
			{
			    Generator.ldarg_0                                           // load args[] (method arguments)
                         .ldc_i4_0                                          // load 0
                         .ldelem_ref                                        // load args[0] (length)
                         .unbox_any( typeof(int) )                          // unbox stack
                         .newarr( CallInfo.TargetType.GetElementType() );   // new T[args[0]]
			}
			else
			{
                ConstructorInfo ctorInfo = LookupUtils.GetConstructor(CallInfo);
                byte startUsableLocalIndex = 0;
				if (CallInfo.HasRefParam)
				{
                    startUsableLocalIndex = CreateLocalsForByRefParams(0, ctorInfo); // create by_ref_locals from argument array
					Generator.DeclareLocal(CallInfo.TargetType);                     // TargetType tmp;
                }
                
                PushParamsOrLocalsToStack(0);               // push arguments and by_ref_locals
                Generator.newobj(ctorInfo);                 // ctor (<stack>)

				if (CallInfo.HasRefParam)
				{
                    Generator.stloc(startUsableLocalIndex); // tmp = <stack>;
                    AssignByRefParamsToArray(0);            // store by_ref_locals back to argument array
                    Generator.ldloc(startUsableLocalIndex); // tmp
				}
			}
            Generator.boxIfValueType(CallInfo.TargetType)
                     .ret();                                // return (box)<stack>;
			return Method.CreateDelegate(typeof (ConstructorInvoker));
		}
	}
}