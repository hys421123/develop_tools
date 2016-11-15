using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace JsonCSharpClassGenerator
{
    class JsonType
    {
        public JsonTypeEnum Type { get; private set; }
        public JsonType InternalType { get; private set; }
        public string AssignedName { get; set; }

        public JsonType(JToken token)
        {
            Type = GetFirstTypeEnum(token);

            if (Type == JsonTypeEnum.Array)
            {
                var array = (JArray)token;
                InternalType = GetCommonType(array.ToArray());
            }
        }

        public static readonly JsonType Null = new JsonType(JsonTypeEnum.NullableSomething);

        private JsonType(JsonTypeEnum type)
        {
            this.Type = type;
        }

        public static JsonType GetCommonType(JToken[] tokens)
        {
            if (tokens.Length == 0) return new JsonType(JsonTypeEnum.NonConstrained);

            var common = new JsonType(tokens[0]);
            for (int i = 1; i < tokens.Length; i++)
            {
                var current = new JsonType(tokens[i]);
                common = common.GetCommonType(current);
            }

            return common;
        }

        public string GetCSharpType(bool useLists)
        {
            switch (Type)
            {
                case JsonTypeEnum.Anything: return "object";
                case JsonTypeEnum.Array:
                    if (MobileSystem.System == "iOS")
                    {
                        return "NSArray<" + InternalType.GetCSharpType(useLists) + ">";
                    }
                    else
                    { 
                        return "List<" + InternalType.GetCSharpType(useLists) + ">";
                    }

                case JsonTypeEnum.Dictionary: return "Dictionary<string, " + InternalType.GetCSharpType(useLists) + ">";
                case JsonTypeEnum.Boolean: 
                    if (MobileSystem.System == "Android")
                    {
                        return "boolean";
                    }
                    else
                    {
                        return "bool";
                    }
                case JsonTypeEnum.Float:
                    if (MobileSystem.System == "iOS")
                    {
                        return "NSNumber";
                    }
                    else
                    {
                        return "double";
                    }

                case JsonTypeEnum.Integer:
                    if (MobileSystem.System == "iOS")
                    {
                        return "NSNumber";
                    }
                    else
                    {
                        return "int";
                    }

                case JsonTypeEnum.Long: return "long";
                case JsonTypeEnum.Date: return "DateTime";
                case JsonTypeEnum.NonConstrained: return "object";
                case JsonTypeEnum.NullableBoolean: return "bool?";
                case JsonTypeEnum.NullableFloat: return "double?";
                case JsonTypeEnum.NullableInteger: return "int?";
                case JsonTypeEnum.NullableLong: return "long?";
                case JsonTypeEnum.NullableDate: return "DateTime?";
                case JsonTypeEnum.NullableSomething: return "object";
                case JsonTypeEnum.Object: return AssignedName;
                case JsonTypeEnum.String:
                    if (MobileSystem.System == "iOS")
                    {
                        return "NSString";
                    }
                    else if (MobileSystem.System == "Android")
                    {
                        return "String";
                    }
                    else
                    {
                        return "string";
                    }

                default: throw new System.NotSupportedException("Unsupported json type");
            }
        }

        public JsonType GetCommonType(JsonType type2)
        {
            var commonType = GetCommonTypeEnum(this.Type, type2.Type);

            if (commonType == JsonTypeEnum.Array)
            {
                if (type2.Type == JsonTypeEnum.NullableSomething) return this;
                if (this.Type == JsonTypeEnum.NullableSomething) return type2;
                var commonInternalType = InternalType.GetCommonType(type2.InternalType);
                if (commonInternalType != InternalType) return new JsonType(JsonTypeEnum.Array) { InternalType = commonInternalType };
            }

            if (this.Type == commonType) return this;
            return new JsonType(commonType);
        }

        private static bool IsNull(JsonTypeEnum type)
        {
            return type == JsonTypeEnum.NullableSomething;
        }

        private JsonTypeEnum GetCommonTypeEnum(JsonTypeEnum type1, JsonTypeEnum type2)
        {
            if (type1 == JsonTypeEnum.NonConstrained) return type2;
            if (type2 == JsonTypeEnum.NonConstrained) return type1;

            switch (type1)
            {
                case JsonTypeEnum.Boolean:
                    if (IsNull(type2)) return JsonTypeEnum.NullableBoolean;
                    if (type2 == JsonTypeEnum.Boolean) return type1;
                    break;
                case JsonTypeEnum.NullableBoolean:
                    if (IsNull(type2)) return type1;
                    if (type2 == JsonTypeEnum.Boolean) return type1;
                    break;
                case JsonTypeEnum.Integer:
                    if (IsNull(type2)) return JsonTypeEnum.NullableInteger;
                    if (type2 == JsonTypeEnum.Float) return JsonTypeEnum.Float;
                    if (type2 == JsonTypeEnum.Long) return JsonTypeEnum.Long;
                    if (type2 == JsonTypeEnum.Integer) return type1;
                    break;
                case JsonTypeEnum.NullableInteger:
                    if (IsNull(type2)) return type1;
                    if (type2 == JsonTypeEnum.Float) return JsonTypeEnum.NullableFloat;
                    if (type2 == JsonTypeEnum.Long) return JsonTypeEnum.NullableLong;
                    if (type2 == JsonTypeEnum.Integer) return type1;
                    break;
                case JsonTypeEnum.Float:
                    if (IsNull(type2)) return JsonTypeEnum.NullableFloat;
                    if (type2 == JsonTypeEnum.Float) return type1;
                    if (type2 == JsonTypeEnum.Integer) return type1;
                    if (type2 == JsonTypeEnum.Long) return type1;
                    break;
                case JsonTypeEnum.NullableFloat:
                    if (IsNull(type2)) return type1;
                    if (type2 == JsonTypeEnum.Float) return type1;
                    if (type2 == JsonTypeEnum.Integer) return type1;
                    if (type2 == JsonTypeEnum.Long) return type1;
                    break;
                case JsonTypeEnum.Long:
                    if (IsNull(type2)) return JsonTypeEnum.NullableLong;
                    if (type2 == JsonTypeEnum.Float) return JsonTypeEnum.Float;
                    if (type2 == JsonTypeEnum.Integer) return type1;
                    break;
                case JsonTypeEnum.NullableLong:
                    if (IsNull(type2)) return type1;
                    if (type2 == JsonTypeEnum.Float) return JsonTypeEnum.NullableFloat;
                    if (type2 == JsonTypeEnum.Integer) return type1;
                    if (type2 == JsonTypeEnum.Long) return type1;
                    break;
                case JsonTypeEnum.Date:
                    if (IsNull(type2)) return JsonTypeEnum.NullableDate;
                    if (type2 == JsonTypeEnum.Date) return JsonTypeEnum.Date;
                    break;
                case JsonTypeEnum.NullableDate:
                    if (IsNull(type2)) return type1;
                    if (type2 == JsonTypeEnum.Date) return type1;
                    break;
                case JsonTypeEnum.NullableSomething:
                    if (IsNull(type2)) return type1;
                    if (type2 == JsonTypeEnum.String) return JsonTypeEnum.String;
                    if (type2 == JsonTypeEnum.Integer) return JsonTypeEnum.NullableInteger;
                    if (type2 == JsonTypeEnum.Float) return JsonTypeEnum.NullableFloat;
                    if (type2 == JsonTypeEnum.Long) return JsonTypeEnum.NullableLong;
                    if (type2 == JsonTypeEnum.Boolean) return JsonTypeEnum.NullableBoolean;
                    if (type2 == JsonTypeEnum.Date) return JsonTypeEnum.NullableDate;
                    if (type2 == JsonTypeEnum.Array) return JsonTypeEnum.Array;
                    if (type2 == JsonTypeEnum.Object) return JsonTypeEnum.Object;
                    break;
                case JsonTypeEnum.Object:
                    if (IsNull(type2)) return type1;
                    if (type2 == JsonTypeEnum.Object) return type1;
                    if (type2 == JsonTypeEnum.Dictionary) throw new ArgumentException();
                    break;
                case JsonTypeEnum.Dictionary:
                    throw new ArgumentException();
                case JsonTypeEnum.Array:
                    if (IsNull(type2)) return type1;
                    if (type2 == JsonTypeEnum.Array) return type1;
                    break;
                case JsonTypeEnum.String:
                    if (IsNull(type2)) return type1;
                    if (type2 == JsonTypeEnum.String) return type1;
                    break;
            }

            return JsonTypeEnum.Anything;
        }

        private static bool IsNull(JTokenType type)
        {
            return type == JTokenType.Null || type == JTokenType.Undefined;
        }

        private static JsonTypeEnum GetFirstTypeEnum(JToken token)
        {
            var type = token.Type;
            if (type == JTokenType.Integer)
            {
                if ((long)((JValue)token).Value < int.MaxValue) return JsonTypeEnum.Integer;
                else return JsonTypeEnum.Long;

            }
            switch (type)
            {
                case JTokenType.Array: return JsonTypeEnum.Array;
                case JTokenType.Boolean: return JsonTypeEnum.Boolean;
                case JTokenType.Float: return JsonTypeEnum.Float;
                case JTokenType.Null: return JsonTypeEnum.NullableSomething;
                case JTokenType.Undefined: return JsonTypeEnum.NullableSomething;
                case JTokenType.String: return JsonTypeEnum.String;
                case JTokenType.Object: return JsonTypeEnum.Object;
                case JTokenType.Date: return JsonTypeEnum.Date;

                default: return JsonTypeEnum.Anything;

            }
        }
    }
}
