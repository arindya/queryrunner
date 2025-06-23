using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace queryrunner
{
    public partial class Form1 : Form
    {
        private string currentQuery = "";
        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            getquery.KeyDown += getquery_KeyDown;

            bgWorkerCopy.DoWork += bgWorkerCopy_DoWork;
            bgWorkerCopy.ProgressChanged += bgWorkerCopy_ProgressChanged;
            bgWorkerCopy.RunWorkerCompleted += bgWorkerCopy_RunWorkerCompleted;
            bgWorkerCopy.WorkerReportsProgress = true;
            bgWorkerCopy.WorkerSupportsCancellation = true;
            //bgWorkerCopy 
            bgWorkerQuery.DoWork += bgWorkerQuery_DoWork;
            bgWorkerQuery.RunWorkerCompleted += bgWorkerQuery_RunWorkerCompleted;
            bgWorkerQuery.WorkerSupportsCancellation = true;
            //btnCopyFiles.Visible = false;
            btnCopyFiles.Visible = false;
            btnCancel.Visible = false;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cbWhereClause.Visible = false;
            txtWhereValue.Visible = false;
            cbWhereClause.SelectedIndexChanged += cbWhereClause_SelectedIndexChanged;
            cbQueryMode.SelectedIndexChanged += cbQueryMode_SelectedIndexChanged;
            getpath.Text = Properties.Settings.Default.path;
            //getquery.Text = Properties.Settings.Default.query;
            getcopyto.Text = Properties.Settings.Default.pathcopyto;
            txtUserId.Text = Properties.Settings.Default.OracleUserId;
            txtPassword.Text = Properties.Settings.Default.OraclePassword;
            txtHost.Text = Properties.Settings.Default.OracleHost;
            txtPort.Text = Properties.Settings.Default.OraclePort;
            txtService.Text = Properties.Settings.Default.OracleService;
            // isi opsi query mode
            cbQueryMode.Items.AddRange(new string[]
            {
            "Query",
            "HKP",
            "WHP Otomatis"
            });
            cbQueryMode.SelectedIndex = 0;

            // isi where clause opsi
            cbWhereClause.Items.AddRange(new string[]
            {
            "Well Name Like",
            "Struc Name =",
            "Struc In Asset",
            "File Name Like",
            "Well Name In",
            "Struc Name In"
            });
            cbWhereClause.SelectedIndex = 0;
            cbWhereClause.TextChanged += (s, ev) =>
            {
                if (cbQueryMode.SelectedItem.ToString() == "WHP Otomatis")
                    UpdateGeneratedWHPQuery();
            };

            txtWhereValue.TextChanged += (s, ev) =>
            {
                if (cbQueryMode.SelectedItem.ToString() == "WHP Otomatis")
                    UpdateGeneratedWHPQuery();
            };

            // Pastikan style dropdown bisa diketik
            cbWhereClause.DropDownStyle = ComboBoxStyle.DropDown;
        }        
        private OracleConnection ConnectToOracle()
        {
            string userId = Properties.Settings.Default.OracleUserId;
            string password = Properties.Settings.Default.OraclePassword;
            string host = Properties.Settings.Default.OracleHost;
            string port = Properties.Settings.Default.OraclePort;
            string serviceName = Properties.Settings.Default.OracleService;

            string connectionString = $"User Id={userId};Password={password};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={serviceName})))";

            OracleConnection conn = new OracleConnection(connectionString);
            conn.Open();
            return conn;
        }
        private void btnexecute_Click(object sender, EventArgs e)
        {
            btnexecute.Enabled = false;
            btnCancel.Enabled = true;

            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = true;

            Application.DoEvents();

            try
            {
                // Tentukan mode berdasarkan ComboBox
                string selectedMode = cbQueryMode.SelectedItem.ToString();

                if (selectedMode == "Query")
                {
                    currentQuery = getquery.Text.Trim(); // manual input                    
                }
                else if (selectedMode == "WHP Otomatis")
                {
                    UpdateGeneratedWHPQuery();
                    
                }
                if (string.IsNullOrWhiteSpace(currentQuery))
                {
                    MessageBox.Show("Query kosong. Mohon isi terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Jalankan query count dulu untuk konfirmasi
                int rowCount = GetRowCount(currentQuery);

                DialogResult result = MessageBox.Show(
                    $"Query akan dijalankan.\nJumlah data: {rowCount}\n\nLanjutkan?",
                    "Konfirmasi Jalankan Query",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                {
                    progressBar1.Visible = false;
                    btnexecute.Enabled = true;
                    btnCancel.Enabled = true;
                    return;
                }
                // Eksekusi query seperti biasa
                if (!bgWorkerQuery.IsBusy)
                    bgWorkerQuery.RunWorkerAsync();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                btnexecute.Enabled = true;
                btnCancel.Enabled = false;
                progressBar1.Visible = false;
            }            
        }
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
        private void cbQueryMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbQueryMode.SelectedItem.ToString();

            if (selected == "Query")
            {
                getquery.ReadOnly = false;
                getquery.Text = getquery.Text.Trim();
                cbWhereClause.Visible = false;
                txtWhereValue.Visible = false;
            }
            else if(selected == "HKP")
            {
                getquery.ReadOnly = false;
                cbWhereClause.Visible = false;
                txtWhereValue.Visible = false;
                UpdateGeneratedHKPQuery(); // load query terakhir          
            }
            else if (selected == "WHP Otomatis")
            {
                getquery.ReadOnly = true;
                // Tampilkan kontrol where
                cbWhereClause.Visible = true;
                txtWhereValue.Visible = true;
                UpdateGeneratedWHPQuery(); // tampilkan WHP query
            }
        }
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
        }
        private void getquery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnexecute.PerformClick();
            }
        }
        private string BuildWhereClause()
        {
            string selected = cbWhereClause.SelectedItem.ToString();
            string val = txtWhereValue.Text.Trim();

            switch (selected)
            {
                case "Well Name Like":
                    return $"well_name LIKE '{val}'";
                case "Struc Name =":
                    return $"lookup.get_structure_name(well_name) = '{val}'";
                case "Struc In Asset":
                    return $@"well_name IN (
                            SELECT well_name
                            FROM well
                            WHERE structure_s IN (
                                SELECT structure_s
                                FROM structure
                                WHERE aset_id IN {val}
                            )
                          )";
                case "File Name Like":
                    return $"(WHP_DOC_FILE_NAME LIKE '{val}' OR WHP_WS_FILE_NAME LIKE '{val}')";
                case "Well Name In":
                    return $"well_name IN {val}"; // val contoh: 'SPA001','SPA002'
                case "Struc Name In":
                    return $"lookup.get_structure_name(well_name) IN {val}";
                default:
                    return "1=1"; // fallback
            }
        }
        private void UpdateGeneratedHKPQuery()
        {
            string query = $@"
                with one as 
                ( select b.w_location well_name
                , replace(a.haf_file_path,'/','\') || '\' || a.haf_file_name as pathfile
                ,  a.haf_load_by as loadBy, to_char(a.haf_load_date,'dd-MON-yyyy') loadDate
                , 'HKP\'||HKP_ASSET_FLAG_DESC||'\'||replace(replace(b.w_location,'/','_'),' ','')||'\'||decode(HAF_TYPE,'0','DOKUMEN','1','PETA',HAF_TYPE)||'\' as FOLDER
                , NULL as FROM_TAB
                , NULL as TAB_ROWID  from hkp_asset_files a
                , hkp_asset b
                , hkp_asset_flag c where c.HKP_ASSET_FLAG = b.HA_ASSET_FLAG and a.ha_s = b.ha_s 
                --AND (B.W_LOCATION LIKE '%MC-8%' OR  B.W_LOCATION LIKE '%LMC-8%' )
                AND B.W_LOCATION IN (
                'EMP.SP-III.BNG',
                'BNG-DL-3',
                'BNG-HH-1',
                'BNG-V',
                'BNG-DL-8',
                'BNG-BBL-6'
                )
                 union all 
                 select b.w_location well_name
                 , replace(a.haod_file_path,'/','\') || '\' || a.haod_file_name as pathfile
                 , a.haod_loaded_by as loadBy, to_char(a.haod_loaded_date,'dd-MON-yyyy') loadDate
                 , 'HKP\'||HKP_ASSET_FLAG_DESC||'\'||replace(replace(b.w_location,'/','_'),' ','_')||'\' as FOLDER
                 , NULL as FROM_TAB
                 , NULL as TAB_ROWID from hkp_asset_owner_detail a
                 , hkp_asset b, hkp_asset_flag c where c.HKP_ASSET_FLAG = b.HA_ASSET_FLAG and a.ha_s = b.ha_s 
                --AND (B.W_LOCATION LIKE '%MC-8%' OR  B.W_LOCATION LIKE '%LMC-8%' ) 
                AND B.W_LOCATION IN (
                'EMP.SP-III.BNG',
                'BNG-DL-3',
                'BNG-HH-1',
                'BNG-V',
                'BNG-DL-8',
                'BNG-BBL-6'

                )
                 ) 
                 select well_name, replace(upper(pathfile),'\DIGDAT','DIGDAT') pathfile, loadby, loaddate, FOLDER, FROM_TAB, TAB_ROWID from one order by 1, 2
                ";

            getquery.Text = query;
            currentQuery = query;
        }
        private void UpdateGeneratedWHPQuery()
        {
            string whereClause = BuildWhereClause();

            string query = $@"
            WITH one AS (
                SELECT
                    well_name,
                    'DIGDAT\WellHistoryProfile\Word\' || WHP_DOC_FILE_NAME AS pathfile,
                    WHP_CREATED_BY AS loadBy,
                    TO_CHAR(WHP_CREATED_DATE, 'dd-MON-yyyy') AS loadDate,
                    lookup.get_aset_name(lookup.get_structure_name(well_name)) || '\' ||
                    lookup.get_structure_name(well_name) || '\' || well_name || '\' || 'WHP' AS FOLDER,
                    NULL AS FROM_TAB,
                    NULL AS TAB_ROWID
                FROM
                    well_history_profile a
                WHERE
                    WHP_LAST_UPDATE = (
                        SELECT MAX(WHP_LAST_UPDATE)
                        FROM WELL_HISTORY_PROFILE
                        WHERE well_name = a.well_name
                    )
                    AND WHP_DOC_FILE_NAME IS NOT NULL
                    AND {whereClause}

                UNION ALL

                SELECT
                    well_name,
                    'DIGDAT\WellHistoryProfile\Excel\' || WHP_WS_FILE_NAME AS pathfile,
                    WHP_CREATED_BY AS loadBy,
                    TO_CHAR(WHP_CREATED_DATE, 'dd-MON-yyyy') AS loadDate,
                    lookup.get_aset_name(lookup.get_structure_name(well_name)) || '\' ||
                    lookup.get_structure_name(well_name) || '\' || well_name || '\' || 'WHP' AS FOLDER,
                    NULL AS FROM_TAB,
                    NULL AS TAB_ROWID
                FROM
                    well_history_profile a
                WHERE
                    WHP_LAST_UPDATE = (
                        SELECT MAX(WHP_LAST_UPDATE)
                        FROM WELL_HISTORY_PROFILE
                        WHERE well_name = a.well_name
                    )
                    AND WHP_WS_FILE_NAME IS NOT NULL
                    AND {whereClause}
            )
            SELECT * FROM one ORDER BY 1, 2";

            getquery.Text = query;
            currentQuery = query;
        }
        private void btnCopyFiles_Click(object sender, EventArgs e)
        {
            btnCancel.Visible = true;
            btnCancel.Enabled = true;
            var result = MessageBox.Show(
                        "Apakah Anda yakin ingin menyalin semua file sekarang?",
                        "Konfirmasi Copy File",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

            // Jika user pilih "No", hentikan
            if (result != DialogResult.Yes)
                return;
            if (!bgWorkerCopy.IsBusy)
            {
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                btnCopyFiles.Enabled = false;
                btnexecute.Enabled = false;
                bgWorkerCopy.RunWorkerAsync();
            }
        }
        private void bgWorkerCopy_DoWork(object sender, DoWorkEventArgs e)
        {
            string sourceRoot = getpath.Text.Trim();
            string targetRoot = getcopyto.Text.Trim()+"\\" ;

            if (!Directory.Exists(targetRoot))
                Directory.CreateDirectory(targetRoot);

            int copied = 0, failed = 0;
            int total = dataGridView1.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);

            int index = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (bgWorkerCopy.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                if (row.IsNewRow) continue;
                string relativePath = row.Cells["PATHFILE"].Value?.ToString();
                string folder = row.Cells["FOLDER"].Value?.ToString() ?? "";
                string wellName = row.Cells["WELL_NAME"].Value?.ToString() ?? "";
                if (string.IsNullOrEmpty(relativePath)) continue;
                string sourceFile = Path.Combine(sourceRoot, relativePath);
                string fileName = Path.GetFileName(relativePath); // ✅ ambil nama file
                string targetDir = Path.Combine(targetRoot, folder); // ✅ folder tujuan lengkap
                string targetFile = Path.Combine(targetDir, fileName); // ✅ nama file ikut

                try
                {
                    //string targetDir = Path.GetDirectoryName(targetFile);
                    if (!Directory.Exists(targetDir))
                        Directory.CreateDirectory(targetDir);

                    //File.Copy(sourceFile, targetFile, true);
                    bool success = CopyFileWithCancel(sourceFile, targetFile, bgWorkerCopy);

                    if (success)
                    {
                        copied++;
                        this.Invoke(new Action(() =>
                        {
                            row.Cells["Status"].Value = "Copied";
                            row.Cells["Statuspath"].Value = targetFile;
                        }));
                    }
                    else
                    {
                        failed++;
                        this.Invoke(new Action(() =>
                        {
                            row.Cells["Status"].Value = "Cancelled";
                            row.Cells["Statuspath"].Value = "Cancelled by user";
                        }));
                        e.Cancel = true;
                        return;
                    }
                    copied++;

                    this.Invoke(new Action(() =>
                    {
                        row.Cells["Status"].Value = "Copied";
                        row.Cells["Statuspath"].Value = targetFile;
                    }));
                }
                catch (Exception ex)
                {
                    failed++;

                    this.Invoke(new Action(() =>
                    {
                        row.Cells["Status"].Value = "Failed";
                        row.Cells["Statuspath"].Value = sourceFile + " - " + ex.Message;
                    }));
                }

                index++;
                int progress = (index * 100) / total;
                bgWorkerCopy.ReportProgress(progress);
            }

            e.Result = new { Copied = copied, Failed = failed };
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
            {
                MessageBox.Show("Proses copy dibatalkan oleh pengguna.");
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("Terjadi kesalahan saat menyalin file:\n" + e.Error.Message);
                return;
            }

            dynamic result = e.Result;
            MessageBox.Show($"Proses selesai!\nBerhasil disalin: {result.Copied}\nGagal disalin: {result.Failed}");
        }        
        private void toolStripBtnOpenRep_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "REP Files (*.rep)|*.rep|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string content = RepParser.ExtractSqlFromRep(ofd.FileName);
                getquery.Text = content;
            }
        }        
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && Path.GetExtension(files[0]).Equals(".rep", StringComparison.OrdinalIgnoreCase))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string repFile = files[0];
                string sql = RepParser.ExtractSqlFromRep(repFile); // Panggil class baru
                getquery.Text = sql;
                MessageBox.Show("File .REP berhasil dimuat.");
            }
        }
        private void bgWorkerQuery_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (OracleConnection conn = ConnectToOracle())
                using (OracleDataAdapter adapter = new OracleDataAdapter(currentQuery, conn))
                {
                    DataTable dt = new DataTable();

                    adapter.Fill(dt); // NOTE: ini tidak bisa dibatalkan saat sedang loading besar

                    if (bgWorkerQuery.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    // Tambah kolom No, Status, Statuspath
                    dt.Columns.Add("No", typeof(int));
                    int counter = 1;
                    foreach (DataRow row in dt.Rows)
                        row["No"] = counter++;
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Statuspath");
                    dt.Columns["No"].SetOrdinal(0);

                    e.Result = dt;
                }
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }
        private void bgWorkerQuery_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Visible = false;
            btnexecute.Enabled = true;
            btnCancel.Enabled = false;

            if (e.Cancelled)
            {
                MessageBox.Show("Query dibatalkan.");
            }
            else if (e.Result is Exception)
            {
                MessageBox.Show("Error: " + ((Exception)e.Result).Message);
            }
            else
            {
                dataGridView1.DataSource = (DataTable)e.Result;
                btnCopyFiles.Visible = true;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bgWorkerCopy.IsBusy && bgWorkerCopy.WorkerSupportsCancellation)
            {
                bgWorkerCopy.CancelAsync();
            }
        }
        private int GetRowCount(string fullQuery)
        {
            try
            {
                string countQuery = $"SELECT COUNT(*) FROM ({fullQuery})";
                using (OracleConnection conn = ConnectToOracle())
                using (OracleCommand cmd = new OracleCommand(countQuery, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch
            {
                return -1; // Jika error hitung
            }
        }
        private bool CopyFileWithCancel(string sourceFile, string targetFile, BackgroundWorker worker)
        {
            const int bufferSize = 81920; // 80 KB
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            using (FileStream targetStream = new FileStream(targetFile, FileMode.Create, FileAccess.Write))
            {
                while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (worker.CancellationPending)
                        return false;

                    targetStream.Write(buffer, 0, bytesRead);
                }
            }

            return true;
        }
    }
}
