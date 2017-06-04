using System.Data;
using System.IO;
using System.Text;

namespace MdbModelGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                return;

            string dbPath = args[0];

            for (int i = 1; i < args.Length; i++)
            {
                GetTableFromMdb(dbPath, args[i]);
            }
        }

        static void GetTableFromMdb(string dbPath, string tableName)
        {
            string ConString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + dbPath;

            DataTable dataTable = new DataTable();

            DbTools.ReadTable($"SELECT * FROM {tableName}", ConString, ref dataTable);
            DataColumnCollection columns = dataTable.Columns;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine();

            sb.AppendLine("namespace MdbModel");
            sb.AppendLine("{");

            sb.AppendLine($"    public class {tableName}");
            sb.AppendLine("    {");

            foreach (var item in columns)
            {
                var column = columns[item.ToString()];

                sb.AppendLine("        public " + column.DataType.Name + " " + item.ToString() + " { get; set; }");
            }
            sb.AppendLine("    }");
            sb.AppendLine("}");

            using (StreamWriter w = File.CreateText($"{tableName}.cs"))
            {
                w.Write(sb.ToString());
            }
        }
    }
}
