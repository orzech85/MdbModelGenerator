using System.Data;
using System.Data.OleDb;

namespace MdbModelGenerator
{
    public static class DbTools
    {
        public static int ReadTable(string query, string conString, ref DataTable dataTable)
        {
            var myConnection = new OleDbConnection(conString);

            var adapter = new OleDbDataAdapter(query, myConnection);
            OleDbCommandBuilder oleDbCommandBuilder = new OleDbCommandBuilder(adapter);
            adapter.Fill(dataTable);

            return dataTable.Rows.Count;
        }
    }
}
