using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EntityGenerator
{
    public enum MobileSystemEnum
    {
        IPhone = 1,
        Android = 2,
        WP7 = 3,
    }

    public partial class Default : System.Web.UI.Page
    {
        JsonClassGenerator gen;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");

                foreach (var item in Enum.GetValues(typeof(MobileSystemEnum)))
                {
                    DataRow dr = dt.Rows.Add();
                    dr["Name"] = item.ToString();
                }

                ddlMobileSystem.Items.Add(MobileSystemEnum.IPhone.ToString());
                ddlMobileSystem.Items.Add(MobileSystemEnum.Android.ToString());
                ddlMobileSystem.Items.Add(MobileSystemEnum.WP7.ToString());

                ddlMobileSystem_SelectedIndexChanged(null, null);
            }
        }

        protected void btnLoadMapping_Click(object sender, EventArgs e)
        {
            gen = new JsonClassGenerator();
            gen.JsonString = StringHelper.GetCleanText(txtJsonString.Text.Trim());
            gen.MainClass = @"MainClass";
            gen.System = (MobileSystemEnum)Enum.Parse(typeof(MobileSystemEnum), ddlMobileSystem.SelectedValue);
            gen.MappingList = new List<MappingInfo>();

            try
            {
                if (ddlMobileSystem.SelectedValue == MobileSystemEnum.WP7.ToString())
                    gen.PrepareCSharpGridView();
                else if (ddlMobileSystem.SelectedValue == MobileSystemEnum.Android.ToString())
                    gen.PrepareJavaGridView();
                else if (ddlMobileSystem.SelectedValue == MobileSystemEnum.IPhone.ToString())
                    gen.PrepareOCGridView();

                gen.MappingList = gen.MappingList.OrderBy(x => x.Level).ToList();
                gvMappingList.DataSource = gen.MappingList;
                gvMappingList.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<scrit>alert('出错啦。" + ex.Message + "');</script>");
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            gen = new JsonClassGenerator();
            gen.System = (MobileSystemEnum)Enum.Parse(typeof(MobileSystemEnum), ddlMobileSystem.SelectedValue);
            gen.Namespace = txtVariable.Value.Trim();
            gen.MobileAPI = txtMobileAPI.Value.Trim();

            gen.MappingList = new List<MappingInfo>();
            for (int i = 0; i < gvMappingList.Rows.Count; i++)
            {
                if ((gvMappingList.Rows[i].Cells[1].FindControl("EntityName") as TextBox).Text == ""
                    || (gvMappingList.Rows[i].Cells[1].FindControl("JsonFromField") as TextBox).Text == ""
                    || (gvMappingList.Rows[i].Cells[1].FindControl("JsonToField") as TextBox).Text == ""
                    || (gvMappingList.Rows[i].Cells[1].FindControl("JsonToType") as TextBox).Text == "")
                    continue;

                gen.MappingList.Add(
                    new MappingInfo()
                    {
                        Level = Convert.ToInt16((gvMappingList.Rows[i].Cells[0].FindControl("Level") as Label).Text),
                        EntityName = (gvMappingList.Rows[i].Cells[1].FindControl("EntityName") as TextBox).Text,
                        JsonFromField = (gvMappingList.Rows[i].Cells[1].FindControl("JsonFromField") as TextBox).Text,
                        JsonToField = (gvMappingList.Rows[i].Cells[1].FindControl("JsonToField") as TextBox).Text,
                        JsonToType = HttpUtility.HtmlDecode((gvMappingList.Rows[i].Cells[1].FindControl("JsonToType") as TextBox).Text),
                    });
            }

            GenerateClass();
        }

        public void GenerateClass()
        {
            Response.Clear();
            Response.Buffer = false;
            Response.ContentType = "application/octet-stream";

            var fileName = gen.MappingList.Where(x => x.Level == 1).ToList()[0].EntityName;
            Response.AppendHeader("content-disposition", "attachment;filename=" + fileName + ".txt;");

            switch (gen.System)
            {
                case MobileSystemEnum.WP7:
                    GenerateCSharpClass();
                    break;
                case MobileSystemEnum.Android:
                    GenerateJavaClasses();
                    break;
                case MobileSystemEnum.IPhone:
                    GenerateOCClasses();
                    break;
                default:
                    break;
            }

            Response.Flush();
            Response.End();

            Response.Write("<scrit>alert('哦yeah，成功啦!');</script>");
        }

        public void GenerateCSharpClass()
        {
            var list = gen.MappingList.GroupBy(x => x.EntityName);
            foreach (var g in list)
            {
                WriteLine("// JSON C# Class Generator");
                WriteLine("// Written by Bruce Bao");
                if (!String.IsNullOrEmpty(gen.MobileAPI))
                {
                    WriteLine("// Used for API: {0}", gen.MobileAPI);
                }
                WriteLine("using System;");
                WriteLine("using System.Collections.Generic;");
                WriteLine("using System.Reflection;");
                WriteLine("using System.Runtime.Serialization;");
                WriteLine();

                WriteLine("namespace {0}", gen.Namespace);
                WriteLine("{");

                WriteLine("    [DataContract]");
                WriteLine("    public partial class {0}", g.Key);
                WriteLine("    {");

                var prefix = "        ";

                foreach (var field in g)
                {
                    WriteLine();
                    WriteLine(prefix + "[DataMember(Name=\"{0}\")]", field.JsonFromField);
                    WriteLine(prefix + "public {0} {1} {{ get; set; }}", field.JsonToType, field.JsonToField);
                }

                WriteLine("    }");
                WriteLine("}");
                WriteLine();
            }
        }

        public void GenerateJavaClasses()
        {
            var list = gen.MappingList.GroupBy(x => x.EntityName);
            foreach (var g in list)
            {
                WriteLine("// JSON Java Class Generator");
                WriteLine("// Written by Bruce Bao");
                if (!String.IsNullOrEmpty(gen.MobileAPI))
                {
                    WriteLine("// Used for API: {0}", gen.MobileAPI);
                }
                WriteLine("package {0}", gen.Namespace);
                WriteLine();
                WriteLine("import java.util.List;");
                WriteLine();


                WriteLine("public class {0} {1}", g.Key, "{");

                foreach (var field in g)
                {
                    WriteLine("	private {0} {1};", field.JsonToType, field.JsonFromField);
                }

                foreach (var field in g)
                {
                    WriteLine();
                    WriteLine("	public {0} get{1}() {2}", field.JsonToType, field.JsonToField, "{");
                    WriteLine("		return {0};", field.JsonFromField);
                    WriteLine("	}");

                    WriteLine();
                    WriteLine("	public void set{0}({1} {2}) {3}", field.JsonToField, field.JsonToType, field.JsonFromField, "{");
                    WriteLine("		this.{0} = {0};", field.JsonFromField);
                    WriteLine("	}");
                }

                WriteLine("}");
                WriteLine();
                WriteLine();
            }
        }

        public void GenerateOCClasses()
        {
            var list = gen.MappingList.GroupBy(x => x.EntityName);
            foreach (var g in list)
            {
                //h file
                WriteLine("// JSON iOS Class Generator");
                WriteLine("// Written by Bruce Bao");
                if (!String.IsNullOrEmpty(gen.MobileAPI))
                {
                    WriteLine("// Used for API: {0}", gen.MobileAPI);
                }
                WriteLine();

                WriteLine("#import <Foundation/Foundation.h>");
                WriteLine();
                WriteLine("@class " + gen.Namespace + ";");

                foreach (var field in g)
                {
                    if (field.JsonToType != "NSNumber" && field.JsonToType != "NSString" && !field.JsonToType.Contains("NSArray"))
                    {
                        WriteLine("@class {0}", field.JsonToType);
                    }
                }

                WriteLine();
                WriteLine("@interface {0} : NSObject", g.Key);
                WriteLine("{");

                foreach (var field in g)
                {
                    if (field.JsonToType.Contains("NSArray<"))
                    {
                        WriteLine("    {0} *{1}; //// JSON: {2}", "NSArray", field.JsonToField, field.JsonFromField);
                    }
                    else
                    {
                        WriteLine("    {0} *{1}; //// JSON: {2}", field.JsonToType, field.JsonToField, field.JsonFromField);
                    }
                }

                WriteLine("}");

                foreach (var field in g)
                {
                    if (field.JsonToType.Contains("NSArray<"))
                    {
                        WriteLine("@property (nonatomic,retain) {0} *{1};", "NSArray", field.JsonToField);
                    }
                    else
                    {
                        WriteLine("@property (nonatomic,retain) {0} *{1};", field.JsonToType, field.JsonToField);
                    }
                }

                WriteLine();
                WriteLine("- (" + gen.Namespace + " *)objectMapping;");
                WriteLine();
                WriteLine("@end");
                WriteLine();
                WriteLine();


                //m file
                WriteLine("// JSON iOS Class Generator");
                WriteLine("// Written by Bruce Bao");
                WriteLine();

                WriteLine("#import \"{0}.h\"", g.Key);
                WriteLine("#import <MTFramework/MTFramework.h>");

                foreach (var field in g)
                {
                    if (field.JsonToType.Contains("NSArray"))
                    {
                        if (!field.JsonToType.Contains("NSNumber")
                        && !field.JsonToType.Contains("NSString"))
                        {
                            var type = field.JsonToType.Replace("NSArray<", "").Replace(">", "");
                            WriteLine("#import \"{0}.h\"", type);
                        }
                    }
                    else if (field.JsonToType != "NSNumber" && field.JsonToType != "NSString")
                    {
                        WriteLine("#import \"{0}.h\"", field.JsonToType);
                    }
                }

                WriteLine();
                WriteLine("@implementation {0}", g.Key);

                WriteLine();
                foreach (var field in g)
                {
                    WriteLine("@synthesize {0};", field.JsonToField);
                }
                WriteLine();

                WriteLine("- (" + gen.Namespace + " *)objectMapping {");
                WriteLine("    " + gen.Namespace + " *mapping = [" + gen.Namespace + " mappingForClass:[" + g.Key + " class]];");

                foreach (var field in g)
                {
                    if (field.JsonToType.Contains("NSArray<"))
                    {
                        var type = field.JsonToType.Replace("NSArray<", "").Replace(">", "");
                        WriteLine("    [mapping mapKeyPath:@\"" + field.JsonFromField + "\" toAttribute:@\"" + field.JsonToField + "\" withMapping:[" + type + " class]];");
                    }
                    else
                    {
                        WriteLine("    [mapping mapKeyPath:@\"" + field.JsonFromField + "\" toAttribute:@\"" + field.JsonToField + "\" withClass:[" + field.JsonToType + " class]];");
                    }
                }
                WriteLine("    return mapping;");
                WriteLine("}");

                WriteLine();
                WriteLine("- (void)dealloc {");

                foreach (var field in g)
                {
                    WriteLine("    [" + field.JsonToField + " release];");
                }

                WriteLine("    [super dealloc];");
                WriteLine("}");
                WriteLine();
                WriteLine("@end");
                WriteLine();
            }
        }

        protected void ddlMobileSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            var system = (MobileSystemEnum)Enum.Parse(typeof(MobileSystemEnum), ddlMobileSystem.SelectedValue);
            switch (system)
            {
                case MobileSystemEnum.IPhone:
                    lblVariable.InnerText = "Framework:";
                    txtVariable.Value = "MTObjectMapping";
                    break;
                case MobileSystemEnum.Android:
                    lblVariable.InnerText = "Package:";
                    txtVariable.Value = "";
                    break;
                case MobileSystemEnum.WP7:
                    lblVariable.InnerText = "Namespace:";
                    txtVariable.Value = "";
                    break;
                default:
                    break;
            }
        }

        #region Response WriteLine
        void WriteLine()
        {
            Response.Write("\r\n");
        }

        void WriteLine(string str)
        {
            Response.Write(str);
            Response.Write("\r\n");
        }

        void WriteLine(string str, string arg0)
        {
            var newStr = String.Format(str, arg0);
            Response.Write(newStr);
            Response.Write("\r\n");
        }

        void WriteLine(string str, string arg0, string arg1)
        {
            var newStr = String.Format(str, arg0, arg1);
            Response.Write(newStr);
            Response.Write("\r\n");
        }

        void WriteLine(string str, string arg0, string arg1, string arg2)
        {
            var newStr = String.Format(str, arg0, arg1, arg2);
            Response.Write(newStr);
            Response.Write("\r\n");
        }

        void WriteLine(string str, string arg0, string arg1, string arg2, string arg3)
        {
            var newStr = String.Format(str, arg0, arg1, arg2, arg3);
            Response.Write(newStr);
            Response.Write("\r\n");
        }
        #endregion
    }
}