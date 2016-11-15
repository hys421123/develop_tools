using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;


namespace JsonCSharpClassGenerator
{
    public class MappingInfo
    {
        public int Level { get; set; }
        public string EntityName { get; set; }
        public string JsonFromField { get; set; }
        public string JsonToField { get; set; }
        public string JsonToType { get; set; }
    }

    public class JsonClassGenerator
    {
        public string JsonString { get; set; }
        public string TargetFolder { get; set; }
        public string Namespace { get; set; }
        public string MainClass { get; set; }
        public string MobileAPI { get; set; }
        public bool IsCapitalFirstLetter { get; set; }

        public List<MappingInfo> MappingList { get; set; }

        private PluralizationService pluralizationService = PluralizationService.CreateService(new CultureInfo("en-us"));

        public void GenerateJavaClasses()
        {
            var list = MappingList.GroupBy(x => x.EntityName);
            foreach (var g in list)
            {
                string path = Path.Combine(TargetFolder, g.Key + ".java");
                using (var sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine("// JSON Java Class Generator");
                    sw.WriteLine("// Written by Bruce Bao");
                    sw.WriteLine("// Used for API: {0}", MobileAPI);
                    sw.WriteLine("package {0};", Namespace);
                    sw.WriteLine();
                    sw.WriteLine("import java.util.List;");
                    sw.WriteLine();


                    sw.WriteLine("public class {0} {1}", g.Key, "{");

                    //for ctor
                    //WriteStringConstructor(sw, className, prefix);

                    foreach (var field in g)
                    {
                        sw.WriteLine("	private {0} {1};", field.JsonToType, field.JsonFromField);
                    }

                    foreach (var field in g)
                    {
                        sw.WriteLine();
                        sw.WriteLine("	public {0} get{1}() {2}", field.JsonToType, field.JsonToField, "{");
                        sw.WriteLine("		return {0};", field.JsonFromField);
                        sw.WriteLine("	}");

                        sw.WriteLine();
                        sw.WriteLine("	public void set{0}({1} {2}) {3}", field.JsonToField, field.JsonToType, field.JsonFromField, "{");
                        sw.WriteLine("		this.{0} = {0};", field.JsonFromField);
                        sw.WriteLine("	}");
                    }

                    sw.WriteLine("}");
                }
            }
        }

        public void GenerateOCClasses()
        {
            var list = MappingList.GroupBy(x => x.EntityName);
            foreach (var g in list)
            {
                string path = Path.Combine(TargetFolder, g.Key + ".h");
                using (var sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine("// JSON iOS Class Generator");
                    sw.WriteLine("// Written by Bruce Bao");
                    sw.WriteLine("// Used for API: {0}", MobileAPI);
                    sw.WriteLine();

                    sw.WriteLine("#import <Foundation/Foundation.h>");
                    sw.WriteLine();
                    sw.WriteLine("@class " + Namespace + ";");

                    foreach (var field in g)
                    {
                        if (field.JsonToType != "NSNumber" && field.JsonToType != "NSString" && !field.JsonToType.Contains("NSArray"))
                        {
                            sw.WriteLine("@class {0};", field.JsonToType);
                        }
                    }
                    
                    sw.WriteLine();
                    sw.WriteLine("@interface {0} : NSObject", g.Key);
                    sw.WriteLine("{");

                    foreach (var field in g)
                    {
                        if (field.JsonToType.Contains("NSArray<"))
                        {
                            sw.WriteLine("    {0} *{1}; //// JSON: {2}", "NSArray", field.JsonToField, field.JsonFromField);
                        }
                        else
                        {
                            sw.WriteLine("    {0} *{1}; //// JSON: {2}", field.JsonToType, field.JsonToField, field.JsonFromField);
                        }
                    }

                    sw.WriteLine("}");

                    foreach (var field in g)
                    {
                        if (field.JsonToType.Contains("NSArray<"))
                        {
                            sw.WriteLine("@property (nonatomic,retain) {0} *{1};", "NSArray", field.JsonToField);
                        }
                        else
                        {
                            sw.WriteLine("@property (nonatomic,retain) {0} *{1};", field.JsonToType, field.JsonToField);
                        }
                    }

                    sw.WriteLine();
                    sw.WriteLine("- (" + Namespace +" *)objectMapping;");
                    sw.WriteLine();
                    sw.WriteLine("@end");
                }


                string path2 = Path.Combine(TargetFolder, g.Key + ".m");
                using (var sw = new StreamWriter(path2, false, Encoding.UTF8))
                {
                    sw.WriteLine("// JSON iOS Class Generator");
                    sw.WriteLine("// Written by Bruce Bao");
                    sw.WriteLine();

                    sw.WriteLine("#import \"{0}.h\"", g.Key);
                    sw.WriteLine("#import <MTFramework/MTFramework.h>");

                    foreach (var field in g)
                    {
                        if (field.JsonToType.Contains("NSArray"))
                        {
                            if (!field.JsonToType.Contains("NSNumber")
                            && !field.JsonToType.Contains("NSString"))
                            {
                                var type = field.JsonToType.Replace("NSArray<", "").Replace(">", "");
                                sw.WriteLine("#import \"{0}.h\"", type);
                            }
                        }
                        else if (field.JsonToType != "NSNumber" && field.JsonToType != "NSString")
                        {
                            sw.WriteLine("#import \"{0}.h\"", field.JsonToType);
                        }
                    }

                    sw.WriteLine();
                    sw.WriteLine("@implementation {0}", g.Key);

                    sw.WriteLine();
                    foreach (var field in g)
                    {
                        sw.WriteLine("@synthesize {0};", field.JsonToField);
                    }
                    sw.WriteLine();

                    sw.WriteLine("- (" + Namespace + " *)objectMapping {");
                    sw.WriteLine("    " + Namespace + " *mapping = [" + Namespace + " mappingForClass:[" + g.Key + " class]];");

                    foreach (var field in g)
                    {
                        if (field.JsonToType.Contains("NSArray<"))
                        {
                            var type  = field.JsonToType.Replace("NSArray<", "").Replace(">", "");
                            sw.WriteLine("    [mapping mapKeyPath:@\"" + field.JsonFromField + "\" toAttribute:@\"" + field.JsonToField + "\" withMapping:[" + type + " class]];");
                        }
                        else
                        {
                            sw.WriteLine("    [mapping mapKeyPath:@\"" + field.JsonFromField + "\" toAttribute:@\"" + field.JsonToField + "\" withClass:[" + field.JsonToType + " class]];");
                        }
                    }
                    sw.WriteLine("    return mapping;");
                    sw.WriteLine("}");

                    sw.WriteLine();
                    sw.WriteLine("- (void)dealloc {");

                    foreach (var field in g)
                    {
                        sw.WriteLine("    [" + field.JsonToField + " release];");
                    }

                    sw.WriteLine("    [super dealloc];");
                    sw.WriteLine("}");
                    sw.WriteLine();
                    sw.WriteLine("@end");
                }
            }
        }

        public void GenerateCSharpClasses()
        {
            var list = MappingList.GroupBy(x => x.EntityName);
            foreach (var g in list)
            { 
                string path = Path.Combine(TargetFolder, g.Key + ".cs");
                using (var sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine("// JSON C# Class Generator");
                    sw.WriteLine("// Written by Bruce Bao");
                    sw.WriteLine("// Used for API: {0}", MobileAPI);
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Collections.Generic;");
                    sw.WriteLine("using System.Reflection;");
                    sw.WriteLine("using System.Runtime.Serialization;");
                    sw.WriteLine();

                    sw.WriteLine("namespace {0}", Namespace);
                    sw.WriteLine("{");

                    sw.WriteLine("    [DataContract]");
                    sw.WriteLine("    public partial class {0}", g.Key);
                    sw.WriteLine("    {");

                    var prefix = "        ";

                    //for ctor
                    //WriteStringConstructor(sw, className, prefix);

                    foreach (var field in g)
                    {
                        sw.WriteLine();
                        sw.WriteLine(prefix + "[DataMember(Name=\"{0}\")]", field.JsonFromField);
                        sw.WriteLine(prefix + "public {0} {1} {{ get; set; }}", field.JsonToType, field.JsonToField);
                    }

                    sw.WriteLine("    }");
                    sw.WriteLine("}");
                }
            }
        }

        public void PrepareCSharpGridView()
        {
            var json = JObject.Parse(JsonString);

            var parentFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
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

            if (IsCapitalFirstLetter)
            {
                foreach (var field in fieldsList)
                {
                    MappingList.Add(new MappingInfo()
                    {
                        Level = level,
                        EntityName = className,
                        JsonFromField = field.JsonMemberName,
                        JsonToField = field.DefaultMemberName,
                        JsonToType = field.Type.GetCSharpType(true),
                    });
                }
            }
            else
            {
                foreach (var field in fieldsList)
                {
                    MappingList.Add(new MappingInfo()
                    {
                        Level = level,
                        EntityName = className,
                        JsonFromField = field.JsonMemberName,
                        JsonToField = field.JsonMemberName,
                        JsonToType = field.Type.GetCSharpType(true),
                    });
                }
            }
        }

        private HashSet<string> GeneratedNames = new HashSet<string>();

        private string CreateName(string name)
        {
            name = ToTitleCase(name);

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
            plural = ToTitleCase(plural);
            return CreateName(pluralizationService.Singularize(plural));
        }

        private void WriteStringConstructor(StreamWriter sw, string className, string prefix)
        {
            sw.WriteLine();
            sw.WriteLine(prefix + "public {0}()", className);
            sw.WriteLine(prefix + "{");
            sw.WriteLine(prefix + "}");
            sw.WriteLine();
        }

        internal static string ToTitleCase(string str)
        {
            var sb = new StringBuilder(str.Length);
            var flag = true;

            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(flag ? char.ToUpper(c) : c);
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }

            return sb.ToString();
        }
    }
}