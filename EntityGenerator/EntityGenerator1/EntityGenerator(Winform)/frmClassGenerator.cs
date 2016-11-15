using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace JsonCSharpClassGenerator
{
    public partial class frmCSharpClassGeneration : Form
    {
        public frmCSharpClassGeneration()
        {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            Program.InitAppServices();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var b = new FolderBrowserDialog())
            {
                b.ShowNewFolderButton = true;
                b.SelectedPath = edtTargetFolder.Text;
                b.Description = "Please select a folder where to save the generated files.";
                if (b.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    edtTargetFolder.Text = b.SelectedPath;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (edtTargetFolder.Text == string.Empty)
            {
                MessageBox.Show(this, "Please specify an output directory.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (edtNamespace.Text == string.Empty)
            {
                MessageBox.Show(this, "Please specify a namespace.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MobileSystem.System = cboSystem.Text;
            
            gen.Namespace = edtNamespace.Text;
            gen.TargetFolder = edtTargetFolder.Text;
            gen.MobileAPI = txtMobileAPI.Text.Trim();

            gen.MappingList = new List<MappingInfo>();
            for (int i = 0; i < dgFieldMappings.RowCount; i++)
            {
                if (dgFieldMappings.Rows[i].Cells[0].Value == null
                    || dgFieldMappings.Rows[i].Cells[1].Value == null
                    || dgFieldMappings.Rows[i].Cells[2].Value == null
                    || dgFieldMappings.Rows[i].Cells[3].Value == null
                    || dgFieldMappings.Rows[i].Cells[4].Value == null)
                    continue;

                gen.MappingList.Add(
                    new MappingInfo()
                    {
                        Level = Convert.ToInt16(dgFieldMappings.Rows[i].Cells[0].Value),
                        EntityName = dgFieldMappings.Rows[i].Cells[1].Value.ToString(),
                        JsonFromField = dgFieldMappings.Rows[i].Cells[2].Value.ToString(),
                        JsonToField = dgFieldMappings.Rows[i].Cells[3].Value.ToString(),
                        JsonToType = dgFieldMappings.Rows[i].Cells[4].Value.ToString(),
                    });
            }

            if (!Directory.Exists(gen.TargetFolder))
                Directory.CreateDirectory(gen.TargetFolder);

            try
            {
                if(cboSystem.Text=="WP7")
                    gen.GenerateCSharpClasses();
                else if(cboSystem.Text=="Android")
                    gen.GenerateJavaClasses();
                else if (cboSystem.Text == "iOS")
                    gen.GenerateOCClasses();

                MessageBox.Show(this, "The code has been generated successfully.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to generate the code: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        JsonClassGenerator gen;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (edtJson.Text.Trim() == "")
            {
                MessageBox.Show(this, "请输入Json字符串", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MobileSystem.System = cboSystem.Text;

            gen = new JsonClassGenerator();
            gen.JsonString = StringHelper.GetCleanText(edtJson.Text.Trim());
            gen.MainClass = @"MainClass";
            gen.MappingList = new List<MappingInfo>();
            gen.IsCapitalFirstLetter = cboCapitalFirstLetter.Checked;

            try
            {
                if (cboSystem.Text == "WP7")
                    gen.PrepareCSharpGridView();
                else if (cboSystem.Text == "Android")
                    gen.PrepareJavaGridView();
                else if (cboSystem.Text == "iOS")
                    gen.PrepareOCGridView();

                BindGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to generate the code: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void BindGridView()
        {
            dgFieldMappings.Rows.Clear();

            gen.MappingList = gen.MappingList.OrderBy(x => x.Level).ToList();
            dgFieldMappings.Rows.Add(gen.MappingList.Count);
            for (int i = 0; i < gen.MappingList.Count; i++)
            {
                dgFieldMappings.Rows[i].Cells[0].Value = gen.MappingList[i].Level;
                dgFieldMappings.Rows[i].Cells[1].Value = gen.MappingList[i].EntityName;
                dgFieldMappings.Rows[i].Cells[2].Value = gen.MappingList[i].JsonFromField;
                dgFieldMappings.Rows[i].Cells[3].Value = gen.MappingList[i].JsonToField;
                dgFieldMappings.Rows[i].Cells[4].Value = gen.MappingList[i].JsonToType;
            }
        }

        private void dgFieldMappings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var newValue = dgFieldMappings.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (e.ColumnIndex == 1)
            {
                var entityName = oldValue.ToString();
                for (int i = 0; i < gen.MappingList.Count; i++)
                {
                    if (dgFieldMappings.Rows[i].Cells[1].Value.ToString() == entityName)
                    {
                        dgFieldMappings.Rows[i].Cells[1].Value = newValue;
                    }
                }

                if (dgFieldMappings.Rows[e.RowIndex].Cells[0].Value.ToString() != "1")
                {
                    for (int i = 0; i < gen.MappingList.Count; i++)
                    {
                        var current = dgFieldMappings.Rows[i].Cells[4].Value.ToString();
                        if (current.Contains("<" + entityName + ">"))
                        {
                            dgFieldMappings.Rows[i].Cells[4].Value = current.Replace("<" + entityName + ">", "<" + newValue + ">");
                        }
                        if (current == entityName)
                        {
                            dgFieldMappings.Rows[i].Cells[4].Value = newValue;
                        }
                    }
                }
            }
        }

        object oldValue;
        private void dgFieldMappings_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldValue = dgFieldMappings.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        private void btnImportXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.Filter = "文本文件(.txt)|*.txt";
            openFile1.FilterIndex = 1;

            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK
                && openFile1.FileName.Length > 0)
            {
                dgFieldMappings.Rows.Clear();
                
                System.IO.StreamReader sr = new System.IO.StreamReader(openFile1.FileName, false);
                try
                {
                    string text = sr.ReadToEnd();
                    string[] rows = text.Split(';');

                    dgFieldMappings.Rows.Add(rows.Length - 1);

                    for (int i = 0; i < rows.Length - 1; i++)
                    {
                        string[] props = rows[i].Split(',');
                        for (int j = 0; j < props.Length; j++)
                        {
                            if(props[j].StartsWith(GridColumn_Level))
                            {
                                dgFieldMappings.Rows[i].Cells[0].Value = props[j].Substring(GridColumn_Level.Length + 1);
                            }
                            else if (props[j].StartsWith(GridColumn_JsonEntityName))
                            {
                                dgFieldMappings.Rows[i].Cells[1].Value = props[j].Substring(GridColumn_JsonEntityName.Length + 1);
                            }
                            else if (props[j].StartsWith(GridColumn_BeforeConvert))
                            {
                                dgFieldMappings.Rows[i].Cells[2].Value = props[j].Substring(GridColumn_BeforeConvert.Length + 1);
                            }
                            else if (props[j].StartsWith(GridColumn_AfterConvert))
                            {
                                dgFieldMappings.Rows[i].Cells[3].Value = props[j].Substring(GridColumn_AfterConvert.Length + 1);
                            }
                            else if (props[j].StartsWith(GridColumn_JsonType))
                            {
                                dgFieldMappings.Rows[i].Cells[4].Value = props[j].Substring(GridColumn_JsonType.Length + 1);
                            }
                        }

                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    sr.Close();
                }
            }
        }

        private void btnExportXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.Filter = "文本文件(.txt)|*.txt";
            saveFile1.FilterIndex = 1;

            int count = dgFieldMappings.Rows.Count;

            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK 
                && saveFile1.FileName.Length > 0)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile1.FileName, false);
                try
                {
                    for (int i = 0; i < count - 1; i++)
                    {
                        string str = String.Format("{0}:{1},{2}:{3},{4}:{5},{6}:{7},{8}:{9};",
                            GridColumn_Level, dgFieldMappings.Rows[i].Cells[0].Value.ToString(),
                            GridColumn_JsonEntityName, dgFieldMappings.Rows[i].Cells[1].Value.ToString(),
                            GridColumn_BeforeConvert, dgFieldMappings.Rows[i].Cells[2].Value.ToString(),
                            GridColumn_AfterConvert, dgFieldMappings.Rows[i].Cells[3].Value.ToString(),
                            GridColumn_JsonType, dgFieldMappings.Rows[i].Cells[4].Value.ToString());
                        sw.Write(str);
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        const string GridColumn_Level = "Level";
        const string GridColumn_JsonEntityName = "JsonEntityName";
        const string GridColumn_BeforeConvert = "BeforeConvert";
        const string GridColumn_AfterConvert = "AfterConvert";
        const string GridColumn_JsonType = "JsonType";

        private void btnClear_Click(object sender, EventArgs e)
        {
            edtJson.Text = "";
            dgFieldMappings.Rows.Clear();
        }

        private void edtJson_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void cboSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSystem.Text == cboItem_iOS)
            {
                cboCapitalFirstLetter.Checked = false;
            }
            else
            {
                cboCapitalFirstLetter.Checked = true;
            }
        }

        private void frmCSharpClassGeneration_Load(object sender, EventArgs e)
        {
            this.cboSystem.Items.AddRange(new object[] {
            cboItem_Android,
            cboItem_WP7,
            cboItem_iOS});
        }

        const string cboItem_Android = "Android";
        const string cboItem_WP7 = "WP7";
        const string cboItem_iOS = "iOS";
    }

    public static class MobileSystem
    {
        public static string System;
    }
}
