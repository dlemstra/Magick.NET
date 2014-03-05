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
using Fasterflect.Caching;

namespace Fasterflect.Emitter
{
    internal abstract class BaseEmitter
    {
        private static readonly Cache<CallInfo, Delegate> cache = new Cache<CallInfo, Delegate>();
        protected static readonly MethodInfo StructGetMethod =
            Constants.StructType.GetMethod("get_Value", BindingFlags.Public | BindingFlags.Instance);
        protected static readonly MethodInfo StructSetMethod =
            Constants.StructType.GetMethod("set_Value", BindingFlags.Public | BindingFlags.Instance);

        protected CallInfo CallInfo;
        protected DynamicMethod Method;
        protected EmitHelper Generator;

        protected BaseEmitter(CallInfo callInfo)
        {
            CallInfo = callInfo;
        }
        
        internal Delegate GetDelegate()
        {
        	var action = cache.Get( CallInfo );
			if( action == null )
			{
				Method = CreateDynamicMethod();
				Generator = new EmitHelper( Method.GetILGenerator() );
				action = CreateDelegate();
				cache.Insert( CallInfo, action, CacheStrategy.Temporary );
			}
			return action;
        }

        protected internal abstract DynamicMethod CreateDynamicMethod();
        protected internal abstract Delegate CreateDelegate();

        protected internal static DynamicMethod CreateDynamicMethod( string name, Type targetType, Type returnType,
                                                                     Type[] paramTypes )
        {
            return new DynamicMethod( name, MethodAttributes.Static | MethodAttributes.Public,
                                      CallingConventions.Standard, returnType, paramTypes,
                                      targetType.IsArray ? targetType.GetElementType() : targetType,
                                      true );
        }

        protected void LoadInnerStructToLocal( byte localPosition )
        {
            Generator
                .castclass( Constants.StructType ) // (ValueTypeHolder)wrappedStruct
                .callvirt(StructGetMethod) // <stack>.get_Value()
                .unbox_any( CallInfo.TargetType ) // unbox <stack>
                .stloc( localPosition ) // localStr = <stack>
                .ldloca_s( localPosition ); // load &localStr
        }

        protected void StoreLocalToInnerStruct( byte localPosition )
        {
            StoreLocalToInnerStruct( 0, localPosition ); // 0: 'this'
        }

        protected void StoreLocalToInnerStruct(byte argPosition, byte localPosition)
        {
            Generator
                .ldarg(argPosition)
                .castclass(Constants.StructType) // wrappedStruct = (ValueTypeHolder)this
                .ldloc(localPosition) // load localStr
                .boxIfValueType(CallInfo.TargetType) // box <stack>
                .callvirt(StructSetMethod); // wrappedStruct.set_Value(<stack>)
        }
    }
}