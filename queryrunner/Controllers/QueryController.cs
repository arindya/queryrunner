using Oracle.ManagedDataAccess.Client;
//using queryrunner.Models;
using queryrunner.Services;
using System;
using System.Data;
using System.Windows.Forms;
using queryrunner.Services;

namespace queryrunner.Controllers
{
    public static class QueryController
    {
        private static string _currentQuery;

        public static string GetCurrentQuery() => _currentQuery;

        public static void SetManualQuery(string rawQuery)
        {
            _currentQuery = rawQuery?.Trim();
        }

        public static void GenerateHKPQuery()
        {
            _currentQuery = QueryBuilder.BuildHKPQuery();
        }

        public static void GenerateWHPQuery(string whereClause)
        {
            _currentQuery = QueryBuilder.BuildWHPQuery(whereClause);
        }

        public static int GetRowCount()
        {
            if (string.IsNullOrWhiteSpace(_currentQuery))
                return 0;

            return OracleService.ExecuteScalarCount(_currentQuery);
        }

        public static DataTable ExecuteQuery()
        {
            if (string.IsNullOrWhiteSpace(_currentQuery))
                throw new InvalidOperationException("Query belum diset.");

            using (var conn = OracleService.Connect())
            using (var adapter = new OracleDataAdapter(_currentQuery, conn))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Tambah kolom info
                dt.Columns.Add("No", typeof(int));
                int no = 1;
                foreach (DataRow row in dt.Rows)
                    row["No"] = no++;
                dt.Columns.Add("Status");
                dt.Columns.Add("Statuspath");
                dt.Columns["No"].SetOrdinal(0);

                return dt;
            }
        }
        public static string LoadRepToQuery(string filePath)
        {
            return RepParser.ExtractSqlFromRep(filePath);
        }

    }
}
