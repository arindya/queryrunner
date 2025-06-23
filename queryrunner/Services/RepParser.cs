using System;
using System.IO;
using System.Text;

namespace queryrunner
{
    public static class RepParser
    {
        public static string ExtractSqlFromRep(string filePath)
        {
            if (!File.Exists(filePath))
                return "";

            StringBuilder sb = new StringBuilder();
            bool insideSqlSection = false;

            foreach (var line in File.ReadLines(filePath))
            {
                if (line.Trim() == "[SQL]")
                {
                    insideSqlSection = true;
                    continue;
                }

                if (line.StartsWith("[") && insideSqlSection)
                {
                    // Keluar dari bagian SQL jika masuk ke section lain
                    break;
                }

                if (insideSqlSection)
                {
                    sb.AppendLine(line);
                }
            }

            return sb.ToString().Trim();
        }
    }
}
