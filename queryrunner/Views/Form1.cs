﻿//'+----------------------------------+
//'|  Created By  : M.Sultan AlFarid  |
//'|  Created Date: 2025/06/12        |
//'+----------------------------------+
using queryrunner.Controllers;
using queryrunner.Services;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace queryrunner.Views
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Mengaktifkan fitur drag & drop file REP
            this.AllowDrop = true;
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            getquery.KeyDown += getquery_KeyDown;

            // Setup BackgroundWorker untuk file copy
            bgWorkerCopy.DoWork += bgWorkerCopy_DoWork;
            bgWorkerCopy.ProgressChanged += bgWorkerCopy_ProgressChanged;
            bgWorkerCopy.RunWorkerCompleted += bgWorkerCopy_RunWorkerCompleted;
            bgWorkerCopy.WorkerReportsProgress = true;
            bgWorkerCopy.WorkerSupportsCancellation = true;

            // Tombol test koneksi Oracle
            this.btnTestKoneksi.Click += new System.EventHandler(this.btnTestKoneksi_Click);

            // Setup BackgroundWorker untuk eksekusi query
            bgWorkerQuery.DoWork += bgWorkerQuery_DoWork;
            bgWorkerQuery.RunWorkerCompleted += bgWorkerQuery_RunWorkerCompleted;
            bgWorkerQuery.WorkerSupportsCancellation = true;

            // Tombol copy dan cancel tidak langsung tampil
            btnCopyFiles.Visible = false;
            btnCancel.Visible = false;

            //setup cbwhere clause psg
            //cbWhereClause.SelectedIndexChanged += cbWhereClause_SelectedIndexChanged;
            this.toolStripBtnOpenRep.Click += new System.EventHandler(this.toolStripBtnOpenReppler_Click);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Inisialisasi opsi query mode
            cbQueryMode.Items.AddRange(new string[] { "Query", "HKP", "WHP Otomatis", "WELL LOG", "WELL FILE" });
            cbQueryMode.SelectedIndex = 0;
            cbQueryMode.SelectedIndexChanged += cbQueryMode_SelectedIndexChanged;

            // Inisialisasi opsi filter where clause
            cbWhereClause.Items.AddRange(new string[] {
                "Well Name Like", "Struc Name =", "Struc In Asset",
                "File Name Like", "Well Name In", "Struc Name In"
            });
            cbWhereClause.DropDownStyle = ComboBoxStyle.DropDown;
            cbWhereClause.SelectedIndex = 0;
            cbWhereClause.SelectedIndexChanged += cbWhereClause_SelectedIndexChanged;
            // Tampilkan hanya saat WHP Otomatis
            cbWhereClause.Visible = false;
            txtWhereValue.Visible = false;
            //Inisiasi clause
            boxclause.Items.AddRange(new string[] {
               "Null", "WF LIKE", "WF IN"
            });
            boxclause.SelectedIndexChanged += txtclause_SelectedIndexChanged;
            //Tampilan hanya saat wellfile
            boxclause.Visible = false;
            txtclause.Visible = false;
            // Tambahkan ini untuk sinkronisasi query manual
            getquery.TextChanged += (s, ex) =>
            {
                if (cbQueryMode.SelectedItem?.ToString() == "Query")
                    QueryController.SetManualQuery(getquery.Text);
            };

            // Update query otomatis jika isian berubah
            txtWhereValue.TextChanged += (s, ev) => UpdateQueryIfAuto();
            //Update query juga boy
            txtclause.TextChanged += (s,ev) => UpdateQueryIfAuto();

            // Pastikan default value langsung muncul (SPA%)
            cbWhereClause_SelectedIndexChanged(cbWhereClause, EventArgs.Empty);

            // Trigger default query WHP/HKP/Query
            cbQueryMode_SelectedIndexChanged(cbQueryMode, EventArgs.Empty);

            // Load konfigurasi
            getpath.Text = Properties.Settings.Default.path;
            getcopyto.Text = Properties.Settings.Default.pathcopyto;
            txtUserId.Text = Properties.Settings.Default.OracleUserId;
            txtPassword.Text = Properties.Settings.Default.OraclePassword;
            txtHost.Text = Properties.Settings.Default.OracleHost;
            txtPort.Text = Properties.Settings.Default.OraclePort;
            txtService.Text = Properties.Settings.Default.OracleService;
        }
        private void UpdateQueryIfAuto()
        {
            string whereClause = QueryBuilder.BuildWhereClause(cbWhereClause.Text, txtWhereValue.Text, cbQueryMode.Text);

            if (cbQueryMode.SelectedItem?.ToString() == "WHP Otomatis")
            {
                QueryController.GenerateWHPQuery(whereClause);
            }
            else if (cbQueryMode.SelectedItem?.ToString() == "WELL LOG")
            {
                QueryController.GenerateWELLLOGQuery(whereClause, cbWhereClause.Text);
            }
            else if (cbQueryMode.SelectedItem?.ToString() == "WELL FILE")
            {
                QueryController.GenerateWELLFILE(whereClause, boxclause.Text, txtclause.Text);
            }

            getquery.Text = QueryController.GetCurrentQuery();
        }
        // Simpan setting saat form ditutup
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.query = getquery.Text;
            Properties.Settings.Default.path = getpath.Text;
            Properties.Settings.Default.pathcopyto = getcopyto.Text;
            Properties.Settings.Default.OracleUserId = txtUserId.Text;
            Properties.Settings.Default.OraclePassword = txtPassword.Text;
            Properties.Settings.Default.OracleHost = txtHost.Text;
            Properties.Settings.Default.OraclePort = txtPort.Text;
            Properties.Settings.Default.OracleService = txtService.Text;
            Properties.Settings.Default.Save();
        }
        // Handle perubahan mode query
        private void cbQueryMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbQueryMode.Text;
            getquery.ReadOnly = selected != "Query" && selected != "HKP";
            cbWhereClause.Visible = selected == "WHP Otomatis" || selected == "WELL LOG" || selected == "WELL FILE";
            txtWhereValue.Visible = selected == "WHP Otomatis" || selected == "WELL LOG" || selected == "WELL FILE";
            boxclause.Visible = selected == "WELL FILE";
            txtclause.Visible = selected == "WELL FILE";
            string whereClause = QueryBuilder.BuildWhereClause(cbWhereClause.Text, txtWhereValue.Text, cbQueryMode.Text);


            if (selected == "Query")
                QueryController.SetManualQuery(getquery.Text);
            else if (selected == "HKP")
                QueryController.GenerateHKPQuery();
            else if (selected == "WHP Otomatis")
                QueryController.GenerateWHPQuery(whereClause);
            else if (selected == "WELL LOG")
                QueryController.GenerateWELLLOGQuery(whereClause, cbWhereClause.Text);
            else if (selected == "WELL FILE")
                QueryController.GenerateWELLFILE(whereClause, boxclause.Text, txtclause.Text);
            getquery.Text = QueryController.GetCurrentQuery();
        }
        // Tombol eksekusi query
        private void btnexecute_Click(object sender, EventArgs e)
        {
            btnexecute.Enabled = false;
            btnCancel.Enabled = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = true;

            try
            {
                string query = QueryController.GetCurrentQuery();
                int rowCount = QueryController.GetRowCount();
                using (var conn = OracleService.Connect())
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        if (string.IsNullOrWhiteSpace(query))
                        {
                            MessageBox.Show("Query kosong.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            btnexecute.Enabled = true;
                            btnCancel.Enabled = false;
                            progressBar1.Visible = false;
                            return;
                        }


                        DialogResult confirm = MessageBox.Show(
                            $"Query akan dijalankan.\nJumlah data: {rowCount}\n\nLanjutkan?",
                            "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (confirm == DialogResult.Yes && !bgWorkerQuery.IsBusy)
                        {
                            bgWorkerQuery.RunWorkerAsync();
                        }
                        else
                        {
                            // Jika user pilih "No", tombol dieksekusi diaktifkan lagi
                            btnexecute.Enabled = true;
                            btnCancel.Enabled = false;
                            progressBar1.Visible = false;
                        }
                    }
                    //MessageBox.Show("✅ Koneksi berhasil!", "Koneksi Oracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        MessageBox.Show("❌ Gagal membuka koneksi.", "Koneksi Oracle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                        
                }             
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                btnexecute.Enabled = true;
                btnCancel.Enabled = false;
                progressBar1.Visible = false;
            }
        }

        // BackgroundWorker eksekusi query
        private void bgWorkerQuery_DoWork(object sender, DoWorkEventArgs e)
        {
            try { e.Result = QueryController.ExecuteQuery(); }
            catch (Exception ex) { e.Result = ex; }
        }

        private void bgWorkerQuery_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Visible = false;
            btnexecute.Enabled = true;
            btnCancel.Enabled = false;

            if (e.Cancelled)
                MessageBox.Show("Query dibatalkan.");
            else if (e.Result is Exception)
                MessageBox.Show("Error: " + ((Exception)e.Result).Message);
            else
            {
                dataGridView1.DataSource = (DataTable)e.Result;
                btnCopyFiles.Visible = true;
            }
        }

        // Tombol untuk menyalin file hasil query
        private void btnCopyFiles_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Copy semua file sekarang?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!bgWorkerCopy.IsBusy)
            {
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                btnCopyFiles.Enabled = false;
                btnexecute.Enabled = false;
                btnCancel.Visible = true;
                btnCancel.Enabled = true;
                bgWorkerCopy.RunWorkerAsync();
            }
        }

        private void bgWorkerCopy_DoWork(object sender, DoWorkEventArgs e)
        {
            string source = getpath.Text.Trim();
            string target = getcopyto.Text.Trim();

            var result = FileCopyService.CopyFilesWithStatus(dataGridView1, source, target, bgWorkerCopy);
            if (bgWorkerCopy.CancellationPending)
                e.Cancel = true;
            else
                e.Result = result;
        }

        private void bgWorkerCopy_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void bgWorkerCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
            btnCopyFiles.Enabled = true;
            btnexecute.Enabled = true;
            btnCancel.Visible = false;

            if (e.Cancelled)
                MessageBox.Show("Copy dibatalkan oleh pengguna.");
            else if (e.Error != null)
                MessageBox.Show("Kesalahan saat copy: " + e.Error.Message);
            else
            {
                dynamic result = e.Result;
                MessageBox.Show($"Selesai!\nCopied: {result.Copied}\nFailed: {result.Failed}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bgWorkerCopy.IsBusy && bgWorkerCopy.WorkerSupportsCancellation)
                bgWorkerCopy.CancelAsync();
        }

        // Event saat tombol Open REP ditekan
        private void toolStripBtnOpenReppler_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "REP Files (*.rep)|*.rep|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string sql = RepParser.ExtractSqlFromRep(ofd.FileName);
                    getquery.Text = sql;
                    QueryController.SetManualQuery(sql);
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && Path.GetExtension(files[0]).Equals(".rep", StringComparison.OrdinalIgnoreCase))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string sql = RepParser.ExtractSqlFromRep(files[0]);
                getquery.Text = sql;
                QueryController.SetManualQuery(sql);
                MessageBox.Show("File .REP berhasil dimuat.");
            }
        }

        private void getquery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnexecute.PerformClick();
            }
        }

        // Otomatis isi contoh value saat dropdown whereClause berubah
        private void cbWhereClause_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbWhereClause.SelectedItem.ToString();
            switch (selected)
            {
                case "Well Name Like":
                    txtWhereValue.Text = "SPA%";
                    break;
                case "Struc Name =":
                    txtWhereValue.Text = "SOPA";
                    break;
                case "Struc In Asset":
                    txtWhereValue.Text = "('PBM')";
                    break;
                case "File Name Like":
                    txtWhereValue.Text = "%SPA-013%";
                    break;
                case "Well Name In":
                    txtWhereValue.Text = "('SPA-001','SPA-002')";
                    break;
                case "Struc Name In":
                    txtWhereValue.Text = "('SOPA','DEWA')";
                    break;
                default:
                    txtWhereValue.Text = "";
                    break;
            }
            UpdateQueryIfAuto();
        }

        private void txtclause_SelectedIndexChanged(object sender,EventArgs e)
        {
            string selected = boxclause.SelectedItem.ToString();
            switch (selected)
            {
                case "Null":
                    txtclause.Text = "";
                    break;
                case "WF LIKE":
                    txtclause.Text = "B%";
                    break;   
                case "WF IN":
                    txtclause.Text = "('A1','B1')";
                    break;
                default:
                    txtWhereValue.Text = "";
                    break;
            }
            UpdateQueryIfAuto();
        }

        // Tombol test koneksi Oracle
        private void btnTestKoneksi_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.query = getquery.Text;
            Properties.Settings.Default.path = getpath.Text;
            Properties.Settings.Default.pathcopyto = getcopyto.Text;
            Properties.Settings.Default.OracleUserId = txtUserId.Text;
            Properties.Settings.Default.OraclePassword = txtPassword.Text;
            Properties.Settings.Default.OracleHost = txtHost.Text;
            Properties.Settings.Default.OraclePort = txtPort.Text;
            Properties.Settings.Default.OracleService = txtService.Text;
            Properties.Settings.Default.Save();
            try
            {
                using (var conn = OracleService.Connect())
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        MessageBox.Show("✅ Koneksi berhasil!", "Koneksi Oracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("❌ Gagal membuka koneksi.", "Koneksi Oracle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Gagal koneksi:\n" + ex.Message, "Koneksi Oracle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                if (dt.Columns.Contains("status"))
                    dt.Columns.Remove("status");

                if (dt.Columns.Contains("Statuspath"))
                    dt.Columns.Remove("Statuspath");
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = "HasilQuery.xlsx";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(dt, "Data");
                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("✅ Berhasil diexport ke Excel!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tidak ada data untuk diexport.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
