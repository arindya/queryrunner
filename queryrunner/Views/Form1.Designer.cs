namespace queryrunner.Views
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.txtclause = new System.Windows.Forms.TextBox();
            this.boxclause = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbQueryMode = new System.Windows.Forms.ComboBox();
            this.txtWhereValue = new System.Windows.Forms.TextBox();
            this.cbWhereClause = new System.Windows.Forms.ComboBox();
            this.TanBmax = new System.Windows.Forms.Label();
            this.btnCopyFiles = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnexecute = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.getquery = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTestKoneksi = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtService = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.getcopyto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.Label();
            this.getpath = new System.Windows.Forms.TextBox();
            this.bgWorkerCopy = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBtnOpenRep = new System.Windows.Forms.ToolStripMenuItem();
            this.bgWorkerQuery = new System.ComponentModel.BackgroundWorker();
            this.Setup = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.Setup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleName = "";
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Nirmala Text", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1067, 613);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.OldLace;
            this.tabPage1.Controls.Add(this.btnExportExcel);
            this.tabPage1.Controls.Add(this.txtclause);
            this.tabPage1.Controls.Add(this.boxclause);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.cbQueryMode);
            this.tabPage1.Controls.Add(this.txtWhereValue);
            this.tabPage1.Controls.Add(this.cbWhereClause);
            this.tabPage1.Controls.Add(this.TanBmax);
            this.tabPage1.Controls.Add(this.btnCopyFiles);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.btnexecute);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.getquery);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1059, 587);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportExcel.Location = new System.Drawing.Point(182, 558);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(82, 23);
            this.btnExportExcel.TabIndex = 12;
            this.btnExportExcel.Text = "Export Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // txtclause
            // 
            this.txtclause.Location = new System.Drawing.Point(540, 3);
            this.txtclause.Name = "txtclause";
            this.txtclause.Size = new System.Drawing.Size(146, 28);
            this.txtclause.TabIndex = 11;
            // 
            // boxclause
            // 
            this.boxclause.FormattingEnabled = true;
            this.boxclause.Location = new System.Drawing.Point(413, 5);
            this.boxclause.Name = "boxclause";
            this.boxclause.Size = new System.Drawing.Size(121, 21);
            this.boxclause.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(795, 559);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbQueryMode
            // 
            this.cbQueryMode.FormattingEnabled = true;
            this.cbQueryMode.Location = new System.Drawing.Point(7, 6);
            this.cbQueryMode.Name = "cbQueryMode";
            this.cbQueryMode.Size = new System.Drawing.Size(121, 21);
            this.cbQueryMode.TabIndex = 8;
            // 
            // txtWhereValue
            // 
            this.txtWhereValue.Location = new System.Drawing.Point(261, 3);
            this.txtWhereValue.Name = "txtWhereValue";
            this.txtWhereValue.Size = new System.Drawing.Size(146, 28);
            this.txtWhereValue.TabIndex = 7;
            // 
            // cbWhereClause
            // 
            this.cbWhereClause.FormattingEnabled = true;
            this.cbWhereClause.Location = new System.Drawing.Point(134, 6);
            this.cbWhereClause.Name = "cbWhereClause";
            this.cbWhereClause.Size = new System.Drawing.Size(121, 21);
            this.cbWhereClause.TabIndex = 6;
            // 
            // TanBmax
            // 
            this.TanBmax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TanBmax.AutoSize = true;
            this.TanBmax.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TanBmax.Location = new System.Drawing.Point(7, 536);
            this.TanBmax.Name = "TanBmax";
            this.TanBmax.Size = new System.Drawing.Size(60, 19);
            this.TanBmax.TabIndex = 5;
            this.TanBmax.Text = "TanBmax";
            // 
            // btnCopyFiles
            // 
            this.btnCopyFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyFiles.Location = new System.Drawing.Point(876, 559);
            this.btnCopyFiles.Name = "btnCopyFiles";
            this.btnCopyFiles.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCopyFiles.Size = new System.Drawing.Size(75, 23);
            this.btnCopyFiles.TabIndex = 4;
            this.btnCopyFiles.Text = "copy-file";
            this.btnCopyFiles.UseVisualStyleBackColor = true;
            this.btnCopyFiles.Click += new System.EventHandler(this.btnCopyFiles_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar1.Location = new System.Drawing.Point(7, 559);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(168, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // btnexecute
            // 
            this.btnexecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexecute.AutoSize = true;
            this.btnexecute.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnexecute.Location = new System.Drawing.Point(957, 559);
            this.btnexecute.Name = "btnexecute";
            this.btnexecute.Size = new System.Drawing.Size(75, 23);
            this.btnexecute.TabIndex = 2;
            this.btnexecute.Text = "execute";
            this.btnexecute.UseVisualStyleBackColor = true;
            this.btnexecute.Click += new System.EventHandler(this.btnexecute_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 243);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1047, 286);
            this.dataGridView1.TabIndex = 1;
            // 
            // getquery
            // 
            this.getquery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.getquery.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getquery.Location = new System.Drawing.Point(6, 32);
            this.getquery.Name = "getquery";
            this.getquery.Size = new System.Drawing.Size(1047, 205);
            this.getquery.TabIndex = 0;
            this.getquery.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Setup);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1059, 587);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Setting";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnTestKoneksi
            // 
            this.btnTestKoneksi.Location = new System.Drawing.Point(261, 282);
            this.btnTestKoneksi.Name = "btnTestKoneksi";
            this.btnTestKoneksi.Size = new System.Drawing.Size(75, 23);
            this.btnTestKoneksi.TabIndex = 22;
            this.btnTestKoneksi.Text = "Save";
            this.btnTestKoneksi.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(146, 233);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 21);
            this.label13.TabIndex = 21;
            this.label13.Text = ":";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(146, 199);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 21);
            this.label12.TabIndex = 20;
            this.label12.Text = ":";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(146, 165);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 21);
            this.label11.TabIndex = 19;
            this.label11.Text = ":";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(146, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 21);
            this.label10.TabIndex = 18;
            this.label10.Text = ":";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(146, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 21);
            this.label9.TabIndex = 17;
            this.label9.Text = ":";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(13, 240);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 21);
            this.label8.TabIndex = 16;
            this.label8.Text = "Service";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(13, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 21);
            this.label7.TabIndex = 15;
            this.label7.Text = "PORT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(13, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 21);
            this.label6.TabIndex = 14;
            this.label6.Text = "Host";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(13, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 21);
            this.label5.TabIndex = 13;
            this.label5.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(13, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "OracleUserId";
            // 
            // txtService
            // 
            this.txtService.Location = new System.Drawing.Point(167, 233);
            this.txtService.Name = "txtService";
            this.txtService.Size = new System.Drawing.Size(185, 28);
            this.txtService.TabIndex = 11;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(167, 199);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(185, 28);
            this.txtPort.TabIndex = 10;
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(167, 165);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(185, 28);
            this.txtHost.TabIndex = 9;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(166, 131);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(185, 28);
            this.txtPassword.TabIndex = 8;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(166, 97);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(185, 28);
            this.txtUserId.TabIndex = 7;
            // 
            // getcopyto
            // 
            this.getcopyto.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getcopyto.Location = new System.Drawing.Point(166, 64);
            this.getcopyto.Name = "getcopyto";
            this.getcopyto.Size = new System.Drawing.Size(185, 26);
            this.getcopyto.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(146, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(146, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "COPY TO";
            // 
            // path
            // 
            this.path.AutoSize = true;
            this.path.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.path.Location = new System.Drawing.Point(13, 37);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(110, 21);
            this.path.TabIndex = 1;
            this.path.Text = "PATH DIGDAT";
            // 
            // getpath
            // 
            this.getpath.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getpath.Location = new System.Drawing.Point(166, 32);
            this.getpath.Name = "getpath";
            this.getpath.Size = new System.Drawing.Size(185, 26);
            this.getpath.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1091, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnOpenRep});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // toolStripBtnOpenRep
            // 
            this.toolStripBtnOpenRep.Name = "toolStripBtnOpenRep";
            this.toolStripBtnOpenRep.Size = new System.Drawing.Size(129, 22);
            this.toolStripBtnOpenRep.Text = "Open here";
            this.toolStripBtnOpenRep.Click += new System.EventHandler(this.toolStripBtnOpenReppler_Click);
            // 
            // bgWorkerQuery
            // 
            this.bgWorkerQuery.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerQuery_DoWork);
            // 
            // Setup
            // 
            this.Setup.Controls.Add(this.txtService);
            this.Setup.Controls.Add(this.btnTestKoneksi);
            this.Setup.Controls.Add(this.getpath);
            this.Setup.Controls.Add(this.label13);
            this.Setup.Controls.Add(this.path);
            this.Setup.Controls.Add(this.label12);
            this.Setup.Controls.Add(this.label1);
            this.Setup.Controls.Add(this.label11);
            this.Setup.Controls.Add(this.label3);
            this.Setup.Controls.Add(this.label10);
            this.Setup.Controls.Add(this.label2);
            this.Setup.Controls.Add(this.label9);
            this.Setup.Controls.Add(this.getcopyto);
            this.Setup.Controls.Add(this.label8);
            this.Setup.Controls.Add(this.txtUserId);
            this.Setup.Controls.Add(this.label7);
            this.Setup.Controls.Add(this.txtPassword);
            this.Setup.Controls.Add(this.label6);
            this.Setup.Controls.Add(this.txtHost);
            this.Setup.Controls.Add(this.label5);
            this.Setup.Controls.Add(this.txtPort);
            this.Setup.Controls.Add(this.label4);
            this.Setup.Location = new System.Drawing.Point(19, 6);
            this.Setup.Name = "Setup";
            this.Setup.Size = new System.Drawing.Size(374, 330);
            this.Setup.TabIndex = 23;
            this.Setup.TabStop = false;
            this.Setup.Text = "Setup";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1091, 648);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Query Runner";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Setup.ResumeLayout(false);
            this.Setup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnexecute;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox getquery;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox getpath;
        private System.Windows.Forms.Label path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox getcopyto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCopyFiles;
        private System.ComponentModel.BackgroundWorker bgWorkerCopy;
        private System.Windows.Forms.Label TanBmax;
        private System.Windows.Forms.ComboBox cbWhereClause;
        private System.Windows.Forms.TextBox txtWhereValue;
        private System.Windows.Forms.ComboBox cbQueryMode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripBtnOpenRep;
        private System.ComponentModel.BackgroundWorker bgWorkerQuery;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtService;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnTestKoneksi;
        private System.Windows.Forms.TextBox txtclause;
        private System.Windows.Forms.ComboBox boxclause;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.GroupBox Setup;
    }
}

