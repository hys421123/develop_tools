namespace JsonCSharpClassGenerator
{
    partial class frmCSharpClassGeneration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCSharpClassGeneration));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.edtJson = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.edtTargetFolder = new System.Windows.Forms.TextBox();
            this.edtNamespace = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dgFieldMappings = new System.Windows.Forms.DataGridView();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JsonEntityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JsonFromField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JsonToField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JsonToType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cboSystem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMobileAPI = new System.Windows.Forms.TextBox();
            this.btnImportXML = new System.Windows.Forms.Button();
            this.btnExportXML = new System.Windows.Forms.Button();
            this.dataSet1 = new System.Data.DataSet();
            this.dsXML = new System.Data.DataSet();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboCapitalFirstLetter = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgFieldMappings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsXML)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(634, 395);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 21);
            this.btnGenerate.TabIndex = 10;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // edtJson
            // 
            this.edtJson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.edtJson.Location = new System.Drawing.Point(11, 152);
            this.edtJson.MaxLength = 10000000;
            this.edtJson.Multiline = true;
            this.edtJson.Name = "edtJson";
            this.edtJson.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.edtJson.Size = new System.Drawing.Size(285, 234);
            this.edtJson.TabIndex = 9;
            this.edtJson.KeyUp += new System.Windows.Forms.KeyEventHandler(this.edtJson_KeyUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Generate classes from sample JSON:";
            // 
            // lblNamespace
            // 
            this.lblNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.Location = new System.Drawing.Point(7, 44);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(113, 12);
            this.lblNamespace.TabIndex = 4;
            this.lblNamespace.Text = "Namespace/package:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(715, 395);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 21);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(269, 94);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 21);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "Target folder:";
            // 
            // edtTargetFolder
            // 
            this.edtTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.edtTargetFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.edtTargetFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.edtTargetFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JsonCSharpClassGenerator.Properties.Settings.Default, "TargetFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtTargetFolder.Location = new System.Drawing.Point(126, 95);
            this.edtTargetFolder.Name = "edtTargetFolder";
            this.edtTargetFolder.Size = new System.Drawing.Size(139, 21);
            this.edtTargetFolder.TabIndex = 2;
            this.edtTargetFolder.Text = global::JsonCSharpClassGenerator.Properties.Settings.Default.TargetFolder;
            // 
            // edtNamespace
            // 
            this.edtNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.edtNamespace.Location = new System.Drawing.Point(126, 41);
            this.edtNamespace.Name = "edtNamespace";
            this.edtNamespace.Size = new System.Drawing.Size(169, 21);
            this.edtNamespace.TabIndex = 0;
            this.edtNamespace.Text = "MTObjectMapping";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(363, 23);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // dgFieldMappings
            // 
            this.dgFieldMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFieldMappings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgFieldMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFieldMappings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Level,
            this.JsonEntityName,
            this.JsonFromField,
            this.JsonToField,
            this.JsonToType});
            this.dgFieldMappings.Location = new System.Drawing.Point(316, 29);
            this.dgFieldMappings.Name = "dgFieldMappings";
            this.dgFieldMappings.RowTemplate.Height = 23;
            this.dgFieldMappings.Size = new System.Drawing.Size(474, 360);
            this.dgFieldMappings.TabIndex = 35;
            this.dgFieldMappings.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgFieldMappings_CellBeginEdit);
            this.dgFieldMappings.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFieldMappings_CellEndEdit);
            // 
            // Level
            // 
            this.Level.HeaderText = "层级";
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            // 
            // JsonEntityName
            // 
            this.JsonEntityName.HeaderText = "Json实体名";
            this.JsonEntityName.MaxInputLength = 100;
            this.JsonEntityName.Name = "JsonEntityName";
            // 
            // JsonFromField
            // 
            this.JsonFromField.HeaderText = "Json原始字段";
            this.JsonFromField.MaxInputLength = 100;
            this.JsonFromField.Name = "JsonFromField";
            // 
            // JsonToField
            // 
            this.JsonToField.HeaderText = "Json有意义的字段";
            this.JsonToField.MaxInputLength = 100;
            this.JsonToField.Name = "JsonToField";
            // 
            // JsonToType
            // 
            this.JsonToType.HeaderText = "类型";
            this.JsonToType.MaxInputLength = 100;
            this.JsonToType.Name = "JsonToType";
            this.JsonToType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.JsonToType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(314, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "定义映射关系：";
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(145, 392);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 39;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cboSystem
            // 
            this.cboSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cboSystem.FormattingEnabled = true;
            this.cboSystem.Location = new System.Drawing.Point(126, 12);
            this.cboSystem.Name = "cboSystem";
            this.cboSystem.Size = new System.Drawing.Size(49, 20);
            this.cboSystem.TabIndex = 40;
            this.cboSystem.Text = "iOS";
            this.cboSystem.SelectedIndexChanged += new System.EventHandler(this.cboSystem_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 41;
            this.label3.Text = "System:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 43;
            this.label5.Text = "Mobile API:";
            // 
            // txtMobileAPI
            // 
            this.txtMobileAPI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMobileAPI.Location = new System.Drawing.Point(126, 66);
            this.txtMobileAPI.Name = "txtMobileAPI";
            this.txtMobileAPI.Size = new System.Drawing.Size(169, 21);
            this.txtMobileAPI.TabIndex = 42;
            // 
            // btnImportXML
            // 
            this.btnImportXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportXML.Location = new System.Drawing.Point(472, 395);
            this.btnImportXML.Name = "btnImportXML";
            this.btnImportXML.Size = new System.Drawing.Size(75, 23);
            this.btnImportXML.TabIndex = 44;
            this.btnImportXML.Text = "Import XML";
            this.btnImportXML.UseVisualStyleBackColor = true;
            this.btnImportXML.Click += new System.EventHandler(this.btnImportXML_Click);
            // 
            // btnExportXML
            // 
            this.btnExportXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportXML.Location = new System.Drawing.Point(553, 395);
            this.btnExportXML.Name = "btnExportXML";
            this.btnExportXML.Size = new System.Drawing.Size(75, 23);
            this.btnExportXML.TabIndex = 45;
            this.btnExportXML.Text = "Export XML";
            this.btnExportXML.UseVisualStyleBackColor = true;
            this.btnExportXML.Click += new System.EventHandler(this.btnExportXML_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // dsXML
            // 
            this.dsXML.DataSetName = "NewDataSet";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(226, 391);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 46;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.cboCapitalFirstLetter);
            this.panel1.Controls.Add(this.cboSystem);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.edtJson);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.edtNamespace);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblNamespace);
            this.panel1.Controls.Add(this.txtMobileAPI);
            this.panel1.Controls.Add(this.edtTargetFolder);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Location = new System.Drawing.Point(1, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 425);
            this.panel1.TabIndex = 47;
            // 
            // cboCapitalFirstLetter
            // 
            this.cboCapitalFirstLetter.AutoSize = true;
            this.cboCapitalFirstLetter.Location = new System.Drawing.Point(215, 14);
            this.cboCapitalFirstLetter.Name = "cboCapitalFirstLetter";
            this.cboCapitalFirstLetter.Size = new System.Drawing.Size(84, 16);
            this.cboCapitalFirstLetter.TabIndex = 47;
            this.cboCapitalFirstLetter.Text = "首字母大写";
            this.cboCapitalFirstLetter.UseVisualStyleBackColor = true;
            // 
            // frmCSharpClassGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(803, 428);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExportXML);
            this.Controls.Add(this.btnImportXML);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgFieldMappings);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGenerate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCSharpClassGeneration";
            this.Text = "JSON Class Generator";
            this.Load += new System.EventHandler(this.frmCSharpClassGeneration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFieldMappings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsXML)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox edtJson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox edtTargetFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridView dgFieldMappings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox cboSystem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMobileAPI;
        private System.Windows.Forms.Button btnImportXML;
        private System.Windows.Forms.Button btnExportXML;
        private System.Data.DataSet dataSet1;
        private System.Data.DataSet dsXML;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn JsonEntityName;
        private System.Windows.Forms.DataGridViewTextBoxColumn JsonFromField;
        private System.Windows.Forms.DataGridViewTextBoxColumn JsonToField;
        private System.Windows.Forms.DataGridViewTextBoxColumn JsonToType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cboCapitalFirstLetter;
    }
}

