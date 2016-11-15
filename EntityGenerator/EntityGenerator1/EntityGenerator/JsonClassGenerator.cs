using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;


namespace EntityGenerator
{
    public class JsonClassGenerator
    {
        public string JsonString { get; set; }
        public string Namespace { get; set; }
        public string MainClass { get; set; }
        public string MobileAPI { get; set; }
        public MobileSystemEnum System { get; set; }

        public List<MappingInfo> MappingList { get; set; }

        private PluralizationService pluralizationService = PluralizationService.CreateService(new CultureInfo("en-us"));

        public void PrepareCSharpGridView()
        {
            var json = JObject.Parse(JsonString);
            GeneratedNames.Add(MainClass.ToLower());
            GenerateClass(new JObject[] { json }, MainClass, true, 1);
        }

        public void PrepareJavaGridView()
        {
            PrepareCSharpGridView();
        }

        public void PrepareOCGridView()
        {
            PrepareCSharpGridView();
        }

        private void GenerateClass(JObject[] examples, string className, bool isRoot, int level)
        {
            var fields = new Dictionary<string, JsonType>();

            var first = true;
            foreach (var obj in examples)
            {
                foreach (var prop in obj.Properties())
                {
                    JsonType fieldType;

                    var currentType = new JsonType(prop.Value);
                    var propName = prop.Name;
                    if (fields.TryGetValue(propName, out fieldType))
                    {
                        var commonType = fieldType.GetCommonType(currentType);

                        fields[propName] = commonType;
                    }
                    else
                    {
                        var commonType = currentType;
                        if (!first) commonType = commonType.GetCommonType(JsonType.Null);
                        fields.Add(propName, commonType);
                    }
                }
                first = false;
            }

            foreach (var field in fields)
            {
                var type = field.Value;
                if (type.Type == JsonTypeEnum.Object)
                {
                    var subexamples = new List<JObject>(examples.Length);
                    foreach (var obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value.Type == JTokenType.Object)
                            {
                                subexamples.Add((JObject)value);
                            }
                        }
                    }

                    field.Value.AssignedName = CreateName(field.Key);
                    GenerateClass(subexamples.ToArray(), field.Value.AssignedName, false, level + 1);
                }

                if (type.InternalType != null && type.InternalType.Type == JsonTypeEnum.Object)
                {
                    var subexamples = new List<JObject>(examples.Length);
                    foreach (var obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value.Type == JTokenType.Array)
                            {
                                subexamples.Add(((JArray)value)[0] as JObject);
                            }
                            else if (value.Type == JTokenType.Object)
                            {
                                subexamples.Add(((JObject)value)[0] as JObject);
                            }
                        }
                    }

                    field.Value.InternalType.AssignedName = CreateNameFromPlural(field.Key);
                    GenerateClass(subexamples.ToArray(), field.Value.InternalType.AssignedName, false, level + 1);
                }
            }

            var fieldsList = fields.Select(x => new FieldInfo(x.Key, x.Value)).ToArray();

            foreach (var field in fieldsList)
            {
                MappingList.Add(new MappingInfo()
                {
                    Level = level,
                    EntityName = className,
                    JsonFromField = field.JsonMemberName,
                    JsonToField = field.DefaultMemberName,
                    JsonToType = field.Type.GetCSharpType(System, true),
                });
            }
        }

        private HashSet<string> GeneratedNames = new HashSet<string>();

        private string CreateName(string name)
        {
            name = StringHelper.ToTitleCase(name);

            var finalName = name;
            if (GeneratedNames.Contains(finalName.ToLower()))
            {
                var i = 2;
                do
                {
                    finalName = name + i.ToString();
                    i++;
                } while (GeneratedNames.Contains(finalName.ToLower()));
            }

            GeneratedNames.Add(finalName.ToLower());
            return finalName;
        }

        private string CreateNameFromPlural(string plural)
        {
            plural = StringHelper.ToTitleCase(plural);
            return CreateName(pluralizationService.Singularize(plural));
        }
    }
}