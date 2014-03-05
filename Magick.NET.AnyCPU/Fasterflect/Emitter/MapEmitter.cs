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
using System.Reflection.Emit;

namespace Fasterflect.Emitter
{
    internal class MapEmitter : BaseEmitter
    {
        private readonly Type sourceType;
        private readonly MemberTypes sourceMemberTypes;
        private readonly MemberTypes targetMemberTypes;
        private readonly string[] names;

        public MapEmitter(Type sourceType, Type targetType, MemberTypes sourceMemberTypes, MemberTypes targetMemberTypes,
                           Flags bindingFlags, params string[] names)
            : base(new MapCallInfo(targetType, null, 
                // Auto-apply IgnoreCase if we're mapping from one membertype to another
                Flags.SetIf(bindingFlags, Flags.IgnoreCase, (sourceMemberTypes & targetMemberTypes) != sourceMemberTypes), 
                MemberTypes.Custom, 
                "Fasterflect_Map", 
                Type.EmptyTypes, 
                null,
				false, 
				sourceType, 
				sourceMemberTypes, 
				targetMemberTypes, 
				names))
        {
            this.sourceType = sourceType;
            this.sourceMemberTypes = sourceMemberTypes;
            this.targetMemberTypes = targetMemberTypes;
            this.names = names;
        }

        protected internal override DynamicMethod CreateDynamicMethod()
        {
            return CreateDynamicMethod(sourceType.Name, sourceType, null, new[] { Constants.ObjectType, Constants.ObjectType });
        }

        protected internal override Delegate CreateDelegate()
        {
            bool handleInnerStruct = CallInfo.ShouldHandleInnerStruct;
            if (handleInnerStruct)
            {
                Generator.ldarg_1.end();                     // load arg-1 (target)
                Generator.DeclareLocal(CallInfo.TargetType); // TargetType localStr;
                Generator
                    .castclass(Constants.StructType) // (ValueTypeHolder)wrappedStruct
                    .callvirt(StructGetMethod) // <stack>.get_Value()
                    .unbox_any(CallInfo.TargetType) // unbox <stack>
                    .stloc(0); // localStr = <stack>
            }

            foreach (var pair in GetMatchingMembers())
            {
                if (handleInnerStruct)
                    Generator.ldloca_s(0).end(); // load &localStr
                else
                    Generator.ldarg_1.castclass(CallInfo.TargetType).end(); // ((TargetType)target)
                Generator.ldarg_0.castclass(sourceType);
                GenerateGetMemberValue(pair.Key);
                GenerateSetMemberValue(pair.Value);
            }

            if (handleInnerStruct)
            {
                StoreLocalToInnerStruct(1, 0);     // ((ValueTypeHolder)this)).Value = tmpStr
            }

            Generator.ret();
            return Method.CreateDelegate(typeof(ObjectMapper));
        }

        private void GenerateGetMemberValue(MemberInfo member)
        {
            if (member is FieldInfo)
            {
                Generator.ldfld((FieldInfo)member);
            }
            else
            {
                var method = ((PropertyInfo)member).GetGetMethod(true);
                Generator.callvirt(method, null);
            }
        }

        private void GenerateSetMemberValue(MemberInfo member)
        {
            if (member is FieldInfo)
            {
                Generator.stfld((FieldInfo)member);
            }
            else
            {
                var method = ((PropertyInfo)member).GetSetMethod(true);
                Generator.callvirt(method, null);
            }
        }

        private IEnumerable<KeyValuePair<MemberInfo, MemberInfo>> GetMatchingMembers()
        {
            StringComparison comparison = CallInfo.BindingFlags.IsSet(Flags.IgnoreCase)
                                            ? StringComparison.OrdinalIgnoreCase
                                            : StringComparison.Ordinal;
            var query = from s in sourceType.Members(sourceMemberTypes, CallInfo.BindingFlags, names)
                        from t in CallInfo.TargetType.Members(targetMemberTypes, CallInfo.BindingFlags, names)
                        where s.Name.Equals(t.Name, comparison) &&
                              t.Type().IsAssignableFrom(s.Type()) &&
                              s.IsReadable() && t.IsWritable()
                        select new { Source = s, Target = t };
            return query.ToDictionary(k => k.Source, v => v.Target);
        }
    }
}