// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FileGenerator.MagickScript
{
    internal sealed class XsdGenerator
    {
        private QuantumDepth _Depth;
        private XDocument _Document;
        private MagickScriptTypes _Types;
        private XNamespace _Namespace = "http://www.w3.org/2001/XMLSchema";
        private XmlNamespaceManager _Namespaces;

        private void AddArguments(XElement element, IEnumerable<MethodBase> methods)
        {
            string[] requiredParameters = (from method in methods
                                           from parameter in method.GetParameters()
                                           group parameter by parameter.Name into g
                                           where g.Count() == methods.Count()
                                           select g.Key).ToArray();

            ParameterInfo[] parameters = (from method in methods
                                          from parameter in method.GetParameters()
                                          orderby parameter.Name
                                          select parameter).DistinctBy(p => p.Name).ToArray();

            AddParameterElements(element, parameters, requiredParameters);
            AddParameterAttributes(element, parameters, requiredParameters);
        }

        private void AddClass(XElement parent, Type type)
        {
            AddClassElements(parent, MagickScriptTypes.GetProperties(type), MagickScriptTypes.GetMethods(type));
            AddClassAttributes(parent, MagickScriptTypes.GetProperties(type));
        }

        private void AddClassAttributes(XElement complexType, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in from property in properties
                                     let typeName = MagickScriptTypes.GetXsdAttributeType(property)
                                     where typeName != null
                                     let name = MagickScriptTypes.GetXsdName(property)
                                     orderby name
                                     select new
                                     {
                                         Name = name,
                                         TypeName = typeName
                                     })
            {
                complexType.Add(new XElement(_Namespace + "attribute",
                          new XAttribute("name", property.Name),
                          new XAttribute("type", property.TypeName)));
            }
        }

        private void AddClassElements(XElement complexType, IEnumerable<PropertyInfo> properties, IEnumerable<MethodInfo> methods)
        {
            XElement sequence = new XElement(_Namespace + "sequence");

            foreach (var property in from property in properties
                                     let typeName = MagickScriptTypes.GetXsdElementType(property)
                                     where typeName != null
                                     let name = MagickScriptTypes.GetXsdName(property)
                                     orderby name
                                     select new
                                     {
                                         Name = name,
                                         TypeName = typeName
                                     })
            {
                XElement element = new XElement(_Namespace + "element",
                              new XAttribute("name", property.Name),
                              new XAttribute("minOccurs", "0"));

                element.Add(new XAttribute("type", property.TypeName));

                sequence.Add(element);
            }

            if (methods.Count() > 0)
            {
                foreach (MethodBase method in methods)
                {
                    XElement element = new XElement(_Namespace + "element",
                                  new XAttribute("name", MagickScriptTypes.GetXsdName(method)),
                                  new XAttribute("minOccurs", "0"),
                                  new XAttribute("maxOccurs", "unbounded"));
                    AddMethods(element, new MethodBase[] { method });
                    sequence.Add(element);
                }
            }

            if (sequence.HasElements)
                complexType.Add(sequence);
        }

        private void AddEnumValues(Type enumType, XElement restriction)
        {
            foreach (string name in Enum.GetNames(enumType).OrderBy(n => n))
            {
                if (name == "Undefined")
                    continue;

                restriction.Add(new XElement(_Namespace + "enumeration",
                            new XAttribute("value", name)));
            }
        }

        private void AddMagickImageCollectionMethods(XElement annotation)
        {
            foreach (MethodInfo[] overloads in _Types.GetGroupedMagickImageCollectionMethods())
            {
                annotation.AddBeforeSelf(CreateElement(overloads));
            }
        }

        private void AddMagickImageCollectionResultMethods(XElement annotation)
        {
            foreach (MethodInfo[] overloads in _Types.GetGroupedMagickImageCollectionResultMethods())
            {
                annotation.AddBeforeSelf(CreateElement(overloads));
            }
        }

        private void AddMagickImageMethods(XElement annotation)
        {
            foreach (MethodInfo[] overloads in _Types.GetGroupedMagickImageMethods())
            {
                annotation.AddBeforeSelf(CreateElement(overloads));
            }
        }

        private void AddMagickImageProperties(XElement annotation)
        {
            foreach (PropertyInfo property in _Types.GetMagickImageProperties())
            {
                annotation.AddBeforeSelf(CreateElement(property));
            }
        }

        private void AddMagickReadSettingsMethods(XElement annotation)
        {
            foreach (MethodInfo[] overloads in _Types.GetGroupedMagickReadSettingsMethods())
            {
                annotation.AddBeforeSelf(CreateElement(overloads));
            }
        }

        private void AddMagickReadSettingsProperties(XElement annotation)
        {
            foreach (PropertyInfo property in _Types.GetMagickReadSettingsProperties())
            {
                annotation.AddBeforeSelf(CreateElement(property));
            }
        }

        private void AddMagickSettingsMethods(XElement annotation)
        {
            foreach (MethodInfo[] overloads in _Types.GetGroupedMagickSettingsMethods())
            {
                annotation.AddBeforeSelf(CreateElement(overloads));
            }
        }

        private void AddMagickSettingsProperties(XElement annotation)
        {
            foreach (PropertyInfo property in _Types.GetMagickSettingsProperties())
            {
                annotation.AddBeforeSelf(CreateElement(property));
            }
        }

        private void AddMethods(XElement element, IEnumerable<MethodBase> methods)
        {
            ParameterInfo[] parameters = (from method in methods
                                          from parameter in method.GetParameters()
                                          select parameter).ToArray();
            if (parameters.Length == 0)
            {
                element.Add(new XAttribute("type", "empty"));
                return;
            }

            if (methods.Count() == 1 && IsTypedElement(methods.First(), parameters))
            {
                string elementTypeName = MagickScriptTypes.GetXsdElementType(parameters[0]);
                if (string.IsNullOrEmpty(elementTypeName))
                    throw new NotImplementedException("AddMethods: " + methods.First().Name);

                element.Add(new XAttribute("type", elementTypeName));
                return;
            }

            XElement complexType = new XElement(_Namespace + "complexType");

            AddArguments(complexType, methods);

            element.Add(complexType);
        }

        private void AddParameterElements(XElement complexType, ParameterInfo[] parameters, string[] requiredParameters)
        {
            XElement sequence = new XElement(_Namespace + "sequence");

            foreach (var parameter in from parameter in parameters
                                      let typeName = MagickScriptTypes.GetXsdElementType(parameter)
                                      where typeName != null
                                      orderby parameter.Name
                                      select new
                                      {
                                          Name = parameter.Name,
                                          TypeName = typeName,
                                          IsRequired = requiredParameters.Contains(parameter.Name)
                                      })
            {
                XElement element = new XElement(_Namespace + "element",
                              new XAttribute("name", parameter.Name));

                if (!parameter.IsRequired)
                    element.Add(new XAttribute("minOccurs", "0"));

                element.Add(new XAttribute("type", parameter.TypeName));

                sequence.Add(element);
            }

            if (sequence.HasElements)
                complexType.Add(sequence);
        }

        private void AddParameterAttributes(XElement complexType, ParameterInfo[] parameters, string[] requiredParameters)
        {
            foreach (var parameter in from parameter in parameters
                                      let typeName = MagickScriptTypes.GetXsdAttributeType(parameter)
                                      where typeName != null
                                      orderby parameter.Name
                                      select new
                                      {
                                          Name = parameter.Name,
                                          TypeName = typeName,
                                          IsRequired = requiredParameters.Contains(parameter.Name)
                                      })
            {
                XElement attribute = new XElement(_Namespace + "attribute",
                                new XAttribute("name", parameter.Name));

                if (parameter.IsRequired)
                    attribute.Add(new XAttribute("use", "required"));

                attribute.Add(new XAttribute("type", parameter.TypeName));

                complexType.Add(attribute);
            }
        }

        private XElement CreateElement(IEnumerable<MethodBase> methods)
        {
            XElement element = new XElement(_Namespace + "element",
                          new XAttribute("name", MagickScriptTypes.GetXsdName(methods.First())));

            AddMethods(element, methods);
            return element;
        }

        private XElement CreateElement(PropertyInfo property)
        {
            string name = MagickScriptTypes.GetXsdName(property);

            string attributeTypeName = MagickScriptTypes.GetXsdAttributeType(property);

            if (attributeTypeName != null)
            {
                XElement complexType = new XElement(_Namespace + "complexType");
                complexType.Add(new XElement(_Namespace + "attribute",
                            new XAttribute("name", "value"),
                            new XAttribute("use", "required"),
                            new XAttribute("type", attributeTypeName)));

                return new XElement(_Namespace + "element",
                      new XAttribute("name", name),
                      complexType);
            }
            else
            {
                string elementTypeName = MagickScriptTypes.GetXsdElementType(property);
                if (string.IsNullOrEmpty(elementTypeName))
                    throw new NotImplementedException("CreateElement: " + name);

                return new XElement(_Namespace + "element",
                  new XAttribute("name", name),
                  new XAttribute("type", elementTypeName));
            }
        }

        private XElement CreateEnumElement(Type enumType)
        {
            XElement restriction = new XElement(_Namespace + "restriction", new XAttribute("base", "xs:NMTOKEN"));
            AddEnumValues(enumType, restriction);

            return CreateVarElement(enumType.Name, restriction);
        }

        private XElement CreateVarElement(string name, XElement restriction)
        {
            return new XElement(_Namespace + "simpleType",
                  new XAttribute("name", name),
                  new XElement(_Namespace + "union",
                    new XElement(_Namespace + "simpleType",
                      restriction),
                    new XElement(_Namespace + "simpleType",
                      new XElement(_Namespace + "restriction",
                        new XAttribute("base", "var")))));
        }

        private static bool IsTypedElement(MethodBase method, ParameterInfo[] parameters)
        {
            if (parameters.Length > 1)
                return false;

            ParameterInfo parameter = parameters[0];

            if (parameter.ParameterType.IsGenericType && parameter.ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return true;

            if (method.DeclaringType.Name == "MagickImage" && method.Name == "SetProfile")
                return true;

            return false;
        }

        private void LoadDocument()
        {
            _Document = XDocument.Load(PathHelper.GetFullPath(@"Tools\FileGenerators\MagickScript\Xsd\MagickScript.xsd"));
        }

        private void RemoveComments()
        {
            _Document.DescendantNodes().OfType<XComment>().Remove();
        }

        private void RemoveUnusedSimpleTypes()
        {
            XElement[] simpleTypes = (from element in _Document.Root.XPathSelectElements("./xs:simpleType", _Namespaces)
                                      select element).ToArray();

            foreach (XElement simpleType in simpleTypes)
            {
                string name = (string)simpleType.Attribute("name");

                if (name == "var")
                    continue;

                if (!_Document.XPathSelectElements("//xs:attribute[@type='" + name + "']", _Namespaces).Any())
                    simpleType.Remove();
            }
        }

        private void ReplaceAnnotations()
        {
            XElement[] annotations = (from element in _Document.XPathSelectElements("//xs:annotation", _Namespaces)
                                      select element).ToArray();

            foreach (XElement annotation in annotations)
            {
                string annotationID = annotation.Attribute("id").Value;
                switch (annotationID)
                {
                    case "color":
                        ReplaceColor(annotation);
                        break;
                    case "collection-actions":
                        ReplaceCollectionActions(annotation);
                        break;
                    case "collection-results":
                        ReplaceCollectionResults(annotation);
                        break;
                    case "colorProfile":
                        ReplaceColorProfile(annotation);
                        break;
                    case "imageProfile":
                    case "pathArc":
                    case "primaryInfo":
                    case "sparseColorArg":
                        ReplaceWithType(annotation, annotationID);
                        break;
                    case "defines":
                        ReplaceDefines(annotation);
                        break;
                    case "drawables":
                        ReplaceDrawables(annotation);
                        break;
                    case "enums":
                        ReplaceEnums(annotation);
                        break;
                    case "iDefines":
                        ReplaceIDefines(annotation);
                        break;
                    case "image-actions":
                        ReplaceImageActions(annotation);
                        break;
                    case "iReadDefines":
                        ReplaceIReadDefines(annotation);
                        break;
                    case "magickReadSettings":
                        ReplaceMagickReadSettings(annotation);
                        break;
                    case "magickSettings":
                        ReplaceMagickSettings(annotation);
                        break;
                    case "complexSettings":
                    case "deskewSettings":
                    case "distortSettings":
                    case "kmeansSettings":
                    case "montageSettings":
                    case "morphologySettings":
                    case "pixelReadSettings":
                    case "quantizeSettings":
                        ReplaceWithClass(annotation, annotationID);
                        break;
                    case "paths":
                        ReplacePaths(annotation);
                        break;
                    default:
                        throw new NotImplementedException("ReplaceAnnotations: " + annotationID);
                }
            }
        }

        private void ReplaceCollectionActions(XElement annotation)
        {
            AddMagickImageCollectionMethods(annotation);

            annotation.Remove();
        }

        private void ReplaceCollectionResults(XElement annotation)
        {
            AddMagickImageCollectionResultMethods(annotation);

            annotation.Remove();
        }

        private void ReplaceColor(XElement annotation)
        {
            XElement restriction = new XElement(_Namespace + "restriction",
                            new XAttribute("base", "xs:string"));

            if (_Depth >= QuantumDepth.Q8)
            {
                restriction.Add(new XElement(_Namespace + "pattern",
                            new XAttribute("value", "#([0-9a-fA-F]{3,4})")));

                restriction.Add(new XElement(_Namespace + "pattern",
                            new XAttribute("value", "#([0-9a-fA-F]{2}){3,4}")));
            }

            if (_Depth >= QuantumDepth.Q16)
                restriction.Add(new XElement(_Namespace + "pattern",
                            new XAttribute("value", "#([0-9a-fA-F]{4}){3,4}")));

            restriction.Add(new XElement(_Namespace + "pattern",
                  new XAttribute("value", "Transparent")));

            annotation.ReplaceWith(CreateVarElement("color", restriction));
        }

        private void ReplaceColorProfile(XElement annotation)
        {
            XElement restriction = new XElement(_Namespace + "restriction",
                            new XAttribute("base", "xs:NMTOKEN"));
            foreach (string name in _Types.GetColorProfileNames())
            {
                restriction.Add(new XElement(_Namespace + "enumeration",
                            new XAttribute("value", name)));
            }

            annotation.ReplaceWith(CreateVarElement("ColorProfile", restriction));
        }

        private void ReplaceDefines(XElement annotation)
        {
            List<XElement> types = new List<XElement>();

            foreach (Type interfaceType in _Types.GetInterfaceTypes("IDefines"))
            {
                XElement complexType = new XElement(_Namespace + "complexType",
                                  new XAttribute("name", MagickScriptTypes.GetXsdName(interfaceType)));
                AddClass(complexType, interfaceType);
                types.Add(complexType);
            }

            annotation.ReplaceWith(types.ToArray());
        }

        private void ReplaceDrawables(XElement annotation)
        {
            foreach (ConstructorInfo[] constructors in _Types.GetDrawables())
            {
                annotation.AddBeforeSelf(CreateElement(constructors));
            }

            annotation.Remove();
        }

        private void ReplaceEnums(XElement annotation)
        {
            foreach (Type enumType in _Types.GetEnums())
            {
                annotation.AddBeforeSelf(CreateEnumElement(enumType));
            }

            annotation.Remove();
        }

        private void ReplaceIDefines(XElement annotation)
        {
            annotation.ReplaceWith(
              from type in _Types.GetInterfaceTypes("IDefines")
              let name = MagickScriptTypes.GetXsdName(type)
              select new XElement(_Namespace + "element",
                new XAttribute("name", name),
                new XAttribute("type", name)));
        }

        private void ReplaceImageActions(XElement annotation)
        {
            AddMagickImageProperties(annotation);
            AddMagickImageMethods(annotation);

            annotation.Remove();
        }

        private void ReplaceIReadDefines(XElement annotation)
        {
            annotation.ReplaceWith(
              from type in _Types.GetInterfaceTypes("IReadDefines")
              let name = MagickScriptTypes.GetXsdName(type)
              select new XElement(_Namespace + "element",
                new XAttribute("name", name),
                new XAttribute("type", name)));
        }

        private void ReplaceMagickReadSettings(XElement annotation)
        {
            AddMagickReadSettingsProperties(annotation);
            AddMagickReadSettingsMethods(annotation);

            annotation.Remove();
        }

        private void ReplaceMagickSettings(XElement annotation)
        {
            AddMagickSettingsProperties(annotation);
            AddMagickSettingsMethods(annotation);

            annotation.Remove();
        }

        private void ReplacePaths(XElement annotation)
        {
            foreach (ConstructorInfo[] constructors in _Types.GetPaths())
            {
                annotation.AddBeforeSelf(CreateElement(constructors));
            }

            annotation.Remove();
        }

        private void ReplaceWithClass(XElement annotation, string typeName)
        {
            AddClass(annotation.Parent, _Types.GetType(typeName));
            annotation.Remove();
        }

        private void Write()
        {
            string folder = MagickScriptTypes.GetFolderName(_Depth);
            string outputFile = PathHelper.GetFullPath(@"src\Magick.NET\Resources\" + folder + @"\MagickScript.xsd");
            Console.WriteLine("Creating: " + outputFile);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";

            using (XmlWriter writer = XmlWriter.Create(outputFile, settings))
            {
                _Document.Save(writer);
            }
        }

        private void ReplaceWithType(XElement annotation, string typeName)
        {
            AddArguments(annotation.Parent, _Types.GetConstructors(typeName));

            annotation.Remove();
        }

        public XsdGenerator(QuantumDepth depth)
        {
            _Depth = depth;
            _Types = new MagickScriptTypes(depth);

            _Namespaces = new XmlNamespaceManager(new NameTable());
            _Namespaces.AddNamespace("xs", _Namespace.ToString());
        }

        public void Generate()
        {
            LoadDocument();
            RemoveComments();
            ReplaceAnnotations();
            RemoveUnusedSimpleTypes();
            Write();
        }
    }
}
