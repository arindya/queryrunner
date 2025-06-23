using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace queryrunner.Services
{
    public class FileCopyResult
    {
        public int Copied { get; set; }
        public int Failed { get; set; }
    }

    public static class FileCopyService
    {
        public static FileCopyResult CopyFilesWithStatus(
            DataGridView dataGrid,
            string sourceRoot,
            string targetRoot,
            BackgroundWorker worker)
        {
            int copied = 0, failed = 0;

            if (!Directory.Exists(targetRoot))
                Directory.CreateDirectory(targetRoot);

            int total = dataGrid.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
            int index = 0;

            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if (worker.CancellationPending)
                    return new FileCopyResult { Copied = copied, Failed = failed };

                if (row.IsNewRow) continue;

                string relativePath = row.Cells["PATHFILE"].Value?.ToString();
                string folder = row.Cells["FOLDER"].Value?.ToString() ?? "";
                if (string.IsNullOrEmpty(relativePath)) continue;

                string fileName = Path.GetFileName(relativePath);
                string sourceFile = Path.Combine(sourceRoot, relativePath);
                string targetDir = Path.Combine(targetRoot, folder);
                string targetFile = Path.Combine(targetDir, fileName);

                try
                {
                    if (!Directory.Exists(targetDir))
                        Directory.CreateDirectory(targetDir);

                    bool success = CopyFileWithCancel(sourceFile, targetFile, worker);

                    if (success)
                    {
                        copied++;
                        UpdateRowStatus(row, "Copied", targetFile);
                    }
                    else
                    {
                        failed++;
                        UpdateRowStatus(row, "Cancelled", "Cancelled by user");
                        return new FileCopyResult { Copied = copied, Failed = failed };
                    }
                }
                catch (Exception ex)
                {
                    failed++;
                    UpdateRowStatus(row, "Failed", sourceFile + " - " + ex.Message);
                }

                index++;
                int progress = (index * 100) / total;
                worker.ReportProgress(progress);
            }

            return new FileCopyResult { Copied = copied, Failed = failed };
        }

        private static void UpdateRowStatus(DataGridViewRow row, string status, string path)
        {
            row.DataGridView.Invoke(new Action(() =>
            {
                row.Cells["Status"].Value = status;
                row.Cells["Statuspath"].Value = path;
            }));
        }

        private static bool CopyFileWithCancel(string sourceFile, string targetFile, BackgroundWorker worker)
        {
            const int bufferSize = 81920;
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
