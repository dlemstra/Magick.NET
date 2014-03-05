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
    internal class ArraySetEmitter : BaseEmitter
    {
        public ArraySetEmitter( Type targetType )
            : base(new CallInfo(targetType, null, Flags.InstanceAnyVisibility, MemberTypes.Method, Constants.ArraySetterName,
                                     new[] { typeof(int), targetType.GetElementType() }, null, false))
        {
        }

        protected internal override DynamicMethod CreateDynamicMethod()
        {
            return CreateDynamicMethod( Constants.ArraySetterName, CallInfo.TargetType, null,
                                        new[] { Constants.ObjectType, Constants.IntType, Constants.ObjectType } );
        }

        protected internal override Delegate CreateDelegate()
        {
            Type elementType = CallInfo.TargetType.GetElementType();
            Generator.ldarg_0 // load array
                .castclass( CallInfo.TargetType ) // (T[])array
                .ldarg_1 // load index
                .ldarg_2 // load value
                .CastFromObject( elementType ) // (unbox | cast) value
                .stelem( elementType ) // array[index] = value
                .ret();
            return Method.CreateDelegate( typeof(ArrayElementSetter) );
        }
    }
}